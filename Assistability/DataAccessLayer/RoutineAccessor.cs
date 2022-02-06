/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Accessor for Routine objects
/// </summary>
///
/// <remarks>
/// </remarks>
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
    public class RoutineAccessor : IRoutineAccessor
    {
        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        /// 
        /// Selects a list of RoutineSteps objects from the database assigned to the Routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The Routine for which to select all RoutineSteps assigned</param>
        /// <exception>No RoutineSteps found</exception>
        /// <returns>A list of RoutineStep objects</returns>
        List<RoutineStep> IRoutineAccessor.SelectRoutineStepsByRoutine(Routine routine)
        {
            List<RoutineStep> steps = new List<RoutineStep>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_routine_steps_by_routine_name", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);

            // Set parameter to value
            cmd.Parameters["@RoutineName"].Value = routine.Name;

            cmd.Parameters.AddWithValue("@Active", true);

            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
                var reader = cmd.ExecuteReader();

                // Capture results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var routineStepID = reader.GetInt32(0);
                        var routineName = reader.GetString(1);
                        var routineStepName = reader.GetString(2);
                        var routineStepDescription = reader.GetString(3);
                        var entryDate = reader.GetDateTime(4);
                        DateTime? editDate = null;
                        if (!reader.IsDBNull(5))
                        {
                            editDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            editDate = null;
                        }
                        DateTime? removalDate;
                        if (!reader.IsDBNull(6))
                        {
                            removalDate = reader.GetDateTime(6);
                        }
                        else
                        {
                            removalDate = null;
                        }
                        var stepOrderNumber = reader.GetInt32(7);
                        var active = reader.GetBoolean(8);

                        var step = new RoutineStep(routineStepID, routineName, routineStepName, routineStepDescription, entryDate, editDate, removalDate, stepOrderNumber, active);

                        steps.Add(step);
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return steps;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Updates a specific Routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="oldRoutine">The Routine to update</param>
        /// <param name="newRoutine">The New Routine</param>
        /// <exception cref="ApplicationException">Routine could not be udpated</exception>
        /// <returns>If the routine was successfully updated</returns>
        public bool UpdateRoutine(Routine oldRoutine, Routine newRoutine)
        {
            int result = 0;
            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_update_routine", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);

            // Set parameter to value
            cmd.Parameters["@RoutineName"].Value = oldRoutine.Name;

            // Add parameter to command
            cmd.Parameters.Add("@RoutineDescription", SqlDbType.NVarChar, 150);

            // Set parameter to value
            cmd.Parameters["@RoutineDescription"].Value = newRoutine.Description;

            // Add parameter to command
            cmd.Parameters.Add("@UserID_Client", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserID_Client"].Value = oldRoutine.UserAccountID_Client;

            // Add parameter to command
            cmd.Parameters.Add("@UserID_Admin", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserID_Admin"].Value = oldRoutine.UserAccountID_Admin;

            // Add parameter to command
            cmd.Parameters.Add("@EditDate", SqlDbType.DateTime);

            if (newRoutine.EditDate.HasValue)
            {
                // Set parameter to value
                cmd.Parameters["@EditDate"].Value = newRoutine.EditDate;
            }
            else
            {
                // Set parameter to value
                cmd.Parameters["@EditDate"].Value = DBNull.Value;
            }

            // Add parameter to command
            cmd.Parameters.Add("@RemovalDate", SqlDbType.DateTime);

            if (newRoutine.RemovalDate.HasValue)
            {
                // Set parameter to value
                cmd.Parameters["@RemovalDate"].Value = newRoutine.RemovalDate;
            }
            else
            {
                // Set parameter to value
                cmd.Parameters["@RemovalDate"].Value = DBNull.Value;
            }

            // Add parameter to command
            cmd.Parameters.Add("@Active", SqlDbType.Bit);

            // Set parameter to value
            cmd.Parameters["@Active"].Value = newRoutine.Active;

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
            return result == 1 ;

        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Selects all active routines for a UserAccount listed as the Client
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountId of the client</param>
        /// <exception cref="ApplicationException">Routines could not be found</exception>
        /// <returns>A list of Routines</returns>
        public List<Routine> SelectActiveRoutinesByUserAccountIDClient(int userAccountID)
        {
            List<Routine> routines = new List<Routine>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_active_routines_by_useraccountid", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@UserID_Client", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserID_Client"].Value = userAccountID;

            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
                var reader = cmd.ExecuteReader();

                // Capture results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var routineName = reader.GetString(0);
                        var routineDescription = reader.GetString(1);
                        var userIDAdmin = reader.GetInt32(2);
                        var entryDate = reader.GetDateTime(3);
                        DateTime? editDate = null;
                        if (!reader.IsDBNull(4))
                        {
                            editDate = reader.GetDateTime(4);
                        }
                        else
                        {
                            editDate = null;
                        }
                        DateTime? removalDate;
                        if (!reader.IsDBNull(5))
                        {
                            removalDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            removalDate = null;
                        }

                        var routine = new Routine(routineName, routineDescription, userAccountID, userIDAdmin,
                            true, entryDate, editDate, removalDate);

                        routines.Add(routine);
                    }
                    reader.Close();
                }
            }
            catch (SqlException)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return routines;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Creates a RoutineStepCompletion
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routineStep">The RoutineStep to complete</param>
        /// <param name="userAccount">The User who completed the step</param>
        /// <exception cref="ApplicationException">RoutineStep could not be completed</exception>
        /// <returns>True if completion stored</returns>
        public bool CreateRoutineStepCompletion(RoutineStep routineStep, UserAccount userAccount)
        {
            int result = 0;

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_create_routinestepcompletion", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@UserID_Client", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserID_Client"].Value = userAccount.UserAccountID;

            // Add parameter to command
            cmd.Parameters.Add("@RoutineStepID", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@RoutineStepID"].Value = routineStep.RoutineStepID;

            // Add parameter to command
            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);

            // Set parameter to value
            cmd.Parameters["@RoutineName"].Value = routineStep.RoutineName;

            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return result == 1;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Creates a RoutineCompletion
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The Routine to complete</param>
        /// <param name="userAccount">The User who completed the step</param>
        /// <exception cref="ApplicationException">Routine could not be completed</exception>
        /// <returns>True if completion stored</returns>
        public bool CreateRoutineCompletion(Routine routine, UserAccount userAccount)
        {
            int result = 0;

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_create_routinecompletion", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@UserID_Client", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserID_Client"].Value = userAccount.UserAccountID;

            // Add parameter to command
            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);

            // Set parameter to value
            cmd.Parameters["@RoutineName"].Value = routine.Name;

            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return result == 1;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/04
        /// 
        /// Gets the routine by the selected userID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The Id of the selected user</param>
        /// <exception cref="ApplicationException">Routine could not be retrieved.</exception>
        /// <returns>A List of routines</returns>
        public List<Routine> SelectRoutinesByUserID(int userAccountID)
        {
            List<Routine> routines = new List<Routine>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_routines_by_useraccountid", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID_client", userAccountID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var RoutineName = reader.GetString(0);
                        var RoutineDescription = reader.GetString(1);
                        var UserId_Client = reader.GetInt32(2);
                        var UserId_Admin = reader.GetInt32(3);
                        var RoutineEntryDate = reader.GetDateTime(4);
                        DateTime? RoutineEditDate = null;
                        if (!reader.IsDBNull(5))
                        {
                            RoutineEditDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            RoutineEditDate = null;
                        }
                        DateTime? RoutineRemovalDate;
                        if (!reader.IsDBNull(6))
                        {
                            RoutineRemovalDate = reader.GetDateTime(6);
                        }
                        else
                        {
                            RoutineRemovalDate = null;
                        }
                        var Active = reader.GetBoolean(7);


                        var routine = new Routine(RoutineName, RoutineDescription, UserId_Client, UserId_Admin, Active, RoutineEntryDate, RoutineEditDate, RoutineRemovalDate);
                        routines.Add(routine);
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
            return routines;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/04
        /// 
        /// Inserts the new routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The routine to be added</param>
        /// <exception cref="ApplicationException">Routine could not be added</exception>
        /// <returns>the name of the routine.</returns>
        public bool InsertNewRoutine(Routine routine)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_routine", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RoutineName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@RoutineDescription", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@UserID_Admin", SqlDbType.Int);
            cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Active", SqlDbType.Bit);



            cmd.Parameters["@RoutineName"].Value = routine.Name;
            cmd.Parameters["@RoutineDescription"].Value = routine.Description;
            cmd.Parameters["@UserID_client"].Value = routine.UserAccountID_Client;
            cmd.Parameters["@UserID_Admin"].Value = routine.UserAccountID_Admin;
            cmd.Parameters["@EntryDate"].Value = routine.EntryDate;
            cmd.Parameters["@Active"].Value = routine.Active;


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
            return result == 1;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/31
        /// 
        /// Update a routine step
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldRoutineStep">The routine step to be updated</param>
        /// <param name="newRoutineStep">The routine step to be updated</param>
        /// <exception cref="ApplicationException">RoutineStep could not be updated</exception>
        /// <returns>Rows affected</returns>
        public bool UpdateRoutineStep(RoutineStep oldRoutineStep, RoutineStep newRoutineStep)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_routinestep", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OldRoutineStepID", oldRoutineStep.RoutineStepID);
            cmd.Parameters.AddWithValue("@OldRoutineStepName", oldRoutineStep.RoutineStepName);
            cmd.Parameters.AddWithValue("@OldRoutineStepDescription", oldRoutineStep.RoutineStepDescription);
            cmd.Parameters.AddWithValue("@OldRoutineStepEntryDate", oldRoutineStep.RoutineStepEntryDate);
            cmd.Parameters.AddWithValue("@OldRoutineStepOrderNumber", oldRoutineStep.RoutineStepOrderNumber);
            cmd.Parameters.AddWithValue("@OldActive", oldRoutineStep.Active);
            cmd.Parameters.AddWithValue("@NewRoutineStepName", newRoutineStep.RoutineStepName);
            cmd.Parameters.AddWithValue("@NewRoutineStepDescription", newRoutineStep.RoutineStepDescription);
            cmd.Parameters.AddWithValue("@NewRoutineStepEntryDate", newRoutineStep.RoutineStepEntryDate);
            if (newRoutineStep.RoutineStepEditDate != null)
            {
                cmd.Parameters.AddWithValue("@NewRoutineStepEditDate", newRoutineStep.RoutineStepEditDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewRoutineStepEditDate", DateTime.Now);
            }
            if (newRoutineStep.RoutineStepRemovalDate != null)
            {
                cmd.Parameters.AddWithValue("@NewRoutineStepRemovalDate", newRoutineStep.RoutineStepRemovalDate);
            }
            else
            {
                if (!newRoutineStep.Active && oldRoutineStep.Active)
                {
                    cmd.Parameters.AddWithValue("@NewRoutineStepRemovalDate", DateTime.Now);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NewRoutineStepRemovalDate", DBNull.Value);
                }
            }
            cmd.Parameters.AddWithValue("@NewRoutineStepOrderNumber", newRoutineStep.RoutineStepOrderNumber);
            cmd.Parameters.AddWithValue("@NewActive", newRoutineStep.Active);

            try
            {
                conn.Open();
                result = 1 == cmd.ExecuteNonQuery();
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
        /// Created: 2021/04/31
        /// 
        /// Swaps the order of two routine steps
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="stepMovingBack">The routine step to be moved backward in the order</param>
        /// <param name="stepMovingForward">The routine step to be moved forward in the order</param>
        /// <exception cref="ApplicationException">RoutineStep could not be updated</exception>
        /// <returns>True if steps' order swapped</returns>
        public bool SwapRoutineStepOrder(RoutineStep stepMovingBack, RoutineStep stepMovingForward)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_routinestep_order", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoutineStepID", stepMovingBack.RoutineStepID);
            cmd.Parameters.AddWithValue("@RoutineStepOrderNumber", stepMovingForward.RoutineStepOrderNumber);


            try
            {
                conn.Open();
                result = 1 == cmd.ExecuteNonQuery();
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
        /// Created: 2021/04/01
        /// 
        /// Selects all active routines for a UserAccount listed as the Client
        /// which do not have a record of completion for the current day
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountId of the client</param>
        /// <exception cref="ApplicationException">Routines could not be found</exception>
        /// <returns>A list of Routines</returns>
        public List<Routine> SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime selectedDate, int userAccountID)
        {
            List<Routine> routines = new List<Routine>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_incomplete_routines_by_useraccountid_by_day", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.AddWithValue("@UserID_Client", userAccountID);
            cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);

            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
                var reader = cmd.ExecuteReader();

                // Capture results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var routineName = reader.GetString(0);
                        var routineDescription = reader.GetString(1);

                        var userIDAdmin = reader.GetInt32(3);
                        var entryDate = reader.GetDateTime(4);
                        DateTime? editDate = null;
                        if (!reader.IsDBNull(5))
                        {
                            editDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            editDate = null;
                        }
                        DateTime? removalDate;
                        if (!reader.IsDBNull(6))
                        {
                            removalDate = reader.GetDateTime(6);
                        }
                        else
                        {
                            removalDate = null;
                        }

                        var routine = new Routine(routineName, routineDescription, userAccountID, userIDAdmin,
                            true, entryDate, editDate, removalDate);

                        routines.Add(routine);
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return routines;
        }
    }
}
    

