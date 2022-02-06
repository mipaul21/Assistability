using DataStorageModels;
using System.Collections.Generic;
using DataViewModels;
using System;

namespace DataAccessLayer
{
    public interface IExtGoalAccessor
    {
        int UpdateExtinctionGoal(ExtGoalViewModel oldExtGoal, ExtGoalViewModel newExtGoal);
        int InsertExtinctionGoal(ExtGoalViewModel extGoal);
        List<ExtGoalViewModel> SelectExtinctionGoalsByActive(int userID_client, bool active);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// interface to get a list of extinction goals by userIDclient
        /// </summary>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        List<ExtGoalViewModel> SelectExtinctionGoalsByUserIDClient(int userID_client);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// interface to deactive an extinction goal based on extgoal name
        /// </summary>
        /// <param name="extGoalName"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        int DeactivateExtinctionGoal(int userID_client, string extGoalName, DateTime extGoalEntryDate);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// interface to reactivate an extinction goal based on extgoal name
        /// </summary>
        /// <param name="extGoalName"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        int ReactivateExtinctionGoal(int userID_client, string extGoalName, DateTime extGoalEntryDate);

    }
}