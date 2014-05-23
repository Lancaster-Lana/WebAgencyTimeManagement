using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWFTransitionData : ENTBaseData<ENTWFTransition>
    {
        #region Overrides

        public override List<ENTWFTransition> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWFTransitionSelectAll().ToList();
            }
        }

        public override ENTWFTransition Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                ISingleResult<ENTWFTransition> eNTWFTransition = db.ENTWFTransitionSelectById(id);

                if (eNTWFTransition != null)
                {
                    return eNTWFTransition.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWFTransitionDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTWorkflowId, string transitionName, Nullable<int> fromENTWFStateId, int toENTWFStateId, string postTransitionMethodName, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTWorkflowId, transitionName, fromENTWFStateId, toENTWFStateId, postTransitionMethodName, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTWorkflowId, string transitionName, Nullable<int> fromENTWFStateId, int toENTWFStateId, string postTransitionMethodName, int insertENTUserAccountId)
        {
            int? eNTWFTransitionId = 0;

            db.ENTWFTransitionInsert(ref eNTWFTransitionId, eNTWorkflowId, transitionName, fromENTWFStateId, toENTWFStateId, postTransitionMethodName, insertENTUserAccountId);

            return Convert.ToInt32(eNTWFTransitionId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWFTransitionId, int eNTWorkflowId, string transitionName, Nullable<int> fromENTWFStateId, int toENTWFStateId, string postTransitionMethodName, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWFTransitionId, eNTWorkflowId, transitionName, fromENTWFStateId, toENTWFStateId, postTransitionMethodName, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWFTransitionId, int eNTWorkflowId, string transitionName, Nullable<int> fromENTWFStateId, int toENTWFStateId, string postTransitionMethodName, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTWFTransitionUpdate(eNTWFTransitionId, eNTWorkflowId, transitionName, fromENTWFStateId, toENTWFStateId, postTransitionMethodName, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<ENTWFTransition> SelectByFromStateId(int fromStateId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                int? stateId;
                if (fromStateId == 0)
                {
                    stateId = null;
                }
                else
                {
                    stateId = fromStateId;
                }

                return db.ENTWFTransitionSelectByFromStateId(stateId).ToList();
            }
        }
    }
}