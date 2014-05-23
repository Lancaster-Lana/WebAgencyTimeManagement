using System;
using System.Collections.Generic;
using System.Linq;

using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.PaidTimeOffBLL
{
    #region PTORequestEO

    [Serializable]
    public class PTORequestEO : ENTBaseWorkflowEO
    {
        #region Properties

        public int ENTUserAccountId { get; set; }
        public DateTime RequestDate { get; set; }
        public PTODayTypeBO.PTODayTypeEnum PTODayTypeId { get; set; }
        public PTORequestTypeBO.PTORequestTypeEnum PTORequestTypeId { get; set; }

        public string RequestDateString
        {
            get { return RequestDate.ToStandardDateFormat(); }
        }

        public string RequestTypeString
        {
            get
            {
                string text;

                switch (PTORequestTypeId)
                {
                    case PTORequestTypeBO.PTORequestTypeEnum.Personal:
                        text = "Personal";
                        break;
                    case PTORequestTypeBO.PTORequestTypeEnum.Vacation:
                        text = "Vacation";
                        break;
                    case PTORequestTypeBO.PTORequestTypeEnum.Unpaid:
                        text = "Unpaid";
                        break;
                    default:
                        throw new Exception("PTO Request Type unkown.");
                }

                switch (PTODayTypeId)
                {
                    case PTODayTypeBO.PTODayTypeEnum.AM:
                        text += "-AM";
                        break;
                    case PTODayTypeBO.PTODayTypeEnum.PM:
                        text += "-PM";
                        break;
                    case PTODayTypeBO.PTODayTypeEnum.Full:
                        break;
                    default:
                        throw new Exception("PTO Day Tyep unknown.");
                }

                return text;
            }
        }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            PTORequest pTORequest = new PTORequestData().Select(id);
            MapEntityToProperties(pTORequest);

            //Chapter 12
            StorePropertyValues();
            return true;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            PTORequest pTORequest = (PTORequest)entity;

            ID = pTORequest.PTORequestId;
            ENTUserAccountId = pTORequest.ENTUserAccountId;
            RequestDate = pTORequest.RequestDate;
            PTODayTypeId = (PTODayTypeBO.PTODayTypeEnum)pTORequest.PTODayTypeId;
            PTORequestTypeId = (PTORequestTypeBO.PTORequestTypeEnum)pTORequest.PTORequestTypeId;
            base.LoadWorkflow(this.GetType().AssemblyQualifiedName, ID);
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
                    bool isNewRecord = IsNewRecord();
                    if (isNewRecord)
                    {
                        //Add
                        ID = new PTORequestData().Insert(db, ENTUserAccountId, RequestDate, Convert.ToInt32(PTODayTypeId), Convert.ToInt32(PTORequestTypeId), userAccountId);

                        AuditAdd(db, ref validationErrors, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new PTORequestData().Update(db, ID, ENTUserAccountId, RequestDate, Convert.ToInt32(PTODayTypeId), Convert.ToInt32(PTORequestTypeId), userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            if (isNewRecord)
                                ID = 0;
                            return false;
                        }
                        else
                        {
                            AuditUpdate(db, ref validationErrors, userAccountId);
                        }
                    }

                    if (base.SaveWorkflow(db, ref validationErrors, this, userAccountId))
                    {
                        return true;
                    }
                    else
                    {
                        if (isNewRecord)
                            ID = 0;
                        return false;
                    }
                }
                else
                {
                    //Didn't pass validation.
                    ID = 0;
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
            //Check if this was already selected as a request.
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new PTORequestData().Delete(db, ID);
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
            PTODayTypeId = PTODayTypeBO.PTODayTypeEnum.Full;
            PTORequestTypeId = PTORequestTypeBO.PTORequestTypeEnum.Vacation;
        }

        protected override string GetDisplayText()
        {
            return RequestDate.ToStandardDateFormat();
        }

        #endregion Overrides

        public void SetRequestToCancelled(HRPaidTimeOffDataContext db)
        {
            new PTORequestData().UpdateCancelled(db, ID, true);
        }

        public static void GetUsed(ref double usedPersonalDays, ref double usedVacationDays, ref double unpaid,
            int ptoRequestId, int userAccountId, short year)
        {
            var result = new PTORequestData().SelectByENTUserAccountIdYear(ptoRequestId, userAccountId, year);

            if (result != null)
            {
                usedVacationDays = Convert.ToDouble(result.CountOfFullVacation + result.CountOfHalfVacation);
                usedPersonalDays = Convert.ToDouble(result.CountOfFullPersonal + result.CountOfHalfPersonal);
                unpaid = Convert.ToDouble(result.CountOfFullUnpaid + result.CountOfHalfUnPaid);
            }
            else
            {
                usedPersonalDays = 0;
                usedVacationDays = 0;
                unpaid = 0;
            }
        }
    }   

    #endregion PTORequestEO

    #region PTORequestEOList

    [Serializable]
    public class PTORequestEOList : ENTBaseEOList<PTORequestEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new PTORequestData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<PTORequest> pTORequests)
        {
            if (pTORequests.Count > 0)
            {
                foreach (var pTORequest in pTORequests)
                {
                    var newPTORequestEO = new PTORequestEO();
                    newPTORequestEO.MapEntityToProperties(pTORequest);
                    this.Add(newPTORequestEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public void LoadPreviousByENTUserAccountId(int ptoRequestId, int entUserAccountId)
        {
            LoadFromList(new PTORequestData().SelectPreviousByENTUserAccountId(ptoRequestId, entUserAccountId));
        }

        public void LoadByENTUserAccountId(int entUserAccountId)
        {
            LoadFromList(new PTORequestData().SelectByENTUserAccountId(entUserAccountId));
        }

        public List<PTORequestEO> GetByRequestDate(DateTime requestDate)
        {
            var ret =
                from r in this
                where r.RequestDate == requestDate
                select r;

            return ret.ToList();
        }

        public void LoadByCurrentOwnerId(int entUserAccountId)
        {
            LoadFromList(new PTORequestData().SelectByCurrentOwnerId(entUserAccountId, typeof(PTORequestEO).AssemblyQualifiedName));
        }
    }

    #endregion PTORequestEOList
}
