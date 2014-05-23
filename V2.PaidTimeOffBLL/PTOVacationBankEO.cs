using System;
using System.Collections;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.PaidTimeOffBLL
{
    #region PTOVacationBankEO

    [Serializable]
    public class PTOVacationBankEO : ENTBaseEO
    {
        #region Properties

        public int ENTUserAccountId { get; set; }
        public short VacationYear { get; set; }
        public byte PersonalDays { get; set; }
        public byte VacationDays { get; set; }

        public string UserName { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            var pTOVacationBank = new PTOVacationBankData().Select(id);
            MapEntityToProperties(pTOVacationBank);
            return true;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var pTOVacationBank = (PTOVacationBank)entity;
            ID = pTOVacationBank.PTOVacationBankId;
            ENTUserAccountId = pTOVacationBank.ENTUserAccountId;
            VacationYear = pTOVacationBank.VacationYear;
            PersonalDays = pTOVacationBank.PersonalDays;
            VacationDays = pTOVacationBank.VacationDays;
            UserName = pTOVacationBank.UserName;
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
                        ID = new PTOVacationBankData().Insert(db, ENTUserAccountId, VacationYear, PersonalDays, VacationDays, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new PTOVacationBankData().Update(db, ID, ENTUserAccountId, VacationYear, PersonalDays, VacationDays, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }
                    return true;
                }
                //Didn't pass validation.
                return false;
            }
            throw new Exception("DBAction not Save.");
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            //Check if this employee already has a record for the same year.
            if (new PTOVacationBankData().IsDuplicate(db, ID, ENTUserAccountId, VacationYear))
            {
                validationErrors.Add("The vacation bank has already been entered for this user for " + VacationYear + ".");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new PTOVacationBankData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            PersonalDays = 3;
            VacationDays = 10;
            VacationYear = Convert.ToInt16(DateTime.Today.Year);
        }

        protected override string GetDisplayText()
        {
            return UserName;
        }

        #endregion Overrides

        public static ArrayList GetDistinctYears()
        {
            var years = new ArrayList();

            var ptoYears = new PTOVacationBankData().SelectDistinctYears();

            foreach (var ptoYear in ptoYears)
            {
                years.Add(ptoYear.VacationYear);
            }

            return years;
        }

        public static void CopyYear(short fromYear, short toYear, int userAccountId)
        {
            new PTOVacationBankData().CopyYear(fromYear, toYear, userAccountId);
        }

        public void Load(int userAccountId, short year)
        {
            var pTOVacationBank = new PTOVacationBankData().SelectByUserAccountIdYear(userAccountId, year);
            MapEntityToProperties(pTOVacationBank);
        }
    }

    #endregion PTOVacationBankEO

    #region PTOVacationBankEOList

    [Serializable]
    public class PTOVacationBankEOList : ENTBaseEOList<PTOVacationBankEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new PTOVacationBankData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<PTOVacationBank> pTOVacationBanks)
        {
            if (pTOVacationBanks.Count > 0)
            {
                foreach (var pTOVacationBank in pTOVacationBanks)
                {
                    var newPTOVacationBankEO = new PTOVacationBankEO();
                    newPTOVacationBankEO.MapEntityToProperties(pTOVacationBank);
                    this.Add(newPTOVacationBankEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion PTOVacationBankEOList
}
