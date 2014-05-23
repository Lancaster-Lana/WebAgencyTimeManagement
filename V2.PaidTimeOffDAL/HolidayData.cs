using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class HolidayData : ENTBaseData<Holiday>
    {
        #region Overrides

        public override List<Holiday> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.HolidaySelectAll().ToList();
            }
        }

        public override Holiday Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var holiday = db.HolidaySelectById(id);

                if (holiday != null)
                {
                    return holiday.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.HolidayDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string holidayName, DateTime holidayDate, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, holidayName, holidayDate, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string holidayName, DateTime holidayDate, int insertENTUserAccountId)
        {
            int? holidayId = 0;
            db.HolidayInsert(ref holidayId, holidayName, holidayDate, insertENTUserAccountId);
            return Convert.ToInt32(holidayId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int holidayId, string holidayName, DateTime holidayDate, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, holidayId, holidayName, holidayDate, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int holidayId, string holidayName, DateTime holidayDate, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.HolidayUpdate(holidayId, holidayName, holidayDate, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public bool IsDuplicateHolidayDate(HRPaidTimeOffDataContext db, int holidayId, DateTime holidayDate)
        {
            return IsDuplicate(db, "Holiday", "HolidayDate", "HolidayId", holidayDate, holidayId);
        }
    }
}