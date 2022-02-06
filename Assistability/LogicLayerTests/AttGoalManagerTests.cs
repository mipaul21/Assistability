/// <summary>
/// Becky Baenziger
/// Created: 2021/04/02
/// ///
/// Tests for the Attainment Goal logic.
/// </summary>
/// <remarks>
/// Updater Name:
/// Update Date:
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayerInterfaces;
using LogicLayer;
using DataAccessFakes;
using DataAccessInterfaces;
using DataViewModels;

namespace LogicLayerTests
{
    [TestClass]
    public class AttGoalManagerTests
    {
        IAttGoalAccessor attGoalAccessor;
        IAttGoalManager attGoalManager;

        [TestInitialize]
        public void TestSetup()
        {
            attGoalAccessor = new AttGoalAccessorFakes();
            attGoalManager = new AttGoalManager(attGoalAccessor);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Test to add/insert an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        [TestMethod]
        public void TestAddAttainmentGoal()
        {
            // arrange
            AttGoalViewModel attGoal = new AttGoalViewModel();
            bool result = false;

            //act
            result = attGoalManager.AddAttainmentGoal(attGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Test to retrieve a list of attainment goals by userIDclient.
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
            List<AttGoalViewModel> data = new List<AttGoalViewModel>();

            // act
            data = attGoalManager.RetrieveAttainmentGoalsByUserIDClient(userID_client);
            actualCount = data.Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Test to update an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes to the accessor.
        /// </remarks>
        [TestMethod]
        public void TestUpdateAttainmentGoals()
        {
            // arrange
            AttGoalViewModel oldAttGoal = new AttGoalViewModel()
            {
                UserID_client = 1,
                UserID_admin = 3,
                AttGoalName = "Sportsmanship",
                AttGoalDescription = "Demonstrate good sportsmanship",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 1,
                Active = true,
                AwardName = "Award number 2",
                PerformanceName = "sportsman like conduct"
            };
            AttGoalViewModel newAttGoal = new AttGoalViewModel()
            {
                UserID_client = 1,
                UserID_admin = 3,
                AttGoalName = "Sportsmanship",
                AttGoalDescription = "Demonstrate good sportsmanship",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 2,
                Active = true,
                AwardName = "Award number 2",
                PerformanceName = "sportsman like conduct"
            };
            bool result = false;

            // act
            result = attGoalManager.EditAttainmentGoal(oldAttGoal, newAttGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Test to deactivate an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes to the accessor.
        /// </remarks>
        [TestMethod]
        public void TestDeactivateAttainmentGoal()
        {
            // arrange
            AttGoalViewModel oldAttGoal = new AttGoalViewModel() {
                UserID_client = 1,
                UserID_admin = 3,
                AttGoalName = "Sportsmanship",
                AttGoalDescription = "Demonstrate good sportsmanship",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 1,
                Active = true,
                AwardName = "Award number 2",
                PerformanceName = "sportsman like conduct"
            };
            AttGoalViewModel newAttGoal = new AttGoalViewModel() {
                UserID_client = 1,
                UserID_admin = 3,
                AttGoalName = "Sportsmanship",
                AttGoalDescription = "Demonstrate good sportsmanship",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 1,
                Active = false,
                AwardName = "Award number 2",
                PerformanceName = "sportsman like conduct"
            };
            bool result = false;

            // act
            result = attGoalManager.EditAttainmentGoal(oldAttGoal, newAttGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Test to reactivate an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes to the accessor.
        /// </remarks>
        [TestMethod]
        public void TestReactivateAttainmentGoal()
        {
            // arrange
            AttGoalViewModel oldAttGoal = new AttGoalViewModel()
            {
                UserID_client = 1,
                UserID_admin = 3,
                AttGoalName = "Thank You",
                AttGoalDescription = "Say thank you",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 1,
                Active = false,
                AwardName = "Award number 3",
                PerformanceName = "Say thank you"
            };
            AttGoalViewModel newAttGoal = new AttGoalViewModel()
            {
                UserID_client = 1,
                UserID_admin = 3,
                AttGoalName = "Thank You",
                AttGoalDescription = "Say thank you",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 1,
                Active = true,
                AwardName = "Award number 3",
                PerformanceName = "Say thank you"
            };
            bool result = false;

            // act
            result = attGoalManager.EditAttainmentGoal(oldAttGoal, newAttGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Test to retrieve a list of attainment goals by active and userIDclient.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        [TestMethod]
        public void TestRetrieveAttainmentGoalsByActive()
        {
            // arrange
            const int expectedCount = 1;
            int userID_client = 3;
            bool active = true;
            int actualCount;
            List<AttGoalViewModel> data = new List<AttGoalViewModel>();

            // act
            data = attGoalManager.RetrieveAttainmentGoalsByActive(userID_client, active);
            actualCount = data.Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
