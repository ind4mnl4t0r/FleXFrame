using AutoMapper;
using FleXCore.Utilities;
using FleXFrameCore.UserAuth;
using FleXFrameCore.UserAuth.DTOs;
using FleXFrameCore.UserAuth.Helpers;
using FleXFrameCore.UserAuth.Models;
using FleXFrameCore.UserAuth.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FleXFrameCore.UnitTest
{
    public class UserServiceTests
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            // Configure the in-memory database for testing
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);

            // Setup AutoMapper if necessary
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles()); // Your AutoMapper profile here
            });
            _mapper = mockMapper.CreateMapper();

            // Initialize UserService with the in-memory DataContext and AutoMapper instance
            _userService = new UserService(_context, _mapper);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldGenerateNewUserIdAndSaveUser()
        {
            // Arrange
            // Seed an existing user to set up the next UserID sequence
            var existingUser = new User
            {
                UserID = "USER-001",
                Username = "existinguser",
                Name = "Existing User",
                PasswordHash = new byte[] { 1, 2, 3 }, // Use non-empty byte array
                PasswordSalt = new byte[] { 4, 5, 6 }, // Use non-empty byte array
                CreatedBy = "admin"
            };
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            // Set up the new user data for creation
            var userCreateDto = new UserCreateDto
            {
                Username = "newuser",
                Name = "New User",
                PasswordHash = new byte[] { 7, 8, 9 }, // Use non-empty byte array
                PasswordSalt = new byte[] { 10, 11, 12 }, // Use non-empty byte array
                CreatedBy = "admin",
                DateCreated = DateTime.UtcNow
            };

            // Act
            var result = await _userService.CreateUserAsync(userCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("newuser", result.Username);
            Assert.Equal("New User", result.Name);

            // Verify UserID format and increment
            Assert.Matches(@"USER-\d{3}", result.UserID);
            Assert.Equal("USER-002", result.UserID);

            // Verify the user was added to the database
            var createdUser = await _context.Users.FindAsync(result.UserID);
            Assert.NotNull(createdUser);
            Assert.Equal("newuser", createdUser.Username);
            Assert.Equal("New User", createdUser.Name);
        }
    }
}
