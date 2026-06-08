using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface IAdminRepository
    {
        Task<int> GetUserCountAsync();
        Task<int> GetRoomCountAsync();
        Task<int> GetSubRoomCountAsync();
        Task<int> GetMessageCountAsync();
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> DeleteRoomAsync(int id);
        Task<List<Room>> GetAllRoomsAsync();
    }
}
