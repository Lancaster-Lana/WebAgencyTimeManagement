using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Drawing;
using Agency.PaidTimeOffBLL;

public partial class Controls_PTORequestsCalendar : UserControl
{
    private PTORequestEOList _requests = new PTORequestEOList();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        _requests.LoadByENTUserAccountId(((BasePage)Page).CurrentUser.ID);
        //}
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        List<PTORequestEO> todaysRequests = _requests.GetByRequestDate(e.Day.Date);

        foreach (PTORequestEO ptoRequest in todaysRequests)
        {
            if ((ptoRequest.CurrentState.StateName != "Cancelled") && (ptoRequest.CurrentState.StateName != "Denied"))
            {
                SetPTORequestCellText(e, ptoRequest.PTORequestTypeId, ptoRequest.PTODayTypeId);
            }
        }
    }

    private void SetPTORequestCellText(DayRenderEventArgs e, PTORequestTypeBO.PTORequestTypeEnum requestType,
        PTODayTypeBO.PTODayTypeEnum dayType)
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
        var lbl = new Label();
        lbl.Text = text;
        lbl.Font.Size = 6;
        e.Cell.Controls.Add(new LiteralControl("<br />"));
        e.Cell.Controls.Add(lbl);
        e.Cell.BackColor = Color.LightSkyBlue;
    }
    //protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    //{
    //    _requests.LoadByENTUserAccountId(((BasePage)Page).CurrentUser.ID);
    //}
}
