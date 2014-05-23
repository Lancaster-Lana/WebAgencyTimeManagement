using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFItemStateHistoryData : ENTBaseData<ENTWFItemStateHistory>
    {
        #region Overrides

        public override List<ENTWFItemStateHistory> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFItemStateHistorySelectAll().ToList();
            }
        }

        public override ENTWFItemStateHistory Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTWFItemStateHistory = db.ENTWFItemStateHistorySelectById(id);

                if (eNTWFItemStateHistory != null)
                {
                    return eNTWFItemStateHistory.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFItemStateHistoryDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWFItemId, int eNTWFStateId, int eNTUserAccountId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWFItemId, eNTWFStateId, eNTUserAccountId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWFItemId, int eNTWFStateId, int eNTUserAccountId, int insertENTUserAccountId)
        {
            int? eNTWFItemStateHistoryId = 0;

            db.ENTWFItemStateHistoryInsert(ref eNTWFItemStateHistoryId, eNTWFItemId, eNTWFStateId, eNTUserAccountId, insertENTUserAccountId);

            return Convert.ToInt32(eNTWFItemStateHistoryId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFItemStateHistoryId, int eNTWFItemId, int eNTWFStateId, int eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFItemStateHistoryId, eNTWFItemId, eNTWFStateId, eNTUserAccountId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFItemStateHistoryId, int eNTWFItemId, int eNTWFStateId, int eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTWFItemStateHistoryUpdate(eNTWFItemStateHistoryId, eNTWFItemId, eNTWFStateId, eNTUserAccountId, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<ENTWFItemStateHistory> SelectByENTWFItemId(int entWFItemId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFItemStateHistorySelectByENTWFItemId(entWFItemId).ToList();
            }
        }
    }
}