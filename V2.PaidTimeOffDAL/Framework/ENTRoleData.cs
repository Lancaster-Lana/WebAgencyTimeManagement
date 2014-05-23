using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTRoleData : ENTBaseData<ENTRole>
    {
        #region Overrides

        public override List<ENTRole> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleSelectAll().ToList();
            }
        }

        public override ENTRole Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTRoleDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string roleName, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, roleName, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string roleName, int insertENTUserAccountId)
        {
            int? entRoleId = 0;
            db.ENTRoleInsert(ref entRoleId, roleName, insertENTUserAccountId);
            return Convert.ToInt32(entRoleId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int entRoleId, string roleName, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, entRoleId, roleName, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int entRoleId, string roleName, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTRoleUpdate(entRoleId, roleName, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        #region Utility Methods

        public bool IsDuplicateRoleName(HRPaidTimeOffDataContext db, int entRoleId, string roleName)
        {
            return IsDuplicate(db, "ENTRole", "RoleName", "ENTRoleID", roleName, entRoleId);
        }

        public List<ENTRole> SelectByENTUserAccountId(int entUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleSelectByENTUserAccountId(entUserAccountId).ToList();
            }
        }

        #endregion Utility Methods
    }
}
