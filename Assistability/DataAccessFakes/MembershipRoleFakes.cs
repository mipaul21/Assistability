using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorageModels;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class MembershipRoleFakes : IMembershipRoleAccessor
    {
        private List<Role> fakeRoles = new List<Role>();
        private List<Role> oldRoles = new List<Role>();



        public MembershipRoleFakes()
        {
            Role fakeRoles = new Role(1, "Admin", "User Administrator");

        }

        public int DeleteUserMembershipRoles(int groupID, int userAccountID, string roleName)
        {
            Role fakeRole = new Role(1, "Admin", "User Administration");
            if (groupID == 1 && userAccountID == 1)
            {
                fakeRoles.Remove(fakeRole);
                return 1;
            }

            return 0;
        }

        public int InsertNewUserMembershipRoles(int groupID, int userAccountID, string roleName)
        {

            if (groupID == 1 && userAccountID == 1)
            {
                oldRoles = fakeRoles;
                return 1;
            }

            return 0;
        }
    }
}
