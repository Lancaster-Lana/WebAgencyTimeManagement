using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTRoleUserAccountData : ENTBaseData<ENTRoleUserAccount>
    {
        #region Overrides

        public override List<ENTRoleUserAccount> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleUserAccountSelectAll().ToList();
            }
        }

        public override ENTRoleUserAccount Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleUserAccountSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTRoleUserAccountDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int entRoleId, int entUserAccountId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, entRoleId, entUserAccountId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int entRoleId, int entUserAccountId, int insertENTUserAccountId)
        {
            int? entRoleUserAccountId = 0;

            db.ENTRoleUserAccountInsert(ref entRoleUserAccountId, entRoleId, entUserAccountId, insertENTUserAccountId);

            return Convert.ToInt32(entRoleUserAccountId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int entRoleUserAccountId, int entRoleId, int entUserAccountId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, entRoleUserAccountId, entRoleId, entUserAccountId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int entRoleUserAccountId, int entRoleId, int entUserAccountId, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTRoleUserAccountUpdate(entRoleUserAccountId, entRoleId, entUserAccountId, updateENTUserAccountId, version);

            return rowsAffected == 1;
        }

        #endregion Update

        #region Custom Select

        public List<ENTRoleUserAccount> SelectByENTRoleId(int entRoleId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleUserAccountSelectByENTRoleId(entRoleId).ToList();
            }
        }

        /*
        public List<ENTRoleUserAccount> SelectByENTUserRoleId(int userAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleSelectByENTUserAccountId(userAccountId).ToList();
            }
        }*/

        #endregion Custom Select
    }
}
