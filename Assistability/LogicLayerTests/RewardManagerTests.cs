/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/18
/// 
/// This is the Class containing the Reward Manager Test methods
/// </summary>

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
    public class RewardManagerTests
    {
        IRewardAccessor rewardAccessor;

        [TestInitialize]
        public void setUp()
        {
            rewardAccessor = new RewardFake();
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/18
        /// 
        /// This tests the RetreiveAllRewards method
        /// </summary>
        [TestMethod]
        public void TestRetreiveAllRewardsReturnsCollection()
        {
            // Arrange
            const int expectedTotal = 3;
            int userID = 1;
            int actualTotal;
            List<Reward> rewards;

            // Act
            rewards = rewardAccessor.SelectAllRewards(userID);
            actualTotal = rewards.Count;

            // Assert
            Assert.AreEqual(expectedTotal, actualTotal);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/25
        /// 
        /// This tests the CreateNewReward method
        /// </summary>
        [TestMethod]
        public void TestCreateNewReward()
        {
            // Arrange
            int userID = 1;
            string rewardName = "New Reward Name";
            string rewardDescription = "New Reward Description";

            // Act
            int createReward = rewardAccessor.CreateNewReward(userID, rewardName, rewardDescription);

            // Assert
            Assert.IsTrue(createReward == 0);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/01
        /// 
        /// This tests the EditReward method
        /// </summary>
        [TestMethod]
        public void TestEditRewardReturnsNewRewardObject()
        {
            // Arrange
            int expectedResult = 1;
            Reward oldReward = new Reward(1, "Get some Ice Cream", "I can redeem this for one free ice cream", 3, false);
            Reward newReward = new Reward(2, "Second Reward", "TestReward2", 3, true);

            // Act
            int actualResult = rewardAccessor.EditReward(oldReward, newReward);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// This tests the RewactivateReward method
        /// </summary>
        [TestMethod]
        public void TestReactivateRewardReactivatesReward()
        {
            // arrange
            const bool expectedResult = true;
            bool actualResult;
            UserAccount selectedUser = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);
            Reward reward = new Reward(1, "Get some Ice Cream", "Third reward", 3, false);

            // act
            actualResult = rewardAccessor.ReactivateReward(selectedUser, reward);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// This tests the DeactivateReward method
        /// </summary>
        [TestMethod]
        public void TestDeactivateRewardDeactivatesReward()
        {
            // arrange
            const bool expectedResult = true;
            bool actualResult;
            UserAccount selectedUser = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);
            Reward reward = new Reward(2, "Get a toy", "Third reward", 3, true);

            // act
            actualResult = rewardAccessor.DeactivateReward(selectedUser, reward);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
