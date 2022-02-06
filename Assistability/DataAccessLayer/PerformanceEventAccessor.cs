/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/03/29
/// 
/// The accessor methods for 
/// the Performance Event data model.
/// 
/// </summary>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/04/25
/// had to fix the SelectPerformanceEventsByPerformanceName, SelectPerformanceEventsByUserID, and 
/// UpdatePerformanceEvent methods
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
    /// <summary>
    /// 
    /// Your Name: Whitney Vinson
    /// Created: 2021/03/29
    /// 
    /// The PerformanceEvent 
    /// accessor class.
    /// 
    /// </summary>
    ///
    /// <remarks>
    /// </remarks>

    public class PerformanceEventAccessor : IPerformanceEventAccessor
    {
        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method for inserting
        /// a PerformanceEvent.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="performanceName">The name of the associated Performance</param>
        /// <param name="clientID">The ID of the user</param>
        /// <param name="adminID">The ID of the administrator</param>
        /// <param name="performanceEvent">The PerformanceEvent</param>
        /// <exception cref="SqlException">Insert Fails ("Data not Added")</exception>
        /// <returns>Rows added</returns>

        public int InsertNewPerformanceEvent(string performanceName, int clientID, int adminID,
                                                PerformanceEvent performanceEvent)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_performance_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PerformanceName", performanceName);
            cmd.Parameters.AddWithValue("@DateOfOccurance", DateTime.Now);
            cmd.Parameters.AddWithValue("@UserIDClient", clientID);
            cmd.Parameters.AddWithValue("@UserIDReporter", adminID);

            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@EventResult", SqlDbType.NVarChar, 250);

            cmd.Parameters["@EventDescription"].Value = performanceEvent.EventDescription;
            cmd.Parameters["@EventResult"].Value = performanceEvent.EventResult;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method for selecting
        /// PerformanceEvents by PerformanceName.
        /// 
        /// </summary>
        ///
        /// <remarks>
		/// Ryan Taylor
		/// Updated: 2021/04/27
		/// Had to rework the reader to look for null and add the eventto the event list
        /// </remarks>
        /// <param name="performanceName">The name of the associated performance</param>
        /// <exception cref="SqlException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Row Count</returns>

        public List<PerformanceEvent> SelectPerformanceEventsByPerformanceName(string performanceName)
        {
            List<PerformanceEvent> performanceEvents = new List<PerformanceEvent>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_performance_events_by_performance_name", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PerformanceName", performanceName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var performanceEventID = reader.GetInt32(0);
                        var performanceNameDB = reader.GetString(1);
                        var dateOfOccurance = reader.GetDateTime(2);
                        var eventDescription = reader.GetString(3);
                        var eventResult = reader.GetString(4);

                        DateTime? eventEditDate;
                        if (!reader.IsDBNull(5))
                        {
                            eventEditDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            eventEditDate = null;
                        }

                        var userIDClient = reader.GetInt32(6);
                        var userIDReporter = reader.GetInt32(7);
                        var performanceEvent = new PerformanceEvent()
						{
							PerformanceEventID = performanceEventID, 
                            PerformanceName = performanceNameDB, 
							DateOfOccurance = dateOfOccurance, 
							EventDescription = eventDescription, 
                            EventResult = eventResult, 
							EventEditDate = eventEditDate, 
							UserIDClient = userIDClient, 
							UserIDReporter = userIDReporter
						};
                        performanceEvents.Add(performanceEvent);
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return performanceEvents;
        }

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method for selecting
        /// PerformanceEvents by UserID.
        /// 
        /// </summary>
        ///
        /// <remarks>
		/// Ryan Taylor
		/// Updated: 2021/04/27
		/// Had to rework the reader to look for null and add the eventto the event list
        /// </remarks>
        /// <param name="userIDClient">The ID of the person receiving the record</param>
        /// <exception cref="SqlException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Row Count</returns>

        public List<PerformanceEvent> SelectPerformanceEventsByUserID(int userIDClient)
        {
            List<PerformanceEvent> performanceEvents = new List<PerformanceEvent>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_performance_events_by_UserIDClient", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserIDClient", userIDClient);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var performanceEventID = reader.GetInt32(0);
                        var performanceName = reader.GetString(1);
                        var dateOfOccurance = reader.GetDateTime(2);
                        var eventDescription = reader.GetString(3);
                        var eventResult = reader.GetString(4);

                        DateTime? eventEditDate;
                        if (!reader.IsDBNull(5))
                        {
                            eventEditDate = reader.GetDateTime(5);
                        }
                        else
                        {
                            eventEditDate = null;
                        }

                        var userIDClientDB = reader.GetInt32(6);
                        var userIDReporter = reader.GetInt32(7);
                        var performanceEvent = new PerformanceEvent()
                        {
                            PerformanceEventID = performanceEventID,
                            PerformanceName = performanceName,
                            DateOfOccurance = dateOfOccurance,
                            EventDescription = eventDescription,
                            EventResult = eventResult,
                            EventEditDate = eventEditDate,
                            UserIDClient = userIDClientDB,
                            UserIDReporter = userIDReporter
                        };
                        performanceEvents.Add(performanceEvent);
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return performanceEvents;
        }

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method for updating
        /// a PerformanceEvent.
        /// 
        /// </summary>
        ///
        /// <remarks>
		/// Ryan Taylor
		/// Updated: 2021/04/27
		/// there where vareables in the method that were not used and had to give 
        /// OldEventEditDate a none null value, which is ok since it is called for 
        /// but never used in the stored procedure. 
        /// Also it was calling the wrong stored procedure
        /// </remarks>
        /// <param name="oldPerformanceEvent">The old PerformanceEvent data.</param>
        /// <param name="newPerformanceEvent">The new PerformanceEvent data.</param>
        /// <exception cref="SqlException">Update Failed ("Data not Available")</exception>
        /// <returns>Rows Affected</returns>

        public int UpdatePerformanceEvent(PerformanceEvent oldPerformanceEvent, PerformanceEvent newPerformanceEvent)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_performance_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PerformanceEventID", SqlDbType.Int);
            cmd.Parameters.Add("@NewEventDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewEventResult", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewEventEditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldEventDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@OldEventResult", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldEventEditDate", SqlDbType.DateTime);

            cmd.Parameters["@PerformanceEventID"].Value = oldPerformanceEvent.PerformanceEventID;
            cmd.Parameters["@NewEventDescription"].Value = newPerformanceEvent.EventDescription;
            cmd.Parameters["@NewEventResult"].Value = newPerformanceEvent.EventResult;
            cmd.Parameters["@NewEventEditDate"].Value = DateTime.Now;
            cmd.Parameters["@OldEventDescription"].Value = oldPerformanceEvent.EventDescription;
            cmd.Parameters["@OldEventResult"].Value = oldPerformanceEvent.EventResult;
            if (oldPerformanceEvent.EventEditDate == null)
            {
                cmd.Parameters["@OldEventEditDate"].Value = DateTime.Now;
            }
            else
            {
                cmd.Parameters["@OldEventEditDate"].Value = oldPerformanceEvent.EventEditDate;
            }

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
