/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/18
/// 
/// This interface has the methods that will be
/// used in the RewardAccessor class
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/01
/// Added edit reward interface
/// </remarks> 
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/01
/// Added ReactivateReward interface
/// </remarks> 
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/01
/// Added DeactivateReward interface
/// </remarks> 

using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IRewardAccessor
    {
        /// Nathaniel Webber
        /// Created: 2021/03/18
        /// 
        /// Selects all Rewards
        /// </summary>
        /// 
        /// <exception>No Reward found</exception>
        /// <returns>A List of Reward objects</returns>
        List<Reward> SelectAllRewards(int userID);

        /// Nathaniel Webber
        /// Created: 2021/03/25
        /// 
        /// Create a Reward
        /// </summary>
        /// 
        /// <exception>No Reward created</exception>
        /// <returns>int</returns>
        int CreateNewReward(int userID, string rewardName, string rewardDescription);

        /// Nick Loesel
        /// Created: 2021/04/01
        /// 
        /// The interface for editing a reward
        /// </summary>
        /// 
        /// <exception>No Reward edited</exception>
        /// <returns>number of rows affected</returns>
        int EditReward(Reward oldReward, Reward newReward);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// Impelemnts the interface for the ReactivateReward method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="selectedUser">The User to view rewards for</param>
        /// <param name="reward">The reward to reactivate</param>
        /// <exception cref="ApplicationException">The reward could not be reactivated</exception>
        /// <returns>True if the reward was successfully reactivated</returns>
        bool ReactivateReward(UserAccount selectedUser, Reward reward);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// Impelemnts the interface for the DeactivateReward method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="selectedUser">The User to view rewards for</param>
        /// <param name="reward">The reward to deactivate</param>
        /// <exception cref="ApplicationException">The reward could not be deactivated</exception>
        /// <returns>True if the reward was successfully deactivated</returns>
        bool DeactivateReward(UserAccount selectedUser, Reward reward);


    }
}
