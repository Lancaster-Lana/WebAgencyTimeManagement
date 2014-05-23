using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.PaidTimeOffBLL
{
    #region PTORequestTypeBO

    [Serializable]
    public class PTORequestTypeBO : ENTBaseBO
    {
        public enum PTORequestTypeEnum
        {
            Vacation = 1,
            Personal = 2,
            Unpaid = 3
        }

        #region Properties

        public string PTORequestTypeName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            var pTORequestType = new PTORequestTypeData().Select(id);
            MapEntityToProperties(pTORequestType);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var pTORequestType = (PTORequestType)entity;
            ID = pTORequestType.PTORequestTypeId;
            PTORequestTypeName = pTORequestType.PTORequestTypeName;
        }
                
        protected override string GetDisplayText()
        {
            return PTORequestTypeName;
        }

        #endregion Overrides
    }

    #endregion PTORequestTypeBO

    #region PTORequestTypeBOList

    [Serializable]
    public class PTORequestTypeBOList : ENTBaseBOList<PTORequestTypeBO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new PTORequestTypeData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<PTORequestType> pTORequestTypes)
        {
            if (pTORequestTypes.Count > 0)
            {
                foreach (var pTORequestType in pTORequestTypes)
                {
                    var newPTORequestTypeBO = new PTORequestTypeBO();
                    newPTORequestTypeBO.MapEntityToProperties(pTORequestType);
                    this.Add(newPTORequestTypeBO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion PTORequestTypeEOList
}
