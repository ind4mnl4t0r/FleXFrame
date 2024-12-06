namespace FleXFrame.WinFormsApp
{
    partial class UserLoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            label1 = new Label();
            txtUsername = new TextBox();
            btnCreate = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 165);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 9;
            label2.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(47, 183);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(219, 23);
            txtPassword.TabIndex = 8;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(99, 296);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(111, 48);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 106);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 6;
            label1.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(47, 124);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(219, 23);
            txtUsername.TabIndex = 5;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(99, 363);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(111, 48);
            btnCreate.TabIndex = 10;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // UserLoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 450);
            Controls.Add(btnCreate);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(label1);
            Controls.Add(txtUsername);
            Name = "UserLoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label label1;
        private TextBox txtUsername;
        private Button btnCreate;
    }
}