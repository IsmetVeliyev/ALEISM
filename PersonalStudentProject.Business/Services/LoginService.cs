using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Text;
using PersonalStudentProject.DataAccess.DTOs.LoginDtos;
namespace PersonalStudentProject.Business.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _user;
        private readonly ITokenService _token;
        private readonly IHashService _hashService;
        public LoginService(IUserRepository user, ITokenService token, IHashService hashService)
        {
            this._user = user;
            this._token = token;
            this._hashService = hashService;
        }

        public async Task<string> checkUpAsync(LoginDto loginDto)
        {
            var user = await _user.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return "User not found";
            }

            if (_hashService.VerifyPassword(loginDto.Password, user.Password))
            {
                return _token.generateToken(user);
            }

            return "Invalid password or email";
        }
    }
}
