using Moq;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Services;
using PersonalStudentProject.DataAccess.DTOs.User;
using PersonalStudentProject.DataAccess.Models;
using Xunit;

namespace PersonalStudentProject.Tests;

public class RegisterServiceTests
{
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<IHashService>    _hashService = new();

    private RegisterService CreateService() =>
        new(_userRepo.Object, _hashService.Object);

    [Fact]
    public async Task Register_PasswordIsHashed_BeforeStoring()
    {
        _hashService.Setup(h => h.HashPassword("plaintext")).Returns("bcrypt_hash");
        _userRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
                 .ReturnsAsync((User u) => u);

        var dto = new RegisterDto { Email = "a@a.com", Name = "Alice", Password = "plaintext", Role = "Student", Age = 20, Location = "Liège" };
        var result = await CreateService().addAsync(dto);

        Assert.Equal("bcrypt_hash", result.Password);
        Assert.NotEqual("plaintext", result.Password);
    }

    [Fact]
    public async Task Register_UserFieldsAreMappedCorrectly()
    {
        _hashService.Setup(h => h.HashPassword(It.IsAny<string>())).Returns("hash");
        _userRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
                 .ReturnsAsync((User u) => u);

        var dto = new RegisterDto { Email = "b@b.com", Name = "Bob", Password = "pass", Age = 30, Location = "Brussels" };
        var result = await CreateService().addAsync(dto);

        Assert.Equal("b@b.com", result.Email);
        Assert.Equal("Bob", result.Name);
        Assert.Equal("User", result.Role);
        Assert.Equal(30, result.Age);
        Assert.Equal("Brussels", result.Location);
    }
}
