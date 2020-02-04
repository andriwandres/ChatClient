using ChatClient.Core;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public ContactService(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<IEnumerable<UserRelationship>> GetContacts(int userId)
        {
            return await _unitOfWork.UserRelationshipRepository.GetContacts(userId);
        }

        public async Task<User> GetUserByCode(string code)
        {
            return await _unitOfWork.UserRepository.GetUserByCode(code);
        }
    }
}
