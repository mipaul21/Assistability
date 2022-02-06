/// <summary>
/// Ryan Taylor
/// Created: 2021/03/30
/// 
/// This class accesses the performance data through
/// the DBConnection class
/// </summary>
/// 
using DataAccessInterfaces;
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PerformanceAccessor : IPerformanceAccessor
    {
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// edits a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="UserIdClient"> the id of the person who will perform the performance</param>
        /// <param name="oldActive">the original state of the performance</param>
        /// <param name="newActive">the new state of the performance</param>
        /// <exception>Performance not reactivated or deactivated</exception>
        /// <returns>an int signifying the rows affected</returns>
        public int DeactivateReactivatePerformance(string performanceName, 
            int UserIdClient, bool oldActive, bool newActive)
        {

            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_or_reactivate_performance", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@PerformanceName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters.Add("@OldActive", SqlDbType.Bit);

            cmd.Parameters["@UserID_client"].Value = UserIdClient;
            cmd.Parameters["@PerformanceName"].Value = performanceName;
            cmd.Parameters["@NewActive"].Value = newActive;
            cmd.Parameters["@OldActive"].Value = oldActive;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("The performance could not be activated or deactivated.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// Inserts a new performance for a client
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="perfomanceDescription"> a detailed description of the performance</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <param name="userIdCreator"> The UserID of the user who created the performance</param>
        /// <exception>Performance not inserted</exception>
        /// <returns>an int signifying the rows affected</returns>
        public int InsertNewPerformance(string performanceName, string perfomanceDescription, int userIdClient, int userIdCreator)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_performance", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@UserIDCreator", SqlDbType.Int);
            cmd.Parameters.Add("@PerformanceName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PerformanceDescription", SqlDbType.NVarChar, 255);

            cmd.Parameters["@UserID_client"].Value = userIdClient;
            cmd.Parameters["@UserIDCreator"].Value = userIdCreator;
            cmd.Parameters["@PerformanceName"].Value = performanceName;
            cmd.Parameters["@PerformanceDescription"].Value = perfomanceDescription;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// selects all performances for a client
        /// </summary>
        /// 
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of perfomance objects related to a client</returns>
        public List<Performance> SelectPerformancesByClient(int userIdClient)
        {
            List<Performance> performances = new List<Performance>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_performances_by_client", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID_client", userIdClient);

            //cmd.Parameters.Add("@UserID_client", SqlDbType.Int);

            //cmd.Parameters["@UserID_client"].Value = userIdClient;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var PerformanceName = reader.GetString(0);
                        var PerformanceDescription = reader.GetString(1);
                        var UserID_client = reader.GetInt32(2);
                        var UserIDCreator = reader.GetInt32(3);
                        var PerformanceEntryDate = reader.GetDateTime(4);
                        DateTime? PerformanceEditDate = null;
                        if (!reader.IsDBNull(5))
                        {
                            PerformanceEditDate = reader.GetDateTime(5);
                        }

                        DateTime? PerformanceRemovalDate = null;
                        if (!reader.IsDBNull(6))
                        {
                            PerformanceRemovalDate = reader.GetDateTime(6);
                        }

                        var Active = reader.GetBoolean(7);
                        var performance = new Performance()
                        {
                            PerformanceName = PerformanceName,
                            PerformanceDescription = PerformanceDescription,
                            UserID_client = UserID_client,
                            UserIDCreator = UserIDCreator,
                            PerformanceEntryDate = PerformanceEntryDate,
                            PerformanceEditDate = PerformanceEditDate,
                            PerformanceRemovalDate = PerformanceRemovalDate,
                            Active = Active,
                        };
                        performances.Add(performance);
                    }
                }
                reader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return performances;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// selects all active or inactive performances for a client
        /// </summary>
        ///         
        /// <param name="active"> deturmins if a performance is deactiveated or not</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of active or inactive perfomance objects related to a client</returns>
        public List<Performance> SelectPerformancesByClientAndActive(int userIdClient, 
            bool active)
        {

            List<Performance> performances = new List<Performance>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_performances_by_client_and_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);

            cmd.Parameters["@Active"].Value = active;
            cmd.Parameters["@UserID_client"].Value = userIdClient;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var PerformanceName = reader.GetString(0);
                        var PerformanceDescription = reader.GetString(1);
                        var UserID_client = reader.GetInt32(2);
                        var UserIDCreator = reader.GetInt32(3);
                        var PerformanceEntryDate = reader.GetDateTime(4);
                        DateTime? PerformanceEditDate = null;
                        if (!reader.IsDBNull(5))
                        {
                            PerformanceEditDate = reader.GetDateTime(5);
                        }

                        DateTime? PerformanceRemovalDate = null;
                        if (!reader.IsDBNull(6))
                        {
                            PerformanceRemovalDate = reader.GetDateTime(6);
                        }

                        var Active = reader.GetBoolean(7);
                        var performance = new Performance()
                        {
                            PerformanceName = PerformanceName,
                            PerformanceDescription = PerformanceDescription,
                            UserID_client = UserID_client,
                            UserIDCreator = UserIDCreator,
                            PerformanceEntryDate = PerformanceEntryDate,
                            PerformanceEditDate = PerformanceEditDate,
                            PerformanceRemovalDate = PerformanceRemovalDate,
                            Active = Active,
                        };

                        performances.Add(performance);
                    }
                }
                reader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return performances;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// updates a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="newPerfomanceDescription"> a new description for the performance</param>
        /// <param name="newUserIdClient"> the new id of the person who will perform the performance</param>
        /// <param name="oldPerfomanceDescription">the original description for the performance</param>
        /// <param name="oldUserIdClient"> the original id of the person who will perform the performance</param>
        /// <exception>Performance not created</exception>
        /// <returns>an int signifying the rows affected</returns>
        public int UpdatePerformance(string performanceName, string newPerfomanceDescription, int newUserIdClient, string oldPerfomanceDescription, int oldUserIdClient)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_performance", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@NewUserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@OldUserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@PerformanceName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewPerformanceDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@OldPerformanceDescription", SqlDbType.NVarChar, 255);

            cmd.Parameters["@NewUserID_client"].Value = newUserIdClient;
            cmd.Parameters["@OldUserID_client"].Value = oldUserIdClient;
            cmd.Parameters["@PerformanceName"].Value = performanceName;
            cmd.Parameters["@NewPerformanceDescription"].Value = newPerfomanceDescription;
            cmd.Parameters["@OldPerformanceDescription"].Value = oldPerfomanceDescription;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("The performance could not be updated.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
