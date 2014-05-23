using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTAuditObjectPropertyEO

    [Serializable]
    public class ENTAuditObjectPropertyEO : ENTBaseEO
    {

        #region Properties

        public int ENTAuditObjectId { get; set; }
        public string PropertyName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTAuditObjectProperty eNTAuditObjectProperty = new ENTAuditObjectPropertyData().Select(id);
            MapEntityToProperties(eNTAuditObjectProperty);
            return eNTAuditObjectProperty != null;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTAuditObjectProperty eNTAuditObjectProperty = (ENTAuditObjectProperty)entity;

            ID = eNTAuditObjectProperty.ENTAuditObjectPropertyId;
            ENTAuditObjectId = eNTAuditObjectProperty.ENTAuditObjectId;
            PropertyName = eNTAuditObjectProperty.PropertyName;
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
                        ID = new ENTAuditObjectPropertyData().Insert(db, ENTAuditObjectId, PropertyName, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTAuditObjectPropertyData().Update(db, ID, ENTAuditObjectId, PropertyName, userAccountId, Version))
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
                throw new Exception("DBAction not Save.");
            }
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //Nothing to validate
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTAuditObjectPropertyData().Delete(db, ID);
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

    #endregion ENTAuditObjectPropertyEO

    #region ENTAuditObjectPropertyEOList

    [Serializable]
    public class ENTAuditObjectPropertyEOList : ENTBaseEOList<ENTAuditObjectPropertyEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTAuditObjectPropertyData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTAuditObjectProperty> eNTAuditObjectPropertys)
        {
            if (eNTAuditObjectPropertys.Count > 0)
            {
                foreach (ENTAuditObjectProperty eNTAuditObjectProperty in eNTAuditObjectPropertys)
                {
                    ENTAuditObjectPropertyEO newENTAuditObjectPropertyEO = new ENTAuditObjectPropertyEO();
                    newENTAuditObjectPropertyEO.MapEntityToProperties(eNTAuditObjectProperty);
                    this.Add(newENTAuditObjectPropertyEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        internal void Delete(HRPaidTimeOffDataContext db, int entAuditObjectID)
        {
            new ENTAuditObjectPropertyData().DeleteByENTAuditObjectId(db, entAuditObjectID);
        }

        public ENTAuditObjectPropertyEO GetByPropertyName(string propertyName)
        {
            return this.SingleOrDefault(p => p.PropertyName == propertyName);
        }

        internal void Load(int entAuditObjectId)
        {
            using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext())
            {
                Load(db, entAuditObjectId);
            }
        }

        internal void Load(HRPaidTimeOffDataContext db, int entAuditObjectId)
        {
            LoadFromList(new ENTAuditObjectPropertyData().SelectByENTAuditObjectId(db, entAuditObjectId));
        }
    }

    #endregion ENTAuditObjectPropertyEOList
}
