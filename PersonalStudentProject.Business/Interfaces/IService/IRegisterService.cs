using PersonalStudentProject.DataAccess.DTOs.User;
using PersonalStudentProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IRegisterService
    {
        public Task<User> addAsync(RegisterDto registerDto);
    }
}
