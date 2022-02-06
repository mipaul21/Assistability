/// <summary>
/// Jory A. Wernette
/// Created: 2021/02/23
/// 
/// This class is the 'fakes' class that I use
/// to test my Journal Accessor code
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
/// Added InsertNewJournalEntryFake
/// Added SelectJournalEntryFake
/// </remarks>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/08
/// Added DeleteJournalEntryFake
/// </remarks>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/03/10
/// Added delete Journal Entry functions to complete 
/// my delete Journal methods to take out any
/// entries within the Journal to be deleted
/// </remarks>

/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/14
/// Added UpdateJournalEntry
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataStorageModels;

namespace DataAccessFakes
{
    /// <summary>
    /// Jory A. Wernette
    /// Created: 2021/02/23
    /// 
    /// This class creates a 'fake' journal to be used
    /// in the JournalManagerTest class
    /// </summary>
    public class JournalAccessorFake : IJournalAccessor
    {
        int rowsAffected;

        private List<Journal> journal = new List<Journal>();
        private Journal singleJournal = new Journal();
		
		public List<JournalEntry> journalEntries = new List<JournalEntry>()
		{
				new JournalEntry()
			{
				UserIDClient = 1,
				UserIDClientJournal = 1,
				JournalID = "Randome",
				JournalEntryBody = "Just here to say I love your work",
				JournalEntryDate = new DateTime(2021, 11, 3, 10, 00, 36)
	
			},
			new JournalEntry()
			{
				UserIDClient = 1,
				UserIDClientJournal = 1,
				JournalID = "Randome Journal",
				JournalEntryBody = "Just here to say I love your work, sike",
				JournalEntryDate = new DateTime(2021, 11, 3, 1, 00, 36)
			},
			new JournalEntry()
			{
				UserIDClient = 1,
				UserIDClientJournal = 1,
				JournalID = "Randome Journal",
				JournalEntryBody = "Just here to say I love your work",
				JournalEntryDate = new DateTime(2021, 11, 3, 9, 00, 36)
			}
		};

