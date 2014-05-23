using System;
namespace Agency.RoleSecurity
{
    partial class WindowsSecurityInfoCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.pagWindows = new System.Windows.Forms.TabPage();
            this.btnAddWinUserToDB = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.ckUsers = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.ckManagers = new System.Windows.Forms.CheckBox();
            this.ckPowerUsers = new System.Windows.Forms.CheckBox();
            this.ckAdministrator = new System.Windows.Forms.CheckBox();
            this.TabControl1.SuspendLayout();
            this.pagWindows.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.pagWindows);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.ItemSize = new System.Drawing.Size(86, 18);
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(415, 214);
            this.TabControl1.TabIndex = 2;
            // 
            // pagWindows
            // 
            this.pagWindows.Controls.Add(this.btnAddWinUserToDB);
            this.pagWindows.Controls.Add(this.txtLogin);
            this.pagWindows.Controls.Add(this.ckUsers);
            this.pagWindows.Controls.Add(this.btnLogin);
            this.pagWindows.Controls.Add(this.ckManagers);
            this.pagWindows.Controls.Add(this.ckPowerUsers);
            this.pagWindows.Controls.Add(this.ckAdministrator);
            this.pagWindows.Location = new System.Drawing.Point(4, 22);
            this.pagWindows.Name = "pagWindows";
            this.pagWindows.Size = new System.Drawing.Size(407, 188);
            this.pagWindows.TabIndex = 0;
            this.pagWindows.Text = "WindowsIdentity and WindowsPrincipal";
            // 
            // btnAddWinUserToDB
            // 
            this.btnAddWinUserToDB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddWinUserToDB.Location = new System.Drawing.Point(192, 11);
            this.btnAddWinUserToDB.Name = "btnAddWinUserToDB";
            this.btnAddWinUserToDB.Size = new System.Drawing.Size(176, 23);
            this.btnAddWinUserToDB.TabIndex = 15;
            this.btnAddWinUserToDB.Text = "Include the Windows User to DB";
            this.btnAddWinUserToDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddWinUserToDB.Click += new System.EventHandler(this.btnAddWinUserToDB_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogin.Location = new System.Drawing.Point(192, 40);
            this.txtLogin.Multiline = true;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(207, 117);
            this.txtLogin.TabIndex = 14;
            // 
            // ckUsers
            // 
            this.ckUsers.AutoCheck = false;
            this.ckUsers.CausesValidation = false;
            this.ckUsers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckUsers.Location = new System.Drawing.Point(16, 88);
            this.ckUsers.Name = "ckUsers";
            this.ckUsers.Size = new System.Drawing.Size(176, 24);
            this.ckUsers.TabIndex = 12;
            this.ckUsers.Text = "Is in Users group";
            // 
            // btnLogin
            // 
            this.btnLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLogin.Location = new System.Drawing.Point(16, 11);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(138, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Retrieve Current User Info";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // ckManagers
            // 
            this.ckManagers.AutoCheck = false;
            this.ckManagers.CausesValidation = false;
            this.ckManagers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckManagers.Location = new System.Drawing.Point(16, 112);
            this.ckManagers.Name = "ckManagers";
            this.ckManagers.Size = new System.Drawing.Size(176, 24);
            this.ckManagers.TabIndex = 13;
            this.ckManagers.Text = "Is in custom Managers group";
            // 
            // ckPowerUsers
            // 
            this.ckPowerUsers.AutoCheck = false;
            this.ckPowerUsers.CausesValidation = false;
            this.ckPowerUsers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckPowerUsers.Location = new System.Drawing.Point(16, 64);
            this.ckPowerUsers.Name = "ckPowerUsers";
            this.ckPowerUsers.Size = new System.Drawing.Size(176, 24);
            this.ckPowerUsers.TabIndex = 11;
            this.ckPowerUsers.Text = "Is in Power Users group";
            // 
            // ckAdministrator
            // 
            this.ckAdministrator.AutoCheck = false;
            this.ckAdministrator.CausesValidation = false;
            this.ckAdministrator.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckAdministrator.Location = new System.Drawing.Point(16, 40);
            this.ckAdministrator.Name = "ckAdministrator";
            this.ckAdministrator.Size = new System.Drawing.Size(176, 24);
            this.ckAdministrator.TabIndex = 10;
            this.ckAdministrator.Text = "Is in Administrators group";
            // 
            // WindowsSecurityInfoCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl1);
            this.Name = "WindowsSecurityInfoCtrl";
            this.Size = new System.Drawing.Size(415, 214);
            this.Load += new System.EventHandler(this.WindowsSecurityInfoCtrl_Load);
            this.TabControl1.ResumeLayout(false);
            this.pagWindows.ResumeLayout(false);
            this.pagWindows.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage pagWindows;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.CheckBox ckUsers;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox ckManagers;
        private System.Windows.Forms.CheckBox ckPowerUsers;
        private System.Windows.Forms.CheckBox ckAdministrator;
        private System.Windows.Forms.Button btnAddWinUserToDB;
    }
}
