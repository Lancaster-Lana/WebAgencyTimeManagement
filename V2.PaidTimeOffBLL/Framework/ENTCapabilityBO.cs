using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTCapability

    [Serializable]
    public class ENTCapabilityBO : ENTBaseBO
    {
        #region Enumerations

        public enum AccessTypeEnum
        {
            ReadOnlyEdit = 0,
            ReadOnly = 1,
            Edit = 2
        }

        #endregion Enumerations

        #region Properties

        public string CapabilityName { get; private set; }
        public Nullable<int> MenuItemId { get; private set; }
        public AccessTypeEnum AccessType { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            ENTCapability capability = new ENTCapabilityData().Select(id);
            MapEntityToProperties(capability);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTCapability capability = (ENTCapability)entity;

            ID = capability.ENTCapabilityId;
            CapabilityName = capability.CapabilityName;
            MenuItemId = capability.ENTMenuItemId;
            AccessType = (AccessTypeEnum)capability.AccessType;
        }

        protected override string GetDisplayText()
        {
            return CapabilityName;
        }

        #endregion Overrides
    }

    #endregion ENTCapability

    #region ENTCapabilityList

    [Serializable]
    public class ENTCapabilityBOList : ENTBaseBOList<ENTCapabilityBO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTCapabilityData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTCapability> capabilities)
        {
            if (capabilities.Count > 0)
            {
                foreach (ENTCapability capability in capabilities)
                {
                    ENTCapabilityBO newCapabilityBO = new ENTCapabilityBO();
                    newCapabilityBO.MapEntityToProperties(capability);
                    this.Add(newCapabilityBO);
                }
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Returns a Capability with the matching name.  If it is not found then null is returned.
        /// </summary>
        /// <param name="capabilityName"></param>
        /// <returns></returns>
        public ENTCapabilityBO GetByName(string capabilityName)
        {
            return this.SingleOrDefault(c => c.CapabilityName == capabilityName);
        }

        #endregion Public Methods

        public IEnumerable<ENTCapabilityBO> GetByMenuItemId(int entMenuItemId)
        {            
            return from c in this
                   where c.MenuItemId == entMenuItemId
                   orderby c.CapabilityName
                   select c;            
        }
    }

    #endregion ENTCapabilityList
}
