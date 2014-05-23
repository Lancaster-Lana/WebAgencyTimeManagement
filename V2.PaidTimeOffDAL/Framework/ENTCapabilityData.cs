using System;
using System.Collections.Generic;
using System.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTCapabilityData : ENTBaseData<ENTCapability>
    {
        public override List<ENTCapability> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTCapabilitySelectAll().ToList();
            }
        }

        public override ENTCapability Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTCapabilitySelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            throw new NotImplementedException();
        }
    }
}
