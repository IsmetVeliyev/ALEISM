using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using PersonalStudentProject.DataAccess.Context;

namespace PersonalStudentProject.Business.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Message> SendMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<List<Message>> GetMessagesAsyncByRoomId(int roomId)
        {
            return await _context.Messages.Where(m => m.RoomId == roomId).ToListAsync();
        }

    }
}