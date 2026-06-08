using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace PersonalStudentProject.Business.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Room> AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }
        
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<(IEnumerable<Room> Rooms, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var total = await _context.Rooms.CountAsync();
            var rooms = await _context.Rooms
                .OrderByDescending(r => r.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (rooms, total);
        }
        public async Task<IEnumerable<Room>> getSearchedAsync(string regex)
        {
            return await _context.Rooms.Where(r => EF.Functions.Like(r.RoomType, $"%{regex}%")).ToListAsync();
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if(room == null)
            {
                return null;
            }
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return room;
        }


        
    }
}