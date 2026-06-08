using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore;
using PersonalStudentProject.DataAccess.Context;    
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.Business.Interfaces.IRepository;


namespace PersonalStudentProject.Business.Repositories
{
    public class UserSubRoomRepository : IUserSubRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public UserSubRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserSubRoom> AddAsync(UserSubRoom userSubRoom)
        {
            var existingUserSubRoom = await _context.UserSubRooms.FirstOrDefaultAsync(usr => usr.UserId == userSubRoom.UserId && usr.SubRoomId == userSubRoom.SubRoomId);
            if (existingUserSubRoom != null)
            {
                return existingUserSubRoom;
            }

            var subRoom = await _context.SubRooms.FindAsync(userSubRoom.SubRoomId);
            if (subRoom == null) throw new Exception("SubRoom not found");

            var room = await _context.Rooms.FindAsync(subRoom.RoomId);
            if (room == null) throw new Exception("Room not found");

            var subRoomIds = await _context.SubRooms
                .Where(s => s.RoomId == subRoom.RoomId)
                .Select(s => s.Id)
                .ToListAsync();

            var currentCount = await _context.UserSubRooms
                .Where(usr => subRoomIds.Contains(usr.SubRoomId))
                .Select(usr => usr.UserId)
                .Distinct()
                .CountAsync();

            if (currentCount >= room.Capacity)
                throw new Exception("Room is full");

            await _context.UserSubRooms.AddAsync(userSubRoom);
            await _context.SaveChangesAsync();
            return userSubRoom;
        }

        public async Task<UserSubRoom> DeleteAsync(UserSubRoom userSubRoom)
        {
            var existingUserSubRoom = await _context.UserSubRooms.FirstOrDefaultAsync(usr => usr.UserId == userSubRoom.UserId && usr.SubRoomId == userSubRoom.SubRoomId);

            if (existingUserSubRoom != null)
            {
                _context.UserSubRooms.Remove(existingUserSubRoom);
                await _context.SaveChangesAsync();
            }
            return userSubRoom;
        }

        public async Task<IEnumerable<User>> GetAllUserSubRoomsAsync(int subRoomId , int userId)
        {
            var userSubRooms = await _context.UserSubRooms.Where(usr => usr.SubRoomId == subRoomId).ToListAsync();
            var users = new List<User>();
            foreach (var userSubRoom in userSubRooms)
            {
                var user = await _context.Users.FindAsync(userSubRoom.UserId);
                if (user != null)
                {
                    users.Add(user);
                }
            }
            return users;
        }

        public async Task<bool> IsMemberAsync(int userId, int subRoomId)
        {
            return await _context.UserSubRooms.AnyAsync(usr => usr.UserId == userId && usr.SubRoomId == subRoomId);
        }
    }
}