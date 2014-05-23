using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTNotificationENTUserAccountData : ENTBaseData<ENTNotificationENTUserAccount>
    {
        #region Overrides

        public override List<ENTNotificationENTUserAccount> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTNotificationENTUserAccountSelectAll().ToList();
            }
        }

        public override ENTNotificationENTUserAccount Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTNotificationENTUserAccountSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTNotificationENTUserAccountDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTNotificationId, int eNTUserAccountId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTNotificationId, eNTUserAccountId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTNotificationId, int eNTUserAccountId, int insertENTUserAccountId)
        {
            int? eNTNotificationENTUserAccountId = 0;

            db.ENTNotificationENTUserAccountInsert(ref eNTNotificationENTUserAccountId, eNTNotificationId, eNTUserAccountId, insertENTUserAccountId);

            return Convert.ToInt32(eNTNotificationENTUserAccountId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTNotificationENTUserAccountId, int eNTNotificationId, int eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTNotificationENTUserAccountId, eNTNotificationId, eNTUserAccountId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTNotificationENTUserAccountId, int eNTNotificationId, int eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTNotificationENTUserAccountUpdate(eNTNotificationENTUserAccountId, eNTNotificationId, eNTUserAccountId, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<ENTNotificationENTUserAccount> SelectByENTUserAccountId(int entUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTNotificationENTUserAccountSelectByENTUserAccountId(entUserAccountId).ToList();
            }
        }

        public List<ENTNotificationENTUserAccount> SelectByENTWFStateId(HRPaidTimeOffDataContext db, int entWFStateId, int entNotificationId)
        {
            return db.ENTNotificationENTUserAccountSelectByENTWFStateId(entWFStateId, entNotificationId).ToList();
        }
    }
}