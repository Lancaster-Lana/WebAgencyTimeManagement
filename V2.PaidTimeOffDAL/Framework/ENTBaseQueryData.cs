using System;
using System.Collections.Generic;
using System.Linq;
using Agency.Common;

namespace Agency.PaidTimeOffDAL.Framework
{
    public abstract class ENTBaseQueryData<T>
    {
        protected abstract string SelectClause();

        protected abstract string FromClause();

        public List<LookupData> GetLookup(string lookupFieldName, string valueField)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ExecuteQuery<LookupData>("SELECT DISTINCT " + lookupFieldName + " AS Text, " + valueField + " AS Value " +
                                              FromClause(), new Object[] { }).ToList();
            }
        }

        public List<T> Run(string whereClause)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ExecuteQuery<T>(SelectClause() + FromClause() + whereClause, new Object[] { }).ToList();
            }
        }
    }
}
