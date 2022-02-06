/// <summary>
/// William Clark
/// Created: 2021/02/04
/// 
/// The UserAccount object class
/// </summary>
///
/// <remarks>
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
    public class UserAccount
    {
        public int UserAccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/04
        /// 
        /// Constructor for the creation of UserAccount Objects with set data fields
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public UserAccount(int userAccountID, string firstName, string lastName, string userName, string email, bool active)
        {
            UserAccountID = userAccountID;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Active = active;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/04
        /// 
        /// Constructor for the creation of UserAccount Objects with set data fields
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public UserAccount()
        {
            UserAccountID = 0;
            FirstName = "";
            LastName = "";
            UserName = "";
            Email = "";
            Active = false;
        }
    }
}
