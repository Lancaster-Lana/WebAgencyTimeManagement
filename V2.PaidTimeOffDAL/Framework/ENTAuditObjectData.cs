using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTAuditObjectData : ENTBaseData<ENTAuditObject>
    {
        #region Overrides

        public override List<ENTAuditObject> Select()
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext())
            {
                return db.ENTAuditObjectSelectAll().ToList();
            }
        }

        public override ENTAuditObject Select(int id)
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext())
            {
                return db.ENTAuditObjectSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTAuditObjectDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string objectName, string objectFullyQualifiedName, int insertENTUserAccountId)
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, objectName, objectFullyQualifiedName, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string objectName, string objectFullyQualifiedName, int insertENTUserAccountId)
        {
            Nullable<int> eNTAuditObjectId = 0;

            db.ENTAuditObjectInsert(ref eNTAuditObjectId, objectName, objectFullyQualifiedName, insertENTUserAccountId);

            return Convert.ToInt32(eNTAuditObjectId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTAuditObjectId, string objectName, string objectFullyQualifiedName, int updateENTUserAccountId, Binary version)
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTAuditObjectId, objectName, objectFullyQualifiedName, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTAuditObjectId, string objectName, string objectFullyQualifiedName, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTAuditObjectUpdate(eNTAuditObjectId, objectName, objectFullyQualifiedName, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public ENTAuditObject Select(HRPaidTimeOffDataContext db, string objectName)
        {
            return db.ENTAuditObjectSelectByObjectName(objectName).SingleOrDefault();
        }        
    }
}