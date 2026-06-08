using PersonalStudentProject.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;  
using PersonalStudentProject.DataAccess.Models;
namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IClaimService
    {
        public List<Claim> getClaims(User user);
    }
}
