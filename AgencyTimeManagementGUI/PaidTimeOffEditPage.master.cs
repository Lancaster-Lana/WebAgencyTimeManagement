using System;
using Agency.PaidTimeOffBLL.Framework;

public partial class PaidTimeOffEditPage : System.Web.UI.MasterPage
{
    public delegate void ButtonClickedHandler(object sender, EventArgs e);

    public event ButtonClickedHandler SaveButton_Click;
    public event ButtonClickedHandler CancelButton_Click;

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveButton_Click != null)
        {
            SaveButton_Click(sender, e);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (CancelButton_Click != null)
        {
            CancelButton_Click(sender, e);
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
            //Hide the save button
            btnSave.Visible = false;
        }
    }
}
