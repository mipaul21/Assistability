///<summary>
/// Becky Baenziger
/// Created: 2021/02/18
/// ///
/// Accessor for using the stored procedure, implements the accessor interface
/// </summary>
/// <remarks>
/// Updater Name:
/// Update Date:
/// </remarks>

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
    public class HabGoalAccessor : IHabGoalAccessor
    {
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Accessor to Insert a Habitual Goal into the database. Uses sp_create_habitual_goal
        /// </summary>
        /// <param name="habGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        public int InsertHabitualGoal(HabGoalViewModel habGoal)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_create_habitual_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@UserID_admin", SqlDbType.Int);
            cmd.Parameters.Add("@HabGoalName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@HabGoalDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@HabGoalTargetDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@HabGoalEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@RoutineFrequency", SqlDbType.Int);
            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);

            // set parameter values
            cmd.Parameters["@UserID_client"].Value = habGoal.UserID_client;
            cmd.Parameters["@UserID_admin"].Value = habGoal.UserID_admin;
            cmd.Parameters["@HabGoalName"].Value = habGoal.HabGoalName;
            cmd.Parameters["@HabGoalDescription"].Value = habGoal.HabGoalDescription;
            cmd.Parameters["@HabGoalTargetDate"].Value = habGoal.HabGoalTargetDate;
            cmd.Parameters["@HabgoalEntryDate"].Value = DateTime.Now;
            cmd.Parameters["@RoutineFrequency"].Value = habGoal.RoutineFrequency;
            cmd.Parameters["@AwardName"].Value = habGoal.AwardName;
            cmd.Parameters["@RoutineName"].Value = habGoal.RoutineName;

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
        /// Created: 2021/03/12
        /// ///
        /// Access for Update Habitual Goal using sp_update_habitual_goal
        /// </summary>
        /// <param name="oldHabGoal"></param>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        public int UpdateHabitualGoal(HabGoalViewModel oldHabGoal, HabGoalViewModel newHabGoal)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_habitual_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Paramters for update_habitual_goal
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@HabGoalName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@HabGoalEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewHabGoalDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewHabGoalEditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewRoutineFrequency", SqlDbType.Int);
            cmd.Parameters.Add("@NewAwardName", SqlDbType.NVarChar, 50);

            cmd.Parameters["@UserID_client"].Value = oldHabGoal.UserID_client;
            cmd.Parameters["@HabGoalName"].Value = oldHabGoal.HabGoalName;
            cmd.Parameters["@HabGoalEntryDate"].Value = oldHabGoal.HabGoalEntryDate;
            cmd.Parameters["@NewHabGoalDescription"].Value = newHabGoal.HabGoalDescription;
            cmd.Parameters["@NewHabGoalEditDate"].Value = DateTime.Today;
            cmd.Parameters["@NewRoutineFrequency"].Value = newHabGoal.RoutineFrequency;
            cmd.Parameters["@NewAwardName"].Value = newHabGoal.AwardName;

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
        /// Created: 2021/02/18
        /// ///
        /// Deactivates an habitual goal/unassigns it. Uses sp_deactivate_habitual_goal
        /// </summary>
        /// <param name="habGoalName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        public int DeactivateHabitualGoal(int userID_client, string habGoalName, DateTime habGoalEntryDate)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_habitual_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter with value
            cmd.Parameters.AddWithValue("@HabGoalName", habGoalName);
            cmd.Parameters.AddWithValue("@UserID_client", userID_client);
            cmd.Parameters.AddWithValue("@HabGoalEntryDate", habGoalEntryDate);

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
        /// Created: 2021/02/18
        /// ///
        /// Gest a list of Habitual Goals based on UserID, uses sp_select_habitual_goal_by_userID_client
        /// 
        /// </summary>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        public List<HabGoalViewModel> SelectHabitualGoalsByUserIDClient(int userID_client)
        {
            List<HabGoalViewModel> habGoals = new List<HabGoalViewModel>();

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_habitual_goal_by_userID_client", conn);
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
                        var habGoal = new HabGoalViewModel()
                        {
                            UserID_client = reader.GetInt32(0),
                            UserID_admin = reader.GetInt32(1),
                            HabGoalName = reader.GetString(2),
                            HabGoalDescription = reader.GetString(3),
                            HabGoalTargetDate = reader.GetDateTime(4),
                            HabGoalEntryDate = reader.GetDateTime(5),
                            RoutineFrequency = reader.GetInt32(6),
                            AwardName = reader.GetString(7),
                            RoutineName = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        };
                        habGoals.Add(habGoal);
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
            return habGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Reactivates an habitual goal/assigns it uses sp_reactivate_habitual_goal
        /// </summary>
        /// <param name="habGoalName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        public int ReactivateHabitualGoal(int userId_client, string habGoalName, DateTime habGoalEntryDate)
        {
            int result = 0;

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_habitual_goal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameter with value
            cmd.Parameters.AddWithValue("@UserID_client", userId_client);
            cmd.Parameters.AddWithValue("@HabGoalName", habGoalName);
            cmd.Parameters.AddWithValue("@HabGoalEntryDate", habGoalEntryDate);

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
        /// Created: 2021/03/02
        /// ///
        /// Gets a list of habitual goals based on active.  Uses sp_select_habitual_goals_by_active
        /// </summary>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        public List<HabGoalViewModel> SelectHabitualGoalsByActive(int userID_client, bool active)
        {
            List<HabGoalViewModel> habGoals = new List<HabGoalViewModel>();

            // connection, command, command type
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_habitual_goal_by_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters with value shortcut
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
                        var habGoal = new HabGoalViewModel()
                        {
                            UserID_client = reader.GetInt32(0),
                            UserID_admin = reader.GetInt32(1),
                            HabGoalName = reader.GetString(2),
                            HabGoalDescription = reader.GetString(3),
                            HabGoalTargetDate = reader.GetDateTime(4),
                            HabGoalEntryDate = reader.GetDateTime(5),
                            RoutineFrequency = reader.GetInt32(6),
                            AwardName = reader.GetString(7),
                            RoutineName = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        };
                        habGoals.Add(habGoal);
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
            return habGoals;
        }
    }
}
