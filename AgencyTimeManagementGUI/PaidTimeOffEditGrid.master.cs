using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework;

public partial class PaidTimeOffEditGrid : System.Web.UI.MasterPage
{
    public delegate void ButtonClickedHandler(object sender, EventArgs e);

    public event ButtonClickedHandler AddButton_Click;
    
    //Chapter 9: Reporting
    public event ButtonClickedHandler PrintButton_Click;

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (AddButton_Click != null)
        {
            AddButton_Click(sender, e);
        }
    }

    //Chapter 9: Reporting
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (PrintButton_Click != null)
        {
            PrintButton_Click(sender, e);
        }
    }

    public ENTValidationErrors ValidationErrors
    {
        get
        {
            return ValidationErrorMessages1.ValidationErrors;
        }

        set
        {
            ValidationErrorMessages1.ValidationErrors = value;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (((BasePage)this.Page).ReadOnly)
        {
            //Hide the Add button
            btnAddNew.Visible = false;
        }
    }

    public Button btnAddNewRef()
    {
        return btnAddNew;
    }

    //Chapter 9: Reporting
    public Button btnPrintRef()
    {
        return btnPrint;
    }        
}
