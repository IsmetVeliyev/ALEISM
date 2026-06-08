using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.DTOs.SubRoom;

namespace PersonalStudentProject.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("stats")]
        [Authorize]
        public async Task<IActionResult> GetStats()
        {
            if (!await _adminService.IsAdminAsync()) return Forbid();

            var stats = await _adminService.GetStatsAsync();
            return Ok(stats);
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!await _adminService.IsAdminAsync()) return Forbid();

            var users = await _adminService.GetAllUsersAsync();

            var result = users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Role,
                u.Age,
                u.Location
            });

            return Ok(result);
        }

        [HttpDelete("users/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!await _adminService.IsAdminAsync()) return Forbid();

            var deleted = await _adminService.DeleteUserAsync(id);
            if (!deleted) return NotFound("User not found");

            return Ok(new { message = "User deleted" });
        }

        [HttpDelete("rooms/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (!await _adminService.IsAdminAsync()) return Forbid();

            var deleted = await _adminService.DeleteRoomAsync(id);
            if (!deleted) return NotFound("Room not found");

            return Ok(new { message = "Room deleted" });
        }

        [HttpGet("rooms")]
        [Authorize]
        public async Task<IActionResult> GetAllRooms()
        {
            if (!await _adminService.IsAdminAsync()) return Forbid();

            var rooms = await _adminService.GetAllRoomsAsync();
            var result = rooms.Select(r => new
            {
                r.Id,
                r.RoomName,
                r.RoomType,
                r.Capacity,
                r.Location,
                r.ExpiryDate,
                r.isPasswordProtected,
                r.DateCreated
            });
            return Ok(result);
        }

        [HttpPost("subrooms")]
        [Authorize]
        public async Task<IActionResult> AddSubRoom([FromBody] SubRoomDto dto)
        {
            if (!await _adminService.IsAdminAsync()) return Forbid();

            var result = await _adminService.AddSubRoomAsync(dto.RoomId, dto.SubRoomName);
            return Ok(result);
        }
    }
}
