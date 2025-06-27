using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HospitalManagementSystem.util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string propertyFilePath)
        {
            string connectionString = DBPropertyUtil.GetConnectionString(propertyFilePath);
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection Error: " + ex.Message);
                return null;
            }
        }
    }
}
