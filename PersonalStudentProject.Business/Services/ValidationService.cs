using PersonalStudentProject.Business.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace PersonalStudentProject.Business.Services
{
    public class ValidationService : IValidationService
    {
        private string _userid;
        private string _email;
        public async Task<bool> validatedRequestValues(string authHeader)
        {
            if(authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();

                var jwtToken = tokenHandler.ReadJwtToken(token);
                var claims = jwtToken.Claims;
                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                Console.WriteLine($"Extracted Claims - Email: {email}, UserId: {userId}, Role: {role}");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(role))
                {
                    _userid = userId;
                    _email = email;
                    return true;
                }
            }
            return false;
        }

        public async Task<string> getUserId()
        {
            return _userid;
        }

        public async Task<string> getEmail()
        {
            return _email;
        }
    }

}
