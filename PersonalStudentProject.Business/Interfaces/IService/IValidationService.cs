using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IValidationService
    {
        public Task<bool> validatedRequestValues(string authHeader);
        public Task<string> getUserId();
        public Task<string> getEmail();
    }
}
