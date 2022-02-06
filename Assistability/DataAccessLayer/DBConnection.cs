/// <summary>
/// Nathaniel Webber
/// Created: 2021/02/05
/// 
/// This class creates a connection to the 
/// databse
/// </summary>
///
/// <remarks>
/// Nathaniel Webber
/// Updated: 2021/02/18
/// First MVP delivered
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Nathaniel Webber
    /// Created: 2021/02/05
    /// 
    /// This method references the sql
    /// </summary>
    ///
    /// <remarks>
    /// Nathaniel Webber
    /// Updated: 2021/02/18 
    /// First MVP delivered
    /// </remarks>
    public class DBConnection
    {
        // This is the only place to connect to the database

        private static string connectionString = @"Data Source=localhost;Initial Catalog=assistability_db;Integrated Security=true";
       //private static string connectionString = @"Data Source=localhost;Initial Catalog=assistability_db;Integrated Security=true";


        public static SqlConnection GetDBConnection()
        {
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
