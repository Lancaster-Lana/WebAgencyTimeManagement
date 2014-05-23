using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTNotificationENTUserAccountEO

    [Serializable]
    public class ENTNotificationENTUserAccountEO : ENTBaseEO
    {
        #region Members

        private ENTNotificationENTWFStateEOList _notificationStates;

        #endregion Memebers

        #region Constructor

        public ENTNotificationENTUserAccountEO()
        {
            LoadStates = true;
            _notificationStates = new ENTNotificationENTWFStateEOList();
        }

        #endregion Constructor

        #region Properties

        public int ENTNotificationId { get; set; }
        public int ENTUserAccountId { get; set; }
        public bool LoadStates { get; set; }

        public ENTNotificationENTWFStateEOList NotificationStates
        {
            get { return _notificationStates; }
        }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTNotificationENTUserAccount eNTNotificationENTUserAccount = new ENTNotificationENTUserAccountData().Select(id);
            MapEntityToProperties(eNTNotificationENTUserAccount);
            return eNTNotificationENTUserAccount != null;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTNotificationENTUserAccount eNTNotificationENTUserAccount = (ENTNotificationENTUserAccount)entity;

            ID = eNTNotificationENTUserAccount.ENTNotificationENTUserAccountId;
            ENTNotificationId = eNTNotificationENTUserAccount.ENTNotificationId;
            ENTUserAccountId = eNTNotificationENTUserAccount.ENTUserAccountId;

            if (LoadStates)
            {
                _notificationStates.Load(ID);
            }
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
                        ID = new ENTNotificationENTUserAccountData().Insert(db, ENTNotificationId, ENTUserAccountId, userAccountId);

                        foreach (ENTNotificationENTWFStateEO notificationState in _notificationStates)
                        {
                            notificationState.ENTNotificationENTUserAccountId = ID;
                        }
                    }
                    else
                    {
                        //Update
                        if (!new ENTNotificationENTUserAccountData().Update(db, ID, ENTNotificationId, ENTUserAccountId, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    //Delete all the states associated with this user
                    _notificationStates.Delete(db, ID);

                    //Add the states that were selected.
                    return _notificationStates.Save(db, ref validationErrors, userAccountId);                    
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
            //throw new NotImplementedException();
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTNotificationENTUserAccountData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            
        }

        public override void Init()
        {
            
        }

        protected override string GetDisplayText()
        {
            return ID.ToString();
        }

        #endregion Overrides                
    }

    #endregion ENTNotificationENTUserAccountEO

    #region ENTNotificationENTUserAccountEOList

    [Serializable]
    public class ENTNotificationENTUserAccountEOList : ENTBaseEOList<ENTNotificationENTUserAccountEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTNotificationENTUserAccountData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTNotificationENTUserAccount> eNTNotificationENTUserAccounts)
        {
            LoadFromList(eNTNotificationENTUserAccounts, true);
        }

        private void LoadFromList(List<ENTNotificationENTUserAccount> eNTNotificationENTUserAccounts, bool loadStates)
        {
            if (eNTNotificationENTUserAccounts.Count > 0)
            {
                foreach (ENTNotificationENTUserAccount eNTNotificationENTUserAccount in eNTNotificationENTUserAccounts)
                {
                    ENTNotificationENTUserAccountEO newENTNotificationENTUserAccountEO = new ENTNotificationENTUserAccountEO();
                    newENTNotificationENTUserAccountEO.LoadStates = loadStates;
                    newENTNotificationENTUserAccountEO.MapEntityToProperties(eNTNotificationENTUserAccount);
                    this.Add(newENTNotificationENTUserAccountEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public ENTNotificationENTUserAccountEO Get(ENTNotificationEO.NotificationType notificationType)
        {
            return this.SingleOrDefault(n => n.ENTNotificationId == (int)notificationType);
        }

        internal void Load(int entUserAccountId)
        {
            LoadFromList(new ENTNotificationENTUserAccountData().SelectByENTUserAccountId(entUserAccountId));
        }

        internal void Load(HRPaidTimeOffDataContext db, int entWFStateId, ENTNotificationEO.NotificationType notificationType)
        {
            LoadFromList(new ENTNotificationENTUserAccountData().SelectByENTWFStateId(db, entWFStateId, (int)notificationType), false);
        }

        internal ENTNotificationENTUserAccountEO GetByENTUserAccountId(int entUserAccountId)
        {
            return this.SingleOrDefault(n => n.ENTUserAccountId == entUserAccountId);
        }
    }

    #endregion ENTNotificationENTUserAccountEOList
}
