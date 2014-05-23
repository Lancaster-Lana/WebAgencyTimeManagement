using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTNotificationENTWFStateEO

    [Serializable]
    public class ENTNotificationENTWFStateEO : ENTBaseEO
    {

        #region Properties

        public int ENTNotificationENTUserAccountId { get; set; }
        public int ENTWFStateId { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTNotificationENTWFState eNTNotificationENTWFState = new ENTNotificationENTWFStateData().Select(id);
            MapEntityToProperties(eNTNotificationENTWFState);
            return eNTNotificationENTWFState != null;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTNotificationENTWFState eNTNotificationENTWFState = (ENTNotificationENTWFState)entity;

            ID = eNTNotificationENTWFState.ENTNotificationENTWFStateId;
            ENTNotificationENTUserAccountId = eNTNotificationENTWFState.ENTNotificationENTUserAccountId;
            ENTWFStateId = eNTNotificationENTWFState.ENTWFStateId;
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
                        ID = new ENTNotificationENTWFStateData().Insert(db, ENTNotificationENTUserAccountId, ENTWFStateId, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTNotificationENTWFStateData().Update(db, ID, ENTNotificationENTUserAccountId, ENTWFStateId, userAccountId, Version))
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
            //throw new NotImplementedException();
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTNotificationENTWFStateData().Delete(db, ID);
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
            throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        #endregion Overrides
    }

    #endregion ENTNotificationENTWFStateEO

    #region ENTNotificationENTWFStateEOList

    [Serializable]
    public class ENTNotificationENTWFStateEOList : ENTBaseEOList<ENTNotificationENTWFStateEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTNotificationENTWFStateData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTNotificationENTWFState> eNTNotificationENTWFStates)
        {
            if (eNTNotificationENTWFStates.Count > 0)
            {
                foreach (ENTNotificationENTWFState eNTNotificationENTWFState in eNTNotificationENTWFStates)
                {
                    ENTNotificationENTWFStateEO newENTNotificationENTWFStateEO = new ENTNotificationENTWFStateEO();
                    newENTNotificationENTWFStateEO.MapEntityToProperties(eNTNotificationENTWFState);
                    this.Add(newENTNotificationENTWFStateEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        internal void Load(int entNotificationENTUserAccountId)
        {
            LoadFromList(new ENTNotificationENTWFStateData().SelectByENTNotificationENTUserAccountId(entNotificationENTUserAccountId));
        }

        internal void Delete(HRPaidTimeOffDataContext db, int entNotificationENTUserAccountId)
        {
            new ENTNotificationENTWFStateData().DeleteByENTNotificationENTUserAccountId(db, entNotificationENTUserAccountId);
        }                
    }

    #endregion ENTNotificationENTWFStateEOList
}
