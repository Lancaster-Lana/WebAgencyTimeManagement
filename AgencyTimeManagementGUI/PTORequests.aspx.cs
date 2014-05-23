using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Agency.PaidTimeOffBLL.Reports;

public partial class PTORequests : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_REQUEST_DATE = 1;
    private const int COL_INDEX_REQUEST_TYPE = 2;
    private const int COL_INDEX_STATUS = 3;
    private const int COL_INDEX_CURRENT_OWNER = 4;
    private const int COL_INDEX_DATE_SUBMITTED = 5;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_AddButton_Click);
        
        //Chapter 9: Reporting
        Master.PrintButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_PrintButton_Click);
                
        if (!IsPostBack)
        {
            Button addButton = Master.btnAddNewRef();
            addButton.Text = "Make Another Request";
                        
            //Tell the control what class to create and what method to call to load the class.
            cgvPTORequests.ListClassName = typeof(PTORequestEOList).AssemblyQualifiedName;
            cgvPTORequests.LoadMethodName = "LoadByENTUserAccountId";
            cgvPTORequests.LoadMethodParameters.Add(CurrentUser.ID);

            //Action column-Contains the Edit link
            cgvPTORequests.AddBoundField("", "Actions", "");

            //Request Date
            cgvPTORequests.AddBoundField("RequestDateString", "Request Date", "RequestDate");
            cgvPTORequests.AddBoundField("RequestTypeString", "Type", "RequestTypeString");
            cgvPTORequests.AddBoundField("", "Status", "");
            cgvPTORequests.AddBoundField("", "Current Owner", "");
            cgvPTORequests.AddBoundField("", "Date Submitted", "");

            //Default the sort to the request date
            cgvPTORequests.SortExpressionLast = "RequestDate";
            
            cgvPTORequests.DataBind();

            #region Chapter 9
            
            Master.btnPrintRef().Visible = true;

            Master.btnPrintRef().Attributes.Add("onclick", GetPrintButtonScript(Master.btnPrintRef()));

            #endregion Chapter 9
        }     
    }
        
    //Chapter 9: Reporting
    void Master_PrintButton_Click(object sender, EventArgs e)
    {
        ReportDocument report = new ReportDocument();
        report.Load(Server.MapPath("Reports/MyPTORequests.rpt"));

        //Get the data                
        report.SetDataSource(ReportMyPTORequests.Select(CurrentUser.ID));                

        report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
    }

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("PTORequest.aspx" + EncryptQueryString("id=0"));
    }

    public override string MenuItemName()
    {
        return "View My Requests";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "View My Requests" };
    }

    protected void cgvPTORequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add the edit link to the action column.
            HyperLink editLink = new HyperLink();
            if (ReadOnly)
            {
                editLink.Text = "View";
            }
            else
            {
                editLink.Text = "Edit";
            }
            editLink.NavigateUrl = "PTORequest.aspx" + EncryptQueryString("id=" + ((PTORequestEO)e.Row.DataItem).ID.ToString());

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);
                        
            //Get the state and show that as the status
            e.Row.Cells[COL_INDEX_STATUS].Text = ((PTORequestEO)e.Row.DataItem).CurrentState.StateName;
            
            e.Row.Cells[COL_INDEX_CURRENT_OWNER].Text = ((PTORequestEO)e.Row.DataItem).CurrentOwnerUserName;

            e.Row.Cells[COL_INDEX_DATE_SUBMITTED].Text = ((PTORequestEO)e.Row.DataItem).InsertDate.ToStandardDateFormat();
        }
    }        
}
