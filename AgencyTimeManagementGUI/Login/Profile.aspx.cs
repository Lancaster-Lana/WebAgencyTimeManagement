using System;

namespace Agency.WebSite
{
    public partial class Profile : BasePage
    {
        private const string VIEW_STATE_KEY_USER = "User";

        void Master_CancelButton_Click(object sender, EventArgs e)
        {
            //GoToGridPage();
        }

        //void Master_SaveButton_Click(object sender, EventArgs e)
        //{
        //    ENTValidationErrors validationErrors = new ENTValidationErrors();

        //    ENTUserAccountEO userAccount = (ENTUserAccountEO)ViewState[VIEW_STATE_KEY_USER];
        //    LoadObjectFromScreen(userAccount);

        //    if (!userAccount.Save(ref validationErrors, 1))
        //    {
        //        Master.ValidationErrors = validationErrors;
        //    }
        //    else
        //    {
        //        GoToGridPage();
        //    }
        //}

        //protected override void LoadObjectFromScreen(ENTUserAccountEO baseEO)
        //{
        //    baseEO.WindowsAccountName = txtWindowsAccountName.Text;
        //    baseEO.FirstName = txtFirstName.Text;
        //    baseEO.LastName = txtLastName.Text;
        //    baseEO.Email = txtEmail.Text;
        //    baseEO.IsActive = chkActive.Checked;
        //}

        //protected override void LoadScreenFromObject(ENTUserAccountEO baseEO)
        //{
        //    txtWindowsAccountName.Text = baseEO.WindowsAccountName;
        //    txtFirstName.Text = baseEO.FirstName;
        //    txtLastName.Text = baseEO.LastName;
        //    txtEmail.Text = baseEO.Email;
        //    chkActive.Checked = baseEO.IsActive;

        //    //Put the object in the view state so it can be attached back to the data context.
        //   ViewState[VIEW_STATE_KEY_USER] = baseEO;
        //}

        //protected override void LoadControls()
        //{

        //}

        //protected /*override*/ void GoToGridPage()
        //{
        //    //Response.Redirect("Users.aspx");
        //}

        public override string MenuItemName()
        {
            return "Users";
        }

        public override string[] CapabilityNames()
        {
            return new string[] { "Users" };
        }

    }
}
