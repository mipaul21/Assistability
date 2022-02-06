/// <summary>
/// William Clark
/// Created: 2021/02/26
/// 
/// Test class for the UserGroupManager
/// </summary>
///
/// <remarks>
/// </remarks>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataStorageModels;
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class RoutineManagerTests
    {
        Routine arbitraryValidRoutine;
        UserAccount arbitraryValidUser;
        IRoutineManager _routineManager;

        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        /// 
        /// TestIntialize method that initializes an IRoutineManager
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestInitialize]
        public void TestSetup()
        {
            _routineManager = new RoutineManager(new RoutineFakes());
            arbitraryValidRoutine = new Routine("FirstRoutine", "First Routine", new DateTime(2021, 2, 26), 3, 1, true);
            arbitraryValidUser = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/18
        /// 
        /// Tests GetRoutineStepsByRoutine method returns a step list
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestGetRoutineStepsByRoutineReturnsRoutineStepList()
        {
            // Arrange
            int expectedResult = 4;
            List<RoutineStep> actualResult;

            // Act 
            actualResult = _routineManager.GetRoutineStepsByRoutine(arbitraryValidRoutine);

            // Assert
            Assert.AreEqual(expectedResult, actualResult.Count);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests GetRoutineStepsByRoutine method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetRoutineStepsByRoutineThrowsApplicationException()
        {
            // Arrange
            Routine invalidRoutine = new Routine("","",-1,-1,true);
            List<RoutineStep> actualResult = new List<RoutineStep>() ;

            // Act 
            actualResult = _routineManager.GetRoutineStepsByRoutine(invalidRoutine);

            // Assert 
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Tests UpdateRoutine method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestUpdateRoutineReturnsBool()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = _routineManager.UpdateRoutine(arbitraryValidRoutine, new Routine("FirstRoutine", "A change has been made", new DateTime(2021, 2, 26), 3, 1, false));

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests UpdateRoutine method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateRoutineThrowsException()
        {
            // Arrange
            Routine invalidUpdatedRoutine = new Routine("", "", -1, -1, true);
            bool actualResult;

            // Act 
            actualResult = _routineManager.UpdateRoutine(arbitraryValidRoutine, invalidUpdatedRoutine);

            // Assert 
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Tests SelectActiveRoutinesByUserAccountIDClient method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestSelectActiveRoutinesByUserAccountIDClientReturnsList()
        {
            // Arrange
            List<Routine> expectedResult = new List<Routine>() {
                new Routine("FirstRoutine", "First Routine", new DateTime(2021, 2, 26), 3, 1, true)
            };
            List<Routine> actualResult = new List<Routine>();
            int validUserAccountID = 3;

            // Act
            actualResult = _routineManager.SelectActiveRoutinesByUserAccountIDClient(validUserAccountID);

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
        public void TestSelectActiveRoutinesByUserAccountIDClientThrowsException()
        {
            // Arrange
            List<Routine> actualResult = new List<Routine>();
            int invalidUserAccountID = -1;

            // Act
            actualResult = _routineManager.SelectActiveRoutinesByUserAccountIDClient(invalidUserAccountID);

            // Assert 
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Tests CompleteRoutineStep method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestCompleteRoutineStepReturnsBool()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            RoutineStep routineStep = new RoutineStep(1, "FirstRoutine", "FirstStep", "The First Step", new DateTime(2021, 2, 26), 1, true);

            // Act
            actualResult = _routineManager.CompleteRoutineStep(routineStep, new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true));

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests TestCompleteRoutineStep method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestTestCompleteRoutineStepThrowsExceptionBadUser()
        {
            // Arrange
            RoutineStep routineStep = new RoutineStep(1, "FirstRoutine", "FirstStep", "The First Step", new DateTime(2021, 2, 26), 1, true);
            UserAccount invalidUser = new UserAccount(-1, "", "", "", "", true);
            bool actualResult;

            // Act
            actualResult = _routineManager.CompleteRoutineStep(routineStep, invalidUser);

            // Assert 
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests TestCompleteRoutineStep method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestTestCompleteRoutineStepThrowsExceptionBadRoutineStep()
        {
            // Arrange
            RoutineStep invalidRoutineStep = new RoutineStep(-1, "", "", "", new DateTime(2021, 2, 26), 1, true);
            bool actualResult;

            // Act
            actualResult = _routineManager.CompleteRoutineStep(invalidRoutineStep, arbitraryValidUser);

            // Assert 
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests CompleteRoutine method returns bool
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestCompleteRoutineReturnsBool()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = _routineManager.CompleteRoutine(arbitraryValidRoutine, arbitraryValidUser);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests TestCompleteRoutineStep method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestTestCompleteRoutineThrowsException()
        {
            // Arrange
            Routine invalidRoutine = new Routine("", "", -1, -1, true);
            bool actualResult;

            // Act
            actualResult = _routineManager.CompleteRoutine(invalidRoutine, arbitraryValidUser);

            // Assert 
            // Expects Exception
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/11
        /// 
        /// Tests Get Routines By UserID method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>

        [TestMethod]
        public void TestSelectRoutinestByUserAccountIDReturnsRoutinesCollection()
        {
            // Arrange
            const int expectedCount = 1;
            int actualCount;
            int userID = 3;
            List<Routine> routines;

            // Act
            routines = _routineManager.GetRoutinesByuserID(userID);
            actualCount = routines.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/11
        /// 
        /// Tests Add Routines Method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestAddNewRoutinesAddsNewRoutine()
        {
            // Arrange
            const bool expectedCount = true;
            bool actualCount;
            Routine routine = new Routine("SecondRoutine", "Second Routine", new DateTime(2021, 2, 26), 3, 1, true);



            // Act
            actualCount = _routineManager.AddNewRoutine(routine);


            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/31
        /// 
        /// Tests UpdateRoutineStep returns boolean
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestUpdateRoutineStepReturnsBool()
        {
            // Arrange
            RoutineStep originalStep = new RoutineStep()
            {
                RoutineStepID = 1,
                RoutineName = "Fake Routine Name",
                RoutineStepName = "Fake Routine Step Name 1",
                RoutineStepDescription = "The first fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-01"),
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = 1,
                Active = true

            };
            RoutineStep newRoutineStep = new RoutineStep()
            {
                RoutineStepID = 1,
                RoutineName = "Fake Routine Name",
                RoutineStepName = "Fake Routine Step Name 1",
                RoutineStepDescription = "The first fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-01"),
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = 2,
                Active = true

            };
            bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = _routineManager.UpdateRoutineStep(originalStep, newRoutineStep);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/31
        /// 
        /// Tests UpdateRoutineStep method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateRoutineStepThrowsException()
        {
            // Arrange
            RoutineStep originalInvalidStep = new RoutineStep()
            {
                RoutineName = "",
                RoutineStepName = "",
                RoutineStepDescription = "",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-01"),
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = -1,
                Active = true

            };
            RoutineStep newRoutineStep = new RoutineStep()
            {
                RoutineName = "Fake Routine Name",
                RoutineStepName = "Fake Routine Step Name 1",
                RoutineStepDescription = "The first fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-01"),
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = 2,
                Active = true

            };
            bool actualResult;

            // Act 
            actualResult = _routineManager.UpdateRoutineStep(originalInvalidStep, newRoutineStep);

            // Assert 
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/31
        /// 
        /// Tests SwapRoutineStepOrder returns boolean
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestSwapRoutineStepOrderReturnsBool()
        {
            // Arrange
            RoutineStep stepMovingForward = new RoutineStep(3, "FirstRoutine", "ThirdStep", "The Third Step", new DateTime(2021, 2, 26), 3, true);
            RoutineStep stepMovingBackward = new RoutineStep(2, "FirstRoutine", "SecondStep", "The Second Step", new DateTime(2021, 2, 26), 2, true);
            
            bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = _routineManager.SwapRoutineStepOrder(stepMovingBackward, stepMovingForward);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/31
        /// 
        /// Tests UpdateRoutineStep method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSwapRoutineStepOrderThrowsException()
        {
            // Arrange
            RoutineStep originalInvalidStep = new RoutineStep()
            {
                RoutineName = "",
                RoutineStepName = "",
                RoutineStepDescription = "",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-01"),
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = -1,
                Active = true

            };
            RoutineStep newRoutineStep = new RoutineStep()
            {
                RoutineName = "Fake Routine Name",
                RoutineStepName = "Fake Routine Step Name 1",
                RoutineStepDescription = "The first fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-01"),
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = 2,
                Active = true

            };
            bool actualResult;

            // Act 
            actualResult = _routineManager.SwapRoutineStepOrder(originalInvalidStep, newRoutineStep);

            // Assert 
            // Expects Exception
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
        public void TestSelectActiveRoutinesWithoutCompletionByUserAccountIDClientReturnsList()
        {
            // Arrange
            List<Routine> expectedResult = new List<Routine>() {
                new Routine("FirstRoutine", "First Routine", new DateTime(2021, 2, 26), 3, 1, true)
            };
            List<Routine> actualResult = new List<Routine>();
            int validUserAccountID = 3;

            // Act
            actualResult = _routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(new DateTime(2021, 2, 26), validUserAccountID);

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
        public void TestSelectActiveRoutinesWithoutCompletionByUserAccountIDClientReturnsEmptyList()
        {
            // Arrange
            List<Routine> expectedResult = new List<Routine>();
            List<Routine> actualResult = new List<Routine>();
            UserAccount client = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);

            // Act
            _routineManager.CompleteRoutine(new Routine("FirstRoutine", "First Routine", new DateTime(2021, 4, 1), 3, 1, true), client);
            actualResult = _routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(new DateTime(2021, 4, 1), client.UserAccountID);

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
        public void TestSelectActiveRoutinesWithoutCompletionByUserAccountIDClientThrowsException()
        {
            // Arrange
            List<Routine> actualResult = new List<Routine>();
            int invalidUserAccountID = -1;

            // Act
            actualResult = _routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime.Now, invalidUserAccountID);

            // Assert 
            // Expects Exception
        }
    }
}
