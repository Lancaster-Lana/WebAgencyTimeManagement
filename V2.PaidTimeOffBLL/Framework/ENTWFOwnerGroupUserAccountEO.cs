using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFOwnerGroupUserAccountEO

    [Serializable]
    public class ENTWFOwnerGroupUserAccountEO : ENTBaseEO
    {
        #region Properties

        public int ENTWFOwnerGroupId { get; set; }
        public int ENTUserAccountId { get; set; }
        public string UserName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            var eNTWFOwnerGroupUserAccount = new ENTWFOwnerGroupUserAccountData().Select(id);
            MapEntityToProperties(eNTWFOwnerGroupUserAccount);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTWFOwnerGroupUserAccount = (ENTWFOwnerGroupUserAccount)entity;

            ID = eNTWFOwnerGroupUserAccount.ENTWFOwnerGroupUserAccountId;
            ENTWFOwnerGroupId = eNTWFOwnerGroupUserAccount.ENTWFOwnerGroupId;
            ENTUserAccountId = eNTWFOwnerGroupUserAccount.ENTUserAccountId;
            UserName = eNTWFOwnerGroupUserAccount.UserName;
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
                        ID = new ENTWFOwnerGroupUserAccountData().Insert(db, ENTWFOwnerGroupId, ENTUserAccountId, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTWFOwnerGroupUserAccountData().Update(db, ID, ENTWFOwnerGroupId, ENTUserAccountId, userAccountId, Version))
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
            //No validation
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWFOwnerGroupUserAccountData().Delete(db, ID);
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
            //None
        }

        protected override string GetDisplayText()
        {
            return ID.ToString();
        }

        #endregion Overrides
    }

    #endregion ENTWFOwnerGroupUserAccountEO

    #region ENTWFOwnerGroupUserAccountEOList

    [Serializable]
    public class ENTWFOwnerGroupUserAccountEOList : ENTBaseEOList<ENTWFOwnerGroupUserAccountEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFOwnerGroupUserAccountData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFOwnerGroupUserAccount> eNTWFOwnerGroupUserAccounts)
        {
            if (eNTWFOwnerGroupUserAccounts.Count > 0)
            {
                foreach (var eNTWFOwnerGroupUserAccount in eNTWFOwnerGroupUserAccounts)
                {
                    var newENTWFOwnerGroupUserAccountEO = new ENTWFOwnerGroupUserAccountEO();
                    newENTWFOwnerGroupUserAccountEO.MapEntityToProperties(eNTWFOwnerGroupUserAccount);
                    this.Add(newENTWFOwnerGroupUserAccountEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public bool IsUserInGroup(int entUserAccountId)
        {
            return (GetByUserAccountId(entUserAccountId) != null);
        }

        public ENTWFOwnerGroupUserAccountEO GetByUserAccountId(int userAccountId)
        {
            return this.SingleOrDefault(u => u.ENTUserAccountId == userAccountId);
        }

        public void Load(int entWFOwnerGroupId)
        {
            LoadFromList(new ENTWFOwnerGroupUserAccountData().SelectByENTWFOwnerGroupId(entWFOwnerGroupId));
        }                
    }

    #endregion ENTWFOwnerGroupUserAccountEOList
}
