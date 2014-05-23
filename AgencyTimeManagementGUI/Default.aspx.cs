using System;
using System.Web;

public partial class Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += Master_AddButton_Click;
        //if(!HttpContext.Current.Request.IsAuthenticated)
        //    Response.Redirect("Startup.aspx");
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override string MenuItemName()
    {
        return "Home" ;
    }

    public override string[] CapabilityNames()
    {
        return new[] { "Auditing" };
    }
}
