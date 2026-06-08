using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.DTOs.SubRoom;
using Microsoft.AspNetCore.Http;




namespace PersonalStudentProject.Business.Services
{
    public class SubRoomService : ISubRoomService
    {
        private readonly ISubRoomRepository _subRoomRepository;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoomRepository _roomRepository;

        public SubRoomService(ISubRoomRepository subRoomRepository, IValidationService validationService, IHttpContextAccessor httpContextAccessor, IRoomRepository roomRepository)
        {
            _subRoomRepository = subRoomRepository;
            _validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
            _roomRepository = roomRepository;
        }


        public async Task<SubRoom> AddAsync(SubRoomDto subRoomDto)
        {
            await checkClaimAsync();

            var room = await _roomRepository.GetByIdAsync(subRoomDto.RoomId);
            if (room == null)
                throw new KeyNotFoundException("Room not found.");

            var userIdStr = await _validationService.getUserId();
            if (string.IsNullOrEmpty(userIdStr))
                throw new UnauthorizedAccessException("User not authenticated.");
            if (room.userId != int.Parse(userIdStr))
                throw new UnauthorizedAccessException("Only the room owner can create sub-rooms.");

            var subRoom = new SubRoom
            {
                RoomId = subRoomDto.RoomId,
                SubRoomName = subRoomDto.SubRoomName
            };
            var result = await _subRoomRepository.AddAsync(subRoom);

            return result;
        }

        public async Task<SubRoom> GetByIdAsync(int id)
        {
            var result = await _subRoomRepository.GetByIdAsync(id);

            return result;
        }

        public async Task<IEnumerable<SubRoom>> GetAllByRoomIdAsync(int roomId)
        {
            var results = await _subRoomRepository.GetAllByRoomIdAsync(roomId);
            return results;
        }

        public async Task<SubRoom> UpdateAsync(int id, SubRoomDto subRoomDto)
        {

            var existingSubRoom = await _subRoomRepository.GetByIdAsync(id);
            if (existingSubRoom == null)
            {
                return null;
            }
            var subRoom = new SubRoom
            {
                RoomId = subRoomDto.RoomId,
                SubRoomName = subRoomDto.SubRoomName
            };
            var result = await _subRoomRepository.UpdateAsync(subRoom);

            return result;

        }

        

        public async Task<SubRoom> DeleteAsync(int id)
        {
            var result = await _subRoomRepository.DeleteAsync(id);
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