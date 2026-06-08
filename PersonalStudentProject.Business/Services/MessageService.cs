using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using PersonalStudentProject.DataAccess.DTOs.Message;
using Microsoft.AspNetCore.Authorization;



namespace PersonalStudentProject.Business.Services
{
    public class MessageService : IMessageService
    { 
        private readonly IMessageRepository _messageRepository;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessageService(
            IMessageRepository messageRepository,
            IValidationService validationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
            _messageRepository = messageRepository;
        }

        public async Task<Message> SendMessage(SendMessageDto messageDto)
        {
            string senderEmail = await _validationService.getEmail();
            int senderId = int.Parse(await _validationService.getUserId());
            var message = new Message
            {
                RoomId = messageDto.RoomId,
                Content = messageDto.Content,
                SenderId = senderId,
                SenderEmail = senderEmail,
                DateSent = DateTime.UtcNow,
                ReplyToId = messageDto.ReplyToId
            };

            return await _messageRepository.SendMessage(message);
        }

        public async Task<List<GetMessageDto>> GetMessagesByRoomId(int roomId)
        {
            var messages = await _messageRepository.GetMessagesAsyncByRoomId(roomId);
            var messageDict = messages.ToDictionary(m => m.Id);

            var result = new List<GetMessageDto>();

            foreach (var message in messages)
            {
                Message replied = message.ReplyToId.HasValue && messageDict.ContainsKey(message.ReplyToId.Value)
                    ? messageDict[message.ReplyToId.Value]
                    : null;

                result.Add(new GetMessageDto
                {
                    Id = message.Id,
                    RoomId = message.RoomId,
                    SenderId = message.SenderId,
                    SenderEmail = message.SenderEmail,
                    Content = message.Content,
                    DateSent = message.DateSent,
                    ReplyToId = message.ReplyToId,
                    ReplyToContent = replied?.Content,
                    ReplyToSenderEmail = replied?.SenderEmail
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