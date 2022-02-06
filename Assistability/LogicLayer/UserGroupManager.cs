/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface for management of UserGroup objects
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserGroupManager : IUserGroupManager
    {
        private IUserGroupAccessor _userGroupAccessor;

        public UserGroupManager(IUserGroupAccessor userGroupAccessor)
        {
            _userGroupAccessor = userGroupAccessor;
        }

        public UserGroupManager()
        {
            _userGroupAccessor = new UserGroupAccessor(); ;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Selects a List of UserGroups of which the UserAccount has an active membership
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountID of the UserAccount for which to select all UserGroups of which the UserAccount has an active membership</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>A List of UserGroup</returns>
        public List<UserGroup> SelectOwnedUserGroupsByUserAccountID(int userAccountID)
        {
            List<UserGroup> result;
            try
            {
                result = _userGroupAccessor.SelectOwnedUserGroupsByUserAccountID(userAccountID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Groups found for this user" + ex.Message);
            }
            return result;
        }

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
        /// <returns>A List of UserAccount</returns>
        public List<UserAccount> GetUserAccountsByUserGroup(UserGroup group)
        {
            List<UserAccount> result;
            try
            {
                result = _userGroupAccessor.SelectUserAccountsByUserGroup(group);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Users found in this group" + ex.Message);
            }
            return result;
        }

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
        public BindingList<UserAccount> GetUserAccountsInUserGroupByRole(IUserManager userManager, UserGroup group, string roleName)
        {
            BindingList<UserAccount> result = new BindingList<UserAccount>();
            BindingList<UserAccount> allUsers;
            try
            {
                allUsers = new BindingList<UserAccount>(GetUserAccountsByUserGroup(group));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not get UserAccounts in Group" + ex.Message);
            }
            
            foreach (var user in allUsers)
            {
                try
                {
                    foreach (var membership in userManager.GetUserAccountVMByUserAccount(user).Memberships)
                    {
                        if (membership.GroupID == group.GroupID)
                        {
                            foreach (var role in membership.Roles)
                            {
                                if (role.Name == roleName)
                                {
                                    result.Add(user);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw new ApplicationException("Could not get get UserAccountVM" + ex.Message);
                }
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
        public UserGroup GetUserGroupByGroupID(int groupID)
        {
            UserGroup group = null;
            try
            {
                group = _userGroupAccessor.SelectUserGroupByGroupID(groupID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("User Group could not be found." + ex.Message);
            }
            return group;
        }


        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/03
        /// 
        /// Retrieves all the Group Available Roles
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <returns>List<Role></returns>
        public List<Role> RetrieveAllRoles()
        {
            List<Role> roles = null;

            try
            {
                roles = _userGroupAccessor.SelectAllRoles();

                if (roles == null)
                {
                    roles = new List<Role>();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return roles;
        }

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
        /// <exception cref="ApplicationException">No UserGroup found</exception>
        /// <exception cref="ApplicationException">No Role found</exception>
        /// <returns>True if user created</returns>
        public bool AddNewUserToUserGroup(UserAccount userAccount, UserGroup userGroup, string roleName)
        {
            bool result = false;
            try
            {
                result = _userGroupAccessor.AddNewUserToUserGroup(userAccount, userGroup, roleName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("User could not be created." + ex.Message);
            }
            return result;
        }
    }
}
