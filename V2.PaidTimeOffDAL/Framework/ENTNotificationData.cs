using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTNotificationData : ENTBaseData<ENTNotification>
    {
        #region Overrides

        public override List<ENTNotification> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTNotificationSelectAll().ToList();
            }
        }

        public override ENTNotification Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return Select(db, id);
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTNotificationDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string description, string fromEmailAddress, string subject, string body, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, description, fromEmailAddress, subject, body, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string description, string fromEmailAddress, string subject, string body, int insertENTUserAccountId)
        {
            int? eNTNotificationId = 0;

            db.ENTNotificationInsert(ref eNTNotificationId, description, fromEmailAddress, subject, body, insertENTUserAccountId);

            return Convert.ToInt32(eNTNotificationId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTNotificationId, string description, string fromEmailAddress, string subject, string body, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTNotificationId, description, fromEmailAddress, subject, body, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTNotificationId, string description, string fromEmailAddress, string subject, string body, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTNotificationUpdate(eNTNotificationId, description, fromEmailAddress, subject, body, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public ENTNotification Select(HRPaidTimeOffDataContext db, int id)
        {
            return db.ENTNotificationSelectById(id).SingleOrDefault();
        }

        public ENTNotification SelectByIdENTUserAccountId(HRPaidTimeOffDataContext db, int notificationId, int entUserAccountId)
        {
            return db.ENTNotificationSelectByIdENTUserAccountId(notificationId, entUserAccountId).SingleOrDefault();
        }
    }
}