using FleXFrame.AuthHub.Helpers;
using FleXFrame.AuthHub.Interfaces.IServices;
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
    public partial class DashboardForm : Form
    {

        public DashboardForm()
        {
            InitializeComponent();
            
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            var userLoginResult = FormFactory.OpenFormAsDialog<UserLoginForm>();

            if (userLoginResult != DialogResult.OK)
                Application.Exit();
            else
                EnableControls();
            

        }

        private void EnableControls()
        {
            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FormFactory.OpenFormInPanel(splitContainer1, () => new UserRegistrationForm());
            FormFactory.OpenFormInPanel<HomeForm>(splitContainer1);
            //FormFactory.OpenFormInPanel<UserRegistrationForm>(splitContainer1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormFactory.OpenFormInPanel<StudentPaymentForm>(splitContainer1);
        }

        private void splitContainer1_SplitterMoved_1(object sender, SplitterEventArgs e)
        {

        }
    }
}
