﻿using System;
using System.Configuration;
using System.Globalization;
using Agency.PaidTimeOffBLL.Framework;

public partial class PaidTimeOff : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ENTUserAccountEO currentUser = ((BasePage)Page).CurrentUser;

        //Set the top menu properties
        MenuTabs1.MenuItems = Globals.GetMenuItems(this.Cache);        
        MenuTabs1.RootPath = BasePage.RootPath(Context);
        MenuTabs1.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
        MenuTabs1.Roles = Globals.GetRoles(this.Cache);
        MenuTabs1.UserAccount = currentUser;

        //Set the side menu properties
        MenuTree1.MenuItems = Globals.GetMenuItems(this.Cache);        
        MenuTree1.RootPath = BasePage.RootPath(Context);
        MenuTree1.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
        MenuTree1.Roles = Globals.GetRoles(this.Cache);
        MenuTree1.UserAccount = currentUser;
        
        lblCurrentUser.Text = Page.User.Identity.Name;
        lblCurrentDateTime.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);

        //Set the version
        lblVersion.Text = ConfigurationManager.AppSettings["version"];
    }
}
