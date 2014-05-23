using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_NotificationTemplates : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_WORKFLOW_NAME = 1;
    private const int COL_INDEX_OBJECT_NAME = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.btnAddNewRef().Visible = false;

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvNotificationTemplates.ListClassName = typeof(ENTNotificationEOList).AssemblyQualifiedName;
            cgvNotificationTemplates.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvNotificationTemplates.AddBoundField("", "Actions", "");

            //Name
            cgvNotificationTemplates.AddBoundField("Description", "Description", "Description");
            cgvNotificationTemplates.AddBoundField("FromEmailAddress", "From Email", "FromEmailAddress");
            cgvNotificationTemplates.AddBoundField("Subject", "Subject", "Subject");

            cgvNotificationTemplates.DataBind();
        }        
    }
    protected void cgvNotificationTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "NotificationTemplate.aspx" + EncryptQueryString("id=" + ((ENTNotificationEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);                        
        }
    }

    public override string MenuItemName()
    {
        return "Notification Templates";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Notification Templates" };
    }
}
