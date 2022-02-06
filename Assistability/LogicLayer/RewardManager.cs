/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/18
/// 
/// This class has the methods that will be used for rewards
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/01
/// Added Edit Reward Method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/01
/// Added Reactivate Reward Method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/01
/// Added Deactivate Reward Method
/// </remarks>

using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class RewardManager : IRewardManager
    {
        private IRewardAccessor _rewardAccessor = null;

        public RewardManager()
        {
            // _rewardAccessor = new RewardFake();
            _rewardAccessor = new RewardAccessor();
        }

        public RewardManager(IRewardAccessor rewardAccessor)
        {
            _rewardAccessor = rewardAccessor;
        }


        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// This is the logic for the method that changes a reward to active.
        /// </summary>
        /// 
        /// <param name="reward">The reward to reactivate</param>
        /// <param name="selectedUser">The Selected user to change the reward for</param>
        /// <exception>No rewards found</exception>
        /// <returns>true if the reward was reactivated</returns>
  
  
        public bool ReactivateReward(UserAccount selectedUser, Reward reward)
        {
            bool result = false;

            try
            {
                result = _rewardAccessor.ReactivateReward(selectedUser, reward);
                if (result == false)
                {
                    throw new ApplicationException("New Reward was not reactivated.");
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("reactivate reward failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/25
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to create a reward in the database
        /// </summary>
        public int CreateReward(int userID, string rewardName, string rewardDescription)
        {
            int rowsAffected;

            try
            {
                rowsAffected = _rewardAccessor.CreateNewReward(userID, rewardName, rewardDescription);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Reward could not be created" + ex.InnerException);
            }

            return rowsAffected;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// This is the logic for the method that changes a reward to inactive.
        /// </summary>
        /// 
        /// <param name="reward">The reward to deactivate</param>
        /// <param name="selectedUser">The Selected user to change the reward for</param>
        /// <exception>No rewards found</exception>
        /// <returns>true if the reward was deactivated</returns>
        public bool DeactivateReward(UserAccount selectedUser, Reward reward)
        {
            bool result = false;

            try
            {
                result = _rewardAccessor.DeactivateReward(selectedUser, reward);
                if (result == false)
                {
                    throw new ApplicationException("New Reward was not deactivated.");
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("deactivate reward failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/01
        /// 
        /// This is the logic for the method that edit a reward.
        /// </summary>
        /// 
        /// <param name="oldreward">The old reward to update</param>
        /// <param name="newReward">The new reward to update</param>
        /// <exception>No reward changed</exception>
        /// <returns>true if the reward was edited</returns>
        public bool EditReward(Reward oldreward, Reward newReward)
        {
            int rowsAffected;

            try
            {
                rowsAffected = _rewardAccessor.EditReward(oldreward, newReward);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Reward could not be created" + ex.InnerException);
            }

            return rowsAffected == 1;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/18
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to retreive all rewards in the database
        /// </summary>
        public List<Reward> RetreiveAllRewards(int userID)
        {
            List<Reward> rewards;

            try
            {
                rewards = _rewardAccessor.SelectAllRewards(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Awards not found" + ex.InnerException);
            }

            return rewards;
        }
    }
}
