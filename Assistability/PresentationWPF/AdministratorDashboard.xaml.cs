/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface to be used by administrator users to view groups
/// </summary>
///
/// <remarks>
/// </remarks>
using DataViewModels;
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
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessFakes;
using DataAccessLayer;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for AdministratorDashboard.xaml
    /// </summary>
    public partial class AdministratorDashboard : Page
    {
        private IUserGroupManager _userGroupManager;
        private IUserManager _userManager;
        private UserAccountVM _user;

        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Constructs an AdministratorDashboard
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="user">The UserAccountVM for which to display this dashboard</param>
        public AdministratorDashboard(UserAccountVM user)
        {
            _user = user;
            _userGroupManager = new UserGroupManager(new UserGroupAccessor());
            _userManager = new UserManager(new UserAccessor());


            InitializeComponent();

            // Instantiates a new group member list page with the groups of which the user is a member
            try
            {
                frmGroupMemberList.Navigate(new GroupMemberList(_userGroupManager.SelectOwnedUserGroupsByUserAccountID(_user.UserAccountID), _userGroupManager, _userManager, _user, "Admin"));
            }
            catch (Exception)
            {
                MessageBox.Show("The Groups you belong to could not be found.");
            }
            
        }

    }
}
