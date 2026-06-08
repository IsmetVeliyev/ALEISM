using Microsoft.AspNetCore.Http;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.DTOs.DirectMessage;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Services
{
    public class DirectMessageService : IDirectMessageService
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidationService _validationService;

        public DirectMessageService(
            IDirectMessageRepository directMessageRepository,
            IHttpContextAccessor httpContextAccessor,
            IValidationService validationService)
        {
            _directMessageRepository = directMessageRepository;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
        }

        public async Task<DirectMessage> SendMessage(SendDirectMessageDto messageDto)
        {
            var senderEmail = await _validationService.getEmail();
            var senderIdString = await _validationService.getUserId();
            var senderId = int.Parse(senderIdString);

            var directMessage = new DirectMessage
            {
                SenderId = senderId,
                SenderEmail = senderEmail,
                ReceiverId = messageDto.ReceiverId,
                Content = messageDto.Content,
                DateSent = DateTime.UtcNow
            };

            return await _directMessageRepository.SendMessage(directMessage);
        }

        public async Task<List<DirectMessageDto>> GetMessagesByUserId(int userId)
        {
            var messages = await _directMessageRepository.GetMessagesAsyncByUserId(userId);

            var userIds = messages.Select(m => m.SenderId)
                .Union(messages.Select(m => m.ReceiverId))
                .Distinct()
                .ToList();

            var userNames = await _directMessageRepository.GetUserNamesByIdsAsync(userIds);

            var result = new List<DirectMessageDto>();
            foreach (var message in messages)
            {
                result.Add(new DirectMessageDto
                {
                    Id = message.Id,
                    SenderId = message.SenderId,
                    SenderEmail = message.SenderEmail,
                    SenderName = userNames.GetValueOrDefault(message.SenderId, message.SenderEmail),
                    ReceiverId = message.ReceiverId,
                    ReceiverName = userNames.GetValueOrDefault(message.ReceiverId, "User #" + message.ReceiverId),
                    Content = message.Content,
                    DateSent = message.DateSent
                });
            }
            return result;
        }

        public async Task<bool> checkClaimAsync()
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var result = await _validationService.validatedRequestValues(authHeader);
            if (!result)
            {
                throw new Exception("Not authorized");
            }
            return true;
        }
    }
}
