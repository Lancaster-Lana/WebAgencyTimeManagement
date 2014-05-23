using System;
using System.Web.UI.WebControls;
using Agency.PaidTimeOffBLL;
using Agency.FrameworkControls;

public partial class Controls_PTORequests : System.Web.UI.UserControl
{
    private const int COL_INDEX_REQUESTDATE = 0;
    private const int COL_INDEX_REQUESTOR = 1;
    private const int COL_INDEX_STATUS = 1;
    private const int COL_INDEX_CURRENT_OWNER = 2;

    public enum FilterEnum
    {
        MyRequests,
        RequestsToApprove
    }
        
    public FilterEnum Filter { get; set; }
            
    protected void Page_Load(object sender, EventArgs e)
    {
        var cgvPTORequests = new CustomGridView();
        cgvPTORequests.ListClassName = typeof(PTORequestEOList).AssemblyQualifiedName;
        cgvPTORequests.AddBoundField("", "Request Date", "");

        //Check the filter type
        switch (Filter)
        {
            case FilterEnum.RequestsToApprove:
                //Get request to approve
                cgvPTORequests.LoadMethodName = "LoadByCurrentOwnerId";
                cgvPTORequests.LoadMethodParameters.Add(((BasePage)Page).CurrentUser.ID);

                //Add the requestor column.
                cgvPTORequests.AddBoundField("", "Requestor", "");
                break;
            default:
                //Default to my request if the developer forgot to set the filter property
                cgvPTORequests.LoadMethodName = "LoadByENTUserAccountId";
                cgvPTORequests.LoadMethodParameters.Add(((BasePage)Page).CurrentUser.ID);

                //Add status and current owner
                cgvPTORequests.AddBoundField("", "Status", "");
                cgvPTORequests.AddBoundField("", "Current Owner", "");
                break;
        }

        cgvPTORequests.AddBoundField("RequestTypeString", "Request Type", "");
        cgvPTORequests.RowDataBound += cgvPTORequests_RowDataBound;

        //Default the sort to the request date
        cgvPTORequests.SortExpressionLast = "RequestDate";
        cgvPTORequests.DataBind();
        Controls.Add(cgvPTORequests);
    }
        
    protected void cgvPTORequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add the edit link to the action column.
            var editLink = new HyperLink();
            var request = (PTORequestEO)e.Row.DataItem;
            editLink.Text = request.RequestDateString;

            if (Filter == FilterEnum.RequestsToApprove)
            {
                editLink.NavigateUrl = BasePage.RootPath(Context) + "PTORequest.aspx" + BasePage.EncryptQueryString("id=" + request.ID.ToString() + "&Approving=true");
            }
            else
            {
                editLink.NavigateUrl = BasePage.RootPath(Context) + "PTORequest.aspx" + BasePage.EncryptQueryString("id=" + request.ID.ToString());
            }

            e.Row.Cells[COL_INDEX_REQUESTDATE].Controls.Add(editLink);


            if (Filter == FilterEnum.RequestsToApprove)
            {
                //Show the requestor
                e.Row.Cells[COL_INDEX_REQUESTOR].Text = Globals.GetUsers(Page.Cache).GetByID(request.ENTUserAccountId).DisplayText;
            }
            else
            {
                //Must be showing my requests.
                e.Row.Cells[COL_INDEX_STATUS].Text = request.CurrentState.StateName;

                e.Row.Cells[COL_INDEX_CURRENT_OWNER].Text = request.CurrentOwnerUserName;
            }
        }
    }
}
