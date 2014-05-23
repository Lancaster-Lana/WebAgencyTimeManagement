using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL.Framework.Reports;
using Agency.PaidTimeOffBLL.Framework;

public partial class Reports_ReportList : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_REPORT_NAME = 1;
    private const int COL_INDEX_REPORT_DESCRIPTION = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.btnAddNewRef().Visible = false;

        if (!IsPostBack)
        {
            //Tell the control what class to create and what method to call to load the class.
            cgvReports.ListClassName = typeof(ENTReportBOList).AssemblyQualifiedName;
            cgvReports.LoadMethodName = "Load";

            //Action column-Contains the Edit link
            cgvReports.AddBoundField("", "Actions", "");

            //Name            
            cgvReports.AddBoundField("ReportName", "Description", "ReportName");
            cgvReports.AddBoundField("Description", "Description", "Description");

            cgvReports.DataBind();
        }        
    }

    public override string MenuItemName()
    {
        return "Report Listing";
    }

    public override string[] CapabilityNames()
    {
        var capabilities = Globals.GetCapabilities(this.Cache).GetByMenuItemId(Globals.GetMenuItems(this.Cache).GetByMenuItemName(MenuItemName()).ID);;

        var capabilityNames = new string[capabilities.Count()];

        for (int i = 0; i < capabilities.Count(); i++)
        {
            capabilityNames[i] = capabilities.ElementAt(i).CapabilityName;
        }                
        return capabilityNames;
    }

    protected override void NoAccessToPage(string capabilityName)
    {
        //This page has a capability associated with each report so override the default functionalit
        //in the BasePage class so an error does not get thrown if the user
        //does not have access to all the reports.
    }

    protected void cgvReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Visible = false;

            //A capabilty must exist for this report in order for it to appear in the list.
            //The capability name must match the report name.
            var capabilities = Globals.GetCapabilities(this.Cache);
            var capability = capabilities.GetByName(((ENTReportBO)e.Row.DataItem).ReportName);

            if (capability != null)
            {
                var roles = Globals.GetRoles(this.Cache);
                int currentUserId = CurrentUser.ID;

                foreach (var role in roles)
                {
                    if (role.RoleUserAccounts.IsUserInRole(currentUserId))
                    {
                        var roleCapability = role.RoleCapabilities.GetByCapabilityID(capability.ID);

                        if ((roleCapability != null) && (roleCapability.AccessFlag != ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.None))
                        {
                            //Add the edit link to the action column.
                            var runLink = new HyperLink();
                            runLink.Text = "Run";

                            runLink.NavigateUrl = "ReportQuery.aspx" + EncryptQueryString("id=" + ((ENTReportBO)e.Row.DataItem).ID.ToString(CultureInfo.InvariantCulture));

                            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(runLink);

                            e.Row.Visible = true;
                        }
                    }
                }
            }            
        }
    }
}
