/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// The MembershipVM object class
/// </summary>
///
/// <remarks>
/// </remarks>
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModels 
{
    public class MembershipVM : Membership
    {
        // A list of a UserAccounts roles 
        public List<Role> Roles;
        public MembershipVM(int groupID, int userAccountID, List<Role> roles) : base(groupID, userAccountID)
        {
            Roles = roles;
        }

        public MembershipVM(Membership membership, List<Role> roles) : base(membership.GroupID, membership.UserAccountID)
        {
            Roles = roles;
        }
    }
}
