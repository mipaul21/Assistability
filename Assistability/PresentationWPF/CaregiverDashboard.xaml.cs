/// <summary>
/// William Clark
/// Created: 2021/05/06
/// 
/// Interface to be used by caregivers users to view groups
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessLayer;
using DataStorageModels;
using DataViewModels;
using LogicLayer;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for CaregiverDashboard.xaml
    /// </summary>
    public partial class CaregiverDashboard : Page
    {
        private IUserGroupManager _userGroupManager;
        private IUserManager _userManager;
        private UserAccountVM _user;

        /// <summary>
        /// William Clark
        /// Created: 2021/05/06
        /// 
        /// Constructs a CaregiverDashboard
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="user">The UserAccountVM for which to display this dashboard</param>
        public CaregiverDashboard(UserAccountVM user)
        {
            _user = user;
            _userGroupManager = new UserGroupManager(new UserGroupAccessor());
            _userManager = new UserManager(new UserAccessor());


            InitializeComponent();

            // Instantiates a new group member list page with the groups of which the user is a member
            try
            {
                List<UserGroup> userGroups = new List<UserGroup>();
                foreach (var membership in _user.Memberships)
                {
                    userGroups.Add(_userGroupManager.GetUserGroupByGroupID(membership.GroupID));
                }
                frmGroupMemberList.Navigate(new GroupMemberList(userGroups, _userGroupManager, _userManager, _user, "Caregiver"));
            }
            catch (Exception)
            {
                MessageBox.Show("The Groups you belong to could not be found.");
            }

        }
    }
}
