using System;
using System.Windows.Forms;

namespace Agency.BackOffice
{
    public partial class AdministationForm : Form
    {
        public AdministationForm()
        {
            InitializeComponent();LoadPermissionsForm();
        }

        #region Handlers and Events

        private void miOpenPermissions_Click(object sender, EventArgs e)
        {
            LoadPermissionsForm();
        }

        private void LoadPermissionsForm()
        {
            UserRolesForm urForm = null;
            try
            {
                urForm = new UserRolesForm();
                urForm.MdiParent = this;
                urForm.Show();
            }
            catch (Exception ex)
            {
                if (DialogResult.OK == MessageBox.Show(ex.Message, "Connection failed! ", MessageBoxButtons.OK, MessageBoxIcon.Error))
                {
                    //using (var connectionForm = new LoginForm())
                    //{
                    //    connectionForm.Show();
                    //    if (urForm != null) urForm.Close();
                    //}
                }
            }
        }

        #endregion
    }
}
