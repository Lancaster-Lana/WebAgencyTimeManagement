using System;
using System.Windows.Forms;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.BackOffice
{
    public partial class UserRolesForm : Form
    {
        public UserRolesForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            //1.1
            lstUsers.ValueMember = "ID";
            lstUsers.DisplayMember = "WindowsAccountName";
            //1.2
            lstRoles.ValueMember = "ID";
            lstRoles.DisplayMember = "RoleName";
            //1.3
            lstUserRoles.ValueMember = "ID";//"UserRoleId"
            lstUserRoles.DisplayMember = "RoleName";

            //2.  Fill data to form controls
            FillForm();
        }

        private void FillForm()
        {
            BindUsersList();
            BindRolesList();
            if (lstUsers.Items.Count > 0)
                lstUsers.SelectedIndex = 0;
        }

        #region Binding Methods

        private void BindUsersList()
        {
            //1. Fill Users 
            lstUsers.DataSource = DBMethods.UsersList;
            lstUsers.Update();
        }

        private void BindRolesList()
        {
            //2. Fill Roles 
            lstRoles.DataSource = DBMethods.RolesList;
            lstRoles.Update();
        }

        /// <summary>
        /// Bind Roles list without assigned user roles
        /// </summary>
        /// <param name="userID"></param>
        private void BindRolesList(ENTUserAccountEO user)
        {
            var unassignedUserRoles = DBMethods.RolesList;
            var userRoles = user.Roles;
            unassignedUserRoles.RemoveAll(role => userRoles.Exists(userRole => userRole.RoleName == role.RoleName));
            lstRoles.DataSource = unassignedUserRoles;
            lstRoles.Update();
        }

        private void BindUserRolesList(ENTUserAccountEO user)
        {
            lstUserRoles.DataSource = user.Roles;//DBMethods.GetUserRolesList(userId);
            lstUserRoles.Update();
        }

        #endregion

        #region Handlers and Events

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedValue != null)
            {
                var userId = (int)lstUsers.SelectedValue;
                var user = (ENTUserAccountEO)lstUsers.SelectedItem;//DBMethods.UsersList.GetByID(userId);
                BindUserRolesList(user);
                BindRolesList(user);
            }
        }

        private void btnAssingUserToRole_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedValue != null && lstRoles.SelectedValue != null)
            {
                //var userId = (int)lstUsers.SelectedValue;
                var user = (ENTUserAccountEO)lstUsers.SelectedItem;//DBMethods.UsersList.GetByID(userId);
                //var roleId = (int)lstRoles.SelectedValue;
                var role = (ENTRoleEO)lstRoles.SelectedItem;

                //1.DB assign user to role
                DBMethods.AssignRoleToUser(user, role);

                //2.Show assigned/unassigned roles lists
                BindUserRolesList(user);//lstUserRoles.Items.Remove(userRoleId); //unassigned roles list
                BindRolesList(user); //lstRoles.Items.Add(userRoleId);                                                 
            }
        }

        private void btnUnassignUserRole_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedValue != null && lstUserRoles.SelectedValue != null)
            {
                var user = (ENTUserAccountEO)lstUsers.SelectedValue;
                var userRole = (ENTRoleEO)lstUserRoles.SelectedValue;

                //1.DB unassign user from role
                DBMethods.UnassignUserFromRole(user, userRole);

                //2.Show assigned/unassigned roles lists
                BindUserRolesList(user);
                BindRolesList(user);
            }
        }

        private void lstRoles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnAssingUserToRole_Click(sender, null);
        }

        private void lstUserRoles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnUnassignUserRole_Click(sender, null);
        }

        #endregion
    }
}
