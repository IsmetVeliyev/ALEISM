using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.DTOs.User;
using PersonalStudentProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalStudentProject.Business.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        public RegisterService(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;

        }
        public Task<User> addAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                Role = "User",
                Age = registerDto.Age,
                Location = registerDto.Location,
                Password = _hashService.HashPassword(registerDto.Password)
            };
            return _userRepository.AddAsync(user);
        }
    }
}
