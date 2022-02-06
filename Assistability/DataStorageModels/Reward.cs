/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/18
/// 
/// This is a storage model for the Reward Objects
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
    public class Reward
    {
        public int RewardID { get; set; }
        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public int UserID { get; set; }
        public bool? Active { get; set; }

        public Reward(int rewardID, string rewardName, string rewardDescription, int userID, bool? active)
        {
            RewardID = rewardID;
            RewardName = rewardName;
            RewardDescription = rewardDescription;
            UserID = userID;
            Active = active;
        }
        public Reward()
        {

        }
    }
}
