using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_WorkflowStates : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_STATE_NAME = 1;
    private const int COL_INDEX_DESCRIPTION = 2;    

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += Master_AddButton_Click;

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvWorkflowStates.ListClassName = typeof(ENTWFStateEOList).AssemblyQualifiedName;
            cgvWorkflowStates.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvWorkflowStates.AddBoundField("", "Actions", "");

            //Name
            cgvWorkflowStates.AddBoundField("DisplayText", "State Name", "DisplayText");
            cgvWorkflowStates.AddBoundField("Description", "Description", "Description");

            cgvWorkflowStates.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvWorkflowStates.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkflowState.aspx" + EncryptQueryString("id=0"));
    }

    protected void cgvWorkflowStates_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "WorkflowState.aspx" + EncryptQueryString("id=" + ((ENTWFStateEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //If the user has read only access then do not show this Delete link.
            if (ReadOnly == false)
            {
                //Add a pipe between the Edit and Delete links
                var lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                var lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((ENTWFStateEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((ENTWFStateEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }
        }
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var state = new ENTWFStateEO();
        state.DBAction = ENTBaseEO.DBActionEnum.Delete;
        state.ID = Convert.ToInt32(e.CommandArgument);
        state.Delete(ref validationErrors, CurrentUser.ID);

        Master.ValidationErrors = validationErrors;

        cgvWorkflowStates.DataBind();
    }

    public override string MenuItemName()
    {
        return "States";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "States" };
    }
}
