/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface to list all of the users in a list of UserGroups
/// </summary>
///
/// <remarks>
/// </remarks>
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DataStorageModels;
using DataViewModels;
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for GroupMemberList.xaml
    /// </summary>
    public partial class GroupMemberList : Page
    {
        private List<UserGroup> _userGroups;
        private BindingList<UserAccount> _adminList = new BindingList<UserAccount>();
        private BindingList<UserAccount> _clientList = new BindingList<UserAccount>();
        private BindingList<UserAccount> _caregiverList = new BindingList<UserAccount>();
        private IUserGroupManager _userGroupManager;
        private IUserManager _userManager;
        private UserAccountVM _user;
        private string _viewerRole;
        private string _firstListRole;
        private string _secondListRole;

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Constructs a GroupMember list
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userGroups">The UserGroups for which this list will display</param>
        /// <param name="userGroupManager">The UserGroupManager reference</param>
        /// <param name="userManager">The UserManager reference</param>
        /// <param name="user">The User currently controlling the list</param>
        /// <param name="role">The role for which this list will display. "Admin", "Client"</param>
        public GroupMemberList(List<UserGroup> userGroups, IUserGroupManager userGroupManager, IUserManager userManager, UserAccountVM user, string role)
        {
            InitializeComponent();

            _userGroups = userGroups;
            _userGroupManager = userGroupManager;
            _userManager = userManager;
            _user = user;
            _viewerRole = role;
            RefreshLists();
            frmSelectedUser.Navigate(new UserGroupMemberEditDetail(_user, _user, _viewerRole, _viewerRole, true));
        }

        private void RefreshLists()
        {
            switch (_viewerRole)
            {
                case "Admin":
                    _clientList = PopulateList("Client");
                    dgFirstList.ItemsSource = _clientList;

                    _caregiverList = PopulateList("Caregiver");
                    dgSecondList.ItemsSource = _caregiverList;

                    _firstListRole = "Client";
                    _secondListRole = "Caregiver";

                    lblFirstList.Content = "Clients";
                    lblSecondList.Content = "Caregivers";
                    break;
                case "Client":
                    _adminList = PopulateList("Admin");
                    dgFirstList.ItemsSource = _adminList;

                    _caregiverList = PopulateList("Caregiver");
                    dgSecondList.ItemsSource = _caregiverList;

                    _firstListRole = "Admin";
                    _secondListRole = "Caregiver";

                    lblFirstList.Content = "Admins";
                    lblSecondList.Content = "Caregivers";
                    break;
                case "Caregiver":
                    _adminList = PopulateList("Client");
                    dgFirstList.ItemsSource = _adminList;

                    _caregiverList = PopulateList("Admin");
                    dgSecondList.ItemsSource = _caregiverList;

                    _firstListRole = "Client";
                    _secondListRole = "Admin";

                    lblFirstList.Content = "Clients";
                    lblSecondList.Content = "Admins";
                    break;
                default:
                    break;
            }
            switch (_viewerRole)
            {
                case "Admin":
                    if (_clientList.Count == 0 && _caregiverList.Count == 0)
                    {
                        dgFirstList.Visibility = Visibility.Hidden;
                        dgSecondList.Visibility = Visibility.Hidden;
                    }
                    break;
                case "Client":
                    if (_adminList.Count == 0 && _caregiverList.Count == 0)
                    {
                        dgFirstList.Visibility = Visibility.Hidden;
                        dgSecondList.Visibility = Visibility.Hidden;
                    }
                    break;
                case "Caregiver":
                    if (_adminList.Count == 0 && _caregiverList.Count == 0)
                    {
                        dgFirstList.Visibility = Visibility.Hidden;
                        dgSecondList.Visibility = Visibility.Hidden;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Retrieves a BindingList of UserAccounts with the role provided
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="role">The role for which this list will display. "Admin", "Client"</param>
        /// <returns>A BindingList of UserAccount"</returns>
        private BindingList<UserAccount> PopulateList(String role)
        {
            foreach (var group in _userGroups)
            {
                try
                {
                    return _userGroupManager.GetUserAccountsInUserGroupByRole(_userManager, group, role);
                }
                catch (Exception)
                {
                    MessageBox.Show("No " + role + "s found.");
                }

            }
            return null;
        }


        private void dgFirstList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = (UserAccount)dgFirstList.SelectedItem;
            GroupMemberClicked(selectedUser, _viewerRole, _firstListRole);
        }

        private void dgSecondList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var selectedUser = (UserAccount)dgSecondList.SelectedItem;
            GroupMemberClicked(selectedUser, _viewerRole, _secondListRole);
            
        }

        private void GroupMemberClicked(UserAccount userAccount, string _viewerRole, string _selectedUserRole)
        {
            if (userAccount != null)
            {
                try
                {
                    frmSelectedUser.Navigate(new UserGroupMemberEditDetail(_userManager.GetUserAccountVMByUserAccount(userAccount), _user, _viewerRole, _selectedUserRole));
                }
                catch (Exception)
                {
                    // Do not navigate
                }
            }
        }

        private void btnAddFirstList_Click(object sender, RoutedEventArgs e)
        {
            UserAccount userAccount = new UserAccount();
            var createAccount = new WPFCreateAccountWindow(userAccount);
            var result = createAccount.ShowDialog();
            if (result == true)
            {
                try
                {
                    UserAccount createdUser = _userManager.GetUserAccountByUserAccountID(userAccount.UserAccountID);
                    // Currently selects the single group for which the person adding is presumably the owner
                    // This should be changed in the future to allow selecting different groups
                    _userGroupManager.AddNewUserToUserGroup(createdUser, _userGroups[0], _firstListRole);
                    MessageBox.Show(createdUser.FirstName + " " + createdUser.LastName + " has been added as a " + _firstListRole + ".");
                    RefreshLists();
                }
                catch (Exception)
                {
                    MessageBox.Show("The user couldn't be created.");
                }
            }
        }

        private void btnAddSecondList_Click(object sender, RoutedEventArgs e)
        {
            UserAccount userAccount = new UserAccount();
            var createAccount = new WPFCreateAccountWindow(userAccount);
            var result = createAccount.ShowDialog();
            if (result == true)
            {
                try
                {
                    UserAccount createdUser = _userManager.GetUserAccountByUserAccountID(userAccount.UserAccountID);
                    // Currently selects the single group for which the person adding is presumably the owner
                    // This should be changed in the future to allow selecting different groups
                    _userGroupManager.AddNewUserToUserGroup(createdUser, _userGroups[0], _secondListRole);
                    MessageBox.Show(createdUser.FirstName + " " + createdUser.LastName + " has been added as a " + _secondListRole + ".");
                    RefreshLists();
                }
                catch (Exception)
                {
                    MessageBox.Show("The user couldn't be added to the group.");
                }
            }
        }
    }
}
