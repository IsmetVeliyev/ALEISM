using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.DTOs.Message;
using Microsoft.AspNetCore.Authorization;

namespace PersonalStudentProject.Controllers.MessageController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IValidationService _validationService;

        public MessageController(IMessageService messageService, IValidationService validationService)
        {
            _messageService = messageService;
            _validationService = validationService;
        }

        [HttpPost("/sendmessage")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageDto)
        {
            await _messageService.checkClaimAsync();
            var result = await _messageService.SendMessage(messageDto);
            return Ok(new { message = "Message sent successfully", result });
        }

        [HttpGet("/messages/{roomId}")]
        [Authorize]
        public async Task<IActionResult> GetMessagesByRoomId(int roomId)
        {
            await _messageService.checkClaimAsync();
            var messages = await _messageService.GetMessagesByRoomId(roomId);
            return Ok(new { messages });
        }
        
    }
}