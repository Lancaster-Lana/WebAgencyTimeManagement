using System;
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffDAL
{
    public class PTOVacationBankData : ENTBaseData<PTOVacationBank>
    {
        #region Overrides

        public override List<PTOVacationBank> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTOVacationBankSelectAll().ToList();
            }
        }

        public override PTOVacationBank Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var pTOVacationBank = db.PTOVacationBankSelectById(id);

                if (pTOVacationBank != null)
                {
                    return pTOVacationBank.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.PTOVacationBankDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTUserAccountId, short vacationYear, byte personalDays, byte vacationDays, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTUserAccountId, vacationYear, personalDays, vacationDays, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTUserAccountId, short vacationYear, byte personalDays, byte vacationDays, int insertENTUserAccountId)
        {
            int? pTOVacationBankId = 0;
            db.PTOVacationBankInsert(ref pTOVacationBankId, eNTUserAccountId, vacationYear, personalDays, vacationDays, insertENTUserAccountId);
            return Convert.ToInt32(pTOVacationBankId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int pTOVacationBankId, int eNTUserAccountId, short vacationYear, byte personalDays, byte vacationDays, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, pTOVacationBankId, eNTUserAccountId, vacationYear, personalDays, vacationDays, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int pTOVacationBankId, int eNTUserAccountId, short vacationYear, byte personalDays, byte vacationDays, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.PTOVacationBankUpdate(pTOVacationBankId, eNTUserAccountId, vacationYear, personalDays, vacationDays, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public bool IsDuplicate(HRPaidTimeOffDataContext db, int ptoVacationBankId, int entUserAccountId, short vacationYear)
        {
            return db.PTOVacationBankIsDuplicate(ptoVacationBankId, entUserAccountId, vacationYear).Single().CountOfDuplicates > 0;
        }

        public List<PTOVacationBankSelectDistinctYearsResult> SelectDistinctYears()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTOVacationBankSelectDistinctYears().ToList();
            }
        }

        public void CopyYear(short fromYear, short toYear, int userAcountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                db.PTOVacationBankCopyYear(fromYear, toYear, userAcountId);
            }
        }

        public PTOVacationBank SelectByUserAccountIdYear(int userAccountId, short year)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var pTOVacationBank = db.PTOVacationBankSelectByUserAccountIdYear(userAccountId, year);

                if (pTOVacationBank != null)
                {
                    return pTOVacationBank.SingleOrDefault();
                }
                return null;
            }
        }
    }
}