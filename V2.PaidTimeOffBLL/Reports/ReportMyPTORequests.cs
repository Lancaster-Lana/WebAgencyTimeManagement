using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Agency.PaidTimeOffDAL.Reports;

namespace Agency.PaidTimeOffBLL.Reports
{
    public static class ReportMyPTORequests
    {
        public static object[] Select(int entUserAccountId)
        {
            return ReportMyPTORequestsData.Select(typeof(PTORequestEO).AssemblyQualifiedName, entUserAccountId).ToArray();
        }
    }
}
