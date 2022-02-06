using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;

namespace DataAccessLayer
{
    public class ExtGoalAccessor : IExtGoalAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// changing whats in extGoals in reader, added userID_client, and how added to list.
        /// </remarks>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<ExtGoalViewModel> SelectExtinctionGoalsByActive(int userID_client, bool active)
        {
            List<ExtGoalViewModel> extGoals = new List<ExtGoalViewModel>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_extinction_goal_by_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID_client", userID_client);
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var extGoal = new ExtGoalViewModel()
                        {
                            UserID_client = reader.GetInt32(0),
                            UserID_admin = reader.GetInt32(1),
                            ExtGoalName = reader.GetString(2),
                            ExtGoalDescription = reader.GetString(3),
                            ExtGoalTargetDate = reader.GetDateTime(4),
                            ExtGoalEntryDate = reader.GetDateTime(5),
                            IncidentFrequency = reader.GetInt32(6),
                            AwardName = reader.GetString(7),
                            IncidentName = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        };
                        extGoals.Add(extGoal);
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
            return extGoals;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/24
        /// changed the paprameters, got rid of removal date, edit date, and active, changed some of the data types
        /// </remarks>
        /// <param name="extGoal"></param>
        /// <returns></returns>
        public int InsertExtinctionGoal(ExtGoalViewModel extGoal)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_ext_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ExtGoalName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ExtGoalDescription", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@IncidentName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@IncidentFrequency", SqlDbType.Int, 15);
            cmd.Parameters.Add("@ExtGoalEntryDate", SqlDbType.DateTime, 15);
            cmd.Parameters.Add("@ExtGoalTargetDate", SqlDbType.DateTime, 15);
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int, 15);
            cmd.Parameters.Add("@UserID_admin", SqlDbType.Int, 15);
            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);


            cmd.Parameters["@ExtGoalName"].Value = extGoal.ExtGoalName;
            cmd.Parameters["@ExtGoalDescription"].Value = extGoal.ExtGoalDescription;
            cmd.Parameters["@IncidentName"].Value = extGoal.IncidentName;
            cmd.Parameters["@IncidentFrequency"].Value = extGoal.IncidentFrequency;
            cmd.Parameters["@ExtGoalEntryDate"].Value = DateTime.Now;
            cmd.Parameters["@ExtGoalTargetDate"].Value = extGoal.ExtGoalTargetDate;
            cmd.Parameters["@UserID_client"].Value = extGoal.UserID_client;
            cmd.Parameters["@UserID_admin"].Value = extGoal.UserID_admin;
            cmd.Parameters["@AwardName"].Value = extGoal.AwardName;

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
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// add @Old and @New variables changed all but end try/catch
        /// </remarks>
        /// <param name="oldExtGoal"></param>
        /// <param name="newExtGoal"></param>
        /// <returns></returns>
        public int UpdateExtinctionGoal(ExtGoalViewModel oldExtGoal, ExtGoalViewModel newExtGoal)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_ext_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Paramters for update_habitual_goal
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@ExtGoalName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ExtGoalEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewExtGoalDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewExtGoalEditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewIncidentFrequency", SqlDbType.Int);
            cmd.Parameters.Add("@NewAwardName", SqlDbType.NVarChar, 50);

            cmd.Parameters["@UserID_client"].Value = oldExtGoal.UserID_client;
            cmd.Parameters["@ExtGoalName"].Value = oldExtGoal.ExtGoalName;
            cmd.Parameters["@ExtGoalEntryDate"].Value = oldExtGoal.ExtGoalEntryDate;
            cmd.Parameters["@NewExtGoalDescription"].Value = newExtGoal.ExtGoalDescription;
            cmd.Parameters["@NewExtGoalEditDate"].Value = DateTime.Today;
            cmd.Parameters["@NewIncidentFrequency"].Value = newExtGoal.IncidentFrequency;
            cmd.Parameters["@NewAwardName"].Value = newExtGoal.AwardName;



            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
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
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Gest a list of Extinction Goals based on UserID, uses sp_select_extinction_goal_by_userID_client
        /// 
        /// </summary>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        public List<ExtGoalViewModel> SelectExtinctionGoalsByUserIDClient(int userID_client)
        {
            List<ExtGoalViewModel> extGoals = new List<ExtGoalViewModel>();

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_extinction_goal_by_userID_client", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters with value shortcut
            cmd.Parameters.AddWithValue("@UserID_client", userID_client);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var extGoal = new ExtGoalViewModel()
                        {
                            UserID_client = reader.GetInt32(0),
                            UserID_admin = reader.GetInt32(1),
                            ExtGoalName = reader.GetString(2),
                            ExtGoalDescription = reader.GetString(3),
                            ExtGoalTargetDate = reader.GetDateTime(4),
                            ExtGoalEntryDate = reader.GetDateTime(5),
                            IncidentFrequency = reader.GetInt32(6),
                            AwardName = reader.GetString(7),
                            IncidentName = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        };
                        extGoals.Add(extGoal);
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
            return extGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Reactivates an extinction goal/assigns it uses sp_reactivate_extinction_goal
        /// </summary>
        /// <param name="extGoalName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        public int ReactivateExtinctionGoal(int userID_client, string extGoalName, DateTime extGoalEntryDate)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_extinction_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter with value
            cmd.Parameters.AddWithValue("@UserID_client", userID_client);
            cmd.Parameters.AddWithValue("@ExtGoalName", extGoalName);
            cmd.Parameters.AddWithValue("@ExtGoalEntryDate", extGoalEntryDate);

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
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Deactivates an extinction goal/unassigns it. Uses sp_deactivate_extinction_goal
        /// </summary>
        /// <param name="extGoalName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        public int DeactivateExtinctionGoal(int userID_client, string extGoalName, DateTime extGoalEntryDate)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_extinction_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter with value
            cmd.Parameters.AddWithValue("@ExtGoalName", extGoalName);
            cmd.Parameters.AddWithValue("@UserID_client", userID_client);
            cmd.Parameters.AddWithValue("@ExtGoalEntryDate", extGoalEntryDate);

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


    }
}


