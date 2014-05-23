namespace Agency.BackOffice
{
    partial class UserRolesForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUsers = new System.Windows.Forms.Label();
            this.lblRoles = new System.Windows.Forms.Label();
            this.btnAssingUserTolRole = new System.Windows.Forms.Button();
            this.btnUnassignUserRole = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.lstUserRoles = new System.Windows.Forms.ListBox();
            this.windowsSecurityInfoCtrl1 = new Agency.RoleSecurity.WindowsSecurityInfoCtrl();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(652, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblUsers
            // 
            this.lblUsers.AutoSize = true;
            this.lblUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUsers.Location = new System.Drawing.Point(5, 219);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(39, 13);
            this.lblUsers.TabIndex = 1;
            this.lblUsers.Text = "Users";
            // 
            // lblRoles
            // 
            this.lblRoles.AutoSize = true;
            this.lblRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRoles.Location = new System.Drawing.Point(223, 219);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(104, 13);
            this.lblRoles.TabIndex = 3;
            this.lblRoles.Text = "Unassigned roles";
            // 
            // btnAssingUserTolRole
            // 
            this.btnAssingUserTolRole.AccessibleDescription = "Assing user to role";
            this.btnAssingUserTolRole.Location = new System.Drawing.Point(408, 264);
            this.btnAssingUserTolRole.Name = "btnAssingUserTolRole";
            this.btnAssingUserTolRole.Size = new System.Drawing.Size(38, 23);
            this.btnAssingUserTolRole.TabIndex = 8;
            this.btnAssingUserTolRole.Text = ">";
            this.btnAssingUserTolRole.UseVisualStyleBackColor = true;
            this.btnAssingUserTolRole.Click += new System.EventHandler(this.btnAssingUserToRole_Click);
            // 
            // btnUnassignUserRole
            // 
            this.btnUnassignUserRole.AccessibleDescription = "Unassign user role";
            this.btnUnassignUserRole.Location = new System.Drawing.Point(408, 286);
            this.btnUnassignUserRole.Name = "btnUnassignUserRole";
            this.btnUnassignUserRole.Size = new System.Drawing.Size(38, 23);
            this.btnUnassignUserRole.TabIndex = 9;
            this.btnUnassignUserRole.Text = "<";
            this.btnUnassignUserRole.UseVisualStyleBackColor = true;
            this.btnUnassignUserRole.Click += new System.EventHandler(this.btnUnassignUserRole_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(449, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Assigned roles";
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(8, 235);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(178, 108);
            this.lstUsers.TabIndex = 12;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(226, 235);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(177, 108);
            this.lstRoles.TabIndex = 13;
            this.lstRoles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRoles_MouseDoubleClick);
            // 
            // lstUserRoles
            // 
            this.lstUserRoles.FormattingEnabled = true;
            this.lstUserRoles.Location = new System.Drawing.Point(452, 235);
            this.lstUserRoles.Name = "lstUserRoles";
            this.lstUserRoles.Size = new System.Drawing.Size(177, 108);
            this.lstUserRoles.TabIndex = 14;
            this.lstUserRoles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstUserRoles_MouseDoubleClick);
            // 
            // windowsSecurityInfoCtrl1
            // 
            this.windowsSecurityInfoCtrl1.Location = new System.Drawing.Point(8, 12);
            this.windowsSecurityInfoCtrl1.Name = "windowsSecurityInfoCtrl1";
            this.windowsSecurityInfoCtrl1.Size = new System.Drawing.Size(426, 184);
            this.windowsSecurityInfoCtrl1.TabIndex = 15;
            // 
            // UserRolesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 375);
            this.Controls.Add(this.windowsSecurityInfoCtrl1);
            this.Controls.Add(this.lstUserRoles);
            this.Controls.Add(this.lstRoles);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUsers);
            this.Controls.Add(this.btnUnassignUserRole);
            this.Controls.Add(this.lblRoles);
            this.Controls.Add(this.btnAssingUserTolRole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(734, 537);
            this.Name = "UserRolesForm";
            this.Text = "Administation Settings";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.Button btnAssingUserTolRole;
        private System.Windows.Forms.Button btnUnassignUserRole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.ListBox lstUserRoles;
        private RoleSecurity.WindowsSecurityInfoCtrl windowsSecurityInfoCtrl1;
    }
}