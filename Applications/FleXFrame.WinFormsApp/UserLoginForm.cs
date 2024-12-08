using FleXFrame.AuthHub.Helpers;
using FleXFrame.AuthHub.Interfaces.IServices;
using FleXFrame.AuthHub.Models;
using FleXFrame.AuthHub.Services;
using FleXFrame.UtilityHub.WinForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleXFrame.WinFormsApp
{
    public partial class UserLoginForm : Form
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        private readonly JwtHelper _jwtHelper;

        public UserLoginForm(IUserService userService, ISessionService sessionService, JwtHelper jwtHelper)
        {
            InitializeComponent();
            _userService = userService;
            _sessionService = sessionService;
            _jwtHelper = jwtHelper;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var _username = txtUsername.Text;
            var _password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }


            var userValidationResult = await _userService.ValidateUserAsync(_username, _password);

            if (userValidationResult.IsSuccess && userValidationResult.Data != null)
            {
                var user = userValidationResult.Data;

                // Generate the JWT token
                var token = _jwtHelper.GenerateToken(user);

                // Create session and generate JWT
                // Create session with the generated token
                var session = new Session
                {
                    UserID = user.UserID,
                    Token = token, // Assign the generated token
                    ExpiryTime = DateTime.UtcNow.AddMinutes(30), // Set expiry time
                    IPAddress = GetClientIPAddress(),
                    DeviceInfo = GetDeviceInfo(),
                    SessionStatus = Session.SessionStatuses.Active
                };

                var tokenResult = await _sessionService.StartSessionAsync(session, _jwtHelper);

                if (tokenResult.IsSuccess)
                {
                    // Store token for later use
                    TokenStorage.SaveToken(token);
                    SessionStorage.StartSession(session);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    //MessageBox.Show("Login successful!");
                    //FormFactory.OpenForm<DashboardForm>(closeCurrentForm: true);
                }
                else
                {
                    MessageBox.Show(tokenResult.Error ?? "Failed to start session.");
                }

               

                //FormFactory.OpenForm<DashboardForm>(closeCurrentForm: true);
                //FormFactory.OpenForm(() => new DashboardForm());

            }

            else
            {
                MessageBox.Show(userValidationResult.Error ?? "Invalid username or password");
            }
        }

        private string GetClientIPAddress()
        {
            // Retrieve the client IP address (e.g., using network adapters or system API)
            return "127.0.0.1"; // Example IP
        }

        private string GetDeviceInfo()
        {
            // Retrieve device info (e.g., machine name, OS version)
            return Environment.MachineName;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //FormFactory.OpenForm<UserRegistrationForm>(closeCurrentForm: true);
            FormFactory.OpenForm(() => new UserRegistrationForm(_userService));
            //FormFactory.OpenForm<UserRegistrationForm>(closeCurrentForm: true);
        }
    }
}
