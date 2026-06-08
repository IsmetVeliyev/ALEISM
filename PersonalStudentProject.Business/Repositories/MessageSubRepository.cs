using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Context;
using PersonalStudentProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalStudentProject.Business.Repositories
{
    public class MessageSubRepository : IMessageSubRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageSubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<MessageSub> SendSubMessage(MessageSub messageSub)
        {
            await _context.SubMessages.AddAsync(messageSub);
            await _context.SaveChangesAsync();
            return messageSub;

        }
        public async Task<List<MessageSub>> GetSubMessagesAsyncByRoomId(int subRoomId)
        {
            return await _context.SubMessages.Where(m => m.subRoomId == subRoomId).ToListAsync();
        }

    }
}