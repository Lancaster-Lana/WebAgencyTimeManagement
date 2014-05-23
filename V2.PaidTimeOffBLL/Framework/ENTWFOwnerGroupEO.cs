using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFOwnerGroupEO

    [Serializable]
    public class ENTWFOwnerGroupEO : ENTBaseEO
    {
        #region Constructor

        public ENTWFOwnerGroupEO()
        {
            UserAccounts = new ENTWFOwnerGroupUserAccountEOList();
        }

        #endregion Constructor

        #region Properties

        public int ENTWorkflowId { get; set; }
        public string OwnerGroupName { get; set; }
        public Nullable<int> DefaultENTUserAccountId { get; set; }
        public bool IsDefaultSameAsLast { get; set; }
        public string Description { get; set; }
        public ENTWFOwnerGroupUserAccountEOList UserAccounts { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTWFOwnerGroup eNTWFOwnerGroup = new ENTWFOwnerGroupData().Select(id);
            MapEntityToProperties(eNTWFOwnerGroup);
                       
            return eNTWFOwnerGroup != null;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTWFOwnerGroup eNTWFOwnerGroup = (ENTWFOwnerGroup)entity;

            ID = eNTWFOwnerGroup.ENTWFOwnerGroupId;
            ENTWorkflowId = eNTWFOwnerGroup.ENTWorkflowId;
            OwnerGroupName = eNTWFOwnerGroup.OwnerGroupName;
            DefaultENTUserAccountId = eNTWFOwnerGroup.DefaultENTUserAccountId;
            IsDefaultSameAsLast = eNTWFOwnerGroup.IsDefaultSameAsLast;
            Description = eNTWFOwnerGroup.Description;

            //Load the users for this group.
            UserAccounts.Load(ID);
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
                        ID = new ENTWFOwnerGroupData().Insert(db, ENTWorkflowId, OwnerGroupName, DefaultENTUserAccountId, IsDefaultSameAsLast, Description, userAccountId);

                        foreach (ENTWFOwnerGroupUserAccountEO user in UserAccounts)
                        {
                            user.ENTWFOwnerGroupId = ID;
                        }
                    }
                    else
                    {
                        //Update
                        if (!new ENTWFOwnerGroupData().Update(db, ID, ENTWorkflowId, OwnerGroupName, DefaultENTUserAccountId, IsDefaultSameAsLast, Description, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    //Now save the users
                    if (UserAccounts.Save(db, ref validationErrors, userAccountId))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
            if (OwnerGroupName.Trim() == "")
            {
                validationErrors.Add("The name is required.");
            }
            else
            {
                //The name must be unique within a workflow.
                if (new ENTWFOwnerGroupData().IsNameUnique(db, OwnerGroupName, ENTWorkflowId, ID)== false)
                {
                    validationErrors.Add("The name must be unique.");
                }
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWFOwnerGroupData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //Do not allow delete if they are associated with a state.
            if (new ENTWFOwnerGroupData().IsAssociatedWithState(db, ID))
            {
                validationErrors.Add("This group is the default owner for one or more Workflow States and can not be deleted.");
            }
        }

        public override void Init()
        {
            //No defaults
        }

        protected override string GetDisplayText()
        {
            return OwnerGroupName;
        }

        #endregion Overrides
    }

    #endregion ENTWFOwnerGroupEO

    #region ENTWFOwnerGroupEOList

    [Serializable]
    public class ENTWFOwnerGroupEOList : ENTBaseEOList<ENTWFOwnerGroupEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFOwnerGroupData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFOwnerGroup> eNTWFOwnerGroups)
        {
            if (eNTWFOwnerGroups.Count > 0)
            {
                foreach (var eNTWFOwnerGroup in eNTWFOwnerGroups)
                {
                    var newENTWFOwnerGroupEO = new ENTWFOwnerGroupEO();
                    newENTWFOwnerGroupEO.MapEntityToProperties(eNTWFOwnerGroup);
                    Add(newENTWFOwnerGroupEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public void Load(int entWorkflowId)
        {
            LoadFromList(new ENTWFOwnerGroupData().SelectByENTWorkflowId(entWorkflowId));
        }

        public ENTWFOwnerGroupEO GetByENTWFOwnerGroupId(int entWFOwnerGroupId)
        {
            return this.Single(og => og.ID == entWFOwnerGroupId);
        }
    }

    #endregion ENTWFOwnerGroupEOList
}
