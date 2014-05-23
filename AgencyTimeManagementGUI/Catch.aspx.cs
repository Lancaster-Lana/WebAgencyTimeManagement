using System;

public partial class Catch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Context.Items["test"].ToString();
    }
}
