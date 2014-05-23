using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_Holidays : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_HOLIDAY_NAME = 1;
    private const int COL_INDEX_HOLIDAY_DATE = 2;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += Master_AddButton_Click;

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvHolidays.ListClassName = typeof(HolidayEOList).AssemblyQualifiedName;
            cgvHolidays.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvHolidays.AddBoundField("", "Actions", "");

            //Name            
            cgvHolidays.AddBoundField("DisplayText", "Name", "DisplayText");
            cgvHolidays.AddBoundField("", "Date", "HolidayDate");

            cgvHolidays.SortExpressionLast = "HolidayDate";
            cgvHolidays.DataBind();
        }
        else
        {
            string eventTarget = Page.Request.Form["__EVENTTARGET"];
            if (eventTarget.IndexOf("lbtnDelete") > -1)
            {
                //Rebind the grid so the delete event is captured.
                cgvHolidays.DataBind();
            }
        }
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Holiday.aspx" + EncryptQueryString("id=0"));
    }
    
    protected void cgvHolidays_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "Holiday.aspx" + EncryptQueryString("id=" + ((HolidayEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //If the user has read only access then do not show this Delete link.
            if (ReadOnly == false)
            {
                //Add a pipe between the Edit and Delete links
                var lc = new LiteralControl(" | ");
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                //Add the Delete link                                    
                var lbtnDelete = new LinkButton();
                lbtnDelete.ID = "lbtnDelete" + ((HolidayEO)e.Row.DataItem).ID.ToString(CultureInfo.InvariantCulture);
                lbtnDelete.Text = "Delete";
                lbtnDelete.CommandArgument = ((HolidayEO)e.Row.DataItem).ID.ToString(CultureInfo.InvariantCulture);
                lbtnDelete.OnClientClick = "return ConfirmDelete();";
                lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
            }

            //Format the date without the time
            e.Row.Cells[COL_INDEX_HOLIDAY_DATE].Text = ((HolidayEO)e.Row.DataItem).HolidayDate.ToShortDateString();
        }
    }

    void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var holiday = new HolidayEO();
        holiday.DBAction = ENTBaseEO.DBActionEnum.Delete;
        holiday.ID = Convert.ToInt32(e.CommandArgument);
        holiday.Delete(ref validationErrors, CurrentUser.ID);

        Master.ValidationErrors = validationErrors;

        cgvHolidays.DataBind();
    }

    public override string MenuItemName()
    {
        return "Holidays";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Holidays" };
    }
}
