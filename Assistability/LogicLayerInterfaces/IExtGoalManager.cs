using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;

namespace LogicLayerInterfaces
{
   public interface IExtGoalManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// ///
        /// Changed name to AddExtinctionGoal from AddNewExtGoal and changed parameter to an ExtGoalViewModel
        /// </remarks>
        /// <param name="newExtGoal"></param>
        /// <returns></returns>
        bool AddExtinctionGoal(ExtGoalViewModel newExtGoal);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// ///
        /// Changed to List<ExtGoalViewModel> added userID_client parameter
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        List<ExtGoalViewModel> RetreiveAllExtGoals(int userID_client, bool active);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// ///
        /// Changed name to EditExtinctionGoal from UpdateExtGoal and switched to ExtGoalViewModel
        /// </remarks>
        /// <param name="oldExtGoal"></param>
        /// <param name="newExtGoal"></param>
        /// <returns></returns>
        bool EditExtinctionGoal(ExtGoalViewModel oldExtGoal, ExtGoalViewModel newExtGoal);

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Interface for Retrieving exinction goals by the userID_client
        /// </summary>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        List<ExtGoalViewModel> RetrieveExtinctionGoalsByUserIDClient(int userID_client);
    }
}
 