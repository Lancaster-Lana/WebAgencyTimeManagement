using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTReportData : ENTBaseData<ENTReport>
    {
        #region Overrides

        public override List<ENTReport> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTReportSelectAll().ToList();
            }
        }

        public override ENTReport Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTReportSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTReportDelete(id);
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string reportName, string fileName, string objectName, string description, string subReportObjectName, string subReportMethodName, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, reportName, fileName, objectName, description, subReportObjectName, subReportMethodName, insertENTUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string reportName, string fileName, string objectName, string description, string subReportObjectName, string subReportMethodName, int insertENTUserAccountId)
        {
            int? eNTReportId = 0;

            db.ENTReportInsert(ref eNTReportId, reportName, fileName, objectName, description, subReportObjectName, subReportMethodName, insertENTUserAccountId);

            return Convert.ToInt32(eNTReportId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, int eNTReportId, string reportName, string fileName, string objectName, string description, string subReportObjectName, string subReportMethodName, int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, eNTReportId, reportName, fileName, objectName, description, subReportObjectName, subReportMethodName, updateENTUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int eNTReportId, string reportName, string fileName, string objectName, string description, string subReportObjectName, string subReportMethodName, int updateENTUserAccountId, Binary version)
        {
            int rowsAffected = db.ENTReportUpdate(eNTReportId, reportName, fileName, objectName, description, subReportObjectName, subReportMethodName, updateENTUserAccountId, version);
            return rowsAffected == 1;
        }

        #endregion Update

    }
}