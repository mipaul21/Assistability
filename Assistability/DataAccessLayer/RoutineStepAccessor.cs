/// <summary>
/// Your Name: Whitney Vinson
/// Created: 2021/02/19
/// 
/// The methods for accessing the Routine Steps.
/// 
/// </summary>
///
/// <remarks>
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/19
/// </remarks>

using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoutineStepAccessor : IRoutineStepAccessor
    {
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The accessor method to select all routine steps.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <exception cref="SqlException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Returns rows affected</returns>

        public List<RoutineStep> SelectAllRoutineSteps()
        {
            List<RoutineStep> routineSteps = new List<RoutineStep>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_routine_steps", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var routineStep = new RoutineStep()
                        {
                            RoutineStepID = reader.GetInt32(0),
                            RoutineName = reader.GetString(1),
                            RoutineStepName = reader.GetString(2),
                            RoutineStepDescription = reader.GetString(3),
                            RoutineStepEntryDate = reader.GetDateTime(4),
                            RoutineStepEditDate = reader.GetDateTime(5),
                            RoutineStepRemovalDate = reader.GetDateTime(6),
                            RoutineStepOrderNumber = reader.GetInt32(7),
                            Active = reader.GetBoolean(8),
                        };
                        routineSteps.Add(routineStep);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return routineSteps;
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The accessor method to retrieve all active routine steps.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="active">Retriving by active if true</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Returns list of active steps</returns>

        public List<RoutineStep> SelectRoutineStepsByActive(bool active = true)
        {
            List<RoutineStep> routineSteps = new List<RoutineStep>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_routine_steps_by_active", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var routineStep = new RoutineStep()
                        {
                            RoutineStepID = reader.GetInt32(0),
                            RoutineName = reader.GetString(1),
                            RoutineStepName = reader.GetString(2),
                            RoutineStepDescription = reader.GetString(3),
                            RoutineStepEntryDate = reader.GetDateTime(4),
                            RoutineStepEditDate = reader.GetDateTime(5),
                            RoutineStepRemovalDate = reader.GetDateTime(6),
                            RoutineStepOrderNumber = reader.GetInt32(7),
                            Active = reader.GetBoolean(8),
                        };
                        routineSteps.Add(routineStep);
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
            return routineSteps;
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The accessor method to retrieve all active routine steps.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="active">Retriving routine step by routine name and active if true</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Returns list of active steps</returns>
        public List<RoutineStep> SelectActiveRoutineStepsByRoutineName(string routineName, bool active = true)
        {
            List<RoutineStep> routineSteps = new List<RoutineStep>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_routine_steps_by_routine_name", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);
            cmd.Parameters.AddWithValue("@Active", active);

            cmd.Parameters["@RoutineName"].Value = routineName;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var routineStep = new RoutineStep()
                        {
                            RoutineStepID = reader.GetInt32(0),
                            RoutineName = reader.GetString(1),
                            RoutineStepName = reader.GetString(2),
                            RoutineStepDescription = reader.GetString(3),
                            RoutineStepEntryDate = reader.GetDateTime(4),
                            RoutineStepEditDate = reader.GetDateTime(5),
                            RoutineStepRemovalDate = reader.GetDateTime(6),
                            RoutineStepOrderNumber = reader.GetInt32(7),
                            Active = reader.GetBoolean(8),
                        };
                        routineSteps.Add(routineStep);
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
            return routineSteps;
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The accessor method to insert a routine step.
        /// </summary>
        ///
        /// <remarks>
        /// Updater: William Clark
        /// Updated: 2021/03/30
        /// Update: Fixed procedure to include not nullable column RoutineName
        /// </remarks>
        /// <param name="routineStep">Inserting a new routine step</param>
        /// <exception cref="ApplicationException">Insert Fails ("Record not created")</exception>
        /// <returns>Returns rows affected</returns>

        public int InsertNewRoutineStep(RoutineStep routineStep)
        {
            int routineStepID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_routine_step", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoutineStepEntryDate", DateTime.Now);

            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@RoutineStepName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@RoutineStepDescription", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@RoutineStepOrderNumber", SqlDbType.Int);

            cmd.Parameters["@RoutineName"].Value = routineStep.RoutineName;
            cmd.Parameters["@RoutineStepName"].Value = routineStep.RoutineStepName;
            cmd.Parameters["@RoutineStepDescription"].Value = routineStep.RoutineStepDescription;
            cmd.Parameters["@RoutineStepOrderNumber"].Value = routineStep.RoutineStepOrderNumber;

            try
            {
                conn.Open();
                routineStepID = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return routineStepID;
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The accessor method to update a routine step.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldRoutineStep">Old routine step</param>
        /// <param name="newRoutineStep">New routine step</param>
        /// <exception cref="ApplicationException">Update Fails("Record not created")</exception>
        /// <returns>Returns rows affected</returns>

        public int UpdateRoutineStep(RoutineStep oldRoutineStep, RoutineStep newRoutineStep)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_routine_step", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RoutineStepID", SqlDbType.Int);
            cmd.Parameters.Add("@NewRoutineName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewRoutineStepName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewRoutineStepDescription", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@NewRoutineStepEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewRoutineStepEditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewRoutineStepRemovalDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewRoutineStepOrderNumber", SqlDbType.Int);
            cmd.Parameters.Add("@OldRoutineName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldRoutineStepName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldRoutineStepDescription", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@OldRoutineStepEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldRoutineStepEditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldRoutineStepRemovalDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldRoutineStepOrderNumber", SqlDbType.Int);

            cmd.Parameters["@RoutineStepID"].Value = oldRoutineStep.RoutineStepID;
            cmd.Parameters["@NewRoutineName"].Value = oldRoutineStep.RoutineName;
            cmd.Parameters["@NewRoutineStepName"].Value = newRoutineStep.RoutineStepName;
            cmd.Parameters["@NewRoutineStepDescription"].Value = newRoutineStep.RoutineStepDescription;
            cmd.Parameters["@NewRoutineStepEntryDate"].Value = oldRoutineStep.RoutineStepEntryDate;
            cmd.Parameters["@NewRoutineStepEditDate"].Value = DateTime.Now;
            cmd.Parameters["@NewRoutineStepRemovalDate"].Value = oldRoutineStep.RoutineStepRemovalDate;
            cmd.Parameters["@NewRoutineStepOrderNumber"].Value = newRoutineStep.RoutineStepOrderNumber;
            cmd.Parameters["@OldRoutineName"].Value = oldRoutineStep.RoutineName;
            cmd.Parameters["@OldRoutineStepName"].Value = oldRoutineStep.RoutineStepName;
            cmd.Parameters["@OldRoutineStepDescription"].Value = oldRoutineStep.RoutineStepDescription;
            cmd.Parameters["@OldRoutineStepEntryDate"].Value = oldRoutineStep.RoutineStepEntryDate;
            cmd.Parameters["@OldRoutineStepEditDate"].Value = oldRoutineStep.RoutineStepEditDate;
            cmd.Parameters["@OldRoutineStepRemovalDate"].Value = oldRoutineStep.RoutineStepRemovalDate;
            cmd.Parameters["@OldRoutineStepOrderNumber"].Value = oldRoutineStep.RoutineStepOrderNumber;

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
        /// William Clark
        /// Created: 2021/04/08
        /// 
        /// The interface method to routine step completions by day by routine name
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="name">The routine name that is being pulled from the database</param>
        /// <param name="date">The day for which to select completions</param>
        /// <exception cref="ApplicationException">Retieval Fails ("Data not Available")</exception>
        /// <returns>List of RoutineStepId's which have been completed on a given date</returns>
        public List<int> SelectRoutineStepCompletionsByDayByRoutineName(string name, DateTime date)
        {
            List<int> result = new List<int>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_completed_routinestepids_by_day", conn);

            cmd.Parameters.AddWithValue("@SelectedDate", date);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var routineStepId = reader.GetInt32(0);
                        result.Add(routineStepId);
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

            return result;
        }
    }
}
