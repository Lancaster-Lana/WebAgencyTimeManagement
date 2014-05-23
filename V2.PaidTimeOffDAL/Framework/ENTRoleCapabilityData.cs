using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTRoleCapabilityData : ENTBaseData<ENTRoleCapability>
    {
        #region Overrides

        public override List<ENTRoleCapability> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleCapabilitySelectAll().ToList();
            }
        }

        public override ENTRoleCapability Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleCapabilitySelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTRoleCapabilityDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int entRoleId, int entCapabilityId, byte accessFlag, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, entRoleId, entCapabilityId, accessFlag, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int entRoleId, int entCapabilityId, byte accessFlag, int insertENTUserAccountId)
        {
            int? entRoleCapabilityId = 0;

            db.ENTRoleCapabilityInsert(ref entRoleCapabilityId, entRoleId, entCapabilityId, accessFlag, insertENTUserAccountId);

            return Convert.ToInt32(entRoleCapabilityId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int entRoleCapabilityId, int entRoleId, int entCapabilityId, byte accessFlag, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, entRoleCapabilityId, entRoleId, entCapabilityId, accessFlag, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int entRoleCapabilityId, int entRoleId, int entCapabilityId, byte accessFlag, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTRoleCapabilityUpdate(entRoleCapabilityId, entRoleId, entCapabilityId, accessFlag, updateENTUserAccountId, version);

            return rowsAffected == 1;
        }

        #endregion Update

        #region Custom Select

        public List<ENTRoleCapability> SelectByENTRoleId(int entRoleId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTRoleCapabilitySelectByENTRoleId(entRoleId).ToList();
            }
        }

        #endregion Custom Select
    }
}
