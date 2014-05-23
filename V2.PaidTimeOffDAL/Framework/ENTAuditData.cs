using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTAuditData : ENTBaseData<ENTAudit>
    {
        #region Overrides

        public override List<ENTAudit> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTAuditSelectAll().ToList();
            }
        }

        public override ENTAudit Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTAuditSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTAuditDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string objectName, int recordId, string propertyName, string oldValue, string newValue, byte auditType, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, objectName, recordId, propertyName, oldValue, newValue, auditType, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string objectName, int recordId, string propertyName, string oldValue, string newValue, byte auditType, int insertENTUserAccountId)
        {
            int? eNTAuditId = 0;

            db.ENTAuditInsert(ref eNTAuditId, objectName, recordId, propertyName, oldValue, newValue, auditType, insertENTUserAccountId);

            return Convert.ToInt32(eNTAuditId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTAuditId, string objectName, int recordId, string propertyName, string oldValue, string newValue, byte auditType, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTAuditId, objectName, recordId, propertyName, oldValue, newValue, auditType, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTAuditId, string objectName, int recordId, string propertyName, string oldValue, string newValue, byte auditType, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTAuditUpdate(eNTAuditId, objectName, recordId, propertyName, oldValue, newValue, auditType, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

    }
}