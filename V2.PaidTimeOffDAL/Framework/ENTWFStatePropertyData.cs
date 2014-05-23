using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFStatePropertyData : ENTBaseData<ENTWFStateProperty>
    {
        #region Overrides

        public override List<ENTWFStateProperty> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFStatePropertySelectAll().ToList();
            }
        }

        public override ENTWFStateProperty Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTWFStateProperty = db.ENTWFStatePropertySelectById(id);

                if (eNTWFStateProperty != null)
                {
                    return eNTWFStateProperty.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFStatePropertyDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWFStateId, string propertyName, bool required, bool readOnly, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWFStateId, propertyName, required, readOnly, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWFStateId, string propertyName, bool required, bool readOnly, int insertENTUserAccountId)
        {
            int? eNTWFStatePropertyId = 0;
            db.ENTWFStatePropertyInsert(ref eNTWFStatePropertyId, eNTWFStateId, propertyName, required, readOnly, insertENTUserAccountId);
            return Convert.ToInt32(eNTWFStatePropertyId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFStatePropertyId, int eNTWFStateId, string propertyName, bool required, bool readOnly, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFStatePropertyId, eNTWFStateId, propertyName, required, readOnly, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFStatePropertyId, int eNTWFStateId, string propertyName, bool required, bool readOnly, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTWFStatePropertyUpdate(eNTWFStatePropertyId, eNTWFStateId, propertyName, required, readOnly, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<ENTWFStateProperty> SelectByENTWFStateId(HRPaidTimeOffDataContext db, int entWFStateId)
        {
            return db.ENTWFStatePropertySelectByENTWFStateId(entWFStateId).ToList();
        }

        public void DeleteByENTWFStateId(HRPaidTimeOffDataContext db, int entWFStateId)
        {
            db.ENTWFStatePropertyDeleteByENTWFStateId(entWFStateId);
        }
    }
}