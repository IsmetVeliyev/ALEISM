using Microsoft.AspNetCore.Mvc;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.DTOs.LoginDtos;
namespace PersonalStudentProject.WebAPI.Controllers.LoginController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _userService;
        public AuthController(ILoginService service)
        {
            _userService = service;

        }
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.checkUpAsync(loginDto);

            if(result == "User not found" || result == "Invalid password or email")
            {
                return Unauthorized(new { message = result });
            }

            return Ok(new { token = result });
        }


    }
}
