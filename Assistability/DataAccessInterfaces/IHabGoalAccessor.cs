/// <summary>
/// Becky Baenziger
/// Created: 2021/02/18
/// ///
/// Interface for the habgoal accessor
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

namespace DataAccessInterfaces
{
    public interface IHabGoalAccessor
    {
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Interface for inserting/creating an habitual goal record
        /// </summary>
        /// <param name="habGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        int InsertHabitualGoal(HabGoalViewModel habGoal);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// interface to get a list of habitual goals by userIDclient
        /// </summary>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        List<HabGoalViewModel> SelectHabitualGoalsByUserIDClient(int userID_client);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Interface to get a list of habitual goals based on active and userID
        /// </summary>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        List<HabGoalViewModel> SelectHabitualGoalsByActive(int userID_client, bool active);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// interface to deactive an habitual goal based on habgoal name
        /// </summary>
        /// <param name="habGoalName"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        int DeactivateHabitualGoal(int userID_client, string habGoalName, DateTime habGoalEntryDate);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// interface to reactivate an habitual goal based on habgoal name
        /// </summary>
        /// <param name="habGoalName"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        int ReactivateHabitualGoal(int userID_client, string habGoalName, DateTime habGoalEntryDate);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/12
        /// ///
        /// interface to update an habitual goal record
        /// </summary>
        /// <param name="oldHabGoal"></param>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        int UpdateHabitualGoal(HabGoalViewModel oldHabGoal, HabGoalViewModel newHabGoal);
    }
}
