using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTAuditEO

    [Serializable]
    public class ENTAuditEO : ENTBaseEO
    {
        #region Enumerations

        public enum AuditTypeEnum
        {
            Add,
            Update,
            Delete
        }

        #endregion Enumerations

        #region Properties

        public string ObjectName { get; set; }
        public int RecordId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public AuditTypeEnum AuditType { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            var eNTAudit = new ENTAuditData().Select(id);
            MapEntityToProperties(eNTAudit);
            return eNTAudit != null;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTAudit = (ENTAudit)entity;
            ID = eNTAudit.ENTAuditId;
            ObjectName = eNTAudit.ObjectName;
            RecordId = eNTAudit.RecordId;
            PropertyName = eNTAudit.PropertyName;
            OldValue = eNTAudit.OldValue;
            NewValue = eNTAudit.NewValue;
            AuditType = (AuditTypeEnum)eNTAudit.AuditType;
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
                        ID = new ENTAuditData().Insert(db, ObjectName, RecordId, PropertyName, OldValue, NewValue, Convert.ToByte(AuditType), userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTAuditData().Update(db, ID, ObjectName, RecordId, PropertyName, OldValue, NewValue, Convert.ToByte(AuditType), userAccountId, Version))
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
            throw new Exception("DBAction not Save.");
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {

        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTAuditData().Delete(db, ID);
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

        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        #endregion Overrides
    }

    #endregion ENTAuditEO

    #region ENTAuditEOList

    [Serializable]
    public class ENTAuditEOList : ENTBaseEOList<ENTAuditEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTAuditData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTAudit> eNTAudits)
        {
            if (eNTAudits.Count > 0)
            {
                foreach (var eNTAudit in eNTAudits)
                {
                    var newENTAuditEO = new ENTAuditEO();
                    newENTAuditEO.MapEntityToProperties(eNTAudit);
                    this.Add(newENTAuditEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion ENTAuditEOList
}
