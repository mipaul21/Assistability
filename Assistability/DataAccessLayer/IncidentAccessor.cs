/// <summary>
/// Nick Loesel
/// Created: 2021/03/17
/// 
/// This class accesses the data through
/// the DBConnection class
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/08
/// Added InsertNewIncident method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/08
/// Added SelectIncidentsByUserID method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added SelectIncidentsByActive method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added ReactivateIncident method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added DeactivateIncident method
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
    public class IncidentAccessor : IIncidentAccessor
    {
        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// Inserts the new incident into the databse
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="newIncident">The new incident object</param>
        /// <exception cref="ApplicationException">Incident could not be inserted.</exception>
        /// <returns>A list of Incidents</returns>
        public bool InsertNewIncident(Incident newIncident)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_incident", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IncidentName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@IncidentDescription", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@DesiredConsequence", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@IncidentEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters.Add("@UserID_Client", SqlDbType.Int);
            cmd.Parameters.Add("@UserID_Admin", SqlDbType.Int);



            cmd.Parameters["@IncidentName"].Value = newIncident.IncidentName;
            cmd.Parameters["@IncidentDescription"].Value = newIncident.IncidentDescription;
            cmd.Parameters["@DesiredConsequence"].Value = newIncident.DesiredConsequence;
            cmd.Parameters["@IncidentEntryDate"].Value = newIncident.IncidentEntryDate;
            cmd.Parameters["@Active"].Value = newIncident.Active;
            cmd.Parameters["@UserID_Client"].Value = newIncident.UserId_Client;
            cmd.Parameters["@UserID_Admin"].Value = newIncident.UserId_Creator;



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
        /// Nick Loesel
        /// Created: 2021/03/17
        /// 
        /// Gets the incident by the selected userID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userID">The Id of the selected user</param>
        /// <exception cref="ApplicationException">Incident could not be retrieved.</exception>
        /// <returns>A list of Incidents</returns>
        public List<Incident> SelectIncidentsByUserID(int userID)
        {
            List<Incident> incidents = new List<Incident>();


            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_incidents_by_useraccountid", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID_client", userID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var IncidentName = reader.GetString(0);
                        var IncidentDescription = reader.GetString(1);
                        var DesiredConsequence = reader.GetString(2);
                        var IncidentEntryDate = reader.GetDateTime(3);
                        DateTime? IncidentEditDate = null;
                        if (!reader.IsDBNull(4))
                        {
                            IncidentEditDate = reader.GetDateTime(4);
                        }
                        else
                        {
                            IncidentEditDate = null;
                        }
                        DateTime? IncidentRemovalDate = null;
                        if (!reader.IsDBNull(5))
                        {
                            IncidentRemovalDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            IncidentRemovalDate = null;
                        }
                        var Active = reader.GetBoolean(6);
                        var UserId_Client = reader.GetInt32(7);
                        var UserId_Creator = reader.GetInt32(8);

                        var incident = new Incident(IncidentName, IncidentDescription, DesiredConsequence, IncidentEntryDate, IncidentEditDate, IncidentRemovalDate, Active, UserId_Client, UserId_Creator);
                        incidents.Add(incident);
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
            return incidents;
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// Updates the incident in the databse to active
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="selectedUser">The selected user to change</param>
        /// <param name="incident">The selected incident to reactivate</param>
        /// <exception cref="ApplicationException">Incident could not be updated.</exception>
        /// <returns>true if the incident was activated</returns>
        public bool ReactivateIncident(UserAccount selectedUser, Incident incident)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_incident", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID_Client", selectedUser.UserAccountID);
            cmd.Parameters.AddWithValue("@IncidentName", incident.IncidentName);
            cmd.Parameters.AddWithValue("@IncidentRemovalDate", incident.IncidentRemovalDate);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("incident could not be deactivated.");
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
            return result == 1;
        }




        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/03/2021
        /// Approver: 
        ///
        /// Method that allows the edit and updating of the incident database through the use of a stored procedure.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        public int UpdateIncident(Incident oldIncident, Incident newIncident)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_incident", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IncidentName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldIncidentDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@OldDesiredConsequence", SqlDbType.NVarChar, 250);




            cmd.Parameters.Add("@NewIncidentDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@NewDesiredConsequence", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewIncidentEditDate", SqlDbType.DateTime);



            cmd.Parameters["@IncidentName"].Value = oldIncident.IncidentName;
            cmd.Parameters["@OldIncidentDescription"].Value = oldIncident.IncidentDescription;
            cmd.Parameters["@OldDesiredConsequence"].Value = oldIncident.DesiredConsequence;

            cmd.Parameters["@NewIncidentDescription"].Value = newIncident.IncidentDescription;
            cmd.Parameters["@NewDesiredConsequence"].Value = newIncident.DesiredConsequence;
            cmd.Parameters["@NewIncidentEditDate"].Value = DateTime.Now;





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
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// Selects The incidents by active and userID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="active">The active incidents</param>
        /// <param name="selectedUser">The Selected user</param>
        /// <exception cref="ApplicationException">Incident could not be Found.</exception>
        /// <returns>A list of Incidents</returns>
        public List<Incident> SelectIncidentsByActive(int selectedUser, bool active)
        {
            List<Incident> incidents = new List<Incident>();


            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_incidents_by_active", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID_client", selectedUser);
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var IncidentName = reader.GetString(0);
                        var IncidentDescription = reader.GetString(1);
                        var DesiredConsequence = reader.GetString(2);
                        var IncidentEntryDate = reader.GetDateTime(3);
                        DateTime? IncidentEditDate = null;
                        if (!reader.IsDBNull(4))
                        {
                            IncidentEditDate = reader.GetDateTime(4);
                        }
                        else
                        {
                            IncidentEditDate = null;
                        }
                        DateTime? IncidentRemovalDate = null;
                        if (!reader.IsDBNull(5))
                        {
                            IncidentRemovalDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            IncidentRemovalDate = null;
                        }
                        var Active = reader.GetBoolean(6);
                        var UserId_Client = reader.GetInt32(7);
                        var UserId_Creator = reader.GetInt32(8);

                        var incident = new Incident(IncidentName, IncidentDescription, DesiredConsequence, IncidentEntryDate, IncidentEditDate, IncidentRemovalDate, Active, UserId_Client, UserId_Creator);
                        incidents.Add(incident);
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
            return incidents;
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/08
        /// 
        /// Gets the incident by the selected userID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userID">The Id of the selected user</param>
        /// <exception cref="ApplicationException">Incident could not be retrieved.</exception>
        /// <returns>A list of Incidents</returns>
		
		public bool DeactivateIncident(UserAccount selectedUser, Incident incident)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_incident", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID_Client", selectedUser.UserAccountID);
            cmd.Parameters.AddWithValue("@IncidentName", incident.IncidentName);
            cmd.Parameters.AddWithValue("@IncidentRemovalDate", DateTime.Now);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("incident could not be deactivated.");
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
            return result == 1;
        }
    }
}
