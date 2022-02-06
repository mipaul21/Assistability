/// <summary>
/// Jory A. Wernette
/// Created: 2021/02/23
/// 
/// This is an interface for data access of the journals
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
/// Updated: 2021/02/05
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

using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Jory A. Wernette
    /// Created: 2021/02/23
    /// 
    /// This interface has the methods that will be
    /// used in the JournalAccessor class
    /// </summary>
    public interface IJournalAccessor
    {
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Creates a new journal
        /// </summary>
        /// 
        /// <param name="userID"> The UserID of the user who creates the Journal</param>
        /// <param name="journalName"> The name of the Journal being created</param>
        /// <param name="journalDescription"> The description of the Journal being created</param>
        /// <exception>No Journal found</exception>
        /// <returns>A List of Journal objects</returns>
        int CreateNewJournal(int userID, string journalName, string journalDescription);

        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Selects every journal currently in the database
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No Journal found</exception>
        /// <returns>A List of Journal objects</returns>
        List<Journal> SelectAllJournals();

        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Selects every journal for a specific user
        /// </summary>
        /// 
        /// <param name="userID"> The UserID of the user searching for their Journals</param>
        /// <exception>No Journal found</exception>
        /// <returns>A List of Journal objects</returns>
        List<Journal> SelectAllJournalsByUserID(int userID);

        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Updates a journal
        /// </summary>
        /// 
        /// <param name="newJournal"> The new things to be saved to the journal</param>
        /// <param name="oldJournal"> The old journal to check that we are overwritting the correct journal</param>
        /// <exception>No Journal updated</exception>
        /// <returns>Rows affected</returns>
        int UpdateJournal(Journal NewJournal, Journal OldJournal);

        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// Retrieves a Journal
        /// </summary>
        /// 
        /// <param name="userID"> The new things to be saved to the journal</param>
        /// <param name="journalName"> The name of the journal</param>
        /// <exception>No Journal found</exception>
        /// <returns>A Journal Object</returns>
        Journal SelectJournalByUserIDAndJournalName(int userID, string journalName);

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
        int DeleteJournalByUserIDAndJournalName(int userID, string journalName);
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// an abstract method to support JournalAccessor in 
        /// creating a new journal entry.
        /// </summary>
        /// 
        ///<param name="journalEntry">The new journal entry</param>
        ///<exception></exception>
        ///<returns>An int to signify the journal entry was crated</returns>
        int InsertNewJournalEntry(JournalEntry journalEntry);
		
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// an abstract method to support JournalAccessor in 
        /// retrieving all journal entries related to a journal.
        /// </summary>
        /// 
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception></exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        List<JournalEntry> SelectJournalEntries(int userID,
            string JournalName);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/08
        /// 
        /// An abstract method for deleting journal entries.
        /// </summary>
        /// 
        ///<param name="UserIDClient">The id of the creator of the journal</param>
        ///<param name="JournalID">The name of the journal the entry is associated with</param>
        ///<param name="JounalEntryBody">The journal entry text</param>
        ///<param name="JournalEntryDate">The date the journal entry was created</param>
        ///<exception></exception>
        ///<returns>number depending on if the journal entry was deleted</returns>
        int DeleteJournalEntry(int UserIDClient, string JournalID,
            string JounalEntryBody, DateTime JournalEntryDate);
			
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/14
        /// 
        /// An abstract method for updateing a journal entry.
        /// </summary>
        /// 
        ///<param name="OldJournalEntry">The old journal entry object</param>
        ///<param name="NewJounalEntryBody">The new journal entry that is to replace the old one</param>
        ///<exception></exception>
        ///<returns>number depending on if the journal entry was updated</returns>
        int UpdateJournalEntry(JournalEntry OldJournalEntry, 
            string NewJounalEntryBody);
    }
}
