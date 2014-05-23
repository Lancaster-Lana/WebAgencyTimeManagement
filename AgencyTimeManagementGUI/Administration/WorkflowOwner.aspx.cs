using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_WorkflowOwner : BaseEditPage<ENTWFOwnerGroupEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_WFGROUP = "WFGroup";

    #endregion Constants

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += Master_SaveButton_Click;
        Master.CancelButton_Click += Master_CancelButton_Click;
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        var validationErrors = new ENTValidationErrors();

        var wfOwnerGroup = (ENTWFOwnerGroupEO)ViewState[VIEW_STATE_KEY_WFGROUP];
        LoadObjectFromScreen(wfOwnerGroup);

        if (!wfOwnerGroup.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    protected void btnMoveToSelected_Click(object sender, EventArgs e)
    {
        AddToDefaultDropDown(false);
        MoveItems(lstUnselectedUsers, lstSelectedUsers, false);        
    }

    private void AddToDefaultDropDown(bool all)
    {
        foreach (ListItem li in lstUnselectedUsers.Items)
        {
            if ((li.Selected) || (all))
            {
                ddlDefaultOwner.Items.Add(li);
            }
        }
    }

    protected void btnMoveToUnselected_Click(object sender, EventArgs e)
    {
        RemoveFromDropDown(false);
        MoveItems(lstSelectedUsers, lstUnselectedUsers, false);
    }

    private void RemoveFromDropDown(bool all)
    {
        foreach (ListItem li in lstSelectedUsers.Items)
        {
            if ((li.Selected) || (all))
            {
                ddlDefaultOwner.Items.Remove(ddlDefaultOwner.Items.FindByValue(li.Value));
            }
        }
    }

    protected void btnMoveAllToSelected_Click(object sender, EventArgs e)
    {
        AddToDefaultDropDown(true);
        MoveItems(lstUnselectedUsers, lstSelectedUsers, true);
    }
    protected void btnMoveAllToUnselected_Click(object sender, EventArgs e)
    {
        RemoveFromDropDown(true);
        MoveItems(lstSelectedUsers, lstUnselectedUsers, true);
    }

    private void MoveItems(ListBox lstSource, ListBox lstDestination, bool moveAll)
    {
        for (int i = 0; i < lstSource.Items.Count; i++)
        {
            ListItem li = lstSource.Items[i];
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

    protected override void LoadObjectFromScreen(ENTWFOwnerGroupEO baseEO)
    {
        baseEO.OwnerGroupName = txtOwnerGroupName.Text;
        baseEO.ENTWorkflowId = Convert.ToInt32(ddlWorkflow.SelectedValue);
        baseEO.Description = txtDescription.Text;
        baseEO.DefaultENTUserAccountId = Convert.ToInt32(ddlDefaultOwner.SelectedValue);
        baseEO.IsDefaultSameAsLast = chkSameAsLast.Checked;

        //Load the selected users                
        //Add any users that were not in the role before.
        foreach (ListItem li in lstSelectedUsers.Items)
        {
            //Check if they were already selected.
            if (baseEO.UserAccounts.IsUserInGroup(Convert.ToInt32(li.Value)) == false)
            {
                //If they weren't then add them.
                baseEO.UserAccounts.Add(new ENTWFOwnerGroupUserAccountEO { ENTUserAccountId = Convert.ToInt32(li.Value), ENTWFOwnerGroupId = baseEO.ID });
            }
        }
        //Remove any users that used to be selected but now are not.
        foreach (ListItem li in lstUnselectedUsers.Items)
        {
            //Check if they were in the role before
            if (baseEO.UserAccounts.IsUserInGroup(Convert.ToInt32(li.Value)))
            {
                //Mark them for deletion.
                var user = baseEO.UserAccounts.GetByUserAccountId(Convert.ToInt32(li.Value));
                user.DBAction = ENTBaseEO.DBActionEnum.Delete;
            }
        }
    }

    protected override void LoadScreenFromObject(ENTWFOwnerGroupEO baseEO)
    {
        var wfOwnerGroup = (ENTWFOwnerGroupEO)baseEO;

        //Load the default users 
        ddlDefaultOwner.DataSource = wfOwnerGroup.UserAccounts;
        ddlDefaultOwner.DataTextField = "UserName";
        ddlDefaultOwner.DataValueField = "ENTUserAccountId";
        ddlDefaultOwner.DataBind();
        ddlDefaultOwner.Items.Insert(0,new ListItem("", "0"));

        txtOwnerGroupName.Text = wfOwnerGroup.OwnerGroupName;
        txtDescription.Text = wfOwnerGroup.Description;
        if (wfOwnerGroup.DefaultENTUserAccountId != null)
        {
            ddlDefaultOwner.Items.FindByValue(wfOwnerGroup.DefaultENTUserAccountId.ToString()).Selected = true;
        }
        else
        {
            ddlDefaultOwner.SelectedIndex = 0;
        }

        chkSameAsLast.Checked = wfOwnerGroup.IsDefaultSameAsLast;
        SetScreenStateForCheckbox(chkSameAsLast.Checked);

        if (wfOwnerGroup.ENTWorkflowId != 0)
        {
            ddlWorkflow.Items.FindByValue(wfOwnerGroup.ENTWorkflowId.ToString()).Selected = true;
        }

        //Select the users
        //Get all the users
        var users = Globals.GetUsers(Page.Cache);

        foreach (var user in users)
        {
            if (wfOwnerGroup.UserAccounts.IsUserInGroup(user.ID))
            {
                lstSelectedUsers.Items.Add(new ListItem(user.DisplayText, user.ID.ToString()));
            }
            else
            {
                lstUnselectedUsers.Items.Add(new ListItem(user.DisplayText, user.ID.ToString()));
            }
        }        

        ViewState[VIEW_STATE_KEY_WFGROUP] = wfOwnerGroup;
    }

    protected override void LoadControls()
    {
        var workflows = new ENTWorkflowEOList();
        workflows.Load();
        ddlWorkflow.DataSource = workflows;
        ddlWorkflow.DataTextField = "DisplayText";
        ddlWorkflow.DataValueField = "ID";
        ddlWorkflow.DataBind();        
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("WorkflowOwners.aspx");
    }

    public override string MenuItemName()
    {
        return "Owners";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Owners" };
    }

    protected void chkSameAsLast_CheckedChanged(object sender, EventArgs e)
    {
        SetScreenStateForCheckbox(chkSameAsLast.Checked);
    }

    private void SetScreenStateForCheckbox(bool isChecked)
    {
        if (!ReadOnly)
        {
            if (isChecked)
            {
                ddlDefaultOwner.SelectedIndex = 0;
                ddlDefaultOwner.Enabled = false;
            }
            else
            {
                ddlDefaultOwner.Enabled = true;
            }
        }
    }
}
