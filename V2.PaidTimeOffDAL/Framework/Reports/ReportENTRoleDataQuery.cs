using Agency.Common;

namespace Agency.PaidTimeOffDAL.Framework.Reports
{
    [QueryField("ENTRole.RoleName", "Role", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    [QueryField("ENTRoleCapability.AccessFlag", "Access Flag", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    [QueryField("ENtCapability.CapabilityName", "Capability", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    public class ReportENTRoleDataQuery : ENTBaseQueryData<ReportRoleCapabilityResult>
    {
        protected override string SelectClause()
        {
            return "SELECT ENTRole.RoleName, ENTRoleCapability.AccessFlag, ENTCapability.CapabilityName ";
        }

        protected override string FromClause()
        {
            return "FROM ENTCapability " +
             "INNER JOIN ENTRoleCapability " +
                    "ON ENTCapability.ENTCapabilityId = ENTRoleCapability.ENTCapabilityId " +
             "INNER JOIN ENTRole " +
                    "ON ENTRoleCapability.ENTRoleId = ENTRole.ENTRoleId ";
        }
    }
}
