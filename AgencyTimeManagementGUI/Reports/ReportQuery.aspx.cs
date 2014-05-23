using System;
using System.Reflection;

using System.Collections.Specialized;
using Agency.PaidTimeOffBLL.Framework;
using Agency.PaidTimeOffBLL.Framework.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class Reports_ReportQuery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnPreview.Attributes.Add("onclick", GetPrintButtonScript(btnPreview));        
    }

    private void SetCurrentReport()
    {
        //Decrypt the query string
        var queryString = DecryptQueryString(Request.QueryString.ToString());

        if (queryString != null)
        {
            //Check if the id was passed in.
            string id = queryString["id"];

            if ((id != null) && (id != "0"))
            {
                //Get the report object
                ENTReportBO report = new ENTReportBO();
                report.Load(Convert.ToInt32(id));

                QueryBuilder1.QueryObjectName = report.ObjectName;

                CurrentReport = report;
            }         
        }
    }

    private ENTReportBO CurrentReport
    {
        get
        {
            return (ENTReportBO)ViewState["Report"];
        }
        set
        {
            ViewState["Report"] = value;
        }
    }

    public override string MenuItemName()
    {
        return "Report Listing";
    }

    public override string[] CapabilityNames()
    {
        SetCurrentReport();
        return new string[] { CurrentReport.ReportName };
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        string whereClause = "";
        var validationErrors = new ENTValidationErrors();

        if (QueryBuilder1.GetWhereClause(ref whereClause, ref validationErrors))
        {
            var currentReport = CurrentReport;

            var report = new ReportDocument();
            report.Load(Server.MapPath(currentReport.FileName));

            var objectType = Type.GetType(currentReport.ObjectName);
            object listObject = Activator.CreateInstance(objectType);
            if (objectType != null)
            {
                var data = (object[])objectType.InvokeMember("Select", BindingFlags.InvokeMethod, null, listObject, new object[] { whereClause });

                //TODO:  added length check after chapter was written.
                if (data.Length > 0)
                {
                    //Get the data                
                    report.SetDataSource(data);

                    //Check if there is a sub report
                    if (report.Subreports.Count > 0)
                    {
                        //Only support 1 subreport
                        //Get the object that can retrieve the data for the subreport                
                        Type objectType1 = Type.GetType(currentReport.SubReportObjectName);
                        //The methods must be static for this data. 
                        if (objectType1 != null)
                        {
                            var subreportData = (object[])objectType1.InvokeMember(currentReport.SubReportMethodName, BindingFlags.InvokeMethod, null, objectType1, new object[] { });

                            report.Subreports[0].SetDataSource(subreportData);
                        }
                    }

                    report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
                }
                else
                {
                    Response.Clear();
                    Response.Write("No data matches your criteria for this report.");
                    Response.End();
                }
            }
        }
        else
        {
            Response.Clear();
            foreach(var ve in validationErrors)
            {
                Response.Write(ve.ErrorMessage + "<br>" + Environment.NewLine);
            }
            Response.End();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportList.aspx");
    }
}
