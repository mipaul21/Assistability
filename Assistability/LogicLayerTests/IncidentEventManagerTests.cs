using DataAccessFakes;
using DataAccessInterfaces;
using DataStorageModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class IncidentEventManagerTests
    {
        IIncidentEventAccessor incidentEventAccessor;


        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/17
        /// 
        /// This is method instantiates my IncidentFake for my tests.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            incidentEventAccessor = new IncidentEventFake();
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/22
        /// 
        /// This is the method testing SelectIncidentEventsByUserID.
        /// </summary>

        [TestMethod]
        public void TestGetIncidentEventsByUserIdReturnsAListOfIncidentEvents()
        {
            // arrange
            const int expectedCount = 1;
            int actualCount;
            string incidentName = "First Incident";
            List<IncidentEvent> incidentEvent;

            // act
            incidentEvent = incidentEventAccessor.SelectIncidentEventsByIncidentName(incidentName);
            actualCount = incidentEvent.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/23
        /// 
        /// This is the method testing InsertNewIncidentEvent.
        /// </summary>
        [TestMethod]
        public void TestAddIncidentAddsNewIncidentObject()
        {
            // arrange
            const bool expectedCount = true;
            bool actualCount;
            IncidentEvent newIncidentEvent = new IncidentEvent(5,"First Incident", new DateTime(2021, 3, 18), "Jimmy","Hit someone.","Has to hit a pillow when mad.", DateTime.Now ,3, 1);

            // act
            actualCount = incidentEventAccessor.InsertNewIncidentEvent(newIncidentEvent);


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/25
        /// 
        /// This is the method testing UpdateIncidentEvent.
        /// </summary>
        [TestMethod]
        public void TestUpdateIncidentEventUpdatesAnIncidentEvent()
        {
            // arrange
            const bool expectedCount = true;
            bool actualCount;
            IncidentEvent newIncidentEvent = new IncidentEvent(3, "Third Incident", new DateTime(2021, 3, 18), "Jimmy", "Hit someone.", "Has to hit a pillow when mad.", DateTime.Now, 3, 1);
            IncidentEvent oldIncidentEvent = new IncidentEvent(3, "Third Incident", new DateTime(2021, 3, 18), "Nathenial", "Nathaniel locked someone is his room.", "no locks on his door.", null, 3, 1);
            // act
            actualCount = incidentEventAccessor.UpdateIncidentEvent(oldIncidentEvent,newIncidentEvent);


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/26
        /// 
        /// This is the method testing DeleteIncidentEvent.
        /// </summary>
        
        [TestMethod]
        public void TestDeleteIncidentEventDeletesAnIncidentEvent()
        {
            // arrange
            const bool expectedCount = true;
            bool actualCount;
            const int incidentEventid = 2;

            // act
            actualCount = incidentEventAccessor.DeleteIncidentEvent(incidentEventid);


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/30
        /// 
        /// This is the method testing selectIncidentEventsbyId.
        /// </summary>
        [TestMethod]
        public void TestSelectIncidentEventByIdReturnsAnIncidentEventObject()
        {
            // arrange
            int incidentEventId = 1;
            const bool expectedCount = true;
            bool actualCount = false;
            IncidentEvent actualIncidentEvent = new IncidentEvent();
            IncidentEvent expectedIncidentEvent = new IncidentEvent(1, "First Incident", new DateTime(2021, 3, 18), "Nick", "Nick bit someone.", "has to eat vegetables.", null, 3, 1);
            // act
            actualIncidentEvent = incidentEventAccessor.SelectIncidentEventById(incidentEventId);

            if (expectedIncidentEvent.IncidentEventID == actualIncidentEvent.IncidentEventID
                && expectedIncidentEvent.IncidentName == actualIncidentEvent.IncidentName 
                && expectedIncidentEvent.PersonsInvolved == actualIncidentEvent.PersonsInvolved
                && expectedIncidentEvent.UserID_Admin == actualIncidentEvent.UserID_Admin
                && expectedIncidentEvent.UserID_Client == actualIncidentEvent.UserID_Client
                && expectedIncidentEvent.DateOfOccurence == actualIncidentEvent.DateOfOccurence
                && expectedIncidentEvent.EventConsequence == actualIncidentEvent.EventConsequence
                && expectedIncidentEvent.EventDescription == actualIncidentEvent.EventDescription
                && expectedIncidentEvent.EventEditDate == actualIncidentEvent.EventEditDate)
            {
                actualCount = true;
            }


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
 