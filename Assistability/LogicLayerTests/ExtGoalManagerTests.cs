using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataViewModels;
using LogicLayerInterfaces;
using LogicLayer;
using DataAccessFakes;
using DataAccessLayer;
using DataAccessInterfaces;

namespace LogicLayerTests
{
    [TestClass]
    public class ExtGoalManagerTests
    {
        IExtGoalAccessor extGoalAccessor;
        IExtGoalManager extGoalManager;

        [TestInitialize]
        public void TestSetup()
        {
            extGoalAccessor = new ExtGoalAccessorFakes();
            extGoalManager = new ExtGoalManager(extGoalAccessor);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Test to add/insert an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        [TestMethod]
        public void TestAddExtinctionGoal()
        {
            // arrange
            ExtGoalViewModel extGoal = new ExtGoalViewModel();
            bool result = false;

            //act
            result = extGoalManager.AddExtinctionGoal(extGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Test to retrieve an extinction goal by userIDclient.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        [TestMethod]
        public void TestRetrieveAttainmentGoalsByUserIDClient()
        {
            // arrange
            const int expectedCount = 2;
            int userID_client = 3;
            int actualCount;
            List<ExtGoalViewModel> data = new List<ExtGoalViewModel>();

            // act
            data = extGoalManager.RetrieveExtinctionGoalsByUserIDClient(userID_client);
            actualCount = data.Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Test to update an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes in the accessor
        /// </remarks>
        [TestMethod]
        public void TestUpdateExtinctionGoals()
        {
            // arrange
            ExtGoalViewModel oldExtGoal = new ExtGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Thank You",
                ExtGoalDescription = "Say thank you",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 1,
                Active = false,
                AwardName = "Award number 3",
                IncidentName = "Say thank you"
            };
            ExtGoalViewModel newExtGoal = new ExtGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Thank You",
                ExtGoalDescription = "Say thank you",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 2,
                Active = false,
                AwardName = "Award number 3",
                IncidentName = "Say thank you"
            };
            bool result = false;

            // act
            result = extGoalManager.EditExtinctionGoal(oldExtGoal, newExtGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Test to deactivate an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes in the accessor
        /// </remarks>
        [TestMethod]
        public void TestDeactivateExtinctionGoal()
        {
            // arrange
            ExtGoalViewModel oldExtGoal = new ExtGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Sportsmanship",
                ExtGoalDescription = "Demonstrate good sportsmanship",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 1,
                Active = true,
                AwardName = "Award number 2",
                IncidentName = "sportsman like conduct"
            };
            ExtGoalViewModel newExtGoal = new ExtGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Sportsmanship",
                ExtGoalDescription = "Demonstrate good sportsmanship",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 1,
                Active = false,
                AwardName = "Award number 2",
                IncidentName = "sportsman like conduct"
            };
            bool result = false;

            // act
            result = extGoalManager.EditExtinctionGoal(oldExtGoal, newExtGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Test to reactivate an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes in the accessor
        /// </remarks>
        [TestMethod]
        public void TestReactivateExtinctionGoal()
        {
            // arrange
            ExtGoalViewModel oldExtGoal = new ExtGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Thank You",
                ExtGoalDescription = "Say thank you",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 1,
                Active = false,
                AwardName = "Award number 3",
                IncidentName = "Say thank you"
            };
            ExtGoalViewModel newExtGoal = new ExtGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Thank You",
                ExtGoalDescription = "Say thank you",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 1,
                Active = true,
                AwardName = "Award number 3",
                IncidentName = "Say thank you"
            };
            bool result = false;

            // act
            result = extGoalManager.EditExtinctionGoal(oldExtGoal, newExtGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Test to retrieve a list if extinction goals by active and useridclient.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        [TestMethod]
        public void TestRetrieveExtinctionGoalsByActive()
        {
            // arrange
            const int expectedCount = 1;
            int userID_client = 3;
            bool active = true;
            int actualCount;
            List<ExtGoalViewModel> data = new List<ExtGoalViewModel>();

            // act
            data = extGoalManager.RetreiveAllExtGoals(userID_client, active);
            actualCount = data.Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
