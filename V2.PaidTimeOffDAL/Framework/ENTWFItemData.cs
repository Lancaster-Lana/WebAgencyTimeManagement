using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFItemData : ENTBaseData<ENTWFItem>
    {
        #region Overrides

        public override List<ENTWFItem> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFItemSelectAll().ToList();
            }
        }

        public override ENTWFItem Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTWFItem = db.ENTWFItemSelectById(id);

                if (eNTWFItem != null)
                {
                    return eNTWFItem.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFItemDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWorkflowId, int itemId, int submitterENTUserAccountId, int currentWFStateId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWorkflowId, itemId, submitterENTUserAccountId, currentWFStateId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWorkflowId, int itemId, int submitterENTUserAccountId, int currentWFStateId, int insertENTUserAccountId)
        {
            int? eNTWFItemId = 0;

            db.ENTWFItemInsert(ref eNTWFItemId, eNTWorkflowId, itemId, submitterENTUserAccountId, currentWFStateId, insertENTUserAccountId);

            return Convert.ToInt32(eNTWFItemId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFItemId, int eNTWorkflowId, int itemId, int submitterENTUserAccountId, int currentWFStateId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFItemId, eNTWorkflowId, itemId, submitterENTUserAccountId, currentWFStateId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFItemId, int eNTWorkflowId, int itemId, int submitterENTUserAccountId, int currentWFStateId, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTWFItemUpdate(eNTWFItemId, eNTWorkflowId, itemId, submitterENTUserAccountId, currentWFStateId, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public ENTWFItem SelectByItemId(int entWorkflowId, int itemId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFItemSelectByItemId(entWorkflowId, itemId).SingleOrDefault();
            }
        }
    }
}