using FleXFrame.Core;
using FleXFrame.Core.Models;
using FleXFrame.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FleXFrame.Core.UnitTest
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        private readonly DbContextOptions<DataContext> _options;

        //public UserRepositoryTests()
        //{
        //    // Set up the in-memory database
        //    _options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDatabase")
        //        .Options;

        //    // Seed the database with some test data
        //    using (var context = new DataContext(_options))
        //    {
        //        context.Users.Add(new User { UserID = "789", Username = "testuser", PasswordHash = new byte[0], PasswordSalt = new byte[0], Name = "test", CreatedBy = "testdata" });
        //        context.SaveChanges();
        //    }

        //    _userRepository = new UserRepository(new DataContext(_options));
        //}

        //[Fact]
        //public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        //{
        //    // Arrange
        //    var userID = "789"; // Change to the seeded ID

        //    // Act
        //    var result = await _userRepository.GetUserByIDAsync(userID);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(userID, result.UserID);
        //}

        //[Fact]
        //public async Task AddUserAsync_AddsUser()
        //{
        //    // Arrange
        //    var newUser = new User { UserID = "456", Username = "testuser2", PasswordHash = new byte[0], PasswordSalt = new byte[0], Name = "test2", CreatedBy = "testdata2" };

        //    // Act
        //    await _userRepository.AddUserAsync(newUser);

        //    // Assert
        //    using (var context = new DataContext(_options))
        //    {
        //        var user = await context.Users.FindAsync("456");
        //        Assert.NotNull(user);
        //        Assert.Equal(newUser.Username, user.Username);
        //    }
        //}
    }
}
