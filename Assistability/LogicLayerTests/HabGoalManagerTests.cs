///<summary>
/// Becky Baenziger
/// Created: 2021/02/18
/// ///
/// Class to test the access layer
/// 
///</summary>
///
///<remarks>
/// Updater Name:
/// Update Date:
/// 
///</remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataAccessInterfaces;
using DataViewModels;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    [TestClass]
    public class HabGoalManagerTests
    {
        IHabGoalAccessor habGoalAccessor;
        IHabGoalManager habGoalManager;

        [TestInitialize]
        public void TestSetup()
        {
            habGoalAccessor = new HabGoalAccessorFakes();
            habGoalManager = new HabGoalManager(habGoalAccessor);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Test to add/insert habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name: 
        /// Update Date:
        /// 
        /// </remarks>
        [TestMethod]
        public void TestAddHabitualGoal()
        {
            // arange
            HabGoalViewModel habGoal = new HabGoalViewModel();
            bool result = false;

            // act
            result = habGoalManager.AddHabitualGoal(habGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Test to retrieve an habitual goal by userIDclient
        /// </summary>
        /// <remarks>
        /// Updater Name: 
        /// Update Date:
        /// 
        /// </remarks>
        [TestMethod]
        public void TestRetrieveHabitualGoalsByUserIDClient()
        {

            // arrange
            const int expectedCount = 2;
            int userID_client = 3;
            int actualCount;
            List<HabGoalViewModel> data = new List<HabGoalViewModel>();

            // act
            data = habGoalManager.RetrieveHabitualGoalsByUserIDClient(userID_client);

            actualCount = data.Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Test to update an habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name: 
        /// Update Date:
        /// 
        /// </remarks>
        [TestMethod]
        public void TestUpdateHabitualGoals()
        {
            // arrange
            HabGoalViewModel oldHabGoal = new HabGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Mornings",
                HabGoalDescription = "establish a morning routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency = 1,
                Active = false,
                AwardName = "Award number 1",
                RoutineName = "Morning Routine"
            };
            HabGoalViewModel newHabGoal = new HabGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Mornings",
                HabGoalDescription = "establish a morning routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency = 2,
                Active = false,
                AwardName = "Award number 1",
                RoutineName = "Morning Routine"
            };
            bool result = false;

            // act
            result = habGoalManager.EditHabitualGoal(oldHabGoal, newHabGoal);

            // assert
            Assert.AreEqual(true, result);

        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Test to deactivate habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes to the accessor
        /// </remarks>
        [TestMethod]
        public void TestDeactivateHabitualGoal()
        {
            // arrange
            HabGoalViewModel oldHabGoal = new HabGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Bedtime",
                HabGoalDescription = "establish a bedtime routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency =1,
                Active = true,
                AwardName = "Award number 2",
                RoutineName = "Bedtime Routine"
            };
            HabGoalViewModel newHabGoal = new HabGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Bedtime",
                HabGoalDescription = "establish a bedtime routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency = 1,
                Active = false,
                AwardName = "Award number 2",
                RoutineName = "Bedtime Routine"
            };
            bool result; ;

            // act
            result = habGoalManager.EditHabitualGoal(oldHabGoal, newHabGoal);

            // assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Test to retrieve a list of habitual goals by userId_client and active
        /// </summary>
        /// <remarks>
        /// Updater Name: 
        /// Update Date:
        /// 
        /// </remarks>
        [TestMethod]
        public void TestRetrieveHabitualGoalsByActive()
        {
            //arrange
            const int expectedCount = 1;
            int userID_client = 3;
            bool active = true;
            int actualCount;
            List<HabGoalViewModel> data = new List<HabGoalViewModel>();

            //act
            data = habGoalManager.RetrieveHabitualGoalsByActive(userID_client, active);
            actualCount = data.Count();

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Test to reactivate habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Updated to reflect changes to the accessor
        /// </remarks>
        [TestMethod]
        public void TestReactivateHabitualGoal()
        {
            // arrange
            HabGoalViewModel oldHabGoal = new HabGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Mornings",
                HabGoalDescription = "establish a morning routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency = 1,
                Active = false,
                AwardName = "Award number 1",
                RoutineName = "Morning Routine"
            };
            HabGoalViewModel newHabGoal = new HabGoalViewModel() {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Mornings",
                HabGoalDescription = "establish a morning routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency = 1,
                Active = true,
                AwardName = "Award number 1",
                RoutineName = "Morning Routine"
            };
            bool result; ;

            // act
            result = habGoalManager.EditHabitualGoal(oldHabGoal, newHabGoal);

            // assert
            Assert.AreEqual(true, result);
        }
    }
}


