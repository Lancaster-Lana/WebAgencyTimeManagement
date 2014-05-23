using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_Roles : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_ROLE_NAME = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += Master_AddButton_Click;

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvRoles.ListClassName = typeof(ENTRoleEOList).AssemblyQualifiedName;
            cgvRoles.LoadMethodName = "Load";
            //Action column-Contains the Edit link
            cgvRoles.AddBoundField("", "Actions", "");
            cgvRoles.AddBoundField("DisplayText", "Name", "DisplayText");
            cgvRoles.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"];
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvRoles.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Role.aspx" + EncryptQueryString("id=0"));
    }

    protected void cgvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "Role.aspx" + EncryptQueryString("id=" + ((ENTRoleEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //If the user has read only access then do not show this Delete link.
            if (!ReadOnly)
            {
                //Add a pipe between the Edit and Delete links
                var lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                var lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((ENTRoleEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((ENTRoleEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += lbtnDelete_Command;
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }
        }
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var role = new ENTRoleEO();
        role.DBAction = ENTBaseEO.DBActionEnum.Delete;
        role.ID = Convert.ToInt32(e.CommandArgument);
        role.Delete(ref validationErrors, CurrentUser.ID);
        Master.ValidationErrors = validationErrors;
        cgvRoles.DataBind();
    }

    public override string MenuItemName()
    {
        return "Roles";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Roles" };
    }
}
