using System;
using System.Linq;
using System.Collections.Generic;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class PTORequestTypeData : ENTBaseData<PTORequestType>
    {
        #region Overrides

        public override List<PTORequestType> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.PTORequestTypeSelectAll().ToList();
            }
        }

        public override PTORequestType Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                var pTORequestType = db.PTORequestTypeSelectById(id);

                if (pTORequestType != null)
                {
                    return pTORequestType.Single();
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