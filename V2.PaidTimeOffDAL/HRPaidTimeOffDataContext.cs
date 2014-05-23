using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace Agency.PaidTimeOffDAL
{
    public partial class HRPaidTimeOffDataContext
    {
        [Function(Name = "dbo.ENTWFTransitionSelectByFromStateId")]
        public ISingleResult<ENTWFTransition> ENTWFTransitionSelectByFromStateId([Parameter(Name = "FromENTWFStateId", DbType = "Int")] System.Nullable<int> fromENTWFStateId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fromENTWFStateId);
            return ((ISingleResult<ENTWFTransition>)(result.ReturnValue));
        }
    }
}
