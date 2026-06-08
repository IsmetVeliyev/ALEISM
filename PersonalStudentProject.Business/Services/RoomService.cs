using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.DTOs.Room;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Interfaces.IRepository;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;


namespace PersonalStudentProject.Business.Services
{
    public class RoomService: IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoomService(IRoomRepository roomRepository, IValidationService validationService, IHttpContextAccessor httpContextAccessor)
        {
            _roomRepository = roomRepository;
            _validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public Task<Room> addAsync(RoomDto roomDto)
        {
            var room = new Room
            {
                userId = roomDto.userId,
                RoomName = roomDto.RoomName,
                RoomType = roomDto.RoomType,
                isPasswordProtected = roomDto.isPasswordProtected,
                Password = roomDto.Password,
                Description = roomDto.Description,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ExpiryDate = roomDto.ExpiryDate,
                Capacity = roomDto.Capacity,
                IsAvailable = true,
                Location = roomDto.Location  
            };
            return _roomRepository.AddAsync(room);
        }
        public Task<Room> getByIdAsync(int id)
        {
            return _roomRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Room>> getAllAsync()
        {
            return _roomRepository.GetAllAsync();
        }

        public Task<(IEnumerable<Room> Rooms, int TotalCount)> getPagedAsync(int page, int pageSize)
        {
            return _roomRepository.GetPagedAsync(page, pageSize);
        }

        public Task<IEnumerable<Room>> getSearchedAsync(string regex)
        {
            return _roomRepository.getSearchedAsync(regex);
        }

        public async Task<Room> updateAsync(int id, RoomDto roomDto)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new KeyNotFoundException("Room not found");

            var userId = await _validationService.getUserId();
            if (string.IsNullOrEmpty(userId) || room.userId != int.Parse(userId))
                throw new UnauthorizedAccessException("Only the room owner can edit this room.");

            room.RoomName = roomDto.RoomName;
            room.RoomType = roomDto.RoomType;
            room.isPasswordProtected = roomDto.isPasswordProtected;
            room.Password = roomDto.Password;
            room.Description = roomDto.Description;
            room.DateUpdated = DateTime.UtcNow;
            room.ExpiryDate = roomDto.ExpiryDate;
            room.Capacity = roomDto.Capacity;
            room.Location = roomDto.Location;

            return await _roomRepository.UpdateAsync(room);
        }

        public async Task<Room> deleteAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new KeyNotFoundException("Room not found");

            var userId = await _validationService.getUserId();
            if (string.IsNullOrEmpty(userId) || room.userId != int.Parse(userId))
                throw new UnauthorizedAccessException("Only the room owner can delete this room.");

            return await _roomRepository.DeleteAsync(id);
        }


        public async Task<bool> checkPasswordAsync(int id, string password)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                throw new Exception("Room not found");
            }

            if (!room.isPasswordProtected)
            {
                return true;
            }

            if (room.Password != password)
            {
                return false;
            }

            return true;
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