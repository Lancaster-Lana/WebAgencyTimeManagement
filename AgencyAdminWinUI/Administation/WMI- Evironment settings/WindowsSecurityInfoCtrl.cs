using System;
using System.Windows.Forms;
using System.Security.Principal;
using System.Security.Permissions;
using Agency.BackOffice;

namespace Agency.RoleSecurity
{
    public partial class WindowsSecurityInfoCtrl : UserControl
    {
        // Declare the WindowsIdentity and WindowsPrincipal objects
        // with class scope so that they can be called by any procedures
        private WindowsIdentity idWindows;
        private WindowsPrincipal prinWindows;
        private readonly string machName = Environment.MachineName;

        public WindowsSecurityInfoCtrl()
        {
            InitializeComponent();
        }

        private void WindowsSecurityInfoCtrl_Load(object sender, EventArgs e)
        {
            // Setting the PrincipalPolicy to WindowsPrincipal specifies how principal 
            // and identity objects should be attached to a thread if the thread 
            // attempts to bind to a principal. In this case, we set the PrincipalPolicy 
            // enumeration to WindowsPrincipal, which maps operating system groups to roles.
            // This statement is needed for role-based security demands.
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            // Create a WindowsIdentity object for the current user 
            idWindows = WindowsIdentity.GetCurrent();
            prinWindows = new WindowsPrincipal(idWindows);
            ckAdministrator.Checked = prinWindows.IsInRole(WindowsBuiltInRole.Administrator);
            ckPowerUsers.Checked = prinWindows.IsInRole(WindowsBuiltInRole.PowerUser);
            ckUsers.Checked = prinWindows.IsInRole(WindowsBuiltInRole.User);
            ckManagers.Checked = prinWindows.IsInRole(machName + @"\Managers");

            // Display the WindowsIdentity properties 
            txtLogin.AppendText(string.Format("Name:  {0}{1}", idWindows.Name, Environment.NewLine));
            txtLogin.AppendText(string.Format("AuthenticationType:  {0}{1}", idWindows.AuthenticationType, Environment.NewLine));
            txtLogin.AppendText(string.Format("IsAuthenticated:  {0}{1}", idWindows.IsAuthenticated, Environment.NewLine));
            txtLogin.AppendText(string.Format("IsAnonymous:  {0}{1}", idWindows.IsAnonymous, Environment.NewLine));
            txtLogin.AppendText(string.Format("IsGuest:  {0}{1}", idWindows.IsGuest, Environment.NewLine));
            txtLogin.AppendText(string.Format("IsSystem:  {0}{1}", idWindows.IsSystem, Environment.NewLine));
            txtLogin.AppendText(string.Format("Token:  {0}{1}", idWindows.Token, Environment.NewLine));
        }

        private void btnAddWinUserToDB_Click(object sender, EventArgs e)
        {
            DBMethods.AddWinUserToDB(idWindows);
        }

    }
}
