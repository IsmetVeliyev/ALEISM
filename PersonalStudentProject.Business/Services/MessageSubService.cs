using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using PersonalStudentProject.DataAccess.DTOs.Message;

namespace PersonalStudentProject.Business.Services
{
    public class MessageSubService : IMessageSubService
    {
        private readonly IMessageSubRepository _messageSubRepository;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSubRoomRepository _userSubRoomRepository;

        public MessageSubService(IMessageSubRepository messageSubRepository,
            IValidationService validationService,
            IHttpContextAccessor httpContextAccessor,
            IUserSubRoomRepository userSubRoomRepository)
        {
            _messageSubRepository = messageSubRepository;
            _validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
            _userSubRoomRepository = userSubRoomRepository;
        }

        public async Task<SendMessageResponseDto> SendSubMessage(SendMessageDto messageDto)
        {
            int userId = int.Parse(await _validationService.getUserId());
            bool isMember = await _userSubRoomRepository.IsMemberAsync(userId, messageDto.RoomId);
            if (!isMember)
                throw new UnauthorizedAccessException("You are not a member of this room.");

            string senderEmail = await _validationService.getEmail();
            var messageSub = new MessageSub
            {
                subRoomId = messageDto.RoomId,
                content = messageDto.Content,
                senderEmail = senderEmail,
                DateSent = DateTime.UtcNow,
                ReplyToId = messageDto.ReplyToId
            };

            var saved = await _messageSubRepository.SendSubMessage(messageSub);

            return new SendMessageResponseDto
            {
                Id = saved.id,
                SubRoomId = saved.subRoomId,
                Content = saved.content,
                DateSent = saved.DateSent
            };
        }

        public async Task<List<GetMessageDto>> GetSubMessagesByRoomId(int subRoomId)
        {
            int userId = int.Parse(await _validationService.getUserId());
            bool isMember = await _userSubRoomRepository.IsMemberAsync(userId, subRoomId);
            if (!isMember)
                throw new UnauthorizedAccessException("You are not a member of this room.");

            var messages = await _messageSubRepository.GetSubMessagesAsyncByRoomId(subRoomId);
            var messageDict = messages.ToDictionary(m => m.id);

            var result = new List<GetMessageDto>();

            foreach (var message in messages)
            {
                MessageSub replied = message.ReplyToId.HasValue && messageDict.ContainsKey(message.ReplyToId.Value)
                    ? messageDict[message.ReplyToId.Value]
                    : null;

                result.Add(new GetMessageDto
                {
                    Id = message.id,
                    SenderEmail = message.senderEmail,
                    Content = message.content,
                    DateSent = message.DateSent,
                    ReplyToId = message.ReplyToId,
                    ReplyToContent = replied?.content,
                    ReplyToSenderEmail = replied?.senderEmail
                });
            }

            return result;
        }

        public async Task<bool> checkClaimAsync()
        {
            if (_httpContextAccessor.HttpContext == null)
                throw new UnauthorizedAccessException("No HTTP context.");

            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var result = await _validationService.validatedRequestValues(authHeader);
            if (!result)
            {
                throw new UnauthorizedAccessException("Not authorized");
            }
            return true;
        }
    }
}