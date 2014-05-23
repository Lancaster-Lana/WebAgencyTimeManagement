using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWorkflowEO

    [Serializable]
    public class ENTWorkflowEO : ENTBaseEO
    {

        #region Properties

        public string WorkflowName { get; set; }
        public string ENTWorkflowObjectName { get; set; }
        
        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTWorkflow eNTWorkflow = new ENTWorkflowData().Select(id);
            MapEntityToProperties(eNTWorkflow);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTWorkflow eNTWorkflow = (ENTWorkflow)entity;

            ID = eNTWorkflow.ENTWorkflowId;
            WorkflowName = eNTWorkflow.WorkflowName;
            ENTWorkflowObjectName = eNTWorkflow.ENTWorkflowObjectName;        
        }

        public override bool Save(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                //Validate the object
                Validate(db, ref validationErrors);

                //Check if there were any validation errors
                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        //Add
                        ID = new ENTWorkflowData().Insert(db, WorkflowName, ENTWorkflowObjectName, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTWorkflowData().Update(db, ID, WorkflowName, ENTWorkflowObjectName, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    return true;

                }
                else
                {
                    //Didn't pass validation.
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not Save.");
            }
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            if (WorkflowName.Trim() == "")
            {
                validationErrors.Add("The workflow name is required.");
            }
            else
            {
                //The name must be unique.
                if (new ENTWorkflowData().IsDuplicateWorkflowName(db, ID, WorkflowName))
                {
                    validationErrors.Add("The name must be unique.");
                }
            }

            //The object name is required
            if (ENTWorkflowObjectName.Trim() == "")
            {
                validationErrors.Add("The class name is required.");
            }
            else
            {
                //The object name must be unique
                if (new ENTWorkflowData().IsDuplicateObjectName(db, ID, ENTWorkflowObjectName))
                {
                    validationErrors.Add("This class already has a workflow associated with it.");
                }
            }

        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWorkflowData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //Nothing to validate.
            if (new ENTWorkflowData().IsWorkflowAssociatedWithItem(db, ID))
            {
                validationErrors.Add("The workflow can not be deleted because there are items associated with this workflow.");
            }
        }

        public override void Init()
        {
            
        }

        protected override string GetDisplayText()
        {
            return WorkflowName;
        }

        #endregion Overrides
                
        public bool LoadByObjectName(string objectName)
        {
            ENTWorkflow eNTWorkflow = new ENTWorkflowData().SelectByObjectName(objectName);
            MapEntityToProperties(eNTWorkflow);
            return eNTWorkflow != null;        
        }
    }

    #endregion ENTWorkflowEO

    #region ENTWorkflowEOList

    [Serializable]
    public class ENTWorkflowEOList : ENTBaseEOList<ENTWorkflowEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWorkflowData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWorkflow> eNTWorkflows)
        {
            if (eNTWorkflows.Count > 0)
            {
                foreach (ENTWorkflow eNTWorkflow in eNTWorkflows)
                {
                    var newENTWorkflowEO = new ENTWorkflowEO();
                    newENTWorkflowEO.MapEntityToProperties(eNTWorkflow);
                    Add(newENTWorkflowEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion ENTWorkflowEOList
}
