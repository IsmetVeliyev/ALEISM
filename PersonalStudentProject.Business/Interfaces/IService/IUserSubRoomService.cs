using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.DTOs.User;
namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IUserSubRoomService
    {
        Task<UserSubRoom> AddUserToSubRoomAsync(UserSubRoom userSubRoom);

        Task<UserSubRoom> AddAsync(UserSubRoom userSubRoom);
        Task<UserSubRoom> RemoveUserFromSubRoomAsync(UserSubRoom userSubRoom);

        Task<IEnumerable<UserDto>> GetAllUserSubRoomsAsync(int subRoomId , int userId);

        public Task<string> getUserIdAsync();
        public Task<bool> checkClaimAsync();
    }
}
