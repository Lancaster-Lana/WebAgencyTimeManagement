using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTUserAccountData : ENTBaseData<ENTUserAccount>
    {
        #region Overrides

        public override List<ENTUserAccount> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTUserAccountSelectAll().ToList();                
            }
        }

        public override ENTUserAccount Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return Select(db, id);
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTUserAccountDelete(id);
        }

        #endregion Overrides
                
        #region Insert

        public int Insert(string connectionString, string windowsAccountName, string firstName,
            string lastName, string email, bool isActive, int insertUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, windowsAccountName, firstName, lastName, email, isActive, insertUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string windowsAccountName, string firstName, 
            string lastName, string email, bool isActive, int insertUserAccountId)
        {
            int? entUserAccountId = 0;

            db.ENTUserAccountInsert(ref entUserAccountId, windowsAccountName, firstName, lastName, 
                email, isActive, insertUserAccountId);

            return Convert.ToInt32(entUserAccountId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int userAccountId, string windowsAccountName,
            string firstName, string lastName, string email, bool isActive, int updateUserAccountId,
            Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, userAccountId, windowsAccountName, firstName, lastName, email,
                    isActive, updateUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int userAccountId, string windowsAccountName, 
            string firstName, string lastName, string email, bool isActive, int updateUserAccountId, 
            Binary version)
        {
            int rowsAffected = db.ENTUserAccountUpdate(userAccountId, windowsAccountName, firstName, 
                lastName, email, isActive, updateUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update     
    
        #region Utility Methods

        /// <summary>
        /// Checks to see that window account name is unique.
        /// </summary>        
        /// <returns>Returns true if the windows account name is already in database</returns>
        public bool IsDuplicateWindowsAccountName(HRPaidTimeOffDataContext db, int userAccountId, string windowsAccountName)
        {
            return IsDuplicate(db, "ENTUserAccount", "WindowsAccountName", "ENTUserAccountId", windowsAccountName, userAccountId);
        }

        public bool IsDuplicateEmail(HRPaidTimeOffDataContext db, int userAccountId, string email)
        {
            return IsDuplicate(db, "ENTUserAccount", "Email", "ENTUserAccountId", email, userAccountId);
        }

        #endregion Utility Methods

        public List<ENTUserAccount> SelectByWFOwnerGroupId(int entWFOwnerGroupId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTUserAccountSelectByENTWFOwnerGroupId(entWFOwnerGroupId).ToList();
            }
        }

        public ENTUserAccount Select(HRPaidTimeOffDataContext db, int id)
        {
            return db.ENTUserAccountSelectById(id).SingleOrDefault();
        }
    }
}
