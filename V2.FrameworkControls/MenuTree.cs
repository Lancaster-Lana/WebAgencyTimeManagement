using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.FrameworkControls
{
    [ToolboxData("<{0}:MenuTree runat=server></{0}:MenuTree>")]
    public class MenuTree : WebControl
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

            string html = string.Empty;

            //Check if the menus are there.  In design mode this is null so you don't want to display an error.
            if (MenuItems == null)
            {
                html = "<div>Tree Goes Here</div>";
            }

            writer.Write(html);
        }

        protected override void CreateChildControls()
        {
            var menuControl = new TreeView();
            menuControl.SelectedNodeStyle.CssClass = "selectedMenuItem";
            menuControl.ID = "tvSideMenu";
            menuControl.NodeWrap = true;
            if (UserAccount != null)
            {
                //Find the top most menu item.  This is the tab at the top.
                var topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);
                CreateChildMenu(menuControl.Nodes, topMenuItem.ChildMenuItems);
            }
            Controls.Add(menuControl);
            base.CreateChildControls();
        }

        #endregion Overrides

        #region Private Methods

        private void CreateChildMenu(TreeNodeCollection nodes, ENTMenuItemBOList menuItems)
        {
            foreach (var mi in menuItems)
            {
                //Check if the user has access to the menu or any children.
                if (mi.HasAccessToMenu(UserAccount, Roles))
                {
                    //Create an instance of the menu
                    var menuNode = new TreeNode(mi.MenuItemName, mi.ID.ToString(), "",
                            (string.IsNullOrEmpty(mi.Url) ? "" : RootPath + mi.Url), "");

                    if (string.IsNullOrEmpty(mi.Url))
                    {
                        menuNode.SelectAction = TreeNodeSelectAction.None;
                    }

                    //Check if this is the menu item that should be selected.
                    if (mi.MenuItemName == CurrentMenuItemName)
                    {
                        menuNode.Selected = true;
                    }

                    //Check if this has children.
                    if (mi.ChildMenuItems.Count > 0)
                    {
                        //Create items for the children.
                        CreateChildMenu(menuNode.ChildNodes, mi.ChildMenuItems);
                    }
                    nodes.Add(menuNode);
                }
            }
        }

        #endregion Private Methods
    }
}
