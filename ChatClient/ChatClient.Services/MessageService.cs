using ChatClient.Core;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public MessageService(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId)
        {
            return await _unitOfWork.MessageRecipientRepository.GetGroupMessages(userId, groupId);
        }

        public async Task<IEnumerable<MessageRecipient>> GetLatestMessages()
        {
            User user = await _authService.GetUser();

            return await _unitOfWork.MessageRecipientRepository.GetLatestMessages(user.UserId);
        }

        public async Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientId)
        {
            return await _unitOfWork.MessageRecipientRepository.GetPrivateMessages(userId, recipientId);
        }
    }
}
