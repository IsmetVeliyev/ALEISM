using PersonalStudentProject.Business.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace PersonalStudentProject.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            int userId = int.Parse(id);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}
