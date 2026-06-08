using System;
using System.Collections.Generic;
using System.Text;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string email);

        Task<User> AddAsync(User user);
    }
}
