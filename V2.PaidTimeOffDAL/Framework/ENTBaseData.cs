using System;
using System.Collections.Generic;
using System.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public abstract class ENTBaseData<T> where T : IENTBaseEntity
    {
/*
        private const string ConnectionStringKey = "HRPaidTimeOffConnectionString";
*/

        public abstract List<T> Select();

        public abstract T Select(int id);

        public abstract void Delete(HRPaidTimeOffDataContext db, int id);

        public void Delete(string connectionString, int id)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                Delete(db, id);
            }
        }

        #region Common Functions

        protected static bool IsDuplicate(HRPaidTimeOffDataContext db, string tableName, string fieldName,
            string fieldNameId, string value, int id)
        {            
            string sql =
                "SELECT COUNT(" + fieldNameId + ") AS DuplicateCount " +
                  "FROM " + tableName +
                " WHERE " + fieldName + " = {0}" +
                  " AND " + fieldNameId + " <> {1}";

            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { value, id });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        protected static bool IsDuplicate(HRPaidTimeOffDataContext db, string tableName, string fieldName,
            string fieldNameId, DateTime value, int id)
        {
            string sql =
                "SELECT COUNT(" + fieldNameId + ") AS DuplicateCount " +
                  "FROM " + tableName +
                " WHERE " + fieldName + " = {0}" +
                  " AND " + fieldNameId + " <> {1}";

            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { value, id });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        #endregion Common Functions

    }
}
