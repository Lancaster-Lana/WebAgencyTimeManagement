using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffDAL
{
    public class PTODayTypeData : ENTBaseData<PTODayType>
    {
        #region Overrides

        public override List<PTODayType> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTODayTypeSelectAll().ToList();
            }
        }

        public override PTODayType Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var pTODayType = db.PTODayTypeSelectById(id);

                if (pTODayType != null)
                {
                    return pTODayType.Single();
                }
                return null;
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            throw new NotImplementedException();
        }

        #endregion Overrides
    }
}