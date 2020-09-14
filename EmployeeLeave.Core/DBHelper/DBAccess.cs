using System.Configuration;
using System.Data.SqlClient;


namespace EmployeeLeave.Core.DBHelper
{
    public class DBAccess
    {
        public static SqlConnection GetOpenConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeLeave"].ConnectionString;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            return conn;
        }
    }
}
