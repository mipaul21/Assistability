/// <summary>
/// Jory A. Wernette
/// Created: 2021/02/19
/// 
/// This is the Class containing the Journal Manager Test methods
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
/// Added TestInsertJournalEntry Test Method
/// Added TestRetrieveJournalEntries Test Method
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/08
/// Added DeleteJournalEntry Test Method
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/14
/// Added EditJournalEntry Test Method
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStorageModels;

namespace LogicLayerTests
{

    /// <summary>
    /// Jory A. Wernette
    /// Created: 2021/02/12
    /// 
    /// This is the Class containing the Journal Manager Test methods
    /// </summary>
    [TestClass]
    public class JournalManagerTests
    {
		JournalEntry newJournalEntry = new JournalEntry()
        {
            UserIDClient = 1,
            UserIDClientJournal = 1,
            JournalID = "Randome Journal",
            JournalEntryBody = "Just here to say I love your work",
            JournalEntryDate = DateTime.Now

        };
        IJournalAccessor journalAccessor;


        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/12
        /// 
        /// This is method instantiates my JournalAccessorFake for my tests.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            journalAccessor = new JournalAccessorFake();
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/12
        /// 
        /// This is the method testing SelectAllJournals.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAlJournalsReturnsCollection()
        {
            // arrange
            const int expectedCount = 4;
            int actualCount;
            List<Journal> journal;

            // act
            journal = journalAccessor.SelectAllJournals();
            actualCount = journal.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/12
        /// 
        /// This is the method testing SelectAllJournalsByUserID.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAlJournalsByUserID()
        {
            // arrange
            int UserID = 927;
            const int expectedCount = 4;
            int actualCount;
            List<Journal> journal;

            // act
            journal = journalAccessor.SelectAllJournalsByUserID(UserID);
            actualCount = journal.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This is the method testing SelectJournalByUserIDAndJournalName.
        /// </summary>
        [TestMethod]
        public void TestRetrieveJournalByUserIDAndJournalName()
        {
            // arrange
            int UserID = 927;
            string expectedJournalName = "Capstone";
            string expectedJournalDescription = "Contains my weekly entries about our Capstone Project.";
            Journal actualJournal;

            // act
            actualJournal = journalAccessor.SelectJournalByUserIDAndJournalName(UserID, expectedJournalName);

            // assert
            Assert.AreEqual(expectedJournalDescription, actualJournal.JournalDescription);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/19
        /// 
        /// This is the method testing CreateAJournalByUserID.
        /// It also takes JournalName and Journal Description
        /// </summary>
        [TestMethod]
        public void TestCreateAJournalByUserID()
        {
            // Arrange
            int userID = 927;
            string journalName = "Ragbrai Journal";
            string journalDescription = "My journal describing my RAGBRAI experience";

            // Act
            int inserted = journalAccessor.CreateNewJournal(userID, journalName, journalDescription);

            // Assert
            Assert.IsTrue(inserted == 0);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/19
        /// 
        /// This is the method testing CreateAJournalByUserID.
        /// It also takes JournalName and Journal Description
        /// </summary>
        [TestMethod]
        public void TestUpdateJournal()
        {
            // Arrange
            Journal newJournal = new Journal
            {
                UserID_Client = 927,
                JournalName = "Ragbrai Journal",
                JournalDescription = "My journal describing my RAGBRAI experience"
            };

            Journal oldJournal = new Journal
            {
                UserID_Client = 927,
                JournalName = "Adventure Journal",
                JournalDescription = "My journal describing my RAGBRAI adventure"
            };

            // Act
            int inserted = journalAccessor.UpdateJournal(newJournal, oldJournal);
            // Assert
            Assert.IsTrue(inserted == 0);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/19
        /// 
        /// This is the method testing CreateAJournalByUserID.
        /// It also takes JournalName and Journal Description
        /// </summary>
        [TestMethod]
        public void TestDeleteJournal()
        {
            // Arrange
            Journal newJournal = new Journal
            {
                UserID_Client = 927,
                JournalName = "Journal to be deleted",
                JournalDescription = "My journal that will soon be deleted"
            };

            // Act
            int result = journalAccessor.DeleteJournalByUserIDAndJournalName(newJournal.UserID_Client, newJournal.JournalName);
            // Assert
            Assert.IsTrue(result == 0);
        }
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// This class is used to test the logic layer add journal entry methode.
        /// </summary>
        [TestMethod]
        public void TestInsertNewJournalEntry()
        {
            //arrange
            const int expectedResult = 1;

            //act
            int acutalResult = journalAccessor.InsertNewJournalEntry(newJournalEntry);

            //assert
            Assert.AreEqual(expectedResult, acutalResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/16
        /// 
        /// This class is used to test the logic layer 
        /// retieve journal enteis for a journal methode.
        /// </summary>
        [TestMethod]
        public void TestRetrieveJournalEntries()
        {
            //arrange
            string journalID = "Randome Journal";
            int userID = 1;
            const int expectedResult = 2;

            //act
            List<JournalEntry> journalEntries =
                journalAccessor.SelectJournalEntries(userID, journalID);
            int actualResult = journalEntries.Count;

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/16
        /// 
        /// This class is used to test the logic layer 
        /// retieve journal enteis for a journal methode.
        /// </summary>
        [TestMethod]
        public void TestDeleteJournalEntry() 
        {
            //arrange
            const int expectedResult = 1;
            int UserIDClient = 1;
            string JournalID = "Randome Journal";
            string JournalEntryBody = "Just here to say I love your work, sike";
            DateTime JournalEntryDate = new DateTime(2021, 11, 3, 1, 00, 36);

            //act
            int actualResult = journalAccessor.DeleteJournalEntry(UserIDClient, JournalID,
                JournalEntryBody, JournalEntryDate);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/14
        /// 
        /// This class is used to test the logic layer 
        /// of edting a journal entry.
        /// </summary>
        [TestMethod]
        public void TestEditJournalEntry()
        {
            //arrange
            const int expectedResult = 1;
            JournalEntry oldJouranlEntry = new JournalEntry()
            {
                UserIDClient = 1,
                UserIDClientJournal = 1,
                JournalID = "Randome Journal",
                JournalEntryBody = "Just here to say I love your work, sike",
                JournalEntryDate = new DateTime(2021, 11, 3, 1, 00, 36)
            };
            string newJournalEntryBody = "I just changed the entry.";

            //act
            int actualResult = journalAccessor.UpdateJournalEntry(oldJouranlEntry, newJournalEntryBody);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
