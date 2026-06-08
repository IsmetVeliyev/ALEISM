using PersonalStudentProject.Business.Services;
using Xunit;

namespace PersonalStudentProject.Tests;

public class HashServiceTests
{
    private readonly HashService _sut = new();

    [Fact]
    public void HashPassword_ReturnsNonEmptyHash()
    {
        var hash = _sut.HashPassword("myPassword123");

        Assert.False(string.IsNullOrEmpty(hash));
        Assert.NotEqual("myPassword123", hash);
    }

    [Fact]
    public void HashPassword_SameInputProducesDifferentHashes()
    {
        var hash1 = _sut.HashPassword("password");
        var hash2 = _sut.HashPassword("password");

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        var hash = _sut.HashPassword("correct");

        Assert.True(_sut.VerifyPassword("correct", hash));
    }

    [Fact]
    public void VerifyPassword_WrongPassword_ReturnsFalse()
    {
        var hash = _sut.HashPassword("correct");

        Assert.False(_sut.VerifyPassword("wrong", hash));
    }
}
