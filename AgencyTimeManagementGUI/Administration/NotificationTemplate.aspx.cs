using System;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_NotificationTemplate : BaseEditPage<ENTNotificationEO>
{
    private const string VIEW_STATE_KEY_NOTIFICATION = "Notification";

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

        var notification = (ENTNotificationEO)ViewState[VIEW_STATE_KEY_NOTIFICATION];
        LoadObjectFromScreen(notification);

        if (!notification.Save(ref validationErrors, 1))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    protected override void LoadObjectFromScreen(ENTNotificationEO baseEO)
    {
        baseEO.FromEmailAddress = txtFromEmailAddress.Text;
        baseEO.Subject = txtSubject.Text;
        baseEO.Body = txtBody.Text;
    }

    protected override void LoadScreenFromObject(ENTNotificationEO baseEO)
    {
        lblDescription.Text = baseEO.Description;
        txtFromEmailAddress.Text = baseEO.FromEmailAddress;
        txtSubject.Text = baseEO.Subject;
        txtBody.Text = baseEO.Body;

        ViewState[VIEW_STATE_KEY_NOTIFICATION] = baseEO;
    }

    protected override void LoadControls()
    {
        
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("NotificationTemplates.aspx");
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
