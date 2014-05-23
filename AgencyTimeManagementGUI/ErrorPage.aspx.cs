using System;

public partial class ErrorPage : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override string MenuItemName()
    {
        return "Home";
    }
        
    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
}
