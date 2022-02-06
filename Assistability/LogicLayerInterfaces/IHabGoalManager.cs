/// <summary>
/// Becky Baenziger
/// Created: 2021/02/18
/// ///
/// Interface for the HabGoalManager
/// </summary>
/// <remarks>
/// Updater Name:
/// Update Date:
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;

namespace LogicLayerInterfaces
{
    public interface IHabGoalManager
    {
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Adds a new habitual goal to the Habitual Goal table
        /// </summary>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        bool AddHabitualGoal(HabGoalViewModel newHabGoal);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Views list of Hab Goals based on userIDclient
        /// </summary>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        List<HabGoalViewModel> RetrieveHabitualGoalsByUserIDClient(int userID_client);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Views list of hab goals based on userIDclient and active
        /// </summary>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        List<HabGoalViewModel> RetrieveHabitualGoalsByActive(int userID_client, bool active);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Edit a habitual goal
        /// </summary>
        /// <param name="oldHabGoal"></param>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        bool EditHabitualGoal(HabGoalViewModel oldHabGoal, HabGoalViewModel newHabGoal);
    }
}
