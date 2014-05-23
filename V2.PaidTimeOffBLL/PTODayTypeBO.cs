using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.PaidTimeOffBLL
{
    #region PTODayTypeBO

    [Serializable]
    public class PTODayTypeBO : ENTBaseBO
    {
        public enum PTODayTypeEnum
        {
            Full = 1,
            AM = 2,
            PM = 3
        }

        #region Properties

        public string PTODayTypeName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            var pTODayType = new PTODayTypeData().Select(id);
            MapEntityToProperties(pTODayType);
            return true;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var pTODayType = (PTODayType)entity;
            ID = pTODayType.PTODayTypeId;
            PTODayTypeName = pTODayType.PTODayTypeName;
        }

        protected override string GetDisplayText()
        {
            return PTODayTypeName;
        }

        #endregion Overrides
    }

    #endregion PTODayTypeBO

    #region PTODayTypeBOList

    [Serializable]
    public class PTODayTypeBOList : ENTBaseBOList<PTODayTypeBO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new PTODayTypeData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<PTODayType> pTODayTypes)
        {
            if (pTODayTypes.Count > 0)
            {
                foreach (var pTODayType in pTODayTypes)
                {
                    var newPTODayTypeBO = new PTODayTypeBO();
                    newPTODayTypeBO.MapEntityToProperties(pTODayType);
                    this.Add(newPTODayTypeBO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion PTODayTypeBOList
}
