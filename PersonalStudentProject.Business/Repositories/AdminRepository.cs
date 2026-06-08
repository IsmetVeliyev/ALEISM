using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Context;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetRoomCountAsync()
        {
            return await _context.Rooms.CountAsync();
        }

        public async Task<int> GetSubRoomCountAsync()
        {
            return await _context.SubRooms.CountAsync();
        }

        public async Task<int> GetMessageCountAsync()
        {
            return await _context.Messages.CountAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            var memberships = await _context.UserSubRooms
                .Where(usr => usr.UserId == id)
                .ToListAsync();
            _context.UserSubRooms.RemoveRange(memberships);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms
                .OrderByDescending(r => r.DateCreated)
                .ToListAsync();
        }
    }
}
