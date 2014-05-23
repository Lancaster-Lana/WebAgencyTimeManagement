using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_WorkflowOwners : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_WORKFLOW_NAME = 1;
    private const int COL_INDEX_WORKFLOW_DESCRIPTION = 2;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_AddButton_Click);

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvWorkflow.ListClassName = typeof(ENTWFOwnerGroupEOList).AssemblyQualifiedName;
            cgvWorkflow.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvWorkflow.AddBoundField("", "Actions", "");

            var A = new ENTWFOwnerGroupEO();
            
            //Name
            cgvWorkflow.AddBoundField("OwnerGroupName", "Name", "OwnerGroupName");
            cgvWorkflow.AddBoundField("Description", "Description", "Description");

            cgvWorkflow.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvWorkflow.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkflowOwner.aspx" + EncryptQueryString("id=0"));
    }

    public override string MenuItemName()
    {
        return "Owners";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Owners" };
    }

    protected void cgvWorkflow_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "WorkflowOwner.aspx" + EncryptQueryString("id=" + ((ENTWFOwnerGroupEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //If the user has read only access then do not show this Delete link.
            if (ReadOnly == false)
            {
                //Add a pipe between the Edit and Delete links
                var lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                var lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((ENTWFOwnerGroupEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((ENTWFOwnerGroupEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }
        }
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        ENTValidationErrors validationErrors = new ENTValidationErrors();
        ENTWFOwnerGroupEO workflowOwnerGroup = new ENTWFOwnerGroupEO();
        workflowOwnerGroup.DBAction = ENTBaseEO.DBActionEnum.Delete;
        workflowOwnerGroup.ID = Convert.ToInt32(e.CommandArgument);
        workflowOwnerGroup.Delete(ref validationErrors, CurrentUser.ID);

        Master.ValidationErrors = validationErrors;

        cgvWorkflow.DataBind();
    }

}
