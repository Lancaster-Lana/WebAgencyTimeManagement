using System;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Agency.PaidTimeOffBLL.Framework.Reports;

public partial class Reports_ReportViewer3Layer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var report = new ReportDocument();
        report.Load(Server.MapPath("Roles3Layer.rpt"));
        //Get the data                
        report.SetDataSource(ReportENTRole.SelectCapabilities());
        report.Subreports[0].SetDataSource(ReportENTRole.SelectUserAccounts());
        report.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, false, "");
    }
}
