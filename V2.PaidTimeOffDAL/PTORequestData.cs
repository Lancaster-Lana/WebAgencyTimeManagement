using System;
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffDAL
{
    public class PTORequestData : ENTBaseData<PTORequest>
    {
        #region Overrides

        public override List<PTORequest> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTORequestSelectAll().ToList();
            }
        }

        public override PTORequest Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTORequestSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.PTORequestDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTUserAccountId, DateTime requestDate, int pTODayTypeId, int pTORequestTypeId, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, eNTUserAccountId, requestDate, pTODayTypeId, pTORequestTypeId, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int eNTUserAccountId, DateTime requestDate, int pTODayTypeId, int pTORequestTypeId, int insertENTUserAccountId)
        {
            int? pTORequestId = 0;

            db.PTORequestInsert(ref pTORequestId, eNTUserAccountId, requestDate, pTODayTypeId, pTORequestTypeId, insertENTUserAccountId);

            return Convert.ToInt32(pTORequestId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int pTORequestId, int eNTUserAccountId, DateTime requestDate, int pTODayTypeId, int pTORequestTypeId, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, pTORequestId, eNTUserAccountId, requestDate, pTODayTypeId, pTORequestTypeId, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int pTORequestId, int eNTUserAccountId, DateTime requestDate, int pTODayTypeId, int pTORequestTypeId, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.PTORequestUpdate(pTORequestId, eNTUserAccountId, requestDate, pTODayTypeId, pTORequestTypeId, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

        public List<PTORequest> SelectPreviousByENTUserAccountId(int ptoRequestId, int entUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTORequestSelectPreviousByENTUserAccountId(ptoRequestId, entUserAccountId).ToList();
            }
        }

        public List<PTORequest> SelectByCurrentOwnerId(int entUserAccountId, string entWorkflowObjectname)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTORequestSelectByCurrentOwner(entUserAccountId, entWorkflowObjectname).ToList();
            }
        }

        public PTORequestSelectByENTUserAccountIdYearResult SelectByENTUserAccountIdYear(int ptoRequestId, int entUserAccountId, short year)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTORequestSelectByENTUserAccountIdYear(ptoRequestId, entUserAccountId, year).SingleOrDefault();
            }
        }

        public List<PTORequest> SelectByENTUserAccountId(int entUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTORequestSelectByENTUserAccountId(entUserAccountId).ToList();
            }
        }

        public void UpdateCancelled(HRPaidTimeOffDataContext db, int ptoRequestId, bool cancelled)
        {
            db.PTORequestUpdateCancelled(ptoRequestId, cancelled);
        }
    }
}