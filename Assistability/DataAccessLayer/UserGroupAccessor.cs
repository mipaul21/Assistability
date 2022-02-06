/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Accessor for UserGroup objects
/// </summary>
///
/// <remarks>
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
    public class UserGroupAccessor : IUserGroupAccessor
    {
        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Selects a list of UserGroup objects from the database with the matching UserAccountID field
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount for which to select all UserGroups of which the UserAccount has an active membership</param>
        /// <exception>No UserGroups found</exception>
        /// <returns>A list of UserGroup objects</returns>
        public List<UserGroup> SelectOwnedUserGroupsByUserAccountID(int userAccountID)
        {
            List<UserGroup> userGroups = new List<UserGroup>();
            
            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_usergroups_by_useraccountid", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@UserAccountID", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserAccountID"].Value = userAccountID;

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
                        var userGroupID = reader.GetInt32(0);

                        var userGroup = new UserGroup(userAccountID, userGroupID);

                        userGroups.Add(userGroup);
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
            return userGroups;
        }


        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a list of MembershipVM objects from the database with the matching UserAccountID field
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount for which to select all MembershipVMs</param>
        /// <exception>No Memberships found</exception>
        /// <returns>A list of MembershipVM objects</returns>
        public List<MembershipVM> SelectAllMembershipsByUserAccountID(int userAccountID)
        {
            List<MembershipVM> membershipVMs = new List<MembershipVM>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_memberships_by_useraccountid", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@UserAccountID", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@UserAccountID"].Value = userAccountID;

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
                        
                        var userGroupID = reader.GetInt32(0);
                        var creationDate = reader.GetDateTime(1);
                        DateTime? expirationDate;
                        if (!reader.IsDBNull(2))
                        {
                            expirationDate = reader.GetDateTime(2);
                        }
                        else
                        {
                            expirationDate = null;
                        }
                        var active = reader.GetBoolean(3);

                        var membership = new Membership(userGroupID, userAccountID, creationDate, expirationDate, active);
                        var roles = SelectAllMembershipRolesByMembership(membership);

                        var membershipVM = new MembershipVM(membership, roles);

                        membershipVMs.Add(membershipVM);

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

            return membershipVMs;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a list of Role objects from the database with the matching Membership
        /// This is all of the roles for which a given UserAccount has within a particular UserGroup
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="membership">The Membership for which to select all MembershipRoles</param>
        /// <exception>No Roles found</exception>
        /// <returns>A list of Role objects</returns>
        public List<Role> SelectAllMembershipRolesByMembership(Membership membership)
        {
            List<Role> roles = new List<Role>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_membershiproles_by_membership", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);

            // Add parameter to command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@GroupID"].Value = membership.GroupID;

            // Set parameter to value
            cmd.Parameters["@UserID"].Value = membership.UserAccountID;

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
                        var roleID = reader.GetInt32(0);
                        var roleName = reader.GetString(1);
                        var roleDescription = reader.GetString(2);

                        var role = new Role(roleID, roleName, roleDescription);

                        roles.Add(role);

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

            return roles;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a list of UserAccount objects from the database which are members of a UserGroup
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="group">The UserGroup for which to select all UserAccounts</param>
        /// <exception>No UserAccounts found</exception>
        /// <returns>A list of UserAccount objects</returns>
        public List<UserAccount> SelectUserAccountsByUserGroup(UserGroup group)
        {
            List<UserAccount> result = new List<UserAccount>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_memberships_by_groupid", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@GroupID"].Value = group.GroupID;

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
                        var userID = reader.GetInt32(0);
                        var creationDate = reader.GetDateTime(1);
                        DateTime? expirationDate;
                        if (!reader.IsDBNull(2))
                        {
                            expirationDate = reader.GetDateTime(2);
                        }
                        else
                        {
                            expirationDate = null;
                        }
                        var active = reader.GetBoolean(3);

                        if (expirationDate.HasValue)
                        {
                            var expirationDateComparison = (DateTime)expirationDate;
                            if (DateTime.Compare(expirationDateComparison, DateTime.Now) > 0)
                            {
                                if (active)
                                {
                                    IUserAccessor userAccessor = new UserAccessor();
                                    var user = userAccessor.SelectUserAccountByUserAccountID(userID);

                                    result.Add(user);
                                }
                            }
                        }
                        else
                        {
                            IUserAccessor userAccessor = new UserAccessor();
                            var user = userAccessor.SelectUserAccountByUserAccountID(userID);

                            result.Add(user);
                        }

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

            return result;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Selects a UserGroup by it's unique identifier
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="groupID">The UserGroupID for which to select the UserGroup</param>
        /// <exception cref="ApplicationException">No UserGroup found</exception>
        /// <returns>A UserGroup</returns>
        public UserGroup SelectUserGroupByGroupID(int groupID)
        {
            UserGroup userGroup = null;

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_usergroups_by_groupid", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to command
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);

            // Set parameter to value
            cmd.Parameters["@GroupID"].Value = groupID;

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
                        var userIDOwner = reader.GetInt32(0);

                        userGroup = new UserGroup(userIDOwner, groupID);
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
            return userGroup;
        }

        public List<Role> SelectAllRoles()
        {
            List<Role> roles = new List<Role>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_all_roles", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;


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
                        var roleID = reader.GetInt32(0);
                        var roleName = reader.GetString(1);
                        var roleDescription = reader.GetString(2);

                        var role = new Role(roleID, roleName, roleDescription);

                        roles.Add(role);

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

            return roles;
        }

        public List<Role> SelectAllMembershipRoleNamesByMembership(Membership membership)
        {
            List<Role> roles = new List<Role>();

            // Retrieve a connection from factory
            var conn = DBConnection.GetDBConnection();

            // Retrieve a command
            var cmd = new SqlCommand("sp_select_membershipnames_by_membership", conn);

            // Set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // Add parameter to command

            // Add parameter to command
            cmd.Parameters["@GroupID"].Value = membership.GroupID;

            // Set parameter to value
            cmd.Parameters["@UserID"].Value = membership.UserAccountID;


            // Set parameter to value

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

                        var roleName = reader.GetString(0);
                        var roleDescription = reader.GetString(1);
                        var roleID = 0;
                        var role = new Role(roleID, roleName, roleDescription);
                        roles.Add(role);

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

            return roles;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        /// 
        /// Stores a user with the provided role in the provided user group.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccount">The UserAccount to create</param>
        /// <param name="userGroup">The group to add this useraccount to</param>
        /// <param name="roleName">The name of the role to give the supplied user</param>
        /// <exception cref="ApplicationException">No UserGroup found</exception>
        /// <exception cref="ApplicationException">No Role found</exception>
        /// <returns>True if user created</returns>
        public bool AddNewUserToUserGroup(UserAccount userAccount, UserGroup userGroup, string roleName)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("create_membership", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", userGroup.GroupID);
            cmd.Parameters.AddWithValue("@UserID", userAccount.UserAccountID);
            cmd.Parameters.AddWithValue("@Active", true);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() > 0;
                if (result)
                {
                    var second = new SqlCommand("sp_insert_membershipRole", conn);
                    second.CommandType = CommandType.StoredProcedure;
                    second.Parameters.AddWithValue("@GroupID", userGroup.GroupID);
                    second.Parameters.AddWithValue("@UserID", userAccount.UserAccountID);
                    second.Parameters.AddWithValue("@RoleName", roleName);
                    try
                    {
                        result = second.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
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

            return result;
        }
    }
}
