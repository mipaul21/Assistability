/// <summary>
/// Your Name: Whitney Vinson
/// Created: 2021/02/19
/// 
/// The tests validating the logic of the routine
/// step methods.
/// 
/// </summary>
///
/// <remarks>
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/19
/// </remarks>

using DataAccessFakes;
using DataAccessInterfaces;
using DataStorageModels;
using LogicInterfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayerTests
{

    [TestClass]
    public class RoutineStepTests
    {
        IRoutineStepAccessor routineStepAccessor;
        IRoutineStepManager routineStepManager;
        RoutineStep fakeRoutineStep;

        [TestInitialize]
        public void TestSetup()
        {
            routineStepAccessor = new RoutineStepAccessorFake();
            routineStepManager = new RoutineStepManager(routineStepAccessor);
            fakeRoutineStep = new RoutineStep(1, "FirstRoutine", "TestStep1", "TestDescription",
                 DateTime.Now, 1, true);
        }

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The test method to select all active routine steps.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>Returns list of all routine steps</returns>

        [TestMethod]
        public void TestSelectAllRoutineSteps()
        {

            // arrange
            List<RoutineStep> routineSteps;
            const int expectedCount = 5;
            int actualCount;

            // act
            routineSteps = routineStepManager.RetrieveAllRoutineSteps();
            actualCount = routineSteps.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The test method to insert a routine step.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="active">Retriving by active if true</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Returns list of active steps</returns>

        [TestMethod]
        public void TestInsertNewRoutineStep()
        {

            // arrange
            RoutineStep routineStep = new RoutineStep();
            bool result = false;

            // act
            result = routineStepManager.AddNewRoutineStep(routineStep);

            // assert
            Assert.AreEqual(true, result);

        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The test method to edit a routine step.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>Returns true</returns>

        [TestMethod]
        public void TestUpdateRoutineStep()
        {
            // arrange
            bool expected = true;
            bool result;

            // act
            result = routineStepManager.EditRoutineStep(fakeRoutineStep, new RoutineStep(
                "FirstRoutine", "TestStepOne", "TestDescription",
                 DateTime.Now, 1, true));

            // assert
            Assert.AreEqual(expected, result);
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The test method to select all active routine steps by active.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>Returns active steps</returns>

        [TestMethod]
        public void TestSelectRoutineStepsByActive()
        {
            // arrange
            List<RoutineStep> routineSteps;

            const int expectedCount = 3;
            int actualCount;
            bool active = true;

            // act
            routineSteps = routineStepManager.RetrieveRoutineStepsByActive(active);
            actualCount = routineSteps.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The accessor method to retrieve all active routine steps by routine name.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>Returns list of active steps by routine name</returns>

        [TestMethod]
        public void TestSelectActiveRoutineStepsByRoutineName()
        {

            // arrange
            List<RoutineStep> routineSteps;
            string routineName = "FirstRoutine";

            const int expectedCount = 3;
            int actualCount;
            bool active = true;

            // act
            routineSteps = routineStepManager.RetrieveActiveRoutineStepsByRoutineName(routineName, active);
            actualCount = routineSteps.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/08
        /// 
        /// Tests SelectRoutineStepCompletionsByDayByRoutineName method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestSelectRoutineStepCompletionsByDayByRoutineNameReturnsList()
        {
            // Arrange
            List<int> expectedResult = new List<int>() { 1 };
            List<int> actualResult = new List<int>();
            string validRoutineName = "Fake Routine Name";

            // Act
            actualResult = routineStepManager.SelectRoutineStepCompletionsByDayByRoutineName(validRoutineName, DateTime.Now);

            // Assert
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/01
        /// 
        /// Tests SelectActiveRoutinesWithoutCompletionByUserAccountIDClient method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestSelectRoutineStepCompletionsByDayByRoutineNameReturnsEmptyList()
        {
            // Arrange
            List<int> expectedResult = new List<int>();
            List<int> actualResult = new List<int>();
            string validRoutineName = "Fake Routine Name";

            // Act
            actualResult = routineStepManager.SelectRoutineStepCompletionsByDayByRoutineName(validRoutineName, DateTime.Now.AddDays(-1));

            // Assert
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests SelectActiveRoutinesByUserAccountIDClient method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectRoutineStepCompletionsByDayByRoutineNameThrowsException()
        {
            // Arrange
            List<int> expectedResult = new List<int>() { 1, 2, 4 };
            List<int> actualResult = new List<int>();
            string invalidRoutineName = "";

            // Act
            actualResult = routineStepManager.SelectRoutineStepCompletionsByDayByRoutineName(invalidRoutineName, DateTime.Now.AddDays(1));

            // Assert 
            // Expects Exception
        }
    }
}
