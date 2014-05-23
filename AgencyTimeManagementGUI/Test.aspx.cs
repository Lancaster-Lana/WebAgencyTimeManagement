using System;

public partial class Test : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        var currentScript = new System.Text.StringBuilder();
                
        // Get the postback script.
        string postback = this.Page.ClientScript.GetPostBackEventReference(Button1, "");
                
        // Remember the current action.
        currentScript.Append("var oldAction = document.forms[0].action;");
        // Change target to a new form.
        string windowName = DateTime.Now.ToString("MMddyyyyHHmmssffff");
        currentScript.Append("document.forms[0].target='" + windowName + "';");

        currentScript.Append("window.open('Catch.html', '" + windowName + "', 'scrollbars=yes,status=no,resizable=yes');");
        
        // Add the postback script.
        currentScript.Append(postback + ";");

        // Reset target back to self.
        currentScript.Append("document.forms[0].target='_self';");
        // Reset the action.
        currentScript.Append("document.forms[0].action=oldAction;");
        // Return false to stop page submitting.
        currentScript.Append("return false;");
        
        Button1.Attributes.Add("onclick", currentScript.ToString());
    }

    public override string MenuItemName()
    {
        return "Home";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Home" };
    }
    protected void Button1_Click(object sender, EventArgs e)
    {        
        int x = 0;
        for (int i = 0; i < 1000000; i++)
        {
            x++;
        }

        Context.Items.Add("test", TextBox1.Text);
        Server.Transfer("Catch.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string x = "";
    }
}

//public class CustomButton : Button
//{
//    protected override void AddAttributesToRender(HtmlTextWriter writer)
//    {
//        System.Text.StringBuilder currentScript = new System.Text.StringBuilder();

//        // Get the current onclientclick script.
//        currentScript.Append(this.OnClientClick);

//        // Get the script from our attributes and then remove the onclick
//        // attribute.
//        if (base.Attributes["onclick"] != null)
//        {
//            currentScript.Append(base.Attributes["onclick"]);
//            base.Attributes.Remove("onclick");
//        }

//        // Get postback options, and make sure we are doing a client script submit,
//        // this helped in fixing the issue of changing the action and reverting back,
//        // granted it needs js but the cross page postback needs js anyway.
//        PostBackOptions opt = this.GetPostBackOptions();
//        opt.ClientSubmit = true;

//        // Get the postback script.
//        string postback = this.Page.ClientScript.GetPostBackEventReference(opt, false);

//        if (postback != null)
//        {

//            if (this.PostBackUrl != string.Empty)
//            {
//                // Remember the current action.
//                currentScript.Append("var oldAction = document.forms[0].action;");
//                // Change target to a new form.
//                currentScript.Append("document.forms[0].target='_blank';");
//            }

//            // Add the postback script.
//            currentScript.Append(postback + ";");

//            if (this.PostBackUrl != string.Empty)
//            {
//                // Reset target back to self.
//                currentScript.Append("document.forms[0].target='_self';");
//                // Reset the action.
//                currentScript.Append("document.forms[0].action=oldAction;");
//                // Return false to stop page submitting.
//                currentScript.Append("return false;");
//            }

//        }

//        // Add our new onclick attribute. 
//        if (currentScript.Length > 0)
//            writer.AddAttribute("onclick", currentScript.ToString());

//        base.AddAttributesToRender(writer);

//    }       

//}

