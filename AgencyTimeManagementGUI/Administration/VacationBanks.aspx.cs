using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL;

public partial class Administration_VacationBanks : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_USER_NAME = 1;
    private const int COL_INDEX_YEAR = 2;
    private const int COL_INDEX_PERSONAL_DAYS = 3;
    private const int COL_INDEX_VACATION_DAYS = 4;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += Master_AddButton_Click;

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvVacationBanks.ListClassName = typeof(PTOVacationBankEOList).AssemblyQualifiedName;
            cgvVacationBanks.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvVacationBanks.AddBoundField("", "Actions", "");

            //Name
            cgvVacationBanks.AddBoundField("UserName", "Employee Name", "UserName");
            cgvVacationBanks.AddBoundField("VacationYear", "Year", "VacationYear");
            cgvVacationBanks.AddBoundField("PersonalDays", "Personal Days", "Personal Days");
            cgvVacationBanks.AddBoundField("VacationDays", "Vacation Days", "VacationDays");

            cgvVacationBanks.DataBind();
        }        
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("VacationBank.aspx" + EncryptQueryString("id=0"));
    }

    public override string MenuItemName()
    {
        return "Vacation Banks";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Vacation Banks" };
    }

    protected void cgvVacationBanks_RowDataBound(object sender, GridViewRowEventArgs e)
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
            editLink.NavigateUrl = "VacationBank.aspx" + EncryptQueryString("id=" + ((PTOVacationBankEO)e.Row.DataItem).ID.ToString(CultureInfo.InvariantCulture));

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);                        
        }
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        Response.Redirect("CopyVacationBank.aspx");
    }
}
