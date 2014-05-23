using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTEmailEO

    [Serializable]
    public class ENTEmailEO : ENTBaseEO
    {
        #region Enumerations

        public enum EmailStatusFlagEnum
        {
            NotSent = 0,
            Sent = 1
        }

        #endregion Enumerations

        #region Properties

        public string ToEmailAddress { get; set; }
        public string CCEmailAddress { get; set; }
        public string BCCEmailAddress { get; set; }
        public string FromEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailStatusFlagEnum EmailStatusFlag { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {            
            //Get the entity object from the DAL.
            ENTEmail eNTEmail = new ENTEmailData().Select(id);
            MapEntityToProperties(eNTEmail);
            return true;        
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTEmail eNTEmail = (ENTEmail)entity;

            ID = eNTEmail.ENTEmailId;
            ToEmailAddress = eNTEmail.ToEmailAddress;
            CCEmailAddress = eNTEmail.CCEmailAddress;
            BCCEmailAddress = eNTEmail.BCCEmailAddress;
            FromEmailAddress = eNTEmail.FromEmailAddress;
            Subject = eNTEmail.Subject;
            Body = eNTEmail.Body;
            EmailStatusFlag = (EmailStatusFlagEnum)eNTEmail.EmailStatusFlag;
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
                        ID = new ENTEmailData().Insert(db, ToEmailAddress, CCEmailAddress, BCCEmailAddress, FromEmailAddress, Subject, Body, Convert.ToByte(EmailStatusFlag), userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new ENTEmailData().Update(db, ID, ToEmailAddress, CCEmailAddress, BCCEmailAddress, FromEmailAddress, Subject, Body, Convert.ToByte(EmailStatusFlag), userAccountId, Version))
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
            //throw new NotImplementedException();
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ENTEmailData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //throw new NotImplementedException();
        }

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            return ID.ToString();
        }

        #endregion Overrides
    }

    #endregion ENTEmailEO

    #region ENTEmailEOList

    [Serializable]
    public class ENTEmailEOList : ENTBaseEOList<ENTEmailEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTEmailData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTEmail> eNTEmails)
        {
            if (eNTEmails.Count > 0)
            {
                foreach (ENTEmail eNTEmail in eNTEmails)
                {
                    ENTEmailEO newENTEmailEO = new ENTEmailEO();
                    newENTEmailEO.MapEntityToProperties(eNTEmail);
                    this.Add(newENTEmailEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public void LoadUnsent()
        {
            LoadFromList(new ENTEmailData().SelectByEmailStatusFlag(Convert.ToByte(ENTEmailEO.EmailStatusFlagEnum.NotSent)));
        }
    }

    #endregion ENTEmailEOList
}
