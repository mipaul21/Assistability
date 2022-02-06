/// <summary>
/// Nathaniel Webber
/// Created: 2021/02/05
/// 
/// This is an interface for the management
/// of the users data
/// </summary>
///
/// <remarks>
/// Nathaniel Webber
/// Updated: 2021/02/18
/// First MVP delivered
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/06
/// Added AddNewUserAccount
/// </remarks>
///
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/03/12
/// Added logout
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/25
/// Added GetUserAccountByUsername and ResetPassword
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorageModels;
using DataViewModels;

namespace LogicLayerInterfaces
{
    
    public interface IUserManager
    {
        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/02/05
        /// 
        /// Describes what parameters are needed to authenticate 
        /// a user login
        /// </summary>
        ///
        /// <remarks>
        /// Nathaniel Webber
        /// Updated: 2021/02/18 
        /// First MVP delivered
        /// </remarks>
        UserAccount AuthenticateUser(string username, string password);

        /// <summary>
        /// William Clark
        /// Created: 2021/02/18
        /// 
        /// Selects a UserAccount from the accessor with the matching UserAccountID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount to be selected</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>A UserAccount object</returns>
        UserAccount GetUserAccountByUserAccountID(int userAccountID);

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a UserAccountVM from the accessor with the matching UserAccount
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="user">The UserAccount of the UserAccountVM to be selected</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>A UserAccountVM object</returns>
        UserAccountVM GetUserAccountVMByUserAccount(UserAccount user);


        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// 
        /// Logic layer method in order to update a user account. 
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        bool UpdateUserAccount(UserAccount oldUserAccount, UserAccount newUserAccount);

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/10/2021
        /// Approver: 
        /// 
        /// Logic layer method in order to delete a user account.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        bool DeleteUserAccount(UserAccount userAccount);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// An abstract method for adding a user account
        /// </summary>
        /// 
        ///<param name="newUserAccount">The newly created user account</param>
        ///<param name="password">The user accounts password</param>
        ///<exception></exception>
        ///<returns>A bool signifying if the account was created</returns>
        bool AddNewUserAccount(UserAccount newUserAccount, string password);

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        ///
        /// Creates a subsidiary user account without their own group
        /// </summary>
        ///
        ///<param name="newUserAccount">The newly created user account</param>
        ///<param name="password">The pasword for that user account</param>
        ///<exception cref="ApplicationException">Could not create UserAccount/exception>
        ///<returns>UserAccountID of created user</returns>
        int  AddSubsidiaryUserAccount(UserAccount newUserAccount, string password);

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/12
        /// 
        /// Gets a User and sets all of the UserAccount fields to default 
        /// and send user to login screen
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="user">The UserAccount of the User to log out</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>Bool confirming logout success</returns>
        UserAccount LogoutUser(UserAccount user);
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/25
        /// 
        /// An abstract method for reseting a password
        /// </summary>
        /// 
        ///<param name="email">The email of the user account</param>
        ///<exception></exception>
        ///<returns>A bool signifying if the password was Reset</returns>
        bool ResetPassword(string email);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// An abstract method for finding a user account by their username
        /// </summary>
        /// 
        ///<param name="username">The user accounts username</param>
        ///<exception></exception>
        ///<returns>the user account</returns>
        UserAccount GetUserAccountByUsername(string username);

        /// <summary>
        /// William Clark
        /// Created: 2021/04/22
        /// 
        /// Selects a UserAccount from the data store with the matching Email
        /// </summary>
        /// 
        ///<param name="email">The email by which to select the UserAccount</param>
        ///<exception cref="ApplicationException">No UserAccount found</exception>
        ///<returns>The matching user account object</returns>
        UserAccount SelectUserAccountByEmail(string email);




        List<UserAccount> RetrieveUserAccounts();

    }
}
