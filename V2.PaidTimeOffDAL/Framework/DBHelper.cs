using System.Configuration;

namespace Agency.PaidTimeOffDAL.Framework
{
    public class DBHelper
    {
        private const string HRPAIDTIMEOFF_CONNSTRING_KEY = "HRPaidTimeOffConnString";

        public static string GetHRPaidTimeOffConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[HRPAIDTIMEOFF_CONNSTRING_KEY].ConnectionString;
        }
    }
}
