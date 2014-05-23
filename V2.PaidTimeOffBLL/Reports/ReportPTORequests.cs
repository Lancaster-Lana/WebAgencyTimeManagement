using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using Agency.PaidTimeOffDAL.Reports;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffBLL.Framework;
using Agency.PaidTimeOffDAL.Framework.Reports;

namespace Agency.PaidTimeOffBLL.Reports
{
    public class ReportPTORequests : ENTBaseQueryBO<ReportPTORequestsQueryData, ReportMyPTORequestsResult>
    {
        
    }

    public class ReportRoleQuery : ENTBaseQueryBO<ReportENTRoleDataQuery, ReportRoleCapabilityResult>
    {

    }
}
