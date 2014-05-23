using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;
using Agency.PaidTimeOffBLL;
using System.Drawing;

public partial class Administration_Holiday : BaseEditPage<HolidayEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_HOLIDAY = "Holiday";

    #endregion Constants

    #region Members

    private HolidayEOList holidays = new HolidayEOList();

    #endregion Members

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += Master_SaveButton_Click;
        Master.CancelButton_Click += Master_CancelButton_Click;

        holidays.Load();

        calHolidayDate.SelectedDayStyle.BackColor = Color.Green;
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        var validationErrors = new ENTValidationErrors();

        var holiday = (HolidayEO)ViewState[VIEW_STATE_KEY_HOLIDAY];
        LoadObjectFromScreen(holiday);

        if (!holiday.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    public override string MenuItemName()
    {
        return "Holidays";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Holidays" };
    }

    protected override void LoadObjectFromScreen(HolidayEO baseEO)
    {
        baseEO.HolidayName = txtHolidayName.Text;
        baseEO.HolidayDate = calHolidayDate.SelectedDate;
    }

    protected override void LoadScreenFromObject(HolidayEO baseEO)
    {
        var holiday = (HolidayEO)baseEO;

        txtHolidayName.Text = holiday.HolidayName;
        calHolidayDate.SelectedDate = holiday.HolidayDate;
        calHolidayDate.VisibleDate = holiday.HolidayDate;

        SetSelectedDateLabel();

        ViewState[VIEW_STATE_KEY_HOLIDAY] = holiday;
    }

    protected override void LoadControls()
    {
        
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("Holidays.aspx");
    }
    protected void calHolidayDate_DayRender(object sender, DayRenderEventArgs e)
    {        
        var holiday = holidays.GetHoliday(e.Day.Date);

        var currentHoliday = (HolidayEO)ViewState[VIEW_STATE_KEY_HOLIDAY];

        if ((holiday != null) && (holiday.ID != currentHoliday.ID))
        {
            e.Cell.Text = "H";
            e.Cell.BackColor = Color.Red;
            e.Cell.ToolTip = holiday.HolidayName;
        }
    }

    private void SetSelectedDateLabel()
    {
        lblSelectedDate.Text = calHolidayDate.SelectedDate.ToString("MMM dd, yyyy");
    }
    protected void calHolidayDate_SelectionChanged(object sender, EventArgs e)
    {
        SetSelectedDateLabel();
    }
}
