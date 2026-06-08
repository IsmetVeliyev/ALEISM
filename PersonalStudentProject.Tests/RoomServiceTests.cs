using Moq;
using Microsoft.AspNetCore.Http;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.Business.Interfaces.IService;
using PersonalStudentProject.Business.Services;
using PersonalStudentProject.DataAccess.DTOs.Room;
using PersonalStudentProject.DataAccess.Models;
using Xunit;

namespace PersonalStudentProject.Tests;

public class RoomServiceTests
{
    private readonly Mock<IRoomRepository>    _roomRepo = new();
    private readonly Mock<IValidationService> _validation = new();
    private readonly Mock<IHttpContextAccessor> _http = new();

    private RoomService CreateService() =>
        new(_roomRepo.Object, _validation.Object, _http.Object);

    [Fact]
    public async Task CheckPassword_RoomNotFound_ThrowsException()
    {
        _roomRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Room?)null);

        await Assert.ThrowsAsync<Exception>(() => CreateService().checkPasswordAsync(99, "pass"));
    }

    [Fact]
    public async Task CheckPassword_UnlockedRoom_ReturnsTrue()
    {
        var room = new Room { Id = 1, isPasswordProtected = false };
        _roomRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(room);

        var result = await CreateService().checkPasswordAsync(1, "anything");

        Assert.True(result);
    }

    [Fact]
    public async Task CheckPassword_CorrectPassword_ReturnsTrue()
    {
        var room = new Room { Id = 2, isPasswordProtected = true, Password = "secret" };
        _roomRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(room);

        var result = await CreateService().checkPasswordAsync(2, "secret");

        Assert.True(result);
    }

    [Fact]
    public async Task CheckPassword_WrongPassword_ReturnsFalse()
    {
        var room = new Room { Id = 3, isPasswordProtected = true, Password = "secret" };
        _roomRepo.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(room);

        var result = await CreateService().checkPasswordAsync(3, "wrong");

        Assert.False(result);
    }

    [Fact]
    public async Task Update_RoomNotFound_ThrowsException()
    {
        _roomRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Room?)null);

        await Assert.ThrowsAsync<Exception>(() =>
            CreateService().updateAsync(99, new RoomDto()));
    }

    [Fact]
    public async Task Delete_RoomNotFound_ThrowsException()
    {
        _roomRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Room?)null);

        await Assert.ThrowsAsync<Exception>(() =>
            CreateService().deleteAsync(99));
    }

    [Fact]
    public async Task AddRoom_MapsFieldsCorrectly()
    {
        var dto = new RoomDto
        {
            userId = 5,
            RoomName = "Study Group",
            RoomType = "Public",
            isPasswordProtected = false,
            Password = "",
            Description = "Math study",
            ExpiryDate = new DateTime(2026, 12, 31),
            Capacity = 20,
            Location = "Liège"
        };

        _roomRepo.Setup(r => r.AddAsync(It.IsAny<Room>()))
                 .ReturnsAsync((Room r) => r);

        var result = await CreateService().addAsync(dto);

        Assert.Equal("Study Group", result.RoomName);
        Assert.Equal(20, result.Capacity);
        Assert.Equal("Liège", result.Location);
        Assert.Equal(5, result.userId);
    }
}
