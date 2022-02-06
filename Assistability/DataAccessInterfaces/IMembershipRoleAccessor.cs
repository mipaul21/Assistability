using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorageModels;

namespace DataAccessInterfaces
{
    public interface IMembershipRoleAccessor
    {
        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 4/03/2021
        /// Approver: 
        /// 
        /// a data access method for inserting User Membership Roles
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        int InsertNewUserMembershipRoles(int groupID, int userAccountID, string roleName);
        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 4/03/2021
        /// Approver: 
        /// 
        /// a data access method for deleting User Membership Roles
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        int DeleteUserMembershipRoles(int groupID, int userAccountID, string roleName);




    }
}
