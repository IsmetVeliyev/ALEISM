using Microsoft.AspNetCore.Mvc;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.DataAccess.DTOs.User;

namespace PersonalStudentProject.WebAPI.Controllers.RegisterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _registerService.addAsync(registerDto);

            return Ok(user); 
        }

    }
}
