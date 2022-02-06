/// <summary>
/// William Clark
/// Created: 2021/03/25
/// 
/// The Invitation object class
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

    public class Invitation
    {
        public string InvitationTypeName { get; set; }
        public int UserID_Admin { get; set; }
        public int UserID_Invitee { get; set; }
        public int GroupID { get; set; }
        public int RoleID { get; set; }
        public DateTime InvitationSentDate { get; set; }
        public DateTime? InvitationExpirationDate { get; set; }

        public Invitation(string invitationTypeName, int userID_Admin, int userID_Invitee, int groupID, int roleID, DateTime invitationSentDate)
        {
            InvitationTypeName = invitationTypeName;
            UserID_Admin = userID_Admin;
            UserID_Invitee = userID_Invitee;
            GroupID = groupID;
            RoleID = roleID;
            InvitationSentDate = invitationSentDate;
        }
    }
}
