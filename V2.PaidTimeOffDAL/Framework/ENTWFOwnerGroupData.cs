using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFOwnerGroupData : ENTBaseData<ENTWFOwnerGroup>
    {
        #region Overrides

        public override List<ENTWFOwnerGroup> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFOwnerGroupSelectAll().ToList();
            }
        }

        public override ENTWFOwnerGroup Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTWFOwnerGroup = db.ENTWFOwnerGroupSelectById(id);

                if (eNTWFOwnerGroup != null)
                {
                    return eNTWFOwnerGroup.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFOwnerGroupDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWorkflowId, string ownerGroupName, Nullable<int> defaultENTUserAccountId, bool isDefaultSameAsLast, string description, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWorkflowId, ownerGroupName, defaultENTUserAccountId, isDefaultSameAsLast, description, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWorkflowId, string ownerGroupName, Nullable<int> defaultENTUserAccountId, bool isDefaultSameAsLast, string description, int insertENTUserAccountId)
        {
            int? eNTWFOwnerGroupId = 0;

            if (defaultENTUserAccountId == 0)
            {
                defaultENTUserAccountId = null;
            }
            db.ENTWFOwnerGroupInsert(ref eNTWFOwnerGroupId, eNTWorkflowId, ownerGroupName, defaultENTUserAccountId, isDefaultSameAsLast, description, insertENTUserAccountId);
            return Convert.ToInt32(eNTWFOwnerGroupId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFOwnerGroupId, int eNTWorkflowId, string ownerGroupName, int? defaultENTUserAccountId, bool isDefaultSameAsLast, string description, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFOwnerGroupId, eNTWorkflowId, ownerGroupName, defaultENTUserAccountId, isDefaultSameAsLast, description, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFOwnerGroupId, int eNTWorkflowId, string ownerGroupName, int? defaultENTUserAccountId, bool isDefaultSameAsLast, string description, int updateENTUserAccountId, Binary version)
        {
            if (defaultENTUserAccountId == 0)
            {
                defaultENTUserAccountId = null;
            }

            int rowsAffected = db.ENTWFOwnerGroupUpdate(eNTWFOwnerGroupId, eNTWorkflowId, ownerGroupName, defaultENTUserAccountId, isDefaultSameAsLast, description, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public bool IsNameUnique(HRPaidTimeOffDataContext db, string ownerGroupName, int entWorkflowId, int entWFOwnerGroupId)
        {
            var result = db.ENTWFOwnerGroupSelectCountByNameWorkflowId(entWFOwnerGroupId, entWorkflowId, ownerGroupName);

            return (result.Single().CountOfNames == 0);
        }

        public bool IsAssociatedWithState(HRPaidTimeOffDataContext db, int entWFOwnerGroupId)
        {
            var result = db.ENTWFStateSelectCountByENTWFOwnerGroupId(entWFOwnerGroupId);

            return (result.Single().CountOfStates > 0);
        }

        public List<ENTWFOwnerGroup> SelectByENTWorkflowId(int entWorkflowId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFOwnerGroupSelectByENTWorkflowId(entWorkflowId).ToList();
            }
        }
    }
}