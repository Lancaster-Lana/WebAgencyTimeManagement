using System;
using System.Data.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public interface IENTBaseEntity
    {
        DateTime InsertDate { get; set; }
        int InsertENTUserAccountId { get; set; }
        DateTime UpdateDate { get; set; }
        int UpdateENTUserAccountId { get; set; }
        Binary Version { get; set; }   
    }
}
