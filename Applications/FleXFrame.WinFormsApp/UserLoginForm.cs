using FleXFrame.Core.Services;
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
        private readonly UserService _userService;

        public UserLoginForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var _username = txtUsername.Text;
            var _password = txtPassword.Text;

            if(string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }

            bool isValidUser = await _userService.ValidateUserAsync(_username, _password);

            if (isValidUser)
            {
                FormFactory.OpenForm<DashboardForm>(closeCurrentForm: true);
            }

            else
            {
                MessageBox.Show("invalid username or password");
            }
        }

    }
}
