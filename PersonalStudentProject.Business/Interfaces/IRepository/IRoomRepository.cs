using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;
namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface IRoomRepository
    {
        Task<Room> AddAsync(Room room);
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAllAsync();
        Task<(IEnumerable<Room> Rooms, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Room> UpdateAsync(Room room);
        Task<Room> DeleteAsync(int id);
        Task<IEnumerable<Room>> getSearchedAsync(string regex);
    }
}