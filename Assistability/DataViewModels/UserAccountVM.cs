/// <summary>
/// William Clark
/// Created: 2021/02/23
/// 
/// The UserAccountVM object class
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
    public class UserAccountVM : UserAccount
    {
        public List<MembershipVM> Memberships;

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Constructor for the creation of UserAccountVM Objects with set data fields
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public UserAccountVM(int userAccountID, string firstName, string lastName, string userName, string email, bool active, List<MembershipVM> memberships) : base(userAccountID, firstName, lastName, userName, email, active)
        {
            Memberships = memberships;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Constructor for the creation of UserAccountVM Objects accepting an existing UserAccount object
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public UserAccountVM(UserAccount userAccount, List<MembershipVM> memberships) : base(userAccount.UserAccountID, userAccount.FirstName, userAccount.LastName, userAccount.UserName, userAccount.Email, userAccount.Active)
        {
            Memberships = memberships;
        }
    }
}
