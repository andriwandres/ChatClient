using ChatClient.Core;
using ChatClient.Core.Models;
using ChatClient.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUser()
        {
            string id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _unitOfWork.UserRepository.GetById(int.Parse(id));
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _unitOfWork.UserRepository.IsEmailTaken(email);
        }
    }
}
