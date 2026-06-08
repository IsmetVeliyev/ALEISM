using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.DTOs.DirectMessage;

namespace PersonalStudentProject.Controllers.MessageController
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectMessageController : ControllerBase
    {
        private readonly IDirectMessageService _directMessageService;

        public DirectMessageController(IDirectMessageService directMessageService)
        {
            _directMessageService = directMessageService;
        }

        [HttpPost("/senddirectmessage")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] SendDirectMessageDto messageDto)
        {
            await _directMessageService.checkClaimAsync();
            var result = await _directMessageService.SendMessage(messageDto);
            return Ok(new { message = "Direct message sent successfully", result });
        }

        [HttpGet("/directmessages/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetMessagesByUserId(int userId)
        {
            await _directMessageService.checkClaimAsync();
            var messages = await _directMessageService.GetMessagesByUserId(userId);
            return Ok(new { messages });
        }
    }
}
