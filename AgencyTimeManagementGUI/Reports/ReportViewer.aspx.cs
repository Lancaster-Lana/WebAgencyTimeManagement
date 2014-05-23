using System;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Reports_ReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var report = new ReportDocument();
        report.Load(Server.MapPath("Roles.rpt"));

        //Set the location for the main report.
        SetTableLocation(report.Database.Tables);

        //Set the location for any of the subreports.
        foreach (ReportDocument rd in report.Subreports)
        {
            SetTableLocation(rd.Database.Tables);
        }

        report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");

    }

    private void SetTableLocation(Tables tables)
    {
        var connectionInfo = new ConnectionInfo();

        connectionInfo.ServerName = "LANCASTER";
        connectionInfo.DatabaseName = "HRPaidTimeOff";
        connectionInfo.UserID = "V2Application";
        connectionInfo.Password = "wrox";

        foreach (Table table in tables)
        {
            TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
            tableLogOnInfo.ConnectionInfo = connectionInfo;
            table.ApplyLogOnInfo(tableLogOnInfo);
        }
    }
}
