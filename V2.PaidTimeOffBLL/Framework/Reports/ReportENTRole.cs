using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffDAL.Framework.Reports;

namespace Agency.PaidTimeOffBLL.Framework.Reports
{
    public static class ReportENTRole
    {
        public static ReportRoleCapabilityResult[] SelectCapabilities()
        {
            return ReportENTRoleData.SelectCapabilities().ToArray();
        }

        public static ReportRoleUserAccountResult[] SelectUserAccounts()
        {
            return ReportENTRoleData.SelectUserAccounts().ToArray();
        }
    }
}
