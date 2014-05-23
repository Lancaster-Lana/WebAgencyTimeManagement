using System;
using System.Reflection;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_WorkflowTransition : BaseEditPage<ENTWFTransitionEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_TRANSITION = "Transition";

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

        var wfTransition = (ENTWFTransitionEO)ViewState[VIEW_STATE_KEY_TRANSITION];
        LoadObjectFromScreen(wfTransition);

        if (!wfTransition.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }


    protected override void LoadObjectFromScreen(ENTWFTransitionEO baseEO)
    {
        var wfTransition = (ENTWFTransitionEO)baseEO;

        wfTransition.TransitionName = txtTransitionName.Text;
        wfTransition.ENTWorkflowId = Convert.ToInt32(ddlWorkflow.SelectedValue);

        if (ddlFromState.SelectedValue == "0")
        {
            wfTransition.FromENTWFStateId = null;
        }
        else
        {
            wfTransition.FromENTWFStateId = Convert.ToInt32(ddlFromState.SelectedValue);
        }        
        
        wfTransition.ToENTWFStateId = Convert.ToInt32(ddlToState.SelectedValue);
                
        if (ddlPostTransitionMethodName.SelectedItem.Text == "")
        {
            wfTransition.PostTransitionMethodName = null;
        }
        else
        {
            wfTransition.PostTransitionMethodName = ddlPostTransitionMethodName.SelectedItem.Text;
        }        
    }

    protected override void LoadScreenFromObject(ENTWFTransitionEO baseEO)
    {
        var transition = baseEO;

        txtTransitionName.Text = transition.TransitionName;

        if (transition.ENTWorkflowId != 0)
        {
            ddlWorkflow.Items.FindByValue(transition.ENTWorkflowId.ToString()).Selected = true;

            LoadStateDropDownLists(transition.ENTWorkflowId);

            if ((transition.FromENTWFStateId != null) && (transition.FromENTWFStateId != 0))
            {
                ddlFromState.Items.FindByValue(transition.FromENTWFStateId.ToString()).Selected = true;
            }

            if (transition.ToENTWFStateId != 0)
            {
                ddlToState.Items.FindByValue(transition.ToENTWFStateId.ToString()).Selected = true;
            }

            LoadMethodDropDownList(transition.ENTWorkflowId);
                        
            if (transition.PostTransitionMethodName != null)
            {
                ddlPostTransitionMethodName.Items.FindByText(transition.PostTransitionMethodName).Selected = true;
            }
        }
        else
        {
            //Fill the lists with the workflow selected in the drop down list.
            if (ddlWorkflow.Items.Count > 0)
            {
                LoadStateDropDownLists(Convert.ToInt32(ddlWorkflow.SelectedValue));

                LoadMethodDropDownList(Convert.ToInt32(ddlWorkflow.SelectedValue));
            }
        }

        ViewState[VIEW_STATE_KEY_TRANSITION] = transition;
    }

    protected override void LoadControls()
    {
        //Load workflows
        var workflows = new ENTWorkflowEOList();
        workflows.Load();
        ddlWorkflow.DataSource = workflows;
        ddlWorkflow.DataTextField = "DisplayText";
        ddlWorkflow.DataValueField = "ID";
        ddlWorkflow.DataBind();                
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("WorkflowTransitions.aspx");
    }

    public override string MenuItemName()
    {
        return "Transitions";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Transitions" };
    }

    private void LoadStateDropDownLists(int entWorkflowId)
    {
        //Load the from state and to state based on the workflow.
        var states = new ENTWFStateEOList();
        states.Load(entWorkflowId);

        ddlFromState.DataSource = states;
        ddlFromState.DataTextField = "DisplayText";
        ddlFromState.DataValueField = "ID";
        ddlFromState.DataBind();
        ddlFromState.Items.Insert(0, new ListItem("", "0"));

        ddlToState.DataSource = states;
        ddlToState.DataTextField = "DisplayText";
        ddlToState.DataValueField = "ID";
        ddlToState.DataBind();
    }

    private void LoadMethodDropDownList(int entWorkflowId)
    {
        var workflow = new ENTWorkflowEO();
        if (workflow.Load(entWorkflowId))
        {                        
            //Create an instance of the type.            
            var methods = Type.GetType(workflow.ENTWorkflowObjectName).GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public );

            //Load the methods for this workflow that return a boolean value into the conditions drop down list.
            foreach (var mi in methods)
            {                               
                //Only methods that take a parameter of a data context can be used.
                var parameters = mi.GetParameters();
                if (parameters.Length == 1)
                {
                    if (parameters[0].ParameterType == typeof(Agency.PaidTimeOffDAL.HRPaidTimeOffDataContext))
                    {
                        ddlPostTransitionMethodName.Items.Add(mi.Name);
                    }
                }
            }
                                    
            ddlPostTransitionMethodName.Items.Insert(0, "");                        
        }
        else
        {
            throw new Exception("The workflow can not be found in the database.");
        }
    }
    protected void ddlWorkflow_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStateDropDownLists(Convert.ToInt32(ddlWorkflow.SelectedValue));
        LoadMethodDropDownList(Convert.ToInt32(ddlWorkflow.SelectedValue));
    }
}