        UserAccount user = new UserAccount();

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/12
        /// 
        /// This is the Fake JournalAccessor so I can test to see if everything but the db is working
        /// </summary>
        public JournalAccessorFake()
        {
            journal.Add(new Journal()
            {
                JournalName = "Capstone",
                JournalDescription = "Contains my weekly entries about our Capstone Project.",
                UserID_Client = 927
            });
            journal.Add(new Journal()
            {
                JournalName = "Java3",
                JournalDescription = "This journal contains my weekly entries about Java3.",
                UserID_Client = 927
            });
            journal.Add(new Journal()
            {
                JournalName = "ECSEL",
                JournalDescription = "This journal contains my weekly entries about my ECSEL project.",
                UserID_Client = 927
            });
            journal.Add(new Journal()
            {
                JournalName = "Data Structures",
                JournalDescription = "This journal contains entries about Data Structures.",
                UserID_Client = 927
            });

            singleJournal.UserID_Client = 927;
            singleJournal.JournalName = "Capstone";
            singleJournal.JournalDescription = "Contains my weekly entries about our Capstone Project.";
        }

        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Selects all Journals Currently in the Database
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No Journal found</exception>
        /// <returns>A list of Journal objects</returns>
        public List<Journal> SelectAllJournals()
        {
            return this.journal;
        }

        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Creates a new Journal
        /// </summary>
        /// 
        /// <param name="userID"> The UserID of the User who created the Journal</param>
        /// <param name="journalName"> The Name of the Journal to be created</param>
        /// <param name="journalDescription">A description of the Journal to be created</param>
        /// <exception>No Journal created</exception>
        /// <returns>Rows Affected</returns>
        public int CreateNewJournal(int userID, string journalName, string journalDescription)
        {
            return rowsAffected;
        }

        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Selects all Journals Currently in the Database under a certain UserID
        /// </summary>
        /// 
        /// <param name="userID"> The UserID of the User who created the Journal</param>
        /// <exception>No Journals Found</exception>
        /// <returns>List of Journals</returns>
        public List<Journal> SelectAllJournalsByUserID(int userID)
        {
            //    foreach (var item in journal)
            //    {
            //        if (!item.UserID_Client.Equals(userID))
            //        {
            //            journal.Remove(item);
            //        }
            //    }
            return this.journal;
        }

        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// Updates an existing Journal
        /// </summary>
        /// 
        /// <param name="NewJournal"> The new Journal info to be savd</param>
        /// <param name="journalNOldJournalame"> The old journal to be overwritten</param>
        /// <exception>No Journal updated</exception>
        /// <returns>Rows Affected</returns>
        public int UpdateJournal(Journal NewJournal, Journal OldJournal)
        {
            return rowsAffected;
        }

        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// Selects a Journal Currently in the Database under a certain UserID with a certain name
        /// </summary>
        /// 
        /// <param name="userID"> The UserID of the User who created the Journal</param>
        /// <param name="journalName"> The name of the Journal</param>
        /// <exception>No Journals Found</exception>
        /// <returns>A Journal Object</returns>
        public Journal SelectJournalByUserIDAndJournalName(int userID, string journalName)
        {
            return singleJournal;
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
            return rowsAffected;
        }
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// a fake method for testing when a jouranl entry is added.
        /// </summary>
        ///<param name="journalEntry">the journal enty you have created</param>
        ///<exception></exception>
        ///<returns>
        ///an int to represent that the journal 
        ///entry was created
        ///</returns>
        public int InsertNewJournalEntry(JournalEntry journalEntry)
        {
            int result = 0;
            int startingAmount = journalEntries.Count;
            if (journalEntry.JournalEntryBody != "")
            {
                journalEntries.Add(journalEntry);
            }
            int endingAmount = journalEntries.Count;
            if(endingAmount > startingAmount) 
            {
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// a fake method for testing when retreiving jouranl entries.
        /// </summary>
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception></exception>
        ///<returns>A list of Journal Entries for the journal</returns>

        public List<JournalEntry> SelectJournalEntries(int userID,
            string JournalName)
        {
            var specificJournalEnties = journalEntries.Where(
                j => j.JournalID == JournalName &&
                    j.UserIDClientJournal == userID)
                .ToList();

            return specificJournalEnties;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/08
        /// 
        /// a fake method for testing deleting jouranl entries.
        /// </summary>
        /// 
        ///<param name="UserIDClient">The id of the creator of the journal</param>
        ///<param name="JournalID">The name of the journal the entry is associated with</param>
        ///<param name="JounalEntryBody">The journal entry text</param>
        ///<param name="JournalEntryDate">The date the journal entry was created</param>
        ///<exception></exception>
        ///<returns>number depending on if the journal entry was deleted</returns>
        public int DeleteJournalEntry(int UserIDClient, string JournalID, string JounalEntryBody, DateTime JournalEntryDate)
        {
            int result = 0;
            List<JournalEntry> oneJournalEntry = journalEntries.Where(
                j => j.UserIDClient == UserIDClient &&
                    j.JournalID == JournalID &&
                    j.JournalEntryBody == JounalEntryBody &&
                    j.JournalEntryDate == JournalEntryDate).ToList();
            if (oneJournalEntry.Count == 1) 
            {
                JournalEntry journalEntry = oneJournalEntry[0];
                journalEntries.Remove(journalEntry);
                result = 1;
            }
            return result;
        }
		
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
        public int UpdateJournalEntry(JournalEntry OldJournalEntry, string NewJounalEntryBody)
        {
            List<JournalEntry> _jouralEntries = journalEntries;
            int result = 0;

            if (NewJounalEntryBody != null && OldJournalEntry != null)
            {
                JournalEntry updatedEntry = OldJournalEntry;
                updatedEntry.JournalEntryBody = NewJounalEntryBody;
                updatedEntry.JournalEditDate = DateTime.Now;
                _jouralEntries.Remove(OldJournalEntry);
                _jouralEntries.Add(updatedEntry);
                if (_jouralEntries.IndexOf(updatedEntry)>-1) 
                {
                    result = 1;
                }
            }

            return result;
        }
    }
}
