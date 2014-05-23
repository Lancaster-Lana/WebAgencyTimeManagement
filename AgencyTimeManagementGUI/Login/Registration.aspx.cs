using System;

    public partial class Registration : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            /*
            lblError.Visible = false;
            DynamicMenu.CommonClasses.WSResults.UserRegistrateResult registerResult;
            if (!(txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim()))
            {
                lblError.Text = "Password and Confirm password field do not match";
                lblError.Visible = true;
                return;
            }
            try
            {
                registerResult = WSRepository.WSMethods.UserWebMethods.UserRegistrate(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                if (!string.IsNullOrEmpty(registerResult.ErrorMessage))
                {
                    lblError.Text = registerResult.ErrorMessage;
                    lblError.Visible = true;
                }
                else
                    Response.Redirect("~/Startup.aspx", true);
            }
            catch (Exception ex)
            {
                throw;
            }*/
        }

        public override string MenuItemName()
        {
            return "Users";
        }

        public override string[] CapabilityNames()
        {
            return new string[] { "Users" };
        }
    }
