using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IAdminService
    {
        Task<bool> IsAdminAsync();
        Task<object> GetStatsAsync();
        Task<List<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
        Task<bool> DeleteRoomAsync(int id);
        Task<List<Room>> GetAllRoomsAsync();
        Task<SubRoom> AddSubRoomAsync(int roomId, string subRoomName);
    }
}
