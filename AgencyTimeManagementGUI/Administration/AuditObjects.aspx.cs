using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_AuditObjects : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_OBJECT_NAME = 1;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_AddButton_Click);

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvAuditObjects.ListClassName = typeof(ENTAuditObjectEOList).AssemblyQualifiedName;
            cgvAuditObjects.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvAuditObjects.AddBoundField("", "Actions", "");

            //Name            
            cgvAuditObjects.AddBoundField("DisplayText", "Name", "DisplayText");
                        
            cgvAuditObjects.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvAuditObjects.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("AuditObject.aspx" + EncryptQueryString("id=0"));
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        ENTValidationErrors validationErrors = new ENTValidationErrors();
        ENTAuditObjectEO auditObject = new ENTAuditObjectEO();
        auditObject.DBAction = ENTBaseEO.DBActionEnum.Delete;
        auditObject.ID = Convert.ToInt32(e.CommandArgument);
        auditObject.Delete(ref validationErrors, CurrentUser.ID);

        Master.ValidationErrors = validationErrors;

        cgvAuditObjects.DataBind();
    }

    public override string MenuItemName()
    {
        return "Auditing";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Auditing" };
    }

    protected void cgvAuditObjects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add the edit link to the action column.
            HyperLink editLink = new HyperLink();
            if (ReadOnly)
            {
                editLink.Text = "View";
            }
            else
            {
                editLink.Text = "Edit";
            }
            editLink.NavigateUrl = "AuditObject.aspx" + EncryptQueryString("id=" + ((ENTAuditObjectEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //If the user has read only access then do not show this Delete link.
            if (ReadOnly == false)
            {
                //Add a pipe between the Edit and Delete links
                LiteralControl lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                LinkButton lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((ENTAuditObjectEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((ENTAuditObjectEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }                        
        }
    }
}
