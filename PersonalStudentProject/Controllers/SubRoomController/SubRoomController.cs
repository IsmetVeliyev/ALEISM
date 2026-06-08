using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.DTOs.SubRoom;
using Microsoft.AspNetCore.Authorization;
using PersonalStudentProject.Business.Interfaces.IRepository;


namespace PersonalStudentProject.Controllers.SubRoomController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubRoomController : ControllerBase
    {
        private readonly ISubRoomService _subRoomService;

        public SubRoomController(ISubRoomService subRoomService)
        {
            _subRoomService = subRoomService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSubRoom([FromBody] SubRoomDto subRoomDto)
        {
            try
            {
                var result = await _subRoomService.AddAsync(subRoomDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, new { message = "Only the room owner can create sub-rooms." });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSubRoomById(int id)
        {
            await _subRoomService.checkClaimAsync();
            var result = await _subRoomService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllSubRooms([FromQuery] int roomId)
        {
            
            var result = await _subRoomService.GetAllByRoomIdAsync(roomId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSubRoom(int id, [FromBody] SubRoomDto subRoomDto)
        {
            await _subRoomService.checkClaimAsync();
            var result = await _subRoomService.UpdateAsync(id,subRoomDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSubRoom(int id)
        {
            await _subRoomService.checkClaimAsync();
            var result = await _subRoomService.DeleteAsync(id);
            return Ok(result);
        }

        
    }
}