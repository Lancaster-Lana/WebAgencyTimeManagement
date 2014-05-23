using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTUserAccountEO

    [Serializable]
    public class ENTUserAccountEO : ENTBaseEO
    {
        public ENTUserAccountEO()
        {
            Roles = new ENTRoleEOList();
        }

        public ENTUserAccountEO(int id):this()
        {
            Load(id);
            //Load roles for user
            this.Roles.LoadByENTUserAccountId(id);
        }

        #region Properties

        public string WindowsAccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public ENTRoleEOList Roles { get; private set; }

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
                    if (IsNewRecord())
                    {
                        //Add
                        ID = new ENTUserAccountData().Insert(db, WindowsAccountName, FirstName, LastName, Email, IsActive, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new ENTUserAccountData().Update(db, ID, WindowsAccountName, FirstName, LastName, Email, IsActive, userAccountId, Version))
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
            throw new Exception("DBAction not save.");
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            var entUserAccountData = new ENTUserAccountData();

            //Windows Account Name is required.
            if (WindowsAccountName.Trim().Length == 0)
            {
                validationErrors.Add("The windows account name is required.");
            }

            ////The windows account name must be unique.
            if (entUserAccountData.IsDuplicateWindowsAccountName(db, ID, WindowsAccountName))
            {
                validationErrors.Add("The windows account name must be unique.");
            }

            //TODO: VV Validate agains AD

            //First name, last name, and email are required.
            if (FirstName.Trim().Length == 0)
            {
                validationErrors.Add("The first name is required.");
            }

            //Last Name is required
            if (LastName.Trim().Length == 0)
            {
                validationErrors.Add("The last name is required.");
            }

            //Email is required
            if (Email.Trim().Length == 0)
            {
                validationErrors.Add("The email address is required.");
            }
            else
            {
                if (entUserAccountData.IsDuplicateEmail(db, ID, Email))
                {
                    validationErrors.Add("The email address must be unique.");
                }
                else
                {
                    if (Email.IndexOf("@") < 0)
                    {
                        validationErrors.Add("The email address must contain the @ sign.");
                    }
                    else
                    {
                        string[] emailParts = Email.Split(new char[] { '@' });
                        if ((emailParts.Length != 2) ||
                           (emailParts[0].Length < 2) ||
                           (emailParts[1].ToUpper() != "POWEREDBYV2.COM"))
                        {
                            validationErrors.Add("The email address must be in the format XX@V2.com");
                        }
                    }                    
                }
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            //throw new NotImplementedException();
            if (DBAction == DBActionEnum.Delete)
            {
                /*
                //Delete userRoles before user
                var allRolesList = new ENTRoleUserAccountEOList();
                allRolesList.Load();//fill with data            
                allRolesList.RemoveUserRoles(ID);
                */
                new ENTUserAccountData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //TODO: validate that user has requests
            //throw new NotImplementedException();
        }

        public override void Init()
        {
            IsActive = true;
        }

        public override bool Load(int id)
        {
            ENTUserAccount userAccount = new ENTUserAccountData().Select(id);
            if (userAccount != null)
            {
                MapEntityToProperties(userAccount);
                return true;
            }
            return false;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var userAccount = (ENTUserAccount)entity;

            ID = userAccount.ENTUserAccountId;
            WindowsAccountName = userAccount.WindowsAccountName;
            FirstName = userAccount.FirstName;
            LastName = userAccount.LastName;
            Email = userAccount.Email;
            IsActive = userAccount.IsActive;
        }

        protected override string GetDisplayText()
        {
            return LastName + ", " + FirstName;
        }

        #endregion Overrides

        #region Public Methods

        /// <summary>
        /// The capabilities are least restrictive.  If a user is in more then one role and one has edit and the other is read only
        /// then edit is returned.
        /// </summary>
        /// <param name="capabilityId"></param>
        /// <param name="rolesWithCapabilities"></param>
        /// <returns></returns>
        public ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum GetCapabilityAccess(int capabilityId, ENTRoleEOList rolesWithCapabilities)
        {
            var retVal = ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.None;

            //The roles in the user object do not include the capabilities.
            foreach (var role in Roles)
            {
                var roleWithCapabilities = rolesWithCapabilities.GetByRoleId(role.ID);

                foreach (var capability in roleWithCapabilities.RoleCapabilities)
                {
                    if (capability.Capability.ID == capabilityId)
                    {
                        if (capability.AccessFlag == ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit)
                        {
                            return ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit;                            
                        }
                        if (capability.AccessFlag == ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly)
                        {
                            //Since this is least restrictive temporarirly set the return value to read only.
                            retVal = ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly;
                        }
                    }
                }
            }

            return retVal;
        }

        #endregion Public Methods

        internal bool Load(HRPaidTimeOffDataContext db, int id)
        {
            ENTUserAccount userAccount = new ENTUserAccountData().Select(db, id);
            if (userAccount != null)
            {
                MapEntityToProperties(userAccount);
                return true;
            }
            return false;
        }
    }

    #endregion ENTUserAccountEOList

    #region ENTUserAccountEOList

    [Serializable]
    public class ENTUserAccountEOList : ENTBaseEOList<ENTUserAccountEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTUserAccountData().Select());
        }

        #endregion Overrides

        #region Private Methods

        protected void LoadFromList(List<ENTUserAccount> users)
        {            
            foreach (var user in users)
            {
                var newUserAccountEO = new ENTUserAccountEO();
                newUserAccountEO.MapEntityToProperties(user);
                this.Add(newUserAccountEO);
            }        
        }

        #endregion Private Methods

        #region Public Methods

        public void LoadWithRoles()
        {
            Load();

            foreach (var user in this)
            {                
                user.Roles.LoadByENTUserAccountId(user.ID);
            }
        }

        public ENTUserAccountEO GetByWindowAccountName(string windowsAccountName)
        {
            return this.SingleOrDefault(u => u.WindowsAccountName.ToUpper() == windowsAccountName.ToUpper());
        }

        //Added in Chapter 11
        public ENTUserAccountEO GetByID(int id)
        {
            return this.SingleOrDefault(u => u.ID == id);
        }

        #endregion Public Methods

        public void LoadByWFOwnerGroupId(int entWFOwnerGroupId)
        {
            LoadFromList(new ENTUserAccountData().SelectByWFOwnerGroupId(entWFOwnerGroupId));
        }                
    }

    #endregion ENTUserAccountEOList
}
