using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Reflection;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_WorkflowState : BaseEditPage<ENTWFStateEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_STATE = "WFState";

    #endregion Constants

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += Master_SaveButton_Click;
        Master.CancelButton_Click += Master_CancelButton_Click;

        if (ddlWorkflow.SelectedValue != "")
        {
            var entWFState = (ENTWFStateEO)ViewState[VIEW_STATE_KEY_STATE];
            LoadPropertiesTable(Convert.ToInt32(ddlWorkflow.SelectedValue), entWFState);
        }
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        var validationErrors = new ENTValidationErrors();

        var state = (ENTWFStateEO)ViewState[VIEW_STATE_KEY_STATE];
        LoadObjectFromScreen(state);

        if (!state.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    protected override void LoadObjectFromScreen(ENTWFStateEO baseEO)
    {
        baseEO.StateName = txtStateName.Text;
        baseEO.ENTWorkflowId = Convert.ToInt32(ddlWorkflow.SelectedValue);
        baseEO.Description = txtDescription.Text;
        baseEO.ENTWFOwnerGroupId = Convert.ToInt32(ddlWFOwnerGroup.SelectedValue);
        baseEO.IsOwnerSubmitter = chkIsSubmitter.Checked;
        baseEO.ENTWFStateProperties.Clear();

        //Load the properties
        if (tblProperties.Rows.Count > 1)
        {
            //skip the header
            for (int row = 1; row < tblProperties.Rows.Count; row++)
            {
                var tr = tblProperties.Rows[row];

                bool readOnly = ((CheckBox)tr.Cells[1].Controls[0]).Checked;

                bool required = ((CheckBox)tr.Cells[2].Controls[0]).Checked;

                string propertyName = tr.Cells[0].Text;

                baseEO.ENTWFStateProperties.Add(new ENTWFStatePropertyEO
                {
                    ENTWFStateId = baseEO.ID,
                    PropertyName = propertyName,
                    ReadOnly = readOnly,
                    Required = required
                });
            }
        }
    }

    protected override void LoadScreenFromObject(ENTWFStateEO baseEO)
    {
        var state = baseEO;

        txtStateName.Text = state.StateName;
        txtDescription.Text = state.Description;
        chkIsSubmitter.Checked = state.IsOwnerSubmitter;

        //Check if a workflow is selected.
        if (state.ENTWorkflowId != 0)
        {
            ddlWorkflow.Items.FindByValue(state.ENTWorkflowId.ToString()).Selected = true;

            //Load the properties table for this workflow.
            LoadPropertiesTable(state.ENTWorkflowId, baseEO);

            //Load the owner groups associated with this workflow.
            LoadOwnerGroupList(state.ENTWorkflowId);

            //Check if there is a owner group selected.          
            if (state.ENTWFOwnerGroupId != null)
            {
                ddlWFOwnerGroup.Items.FindByValue(state.ENTWFOwnerGroupId.ToString()).Selected = true;
            }
        }

        SetControlStateByCheckbox();
        ViewState[VIEW_STATE_KEY_STATE] = baseEO;
    }

    private void LoadPropertiesTable(int entWorkflowId, ENTWFStateEO entWFStateEO)
    {
        if (entWorkflowId != 0)
        {
            var workflow = new ENTWorkflowEO();
            if (workflow.Load(entWorkflowId))
            {
                tblProperties.Rows.Clear();

                //Add header
                var trHeader = new TableRow();
                var tc1 = new TableCell();
                tc1.Text = "Property";
                trHeader.Cells.Add(tc1);

                var tc2 = new TableCell();
                tc2.Text = "Read Only";
                trHeader.Cells.Add(tc2);

                var tc3 = new TableCell();
                tc3.Text = "Required";
                trHeader.Cells.Add(tc3);

                tblProperties.Rows.Add(trHeader);

                //Create an instance of the type.            
                var type = Type.GetType(workflow.ENTWorkflowObjectName);
                if (type != null)
                {
                    var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

                    //Populate the table with all the fields.
                    foreach (var prop in properties)
                    {
                        //Only show properties the have a public set property.
                        var methodInfo = prop.GetAccessors();

                        //Get the set method
                        var set =
                            from m in methodInfo
                            where m.Name.StartsWith("set")
                            select m;

                        if (set.Any())
                        {
                            if (set.Single().IsPublic)
                            {
                                var entWFStateProperty = new ENTWFStatePropertyEO();

                                if (entWFStateEO.ENTWorkflowId == Convert.ToInt32(ddlWorkflow.SelectedValue))
                                {
                                    //Try to find this property in the ENTWFStateObject
                                    entWFStateProperty = entWFStateEO.ENTWFStateProperties.GetByPropertyName(prop.Name);

                                    if (entWFStateProperty == null) entWFStateProperty = new ENTWFStatePropertyEO();
                                }

                                var tr = new TableRow();

                                //Name of property
                                var tcName = new TableCell();
                                tcName.Text = prop.Name;
                                tr.Cells.Add(tcName);

                                //Read Only checkbox
                                var tcReadOnly = new TableCell();
                                var chkReadOnly = new CheckBox();
                                chkReadOnly.Enabled = !ReadOnly;
                                chkReadOnly.Checked = entWFStateProperty.ReadOnly;
                                tcReadOnly.Controls.Add(chkReadOnly);
                                tr.Cells.Add(tcReadOnly);

                                //Required checkbox
                                var tcRequired = new TableCell();
                                var chkRequired = new CheckBox();
                                chkRequired.Enabled = !ReadOnly;
                                chkRequired.Checked = entWFStateProperty.Required;
                                tcRequired.Controls.Add(chkRequired);
                                tr.Cells.Add(tcRequired);
                                tblProperties.Rows.Add(tr);
                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception("The workflow can not be found in the database.");
            }
        }
    }

    private void LoadOwnerGroupList(int workflowId)
    {
        var wfOwnerGroups = new ENTWFOwnerGroupEOList();
        wfOwnerGroups.Load(workflowId);

        ddlWFOwnerGroup.DataSource = wfOwnerGroups;
        ddlWFOwnerGroup.DataTextField = "DisplayText";
        ddlWFOwnerGroup.DataValueField = "ID";
        ddlWFOwnerGroup.DataBind();

        ddlWFOwnerGroup.Items.Insert(0, new ListItem("", "0"));
    }

    protected override void LoadControls()
    {
        //workflows
        var workflows = new ENTWorkflowEOList();
        workflows.Load();

        ddlWorkflow.DataSource = workflows;
        ddlWorkflow.DataTextField = "DisplayText";
        ddlWorkflow.DataValueField = "ID";
        ddlWorkflow.DataBind();

        ddlWorkflow.Items.Insert(0, new ListItem("", "0"));

        ddlWFOwnerGroup.Items.Insert(0, new ListItem("", "0"));
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("WorkflowStates.aspx");
    }

    public override string MenuItemName()
    {
        return "States";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "States" };
    }

    protected void ddlWorkflow_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOwnerGroupList(Convert.ToInt32(ddlWorkflow.SelectedValue));
        var entWFState = (ENTWFStateEO)ViewState[VIEW_STATE_KEY_STATE];
        LoadPropertiesTable(Convert.ToInt32(ddlWorkflow.SelectedValue), entWFState);
    }

    protected void chkIsSubmitter_CheckedChanged(object sender, EventArgs e)
    {
        SetControlStateByCheckbox();
    }

    private void SetControlStateByCheckbox()
    {
        if (chkIsSubmitter.Checked)
        {
            ddlWFOwnerGroup.SelectedIndex = 0;
            ddlWFOwnerGroup.Enabled = false;
        }
        else
        {
            ddlWFOwnerGroup.Enabled = true;
        }
    }
}
