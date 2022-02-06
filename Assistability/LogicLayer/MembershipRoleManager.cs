using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class MembershipRoleManager : IMembershipRoleManager
    {

        private IMembershipRoleAccessor _membershipRoleAccessor = null;

        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/03/31
        /// 
        /// Default constructor initializes an accessor
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public MembershipRoleManager()
        {
            //TODO :: _membershipRoleAccessor = new UserFakes();
            _membershipRoleAccessor = new MembershipRoleAccessor();
        }


        public MembershipRoleManager(IMembershipRoleAccessor roleAccessor)
        {
            _membershipRoleAccessor = roleAccessor;
        }



        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/04
        ///
        /// Allows the insertion of a membership role 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// ///
        /// <param name="userID">The userID of the user to be changed.</param>
        /// <param name="groupID">The group id that the user belongs to</param>
        /// <param name="roleName">The Role to be changed</param>
        public int AddUserMembershipRole(int userID, int groupID, string roleName)
        {
            int result = 0;
            try
            {
                result = _membershipRoleAccessor.InsertNewUserMembershipRoles(groupID, userID, roleName);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/04
        ///
        /// Allows the deletion of a membership role 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// ///
        /// <param name="userID">The userID of the user to be changed.</param>
        /// <param name="groupID">The group id that the user belongs to</param>
        /// <param name="roleName">The Role to be changed</param>
        public int DeleteUserMembershipRole(int userID, int groupID, string roleName)
        {
            int result = 0;
            try
            {
                result = _membershipRoleAccessor.DeleteUserMembershipRoles(groupID, userID, roleName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
