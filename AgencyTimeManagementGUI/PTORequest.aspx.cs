using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Drawing;
using System.Collections.Specialized;
using Agency.PaidTimeOffBLL;
using Agency.PaidTimeOffBLL.Framework;

public partial class PTORequest : BaseEditPage<PTORequestEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_PTOREQUEST = "PTORequest";

    #endregion Constants

    #region Members

    private HolidayEOList _holidays = new HolidayEOList();
    private PTORequestEOList _priorDaysOff = new PTORequestEOList();

    #endregion Members

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        wfcPTORequest.WorkflowObjectName = typeof(PTORequestEO).AssemblyQualifiedName;                
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        Master.SaveButton_Click += new PaidTimeOffEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new PaidTimeOffEditPage.ButtonClickedHandler(Master_CancelButton_Click);

        _holidays.Load();
                
        //Load the list of days this user has already requested.  This is used in the calendar's day render event to 
        //disable those days.      
        int entUserAccountId;
        int ptoRequestId;
        
        PTORequestEO ptoRequest = (PTORequestEO)ViewState[VIEW_STATE_KEY_PTOREQUEST];

        if (ptoRequest == null)
        {
            //Check if the id was passed from the query string
            ptoRequestId = GetId();

            if (ptoRequestId == 0)
            {
                //This will be 0 if a new record is being added the first time in this page
                entUserAccountId = CurrentUser.ID;
            }
            else
            {
                ptoRequest = new PTORequestEO();
                ptoRequest.Load(ptoRequestId);
                entUserAccountId = ptoRequest.ENTUserAccountId;
            }            
        }
        else        
        {                       
            entUserAccountId = ptoRequest.ENTUserAccountId;            
            ptoRequestId = ptoRequest.ID;
        }
        
        _priorDaysOff.LoadPreviousByENTUserAccountId(ptoRequestId, entUserAccountId);                
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ENTValidationErrors validationErrors = new ENTValidationErrors();
        PTORequestEO ptoRequest = (PTORequestEO)ViewState[VIEW_STATE_KEY_PTOREQUEST];

        //Get the original state of the object.
        PTORequestEO originalPTORequest = new PTORequestEO();
        originalPTORequest.Load(ptoRequest.ID);
        ptoRequest.OriginalItem = (ENTBaseEO)originalPTORequest;

        LoadObjectFromScreen(ptoRequest);

        if (!ptoRequest.Save(ref validationErrors, CurrentUser.ID))
        {
            //Reload the object.  If any validation errors occurred after a property was changed then
            //the object would retain that changed property.  You want to revert back to what it was 
            //originally.                        
            if ((ptoRequest == null) || (ptoRequest.ID == 0))
            {
                CreateNew(ref ptoRequest);
            }
            else
            {
                ptoRequest = originalPTORequest;
            }
            ViewState[VIEW_STATE_KEY_PTOREQUEST] = ptoRequest;
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }    
    }

    private void CreateNew(ref PTORequestEO ptoRequest)
    {
        //PTO specific
        ptoRequest = new PTORequestEO();
        ptoRequest.Init();
        ptoRequest.RequestDate = GetBusinessDay(DateTime.Today);
        ptoRequest.ENTUserAccountId = CurrentUser.ID;

        //Workflow specific
        ptoRequest.WFItem.SubmitterENTUserAccountId = CurrentUser.ID;
        ptoRequest.InitWorkflow(typeof(PTORequestEO).AssemblyQualifiedName);
    }

    protected override void LoadNew()
    {
        PTORequestEO ptoRequest = new PTORequestEO();
        CreateNew(ref ptoRequest);
        LoadScreenFromObject(ptoRequest);
    }

    protected override void LoadObjectFromScreen(PTORequestEO baseEO)
    {        
        baseEO.PTORequestTypeId = (PTORequestTypeBO.PTORequestTypeEnum)Convert.ToInt32(ddlPTORequestType.SelectedValue);
        baseEO.PTODayTypeId = (PTODayTypeBO.PTODayTypeEnum)Convert.ToInt32(ddlPTODayType.SelectedValue);        
        baseEO.RequestDate = calFullDay.SelectedDate;
                
        wfcPTORequest.LoadObjectFromControl(baseEO);
    }
        
    protected override void LoadScreenFromObject(PTORequestEO baseEO)
    {
        ddlPTORequestType.Items.FindByValue(Convert.ToInt32(baseEO.PTORequestTypeId).ToString()).Selected = true;
        ddlPTODayType.Items.FindByValue(Convert.ToInt32(baseEO.PTODayTypeId).ToString()).Selected = true;
                
        calFullDay.SelectedDate = baseEO.RequestDate;        

        lblRequestDate.Text = calFullDay.SelectedDate.ToStandardDateFormat();
        calFullDay.VisibleDate = calFullDay.SelectedDate;

        SetBalances(Convert.ToInt16(baseEO.RequestDate.Year), baseEO.ENTUserAccountId, baseEO.ID);
        
        wfcPTORequest.LoadControlFromObject(baseEO, CurrentUser.ID);
       
        ViewState[VIEW_STATE_KEY_PTOREQUEST] = baseEO;
    }

    protected override void LoadControls()
    {
        PTORequestTypeBOList ptoRequestTypes = new PTORequestTypeBOList();
        ptoRequestTypes.Load();
        ddlPTORequestType.DataSource = ptoRequestTypes;
        ddlPTORequestType.DataTextField = "DisplayText";
        ddlPTORequestType.DataValueField = "ID";
        ddlPTORequestType.DataBind();

        PTODayTypeBOList ptoDayTypes = new PTODayTypeBOList();
        ptoDayTypes.Load();
        ddlPTODayType.DataSource = ptoDayTypes;
        ddlPTODayType.DataTextField = "DisplayText";
        ddlPTODayType.DataValueField = "ID";
        ddlPTODayType.DataBind();                
    }

    protected override void GoToGridPage()
    {
        bool approving = false;
        NameValueCollection queryString = DecryptQueryString(Request.QueryString.ToString());

        if (queryString != null)
        {
            //Check if the user came from the approval screen.
            string approvingQueryString = queryString["Approving"];

            if (approvingQueryString != null)
            {
                approving = true;
            }
        }
        if (approving)
        {
            Response.Redirect("PTORequestApprove.aspx");
        }
        else
        {
            Response.Redirect("PTORequests.aspx");
        }
    }

    public override string MenuItemName()
    {
        return "Submit Request";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Submit Request" };
    }
           
    private DateTime GetBusinessDay(DateTime date)
    {               
        while (IsBusinessDay(date) == false)
        {
            date = date.AddDays(1);
        }

        return date;
    }

    private bool IsBusinessDay(DateTime date)
    {
        if (date.IsWeekend())
        {
            return false;
        }
        else if (_holidays.IsHoliday(date))
        {
            return false;
        }
        else if (_priorDaysOff.GetByRequestDate(date).Count > 0)            
        {                       
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void calFullDay_DayRender(object sender, DayRenderEventArgs e)
    {                   
        if (calFullDay.SelectedDate == e.Day.Date)
        {
            SetPTORequestCellText(e, e.Day.Date, (PTORequestTypeBO.PTORequestTypeEnum)Convert.ToInt32(ddlPTORequestType.SelectedValue),
                (PTODayTypeBO.PTODayTypeEnum)Convert.ToInt32(ddlPTODayType.SelectedValue), Color.LightGreen);      
      
            //check if there is another request for this day.  This could happen if the user requested two half
            //days on the same day.
             List<PTORequestEO> ptoRequests = _priorDaysOff.GetByRequestDate(e.Day.Date);
            if (ptoRequests.Count != 0)
            {
                foreach (PTORequestEO ptoRequest in ptoRequests)
                {
                    SetPTORequestCellText(e, e.Day.Date, ptoRequest.PTORequestTypeId, ptoRequest.PTODayTypeId, Color.LightGreen);          
                }                
            }
        }
        else        
        {
            if (e.Day.Date.IsWeekend())
            {
                e.Day.IsSelectable = false;
            }
            else
            {
                HolidayEO holiday = _holidays.GetHoliday(e.Day.Date);
                if (holiday != null)
                {
                    SetHolidayCell(e, holiday);
                }
                else
                {
                    List<PTORequestEO> ptoRequests = _priorDaysOff.GetByRequestDate(e.Day.Date);

                    if (ptoRequests.Count != 0)
                    {
                        foreach (PTORequestEO ptoRequest in ptoRequests)
                        {
                            SetPriorPTOCell(e, ptoRequest);    
                        }
                        if (ptoRequests.Count > 1)
                        {
                            e.Day.IsSelectable = false;
                        }
                    }
                }                
            }
        }        
    }

    private void SetHolidayCell(DayRenderEventArgs e, HolidayEO holiday)
    {   
            e.Day.IsSelectable = false;
            e.Cell.Controls.Add(new LiteralControl("<br />"));

            Label lbl = new Label();
            lbl.Text = "H";
            lbl.Font.Size = 6;
            e.Cell.Controls.Add(lbl);

            e.Cell.ToolTip = holiday.HolidayName;
            e.Cell.BackColor = Color.LightSkyBlue;
    }

    private void SetPriorPTOCell(DayRenderEventArgs e, PTORequestEO ptoRequest)
    {
        SetPTORequestCellText(e, ptoRequest.RequestDate, ptoRequest.PTORequestTypeId, ptoRequest.PTODayTypeId, Color.LightSkyBlue);        
    }

    private void SetPTORequestCellText(DayRenderEventArgs e, DateTime requestDate, PTORequestTypeBO.PTORequestTypeEnum requestType,
        PTODayTypeBO.PTODayTypeEnum dayType, Color color)
    {
        string text;
        
        switch (requestType)
        {
            case PTORequestTypeBO.PTORequestTypeEnum.Vacation:
                text = "V";
                break;
            case PTORequestTypeBO.PTORequestTypeEnum.Personal:
                text = "P";
                break;
            case PTORequestTypeBO.PTORequestTypeEnum.Unpaid:
                text = "U";
                break;
            default:
                throw new Exception("Unknown request type.");
        }

        switch (dayType)
        {
            case PTODayTypeBO.PTODayTypeEnum.AM:
                text += "-AM";
                e.Day.IsSelectable = true;
                break;
            case PTODayTypeBO.PTODayTypeEnum.PM:
                text += "-PM";
                e.Day.IsSelectable = true;
                break;
            case PTODayTypeBO.PTODayTypeEnum.Full:
                e.Day.IsSelectable = false;
                break;
            default:
                throw new Exception("Unknown day type.");
        }

        Label lbl = new Label();
        lbl.Text = text;
        lbl.Font.Size = 6;
                
        e.Cell.Controls.Add(new LiteralControl("<br />"));
        e.Cell.Controls.Add(lbl);

        e.Cell.BackColor = color;

    }
    protected void calFullDay_SelectionChanged(object sender, EventArgs e)
    {
        //Check if this already has a half day assigned to it.
        List<PTORequestEO> ptoRequests = _priorDaysOff.GetByRequestDate(calFullDay.SelectedDate);

        if (ptoRequests.Count != 0)
        {
            //Remove Full and what is already selected from the drop down list.
            ddlPTODayType.Items.Remove(ddlPTODayType.Items.FindByValue(Convert.ToInt32(PTODayTypeBO.PTODayTypeEnum.Full).ToString()));
            if (ptoRequests[0].PTODayTypeId == PTODayTypeBO.PTODayTypeEnum.AM)
            {
                ddlPTODayType.Items.Remove(ddlPTODayType.Items.FindByValue(Convert.ToInt32(PTODayTypeBO.PTODayTypeEnum.AM).ToString()));
            }
            else
            {
                ddlPTODayType.Items.Remove(ddlPTODayType.Items.FindByValue(Convert.ToInt32(PTODayTypeBO.PTODayTypeEnum.PM).ToString()));
            }
            ddlPTODayType.SelectedIndex = 0;
        }
        else
        {
            //Make sure all the Day types are in the drop down list
            if (ddlPTODayType.Items.Count != Enum.GetNames(typeof(PTODayTypeBO.PTODayTypeEnum)).Length)
            {
                //Add full back
                ddlPTODayType.Items.Insert(0, new ListItem("Full", Convert.ToInt32(PTODayTypeBO.PTODayTypeEnum.Full).ToString()));

                //Add either AM or PM whichever is missing
                if ( (PTODayTypeBO.PTODayTypeEnum)Enum.Parse(typeof(PTODayTypeBO.PTODayTypeEnum), ddlPTODayType.Items[1].Value) == PTODayTypeBO.PTODayTypeEnum.AM)
                {
                    ddlPTODayType.Items.Insert(2, new ListItem("PM", Convert.ToInt32(PTODayTypeBO.PTODayTypeEnum.PM).ToString()));
                }
                else
                {
                    ddlPTODayType.Items.Insert(1, new ListItem("AM", Convert.ToInt32(PTODayTypeBO.PTODayTypeEnum.AM).ToString()));
                }
            }
        }
        
        lblRequestDate.Text = calFullDay.SelectedDate.ToStandardDateFormat();
    }
    protected void calFullDay_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        if (e.PreviousDate.Year != e.NewDate.Year)
        {
            PTORequestEO ptoRequest = (PTORequestEO)ViewState[VIEW_STATE_KEY_PTOREQUEST];
            SetBalances(Convert.ToInt16(e.NewDate.Year), ptoRequest.ENTUserAccountId, ptoRequest.ID);
        }
    }

    private void SetBalances(short year, int entUserAccountId, int ptoRequestId)
    {
        //Load the vacation bank section
        //Get the bank for the year in the calendar
        PTOVacationBankEO vacationBank = new PTOVacationBankEO();
        vacationBank.Load(entUserAccountId, year);

        lblPersonalDaysBank.Text = vacationBank.PersonalDays.ToString();
        lblVacationDaysBank.Text = vacationBank.VacationDays.ToString();

        //Get the amount used so far
        double usedPersonalDays = 0;
        double usedVacationDays = 0;
        double unpaid = 0;
        PTORequestEO.GetUsed(ref usedPersonalDays, ref usedVacationDays, ref unpaid, entUserAccountId, entUserAccountId, year);

        lblPersonalDaysUsed.Text = usedPersonalDays.ToString();
        lblVacationDaysUsed.Text = usedVacationDays.ToString();
        lblUnpaidUsed.Text = unpaid.ToString();

        //Get the carry over from the previous year
        PTOVacationBankEO vacationBankPreviousYear = new PTOVacationBankEO();
        vacationBankPreviousYear.Load(entUserAccountId, Convert.ToInt16(year - 1));

        double usedPersonalDaysPreviousYear = 0;
        double usedVacationDaysPreviousYear = 0;
        double unpaidPreviousYear = 0;
        PTORequestEO.GetUsed(ref usedPersonalDaysPreviousYear, ref usedVacationDaysPreviousYear, ref unpaidPreviousYear, ptoRequestId, entUserAccountId, Convert.ToInt16(year - 1));

        //You are allowed to carry over 5 unused vacation days from the previous year.
        double netVacationLastYear = vacationBankPreviousYear.VacationDays - usedVacationDaysPreviousYear;
        if ((netVacationLastYear >= 5) || (netVacationLastYear < 0))
        {
            lblVacationCarry.Text = netVacationLastYear.ToString();
        }
        else
        {
            lblVacationCarry.Text = "0";
        }

        //show balance
        lblPersonalBalance.Text = (vacationBank.PersonalDays - usedPersonalDays).ToString();
        lblVacationBalance.Text = ((vacationBank.VacationDays + Convert.ToDouble(lblVacationCarry.Text)) - usedVacationDays).ToString();
    }        
}