using System;
using Agency.PaidTimeOffBLL.Framework;
using System.Reflection;

public partial class Administration_Workflow : BaseEditPage<ENTWorkflowEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_WORKFLOW = "Workflow";

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
        var workflow = (ENTWorkflowEO)ViewState[VIEW_STATE_KEY_WORKFLOW];
        LoadObjectFromScreen(workflow);

        if (!workflow.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    protected override void LoadObjectFromScreen(ENTWorkflowEO baseEO)
    {
        var workflow = (ENTWorkflowEO)baseEO;
        workflow.WorkflowName = txtWorkflowName.Text;
        workflow.ENTWorkflowObjectName = ddlObjectName.Text;
    }

    protected override void LoadScreenFromObject(ENTWorkflowEO baseEO)
    {
        var workflow = baseEO;
        txtWorkflowName.Text = workflow.WorkflowName;
        if (workflow.ENTWorkflowObjectName != null)
        {
            ddlObjectName.Items.FindByText(workflow.ENTWorkflowObjectName).Selected = true;
        }
        ViewState[VIEW_STATE_KEY_WORKFLOW] = workflow;
    }

    protected override void LoadControls()
    {
        //Load the drop down list with the objects in this BLL.
        var assembly = Assembly.Load("Agency.PaidTimeOffBLL");
                
        Type[] types = assembly.GetTypes();

        foreach (Type t in types)
        {
            if ((t.IsClass) && (t.BaseType == typeof(ENTBaseWorkflowEO)))
            {
                ddlObjectName.Items.Add(t.AssemblyQualifiedName);
            }
        }        
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("Workflows.aspx");
    }

    public override string MenuItemName()
    {
        return "Workflows";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Workflows" };
    }
}
