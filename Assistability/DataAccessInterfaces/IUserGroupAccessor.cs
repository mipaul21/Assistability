/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface for the UserGroupAccessor
/// </summary>
///
/// <remarks>
/// </remarks>
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IUserGroupAccessor
    {
        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Selects a list of owned UserGroup objects from the database with the matching UserAccountID field
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount for which to select all UserGroups to which the UserAccount is an owner</param>
        /// <exception>No UserGroups found</exception>
        /// <returns>A list of UserGroup objects</returns>
        List<UserGroup> SelectOwnedUserGroupsByUserAccountID(int userAccountID);

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
        List<MembershipVM> SelectAllMembershipsByUserAccountID(int userAccountID);

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
        List<Role> SelectAllMembershipRolesByMembership(Membership membership);

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
        List<UserAccount> SelectUserAccountsByUserGroup(UserGroup group);

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
        UserGroup SelectUserGroupByGroupID(int groupID);



        List<Role> SelectAllRoles();


        List<Role> SelectAllMembershipRoleNamesByMembership(Membership membership);

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
        bool AddNewUserToUserGroup(UserAccount userAccount, UserGroup userGroup, string roleName);

    }
}
