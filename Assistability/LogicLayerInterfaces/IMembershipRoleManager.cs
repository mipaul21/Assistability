using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IMembershipRoleManager
    {

        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/03
        /// 
        /// Adds a user membership role. 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userID">The userID of the user to be changed.</param>
        /// <param name="groupID">The group id that the user belongs to</param>
        /// <param name="roleName">The Role to be changed</param>

        int AddUserMembershipRole(int userID, int groupID, string roleName);

        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/03
        /// 
        /// Deletes a user membership role. 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userID">The userID of the user to be changed.</param>
        /// <param name="groupID">The group id that the user belongs to</param>
        /// <param name="roleName">The Role to be changed</param>
        int DeleteUserMembershipRole(int userID, int groupID, string roleName);

    }
}
