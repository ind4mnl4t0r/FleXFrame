﻿namespace FleXFrame.WinFormsApp
{
    partial class DashboardForm
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
            menuStrip = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            button4 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1271, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            menuStrip.ItemClicked += menuStrip1_ItemClicked;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(button4);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanel1);
            splitContainer1.Panel1.Controls.Add(button2);
            splitContainer1.Panel1.Controls.Add(button3);
            splitContainer1.Panel1.Enabled = false;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Enabled = false;
            splitContainer1.Size = new Size(1271, 581);
            splitContainer1.SplitterDistance = 124;
            splitContainer1.TabIndex = 1;
            // 
            // button4
            // 
            button4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button4.Dock = DockStyle.Top;
            button4.Location = new Point(0, 104);
            button4.Name = "button4";
            button4.Size = new Size(124, 52);
            button4.TabIndex = 3;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = FlowDirection.BottomUp;
            flowLayoutPanel1.Location = new Point(0, 429);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(124, 152);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(3, 97);
            button1.Name = "button1";
            button1.Size = new Size(75, 52);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Top;
            button2.Location = new Point(0, 52);
            button2.Name = "button2";
            button2.Size = new Size(124, 52);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button3.Dock = DockStyle.Top;
            button3.Location = new Point(0, 0);
            button3.Name = "button3";
            button3.Size = new Size(124, 52);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1271, 605);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DashboardForm";
            WindowState = FormWindowState.Maximized;
            Load += DashboardForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SplitContainer splitContainer1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}