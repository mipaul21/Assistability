/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/08
/// 
/// This interface has the methods that will be
/// used in the AwardAccessor class
/// </summary>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/29
/// Added function to get active and not active awards
/// and to reactivate awards
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorageModels;

namespace DataAccessInterfaces
{
    public interface IAwardAccessor
    {
        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// Creates a new award
        /// </summary>
        /// 
        /// <param name="awardName"> The name of the Award being created</param>
        /// <param name="awardDescription"> The description of the Award being created</param>
        /// <param name="goalID"> the GoalID of the type of goal the award is set to</param>
        /// <exception>No Award found</exception>
        /// <returns>A List of Award objects</returns>
        int CreateNewAward(string awardName, string awardDescription);

        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// Selects an award by userID
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/24
        /// Changed return type to Award from List<Award>
        /// </remarks>
        /// <param name="awardName"> The Name  awards I'm viewing</param>
        /// <exception>No Award found</exception>
        /// <returns>A List of Award objects</returns>
        Award SelectAwardByAwardName(string awardName);

        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// Selects all awards (active and not)
        /// </summary>
        /// 
        /// <exception>No Award found</exception>
        /// <returns>A List of Award objects</returns>
        List<Award> SelectEveryAward();

        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// Selects all awards
        /// </summary>
        /// 
        /// <exception>No Award found</exception>
        /// <returns>A List of Award objects</returns>
        List<Award> SelectAllAwards(bool active = true);


        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// Updates an award
        /// </summary>
        /// 
        /// <param name="newAward"> The new, edited award</param>
        /// <param name="oldAward"> The oldAward record</param>
        /// <exception>No Award found</exception>
        /// <returns>Rows affected</returns>
        int UpdateAward(Award newAward, Award oldAward);

        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// Safely deactivates an award
        /// </summary>
        ///
        /// <param name="awardName"> The name of the award to be deactivated</param>
        /// <exception>No Award found</exception>
        /// <returns>Rows affected</returns>
        int SafelyDeactivateAwardByAwardName(string awardName);

        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// Safely reactivates an award
        /// </summary>
        /// 
        /// <param name="awardName"> The name of the award to be reactivated</param>
        /// <exception>No Award found</exception>
        /// <returns>Rows affected</returns>
        int ReactivateAwardByAwardName(string awardName);

        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// Safely deletes an award
        /// </summary>
        /// <param name="awardName"> The name of the award to be deleted</param>
        /// <exception>No Award found</exception>
        /// <returns>Rows affected</returns>
        int DeleteAward(string awardName);
    }
}
