using System;
using System.Collections.Generic;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework.Reports
{
    #region ENTReportBO

    [Serializable]
    public class ENTReportBO : ENTBaseBO
    {
        #region Properties

        public string ReportName { get; set; }
        public string FileName { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public string SubReportObjectName { get; set; }
        public string SubReportMethodName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            var eNTReport = new ENTReportData().Select(id);
            MapEntityToProperties(eNTReport);
            return eNTReport != null;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var eNTReport = (ENTReport)entity;

            ID = eNTReport.ENTReportId;
            ReportName = eNTReport.ReportName;
            FileName = eNTReport.FileName;
            ObjectName = eNTReport.ObjectName;
            Description = eNTReport.Description;
            SubReportObjectName = eNTReport.SubReportObjectName;
            SubReportMethodName = eNTReport.SubReportMethodName;
        }

        protected override string GetDisplayText()
        {
            return ReportName;
        }

        #endregion Overrides
    }

    #endregion ENTReportBO

    #region ENTReportBOList

    [Serializable]
    public class ENTReportBOList : ENTBaseBOList<ENTReportBO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new ENTReportData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<ENTReport> eNTReports)
        {
            if (eNTReports.Count > 0)
            {
                foreach (var eNTReport in eNTReports)
                {
                    var newENTReportBO = new ENTReportBO();
                    newENTReportBO.MapEntityToProperties(eNTReport);
                    this.Add(newENTReportBO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion ENTReportBOList
}
