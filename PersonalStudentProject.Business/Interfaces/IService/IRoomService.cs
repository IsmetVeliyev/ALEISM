using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.DTOs.Room;
using PersonalStudentProject.DataAccess.Models;
namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IRoomService
    {

        public Task<Room> addAsync(RoomDto roomDto);
        public Task<Room> getByIdAsync(int id);

        public Task<IEnumerable<Room>> getAllAsync();
        public Task<(IEnumerable<Room> Rooms, int TotalCount)> getPagedAsync(int page, int pageSize);
        public Task<Room> updateAsync(int id, RoomDto roomDto);
        public Task<Room> deleteAsync(int id);
        public Task<bool> checkClaimAsync();
        public Task<IEnumerable<Room>> getSearchedAsync(string regex);
        public Task<bool> checkPasswordAsync(int id, string password);
        
    }
}