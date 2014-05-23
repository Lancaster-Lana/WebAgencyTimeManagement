using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTAuditObjectPropertyData : ENTBaseData<ENTAuditObjectProperty>
    {
        #region Overrides

        public override List<ENTAuditObjectProperty> Select()
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext())
            {
                return db.ENTAuditObjectPropertySelectAll().ToList();
            }
        }

        public override ENTAuditObjectProperty Select(int id)
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext())
            {
                return db.ENTAuditObjectPropertySelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTAuditObjectPropertyDelete(id);
        }
                
        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTAuditObjectId, string propertyName, int insertENTUserAccountId)
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTAuditObjectId, propertyName, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTAuditObjectId, string propertyName, int insertENTUserAccountId)
        {
            Nullable<int> eNTAuditObjectPropertyId = 0;

            db.ENTAuditObjectPropertyInsert(ref eNTAuditObjectPropertyId, eNTAuditObjectId, propertyName, insertENTUserAccountId);

            return Convert.ToInt32(eNTAuditObjectPropertyId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTAuditObjectPropertyId, int eNTAuditObjectId, string propertyName, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTAuditObjectPropertyId, eNTAuditObjectId, propertyName, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTAuditObjectPropertyId, int eNTAuditObjectId, string propertyName, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTAuditObjectPropertyUpdate(eNTAuditObjectPropertyId, eNTAuditObjectId, propertyName, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public void DeleteByENTAuditObjectId(HRPaidTimeOffDataContext db, int entAuditObjectId)
        {
            db.ENTAuditObjectPropertyDeleteByENTAuditObjectId(entAuditObjectId);
        }

        public List<ENTAuditObjectProperty> SelectByENTAuditObjectId(int entAuditObjectId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return SelectByENTAuditObjectId(db, entAuditObjectId);
            }
        }

        public List<ENTAuditObjectProperty> SelectByENTAuditObjectId(HRPaidTimeOffDataContext db, int entAuditObjectId)
        {
            return db.ENTAuditObjectPropertySelectByENTAuditObjectId(entAuditObjectId).ToList();
        }
    }
}