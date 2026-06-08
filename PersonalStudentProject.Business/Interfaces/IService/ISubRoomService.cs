using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.DTOs.SubRoom;

namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface ISubRoomService
    {
        Task<SubRoom> AddAsync(SubRoomDto subRoomDto);
        Task<SubRoom> GetByIdAsync(int id);
        Task<IEnumerable<SubRoom>> GetAllByRoomIdAsync(int roomId);
        
        Task<SubRoom> UpdateAsync(int id,SubRoomDto subRoomDto);
        Task<SubRoom> DeleteAsync(int id);
        Task<bool> checkClaimAsync();
        
    }
}