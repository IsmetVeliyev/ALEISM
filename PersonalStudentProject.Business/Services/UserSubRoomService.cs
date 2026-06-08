using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Repositories;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using PersonalStudentProject.DataAccess.DTOs.User;
namespace PersonalStudentProject.Business.Services
{
    public class UserSubRoomService : IUserSubRoomService
    {
        private readonly IUserSubRoomRepository _userSubRoomRepository;
        private readonly IValidationService _validationService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSubRoomService(IUserSubRoomRepository userSubRoomRepository , IValidationService validationService, IHttpContextAccessor httpContextAccessor)
        {
            _validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
            _userSubRoomRepository = userSubRoomRepository;
        }

        public async Task<UserSubRoom> AddAsync(UserSubRoom userSubRoom)
        {
            return await _userSubRoomRepository.AddAsync(userSubRoom);
        }

        public async Task<UserSubRoom> AddUserToSubRoomAsync(UserSubRoom userSubRoom)
        {
            return await _userSubRoomRepository.AddAsync(userSubRoom);
        }

        public async Task<UserSubRoom> RemoveUserFromSubRoomAsync(UserSubRoom userSubRoom)
        {
            return await _userSubRoomRepository.DeleteAsync(userSubRoom);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserSubRoomsAsync(int subRoomId , int userId)
        {
            var users = await _userSubRoomRepository.GetAllUserSubRoomsAsync(subRoomId , userId);
            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Age = u.Age
            }).ToList();
            return userDtos;
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

        public async Task<string> getUserIdAsync()
        {
            return await _validationService.getUserId();
        }
        
    }
}