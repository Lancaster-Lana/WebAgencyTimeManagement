using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTRoleCapabilityEO

    [Serializable]
    public class ENTRoleCapabilityEO : ENTBaseEO
    {
        #region Enumerations

        public enum CapabiiltyAccessFlagEnum
        {
            None,
            ReadOnly,
            Edit
        }

        #endregion Enumerations

        #region Constructor

        public ENTRoleCapabilityEO()
        {
            Capability = new ENTCapabilityBO();
        }

        #endregion Constructor

        #region Properties

        public int ENTRoleId { get; set; }
        public CapabiiltyAccessFlagEnum AccessFlag { get; set; }
        public ENTCapabilityBO Capability { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            ENTRoleCapability roleCapability = new ENTRoleCapabilityData().Select(id);
            MapEntityToProperties(roleCapability);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var roleCapability = (ENTRoleCapability)entity;

            ID = roleCapability.ENTRoleCapabilityId;
            ENTRoleId = roleCapability.ENTRoleId;
            AccessFlag = (CapabiiltyAccessFlagEnum)roleCapability.AccessFlag;
            Capability.Load(roleCapability.ENTCapabilityId);
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
                    if (ID <= 0)
                    {
                        //Add
                        ID = new ENTRoleCapabilityData().Insert(db, ENTRoleId, Capability.ID, Convert.ToByte(AccessFlag), userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new ENTRoleCapabilityData().Update(db, ID, ENTRoleId, Capability.ID, Convert.ToByte(AccessFlag), userAccountId, Version))
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
                throw new Exception("DBAction not save.");
            }
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //No validation
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTRoleCapabilityData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //No validation
        }

        public override void Init()
        {
            //No defaults
        }

        protected override string GetDisplayText()
        {
            //never should happen since this is a junction table.
            throw new NotImplementedException();
        }

        #endregion Overrides
    }

    #endregion ENTRoleCapabilityEO

    #region ENTRoleCapabilityEOList

    [Serializable]
    public class ENTRoleCapabilityEOList : ENTBaseEOList<ENTRoleCapabilityEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTRoleCapabilityData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTRoleCapability> roleCapabilities)
        {
            if (roleCapabilities.Count > 0)
            {
                foreach (ENTRoleCapability roleCapability in roleCapabilities)
                {
                    ENTRoleCapabilityEO newRoleCapabilityEO = new ENTRoleCapabilityEO();
                    newRoleCapabilityEO.MapEntityToProperties(roleCapability);
                    this.Add(newRoleCapabilityEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        /// <summary>
        /// Returns all the Role\Capabilitis for the specificed menuItemId.
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        internal IEnumerable<ENTRoleCapabilityEO> GetByMenuItemId(int menuItemId)
        {
            return from rc in this
                   where rc.Capability.MenuItemId == menuItemId
                   select rc;
        }

        /// <summary>
        /// Load this instance by role id.
        /// </summary>
        /// <param name="roleId"></param>
        internal void LoadByENTRoleId(int entRoleId)
        {
            LoadFromList(new ENTRoleCapabilityData().SelectByENTRoleId(entRoleId));
        }

        #endregion Internal Methods

        #region Public Methods

        /// <summary>
        /// Get from this instance the object with the specified capability.
        /// </summary>
        /// <param name="capabilityId"></param>
        /// <returns></returns>
        public ENTRoleCapabilityEO GetByCapabilityID(int capabilityId)
        {
            return this.SingleOrDefault(rc => rc.Capability.ID == capabilityId);
        }
                
        #endregion Public Methods                
    }

    #endregion ENTRoleCapabilityEOList
}
