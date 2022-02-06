/// <summary>
/// Jory A. Wernette
/// Created: 2021/02/23
/// 
/// This class accesses the data through
/// the DBConnection class
/// </summary>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/03/03
/// Added delete Journal functions
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/02/25
/// Added InsertNewJournalEntry
/// Added SelectJournalEntries
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/08
/// Added DeleteJournalEntry
/// </remarks>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/03/10
/// Added delete Journal Entry functions
/// so that my delete Journal will also delete all of its
/// entries contained within it
/// </remarks>

/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/14
/// Added UpdateJournalEntry
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
    public class JournalAccessor : IJournalAccessor
    {
        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This method grabs the corresponding stored procedure from
        /// the sql to create a Journal using their userID, a journal name, 
        /// and journal description
        /// </summary>
        /// 
        /// <param name="userID"> The UserID of the User who is creatingthe Journal</param>
        /// <param name="journalName"> The Name of the Journal</param>
        /// <param name="journalDescription"> A Description of the Journal</param>
        /// <exception>No Journal created</exception>
        /// <returns>Rows Affected</returns>
        public int CreateNewJournal(int userID, string journalName, string journalDescription)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_journal_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID_Client", userID);
            cmd.Parameters.AddWithValue("@JournalName", journalName);
            cmd.Parameters.AddWithValue("@JournalDescription", journalDescription);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("The '" + journalName + "' Journal could not be added.");
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
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to help get all Journals by a user's UserID
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User whose Journals are getting retreived</param>
        /// <exception>No Journals found</exception>
        /// <returns>A List of Journal objects</returns>
        public List<Journal> SelectAllJournalsByUserID(int userID)
        {
            List<Journal> journals = new List<Journal>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_journals_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID_Client", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Journal journal = new Journal();
                        //journal.UserID_Client = Convert.ToInt32(reader.GetString(0));
                        journal.JournalName = reader.GetString(0);
                        journal.JournalDescription = reader.GetString(1);

                        journals.Add(journal);
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
            return journals;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to help get all Journals in the database
        /// </summary>
        ///
        /// <param></param>
        /// <exception>No Journals found</exception>
        /// <returns>A List of Journal objects</returns>
        public List<Journal> SelectAllJournals()
        {
            List<Journal> journals = new List<Journal>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_journals", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        journals.Add(new Journal()
                        {
                            UserID_Client = reader.GetInt32(0),
                            JournalName = reader.GetString(1),
                            JournalDescription = reader.GetString(2)
                        });
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
            return journals;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This method grabs the corresponding stored procedure from
        /// the sql to update a Journal using their userID, a journal name, 
        /// and journal description
        /// </summary>
        /// 
        /// <param name="newJournal"> The new things to be saved to the journal</param>
        /// <param name="oldJournal"> The old journal to check that we are overwritting the correct journal</param>
        /// <exception>No Journal updated</exception>
        /// <returns>Rows affected</returns>
        public int UpdateJournal(Journal newJournal, Journal oldJournal)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_journal", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", newJournal.UserID_Client);
            cmd.Parameters.AddWithValue("@NewJournalName", newJournal.JournalName);
            cmd.Parameters.AddWithValue("@NewJournalDescription", newJournal.JournalDescription);


            cmd.Parameters.AddWithValue("@OldJournalName", oldJournal.JournalName);
            cmd.Parameters.AddWithValue("@OldJournalDescription", oldJournal.JournalDescription);

            //cmd.Parameters["@OldJournalName"].Value = oldJournal.JournalName;
            //cmd.Parameters["@OldJournalDescription"].Value = oldJournal.JournalDescription;

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
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to help get a Journal by a user's UserID and Journal Name
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User whose Journal is getting retreived</param>
        /// <param name="journalName">The name of the Journal getting retreived</param>
        /// <exception>No Journals found</exception>
        /// <returns>A List of Journal objects</returns>
        public Journal SelectJournalByUserIDAndJournalName(int userID, string journalName)
        {
            Journal journal = new Journal();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_journal_by_userID_and_journalName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID_Client", userID);
            cmd.Parameters.AddWithValue("@JournalName", journalName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //journal.UserID_Client = Convert.ToInt32(reader.GetString(0));
                        journal.JournalName = reader.GetString(0);
                        journal.JournalDescription = reader.GetString(1);
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
            return journal;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to help delete a Journal by a user's UserID, and a Journal's name
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User whose Journals are getting retreived</param>
        /// <param name="journalName">The Name of this Journal</param>
        /// <exception>No Journal found</exception>
        /// <returns>Int Rows Affected</returns>
        public int DeleteJournalByUserIDAndJournalName(int userID, string journalName)
        {
            int result;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_journal_by_userID_and_journalName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID_Client", userID);
            cmd.Parameters.AddWithValue("@JournalName", journalName);

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
        /// Created: 2021/02/25
        /// 
        /// A method used to create a new journal entry to be 
        /// stored within the data base.
        /// </summary>
        /// 
        ///<param name="journalEntry">newly created journal</param>
        ///<exception></exception>
        ///<returns>An int to signify the journal was created</returns>
        public int InsertNewJournalEntry(JournalEntry journalEntry)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_journal_entry", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@UserID_ClientJournal", SqlDbType.Int);
            cmd.Parameters.Add("@JournalID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@JournalEntryBody", SqlDbType.NVarChar, 500);

            cmd.Parameters["@UserID_client"].Value = journalEntry.UserIDClient;
            cmd.Parameters["@UserID_ClientJournal"].Value = journalEntry.UserIDClientJournal;
            cmd.Parameters["@JournalID"].Value = journalEntry.JournalID;
            cmd.Parameters["@JournalEntryBody"].Value = journalEntry.JournalEntryBody;

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
        /// Created: 2021/02/25
        /// 
        /// A method used to retreive journal entreis related to a joural 
        /// that are stored within the data base.
        /// </summary>
        /// 
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception></exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        public List<JournalEntry> SelectJournalEntries(int userID,
            string JournalName)
        {

            List<JournalEntry> journalEntries = new List<JournalEntry>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_retrieve_journals_journal_entries_", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@JournalID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@UserID_ClientJournal", SqlDbType.Int);

            cmd.Parameters["@JournalID"].Value = JournalName;
            cmd.Parameters["@UserID_ClientJournal"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var journalEntry = new JournalEntry()
                        {
                            UserIDClient = reader.GetInt32(0),
                            UserIDClientJournal = reader.GetInt32(1),
                            JournalID = reader.GetString(2),
                            JournalEntryBody = reader.GetString(3),
                            JournalEntryDate = reader.GetDateTime(4),
                            JournalEditDate = reader.GetDateTime(5)
                        };
                        journalEntries.Add(journalEntry);
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

            return journalEntries;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/11
        /// 
        /// An abstract method for deleting journal entries.
        /// </summary>
        /// 
        ///<param name="UserIDClient">The id of the creator of the journal</param>
        ///<param name="JournalID">The name of the journal the entry is associated with</param>
        ///<param name="JounalEntryBody">The journal entry text</param>
        ///<param name="JournalEntryDate">The date the journal entry was created</param>
        ///<exception>The journal entry was not deleted</exception>
        ///<returns>number depending on if the journal entry was deleted</returns>
        public int DeleteJournalEntry(int UserIDClient, string JournalID, string JounalEntryBody, DateTime JournalEntryDate)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_one_journal_entry", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@JournalID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@JournalEntryBody", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@JournalEntryDate", SqlDbType.DateTime);

            cmd.Parameters["@UserID_client"].Value = UserIDClient;
            cmd.Parameters["@JournalID"].Value = JournalID;
            cmd.Parameters["@JournalEntryBody"].Value = JounalEntryBody;
            cmd.Parameters["@JournalEntryDate"].Value = JournalEntryDate;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("The journal entry could not be removed.");
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
        /// Created: 2021/03/14
        /// 
        /// An method for updating a journal entry.
        /// </summary>
        /// 
        ///<param name="OldJournalEntry">The old journal entry object</param>
        ///<param name="NewJounalEntryBody">The new journal entry that is to replace the old one</param>
        ///<exception>if the update failed</exception>
        ///<returns>number depending on if the journal entry was updated</returns>
        public int UpdateJournalEntry(JournalEntry OldJournalEntry, string NewJounalEntryBody)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_journal_entry", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID_client", SqlDbType.Int);
            cmd.Parameters.Add("@UserID_ClientJournal", SqlDbType.Int);
            cmd.Parameters.Add("@JournalID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@JournalEntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldJournalEntryBody", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@JournalEditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewJournalEntryBody", SqlDbType.NVarChar, 500);

            cmd.Parameters["@UserID_client"].Value = OldJournalEntry.UserIDClient;
            cmd.Parameters["@UserID_ClientJournal"].Value = OldJournalEntry.UserIDClientJournal;
            cmd.Parameters["@JournalID"].Value = OldJournalEntry.JournalID;
            cmd.Parameters["@JournalEntryDate"].Value = OldJournalEntry.JournalEntryDate;
            cmd.Parameters["@OldJournalEntryBody"].Value = OldJournalEntry.JournalEntryBody;
            cmd.Parameters["@JournalEditDate"].Value = OldJournalEntry.JournalEditDate;
            cmd.Parameters["@NewJournalEntryBody"].Value = NewJounalEntryBody;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("The journal entry could not be updated.");
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
