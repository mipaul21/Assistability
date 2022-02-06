using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;

namespace LogicLayerInterfaces
{
    public interface IAttGoalManager
    {
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// interface for adding a new attainment goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        bool AddAttainmentGoal(AttGoalViewModel newAttGoal);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// interface to view list of attainment goals based on userID
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date;
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        List<AttGoalViewModel> RetrieveAttainmentGoalsByUserIDClient(int userID_client);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// interface to view list of attainment goals based on userID and active
        /// </summary>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        List<AttGoalViewModel> RetrieveAttainmentGoalsByActive(int userID_client, bool active);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// interface for editing an attainment goal, includes deactivate and reactivate
        /// </summary>
        /// <param name="oldAttGoal"></param>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        bool EditAttainmentGoal(AttGoalViewModel oldAttGoal, AttGoalViewModel newAttGoal);
    }
}
