using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFItemStateHistoryEO

    [Serializable]
    public class ENTWFItemStateHistoryEO : ENTBaseEO
    {
        #region Properties

        public int ENTWFItemId { get; set; }
        public int ENTWFStateId { get; set; }
        public int ENTUserAccountId { get; set; }
        public string StateName { get; private set; }
        public string OwnerName { get; private set; }
        public string InsertedBy { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            var eNTWFItemStateHistory = new ENTWFItemStateHistoryData().Select(id);
            MapEntityToProperties(eNTWFItemStateHistory);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTWFItemStateHistory = (ENTWFItemStateHistory)entity;

            ID = eNTWFItemStateHistory.ENTWFItemStateHistoryId;
            ENTWFItemId = eNTWFItemStateHistory.ENTWFItemId;
            ENTWFStateId = eNTWFItemStateHistory.ENTWFStateId;
            ENTUserAccountId = eNTWFItemStateHistory.ENTUserAccountId;

            StateName = eNTWFItemStateHistory.StateName;
            OwnerName = eNTWFItemStateHistory.OwnerName;
            InsertedBy = eNTWFItemStateHistory.InsertedBy;
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
                        ID = new ENTWFItemStateHistoryData().Insert(db, ENTWFItemId, ENTWFStateId, ENTUserAccountId, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTWFItemStateHistoryData().Update(db, ID, ENTWFItemId, ENTWFStateId, ENTUserAccountId, userAccountId, Version))
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
            
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWFItemStateHistoryData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();        }

        #endregion Overrides
    }

    #endregion ENTWFItemStateHistoryEO

    #region ENTWFItemStateHistoryEOList

    [Serializable]
    public class ENTWFItemStateHistoryEOList : ENTBaseEOList<ENTWFItemStateHistoryEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFItemStateHistoryData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFItemStateHistory> eNTWFItemStateHistorys)
        {
            if (eNTWFItemStateHistorys.Count > 0)
            {
                foreach (var eNTWFItemStateHistory in eNTWFItemStateHistorys)
                {
                    var newENTWFItemStateHistoryEO = new ENTWFItemStateHistoryEO();
                    newENTWFItemStateHistoryEO.MapEntityToProperties(eNTWFItemStateHistory);
                    this.Add(newENTWFItemStateHistoryEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public void Load(int entWFItemId)
        {
            LoadFromList(new ENTWFItemStateHistoryData().SelectByENTWFItemId(entWFItemId));
        }
    }

    #endregion ENTWFItemStateHistoryEOList
}
