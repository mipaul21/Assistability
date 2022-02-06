/// Nathaniel Webber
/// Created: 2021/03/18
/// All necessary Fakes for testing 
/// </remarks>

using DataAccessInterfaces;
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class RewardFake : IRewardAccessor
    {
        private List<Reward> fakeReward = new List<Reward>();
        int rowsAffected;

        public RewardFake()
        {
            fakeReward.Add(new Reward
            {
                RewardID = 1,
                RewardName = "Get some Ice Cream",
                RewardDescription = "I can redeem this for one free ice cream",
                UserID = 3,
                Active = false
            });

            fakeReward.Add(new Reward
            {
                RewardID = 2,
                RewardName = "Get a toy",
                RewardDescription = "I can redeem this for one toy",
                UserID = 3,
                Active = true
            });

            fakeReward.Add(new Reward
            {
                RewardID = 3,
                RewardName = "10 more minutes",
                RewardDescription = "I can redeem this to stay up past my bedtime by ten minutes",
                UserID = 3,
                Active = true
            });
        }

        /// Nathaniel Webber
        /// Created: 2021/03/25 
        public int CreateNewReward(int userID, string rewardName, string rewardDescription)
        {
            return rowsAffected;
        }

        /// Nick Loesel
        /// Created: 2021/04/08
        public bool DeactivateReward(UserAccount selectedUser, Reward reward)
        {
            bool result = false;
            foreach (var Rewards in fakeReward)
            {
                if (reward.UserID == selectedUser.UserAccountID && reward.RewardName == Rewards.RewardName && Rewards.RewardID == reward.RewardID)
                {
                    reward.Active = false;
                    if (reward.Active == false)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        /// Nick Loesel
        /// Created: 2021/03/18
        public int EditReward(Reward oldReward, Reward newReward)
        {
            int result = -1;
            foreach (var reward in fakeReward)
            {
                if (oldReward.RewardName == reward.RewardName && oldReward.RewardID == reward.RewardID
                    && oldReward.RewardDescription == reward.RewardDescription && oldReward.UserID == reward.UserID
                    && oldReward.Active == reward.Active)
                {
                    reward.RewardID = newReward.RewardID;
                    reward.RewardDescription = newReward.RewardDescription;
                    reward.RewardName = newReward.RewardName;
                    reward.UserID = newReward.UserID;
                    reward.Active = newReward.Active;
                    if (reward.RewardID == newReward.RewardID && reward.RewardName == newReward.RewardName
                        && reward.RewardDescription == newReward.RewardDescription && reward.UserID == newReward.UserID
                        && reward.Active == newReward.Active)
                    {
                        result = 1;
                        break;
                    }
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        /// Nick Loesel
        /// Created: 2021/04/08
        public bool ReactivateReward(UserAccount selectedUser, Reward reward)
        {
            bool result = false;
            foreach (var Rewards in fakeReward)
            {
                if (reward.UserID == selectedUser.UserAccountID && reward.RewardName == Rewards.RewardName && Rewards.RewardID == reward.RewardID)
                {
                    reward.Active = true;
                    if (reward.Active == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        /// Nathaniel Webber
        /// Created: 2021/03/18
        public List<Reward> SelectAllRewards(int userID)
        {
            return this.fakeReward;
        }
    }
}
