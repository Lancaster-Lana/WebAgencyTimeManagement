using Agency.PaidTimeOffBLL.Framework;
using System.Windows.Forms;

namespace Agency.BackOffice
{
    public class DBMethods
    {
        #region Properties

        //public static string DbWebServiceUrl
        //{
        //    get
        //    {
        //        return SettingsManager.GetConfigSettings("eAuthWebServiceUrl");
        //    }
        //    set
        //    {
        //        SettingsManager.GetConfigSettings("eAuthWebServiceUrl", value);
        //    }
        //}

        //static DBWebService.eAuthDBWebService _proxy;
        //public static DBWebService.eAuthDBWebService AuthentificationServiceProxy
        //{
        //    get
        //    {
        //        if (_proxy == null)                
        //            _proxy = new DynamicMenu.BackOffice.DBWebService.eAuthDBWebService();

        //            _proxy.Url = DbWebServiceUrl;
        //            // Or use WebRequest
        //            //WebRequest requestToWebService = WebRequest.Create(DbWebServiceUrl);
        //            //requestToWebService.GetResponse();                   

        //        return _proxy;
        //    }
        //}

        #endregion

        public static ENTUserAccountEOList UsersList
        {
            get
            {
                var users = new ENTUserAccountEOList();
                users.LoadWithRoles();
                return users;
            }
        }

        public static ENTRoleEOList RolesList
        {
            get
            {
                var roles = new ENTRoleEOList();
                roles.Load();
                return roles;
            }
        }

        #region Methods

        public static ENTRoleEOList GetUserRolesList(int userId)
        {
            var user = UsersList.GetByID(userId);
            return user.Roles;
        }


        public static void AssignRoleToUser(ENTUserAccountEO user, ENTRoleEO role)
        {
            role.RoleUserAccounts.Add(new ENTRoleUserAccountEO { ENTUserAccountId = user.ID, ENTRoleId = role.ID });
            //user.Roles.Add(role);
        }

        public static void UnassignUserFromRole(ENTUserAccountEO user, ENTRoleEO role)
        {
            role.RoleUserAccounts.Remove(new ENTRoleUserAccountEO { ENTUserAccountId = user.ID, ENTRoleId = role.ID });
        }

        internal static void AddWinUserToDB(System.Security.Principal.WindowsIdentity idWindows)
        {
            var dbUser = UsersList.Find((a) => a.WindowsAccountName == idWindows.Name);
            var adminUser = UsersList.Find((a) => a.WindowsAccountName == "admin");
            if (dbUser != null)
            {
                MessageBox.Show("The windows user " + idWindows.Name + " already exists in DB");
            }
            else
            {
                var cloneAdminUser = UsersList.Find((a) => a.WindowsAccountName == "LANA-PC\\Lana");
                dbUser = cloneAdminUser;
                ENTValidationErrors validationErrors = null;
                dbUser.Save(ref validationErrors, 0);//pass ID = 0, as it is new user
            }          
        }

        #endregion
    }
}
