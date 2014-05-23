using System.Collections.Generic;
using Agency.Common;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffBLL.Framework
{
    public class ENTBaseQueryBO<T, R> 
        where T : ENTBaseQueryData<R>, new()        
    {
        public object[] GetCustomAttributes()
        {
            return typeof(T).GetCustomAttributes(false);
        }
        
        public List<LookupData> GetLookup(string lookupFieldName, string valueField)
        {
            T reportData = new T();
            return reportData.GetLookup(lookupFieldName, valueField);
        }

        public virtual object[] Select(string whereClause)
        {
            T reportData = new T();
            return (object[])(object)reportData.Run(whereClause).ToArray();
        }
    }
}
