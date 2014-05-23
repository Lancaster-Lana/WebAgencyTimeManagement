using System;
using System.Collections.Generic;
using System.Linq;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class ENTMenuItemData : ENTBaseData<ENTMenuItem> 
    {
        public override List<ENTMenuItem> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTMenuItemSelectAll().ToList();
            }
        }

        public override ENTMenuItem Select(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            throw new NotImplementedException();
        }
    }
}
