/// <summary>
/// William Clark
/// Created: 2021/04/03
/// 
/// Accessor for Membership Roles
/// </summary>
///
/// <remarks>
/// </remarks>

using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    public class MembershipRoleAccessor : IMembershipRoleAccessor
    {

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 4/03/2021
        /// Approver: 
        /// 
        /// Method that allows the Deletion of a membership Role
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        public int DeleteUserMembershipRoles(int groupID, int userAccountID, string roleName)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_remove_userrole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            cmd.Parameters.AddWithValue("@UserID", userAccountID);
            cmd.Parameters.AddWithValue("@RoleName", roleName);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 4/03/2021
        /// Approver: 
        /// 
        /// Method that allows the Insertion of a membership Role
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        public int InsertNewUserMembershipRoles(int groupID, int userAccountID, string roleName)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_membershipRole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            cmd.Parameters.AddWithValue("@UserID", userAccountID);
            cmd.Parameters.AddWithValue("@RoleName", roleName);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


    }
}
