using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL;

public partial class PTORequestApprove : BasePage
{
    private const int COL_INDEX_ACTION = 0;
    private const int COL_INDEX_REQUEST_DATE = 1;
    private const int COL_INDEX_REQUEST_TYPE = 2;
    private const int COL_INDEX_STATUS = 3;    
    private const int COL_INDEX_DATE_SUBMITTED = 4;

    protected void Page_Load(object sender, EventArgs e)
    {    
        if (!IsPostBack)
        {
            Button addButton = Master.btnAddNewRef();
            addButton.Visible = false;

            //Tell the control what class to create and what method to call to load the class.
            cgvPTORequests.ListClassName = typeof(PTORequestEOList).AssemblyQualifiedName;
            cgvPTORequests.LoadMethodName = "LoadByCurrentOwnerId";
            cgvPTORequests.LoadMethodParameters.Add(CurrentUser.ID);

            //Action column-Contains the Edit link
            cgvPTORequests.AddBoundField("", "Actions", "");

            //Request Date
            cgvPTORequests.AddBoundField("RequestDateString", "Request Date", "RequestDate");
            cgvPTORequests.AddBoundField("RequestTypeString", "Type", "RequestTypeString");
            cgvPTORequests.AddBoundField("", "Status", "");            
            cgvPTORequests.AddBoundField("", "Date Submitted", "");

            //Default the sort to the request date
            cgvPTORequests.SortExpressionLast = "RequestDate";

            cgvPTORequests.DataBind();
        }     
    }
        
    public override string MenuItemName()
    {
        return "Approve Requests";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Approve Requests" };
    }

    protected void cgvPTORequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add the edit link to the action column.
            HyperLink editLink = new HyperLink();            
            editLink.Text = "View";
            
            editLink.NavigateUrl = "PTORequest.aspx" + EncryptQueryString("id=" + ((PTORequestEO)e.Row.DataItem).ID.ToString() + "&Approving=true");

            e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

            //Get the state and show that as the status
            e.Row.Cells[COL_INDEX_STATUS].Text = ((PTORequestEO)e.Row.DataItem).CurrentState.StateName;

            e.Row.Cells[COL_INDEX_DATE_SUBMITTED].Text = ((PTORequestEO)e.Row.DataItem).InsertDate.ToStandardDateFormat();
        }
    }        
}
