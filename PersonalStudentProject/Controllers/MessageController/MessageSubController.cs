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
    public class MessageSubController : ControllerBase
    {
        private readonly IMessageSubService _messageSubService;

        public MessageSubController(IMessageSubService messageSubService)
        {
            _messageSubService = messageSubService;
        }

        [HttpPost("/sendsubmessage")]
        [Authorize]
        public async Task<IActionResult> SendSubMessage([FromBody] SendMessageDto messageDto)
        {
            try
            {
                await _messageSubService.checkClaimAsync();
                var result = await _messageSubService.SendSubMessage(messageDto);
                return Ok(new { message = "Message sent successfully", result });
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, new { message = "You are not a member of this sub-room." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet("/submessages/{subroomId}")]
        [Authorize]
        public async Task<IActionResult> GetSubMessagesByRoomId(int subroomId)
        {
            try
            {
                await _messageSubService.checkClaimAsync();
                var messages = await _messageSubService.GetSubMessagesByRoomId(subroomId);
                return Ok(new { messages });
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, new { message = "You are not a member of this sub-room." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

    }
}