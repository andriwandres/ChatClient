using ChatClient.Core;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Services;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserByCode(string code)
        {
            return await _unitOfWork.UserRepository.GetUserByCode(code);
        }
    }
}
