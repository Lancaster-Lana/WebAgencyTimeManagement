using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFStatePropertyEO

    [Serializable]
    public class ENTWFStatePropertyEO : ENTBaseEO
    {

        #region Properties

        public int ENTWFStateId { get; set; }
        public string PropertyName { get; set; }
        public bool Required { get; set; }
        public bool ReadOnly { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTWFStateProperty eNTWFStateProperty = new ENTWFStatePropertyData().Select(id);
            MapEntityToProperties(eNTWFStateProperty);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTWFStateProperty eNTWFStateProperty = (ENTWFStateProperty)entity;

            ID = eNTWFStateProperty.ENTWFStatePropertyId;
            ENTWFStateId = eNTWFStateProperty.ENTWFStateId;
            PropertyName = eNTWFStateProperty.PropertyName;
            Required = eNTWFStateProperty.Required;
            ReadOnly = eNTWFStateProperty.ReadOnly;
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
                        ID = new ENTWFStatePropertyData().Insert(db, ENTWFStateId, PropertyName, Required, ReadOnly, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTWFStatePropertyData().Update(db, ID, ENTWFStateId, PropertyName, Required, ReadOnly, userAccountId, Version))
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
            
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWFStatePropertyData().Delete(db, ID);
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

    #endregion ENTWFStatePropertyEO

    #region ENTWFStatePropertyEOList

    [Serializable]
    public class ENTWFStatePropertyEOList : ENTBaseEOList<ENTWFStatePropertyEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFStatePropertyData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFStateProperty> eNTWFStatePropertys)
        {
            if (eNTWFStatePropertys.Count > 0)
            {
                foreach (ENTWFStateProperty eNTWFStateProperty in eNTWFStatePropertys)
                {
                    ENTWFStatePropertyEO newENTWFStatePropertyEO = new ENTWFStatePropertyEO();
                    newENTWFStatePropertyEO.MapEntityToProperties(eNTWFStateProperty);
                    this.Add(newENTWFStatePropertyEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        internal void Load(int entWFStateId)
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                Load(db, entWFStateId);
            }
        }

        internal void Load(HRPaidTimeOffDataContext db, int entWFStateId)
        {
            LoadFromList(new ENTWFStatePropertyData().SelectByENTWFStateId(db, entWFStateId));
        }

        public ENTWFStatePropertyEO GetByPropertyName(string propertyName)
        {
            return this.SingleOrDefault(sp => sp.PropertyName == propertyName);
        }

        internal void Delete(HRPaidTimeOffDataContext db, int entWFStateId)
        {
            new ENTWFStatePropertyData().DeleteByENTWFStateId(db, entWFStateId);
        }                
    }

    #endregion ENTWFStatePropertyEOList
}
