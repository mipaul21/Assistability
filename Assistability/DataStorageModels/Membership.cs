/// <summary>
/// William Clark
/// Created: 2021/02/23
/// 
/// The Role object class
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
    public class Membership
    {
        public int GroupID { get; private set; }
        public int UserAccountID { get; private set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool Active { get; private set; }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Constructor for the creation of Membership Objects with set data fields
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public Membership(int groupID, int userAccountID)
        {
            GroupID = groupID;
            UserAccountID = userAccountID;
            CreationDate = DateTime.Now;
            Active = true;
        }

        public Membership(int groupID, int userAccountID, DateTime creationDate, DateTime? expirationDate, bool active) : this(groupID, userAccountID)
        {
            GroupID = groupID;
            UserAccountID = userAccountID;
            CreationDate = creationDate;
            ExpirationDate = expirationDate;
            Active = active;
        }
    }
}
