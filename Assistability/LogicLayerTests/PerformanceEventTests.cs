/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/03/29
/// 
/// The tests validating the 
/// logic of the PerformanceEvent 
/// logic methods.
/// 
/// </summary>
///
/// <remarks>
/// </remarks>

using DataAccessFakes;
using DataAccessInterfaces;
using DataStorageModels;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayerTests
{
    /// <summary>
    /// Summary description for PerformanceEventTests
    /// </summary>

    [TestClass]
    public class PerformanceEventTests
    {
        IPerformanceEventAccessor performanceEventAccessor;
        IPerformanceEventManager performanceEventManager;
        PerformanceEvent fakePerformanceEvent = new PerformanceEvent();


        [TestInitialize]
        public void TestSetup()
        {
            performanceEventAccessor = new PerformanceEventAccessorFakes();
            performanceEventManager = new PerformanceEventManager(performanceEventAccessor);
        }

        ///// <summary>
        ///// Your Name: Whitney Vinson
        ///// Created: 2021/03/29
        ///// 
        ///// The test method to insert a performance event.
        ///// </summary>
        /////
        ///// <remarks>
        ///// </remarks>
        ///// <returns>Rows Affected</returns>

        [TestMethod]
        public void TestInsertNewPerformanceEvent()
        {
            // arrange
            bool result;
            string performanceName = "Fake";
            int clientID = 2;
            int adminID = 1;
            PerformanceEvent performanceEvent = new PerformanceEvent();

            // act
            result = performanceEventManager.AddNewPerformanceEvent(performanceName, clientID, adminID,
                                                        performanceEvent);

            // assert
            Assert.AreEqual(result, true);
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The test method to edit a performance event.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>Returns true</returns>

        [TestMethod]
        public void TestUpdatePerformanceEvent()
        {
            // arrange
            bool expected = true;
            bool result;

            // act
            result = performanceEventManager.EditPerformanceEvent(fakePerformanceEvent, new PerformanceEvent(
                 1, "OtherPerformanceDescription", "OtherResult"));

            // assert
            Assert.AreEqual(expected, result);
        }
        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The test method to select performance events
        /// by UserIDClient.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>Returns list of client performance events</returns>

        [TestMethod]
        public void TestSelectPerformanceEventsByUserID()
        {

            // arrange
            List<PerformanceEvent> performanceEvents;
            const int expectedCount = 3;
            int userIDClient = 1;
            int actualCount;

            // act
            performanceEvents = performanceEventManager.RetrieveAllPerformanceEventsByUserID(userIDClient);
            actualCount = performanceEvents.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The test method to select performance events
        /// by PerformanceName.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>Returns list of performance events</returns>

        [TestMethod]
        public void TestSelectPerformanceEventsByPerformanceName()
        {

            // arrange
            List<PerformanceEvent> performanceEvents;
            const int expectedCount = 1;
            string performanceName = "FirstPerformance";
            int actualCount;

            // act
            performanceEvents = performanceEventManager.RetrieveAllPerformanceEventsByPerformanceName(performanceName);
            actualCount = performanceEvents.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
