using System;
using System.Linq;
using System.Security.Principal;
using Agency.PaidTimeOffDAL;
using System.Data.Linq;
using System.DirectoryServices;

public partial class TestAdminPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (var db = new HRPaidTimeOffDataContext())
        {
            ENTUserAccount userAccount = db.ENTUserAccounts.FirstOrDefault(ua => ua.WindowsAccountName == @"admin");//ENTUserAccounts.FirstOrDefault();                                
            ViewState["ENTUserAccountId"] = userAccount.ENTUserAccountId;
            ViewState["InsertENTUserAccountId"] = userAccount.InsertENTUserAccountId;
            ViewState["InsertDate"] = userAccount.InsertDate;
            ViewState["Version"] = userAccount.Version;
        }
    }

    private void GetDomainInfo(ref string firstName, ref string lastName, ref string email)
    {
        var domain = Environment.UserDomainName;
        string userInQuestion = Environment.UserName;//identity.Name.Split('\\')[1];
        var entry = new DirectoryEntry("LDAP://" + domain);
        var adSearcher = new DirectorySearcher(entry);
        adSearcher.SearchScope = SearchScope.Subtree;
        adSearcher.Filter = "(&(objectClass=user)(samaccountname=" + userInQuestion + "))";
        SearchResult userObj = adSearcher.FindOne();
        if (userObj != null)
        {
            var props = new[] { "title", "mail" };
            foreach (var prop in props)
            {
                var res = String.Format("{0}", userObj.Properties[prop][0]);

            }
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        //Create an instance of the data context
        using (var db = new HRPaidTimeOffDataContext())
        {
            var idWindows = WindowsIdentity.GetCurrent();
            // The WindowsPrincipal object contains the current user's Group membership.
            var prinWindows = new WindowsPrincipal(idWindows);
            var domainUser = idWindows.Name;
            string firstName = Environment.UserName;
            string lastName = string.Empty;//prinWindows.Identity.Name;//TODO
            string email = string.Empty;

            GetDomainInfo(ref firstName, ref lastName, ref  email);

            //Create a new ENTUserAccount object and set the properties
            var userAccount = new ENTUserAccount
                                  {
                                      WindowsAccountName = domainUser,
                                      FirstName = firstName,//"FirstName",
                                      LastName = lastName,//"LastName",
                                      Email = email,
                                      IsActive = true,
                                      InsertDate = DateTime.Now,
                                      InsertENTUserAccountId = 1,
                                      UpdateDate = DateTime.Now,
                                      UpdateENTUserAccountId = 1
                                  };

            //Signal the context to insert this record
            db.ENTUserAccounts.InsertOnSubmit(userAccount);
            //Save the changes to the database
            db.SubmitChanges();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        var idWindows = WindowsIdentity.GetCurrent();
        // The WindowsPrincipal object contains the current user's Group membership.
        var prinWindows = new WindowsPrincipal(idWindows);
        var domainUser = idWindows.Name;
        string firstName = Environment.UserName;
        string lastName = string.Empty;//prinWindows.Identity.Name;//TODO
        string email = string.Empty;

        GetDomainInfo(ref firstName, ref lastName, ref  email);

        using (var db = new HRPaidTimeOffDataContext())
        {
            //Create a ENTUserAccount object and set the properties
            var userAccount = new ENTUserAccount
            {
                WindowsAccountName = @domainUser,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                IsActive = false,
                UpdateDate = DateTime.Now,
                UpdateENTUserAccountId = 1
            };
            userAccount.ENTUserAccountId = Convert.ToInt32(ViewState["ENTUserAccountId"]);
            userAccount.Version = (Binary)ViewState["Version"];
            db.ENTUserAccounts.Attach(userAccount, true);
            db.SubmitChanges();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        using (var db = new HRPaidTimeOffDataContext())
        {
            //Create a ENTUserAccount object and set the properties
            var userAccount = new ENTUserAccount();
            userAccount.ENTUserAccountId = Convert.ToInt32(ViewState["ENTUserAccountId"]);
            userAccount.Version = (Binary)ViewState["Version"];
            db.ENTUserAccounts.Attach(userAccount);
            db.ENTUserAccounts.DeleteOnSubmit(userAccount);
            db.SubmitChanges();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        var db = new HRPaidTimeOffDataContext();

        //        GridView1.DataSource = db.ENTUserSelectAll();
        GridView1.DataBind();
    }
    protected void btnCreateError_Click(object sender, EventArgs e)
    {
        int x = 0;
        try
        {
            x = 0;
            int y = 5 / x;
        }
        catch (DivideByZeroException divByZeroEx)
        {
            throw new Exception("You attempted to divide by zero.  Try again!");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public void Page_Error(object sender, EventArgs e)
    //{
    //    if (!EventLog.SourceExists("PaidTimeOff"))
    //    {
    //        EventLog.CreateEventSource("PaidTimeOff", "Application");
    //    }
    //    EventLog eventLog = new EventLog();
    //    eventLog.Log = "Application";
    //    eventLog.Source = "PaidTimeOff";
    //    eventLog.WriteEntry(Server.GetLastError().GetBaseException().ToString(), EventLogEntryType.Error);
    //    Response.Redirect("ErrorPage.aspx");
    //}
}
