/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface to be used by Clients
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessFakes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for ClientDashboard.xaml
    /// </summary>
    public partial class ClientDashboard : Page
    {
        private IUserGroupManager _userGroupManager;
        private IUserManager _userManager;
        private UserAccountVM _user;
        public ClientDashboard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Constructs a ClientDashboard
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="user">The UserAccount for which to display this dashboard</param>
        public ClientDashboard(UserAccountVM user)
        {
            _user = user;
            _userGroupManager = new UserGroupManager(new UserGroupFakes());
            _userManager = new UserManager(new UserFakes());


            InitializeComponent();

            // Instantiates a new group member list page with the groups of which the user is a member
            try
            {
                List<UserGroup> userGroups = new List<UserGroup>();
                foreach (var membership in _user.Memberships)
                {
                    userGroups.Add(_userGroupManager.GetUserGroupByGroupID(membership.GroupID));
                }
                frmGroupMemberList.Navigate(new GroupMemberList(userGroups, _userGroupManager, _userManager, _user, "Client"));
                
            }
            catch (Exception)
            {
                MessageBox.Show("The Groups you belong to could not be found.");
            }
        }
    }
}
