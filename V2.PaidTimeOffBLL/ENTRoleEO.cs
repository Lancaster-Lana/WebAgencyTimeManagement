using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.PaidTimeOffBLL
{
    #region ENTRoleEO

    [Serializable]
    public class ENTRoleEO : ENTBaseEO
    {
        #region Properties

        public string RoleName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            var eNTRole = new ENTRoleData().Select(id);
            MapEntityToProperties(eNTRole);
            return eNTRole != null;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTRole = (ENTRole)entity;
            ID = eNTRole.ENTRoleId;
            RoleName = eNTRole.RoleName;
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
                    return true;
                }
                //Didn't pass validation.
                return false;
            }
            throw new Exception("DBAction not Save.");
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            throw new NotImplementedException();
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

        private void LoadFromList(List<ENTRole> eNTRoles)
        {
            if (eNTRoles.Count > 0)
            {
                foreach (var eNTRole in eNTRoles)
                {
                    var newENTRoleEO = new ENTRoleEO();
                    newENTRoleEO.MapEntityToProperties(eNTRole);
                    this.Add(newENTRoleEO);
                }
            }
        }

        #endregion Private Methods
    }

    #endregion ENTRoleEOList
}
