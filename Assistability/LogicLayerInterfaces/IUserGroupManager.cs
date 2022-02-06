/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface for management of UserGroup objects
/// </summary>
///
/// <remarks>
/// </remarks>
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IUserGroupManager
    {
        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Selects a List of UserGroups of which the UserAccount owns
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount for which to select all UserGroups of which the UserAccount owns</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>A List of UserGroup</returns>
        List<UserGroup> SelectOwnedUserGroupsByUserAccountID(int userAccountID);

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a List of UserAccounts of which are caregivers in the UserGroup
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="group">The UserGroup for which to select all UserAccounts that are caregivers</param>
        /// <param name="userManager">The IUserManager which will handle UserAccount management</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>A List of UserGroup</returns>
        BindingList<UserAccount> GetUserAccountsInUserGroupByRole(IUserManager userManager, UserGroup group, String roleName);

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
        UserGroup GetUserGroupByGroupID(int groupID);

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Selects a List of UserAccounts of which are members in the UserGroup
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="group">The UserGroup for which to select all UserAccounts that are members</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>A List of UserGroup</returns>
        List<UserAccount> GetUserAccountsByUserGroup(UserGroup group);

        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/02/25
        /// 
        /// Selects a List of All the Roles available to a user group.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <returns>A List of UserGroup Roles</returns>
        List<Role> RetrieveAllRoles();

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        /// 
        /// Creates a user with the provided role in the provided user group.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccount">The UserAccount to create</param>
        /// <param name="userGroup">The group to add this useraccount to</param>
        /// <param name="roleName">The name of the role to give the supplied user</param>
        /// <exception cref="ApplicationException">Invalid UserAccount</exception>
        /// <exception cref="ApplicationException">No UserGroup found</exception>
        /// <exception cref="ApplicationException">No Role found</exception>
        /// <returns>True if user created</returns>
        bool AddNewUserToUserGroup(UserAccount userAccount, UserGroup userGroup, string roleName);
    }
}
