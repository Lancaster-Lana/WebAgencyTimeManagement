using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFItemOwnerData : ENTBaseData<ENTWFItemOwner>
    {
        #region Overrides

        public override List<ENTWFItemOwner> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFItemOwnerSelectAll().ToList();
            }
        }

        public override ENTWFItemOwner Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTWFItemOwner = db.ENTWFItemOwnerSelectById(id);

                if (eNTWFItemOwner != null)
                {
                    return eNTWFItemOwner.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFItemOwnerDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWFItemId, int eNTWFOwnerGroupId, Nullable<int> eNTUserAccountId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWFItemId, eNTWFOwnerGroupId, eNTUserAccountId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWFItemId, int eNTWFOwnerGroupId, Nullable<int> eNTUserAccountId, int insertENTUserAccountId)
        {
            int? eNTWFItemOwnerId = 0;

            if (eNTUserAccountId == 0)
            {
                eNTUserAccountId = null;
            }

            db.ENTWFItemOwnerInsert(ref eNTWFItemOwnerId, eNTWFItemId, eNTWFOwnerGroupId, eNTUserAccountId, insertENTUserAccountId);

            return Convert.ToInt32(eNTWFItemOwnerId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFItemOwnerId, int eNTWFItemId, int eNTWFOwnerGroupId, Nullable<int> eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFItemOwnerId, eNTWFItemId, eNTWFOwnerGroupId, eNTUserAccountId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFItemOwnerId, int eNTWFItemId, int eNTWFOwnerGroupId, Nullable<int> eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            if (eNTUserAccountId == 0)
            {
                eNTUserAccountId = null;
            }

            int rowsAffected = db.ENTWFItemOwnerUpdate(eNTWFItemOwnerId, eNTWFItemId, eNTWFOwnerGroupId, eNTUserAccountId, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<ENTWFItemOwner> SelectByENTWFItemId(int entWFItemId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFItemOwnerSelectByENTWFItemId(entWFItemId).ToList();
            }
        }

        public ENTWFItemOwnerSelectLastUserByGroupIdResult SelectLastUserByGroupId(int entWFOwnerGroupId, int entUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFItemOwnerSelectLastUserByGroupId(entWFOwnerGroupId, entUserAccountId).SingleOrDefault();
            }
        }
    }
}