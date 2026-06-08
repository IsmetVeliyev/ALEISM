using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.Business.Interfaces.IService;
using Microsoft.Extensions.Configuration;

namespace PersonalStudentProject.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IClaimService _claimService;

        public TokenService(IConfiguration configuration, IClaimService claimService)
        {
            _configuration = configuration;
            _claimService = claimService;
        }

        public string generateToken(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: _claimService.getClaims(user),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}