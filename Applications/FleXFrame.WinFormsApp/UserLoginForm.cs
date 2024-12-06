using FleXFrame.AuthHub.Interfaces.IServices;
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

        public UserLoginForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
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


            var result = await _userService.ValidateUserAsync(_username, _password);

            if (result.IsSuccess && result.Data)
            {
                FormFactory.OpenForm<DashboardForm>(closeCurrentForm: true);
                //FormFactory.OpenForm(() => new DashboardForm());

            }

            else
            {
                MessageBox.Show(result.Error ?? "Invalid username or password");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            FormFactory.OpenForm(() => new UserRegistrationForm(_userService));
            //FormFactory.OpenForm<UserRegistrationForm>(closeCurrentForm: true);
        }
    }
}
