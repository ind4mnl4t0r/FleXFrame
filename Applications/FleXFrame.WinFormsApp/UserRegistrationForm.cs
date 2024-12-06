using FleXFrame.AuthHub.Helpers;
using FleXFrame.AuthHub.Services;
using FleXFrame.AuthHub;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FleXFrame.AuthHub.DTOs;
using FleXFrame.UtilityHub;
using System;
using FleXFrame.AuthHub.DTOs.UserDtos;
using FleXFrame.AuthHub.Interfaces.IServices;

namespace FleXFrame.WinFormsApp
{
    public partial class UserRegistrationForm : Form
    {
        private readonly IUserService _userService;
        private string? _username, _password, _name;

        public UserRegistrationForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from TextBox controls
                _username = txtUsername.Text;
                _password = txtPassword.Text;
                _name = txtName.Text;

                // Validate required fields
                if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password) || string.IsNullOrWhiteSpace(_name))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // Generate salt and hash the password using PasswordEngine
                byte[] salt = PasswordEngine.GenerateSalt();
                byte[] hashedPassword = PasswordEngine.HashPassword(_password, salt);

                // Create the new UserCreateDto
                var userCreateDto = new UserCreateDto
                {
                    Username = _username,
                    Name = _name,
                    PasswordHash = hashedPassword,
                    PasswordSalt = salt,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System"
                };

                // Act: Create the user using the UserService
                var result = await _userService.CreateUserAsync(userCreateDto);
                //string createdUserID =  _userService.GenerateUserID();

                // Handle the result
                // Display confirmation on the UI thread
                if (result.IsSuccess)
                {
                    MessageBox.Show($"User {_username} created successfully with UserID: {result.Data}");
                }
                else
                {
                    MessageBox.Show(result.Error ?? "An error occurred while creating the user.");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
