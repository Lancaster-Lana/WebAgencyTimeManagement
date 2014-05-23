using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFItemOwnerEO

    [Serializable]
    public class ENTWFItemOwnerEO : ENTBaseEO
    {
        #region Properties

        public int ENTWFItemId { get; set; }
        public int ENTWFOwnerGroupId { get; set; }
        public int? ENTUserAccountId { get; set; }
        public string UserName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            var eNTWFItemOwner = new ENTWFItemOwnerData().Select(id);
            MapEntityToProperties(eNTWFItemOwner);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTWFItemOwner = (ENTWFItemOwner)entity;

            ID = eNTWFItemOwner.ENTWFItemOwnerId;
            ENTWFItemId = eNTWFItemOwner.ENTWFItemId;
            ENTWFOwnerGroupId = eNTWFItemOwner.ENTWFOwnerGroupId;
            ENTUserAccountId = eNTWFItemOwner.ENTUserAccountId;
            UserName = eNTWFItemOwner.UserName;
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
                        ID = new ENTWFItemOwnerData().Insert(db, ENTWFItemId, ENTWFOwnerGroupId, ENTUserAccountId, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTWFItemOwnerData().Update(db, ID, ENTWFItemId, ENTWFOwnerGroupId, ENTUserAccountId, userAccountId, Version))
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
                new ENTWFItemOwnerData().Delete(db, ID);
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
            throw new NotImplementedException();        
        }

        #endregion Overrides
    }

    #endregion ENTWFItemOwnerEO

    #region ENTWFItemOwnerEOList

    [Serializable]
    public class ENTWFItemOwnerEOList : ENTBaseEOList<ENTWFItemOwnerEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFItemOwnerData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFItemOwner> eNTWFItemOwners)
        {
            if (eNTWFItemOwners.Count > 0)
            {
                foreach (var eNTWFItemOwner in eNTWFItemOwners)
                {
                    var newENTWFItemOwnerEO = new ENTWFItemOwnerEO();
                    newENTWFItemOwnerEO.MapEntityToProperties(eNTWFItemOwner);
                    this.Add(newENTWFItemOwnerEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public void Load(int entWFItemId)
        {
            //Get the entity object from the DAL.
            LoadFromList(new ENTWFItemOwnerData().SelectByENTWFItemId(entWFItemId));
        }

        public ENTWFItemOwnerEO GetByENTWFOwnerGroupId(int ownerGroupId)
        {            
            return this.Single(o => o.ENTWFOwnerGroupId == ownerGroupId);
        }                
    }

    #endregion ENTWFItemOwnerEOList
}
