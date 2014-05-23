using System.Collections.Generic;
using System.Linq;

namespace Agency.PaidTimeOffDAL.Framework.Reports
{
    public static class ReportENTRoleData
    {
        public static List<ReportRoleCapabilityResult> SelectCapabilities()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ReportRoleCapability().ToList();
            }
        }

        public static List<ReportRoleUserAccountResult> SelectUserAccounts()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ReportRoleUserAccount().ToList();
            }
        }
    }
}
