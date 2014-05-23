using System;
using System.Web.UI.WebControls.WebParts;

public partial class Home : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);        
    }

    public override string MenuItemName()
    {
        return "Home";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Home" };
    }
    protected void lbtnCustomize_Click(object sender, EventArgs e)
    {
        WebPartManager1.DisplayMode = WebPartManager.CatalogDisplayMode;
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        WebPartManager1.Personalization.ResetPersonalizationState();
    }
}
