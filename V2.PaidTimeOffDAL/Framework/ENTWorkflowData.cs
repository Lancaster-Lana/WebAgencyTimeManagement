using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTWorkflowData : ENTBaseData<ENTWorkflow>
    {
        #region Overrides

        public override List<ENTWorkflow> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWorkflowSelectAll().ToList();
            }
        }

        public override ENTWorkflow Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWorkflowSelectById(id).SingleOrDefault();                                
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTWorkflowDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string workflowName, string eNTWorkflowObjectName, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, workflowName, eNTWorkflowObjectName, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string workflowName, string eNTWorkflowObjectName, int insertENTUserAccountId)
        {
            int? eNTWorkflowId = 0;

            db.ENTWorkflowInsert(ref eNTWorkflowId, workflowName, eNTWorkflowObjectName, insertENTUserAccountId);

            return Convert.ToInt32(eNTWorkflowId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTWorkflowId, string workflowName, string eNTWorkflowObjectName, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTWorkflowId, workflowName, eNTWorkflowObjectName, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTWorkflowId, string workflowName, string eNTWorkflowObjectName, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTWorkflowUpdate(eNTWorkflowId, workflowName, eNTWorkflowObjectName, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        #region Utility Methods

        public bool IsDuplicateWorkflowName(HRPaidTimeOffDataContext db, int entWorkflowId, string workflowName)
        {
            return IsDuplicate(db, "ENTWorkflow", "WorkflowName", "ENTWorkflowID", workflowName, entWorkflowId);
        }

        public bool IsDuplicateObjectName(HRPaidTimeOffDataContext db, int entWorkflowId, string entWorkflowObjectName)
        {
            return IsDuplicate(db, "ENTWorkflow", "ENTWorkflowObjectName", "ENTWorkflowID", entWorkflowObjectName, entWorkflowId);
        }

        #endregion Utility Methods
                
        public ENTWorkflow SelectByObjectName(string objectName)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTWorkflowSelectByObjectName(objectName).SingleOrDefault();                                
            }
        }

        public bool IsWorkflowAssociatedWithItem(HRPaidTimeOffDataContext db, int entWorkflowId)
        {
            return (db.ENTWFItemSelectByWorkflowId(entWorkflowId).ToList()[0].CountOfWFItems > 0);
        }
    }
}