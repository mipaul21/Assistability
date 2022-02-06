/// <summary>
/// Jory A. Wernette
/// Created: 2021/02/23
/// 
/// Implements the Journal Manager interface
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
/// Updated: 2021/03/11
/// Added AddJournalEntry
/// Added RetrieveJournalEntry
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/11
/// Added RemoveJournalEntry
/// </remarks>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/03/10
/// Added delete Journal entries onto delete journal, they arent floating in the DB
/// </remarks>

/// Ryan Taylor
/// Updated: 2021/03/14
/// Added EditJournalEntry
/// </remarks>

using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class JournalManager : IJournalManager
    {
        private IJournalAccessor _journalAccessor = null;

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Default constructor initializes an accessor
        /// </summary>
        public JournalManager()
        {
            //_journalAccessor = new JournalFakes();
            _journalAccessor = new JournalAccessor();
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Takes an IJournalAccessor and constructs a JournalManager with the specified accessor
        /// </summary>
        ///
        /// <param name="journalAccessor">The IJournalAccessor to be used as the accessor by this instance</param>
        public JournalManager(IJournalAccessor journalAccessor)
        {
            _journalAccessor = journalAccessor;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to retreive all journals in the database
        /// </summary>
        public List<Journal> RetreiveAllJournals()
        {
            List<Journal> journals;
            try
            {
                journals = _journalAccessor.SelectAllJournals();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Journals not found" + ex.InnerException);
            }
            return journals;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to create a journal
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the user who created this Journal</param>
        /// <param name="journalName">The Name of this Journal</param>
        /// <param name="journalDescription">The Description this Journal</param>
        public int CreateAJournalWithUserID(int userID, string journalName, string journalDescription)
        {
            int rowsAffected;
            try
            {
                rowsAffected = _journalAccessor.CreateNewJournal(userID, journalName, journalDescription);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Journal could not be created" + ex.InnerException);
            }
            return rowsAffected;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to update a journal
        /// </summary>
        /// 
        /// <param name="newJournal"> The new things to be saved to the journal</param>
        /// <param name="oldJournal"> The old journal to check that we are overwritting the correct journal</param>
        /// <exception>No Journal updated</exception>
        /// <returns>Rows affected</returns>
        public int UpdateJournal(Journal newJournal, Journal oldJournal)
        {
            int rowsAffected;
            try
            {
                rowsAffected = _journalAccessor.UpdateJournal(newJournal, oldJournal);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Journal could not be updated" + ex.InnerException);
            }
            return rowsAffected;
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
            List<Journal> journals;
            try
            {
                journals = _journalAccessor.SelectAllJournalsByUserID(userID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Journals not found" + ex.InnerException);
            }
            return journals;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to help get a Journal by a user's UserID, and a Journal's name
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User whose Journals are getting retreived</param>
        /// <param name="journalName">The Name of this Journal</param>
        /// <exception>No Journal found</exception>
        /// <returns>A Journal object</returns>
        public Journal SelectJournalByUserIDAndJournalName(int userID, string journalName)
        {
            Journal journal;
            try
            {
                journal = _journalAccessor.SelectJournalByUserIDAndJournalName(userID, journalName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Journal not found" + ex.InnerException);
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
        /// 
        /// <remarks>
        /// Jory A. Wernette
        /// Updated: 2021/03/10
        /// Added delete Journal entries onto delete journal, they arent floating in the DB
        /// </remarks>
        public int DeleteJournalByUserIDAndJournalName(int userID, string journalName)
        {
            List<JournalEntry> entryList = _journalAccessor.SelectJournalEntries(userID, journalName);
            foreach (var item in entryList)
            {
                // The remove entry method, to be replaced with Ryan's working version
                _journalAccessor.DeleteJournalEntry(userID, item.JournalID, item.JournalEntryBody, item.JournalEntryDate);
            }
            int result = _journalAccessor.DeleteJournalByUserIDAndJournalName(userID, journalName);

            return result;
        }
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// This method helps make sure a new journal entry is 
        /// passed into the data base.
        /// </summary>
        /// 
        ///<param name="journalEntry">the newly created journal entry</param>
        ///<exception>if the journal entry wasn't created or not fount</exception>
        ///<returns>A bool to signify that the journal entry was created</returns>
        public bool AddJournalEntry(JournalEntry journalEntry)
        {
            bool result = false;
            int newJournalEntry = 0;

            try
            {
                newJournalEntry = _journalAccessor.InsertNewJournalEntry(journalEntry);
                if (newJournalEntry == 0)
                {
                    throw new ApplicationException("New journal entry was not " +
                        "added.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Journal Entry Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// This method helps make sure the needed journal entries are retrieved 
        /// from the database.
        /// </summary>
        /// 
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception>The entries were not found for that journal</exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        public List<JournalEntry> RetrieveJournalEntreisFromJournal(int userID,
            string JournalName)
        {
            List<JournalEntry> journalEntries = new List<JournalEntry>();
            try
            {
                journalEntries = _journalAccessor.SelectJournalEntries(userID,
                    JournalName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Journal entries not available."
                    , ex);
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
        public bool RemoveJournalEntry(int UserIDClient, string JournalID, string JounalEntryBody, DateTime JournalEntryDate)
        {
            bool result = false;

            try
            {
                result = (1 == _journalAccessor.DeleteJournalEntry(UserIDClient, JournalID,
                    JounalEntryBody, JournalEntryDate));
                if (result == false)
                {
                    throw new ApplicationException("Journal Entry data was not deleted.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Deletion Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/14
        /// 
        /// A method for editing a journal entry.
        /// </summary>
        /// 
        ///<param name="OldJournalEntry">The old journal entry object</param>
        ///<param name="NewJounalEntryBody">The new journal entry that is to replace the old one</param>
        ///<exception>if the update failed</exception>
        ///<returns>number depending on if the journal entry was updated</returns>
        public bool EditJournalEntry(JournalEntry OldJournalEntry, string NewJounalEntryBody)
        {
            bool result = false;

            try
            {
                result = (1 == _journalAccessor.UpdateJournalEntry(OldJournalEntry, NewJounalEntryBody));
                if (result == false)
                {
                    throw new ApplicationException("Journal Entry not changed.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }
    }
}
