using FleXCore.Utilities;
using FleXFrameCore.UserAuth;
using FleXFrameCore.UserAuth.DTOs;
using FleXFrameCore.UserAuth.Helpers;
using FleXFrameCore.UserAuth.Models;
using FleXFrameCore.UserAuth.Services;
using FleXFrameCore.UtilityHub.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace FleXFrameCoreConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Set up dependency injection
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")))
                .AddAutoMapper(typeof(MappingProfiles)) // Register AutoMapper profile
                .AddScoped<UserService>() // Register UserService
                .BuildServiceProvider();

            // Apply migrations (this happens only once, and the context will be disposed correctly by DI)
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
                Console.WriteLine("Migration completed");
            }

            // Example: Creating a new user
            var userService = serviceProvider.GetService<UserService>();

            if (userService == null)
            {
                Console.WriteLine("UserService is not available.");
                return;
            }



            // Generate a new user ID using the IDGenerator
            using (var scope = serviceProvider.CreateScope())
            {
                // Create a new UserCreateDto object
                var userDto = new UserCreateDto
                {
                    Username = "newuser",
                    Name = "New User",
                    PasswordHash = new byte[] { 1, 2, 3, 4 }, // Provide a simple hash for testing
                    PasswordSalt = new byte[] { 5, 6, 7, 8 }, // Provide a simple salt for testing
                    DateCreated = DateTime.Now,
                    CreatedBy = "System"
                };

                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                var latestUser = await context.Users.OrderByDescending(u => u.UserID).FirstOrDefaultAsync();

                int sequenceNumber = 1;
                if (latestUser?.UserID != null)
                {
                    var sequencePart = latestUser.UserID.Substring(latestUser.UserID.Length - 4);
                    if (int.TryParse(sequencePart, out int latestSequence))
                    {
                        sequenceNumber = latestSequence + 1;
                    }
                }

                // Generate the new user ID using IDGenerator
                string newUserID = IDGenerator.GenerateID("CUST-USER-{S}", sequenceNumber);

                // Assign the generated ID to the user DTO
                userDto.UserID = newUserID;

                // Act: Create the user using the UserService and capture the generated UserID
                string createdUserID = await userService.CreateUserAsync(userDto);

                // Log the UserID to the console (whether it's custom or default)
                Console.WriteLine($"User {userDto.Username} created successfully with UserID: {createdUserID}");
            }
        }
    }
}
