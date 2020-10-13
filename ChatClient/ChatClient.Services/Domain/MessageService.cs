using ChatClient.Core;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Message;
using ChatClient.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Services.Domain
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

        public Task AddGroupMessage(int groupId, string message)
        {
            throw new System.NotImplementedException();
        }

        public Task AddPrivateMessage(int recipientId, string message)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId)
        {
            return await _unitOfWork.MessageRecipientRepository.GetGroupMessages(userId, groupId);
        }

        public async Task<IEnumerable<LatestMessageViewModel>> GetLatestMessages()
        {
            User user = await _authService.GetCurrentUser();

            return await _unitOfWork.MessageRecipientRepository.GetLatestMessages(user.UserId);
        }

        public async Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientId)
        {
            return await _unitOfWork.MessageRecipientRepository.GetPrivateMessages(userId, recipientId);
        }
    }
}
