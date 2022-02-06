using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataViewModels;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class AttGoalAccessor : IAttGoalAccessor
    {
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Deactivates/unassigns an attainment goal.  Uses sp_deactivate_attainment_goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="attGoalName"></param>
        /// <returns></returns>
        public int DeactivateAttainmentGoal(int userID_client, string attGoalName, DateTime attGoalEntryDate)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_attainment_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter with value
            cmd.Parameters.AddWithValue("@AttGoalName", attGoalName);
            cmd.Parameters.AddWithValue("@UserID_client", userID_client);
            cmd.Parameters.AddWithValue("@AttGoalEntryDate", attGoalEntryDate);

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
        /// Created: 2021/03/28
        /// ///
        /// Accessor to insert an attainment goal into the database. Uses sp_create_attainment_goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="attGoal"></param>
        /// <returns></returns>
        public int InsertAttainmentGoal(AttGoalViewModel attGoal)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_create_attainment_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@UserID_admin", SqlDbType.Int);
            cmd.Parameters.Add("@AttGoalName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@AttGoalDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@AttGoalTargetDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@AttGoalEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@PerformanceFrequency", SqlDbType.Int);
            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PerformanceName", SqlDbType.NVarChar, 50);

            

            // set parameter values
            cmd.Parameters["@UserID_client"].Value = attGoal.UserID_client;
            cmd.Parameters["@UserID_admin"].Value = attGoal.UserID_admin;
            cmd.Parameters["@AttGoalName"].Value = attGoal.AttGoalName;
            cmd.Parameters["@AttGoalDescription"].Value = attGoal.AttGoalDescription;
            cmd.Parameters["@AttGoalTargetDate"].Value = attGoal.AttGoalTargetDate;
            cmd.Parameters["@AttgoalEntryDate"].Value = DateTime.Now;
            cmd.Parameters["@PerformanceFrequency"].Value = attGoal.PerformanceFrequency;
            cmd.Parameters["@AwardName"].Value = attGoal.AwardName;
            cmd.Parameters["@PerformanceName"].Value = attGoal.PerformanceName;

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
        /// Created: 2021/03/28
        /// ///
        /// Reactivates/assigns an attainment goals it uses sp_reactivate_attainment_goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="attGoalName"></param>
        /// <returns></returns>
        public int ReactivateAttainmentGoal(int userID_client, string attGoalName, DateTime attGoalEntryDate)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_attainment_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter with value
            cmd.Parameters.AddWithValue("@UserID_client", userID_client);
            cmd.Parameters.AddWithValue("@AttGoalName", attGoalName);
            cmd.Parameters.AddWithValue("@AttGoalEntryDate", attGoalEntryDate);

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
        /// Created: 2021/03/28
        /// ///
        /// Gets a list of attainment goals based on userID and active.  Uses sp_select_attainment_goals_by_active
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<AttGoalViewModel> SelectAttainmentGoalsByActive(int userID_client, bool active)
        {
            List<AttGoalViewModel> attGoals = new List<AttGoalViewModel>();

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_attainment_goal_by_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters with value shortcut
            cmd.Parameters.AddWithValue("@userID_client", userID_client);
            cmd.Parameters.AddWithValue("@active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var attGoal = new AttGoalViewModel()
                        {
                            UserID_client = reader.GetInt32(0),
                            UserID_admin = reader.GetInt32(1),
                            AttGoalName = reader.GetString(2),
                            AttGoalDescription = reader.GetString(3),
                            AttGoalTargetDate = reader.GetDateTime(4),
                            AttGoalEntryDate = reader.GetDateTime(5),
                            PerformanceFrequency = reader.GetInt32(6),
                            AwardName = reader.GetString(7),
                            PerformanceName = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        };
                        attGoals.Add(attGoal);
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
            return attGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Gets a list of habitual goals based on userID, uses sp_select_attainment_goal_by_userID_client
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        public List<AttGoalViewModel> SelectAttainmentGoalsByUserIDClient(int userID_client)
        {
            List<AttGoalViewModel> attGoals = new List<AttGoalViewModel>();

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_attainment_goal_by_userID_client", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters with value shortcut
            cmd.Parameters.AddWithValue("@userID_client", userID_client);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var attGoal = new AttGoalViewModel()
                        {
                            UserID_client = reader.GetInt32(0),
                            UserID_admin = reader.GetInt32(1),
                            AttGoalName = reader.GetString(2),
                            AttGoalDescription = reader.GetString(3),
                            AttGoalTargetDate = reader.GetDateTime(4),
                            AttGoalEntryDate = reader.GetDateTime(5),
                            PerformanceFrequency = reader.GetInt32(6),
                            AwardName = reader.GetString(7),
                            PerformanceName = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        };
                        attGoals.Add(attGoal);
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
            return attGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Accessor for updating an attainment goal record using sp_update_attainment_goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="oldAttGoal"></param>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        public int UpdateAttainmentGoal(AttGoalViewModel oldAttGoal, AttGoalViewModel newAttGoal)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_attainment_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Paramters for update_habitual_goal
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@AttGoalName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@AttGoalEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewAttGoalDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewAttGoalTargetDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewAttGoalEditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewPerformanceFrequency", SqlDbType.Int);
            cmd.Parameters.Add("@NewAwardName", SqlDbType.NVarChar, 50);

            cmd.Parameters["@UserID_client"].Value = oldAttGoal.UserID_client;
            cmd.Parameters["@AttGoalName"].Value = oldAttGoal.AttGoalName;
            cmd.Parameters["@AttGoalEntryDate"].Value = oldAttGoal.AttGoalEntryDate;
            cmd.Parameters["@NewAttGoalDescription"].Value = newAttGoal.AttGoalDescription;
            cmd.Parameters["@NewAttGoalTargetDate"].Value = oldAttGoal.AttGoalTargetDate;
            cmd.Parameters["@NewAttGoalEditDate"].Value = DateTime.Today;
            cmd.Parameters["@NewPerformanceFrequency"].Value = newAttGoal.PerformanceFrequency;
            cmd.Parameters["@NewAwardName"].Value = newAttGoal.AwardName;

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
