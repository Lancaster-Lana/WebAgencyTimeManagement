using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFStateData : ENTBaseData<ENTWFState>
    {
        #region Overrides

        public override List<ENTWFState> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFStateSelectAll().ToList();
            }
        }

        public override ENTWFState Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTWFState = db.ENTWFStateSelectById(id);

                if (eNTWFState != null)
                {
                    return eNTWFState.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFStateDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWorkflowId, string stateName, string description, Nullable<int> eNTWFOwnerGroupId, bool isOwnerSubmitter, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWorkflowId, stateName, description, eNTWFOwnerGroupId, isOwnerSubmitter, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWorkflowId, string stateName, string description, Nullable<int> eNTWFOwnerGroupId, bool isOwnerSubmitter, int insertENTUserAccountId)
        {
            int? eNTWFStateId = 0;

            if (eNTWFOwnerGroupId == 0)
            {
                eNTWFOwnerGroupId = null;
            }

            db.ENTWFStateInsert(ref eNTWFStateId, eNTWorkflowId, stateName, description, eNTWFOwnerGroupId, isOwnerSubmitter, insertENTUserAccountId);

            return Convert.ToInt32(eNTWFStateId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFStateId, int eNTWorkflowId, string stateName, string description, Nullable<int> eNTWFOwnerGroupId, bool isOwnerSubmitter, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFStateId, eNTWorkflowId, stateName, description, eNTWFOwnerGroupId, isOwnerSubmitter, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFStateId, int eNTWorkflowId, string stateName, string description, Nullable<int> eNTWFOwnerGroupId, bool isOwnerSubmitter, int updateENTUserAccountId, Binary version)
        {
            if (eNTWFOwnerGroupId == 0)
            {
                eNTWFOwnerGroupId = null;
            }

            int rowsAffected = db.ENTWFStateUpdate(eNTWFStateId, eNTWorkflowId, stateName, description, eNTWFOwnerGroupId, isOwnerSubmitter, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public bool IsDuplicateStateName(HRPaidTimeOffDataContext db, int wfStateId, string stateName)
        {
            return IsDuplicate(db, "ENTWFState", "StateName", "ENTWFStateId", stateName, wfStateId);
        }

        public List<ENTWFState> SelectByENTWorkflowId(int entWorkflowId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFStateSelectByENTWorkflowId(entWorkflowId).ToList();
            }
        }
    }
}