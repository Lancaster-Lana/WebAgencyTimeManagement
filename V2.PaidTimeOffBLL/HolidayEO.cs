using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.PaidTimeOffBLL
{
    #region HolidayEO

    [Serializable]
    public class HolidayEO : ENTBaseEO
    {
        #region Properties

        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            Holiday holiday = new HolidayData().Select(id);
            MapEntityToProperties(holiday);
            return true;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            Holiday holiday = (Holiday)entity;

            ID = holiday.HolidayId;
            HolidayName = holiday.HolidayName;
            HolidayDate = holiday.HolidayDate;
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
                        ID = new HolidayData().Insert(db, HolidayName, HolidayDate, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new HolidayData().Update(db, ID, HolidayName, HolidayDate, userAccountId, Version))
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
            if (HolidayName.Trim() == "")
            {
                validationErrors.Add("The name is required.");
            }

            if (HolidayDate == DateTime.MinValue)
            {
                validationErrors.Add("The date is required.");
            }
            else
            {
                if (new HolidayData().IsDuplicateHolidayDate(db, ID, HolidayDate))
                {
                    validationErrors.Add("The date must be unique.");
                }
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new HolidayData().Delete(db, ID);
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
            HolidayDate = DateTime.Today;
        }

        protected override string GetDisplayText()
        {
            return HolidayName;
        }

        #endregion Overrides
    }

    #endregion HolidayEO

    #region HolidayEOList

    [Serializable]
    public class HolidayEOList : ENTBaseEOList<HolidayEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new HolidayData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<Holiday> holidays)
        {
            if (holidays.Count > 0)
            {
                foreach (Holiday holiday in holidays)
                {
                    HolidayEO newHolidayEO = new HolidayEO();
                    newHolidayEO.MapEntityToProperties(holiday);
                    this.Add(newHolidayEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public bool IsHoliday(DateTime date)
        {
            return (GetHoliday(date) != null);
        }

        public HolidayEO GetHoliday(DateTime date)
        {
            return this.SingleOrDefault(h => h.HolidayDate == date);
        }
    }

    #endregion HolidayEOList
}
