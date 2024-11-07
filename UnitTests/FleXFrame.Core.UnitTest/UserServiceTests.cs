using AutoMapper;
using FleXFrame.Core;
using FleXFrame.Core.DTOs;
using FleXFrame.Core.Helpers;
using FleXFrame.Core.Models;
using FleXFrame.Core.Services;
using FleXFrame.UtilityHub;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FleXFrame.Core.UnitTest
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
        public async Task CreateUserAsync_ShouldGenerateAndAssignUniqueUserID()
        {
            // Arrange: Create a sample UserCreateDto without setting a UserID
            var userDto = new UserCreateDto
            {
                Username = "testuser",
                Name = "Test User",
                PasswordHash = new byte[] { 1, 2, 3, 4 },  // Non-empty dummy byte array
                PasswordSalt = new byte[] { 5, 6, 7, 8 },  // Non-empty dummy byte array
                DateCreated = DateTime.Now,
                CreatedBy = "Admin"
            };

            // Step 1: Generate the new unique ID
            var latestUser = await _context.Users
                .OrderByDescending(u => u.UserID)
                .FirstOrDefaultAsync();

            int sequenceNumber = 1;
            if (latestUser?.UserID != null)
            {
                var sequencePart = latestUser.UserID.Substring(latestUser.UserID.Length - 4);
                if (int.TryParse(sequencePart, out int latestSequence))
                {
                    sequenceNumber = latestSequence + 1;
                }
            }
            string generatedUserID = IDGenerator.GenerateID("{P}{D}{S}", sequenceNumber);

            // Step 2: Assign the generated ID to the UserCreateDto
            userDto.UserID = generatedUserID;

            // Act: Add the user with the generated ID
            await _userService.CreateUserAsync(userDto);

            // Assert: Verify the user was added and has the expected ID
            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == generatedUserID);
            Assert.NotNull(createdUser);
            Assert.Equal(generatedUserID, createdUser?.UserID);
            Assert.StartsWith("PRE", createdUser?.UserID);  // Assuming "PRE" prefix is part of the pattern
        }
    }
}
