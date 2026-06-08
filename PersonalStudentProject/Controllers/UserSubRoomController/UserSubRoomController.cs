using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.Business.Interfaces.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace PersonalStudentProject.Controllers.UserSubRoomController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubRoomController : ControllerBase
    {
        private readonly IUserSubRoomService _userSubRoomService;
        private readonly IValidationService _validationService;


        public UserSubRoomController(IUserSubRoomService userSubRoomService, IValidationService validationService)
        {

            _userSubRoomService = userSubRoomService;
            _validationService = validationService;
        }


        [HttpGet("/getAll")]
        [Authorize]
        public async Task<IActionResult> GetAllUserSubRooms([FromQuery] int subRoomId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return Unauthorized();
            var result = await _userSubRoomService.GetAllUserSubRoomsAsync(subRoomId, int.Parse(userIdStr));
            return Ok(result);
        }




        [HttpPost("/add")]
        [Authorize]
        public async Task<IActionResult> AddRoom(UserSubRoom userSubRoom)
        {
            await _userSubRoomService.checkClaimAsync();
            var result = await _userSubRoomService.AddAsync(userSubRoom);
            return Ok(result);
        }


        [HttpPost("/join")]
        [Authorize]
        public async Task<IActionResult> JoinSubRoom([FromQuery] int subRoomId)
        {
            await _userSubRoomService.checkClaimAsync();
            var userSubRoom = new UserSubRoom
            {
                SubRoomId = subRoomId,
                UserId = int.Parse(await _userSubRoomService.getUserIdAsync())
            };
            try
            {
                var result = await _userSubRoomService.AddUserToSubRoomAsync(userSubRoom);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/leave")]
        [Authorize]
        public async Task<IActionResult> LeaveSubRoom([FromQuery] int subRoomId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return Unauthorized();

            var userSubRoom = new UserSubRoom
            {
                UserId = int.Parse(userIdStr),
                SubRoomId = subRoomId
            };

            var result = await _userSubRoomService.RemoveUserFromSubRoomAsync(userSubRoom);
            return Ok(result);
        }
    }
}