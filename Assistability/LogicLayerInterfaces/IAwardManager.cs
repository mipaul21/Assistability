/// Nathaniel Webber
/// Updated: 2021/03/08
/// All necessary functions for the Award Object
/// </remarks>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/29
/// Added function to get active and not active awards
/// and to reactivate awards
/// </remarks>

using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IAwardManager
    {
        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// This method will allow the user to create a new award
        /// </summary>
        /// <param name="userID">The UserID of the User who created this Award</param>
        /// <param name="awardName">The Name of this Award</param>
        /// <param name="awardDescription">The Description of this Award</param>
        /// <exception>No Award created</exception>
        /// <returns>Count of rows affected</returns>
        int CreateAward(string awardName, string awardDescription);

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// This is the interface of the method that will grab every Award in the Database, 
        /// which will require a UserID.
        /// </summary>
        /// <param name="userID">The UserID of the User who created this Award</param>
        /// <exception>No Award created</exception>
        /// <returns>Count of rows affected</returns>
        Award RetreiveAwardByAwardName(string awardName);

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to retreive all awards (active and not) in the database
        /// </summary>
        List<Award> RetreiveEveryAward();

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// This is the interface of the method that will grab every Award in the Database, 
        /// that is attached to the account.
        /// </summary>
        /// <exception>No Award created</exception>
        /// <returns>Count of rows affected</returns>
        List<Award> RetreiveAllAwards(bool active = true);

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// This method will update an Award
        /// </summary>
        /// <param name="newAward">The newly edited Award</param>
        /// <param name="oldAward">The old award of this Award</param>
        /// <exception>Could not Edit</exception>
        /// <returns>Count of rows affected</returns>
        int UpdateAward(Award newAward, Award oldAward);

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// This method will deactivate an Award no longer in use
        /// </summary>
        /// <param name="userID">The UserID of the User who created this Award</param>
        /// <param name="awardName">The Name of this Award</param>
        /// <exception>No Award deactivated</exception>
        /// <returns>Count of rows affected</returns>
        int DeactivateAwardByAwardName(string awardName);

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This method will reactivate an Award
        /// </summary>
        /// <param name="awardName">The Name of this Award</param>
        /// <exception>No Award reactivated</exception>
        /// <returns>Count of rows affected</returns>
        int ReactivateAwardByAwardName(string awardName);

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/08
        /// 
        /// This method will delete an Award no longer in use
        /// </summary>
        /// <param name="userID">The UserID of the User who created this Award</param>
        /// <param name="awardName">The Name of this Award</param>
        /// <exception>No Award deleted</exception>
        /// <returns>Count of rows affected</returns>
        int DeleteAward(string awardName);
    }
}
