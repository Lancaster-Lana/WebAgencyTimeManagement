using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTEmailData : ENTBaseData<ENTEmail>
    {
        #region Overrides

        public override List<ENTEmail> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTEmailSelectAll().ToList();
            }
        }

        public override ENTEmail Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var eNTEmail = db.ENTEmailSelectById(id);

                if (eNTEmail != null)
                {
                    return eNTEmail.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTEmailDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string toEmailAddress, string cCEmailAddress, string bCCEmailAddress, string fromEmailAddress, string subject, string body, byte emailStatusFlag, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, toEmailAddress, cCEmailAddress, bCCEmailAddress, fromEmailAddress, subject, body, emailStatusFlag, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string toEmailAddress, string cCEmailAddress, string bCCEmailAddress, string fromEmailAddress, string subject, string body, byte emailStatusFlag, int insertENTUserAccountId)
        {
            int? eNTEmailId = 0;

            db.ENTEmailInsert(ref eNTEmailId, toEmailAddress, cCEmailAddress, bCCEmailAddress, fromEmailAddress, subject, body, emailStatusFlag, insertENTUserAccountId);

            return Convert.ToInt32(eNTEmailId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTEmailId, string toEmailAddress, string cCEmailAddress, string bCCEmailAddress, string fromEmailAddress, string subject, string body, byte emailStatusFlag, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTEmailId, toEmailAddress, cCEmailAddress, bCCEmailAddress, fromEmailAddress, subject, body, emailStatusFlag, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTEmailId, string toEmailAddress, string cCEmailAddress, string bCCEmailAddress, string fromEmailAddress, string subject, string body, byte emailStatusFlag, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTEmailUpdate(eNTEmailId, toEmailAddress, cCEmailAddress, bCCEmailAddress, fromEmailAddress, subject, body, emailStatusFlag, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<ENTEmail> SelectByEmailStatusFlag(byte emailStatusFlag)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTEmailSelectByEmailStatusFlag(emailStatusFlag).ToList();
            }
        }
    }
}