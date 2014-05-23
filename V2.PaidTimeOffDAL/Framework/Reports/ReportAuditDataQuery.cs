using Agency.Common;

namespace Agency.PaidTimeOffDAL.Framework.Reports
{
    [QueryField("ObjectName", "Object Name", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    [QueryField("RecordId", "Record Id", QueryFieldAttribute.QueryFieldTypeEnum.Number)]
    [QueryField("PropertyName", "Property Name", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    [QueryField("AuditType", "AuditType", QueryFieldAttribute.QueryFieldTypeEnum.Number)]
    [QueryField("CAST(ENTAudit.InsertDate AS smalldatetime)", "Audit Date", QueryFieldAttribute.QueryFieldTypeEnum.Date)]
    [QueryField("ENTAudit.LastName + ', ' + ENTAudit.FirstName", "User", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    public class ReportAuditQueryData : ENTBaseQueryData<ReportAuditResult>
    {
        protected override string SelectClause()
        {
            return "SELECT ENTAuditId, ObjectName, RecordId, PropertyName, OldValue, NewValue, AuditType, ENTAudit.InsertDate, FirstName, LastName ";
        }

        protected override string FromClause()
        {
            return "FROM ENTAudit " +
             "INNER JOIN ENTUserAccount ON ENTAudit.InsertENTUserAccountId = ENTUserAccount.ENTUserAccountId ";
        }
    }
}
