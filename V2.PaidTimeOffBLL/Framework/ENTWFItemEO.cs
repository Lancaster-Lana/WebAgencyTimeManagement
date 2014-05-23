using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTWFItemEO

    [Serializable]
    public class ENTWFItemEO : ENTBaseEO
    {

        #region Properties

        public int ENTWorkflowId { get; set; }
        public int ItemId { get; set; }
        public int SubmitterENTUserAccountId { get; set; }
        public string SubmitterUserName { get; private set; }
        public int CurrentWFStateId { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTWFItem eNTWFItem = new ENTWFItemData().Select(id);
            if (eNTWFItem != null)
            {
                MapEntityToProperties(eNTWFItem);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTWFItem eNTWFItem = (ENTWFItem)entity;

            ID = eNTWFItem.ENTWFItemId;
            ENTWorkflowId = eNTWFItem.ENTWorkflowId;
            ItemId = eNTWFItem.ItemId;
            SubmitterENTUserAccountId = eNTWFItem.SubmitterENTUserAccountId;
            SubmitterUserName = eNTWFItem.SubmitterUserName;
            CurrentWFStateId = eNTWFItem.CurrentWFStateId;
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
                        ID = new ENTWFItemData().Insert(db, ENTWorkflowId, ItemId, SubmitterENTUserAccountId, CurrentWFStateId, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTWFItemData().Update(db, ID, ENTWorkflowId, ItemId, SubmitterENTUserAccountId, CurrentWFStateId, userAccountId, Version))
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
            if (SubmitterENTUserAccountId == 0)
            {
                throw new Exception("The submitter is required.");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTWFItemData().Delete(db, ID);
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

        public bool LoadByItemId(int entWorkflowId, int itemId)
        {
            //Get the entity object from the DAL.
            ENTWFItem eNTWFItem = new ENTWFItemData().SelectByItemId(entWorkflowId, itemId);
            if (eNTWFItem != null)
            {
                MapEntityToProperties(eNTWFItem);
                return true;
            }
            else
            {
                return false;
            }
        }                
    }

    #endregion ENTWFItemEO

    #region ENTWFItemEOList

    [Serializable]
    public class ENTWFItemEOList : ENTBaseEOList<ENTWFItemEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTWFItemData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTWFItem> eNTWFItems)
        {
            if (eNTWFItems.Count > 0)
            {
                foreach (ENTWFItem eNTWFItem in eNTWFItems)
                {
                    ENTWFItemEO newENTWFItemEO = new ENTWFItemEO();
                    newENTWFItemEO.MapEntityToProperties(eNTWFItem);
                    this.Add(newENTWFItemEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion ENTWFItemEOList
}
