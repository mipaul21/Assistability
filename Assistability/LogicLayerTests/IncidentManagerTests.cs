using DataAccessFakes;
using DataAccessInterfaces;
using DataStorageModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTests
{
    [TestClass]
    public class IncidentManagerTests
    {
        IIncidentAccessor incidentAccessor;


        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/17
        /// 
        /// This is method instantiates my IncidentFake for my tests.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            incidentAccessor = new IncidentFake();
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/17
        /// 
        /// This is the method testing SelectIncidentsByUserID.
        /// </summary>
        [TestMethod]
        public void TestGetIncidentByUserIdReturnsAListOfIncidents()
        {
            // arrange
            const int expectedCount = 3;
            int actualCount;
            int userID = 3;
            List<Incident> incident;

            // act
            incident = incidentAccessor.SelectIncidentsByUserID(userID);
            actualCount = incident.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// This is the method testing InsertNewIncident.
        /// </summary>
        [TestMethod]
        public void TestAddIncidentAddsNewIncidentObject()
        {
            // arrange
            const bool expectedCount = true;
            bool actualCount;
            Incident newIncident = new Incident("Test Incident", "Test Description", "Test Consequence", new DateTime(2021, 3, 18), null, null, true, 3, 1);

            // act
            actualCount = incidentAccessor.InsertNewIncident(newIncident);
            

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
		
		        [TestMethod]

        public void TestUpdateIncident()
        {
            // arrange 
            Incident oldIncident = new Incident
            {
                IncidentName = "testName",
                IncidentDescription = "testDescription",
                DesiredConsequence = "testDesiredConsequence",
                UserId_Client = 1,
                UserId_Creator = 2
        };

            Incident newIncident = new Incident
            {
                IncidentName = "testName2",
                IncidentDescription = "testDescription2",
                DesiredConsequence = "testDesiredConsequence2",
                UserId_Client = 4,
                UserId_Creator = 3

            };

            //act 
            int result = incidentAccessor.UpdateIncident(oldIncident, newIncident);
            bool actualResult = (result == 1);
            //assert
            Assert.AreEqual(true, actualResult);
        }
		
		 /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the method testing SelectIncidentsByActive.
        /// </summary>
        [TestMethod]
        public void TestSelectIncidentsByActiveReturnAListOfActiveIncidents()
        {
            // arrange
            const int expectedCount = 2;
            int actualCount;
            bool active = true;
            UserAccount selectedUser = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);
            List<Incident> incidents;

            // act
            incidents = incidentAccessor.SelectIncidentsByActive(selectedUser.UserAccountID, active);
            actualCount = incidents.Count;


            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the method testing ReactivateIncident.
        /// </summary>
        [TestMethod]
        public void TestReactivateIncidentReturnsTrue()
        {
            // arrange
            const bool expectedResult = true;
            bool actualResult;
            UserAccount selectedUser = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);
            Incident incident = new Incident("Third Incident", "Third Incident", "Test Consequence", new DateTime(2021, 3, 18), null, null, true, 3, 1);

            // act
            actualResult = incidentAccessor.ReactivateIncident(selectedUser, incident);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the method testing DeactivateIncident.
        /// </summary>
        [TestMethod]
        public void TestDeactivateIncidentReturnsTrue()
        {
            // arrange
            const bool expectedResult = true;
            bool actualResult;
            UserAccount selectedUser = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);
            Incident incident = new Incident("First Incident", "First Incident", "Test Consequence", new DateTime(2021, 3, 18), null, null, false, 3, 1);

            // act
            actualResult = incidentAccessor.DeactivateIncident(selectedUser, incident);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }
		
		
		
		
		
		
		
    }
}
