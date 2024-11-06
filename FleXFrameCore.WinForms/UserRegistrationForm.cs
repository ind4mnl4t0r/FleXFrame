using FleXFrameCore.UserAuth.Helpers;
using FleXFrameCore.UserAuth.Services;
using FleXFrameCore.UserAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FleXCore.Utilities;
using FleXFrameCore.UserAuth.DTOs;
using FleXFrameCore.UtilityHub;
using System;

namespace FleXFrameCore.WinForms
{
    public partial class UserRegistrationForm : Form
    {
        private readonly UserService _userService;
        private string? _username, _password, _name;

        public UserRegistrationForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
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

            // Get the latest user and determine the sequence number for new user ID
            var latestUser = await _userService.GetLatestUserAsync();
            int sequenceNumber = 1;
            if (latestUser?.UserID != null)
            {
                var sequencePart = latestUser.UserID.Substring(latestUser.UserID.Length - 4);
                if (int.TryParse(sequencePart, out int latestSequence))
                {
                    sequenceNumber = latestSequence + 1;
                }
            }
            string newUserID = IDGenerator.GenerateID("CUST-USER-{S}", sequenceNumber);

            // Create the new UserCreateDto
            var userDto = new UserCreateDto
            {
                UserID = newUserID,
                Username = _username,
                Name = _name,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                DateCreated = DateTime.Now,
                CreatedBy = "System"
            };

            // Act: Create the user using the UserService
            string createdUserID = await _userService.CreateUserAsync(userDto);

            // Display confirmation
            MessageBox.Show($"User {_username} created successfully with UserID: {createdUserID}");
        }
    }
}
