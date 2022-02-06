/// <summary>
/// Ryan Taylor
/// Created: 2021/03/30
/// 
/// Test class for the PerformanceManager
/// </summary>
using DataAccessFakes;
using DataStorageModels;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class PerformanceTests
    {
        private IPerformanceManager _performanceManager = null;
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// TestIntialize method that initializes an IPerformanceManager
        /// </summary>
        /// 
        [TestInitialize]
        public void TestSetup()
        {
            _performanceManager = new PerformanceManager(new PerformanceFakes());
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/31
        /// 
        /// Test method used to test if AddNewPerformace() works.
        /// </summary>
        [TestMethod]
        public void TestAddNewPerformance() 
        {
            //arrange
            const bool expectedResult = true;
            Performance test = new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 3,
                PerformanceName = "Test",
                PerformanceDescription = "More tests",
                PerformanceEntryDate = DateTime.Now,
                Active = true,
            };
            //act
            bool actualResult = _performanceManager.CreatePerformance(test.PerformanceName, 
                test.PerformanceDescription, test.UserID_client, test.UserIDCreator);
            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/31
        /// 
        /// Test method used to test if RetrievePerformanceByClient() works.
        /// </summary>
        [TestMethod]
        public void TestRetrievePerformanceByClient()
        {
            //arrange
            const int expectedResult = 3;
            int clientId = 3;
            //act
            int actualResult = 
                _performanceManager.RetrievePerformancesByClient(clientId).Count();
            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/31
        /// 
        /// Test method used to test if RetrievePerformanceByClientAndActive() works.
        /// </summary>
        [TestMethod]
        public void TestRetrievePerformanceByClientAndActive()
        {
            //arrange
            const int expectedResult = 2;
            int clientId = 3;
            //act
            int actualResult =
                _performanceManager.RetrievePerformancesByClientAndActive(clientId, true).Count();
            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/31
        /// 
        /// Test method used to test if EditPerformance() works.
        /// </summary>
        [TestMethod]
        public void TestEditPerformance()
        {
            //arrange
            const bool expectedResult = true;
            Performance oldPerformance = new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 1,
                PerformanceName = "different",
                PerformanceDescription = "Something different",
                PerformanceEntryDate = DateTime.Now,
                Active = true,
            };

            Performance editedPerformance = new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 3,
                PerformanceName = "different",
                PerformanceDescription = "Just changed something",
                PerformanceEditDate = DateTime.Now,
                Active = true,
            };
            //act
            bool actualResult = _performanceManager.EditePerformance(
                oldPerformance.PerformanceName, editedPerformance.PerformanceDescription,
                editedPerformance.UserID_client, oldPerformance.PerformanceDescription,
                oldPerformance.UserID_client);
            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/31
        /// 
        /// Test method used to test if DeactivateReactivatePerformance() works.
        /// </summary>
        [TestMethod]
        public void TestDeactivateReactivatePerformance()
        {
            //arrange
            const bool expectedResult = true;
            Performance deactivatePerformance = new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 1,
                PerformanceName = "different",
                PerformanceDescription = "Something different",
                PerformanceEntryDate = DateTime.Now,
                Active = true,
            };

            //act
            bool actualResult = 
                _performanceManager.DeactivateReactivatePerformance(
                    deactivatePerformance.PerformanceName, 
                    deactivatePerformance.UserID_client, 
                    deactivatePerformance.Active, false);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
