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

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var serviceProvider = new ServiceCollection()
    .AddDbContext<DataContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")))
    .AddAutoMapper(typeof(MappingProfiles))
    .AddScoped<UserService>()
    .BuildServiceProvider();

using var context = serviceProvider.GetService<DataContext>();
context.Database.Migrate();

Console.WriteLine("Migration completed");


// Example: Adding a new user
var userService = serviceProvider.GetService<UserService>();

if (userService == null)
{
    Console.WriteLine("UserService is not available.");
    return;
}

var newUserDto = new UserCreateDto
{
    Username = "newuser123",
    Name = "New User",
    PasswordHash = new byte[] { /* set password hash */ },
    PasswordSalt = new byte[] { /* set password salt */ },
    DateCreated = DateTime.Now,
    CreatedBy = "admin"
};

try
{
    var createdUser = await userService.CreateUserAsync(newUserDto);
    Console.WriteLine($"User created with ID: {createdUser.UserID}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error creating user: {ex.Message}");
}

// Get the user

//var user = await userService.GetUserByIdAsync("2");
//if (user.IsSuccess)
//    Console.WriteLine(user.Data.Username + "-" + user.Data.Name);
//else
//    Console.WriteLine(user.Error);


//if (user != null)
//{
//    Console.WriteLine(user.Username + "-" + user.Name);
//    //user.Username = "updatedusername";
//    //await userService.UpdateUserAsync(user);
//}

// Delete the user
//await userService.DeleteUserAsync("1");

//Console.WriteLine("User operations completed.");
