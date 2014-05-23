using System;
using System.Collections;
using Agency.PaidTimeOffBLL;
using Agency.PaidTimeOffBLL.Framework;

public partial class Administration_CopyVacationBank : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Load the years
            //The copy from should list all years that have a value
            ArrayList years = PTOVacationBankEO.GetDistinctYears();

            foreach (short year in years)
            {
                ddlFrom.Items.Add(year.ToString());
            }

            //Copy to should list the next 10 years
            int startDate = DateTime.Today.Year;
            int endDate = DateTime.Today.AddYears(10).Year;
            for (int i = startDate; i < endDate; i++)
            {
                ddlTo.Items.Add(i.ToString());
            }

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VacationBanks.aspx");
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        ENTValidationErrors validationErrors = new ENTValidationErrors();

        if (ddlFrom.Text == ddlTo.Text)
        {
            validationErrors.Add("The from and to years can not be the same.");
            ValidationErrorMessages1.ValidationErrors = validationErrors;
        }
        else
        {
            PTOVacationBankEO.CopyYear(Convert.ToInt16(ddlFrom.Text), Convert.ToInt16(ddlTo.Text), CurrentUser.ID);
            Response.Redirect("VacationBanks.aspx");
        }
    }

    public override string MenuItemName()
    {
        return "Vacation Banks";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Vacation Banks" };
    }
}
