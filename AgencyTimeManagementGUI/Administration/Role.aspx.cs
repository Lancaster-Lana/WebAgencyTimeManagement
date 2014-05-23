using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_Role : BaseEditPage<ENTRoleEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_ROLE = "Role";

    #endregion Constants

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += new PaidTimeOffEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new PaidTimeOffEditPage.ButtonClickedHandler(Master_CancelButton_Click);
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ENTValidationErrors validationErrors = new ENTValidationErrors();

        ENTRoleEO role = (ENTRoleEO)ViewState[VIEW_STATE_KEY_ROLE];
        LoadObjectFromScreen(role);

        if (!role.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            //Reload the globals
            Globals.LoadUsers(Page.Cache);
            Globals.LoadRoles(Page.Cache);
            GoToGridPage();
        }
    }

    protected void btnMoveToSelected_Click(object sender, EventArgs e)
    {
        MoveItems(lstUnselectedUsers, lstSelectedUsers, false);
    }
    protected void btnMoveToUnselected_Click(object sender, EventArgs e)
    {
        MoveItems(lstSelectedUsers, lstUnselectedUsers, false);
    }

    protected void btnMoveAllToSelected_Click(object sender, EventArgs e)
    {
        MoveItems(lstUnselectedUsers, lstSelectedUsers, true);
    }
    protected void btnMoveAllToUnselected_Click(object sender, EventArgs e)
    {
        MoveItems(lstSelectedUsers, lstUnselectedUsers, true);
    }

    #endregion Events

    #region Overrides

    protected override void OnInit(EventArgs e)
    {
        //You need to build the table here so it retains state between postbacks.
        BuildCapabilityTable();
        base.OnInit(e);
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Roles" };
    }

    protected override void LoadObjectFromScreen(ENTRoleEO baseEO)
    {
        baseEO.RoleName = txtRoleName.Text;

        //Load the capabilities
        for (int row = 0; row < tblCapabilities.Rows.Count; row++)
        {
            var tr = tblCapabilities.Rows[row];

            if (tr.Cells.Count > 1)
            {
                //The 2nd cell has the radio list
                var radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];
                //The radio button's id contains the id of the capability.
                int capabilityId = Convert.ToInt32(radioButtons.ID);

                string value = radioButtons.SelectedValue;

                var accessFlag = (ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum)Enum.Parse(typeof(ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum), value);

                //Try to find an existing record for this capability
                var capability = baseEO.RoleCapabilities.GetByCapabilityID(capabilityId);
                if (capability == null)
                {
                    //New record
                    var roleCapability = new ENTRoleCapabilityEO();
                    roleCapability.ENTRoleId = baseEO.ID;
                    roleCapability.Capability.ID = capabilityId;
                    roleCapability.AccessFlag = accessFlag;
                    baseEO.RoleCapabilities.Add(roleCapability);
                }
                else
                {
                    //Update an existing record                    
                    capability.AccessFlag = accessFlag;
                }
            }
        }

        //Load the selected users                
        //Add any users that were not in the role before.
        foreach (ListItem li in lstSelectedUsers.Items)
        {
            //Check if they were already selected.
            if (!baseEO.RoleUserAccounts.IsUserInRole(Convert.ToInt32(li.Value)))
            {
                //If they weren't then add them.
                baseEO.RoleUserAccounts.Add(new ENTRoleUserAccountEO { ENTUserAccountId = Convert.ToInt32(li.Value), ENTRoleId = baseEO.ID });
            }
        }
        //Remove any users that used to be selected but now are not.
        foreach (ListItem li in lstUnselectedUsers.Items)
        {
            //Check if they were in the role before
            if (baseEO.RoleUserAccounts.IsUserInRole(Convert.ToInt32(li.Value)))
            {
                //Mark them for deletion.
                var user = baseEO.RoleUserAccounts.GetByUserAccountId(Convert.ToInt32(li.Value));
                user.DBAction = ENTBaseEO.DBActionEnum.Delete;
            }
        }
    }

    protected override void LoadScreenFromObject(ENTRoleEO baseEO)
    {
        var role = baseEO;
        txtRoleName.Text = role.RoleName;

        //Select the capabilities        
        for (int row = 0; row < tblCapabilities.Rows.Count; row++)
        {
            var tr = tblCapabilities.Rows[row];

            if (tr.Cells.Count > 1)
            {
                //The 2nd cell has the radio list
                var radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];

                //Check if the role has this capability            
                var capability = role.RoleCapabilities.GetByCapabilityID(Convert.ToInt32(radioButtons.ID));

                if (capability != null)
                {
                    //set the access
                    radioButtons.SelectedValue = capability.AccessFlag.ToString();
                }
                else
                {
                    //default to none.
                    radioButtons.SelectedIndex = 0;
                }
            }
        }
        //Select the users
        //Get all the users
        var users = Globals.GetUsers(Page.Cache);
        foreach (var user in users)
        {
            if (role.RoleUserAccounts.IsUserInRole(user.ID))
            {
                lstSelectedUsers.Items.Add(new ListItem(user.DisplayText, user.ID.ToString()));
            }
            else
            {
                lstUnselectedUsers.Items.Add(new ListItem(user.DisplayText, user.ID.ToString()));
            }
        }

        ViewState[VIEW_STATE_KEY_ROLE] = role;
    }

    protected override void LoadControls()
    {

    }

    protected override void GoToGridPage()
    {
        Response.Redirect("Roles.aspx");
    }

    public override string MenuItemName()
    {
        return "Roles";
    }

    public override void CustomReadOnlyLogic(string capabilityName)
    {
        base.CustomReadOnlyLogic(capabilityName);

        //If this is read only then do not show the available choice for the users or the buttons to 
        //swap between list boxes
        lstUnselectedUsers.Visible = false;
        btnMoveAllToSelected.Visible = false;
        btnMoveAllToUnselected.Visible = false;
        btnMoveToSelected.Visible = false;
        btnMoveToUnselected.Visible = false;
        lblUsers.Visible = false;
        lblUserHeader.Visible = false;
    }

    #endregion Overrides

    #region Private Methods

    private void MoveItems(ListBox lstSource, ListBox lstDestination, bool moveAll)
    {
        for (int i = 0; i < lstSource.Items.Count; i++)
        {
            var li = lstSource.Items[i];
            if (moveAll || li.Selected)
            {
                //Add to destination
                li.Selected = false;
                lstDestination.Items.Add(li);
                lstSource.Items.RemoveAt(i);
                i--;
            }
        }
    }

    /// <summary>
    /// Build the capabilities grid in the OnInit event so that it keeps it state between
    /// postbacks.  This method just builds the grid, it does select the options for this role.
    /// That is handled in the LoadScreenFromObject method.
    /// </summary>
    private void BuildCapabilityTable()
    {
        //Get the capabilities
        var capabilities = Globals.GetCapabilities(Page.Cache);
        //Get the menu items
        var menuItems = Globals.GetMenuItems(Page.Cache);
        AddCapabilitiesForMenuItems(menuItems, capabilities, "");
    }

    private void AddCapabilitiesForMenuItems(ENTMenuItemBOList menuItems, ENTCapabilityBOList capabilities, string indentation)
    {
        //Loop around each menu item and create a row for each menu item and capability associated with the menu item
        foreach (var menuItem in menuItems)
        {
            //Get any capabilities with this item
            var capabilitiesForMenuItem = capabilities.GetByMenuItemId(menuItem.ID);

            if (!capabilitiesForMenuItem.Any())
            {
                //Just add the menu item to the row
                var tr = new TableRow();
                var tc = new TableCell();
                var lc = new LiteralControl();
                lc.Text = indentation + menuItem.MenuItemName;
                tc.CssClass = "capabilityHeader";
                tc.Controls.Add(lc);
                tc.ColumnSpan = 3;
                tr.Cells.Add(tc);
                tblCapabilities.Rows.Add(tr);
            }
            else
            {
                //If there is only one capability associated with this menu item then just display the
                //menu item name and the radio buttons
                if (capabilitiesForMenuItem.Count() == 1)
                {
                    AddCapabilityToTable(capabilitiesForMenuItem.ElementAt(0), indentation + menuItem.MenuItemName);
                }
                else
                {
                    //Add a row for each capability
                    foreach (ENTCapabilityBO capability in capabilitiesForMenuItem)
                    {
                        AddCapabilityToTable(capability, indentation + menuItem.MenuItemName + " (" + capability.CapabilityName + ")");
                    }
                }
            }
            if (menuItem.ChildMenuItems.Count > 0)
            {
                AddCapabilitiesForMenuItems(menuItem.ChildMenuItems, capabilities, indentation + "---");
            }
        }
    }

    private void AddCapabilityToTable(ENTCapabilityBO capability, string text)
    {
        var tr = new TableRow();

        //Name
        var tc = new TableCell();
        var lc = new LiteralControl();
        lc.Text = text;
        tc.Controls.Add(lc);
        tr.Cells.Add(tc);

        //access flag
        var tc1 = new TableCell();

        var radioButtons = new RadioButtonList();
        radioButtons.ID = capability.ID.ToString();

        switch (capability.AccessType)
        {
            case ENTCapabilityBO.AccessTypeEnum.ReadOnlyEdit:
                radioButtons.Items.Add(new ListItem("None", ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Read Only", ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly.ToString()));
                radioButtons.Items.Add(new ListItem("Edit", ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
            case ENTCapabilityBO.AccessTypeEnum.ReadOnly:
                radioButtons.Items.Add(new ListItem("None", ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Read Only", ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
            case ENTCapabilityBO.AccessTypeEnum.Edit:
                radioButtons.Items.Add(new ListItem("None", ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Edit", ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
        }
        tc1.Controls.Add(radioButtons);
        tr.Cells.Add(tc1);
        tblCapabilities.Rows.Add(tr);
    }

    #endregion Private Methods
}
