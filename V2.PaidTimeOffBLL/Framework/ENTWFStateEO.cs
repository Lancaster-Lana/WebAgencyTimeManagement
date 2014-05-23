using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFStateEO

    [Serializable]
    public class ENTWFStateEO : ENTBaseEO
    {
        #region Constructor

        public ENTWFStateEO()
        {
            ENTWFStateProperties = new ENTWFStatePropertyEOList();
        }

        #endregion Constructor

        #region Properties

        public int ENTWorkflowId { get; set; }
        public string StateName { get; set; }
        public string Description { get; set; }
        public Nullable<int> ENTWFOwnerGroupId { get; set; }
        public bool IsOwnerSubmitter { get; set; }
        public ENTWFStatePropertyEOList ENTWFStateProperties { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTWFState eNTWFState = new ENTWFStateData().Select(id);
            MapEntityToProperties(eNTWFState);

            return eNTWFState != null;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTWFState eNTWFState = (ENTWFState)entity;

            ID = eNTWFState.ENTWFStateId;
            ENTWorkflowId = eNTWFState.ENTWorkflowId;
            StateName = eNTWFState.StateName;
            Description = eNTWFState.Description;
            IsOwnerSubmitter = eNTWFState.IsOwnerSubmitter;
            ENTWFOwnerGroupId = eNTWFState.ENTWFOwnerGroupId;
            ENTWFStateProperties.Load(ID);
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
                        ID = new ENTWFStateData().Insert(db, ENTWorkflowId, StateName, Description, ENTWFOwnerGroupId, IsOwnerSubmitter, userAccountId);

                        //Update the ID on all the ENTWFStateProperty objects
                        foreach (ENTWFStatePropertyEO stateProperty in ENTWFStateProperties)
                        {
                            stateProperty.ENTWFStateId = ID;
                        }
                    }
                    else
                    {
                        //Update
                        if (!new ENTWFStateData().Update(db, ID, ENTWorkflowId, StateName, Description, ENTWFOwnerGroupId, IsOwnerSubmitter, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    //Delete all the existing ENTWFStateProperty records
                    ENTWFStateProperties.Delete(db, ID);

                    //Add the records that were chosen on the screen.
                    if (ENTWFStateProperties.Save(db, ref validationErrors, userAccountId))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
            if (StateName.Trim() == "")
            {
                validationErrors.Add("The state name is required.");
            }
            else
            {
                ////The windows account name must be unique.
                if (new ENTWFStateData().IsDuplicateStateName(db, ID, StateName))
                {
                    validationErrors.Add("The state name must be unique.");
                }
            }

            if (ENTWorkflowId == 0)
            {
                validationErrors.Add("Please select a workflow to associate with this state.");
            }

            if ((ENTWFOwnerGroupId == 0) && (IsOwnerSubmitter == false))
            {
                validationErrors.Add("Please select the group that owns the issue while in this state.");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWFStateData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //throw new NotImplementedException();
            //Check if associates with a transition
                        
        }

        public override void Init()
        {
            //Nothing to default
        }

        protected override string GetDisplayText()
        {
            return StateName;
        }

        #endregion Overrides
    }

    #endregion ENTWFStateEO

    #region ENTWFStateEOList

    [Serializable]
    public class ENTWFStateEOList : ENTBaseEOList<ENTWFStateEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFStateData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFState> eNTWFStates)
        {
            if (eNTWFStates.Count > 0)
            {
                foreach (ENTWFState eNTWFState in eNTWFStates)
                {
                    ENTWFStateEO newENTWFStateEO = new ENTWFStateEO();
                    newENTWFStateEO.MapEntityToProperties(eNTWFState);
                    this.Add(newENTWFStateEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public void Load(int entWorkflowId)
        {
            LoadFromList(new ENTWFStateData().SelectByENTWorkflowId(entWorkflowId));
        }
    }

    #endregion ENTWFStateEOList
}
