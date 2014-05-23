using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFTransitionEO

    [Serializable]
    public class ENTWFTransitionEO : ENTBaseEO
    {

        #region Properties

        public int ENTWorkflowId { get; set; }
        public string TransitionName { get; set; }
        public Nullable<int> FromENTWFStateId { get; set; }
        public string FromStateName { get; private set; }
        public int ToENTWFStateId { get; set; }
        public string ToStateName { get; private set; }
        public string PostTransitionMethodName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTWFTransition eNTWFTransition = new ENTWFTransitionData().Select(id);
            MapEntityToProperties(eNTWFTransition);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTWFTransition = (ENTWFTransition)entity;

            ID = eNTWFTransition.ENTWFTransitionId;
            ENTWorkflowId = eNTWFTransition.ENTWorkflowId;
            TransitionName = eNTWFTransition.TransitionName;
            FromENTWFStateId = eNTWFTransition.FromENTWFStateId;
            FromStateName = eNTWFTransition.FromStateName;
            ToENTWFStateId = eNTWFTransition.ToENTWFStateId;
            ToStateName = eNTWFTransition.ToStateName;
            PostTransitionMethodName = eNTWFTransition.PostTransitionMethodName;
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
                        ID = new ENTWFTransitionData().Insert(db, ENTWorkflowId, TransitionName, FromENTWFStateId, ToENTWFStateId, PostTransitionMethodName, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTWFTransitionData().Update(db, ID, ENTWorkflowId, TransitionName, FromENTWFStateId, ToENTWFStateId, PostTransitionMethodName, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    return true;

                }
                //Didn't pass validation.
                return false;
            }
            throw new Exception("DBAction not Save.");
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            if (TransitionName.Trim() == "")
            {
                validationErrors.Add("The transition name is required.");
            }
            
            if (ENTWorkflowId == 0)
            {
                validationErrors.Add("Please select a workflow.");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWFTransitionData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            
        }

        public override void Init()
        {
            
        }

        protected override string GetDisplayText()
        {
            return TransitionName;    
        }

        #endregion Overrides
    }

    #endregion ENTWFTransitionEO

    #region ENTWFTransitionEOList

    [Serializable]
    public class ENTWFTransitionEOList : ENTBaseEOList<ENTWFTransitionEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFTransitionData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFTransition> eNTWFTransitions)
        {
            if (eNTWFTransitions.Count > 0)
            {
                foreach (var eNTWFTransition in eNTWFTransitions)
                {
                    var newENTWFTransitionEO = new ENTWFTransitionEO();
                    newENTWFTransitionEO.MapEntityToProperties(eNTWFTransition);
                    Add(newENTWFTransitionEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public void Load(int fromStateId)
        {
            LoadFromList(new ENTWFTransitionData().SelectByFromStateId(fromStateId));
        }

        internal ENTWFTransitionEO Get(int entWFTransitionId)
        {
            return this.Single(t => t.ID == entWFTransitionId);
        }
    }

    #endregion ENTWFTransitionEOList
}
