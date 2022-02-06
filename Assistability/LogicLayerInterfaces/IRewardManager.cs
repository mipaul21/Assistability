/// Nathaniel Webber
/// Updated: 2021/03/18
/// All necessary functions for the Award Object
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/01
/// Added Edit Reward Interface
/// </remarks>
///
///  <remarks>
/// Nick Loesel
/// Updated: 2021/04/08
/// Added Reactivate Reward Interface
/// </remarks>
/// 
///  <remarks>
/// Nick Loesel
/// Updated: 2021/04/08
/// Added Deactivate Reward Interface
/// </remarks>

using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IRewardManager
    {
        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/18
        /// 
        /// This is the interface of the method that will grab every Reward in the Database, 
        /// that is attached to the account.
        /// </summary>
        /// <exception>No Reward created</exception>
        /// <returns>Count of rows affected</returns>
        List<Reward> RetreiveAllRewards(int userID);

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/25
        /// 
        /// This is the interface of the method that will create a new Reward
        /// </summary>
        /// <exception>No Reward created</exception>
        /// <returns>Count of rows affected</returns>
        int CreateReward(int userID, string rewardName, string rewardDescription);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/01
        /// 
        /// This is the interface of the method that will edit a reward 
        /// </summary>
        /// <exception>No Reward edited</exception>
        /// <returns>Count of rows affected</returns>
        bool EditReward(Reward oldreward, Reward newReward);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// This is the interface of the method that will update a reward to active.
        /// </summary>
        /// 
        /// <param name="reward">The reward to reacivate</param>
        /// <param name="selectedUser">The Selected user to view rewards for</param>
        /// <exception>No Incidents found</exception>
        /// <returns>true if the reward was updated</returns>
        bool ReactivateReward(UserAccount selectedUser, Reward reward);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/08
        /// 
        /// This is the interface of the method that will update a reward to inactive.
        /// </summary>
        /// 
        /// <param name="reward">The reward to deactivate</param>
        /// <param name="selectedUser">The Selected user to view rewards for</param>
        /// <exception>No Incidents found</exception>
        /// <returns>true if the reward was updated</returns>
        bool DeactivateReward(UserAccount selectedUser, Reward reward);
    }
}
