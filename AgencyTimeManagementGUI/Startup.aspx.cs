using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AgencyTimeManagementGUI
{
    public partial class Startup : Page
    {
        //public MembershipProvider Provider
        //{
        //    get;
        //    private set;
        //}

        protected void ctrlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var ctrlLogin = (Login) sender;

            string userName = ctrlLogin.UserName;
            string password = ctrlLogin.Password;

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                //ctrlLogin.errorLabel.Text = UserSettingManager.Instance.GetLangMessage(rms.UsernameAndPasswordWereLeftBlank).Caption;
                e.Authenticated = false;
            }
            else
            {
                try
                {
                    var isCurrentUserValid = Membership.ValidateUser(userName, password);
                        //Provider.ValidateUser(User.Identity.Name, ctrlLogin.Password);

                    //AuthentificationServiceProxy.UserAuthenticate(userName, password);                                    
                    e.Authenticated = isCurrentUserValid; // authResult.IsAuthenticated;//authResult.IsAuthenticated;

                    if (!e.Authenticated)
                    {
                        //txtStatus.Visible = true;
                        //txtStatus.Text = authResult.ErrorMessage;
                        return;
                    }
                    Session["UserID"] = userName;
                }
                catch (Exception ex)
                {
                    txtStatus.Text = ex.ToString();
                }
            }

            //if (e.Authenticated)
            //{
            //Response.Redirect("~/Default.aspx");                                 
            //}            
        }

        protected void ctrlLogin_LoggedIn(object sender, EventArgs e)
        {
            var ctrlLogin = (Login) sender;

            string userName = ctrlLogin.UserName;
            string password = ctrlLogin.Password;
            bool isRememberMeSet = ctrlLogin.RememberMeSet;

            FormsAuthentication.SetAuthCookie(userName, isRememberMeSet);
            FormsAuthentication.RedirectFromLoginPage(userName, true);
            //Response.Redirect("Default.aspx", true);
        }

        protected void ctrlLogin_LoginError(object sender, EventArgs e)
        {
            //FormsAuthentication.SignOut();                        
            //HyperLink lnkRegister = (HyperLink)FindControl("lnkRegister");
            //lnkRegister.Visible = true;
        }
    }
}