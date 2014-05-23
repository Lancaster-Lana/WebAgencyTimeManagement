using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;

namespace CrystalDecisions.ReportSource
{
    public interface ICachedReport
    {
        bool IsCacheable
        {
            get;
            set;
        }
        bool ShareDBLogonInfo
        {
            get;
            set;
        }
        TimeSpan CacheTimeOut
        {
            get;
            set;
        }
        ReportDocument CreateReport();
        string GetCustomizedCacheKey(RequestContext request);
    }
}