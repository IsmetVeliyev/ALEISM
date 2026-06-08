using Microsoft.EntityFrameworkCore;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Context;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Repositories
{
    public class DirectMessageRepository : IDirectMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public DirectMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DirectMessage> SendMessage(DirectMessage message)
        {
            await _context.DirectMessages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<List<DirectMessage>> GetMessagesAsyncByUserId(int userId)
        {
            return await _context.DirectMessages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ToListAsync();
        }

        public async Task<Dictionary<int, string>> GetUserNamesByIdsAsync(List<int> userIds)
        {
            return await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.Name);
        }
    }
}
