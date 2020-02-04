using AutoMapper;
using ChatClient.Core;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos.Auth;
using ChatClient.Core.Models.ViewModels.Auth;
using ChatClient.Core.Options;
using ChatClient.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoService _cryptoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUnitOfWork unitOfWork, ICryptoService cryptoService, IHttpContextAccessor httpContextAccessor, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtOptions = jwtOptions.Value;
            _cryptoService = cryptoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticatedUser> Authenticate()
        {
            User user = await GetUser();

            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            string token = authHeader.Split(' ').Last();

            return _mapper.Map<User, AuthenticatedUser>(user, options =>
            {
                options.Items["Token"] = token;
            });
        }

        public async Task<User> GetUser()
        {
            string id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _unitOfWork.UserRepository.GetUserById(int.Parse(id));
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _unitOfWork.UserRepository.IsEmailTaken(email);
        }

        public async Task<AuthenticatedUser> Login(LoginDto credentials)
        {
            // Look for user with given email address
            User user = await _unitOfWork.UserRepository.GetUserByEmail(credentials.Email);

            if (user == null)
            {
                return null;
            }

            // Verify that the password is correct
            bool passwordCorrect = _cryptoService.VerifyPassword(user.PasswordHash, user.PasswordSalt, credentials.Password);

            if (!passwordCorrect)
            {
                return null;
            }

            // Generate jwt access token
            string token = _cryptoService.GenerateToken(user, _jwtOptions.Secret);

            AuthenticatedUser result = _mapper.Map<User, AuthenticatedUser>(user, options =>
            {
                options.Items["Token"] = token;
            });

            return result;
        }

        public async Task Register(RegisterDto credentials)
        {
            User user = _mapper.Map<RegisterDto, User>(credentials);

            // Generate salt + password hash
            byte[] salt = _cryptoService.GenerateSalt();
            byte[] hash = _cryptoService.HashPassword(credentials.Password, salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.CreatedAt = DateTime.Now;

            // Generate unique user code
            do user.UserCode = _cryptoService.GenerateUserCode();
            while (await _unitOfWork.UserRepository.UserCodeExists(user.UserCode));

            // Save user to database
            await _unitOfWork.UserRepository.AddUser(user);
            await _unitOfWork.Commit(); 
        }
    }
}
