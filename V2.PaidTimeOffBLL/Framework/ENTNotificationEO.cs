using System;
using System.Collections.Generic;
using System.Text;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTNotificationEO

    [Serializable]
    public class ENTNotificationEO : ENTBaseEO
    {
        public enum NotificationType
        {
            IBecameOwnerOfIssue = 1,
            MyRequestChangedState = 2,
            IssueIOwnedGoesToState = 3
        }

        #region Properties

        public string Description { get; set; }
        public string FromEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTNotification eNTNotification = new ENTNotificationData().Select(id);
            MapEntityToProperties(eNTNotification);
            return eNTNotification != null;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTNotification eNTNotification = (ENTNotification)entity;

            ID = eNTNotification.ENTNotificationId;
            Description = eNTNotification.Description;
            FromEmailAddress = eNTNotification.FromEmailAddress;
            Subject = eNTNotification.Subject;
            Body = eNTNotification.Body;
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
                        ID = new ENTNotificationData().Insert(db, Description, FromEmailAddress, Subject, Body, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTNotificationData().Update(db, ID, Description, FromEmailAddress, Subject, Body, userAccountId, Version))
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
            throw new NotImplementedException();
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
            return Description;
        }

        #endregion Overrides

        internal bool Load(HRPaidTimeOffDataContext db, int id)
        {
            //Get the entity object from the DAL.
            ENTNotification eNTNotification = new ENTNotificationData().Select(db, id);
            MapEntityToProperties(eNTNotification);
            return eNTNotification != null;        
        }

        internal bool Load(HRPaidTimeOffDataContext db, ENTNotificationEO.NotificationType notificationType, int entUserAccountId)
        {
            //Get the entity object from the DAL.
            ENTNotification eNTNotificationENTUserAccount = new ENTNotificationData().SelectByIdENTUserAccountId(db, (int)notificationType, entUserAccountId);
            MapEntityToProperties(eNTNotificationENTUserAccount);
            return eNTNotificationENTUserAccount != null;
        }

        public static string ReplaceTokens(List<Token> tokens, string template)
        {
            StringBuilder sb = new StringBuilder(template);

            foreach (Token token in tokens)
            {
                //state
                sb.Replace(token.TokenString, token.Value);
            }
            
            return sb.ToString();
        }
    }

    #endregion ENTNotificationEO

    #region ENTNotificationEOList

    [Serializable]
    public class ENTNotificationEOList : ENTBaseEOList<ENTNotificationEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTNotificationData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTNotification> eNTNotifications)
        {
            if (eNTNotifications.Count > 0)
            {
                foreach (ENTNotification eNTNotification in eNTNotifications)
                {
                    ENTNotificationEO newENTNotificationEO = new ENTNotificationEO();
                    newENTNotificationEO.MapEntityToProperties(eNTNotification);
                    this.Add(newENTNotificationEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion ENTNotificationEOList

    public class Token
    {
        public string TokenString { get; set; }
        public string Value { get; set; }
    }
}
