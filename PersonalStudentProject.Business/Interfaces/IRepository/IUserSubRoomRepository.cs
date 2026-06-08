using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface IUserSubRoomRepository
    {
        Task<UserSubRoom> AddAsync(UserSubRoom userSubRoom);
        Task<UserSubRoom> DeleteAsync(UserSubRoom userSubRoom);

        Task<IEnumerable<User>> GetAllUserSubRoomsAsync(int subRoomId , int userId);
        Task<bool> IsMemberAsync(int userId, int subRoomId);

    }
}