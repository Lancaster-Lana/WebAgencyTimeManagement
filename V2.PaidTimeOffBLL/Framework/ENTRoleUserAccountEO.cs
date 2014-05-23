using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTRoleUserAccountEO

    [Serializable]
    public class ENTRoleUserAccountEO : ENTBaseEO
    {
        #region Properties

        public int ENTRoleId { get; set; }
        public int ENTUserAccountId { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Save(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                //Validate the object
                Validate(db, ref validationErrors);

                //Check if there were any validation errors
                if (validationErrors.Count == 0)
                {
                    if (ID <= 0)
                    {
                        //Add
                        ID = new ENTRoleUserAccountData().Insert(db, ENTRoleId, ENTUserAccountId, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new ENTRoleUserAccountData().Update(db, ID, ENTRoleId, ENTUserAccountId, userAccountId, Version))
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
            throw new Exception("DBAction not delete.");
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //None
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTRoleUserAccountData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //None
        }

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Load(int id)
        {
            //Get a data reader from the database.
            var userRole = new ENTRoleUserAccountData().Select(id);
            MapEntityToProperties(userRole);
            return true;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var roleUserAccount = (ENTRoleUserAccount)entity;

            ID = roleUserAccount.ENTRoleUserAccountId;
            ENTRoleId = roleUserAccount.ENTRoleId;
            ENTUserAccountId = roleUserAccount.ENTUserAccountId;
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        #endregion Overrides
    }

    #endregion ENTRoleCapabilityEO

    #region ENTRoleUserAccountEOList

    [Serializable]
    public class ENTRoleUserAccountEOList : ENTBaseEOList<ENTRoleUserAccountEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTRoleUserAccountData().Select());
        }     

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTRoleUserAccount> roleUserAccounts)
        {
            if (roleUserAccounts.Count > 0)
            {
                foreach (ENTRoleUserAccount roleUserAccount in roleUserAccounts)
                {
                    var newRoleUserAccountEO = new ENTRoleUserAccountEO();
                    newRoleUserAccountEO.MapEntityToProperties(roleUserAccount);
                    this.Add(newRoleUserAccountEO);
                }
            }
        }

        #endregion Private Methods

        #region Public Methods
        
        public bool IsUserInRole(int userAccountId)
        {
            return (GetByUserAccountId(userAccountId) != null);
        }
       
        /// <summary>
        /// Return the object from this instance with the specified userAccountId
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <returns></returns>
        public ENTRoleUserAccountEO GetByUserAccountId(int userAccountId)
        {
            return this.SingleOrDefault(u => u.ENTUserAccountId == userAccountId);
        }

        public List<ENTRoleUserAccountEO> GetUserRolesList(int userAccountId)
        {
            return this.Where(u => u.ENTUserAccountId == userAccountId).ToList();
            //return new ENTRoleData().SelectByENTUserAccountId(userAccountId);
        }

        /*
        public void RemoveUserRoles(int userAccountId)
        {
            //var userRoles = this.FindAll((i) => i.ENTUserAccountId == userAccountId);
            //allRolesList.Delete(userRoles);
            this.RemoveAll((i) => i.ENTUserAccountId == userAccountId);          
        }*/

        /// <summary>
        /// Load the current instance by role id
        /// </summary>
        /// <param name="roleID"></param>
        internal void LoadByEntRoleId(int entRoleID)
        {
            LoadFromList(new ENTRoleUserAccountData().SelectByENTRoleId(entRoleID));
        }

        #endregion Public Methods
    }

    #endregion ENTRoleUserAccountEOList
}
