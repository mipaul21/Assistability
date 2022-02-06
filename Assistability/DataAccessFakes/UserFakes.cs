/// <summary>
/// Nathaniel Webber
/// Created: 2021/02/05
/// 
/// This class is the 'fakes' class that I use
/// to test my code
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
/// Added InsertNewUserAccount fakes
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
/// Updated: 2021/03/06
/// Added ResetPassword fakes
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;

namespace DataAccessFakes
{
    /// <summary>
    /// Nathaniel Webber
    /// Created: 2021/02/05
    /// 
    /// This class creates a 'fake' account to be used
    /// in the UserObjectTest class
    /// </summary>
    ///
    /// <remarks>
    /// Nathaniel Webber
    /// Updated: 2021/02/18 
    /// First MVP delivered
    /// </remarks>
    public class UserFakes : IUserAccessor
    {
        private UserAccount fakeAdmin = new UserAccount(1, "First", "Administrator", "firstAdmin", "first@administrator.com", true);
        
        private List<MembershipVM> fakeAdminMemberships = new List<MembershipVM>()
        {
            new MembershipVM(1, 1, new List<Role>()
            {
                new Role(1, "Admin", "User Administrator")
            })

        };

        private UserAccount fakeCaregiver = new UserAccount(2, "Care", "Giver", "Caregiver", "caregiver@Assisstability.com", true);

        private List<MembershipVM> fakeCaregiverMemberships = new List<MembershipVM>()
        {
            new MembershipVM(1, 1, new List<Role>()
            {
                new Role(1, "Caregiver", "Caregiver User")
            })

        };

        private UserAccount fakeClient = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);

        private List<MembershipVM> fakeClientMemberships = new List<MembershipVM>()
        {
            new MembershipVM(1, 1, new List<Role>()
            {
                new Role(1, "Client", "Client User")
            })

        };

        private List<UserAccount> fakeUserList = new List<UserAccount>();

        public UserFakes()
        {
            fakeUserList.Add(fakeAdmin);
            fakeUserList.Add(fakeCaregiver);
            fakeUserList.Add(fakeClient);
        }



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
        public UserAccount SelectUserAccountByUserAccountID(int userAccountID)
        {
            if (userAccountID == 1)
            {
                return fakeAdmin;
            }
            else
            {
                throw new ApplicationException("User not found");
            }
            
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/04
        /// 
        /// Deletes a UserAccount from the database
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount to be deleted</param>
        /// <returns>1, indicating a UserAccount has been successfully deleted</returns>
        public int DeleteUserAccountByUserAccountID(int userAccountID)
        {
            if (userAccountID == fakeAdmin.UserAccountID)
            {
                return 1;
            }
            return 0;
        }


        public UserAccount SelectUserByUserName(string userName)
        {
            UserAccount result;
            result = fakeUserList.Find( u => u.UserName == userName);
            if (result == null)
            {
                throw new ApplicationException("User not found");
            }
            return result;
        }

        public int VerifyUserNameAndPassword(string userName, string passwordHash)
        {
            return passwordHash.ToUpper().Equals("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E") ? 1 : 0;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a UserAccountVM from the database from a UserAccount
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="user">The UserAccount of the UserAccountVM to be selected</param>
        /// <exception>No UserAccountVM found</exception>
        /// <returns>A UserAccountVM object</returns>
        public UserAccountVM SelectUserAccountVMByUserAccount(UserAccount user)
        {
            switch (user.UserAccountID)
            {
                case 1:
                    return new UserAccountVM(fakeAdmin, fakeAdminMemberships);
                case 2:
                    return new UserAccountVM(fakeCaregiver, fakeCaregiverMemberships);
                case 3:
                    return new UserAccountVM(fakeClient, fakeClientMemberships);
                default:
                    throw new ApplicationException("User not found");
            }
            
        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// 
        /// a fake data access method for testing UpdateUserAccount
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        public int UpdateUserAccount(UserAccount oldUserAccount, UserAccount newUserAccount)
        {
            int rows = 1;
            return rows;
        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// 
        /// a fake data access method for testing DeactivateUserAccount
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        public int DeactivateUserAccount(int userAccountID)
        {
            int rows = 1;
            return rows;
        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// 
        /// a fake data access method for testing Reactivate User Account.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        public int ReactivateUserAccount(int userAccountID)
        {
            int rows = 1;
            return rows;
        }
		
		public List<UserAccount> userAccounts = new List<UserAccount>();
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// a fake method for testing perposes.
        /// </summary>
        ///<param name="newUserAccount">
        ///The user account the 
        ///user has created
        ///</param>
        ///<param name="password">The users password</param>
        ///<exception></exception>
        ///<returns>the users userID</returns>
        public int InsertNewUserAccount(UserAccount
            newUserAccount, string password)
        {
            int startingAmount = userAccounts.Count;
            if (newUserAccount.FirstName != "")
            {
                newUserAccount.UserAccountID = userAccounts.Count + 1;
                userAccounts.Add(newUserAccount);
            }
            return newUserAccount.UserAccountID;
		}

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// 
        /// a fake data access method for testing delete User Account
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>

        public int DeleteUserAccount(int userAccountID)
        {
            int rows = 1;
            return rows;
        }

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
        public bool LogoutUser(UserAccount user)
        {
            UserAccount userToLogout = user;

            userToLogout.UserAccountID = 0;
            userToLogout.FirstName = "";
            userToLogout.LastName = "";
            userToLogout.UserName = "";
            userToLogout.Email = "";
            userToLogout.Active = false;

            if (user.UserAccountID.Equals(0))
            {
                return true;
            }

            return false;
        }
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// a fake method for testing updating a password.
        /// </summary>
        ///<param name="email">
        ///The user accounts email
        ///</param>
        ///<param name="oldPassword">The users original password</param>
        ///<param name="newPassword">The users new password</param>
        ///<exception></exception>
        ///<returns>the users userID</returns>
        public int ResetPassword(string email)
        {
            int answer = 0;

            List<string> passwords = new List<string>();
            passwords.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            passwords.Add("5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8");
            List<string> emails = new List<string>();
            emails.Add("jjj@man.com");
            emails.Add("testa@test.com");

            int space = emails.IndexOf(email);
            string newPassword = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            string lPassword = "";

            if (space >= 0) 
            {
                
                lPassword = passwords[space];
                passwords[space] = newPassword;
            }
            if (passwords.Contains(newPassword) && !passwords.Contains(lPassword)) 
            {
                answer = 1;
            }

            return answer;
        }

        public int InsertSubsidiaryUserAccount(UserAccount newUserAccount, string password)
        {
            if (1 != fakeUserList.Count(account => account.Email == newUserAccount.Email))
            {
                newUserAccount.UserAccountID = fakeUserList.Count + 1;
                fakeUserList.Add(newUserAccount);
                return newUserAccount.UserAccountID;
            }
            else
            {
                throw new ApplicationException();
            }
            
        }

        public UserAccount SelectUserAccountByEmail(string email)
        {
            if (email.Equals(fakeAdmin.Email))
            {
                return fakeAdmin;
            }
            else if (email.Equals(fakeCaregiver.Email))
            {
                return fakeCaregiver;
            }
            else if (email.Equals(fakeClient.Email))
            {
                return fakeCaregiver;
            }
            else
            {
                throw new ApplicationException("User not found");
            }
        }

        public List<UserAccount> SelectAllUsers()
        {
            return userAccounts;
        }
    }
}
