using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;

namespace DataAccessInterfaces
{
    public interface IAttGoalAccessor
    {
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Interface for inserting an attainment goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="attGoal"></param>
        /// <returns></returns>
        int InsertAttainmentGoal(AttGoalViewModel attGoal);


        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Interface to get list of attainment goals by the userID of the client
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        List<AttGoalViewModel> SelectAttainmentGoalsByUserIDClient(int userID_client);


        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Interface to get list of active attainment goals by the userID of the client
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        List<AttGoalViewModel> SelectAttainmentGoalsByActive(int userID_client, bool active);


        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// interface to deactivate an attainment goal based on the attainment goal name
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="attGoalName"></param>
        /// <returns></returns>
        int DeactivateAttainmentGoal(int userID_client, string attGoalName, DateTime attGoalEntryDate);


        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// interface to reactivate an attainment goal based on the attainment goal name
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="attGoalName"></param>
        /// <returns></returns>
        int ReactivateAttainmentGoal(int userID_client, string attGoalName, DateTime attGoalEntryDate);


        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// interface to update an attainment goal record
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="oldAttGoal"></param>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        int UpdateAttainmentGoal(AttGoalViewModel oldAttGoal, AttGoalViewModel newAttGoal);


    }
}
