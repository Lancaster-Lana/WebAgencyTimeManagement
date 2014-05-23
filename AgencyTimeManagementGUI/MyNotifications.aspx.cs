using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class MyNotifications : BaseEditPage<MyNotificationsEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_MYNOTIFICATIONS = "MyNotifications";

    #endregion Constants

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += new PaidTimeOffEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new PaidTimeOffEditPage.ButtonClickedHandler(Master_CancelButton_Click);
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var myNotifications = (MyNotificationsEO)ViewState[VIEW_STATE_KEY_MYNOTIFICATIONS];
        LoadObjectFromScreen(myNotifications);

        if (!myNotifications.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            var myNotifications = new MyNotificationsEO();
            myNotifications.LoadByENTUserAccountId(CurrentUser.ID);
            LoadScreenFromObject(myNotifications);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        //Build the notification list.
        var notifications = new ENTNotificationEOList();
        notifications.Load();

        foreach (ENTNotificationEO notification in notifications)
        {
            var tr = new HtmlTableRow();
            
            //the first cell contains a checkbox
            var tc1 = new HtmlTableCell();
            tc1.Attributes.Add("NotificationId", notification.ID.ToString());

            var chkNotify = new CheckBox();
            chkNotify.Text = notification.Description;
            chkNotify.ID = "chk" + notification.ID.ToString();
            tc1.Controls.Add(chkNotify);
            tc1.VAlign = "top";
            tr.Cells.Add(tc1);

            //if this is the state notification then add a listbox to the second cell.
            if ((ENTNotificationEO.NotificationType)notification.ID == ENTNotificationEO.NotificationType.IssueIOwnedGoesToState)
            {
                var tc2 = new HtmlTableCell();

                var lstStates = new ListBox();
                lstStates.SelectionMode = ListSelectionMode.Multiple;

                //get the states
                var states = new ENTWFStateEOList();
                states.Load();
                var sortedStates = states.SortByPropertyName("StateName", true);

                if (states.Count > 0)
                {
                    lstStates.DataSource = sortedStates;
                    lstStates.DataTextField = "DisplayText";
                    lstStates.DataValueField = "ID";
                    lstStates.DataBind();

                    lstStates.Rows = states.Count;
                }
                tc2.Controls.Add(lstStates);

                tr.Cells.Add(tc2);
            }
            else
            {
                tc1.ColSpan = 2;
            }            
            tblNotifications.Rows.Add(tr);
        }
    }
        
    protected override void LoadControls()
    {
        
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("Home.aspx");
    }

    public override string MenuItemName()
    {
        return "My Notifications";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "My Notifications" };
    }
           
    protected override void LoadObjectFromScreen(MyNotificationsEO baseEO)
    {
        foreach (HtmlTableRow tr in tblNotifications.Rows)
        {
            //Cell one has the checkbox and the id for the notification.
            var notificationType = (ENTNotificationEO.NotificationType)((int.Parse(tr.Cells[0].Attributes["NotificationId"].ToString())));

            //Try to find this in the object.
            var myNotification = baseEO.UserNotifications.Get(notificationType);

            bool isChecked = ((CheckBox)tr.Cells[0].Controls[0]).Checked;

            if (myNotification == null)
            {
                if (isChecked)
                {
                    myNotification = new ENTNotificationENTUserAccountEO();
                    myNotification.ENTNotificationId = (int)notificationType;
                    myNotification.ENTUserAccountId = CurrentUser.ID;

                    baseEO.UserNotifications.Add(myNotification);
                }
            }
            else
            {
                if (isChecked == false)
                {
                    myNotification.DBAction = ENTBaseEO.DBActionEnum.Delete;
                }
            }

            if ((isChecked) && (notificationType == ENTNotificationEO.NotificationType.IssueIOwnedGoesToState))
            {
                myNotification.NotificationStates.Clear();
                var lstState = (ListBox)tr.Cells[1].Controls[0];

                foreach (ListItem li in lstState.Items)
                {
                    if (li.Selected == true)
                    {
                        myNotification.NotificationStates.Add(new ENTNotificationENTWFStateEO
                        {
                            ENTNotificationENTUserAccountId = myNotification.ID,
                            ENTWFStateId = int.Parse(li.Value)
                        });
                    }
                }
            }
        }
    }

    protected override void LoadScreenFromObject(MyNotificationsEO baseEO)
    {
        foreach (HtmlTableRow tr in tblNotifications.Rows)
        {
            //Cell one has the checkbox and the id for the notification.
            var notificationType = (ENTNotificationEO.NotificationType) int.Parse(tr.Cells[0].Attributes["NotificationId"].ToString());

            //Try to find this in the object.
            ENTNotificationENTUserAccountEO myNotification = baseEO.UserNotifications.Get(notificationType);

            if (myNotification != null)
            {
                //Check the box
                ((CheckBox)tr.Cells[0].Controls[0]).Checked = true;

                if (notificationType == ENTNotificationEO.NotificationType.IssueIOwnedGoesToState)
                {
                    var lstStates = (ListBox)tr.Cells[1].Controls[0];

                    //Highlight each state in the list box.
                    foreach (ENTNotificationENTWFStateEO state in myNotification.NotificationStates)
                    {
                        //Find this item in the list box.
                        foreach (ListItem li in lstStates.Items)
                        {
                            if (li.Value == state.ENTWFStateId.ToString())
                            {
                                //Set it to selected.
                                li.Selected = true;
                                break;
                            }
                        }
                    }

                }
            }
        }

        ViewState[VIEW_STATE_KEY_MYNOTIFICATIONS] = baseEO;
    }
}
