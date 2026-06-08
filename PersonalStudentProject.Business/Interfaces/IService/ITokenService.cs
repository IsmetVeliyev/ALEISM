using System;
using System.Collections.Generic;
using System.Text;
using PersonalStudentProject.DataAccess.Models;
namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface ITokenService
    {
        public string generateToken(User user);
    }
}
