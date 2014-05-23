using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTRoleEO

    [Serializable]
    public class ENTRoleEO : ENTBaseEO
    {
        #region Constructor

        public ENTRoleEO()
        {
            RoleCapabilities = new ENTRoleCapabilityEOList();
            RoleUserAccounts = new ENTRoleUserAccountEOList();
        }

        #endregion Constructor

        #region Properties

        public string RoleName { get; set; }
        public ENTRoleCapabilityEOList RoleCapabilities { get; private set; }
        public ENTRoleUserAccountEOList RoleUserAccounts { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get a data reader from the database.
            var role = new ENTRoleData().Select(id);
            MapEntityToProperties(role);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var role = (ENTRole)entity;

            ID = role.ENTRoleId;
            RoleName = role.RoleName;
            RoleCapabilities.LoadByENTRoleId(ID);
            RoleUserAccounts.LoadByEntRoleId(ID);
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
                        ID = new ENTRoleData().Insert(db, RoleName, userAccountId);

                        //Since this was an add you need to update all the role ids for the user and capability records
                        foreach (ENTRoleCapabilityEO capability in RoleCapabilities)
                        {
                            capability.ENTRoleId = ID;
                        }

                        foreach (ENTRoleUserAccountEO user in RoleUserAccounts)
                        {
                            user.ENTRoleId = ID;
                        }
                    }
                    else
                    {
                        //Update
                        if (!new ENTRoleData().Update(db, ID, RoleName, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    //Now save the capabilities
                    if (RoleCapabilities.Save(db, ref validationErrors, userAccountId))
                    {
                        //Now save the users
                        if (RoleUserAccounts.Save(db, ref validationErrors, userAccountId))
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
            if (RoleName.Trim().Length == 0)
            {
                validationErrors.Add("The name is required.");
            }

            //The role name must be unique.
            if (new ENTRoleData().IsDuplicateRoleName(db, ID, RoleName))
            {
                validationErrors.Add("The name must be unique.");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTRoleData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //No Validation
        }

        public override void Init()
        {
            //Nothing to default
        }

        protected override string GetDisplayText()
        {
            return RoleName;
        }

        #endregion Overrides
    }

    #endregion ENTRoleEO

    #region ENTRoleEOList

    [Serializable]
    public class ENTRoleEOList : ENTBaseEOList<ENTRoleEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTRoleData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTRole> roles)
        {
            if (roles.Count > 0)
            {
                foreach (ENTRole role in roles)
                {
                    var newRoleEO = new ENTRoleEO();
                    newRoleEO.MapEntityToProperties(role);
                    this.Add(newRoleEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        internal ENTRoleEO GetByRoleId(int roleId)
        {
            return this.SingleOrDefault(r => r.ID == roleId);
        }

        internal void LoadByENTUserAccountId(int entUserAccountId)
        {
            LoadFromList(new ENTRoleData().SelectByENTUserAccountId(entUserAccountId));
        }

        #endregion Internal Methods
    }

    #endregion ENTRoleEOList
        
}
