using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffDAL.Reports
{
    public static class ReportMyPTORequestsData
    {
        public static List<ReportMyPTORequestsResult> Select(string entWorkflowObjectName, int entUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ReportMyPTORequests(entWorkflowObjectName, entUserAccountId).ToList();
            }
        }
    }
}
