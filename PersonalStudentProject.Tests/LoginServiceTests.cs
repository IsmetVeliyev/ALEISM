using Moq;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Services;
using PersonalStudentProject.DataAccess.DTOs.LoginDtos;
using PersonalStudentProject.DataAccess.Models;
using Xunit;

namespace PersonalStudentProject.Tests;

public class LoginServiceTests
{
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<ITokenService>   _tokenService = new();
    private readonly Mock<IHashService>    _hashService = new();

    private LoginService CreateService() =>
        new(_userRepo.Object, _tokenService.Object, _hashService.Object);

    [Fact]
    public async Task Login_UserNotFound_ReturnsUserNotFound()
    {
        _userRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                 .ReturnsAsync((User?)null);

        var result = await CreateService().checkUpAsync(new LoginDto { Email = "x@x.com", Password = "123" });

        Assert.Equal("User not found", result);
    }

    [Fact]
    public async Task Login_WrongPassword_ReturnsInvalidMessage()
    {
        var user = new User { Email = "a@a.com", Password = "hashed" };
        _userRepo.Setup(r => r.GetByEmailAsync("a@a.com")).ReturnsAsync(user);
        _hashService.Setup(h => h.VerifyPassword("wrong", "hashed")).Returns(false);

        var result = await CreateService().checkUpAsync(new LoginDto { Email = "a@a.com", Password = "wrong" });

        Assert.Equal("Invalid password or email", result);
    }

    [Fact]
    public async Task Login_CorrectCredentials_ReturnsToken()
    {
        var user = new User { Email = "a@a.com", Password = "hashed" };
        _userRepo.Setup(r => r.GetByEmailAsync("a@a.com")).ReturnsAsync(user);
        _hashService.Setup(h => h.VerifyPassword("correct", "hashed")).Returns(true);
        _tokenService.Setup(t => t.generateToken(user)).Returns("jwt.token.here");

        var result = await CreateService().checkUpAsync(new LoginDto { Email = "a@a.com", Password = "correct" });

        Assert.Equal("jwt.token.here", result);
    }
}
