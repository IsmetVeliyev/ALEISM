using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;



namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface ISubRoomRepository
    {
        Task<SubRoom> AddAsync(SubRoom subRoom);
        Task<SubRoom> GetByIdAsync(int id);
         Task<IEnumerable<SubRoom>> GetAllByRoomIdAsync(int roomId);
        Task<SubRoom> UpdateAsync(SubRoom subRoom);
        Task<SubRoom> DeleteAsync(int id);
    }
}