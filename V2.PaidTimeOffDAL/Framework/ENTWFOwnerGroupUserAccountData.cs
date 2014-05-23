using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFOwnerGroupUserAccountData : ENTBaseData<ENTWFOwnerGroupUserAccount>
    {
        #region Overrides

        public override List<ENTWFOwnerGroupUserAccount> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFOwnerGroupUserAccountSelectAll().ToList();
            }
        }

        public override ENTWFOwnerGroupUserAccount Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTWFOwnerGroupUserAccount = db.ENTWFOwnerGroupUserAccountSelectById(id);

                if (eNTWFOwnerGroupUserAccount != null)
                {
                    return eNTWFOwnerGroupUserAccount.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFOwnerGroupUserAccountDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWFOwnerGroupId, int eNTUserAccountId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWFOwnerGroupId, eNTUserAccountId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWFOwnerGroupId, int eNTUserAccountId, int insertENTUserAccountId)
        {
            int? eNTWFOwnerGroupUserAccountId = 0;

            db.ENTWFOwnerGroupUserAccountInsert(ref eNTWFOwnerGroupUserAccountId, eNTWFOwnerGroupId, eNTUserAccountId, insertENTUserAccountId);

            return Convert.ToInt32(eNTWFOwnerGroupUserAccountId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFOwnerGroupUserAccountId, int eNTWFOwnerGroupId, int eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFOwnerGroupUserAccountId, eNTWFOwnerGroupId, eNTUserAccountId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFOwnerGroupUserAccountId, int eNTWFOwnerGroupId, int eNTUserAccountId, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTWFOwnerGroupUserAccountUpdate(eNTWFOwnerGroupUserAccountId, eNTWFOwnerGroupId, eNTUserAccountId, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        #region Custom Select

        public List<ENTWFOwnerGroupUserAccount> SelectByENTWFOwnerGroupId(int entWFOwnerGroupId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFOwnerGroupUserAccountSelectByENTWFOwnerGroupId(entWFOwnerGroupId).ToList();
            }
        }

        #endregion Custom Select
    }
}