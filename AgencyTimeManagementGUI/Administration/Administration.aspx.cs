using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_Administration : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get the Administration menu item from the cache
            ENTMenuItemBO administrationMenuItem = Globals.GetMenuItems(Cache).GetByMenuItemName("Administration");

            //Create a node for each child element of the administrative menu.
            CreateChildNodes(tvMenuDescriptions.Nodes, administrationMenuItem.ChildMenuItems);
        }
    }

    private void CreateChildNodes(TreeNodeCollection treeNodeCollection, ENTMenuItemBOList childMenuItems)
    {
        foreach (var menuItem in childMenuItems)
        {
            var menuNode = new TreeNode(menuItem.MenuItemName + (menuItem.Description == null ? "" : ": " + menuItem.Description));
            menuNode.SelectAction = TreeNodeSelectAction.None;

            if (menuItem.ChildMenuItems.Count > 0)
            {
                CreateChildNodes(menuNode.ChildNodes, menuItem.ChildMenuItems);
            }
            treeNodeCollection.Add(menuNode);
        }
    }

    public override string MenuItemName()
    {
        return "Administration";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Administration"/*, "Auditing" */};
    }
}
