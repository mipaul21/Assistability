/// <summary>
/// Nick Loesel
/// Created: 2021/04/22
/// 
/// This class accesses the data through
/// the DBConnection class
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/22
/// Added SelectIncidentEventsByUserID method
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
    public class IncidentEventAccessor : IIncidentEventAccessor
    {
        public bool DeleteIncidentEvent(int incidentEventid)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_incidentEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IncidentEventID", SqlDbType.Int);



            cmd.Parameters["@IncidentEventID"].Value = incidentEventid;




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

        public bool InsertNewIncidentEvent(IncidentEvent newIncidentEvent)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_incidentEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IncidentName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@DateOfOccurence", SqlDbType.DateTime);
            cmd.Parameters.Add("@PersonsInvolved", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@EventConsequence", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@UserIDClient", SqlDbType.Int);
            cmd.Parameters.Add("@UserIDAdmin", SqlDbType.Int);



            cmd.Parameters["@IncidentName"].Value = newIncidentEvent.IncidentName;
            cmd.Parameters["@DateOfOccurence"].Value = newIncidentEvent.DateOfOccurence;
            cmd.Parameters["@PersonsInvolved"].Value = newIncidentEvent.PersonsInvolved;
            cmd.Parameters["@EventDescription"].Value = newIncidentEvent.EventDescription;
            cmd.Parameters["@EventConsequence"].Value = newIncidentEvent.EventConsequence;
            cmd.Parameters["@UserIDClient"].Value = newIncidentEvent.UserID_Client;
            cmd.Parameters["@UserIDAdmin"].Value = newIncidentEvent.UserID_Admin;



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

        public IncidentEvent SelectIncidentEventById(int incidentEventId)
        {
            IncidentEvent incidentEvent = new IncidentEvent();
            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_incidentEvent_by_incidentEventId", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@IncidentEventID", SqlDbType.NVarChar, 250);

            // Set parameter to value
            cmd.Parameters["@IncidentEventID"].Value = incidentEventId;

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
                        var incidentEventID = reader.GetInt32(0);
                        var incidentName = reader.GetString(1);
                        var dateOfOccurence = reader.GetDateTime(2);
                        var personsinvolved = reader.GetString(3);
                        var eventDescription = reader.GetString(4);
                        var eventConsequence = reader.GetString(5);
                        DateTime? EventEditDate = null;
                        if (!reader.IsDBNull(6))
                        {
                            EventEditDate = reader.GetDateTime(6);
                        }
                        else
                        {
                            EventEditDate = null;
                        }
                        var UserIdClient = reader.GetInt32(7);
                        var UserIdAdmin = reader.GetInt32(8);

                        IncidentEvent incidentevent = new IncidentEvent(incidentEventID, incidentName, dateOfOccurence, personsinvolved, eventDescription, eventConsequence, EventEditDate, UserIdClient, UserIdAdmin);
                        incidentEvent = incidentevent;
                    }
                    reader.Close();
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
            return incidentEvent;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/22
        /// 
        /// Gets the incidentEvent by the selected userID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userID">The Id of the selected user</param>
        /// <exception cref="ApplicationException">Incident could not be retrieved.</exception>
        /// <returns>A list of Incidents</returns>
        public List<IncidentEvent> SelectIncidentEventsByIncidentName(string incidentName)
        {
            List<IncidentEvent> incidentEvents = new List<IncidentEvent>();


            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_incident_events_by_incident_name", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IncidentName", incidentName);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var IncidentEventID = reader.GetInt32(0);
                        var IncidentName = reader.GetString(1);
                        var DateOfOccurence = reader.GetDateTime(2);
                        var PersonsInvolved = reader.GetString(3);
                        var EventDescription = reader.GetString(4);
                        var EventConsequence = reader.GetString(5);
                        
                        DateTime? EventEditDate = null;
                        if (!reader.IsDBNull(6))
                        {
                            EventEditDate = reader.GetDateTime(6);
                        }
                        else
                        {
                            EventEditDate = null;
                        }
                        var UserId_Client = reader.GetInt32(7);
                        var UserId_Creator = reader.GetInt32(8);

                        var incidentEvent = new IncidentEvent(IncidentEventID, IncidentName, DateOfOccurence, PersonsInvolved, EventDescription, EventConsequence, EventEditDate, UserId_Client, UserId_Creator);
                        incidentEvents.Add(incidentEvent);
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
            return incidentEvents;
        }

        public bool UpdateIncidentEvent(IncidentEvent oldIncidentEvent, IncidentEvent newIncidentEvent)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_incidentEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IncidentName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldEventDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@OldPersonsInvolved", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldEventConsequence", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldDateOfOccurence", SqlDbType.DateTime);




            cmd.Parameters.Add("@NewEventDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewPersonsInvolved", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewEventConsequence", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewDateOfOccurence", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewEventEditDate", SqlDbType.DateTime);


            cmd.Parameters["@IncidentName"].Value = oldIncidentEvent.IncidentName;
            cmd.Parameters["@OldEventDescription"].Value = oldIncidentEvent.EventDescription;
            cmd.Parameters["@OldPersonsInvolved"].Value = oldIncidentEvent.PersonsInvolved;
            cmd.Parameters["@OldEventConsequence"].Value = oldIncidentEvent.EventConsequence;
            cmd.Parameters["@OldDateOfOccurence"].Value = oldIncidentEvent.DateOfOccurence;



            cmd.Parameters["@NewEventDescription"].Value = newIncidentEvent.EventDescription;
            cmd.Parameters["@NewPersonsInvolved"].Value = newIncidentEvent.PersonsInvolved;
            cmd.Parameters["@NewEventConsequence"].Value = newIncidentEvent.EventConsequence;
            cmd.Parameters["@NewDateOfOccurence"].Value = newIncidentEvent.DateOfOccurence;
            cmd.Parameters["@NewEventEditDate"].Value = DateTime.Now;





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
    }
}
