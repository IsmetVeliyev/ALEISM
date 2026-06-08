using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.Context;
using Microsoft.EntityFrameworkCore;


namespace PersonalStudentProject.Business.Repositories
{
    public class SubRoomRepository : ISubRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public SubRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SubRoom> AddAsync(SubRoom subRoom)
        {
            await _context.SubRooms.AddAsync(subRoom);
            await _context.SaveChangesAsync();
            return subRoom;
        }

        public async Task<SubRoom> DeleteAsync(int id)
        {
            var subRoom = await _context.SubRooms.FindAsync(id);
            if(subRoom == null)
            {
                return null;
            }
            _context.SubRooms.Remove(subRoom);
            await _context.SaveChangesAsync();
            return subRoom;
        }

        public async Task<IEnumerable<SubRoom>> GetAllByRoomIdAsync(int roomId)
        {
            return await _context.SubRooms.Where(s => s.RoomId == roomId).ToListAsync();
        }

        public async Task<SubRoom> GetByIdAsync(int id)
        {
            return await _context.SubRooms.FindAsync(id);
        }

        

        public async Task<SubRoom> UpdateAsync(SubRoom subRoom)
        {
            _context.SubRooms.Update(subRoom);
            await _context.SaveChangesAsync();
            return subRoom;
        }

        
    }
}