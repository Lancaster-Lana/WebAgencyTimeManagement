using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTNotificationENTWFStateData : ENTBaseData<ENTNotificationENTWFState>
    {
        #region Overrides

        public override List<ENTNotificationENTWFState> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTNotificationENTWFStateSelectAll().ToList();
            }
        }

        public override ENTNotificationENTWFState Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTNotificationENTWFStateSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTNotificationENTWFStateDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTNotificationENTUserAccountId, int eNTWFStateId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTNotificationENTUserAccountId, eNTWFStateId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTNotificationENTUserAccountId, int eNTWFStateId, int insertENTUserAccountId)
        {
            int? eNTNotificationENTWFStateId = 0;

            db.ENTNotificationENTWFStateInsert(ref eNTNotificationENTWFStateId, eNTNotificationENTUserAccountId, eNTWFStateId, insertENTUserAccountId);

            return Convert.ToInt32(eNTNotificationENTWFStateId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTNotificationENTWFStateId, int eNTNotificationENTUserAccountId, int eNTWFStateId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTNotificationENTWFStateId, eNTNotificationENTUserAccountId, eNTWFStateId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTNotificationENTWFStateId, int eNTNotificationENTUserAccountId, int eNTWFStateId, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTNotificationENTWFStateUpdate(eNTNotificationENTWFStateId, eNTNotificationENTUserAccountId, eNTWFStateId, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<ENTNotificationENTWFState> SelectByENTNotificationENTUserAccountId(int entNotificationENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTNotificationENTWFStateSelectByENTNotificationENTUserAccountId(entNotificationENTUserAccountId).ToList();
            }
        }

        public void DeleteByENTNotificationENTUserAccountId(HRPaidTimeOffDataContext db, int entNotificationENTUserAccountId)
        {
            db.ENTNotificationENTWFStateDeleteByENTNotificationENTUserAccountId(entNotificationENTUserAccountId);
        }
    }
}