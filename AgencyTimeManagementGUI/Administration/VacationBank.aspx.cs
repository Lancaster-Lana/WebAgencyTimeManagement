using System;
using Agency.PaidTimeOffBLL.Framework;
using Agency.PaidTimeOffBLL;

public partial class Administration_VacationBank : BaseEditPage<PTOVacationBankEO>
{
    #region Constants

    private const string VIEW_STATE_KEY_VACATION_BANK = "VacationBank";

    #endregion Constants

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += Master_SaveButton_Click;
        Master.CancelButton_Click += Master_CancelButton_Click;
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        var validationErrors = new ENTValidationErrors();
        var ptoVacationBank = (PTOVacationBankEO)ViewState[VIEW_STATE_KEY_VACATION_BANK];
        LoadObjectFromScreen(ptoVacationBank);

        if (!ptoVacationBank.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            GoToGridPage();
        }
    }

    protected override void LoadObjectFromScreen(PTOVacationBankEO baseEO)
    {
        baseEO.ENTUserAccountId = Convert.ToInt32(ddlENTUserAccount.SelectedValue);
        baseEO.VacationYear = Convert.ToInt16(ddlYear.Text);
        baseEO.PersonalDays = Convert.ToByte(txtPersonalDays.Text);
        baseEO.VacationDays = Convert.ToByte(txtVacationDays.Text);
    }

    protected override void LoadScreenFromObject(PTOVacationBankEO baseEO)
    {
        if (baseEO.ENTUserAccountId != 0)
        {
            ddlENTUserAccount.Items.FindByValue(baseEO.ENTUserAccountId.ToString()).Selected = true;
        }

        ddlYear.Items.FindByText(baseEO.VacationYear.ToString()).Selected = true;
        txtPersonalDays.Text = baseEO.PersonalDays.ToString();
        txtVacationDays.Text = baseEO.VacationDays.ToString();

        ViewState[VIEW_STATE_KEY_VACATION_BANK] = baseEO;
    }

    protected override void LoadControls()
    {
        //Load the year
        int startYear = DateTime.Today.AddYears(-5).Year;
        int endYear = startYear + 15;
        for (int i = startYear; i < endYear; i++)
        {
            ddlYear.Items.Add(i.ToString());
        }
               
        //Load all the users
        var users = new ENTUserAccountEOList();
        users.Load();

        ddlENTUserAccount.DataSource = users;
        ddlENTUserAccount.DataTextField = "DisplayText";
        ddlENTUserAccount.DataValueField = "ID";
        ddlENTUserAccount.DataBind();
    }

    protected override void GoToGridPage()
    {
        Response.Redirect("VacationBanks.aspx");
    }

    public override string MenuItemName()
    {
        return "Vacation Banks";
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Vacation Banks" };
    }
}
