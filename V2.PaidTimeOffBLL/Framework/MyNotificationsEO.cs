using System;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    [Serializable]
    public class MyNotificationsEO : ENTBaseEO
    {
        private ENTNotificationENTUserAccountEOList _userNotifications;

        public MyNotificationsEO()
        {
            _userNotifications = new ENTNotificationENTUserAccountEOList();
        }

        public ENTNotificationENTUserAccountEOList UserNotifications
        {
            get { return _userNotifications; }
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
                    return _userNotifications.Save(db, ref validationErrors, userAccountId);
                }
                //Didn't pass validation.
                return false;
            }
            throw new Exception("DBAction not Save.");
        }

        protected override void Validate(Agency.PaidTimeOffDAL.HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            
        }

        protected override void DeleteForReal(Agency.PaidTimeOffDAL.HRPaidTimeOffDataContext db)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateDelete(Agency.PaidTimeOffDAL.HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            
        }

        public override bool Load(int id)
        {
            throw new NotImplementedException();
        }

        protected override void MapEntityToCustomProperties(Agency.PaidTimeOffDAL.Framework.IENTBaseEntity entity)
        {
            
        }

        protected override string GetDisplayText()
        {
            return "Not Implemented.";
        }

        public void LoadByENTUserAccountId(int entUserAccountId)
        {
            _userNotifications.Load(entUserAccountId);            
        }
    }
}
