/// <summary>
/// William Clark
/// Created: 2021/02/23
/// 
/// The UserGroup object class
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
    public class UserGroup
    {
        public int UserID_Owner { get; set; }
        public int GroupID { get; set; }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Constructor for the creation of UserGroup Objects with set data fields
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public UserGroup(int userID_Owner, int groupID)
        {
            UserID_Owner = userID_Owner;
            GroupID = groupID;
        }
    }
}
