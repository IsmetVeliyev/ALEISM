using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace PersonalStudentProject.Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ISubRoomRepository _subRoomRepository;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(
            IAdminRepository adminRepository,
            ISubRoomRepository subRoomRepository,
            IValidationService validationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _adminRepository = adminRepository;
            _subRoomRepository = subRoomRepository;
            _validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> IsAdminAsync()
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var isValid = await _validationService.validatedRequestValues(authHeader);
            if (!isValid) return false;

            var email = await _validationService.getEmail();
            var user = await _adminRepository.GetUserByEmailAsync(email);

            return user != null && user.Role == "Admin";
        }

        public async Task<object> GetStatsAsync()
        {
            return new
            {
                totalUsers = await _adminRepository.GetUserCountAsync(),
                totalRooms = await _adminRepository.GetRoomCountAsync(),
                totalSubRooms = await _adminRepository.GetSubRoomCountAsync(),
                totalMessages = await _adminRepository.GetMessageCountAsync()
            };
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _adminRepository.GetAllUsersAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _adminRepository.DeleteUserAsync(id);
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            return await _adminRepository.DeleteRoomAsync(id);
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _adminRepository.GetAllRoomsAsync();
        }

        public async Task<SubRoom> AddSubRoomAsync(int roomId, string subRoomName)
        {
            var subRoom = new SubRoom
            {
                RoomId = roomId,
                SubRoomName = subRoomName
            };
            return await _subRoomRepository.AddAsync(subRoom);
        }
    }
}
