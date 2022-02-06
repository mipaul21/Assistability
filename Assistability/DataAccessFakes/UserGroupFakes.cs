/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Fake UserGroupAccessor for testing
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class UserGroupFakes : IUserGroupAccessor
    {
        private UserGroup fakeUserGroup = new UserGroup(1, 1);
        private Membership fakeMembership = new Membership(1, 1);
        private Role fakeRole1 = new Role(1, "Admin", "User Administrator");
        private Role fakeRole2 = new Role(2, "Caregiver", "Caregiver User");
        private Role fakeRole3 = new Role(3, "Client", "Client User");
        private List<MembershipVM> fakeMemberships;
        private List<Role> fakeRoles;
        public UserGroupFakes()
        {
            fakeRoles = new List<Role>()
            {
                fakeRole1, fakeRole2, fakeRole3
            };
            fakeMemberships = new List<MembershipVM>()
            {
                new MembershipVM(fakeMembership, fakeRoles)
            };
        }

        public bool AddNewUserToUserGroup(UserAccount userAccount, UserGroup userGroup, string roleName)
        {
            
            bool result = false;
            if (SelectUserAccountsByUserGroup(fakeUserGroup).Count(account => account.UserAccountID == userAccount.UserAccountID) == 0)
            {

                if (userGroup.GroupID == fakeUserGroup.GroupID && userGroup.UserID_Owner == fakeUserGroup.UserID_Owner)
                {
                    var role = fakeRoles.Where(r => r.Name == roleName).ToList();
                    if (role.Count > 0)
                    {
                        Membership membership = new Membership(userGroup.GroupID, userAccount.UserAccountID);
                        fakeMemberships.Add(new MembershipVM(membership, role));
                        result = true;
                    }
                    else
                    {
                        // Invalid Role
                        throw new ApplicationException("Invalid Role");
                    }
                }
                else
                {
                    // Group not found
                    throw new ApplicationException("Group could not be found");
                }
            }
            else
            {
                throw new ApplicationException("User already exists");
            }
            return result;
        }

        public List<Role> SelectAllMembershipRoleNamesByMembership(Membership membership)
        {
            throw new NotImplementedException();
        }

        public List<Role> SelectAllMembershipRolesByMembership(Membership membership)
        {
            return fakeRoles;
        }

        public List<MembershipVM> SelectAllMembershipsByUserAccountID(int userAccountID)
        {
            return fakeMemberships;
        }

        public List<Role> SelectAllRoles()
        {
            throw new NotImplementedException();
        }

        public List<UserGroup> SelectOwnedUserGroupsByUserAccountID(int userAccountID)
        {
            if (userAccountID == 1)
            {
                return new List<UserGroup> { fakeUserGroup };
            }
            else
            {
                throw new ApplicationException("UserAccount not found");
            }
            
        }


        public List<UserAccount> SelectUserAccountsByUserGroup(UserGroup group)
        {
            if (group.GroupID == 1)
            {
                return new List<UserAccount>()
                {
                    new UserAccount(1, "First", "Administrator", "firstAdmin", "first@administrator.com", true),
                    new UserAccount(2, "Care", "Giver", "Caregiver", "caregiver@Assisstability.com", true),
                    new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true)
                };
            }
            else
            {
                throw new ApplicationException("Group not found");
            }
            
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
            if (groupID == 1)
            {
                return new UserGroup(1, 1);
            }
            else
            {
                throw new ApplicationException("Group not found");
            }
        }
    }
}
