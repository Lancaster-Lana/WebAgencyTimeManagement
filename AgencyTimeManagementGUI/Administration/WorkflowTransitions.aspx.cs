using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_WorkflowTransitions : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_TRANSITION_NAME = 1;
    private const int COL_INDEX_FROM_STATE = 2;
    private const int COL_INDEX_TO_STATE = 3;
    private const int COL_INDEX_REQUIRE_NOTE = 4;
    private const int COL_INDEX_CONDITION_METHOD = 5;
    private const int COL_INDEX_POST_TRANSITION_METHOD = 6;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_AddButton_Click);

        if (!IsPostBack)
        {
            cgvTransitions.PageSize = 50;
            //Tell the control what class to create and what method to call to load the class.
            cgvTransitions.ListClassName = typeof(ENTWFTransitionEOList).AssemblyQualifiedName;
            cgvTransitions.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvTransitions.AddBoundField("", "Actions", "");

            //Name
            cgvTransitions.AddBoundField("DisplayText", "Name", "DisplayText");
            cgvTransitions.AddBoundField("FromStateName", "From State", "FromStateName");
            cgvTransitions.AddBoundField("ToStateName", "To State", "ToStateName");
            cgvTransitions.AddBoundField("PostTransitionMethodName", "Post Transition", "PostTransitionMethodName");

            cgvTransitions.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvTransitions.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkflowTransition.aspx" + EncryptQueryString("id=0"));
    }

    protected void cgvTransitions_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "WorkflowTransition.aspx" + EncryptQueryString("id=" + ((ENTWFTransitionEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //If the user has read only access then do not show this Delete link.
            if (ReadOnly == false)
            {
                //Add a pipe between the Edit and Delete links
                var lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                var lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((ENTWFTransitionEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((ENTWFTransitionEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }
        }
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var wfTransition = new ENTWFTransitionEO();
        wfTransition.DBAction = ENTBaseEO.DBActionEnum.Delete;
        wfTransition.ID = Convert.ToInt32(e.CommandArgument);
        wfTransition.Delete(ref validationErrors, CurrentUser.ID);
        Master.ValidationErrors = validationErrors;
        cgvTransitions.DataBind();
    }

    public override string MenuItemName()
    {
        return "Transitions";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Transitions" };
    }
}
