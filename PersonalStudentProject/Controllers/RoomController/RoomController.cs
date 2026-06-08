using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.DTOs.Room;
using Microsoft.AspNetCore.Mvc;
using PersonalStudentProject.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using PersonalStudentProject.Business.Interfaces.IRepository;
using System.Numerics;
using PersonalStudentProject.DataAccess.DTOs.Room;
namespace PersonalStudentProject.Controllers.RoomController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IValidationService _validationService; 

        public RoomController(IRoomService roomService , IValidationService validationService)
        {
            _roomService = roomService;
            _validationService = validationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRoom([FromBody] RoomDto roomDto)
        {
            await _roomService.checkClaimAsync();
            string userIdString = await _validationService.getUserId();
            int userId = int.Parse(userIdString);
            roomDto.userId = userId;
            var result = await _roomService.addAsync(roomDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRoomById(int id)
        {
            await _roomService.checkClaimAsync();
            var result = await _roomService.getByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRooms([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        {
            await _roomService.checkClaimAsync();
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 6;
            var (rooms, total) = await _roomService.getPagedAsync(page, pageSize);
            return Ok(new
            {
                rooms,
                totalCount = total,
                totalPages = (int)Math.Ceiling((double)total / pageSize),
                currentPage = page
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoom(int id , [FromBody] RoomDto roomDto)
        {
            await _roomService.checkClaimAsync();
            try
            {
                var result = await _roomService.updateAsync(id, roomDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomService.checkClaimAsync();
            try
            {
                var result = await _roomService.deleteAsync(id);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/enter")]
        [Authorize]
        public async Task<IActionResult> EnterRoom(int id, [FromBody] EnterRoomDto enterRoomDto)
        {
            await _roomService.checkClaimAsync();
            var result = await _roomService.checkPasswordAsync(id, enterRoomDto.Password);
            if (!result)
            {
                return StatusCode(403, "Wrong password");
            }
            return Ok("Access granted");
        }

        [HttpGet("/search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string regex)
        {
           await _roomService.checkClaimAsync();
           var rooms = await _roomService.getSearchedAsync(regex);
           return Ok(rooms); 
        }


    }
}
