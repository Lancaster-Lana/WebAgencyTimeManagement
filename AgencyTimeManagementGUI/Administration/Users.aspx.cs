using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;
using System.Web.UI;

public partial class Administration_Users : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_NAME = 1;
    private const int COL_INDEX_WINDOWSNAME = 2;
    private const int COL_INDEX_EMAIL = 3;
    private const int COL_INDEX_ACTIVE = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += Master_AddButton_Click;

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvUsers.ListClassName = typeof(ENTUserAccountEOList).AssemblyQualifiedName;
            cgvUsers.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvUsers.AddBoundField("", "Actions", "");

            //Name
            cgvUsers.AddBoundField("DisplayText", "Name", "DisplayText");

            //Windows Account Name
            cgvUsers.AddBoundField("WindowsAccountName", "Windows Account", "WindowsAccountName");

            //Email
            cgvUsers.AddBoundField("Email", "Email", "Email");

            //Is Active-This will be a checkbox column
            cgvUsers.AddBoundField("", "Active", "IsActive");

            cgvUsers.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvUsers.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx" + EncryptQueryString("id=0"));
    }

    public override string MenuItemName()
    {
        return "Users";
    }

    protected void cgvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "User.aspx" + EncryptQueryString("id=" + ((ENTUserAccountEO)e.Row.DataItem).ID.ToString(CultureInfo.InvariantCulture));
            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //Add checkbox to display the isactive field.
            var chkActive = new CheckBox();
            chkActive.Checked = ((ENTUserAccountEO)e.Row.DataItem).IsActive;
            chkActive.Enabled = false;

            e.Row.Cells[COL_INDEX_ACTIVE].Controls.Add(chkActive);

            //If the user has read only access then do not show this Delete link.
            if (!ReadOnly)
            {
                //Add a pipe between the Edit and Delete links
                var lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                var lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((ENTUserAccountEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((ENTUserAccountEO)e.Row.DataItem).ID.ToString();
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += lbtnDelete_Command;
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }
        }    
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var user = new ENTUserAccountEO(Convert.ToInt32(e.CommandArgument));
        user.DBAction = ENTBaseEO.DBActionEnum.Delete;
        //user.ID = Convert.ToInt32(e.CommandArgument);           
        user.Delete(ref validationErrors, CurrentUser.ID);
        Master.ValidationErrors = validationErrors;
        cgvUsers.DataBind();
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Users" };
    }
}
