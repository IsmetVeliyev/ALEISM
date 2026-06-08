using System;
using System.Collections.Generic;
using System.Text;
using PersonalStudentProject.DataAccess.DTOs.LoginDtos;

namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface ILoginService
    {
        public Task<string> checkUpAsync(LoginDto loginDto);
    }
}
