/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/09
/// 
/// This is the Class containing the Award Manager Test methods
/// </summary>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/29
/// Added function to test get active and not active awards
/// and reactivate award
/// and to reactivate awards
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataAccessInterfaces;
using DataStorageModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    [TestClass]
    public class AwardManagerTests
    {
        IAwardAccessor awardAccessor;

        [TestInitialize]
        public void setUp()
        {
            awardAccessor = new AwardFake();
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/09
        /// 
        /// This tests the CreateAward Method
        /// </summary>
        [TestMethod]
        public void TestCreateAward()
        {
            // Arrange
            string awardName = "Test Award";
            string awardDescription = "Test Description";

            // Act
            int createAward = awardAccessor.CreateNewAward(awardName, awardDescription);

            // Assert
            Assert.IsTrue(createAward == 0);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/09
        /// 
        /// This tests the RetreiveAwardByAwardID method
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/24
        /// Changed test to look for single award v. a list of awards.
        /// </remarks>
        [TestMethod]
        public void TestRetreiveAwardByAwardName()
        {
            // Arrange
            string awardName = "Fake Award 1";
            Award award;

            Award origAward = new Award()
            {
                AwardName = "Fake Award 1",
                AwardDescription = "Fake Award Description 1",
                Active = true
            };
            

            // Act
            award = awardAccessor.SelectAwardByAwardName(awardName);

            // Assert
            Assert.AreEqual(origAward.AwardName, award.AwardName);
            Assert.AreEqual(origAward.AwardDescription, award.AwardDescription);
            Assert.AreEqual(origAward.Active, award.Active);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/09
        /// 
        /// This tests the RetreiveAllAwards method
        /// </summary>
        [TestMethod]
        public void TestRetreiveAllAwards()
        {
            // Arrange
            const int expectedCount = 4;
            int actualCount;
            List<Award> award;

            // Act
            award = awardAccessor.SelectAllAwards();
            actualCount = award.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This tests the RetreiveEveryAward method
        /// </summary>
        [TestMethod]
        public void TestRetreiveEveryAward()
        {
            // Arrange
            const int expectedCount = 4;
            int actualCount;
            List<Award> award;

            // Act
            award = awardAccessor.SelectEveryAward();
            actualCount = award.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/09
        /// 
        /// This tests the UpdateAward method
        /// </summary>
        [TestMethod]
        public void TestUpdateAward()
        {
            // Arrange
            Award newAward = new Award
            {
                AwardName = "Test Award",
                AwardDescription = "Test Description",
            };
            Award oldAward = new Award
            {
                AwardName = "Rewritten Test Award",
                AwardDescription = "Test Description",
            };

            // Act
            int changed = awardAccessor.UpdateAward(newAward, oldAward);

            // Assert
            Assert.IsTrue(changed == 0);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/09
        /// 
        /// This tests the SafelyDeactivateAward method
        /// </summary>
        [TestMethod]
        public void TestDeactivateAward()
        {
            // Arrange
            Award newAward = new Award
            {
                AwardName = "Test Award",
                AwardDescription = "Test Description",
                Active = true
            };

            // Act
            int deactivated = awardAccessor.SafelyDeactivateAwardByAwardName(newAward.AwardName);

            // Assert
            Assert.IsTrue(deactivated == 0);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This tests the ReactivateAward method
        /// </summary>
        [TestMethod]
        public void TestReactivateAward()
        {
            // Arrange
            Award newAward = new Award
            {
                AwardName = "Test Award",
                AwardDescription = "Test Description",
                Active = true
            };

            // Act
            int deactivated = awardAccessor.ReactivateAwardByAwardName(newAward.AwardName);

            // Assert
            Assert.IsTrue(deactivated == 0);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/09
        /// 
        /// This tests the DeleteAward method
        /// </summary>
        [TestMethod]
        public void TestDeleteAward()
        {
            // Arrange
            Award newAward = new Award
            {
                AwardName = "Test Award",
                AwardDescription = "Test Description",
                Active = true
            };

            // Act
            int delete = awardAccessor.DeleteAward(newAward.AwardName);

            // Assert
            Assert.IsTrue(delete == 0);
        }
    }
}
