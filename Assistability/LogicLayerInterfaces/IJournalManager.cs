/// <summary>
/// Jory A. Wernette
/// Created: 2021/02/23
/// 
/// This is an interface for the management
/// of the Journals
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
/// Added AddJournalEntry
/// Added RetrieveJournalEntriesFromJournal
/// </remarks>
///  
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/08
/// Added RemoveJournalEntry
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/14
/// Added EditJournalEntry
/// </remarks>

using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IJournalManager
    {
        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/12
        /// 
        /// This is the interface of the method that will grab every Journal in the Database, without asking for UserID or any other Parameter.
        /// The more functional version will require a UserID.
        /// </summary>
        List<Journal> RetreiveAllJournals();

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/12
        /// 
        /// This is the interface of the method that will grab every Journal in the Database, without asking for UserID or any other Parameter.
        /// The more functional version will require a UserID.
        /// </summary>
        /// <param name="userID">The UserID of the User who created this Journal</param>
        /// <param name="journalName">The Name of this Journal</param>
        /// <param name="journalDescription">The Description of this Journal</param>
        /// <exception>No Journal created</exception>
        /// <returns>Count of rows affected</returns>
        int CreateAJournalWithUserID(int userID, string journalName, string journalDescription);

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
        int UpdateJournal(Journal newJournal, Journal oldJournal);

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/12
        /// 
        /// This is the interface of the method that will grab every Journal created by a specific user
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User who created this Journal</param>
        /// <exception>No Journals found</exception>
        /// <returns>A List of Journal objects</returns>
        List<Journal> SelectAllJournalsByUserID(int userID);

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This is the interface of the method that will grab a Journal created by a specific user
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User who created this Journal</param>
        /// <param name="journalName">The name of a Journal the user would like to view / edt</param>
        /// <exception>No Journal found</exception>
        /// <returns>A Journal object</returns>
        Journal SelectJournalByUserIDAndJournalName(int userID, string journalName);

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This is the interface of the method that will delete a Journal created by a specific user
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User who created this Journal</param>
        /// <param name="journalName">The name of a Journal that will be removed</param>
        /// <exception>No Journal found</exception>
        /// <returns>Int Rows Affected</returns>
        int DeleteJournalByUserIDAndJournalName(int userID, string journalName);
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// An abstract method for adding a Journal Entry.
        /// </summary>
        /// 
        ///<param name="journalEntry">the newly created journal entry</param>
        ///<exception></exception>
        ///<returns>A bool signifying if the journal entry was created</returns>
        bool AddJournalEntry(JournalEntry journalEntry);
        
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// An abstract method for retrieving journal entries 
        /// related to a journal.
        /// </summary>
        /// 
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception></exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        List<JournalEntry> RetrieveJournalEntreisFromJournal(int userID,
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
        ///<returns>true or false depending on if the journal entry was deleted</returns>
        bool RemoveJournalEntry(int UserIDClient, string JournalID, 
            string JounalEntryBody, DateTime JournalEntryDate);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/14
        /// 
        /// An abstract method for editing a journal entry.
        /// </summary>
        /// 
        ///<param name="OldJournalEntry">The old journal entry object</param>
        ///<param name="NewJounalEntryBody">The new journal entry that is to replace the old one</param>
        ///<exception></exception>
        ///<returns>number depending on if the journal entry was updated</returns>
        bool EditJournalEntry(JournalEntry OldJournalEntry,
            string NewJounalEntryBody);
    }
}
