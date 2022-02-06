/// <summary>
/// Nathaniel Webber
/// Created: 2021/02/05
/// 
/// This is an interface for data access of the users
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
/// Added InsertNewUserAccount
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/25
/// Added ResetPassword
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorageModels;
using DataViewModels;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Nathaniel Webber
    /// Created: 2021/02/05
    /// 
    /// This interface has the methods that will be
    /// used in the UserAccessor class
    /// </summary>
    ///
    /// <remarks>
    /// Nathaniel Webber
    /// Updated: 2021/02/18 
    /// First MVP delivered
    /// </remarks>
    public interface IUserAccessor
    {
        UserAccount SelectUserByUserName(string userName);
        int VerifyUserNameAndPassword(string userName, string passwordHash);

        /// <summary>
        /// William Clark
        /// Created: 2021/02/18
        /// 
        /// Selects a UserAccount from the database with the matching UserAccountID field
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount to be selected</param>
        /// <exception>No UserAccount found</exception>
        /// <returns>A UserAccount object</returns>
        UserAccount SelectUserAccountByUserAccountID(int userAccountID);

        
        UserAccountVM SelectUserAccountVMByUserAccount(UserAccount user);
        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 4/30/2021
        /// Approver: 
        /// 
        /// a data access method for selecting all of the user accounts
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        List<UserAccount> SelectAllUsers();


        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/03/2021
        /// Approver: 
        /// 
        /// a data access method for updating an account
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        int UpdateUserAccount(UserAccount oldUserAccount, UserAccount newUserAccount);

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/23/2021
        /// Approver: 
        /// 
        /// a data access method for deactivating an account
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        /// 
        int DeactivateUserAccount(int userAccountID);

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/23/2021
        /// Approver: 
        /// 
        /// a data access method for reactivating an account
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        /// 
        int ReactivateUserAccount(int userAccountID);
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// an abstract method to support UserAccountAccessor in 
        /// creating a new user account.
        /// </summary>
        /// 
        ///<param name="newUserAccount">The newly crated account</param>
        ///<param name="password">the accounts password</param>
        ///<exception></exception>
        ///<returns>int userID</returns>
        int InsertNewUserAccount(UserAccount newUserAccount, string password);

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        ///
        /// Stores a subsidiary user account without their own group
        /// </summary>
        ///
        ///<param name="newUserAccount">The newly created user account</param>
        ///<param name="password">The pasword for that user account</param>
        ///<exception cref="ApplicationException">Could not create UserAccount/exception>
        ///<returns>UserAccountID of created user</returns>
        int InsertSubsidiaryUserAccount(UserAccount newUserAccount, string password);

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/23/2021
        /// Approver: 
        /// 
        /// a data access method for safely deleting a UserAccount with no roles
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        /// 
        int DeleteUserAccount(int userAccountID);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/25
        /// an abstract method to support UserAccountAccessor in 
        /// reset a password.        
        /// </summary>
        /// 
        ///<param name="email">The user accounts email</param>
        ///<exception></exception>
        ///<returns>int to tell if the password was reset</returns>
        int ResetPassword(string email);

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
    }
}
