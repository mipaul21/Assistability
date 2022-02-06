/// <summary>
/// Nathaniel Webber
/// Created: 2021/02/05
/// 
/// This class accesses the data through
/// the DBConnection class
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

using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/02/05
        /// 
        /// This method grabs the corresponding stored procedure from
        /// the sql to select a particular user by their username
        /// </summary>
        ///
        /// <remarks>
        /// Nathaniel Webber
        /// Updated: 2021/02/18 
        /// First MVP delivered
        /// </remarks>
        public UserAccount SelectUserByUserName(string userName)
        {
            UserAccount user = null;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_user_by_username", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@UserName"].Value = userName;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var userID = reader.GetInt32(0);
                        var firstName = reader.GetString(1);
                        var lastName = reader.GetString(2);
                        var username = reader.GetString(3);
                        var email = reader.GetString(4);
                        var active = reader.GetBoolean(5);

                        user = new UserAccount(userID, firstName, lastName, username, email, active);
                    }
                    reader.Close();
                }
                else
                {
                    throw new ApplicationException("User not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/02/05
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to help verfiy a login
        /// </summary>
        ///
        /// <remarks>
        /// Nathaniel Webber
        /// Updated: 2021/02/18 
        /// First MVP delivered
        /// </remarks>
        public int VerifyUserNameAndPassword(string userName, string passwordHash)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_authenticate_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@UserName"].Value = userName;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
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
            UserAccount userAccount = null;
            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_useraccount_by_useraccountid", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserID"].Value = userAccountID;

            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
                var reader = cmd.ExecuteReader();

                // Capture results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var firstName = reader.GetString(0);
                        var lastName = reader.GetString(1);
                        var userName = reader.GetString(2);
                        var emailResult = reader.GetString(3);
                        var active = reader.GetBoolean(4);

                        userAccount = new UserAccount(userAccountID, firstName, lastName, userName, emailResult, active);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return userAccount;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a UserAccountVM from the database with the matching UserAccountID field
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount to be selected</param>
        /// <exception>No UserAccount found</exception>
        /// <returns>A UserAccountVM object</returns>
        public UserAccountVM SelectUserAccountVMByUserAccount(UserAccount user)
        {
            var userGroupAccessor = new UserGroupAccessor();
            UserAccountVM userAccountVM = new UserAccountVM(user, userGroupAccessor.SelectAllMembershipsByUserAccountID(user.UserAccountID));
            return userAccountVM;
        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/03/2021
        /// Approver: 
        /// 
        /// Method that allows the update of a UserAccount database item through the use of a Stored Procedure.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        public int UpdateUserAccount(UserAccount oldUserAccount, UserAccount newUserAccount)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_UserAccount", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters.Add("@OldFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldUserName", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@NewFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewUserName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 250);

            cmd.Parameters["@UserID"].Value = oldUserAccount.UserAccountID;
            cmd.Parameters["@OldFirstName"].Value = oldUserAccount.FirstName;
            cmd.Parameters["@OldLastName"].Value = oldUserAccount.LastName;
            cmd.Parameters["@OldUserName"].Value = oldUserAccount.UserName;
            cmd.Parameters["@OldEmail"].Value = oldUserAccount.Email;

            cmd.Parameters["@NewFirstName"].Value = newUserAccount.FirstName;
            cmd.Parameters["@NewLastName"].Value = newUserAccount.LastName;
            cmd.Parameters["@NewUserName"].Value = newUserAccount.UserName;
            cmd.Parameters["@NewEmail"].Value = newUserAccount.Email;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;

        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/23/2021
        /// Approver: 
        ///
        /// Method that allows the Deactivation of a UserAccount database item through the use of a Stored Procedure.
        /// While also disallowing the admin from deactiving itself, without making another user admin first.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        public int DeactivateUserAccount(int userAccountID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_deactivate_UserAccount", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userAccountID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/23/2021
        /// Approver: 
        ///
        /// Method that allows the Reactivation of a UserAccount database item through the use of a Stored Procedure.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        public int ReactivateUserAccount(int userAccountID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_reactivate_UserAccount", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserAccountID", userAccountID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/05
        /// 
        /// A method used to create a new user account to be 
        /// stored within the data base.
        /// </summary>
        /// 
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception></exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        public int InsertNewUserAccount(UserAccount newUserAccount, string password)
        {
            int userID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = newUserAccount.Email;
            cmd.Parameters["@FirstName"].Value = newUserAccount.FirstName;
            cmd.Parameters["@LastName"].Value = newUserAccount.LastName;
            cmd.Parameters["@UserName"].Value = newUserAccount.UserName;
            cmd.Parameters["@PasswordHash"].Value = password;

            try
            {
                conn.Open();
                userID = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return userID;
        }
		
        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/10/2021
        /// Approver: 
        ///
        /// Method that allows the deletion of a UserAccount database item through the use of a Stored Procedure.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>

        public int DeleteUserAccount(int userAccountID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_delete_UserAccount", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userAccountID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/25
        /// 
        /// A method used to reset a password.
        /// </summary>
        /// 
        ///<param name="email">The email of the user account</param>
        ///<exception></exception>
        ///<returns>A int of rows affected</returns>
        public int ResetPassword(string email)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reset_passwordhash", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

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
        ///<returns>True if the account was successfully created</returns>
        public int InsertSubsidiaryUserAccount(UserAccount newUserAccount, string password)
        {
            int userID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_user_non_admin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = newUserAccount.Email;
            cmd.Parameters["@FirstName"].Value = newUserAccount.FirstName;
            cmd.Parameters["@LastName"].Value = newUserAccount.LastName;
            cmd.Parameters["@UserName"].Value = newUserAccount.UserName;
            cmd.Parameters["@PasswordHash"].Value = password;

            try
            {
                conn.Open();
                userID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return userID;
        }

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
        public UserAccount SelectUserAccountByEmail(string email)
        {
            UserAccount userAccount = null;
            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_useraccount_by_email", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // Set parameter to value
            cmd.Parameters["@Email"].Value = email;

            // Execute command
            try
            {
                // Open connection
                conn.Open();

                // Execute command
                var reader = cmd.ExecuteReader();

                // Capture results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var userAccountID = reader.GetInt32(0);
                        var firstName = reader.GetString(1);
                        var lastName = reader.GetString(2);
                        var userName = reader.GetString(3);
                        var active = reader.GetBoolean(4);

                        userAccount = new UserAccount(userAccountID, firstName, lastName, userName, email, active);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return userAccount;
        }


        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/30
        /// Places all of the users into a list.
        /// </summary>
        ///

        /// </remarks>
        public List<UserAccount> SelectAllUsers()
        {
            List<UserAccount> users = new List<UserAccount>();

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_select_UserAccounts", conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new UserAccount
                        {
                            UserAccountID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            UserName = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                        };
                        users.Add(user);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;
        }
    }
}
