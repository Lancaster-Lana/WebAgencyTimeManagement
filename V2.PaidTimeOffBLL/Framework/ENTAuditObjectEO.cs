using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTAuditObjectEO

    [Serializable]
    public class ENTAuditObjectEO : ENTBaseEO
    {
        #region Members

        private ENTAuditObjectPropertyEOList _properties = new ENTAuditObjectPropertyEOList();

        #endregion Members

        #region Properties

        public string ObjectName { get; set; }
        public string ObjectFullyQualifiedName { get; set; }

        public ENTAuditObjectPropertyEOList Properties
        {
            get { return _properties; }
        }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            var eNTAuditObject = new ENTAuditObjectData().Select(id);
            MapEntityToProperties(eNTAuditObject);
            _properties.Load(ID);
            return eNTAuditObject != null;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTAuditObject = (ENTAuditObject)entity;

            ID = eNTAuditObject.ENTAuditObjectId;
            ObjectName = eNTAuditObject.ObjectName;
            ObjectFullyQualifiedName = eNTAuditObject.ObjectFullyQualifiedName;
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
                        ID = new ENTAuditObjectData().Insert(db, ObjectName, ObjectFullyQualifiedName, userAccountId);

                        //Add the ID to all the property objects
                        foreach (ENTAuditObjectPropertyEO property in _properties)
                        {
                            property.ENTAuditObjectId = ID;
                        }
                    }
                    else
                    {
                        //Update
                        if (!new ENTAuditObjectData().Update(db, ID, ObjectName, ObjectFullyQualifiedName, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                        //Delete the existing records for this object
                        _properties.Delete(db, ID);
                    }

                    //Save the new settings
                    _properties.Save(db, ref validationErrors, userAccountId);
                    return true;
                }
                //Didn't pass validation.
                return false;
            }
            throw new Exception("DBAction not Save.");
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            if (ObjectName == "")
            {
                validationErrors.Add("Please select an object to audit.");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTAuditObjectData().Delete(db, ID);
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
            //Nothing to initialize
        }

        protected override string GetDisplayText()
        {
            return ObjectName;
        }

        #endregion Overrides

        internal bool Load(string objectName)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return Load(db, objectName);
            }
        }

        internal bool Load(HRPaidTimeOffDataContext db, string objectName)
        {
            //Get the entity object from the DAL.
            var eNTAuditObject = new ENTAuditObjectData().Select(db, objectName);
            MapEntityToProperties(eNTAuditObject);
            _properties.Load(db, ID);
            return eNTAuditObject != null;
        }
    }

    #endregion ENTAuditObjectEO

    #region ENTAuditObjectEOList

    [Serializable]
    public class ENTAuditObjectEOList : ENTBaseEOList<ENTAuditObjectEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTAuditObjectData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTAuditObject> eNTAuditObjects)
        {
            if (eNTAuditObjects.Count > 0)
            {
                foreach (var eNTAuditObject in eNTAuditObjects)
                {
                    var newENTAuditObjectEO = new ENTAuditObjectEO();
                    newENTAuditObjectEO.MapEntityToProperties(eNTAuditObject);
                    this.Add(newENTAuditObjectEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public ENTAuditObjectEO GetByObjectName(string objectName)
        {
            return this.SingleOrDefault(a => a.ObjectName == objectName);
        }
    }

    #endregion ENTAuditObjectEOList
}
