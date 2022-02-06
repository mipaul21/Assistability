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
    public class Role
    {
        public int? RoleID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Constructor for the creation of Role Objects with set data fields
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public Role(int? roleID, string name, string description)
        {
            RoleID = roleID;
            Name = name;
            Description = description;
        }

        public Role()
        {

        }
    }
}
