using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.FrameworkControls
{
    [ToolboxData("<{0}:MenuTabs runat=server></{0}:MenuTabs>")]
    public class MenuTabs : WebControl
    {
        #region Properties

        [Browsable(false)]
        public ENTMenuItemBOList MenuItems { get; set; }

        [Browsable(false)]
        public string CurrentMenuItemName { get; set; }

        [Browsable(true)]
        [DefaultValue("Enter Application Root Path")]
        [Description("Enter the root path for your application.  This is used to determine the path for all items in the menu.")]
        public string RootPath { get; set; }

        [Browsable(false)]
        public ENTUserAccountEO UserAccount { get; set; }

        [Browsable(false)]
        public ENTRoleEOList Roles { get; set; }

        #endregion Properties

        #region Overrides

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);

            string html;
            //Check if the menus are there.  In design mode this is null so you don't want to display an error.
            if (MenuItems != null)
            {
                //Get the parent menu item for the current menu item.  The parent will be the one with a null ParentMenuItemId
                var topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);
                var subMenuItems = MenuItems.GetSubMenuItems(topMenuItem);

                html = "<ul class=\"glossymenu\">";

                //Loop around the top level items
                foreach (var mi in MenuItems)
                {
                    //Only show the tabs for the side menu item that the user has access to.                                
                    if (mi.HasAccessToMenu(UserAccount, Roles))
                    {
                        //Check if this is the selected menu tab.                        
                        if (mi.MenuItemName == topMenuItem.MenuItemName)
                        {
                            html += GetActiveTab(mi);
                        }
                        else
                        {
                            html += GetInactiveTab(mi);
                        }
                    }
                }
                html += "</ul>";
            }
            else
            {
                html = "<div>Top Menu Goes Here</div>";
            }
            writer.Write(html);
        }

        #endregion Overrides

        #region Private Methods

        private string GetActiveTab(ENTMenuItemBO subMenu)
        {
            string menu = "<li class=\"current\"><a href=\"" + RootPath + subMenu.Url + "\"><b>" + subMenu.MenuItemName +
                          "</b></a>";
            foreach (var subItem in subMenu.ChildMenuItems)
            {
                menu += "<ul class=\"subMenu\">"
                    //+"<a href=\"" + RootPath + subItem.Url + "\">" 
                    //+ subItem.MenuItemName 
                    //+ "</a>"
                    +"</ul>";
            }
            menu += "</li>";
            return menu;
        }

        private string GetInactiveTab(ENTMenuItemBO subMenu)
        {
            return "<li><a href=\"" + RootPath + subMenu.Url + "\"><b>" + subMenu.MenuItemName + "</b></a></li>";
        }

        #endregion Private Methods
    }
}