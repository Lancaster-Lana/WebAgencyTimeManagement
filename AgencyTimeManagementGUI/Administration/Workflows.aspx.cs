using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_Workflows : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_WORKFLOW_NAME = 1;
    private const int COL_INDEX_OBJECT_NAME = 2;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_AddButton_Click);

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvWorkflows.ListClassName = typeof(ENTWorkflowEOList).AssemblyQualifiedName;
            cgvWorkflows.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvWorkflows.AddBoundField("", "Actions", "");

            //Name
            cgvWorkflows.AddBoundField("DisplayText", "Name", "DisplayText");
            cgvWorkflows.AddBoundField("ENTWorkflowObjectName", "Class Name", "ENTWorkflowObjectName");

            cgvWorkflows.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvWorkflows.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Workflow.aspx" + EncryptQueryString("id=0"));
    }

    protected void cgvWorkflows_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add the edit link to the action column.
            var editLink = new HyperLink();
            if (ReadOnly)
            {
                editLink.Text = "View";
            }
            else
            {
                editLink.Text = "Edit";
            }
            editLink.NavigateUrl = "Workflow.aspx" + EncryptQueryString("id=" + ((ENTWorkflowEO)e.Row.DataItem).ID.ToString(CultureInfo.InvariantCulture));

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //If the user has read only access then do not show this Delete link.
            if (ReadOnly == false)
            {
                //Add a pipe between the Edit and Delete links
                var lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                var lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((ENTWorkflowEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((ENTWorkflowEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }
        }
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var workflow = new ENTWorkflowEO();
        workflow.DBAction = ENTBaseEO.DBActionEnum.Delete;
        workflow.ID = Convert.ToInt32(e.CommandArgument);
        workflow.Delete(ref validationErrors, CurrentUser.ID);

        Master.ValidationErrors = validationErrors;

        cgvWorkflows.DataBind();
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
