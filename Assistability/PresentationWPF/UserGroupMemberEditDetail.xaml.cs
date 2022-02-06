/// <summary>
/// William Clark
/// Created: 2021/02/26
///
/// Interface to view UserGroup members
/// </summary>
///
/// <remarks>
/// Ryan Taylor
/// Created: 2021/04/21
/// 
/// added a way to view performances.
/// </remarks>
using DataAccessFakes;
using DataAccessLayer;
using DataStorageModels;
using DataViewModels;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserGroupMemberEditDetail.xaml
    /// </summary>
    public partial class UserGroupMemberEditDetail : Page
    {
        private UserAccountVM _selectedUser;
        private UserAccountVM _controllingUser;
        private string _selectedUserRole;
        private bool _isViewingSelf;
        private string _viewerRole;

        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        ///
        /// Constructor that accepts a UserAccountVM, viewer's role, and the role of the selected user
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        ///
        /// <param name="selectedUser">The UserAccountVM for which to view active routines</param>
        /// <param name="controllingUser">The UserAccountVM which is controlling</param>
        /// <param name="viewerRole">The role of the User viewing</param>
        /// <param name="selectedUserRole">The role of the User selected</param>
        public UserGroupMemberEditDetail(UserAccountVM selectedUser, UserAccountVM controllingUser, string viewerRole, string selectedUserRole)
        {
            _selectedUser = selectedUser;
            _viewerRole = viewerRole;
            _selectedUserRole = selectedUserRole;
            _isViewingSelf = false;
            _controllingUser = controllingUser;
            InitializeComponent();
            HandleControlVisibility();
            lblUserGroupMemberName.Content = _selectedUser.FirstName + " " + _selectedUser.LastName;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        ///
        /// Constructor that accepts a UserAccountVM, viewer's role, the role of the selected user, and if the user is viewing themselves
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        ///
        /// <param name="selectedUser">The UserAccountVM for which to view active routines</param>
        /// <param name="controllingUser">The UserAccountVM which is controlling</param>
        /// <param name="viewerRole">The role of the User viewing</param>
        /// <param name="selectedUserRole">The role of the User selected</param>
        /// <param name="isViewingSelf">If the user is currently viewing themselves</param>
        public UserGroupMemberEditDetail(UserAccountVM selectedUser, UserAccountVM controllingUser, string viewerRole, string selectedUserRole, bool isViewingSelf)
        {
            _selectedUser = selectedUser;
            _viewerRole = viewerRole;
            _selectedUserRole = selectedUserRole;
            _isViewingSelf = isViewingSelf;
            _controllingUser = controllingUser;
            InitializeComponent();
            HandleControlVisibility();
            lblUserGroupMemberName.Content = _selectedUser.FirstName + " " + _selectedUser.LastName;
        }
        private void HandleControlVisibility()
        {
            HideControls();
            if (_isViewingSelf)
            {
                switch (_viewerRole)
                {
                    case "Admin":
                        HandleAdminViewingAdmin();
                        break;
                    case "Caregiver":
                        break;
                    case "Client":
                        HandleClientViewingClient();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (_viewerRole)
                {
                    case "Admin":
                        switch (_selectedUserRole)
                        {
                            case "Admin":
                                HandleAdminViewingAdmin();
                                break;
                            case "Caregiver":
                                HandleAdminViewingCaregiver();
                                break;
                            case "Client":
                                HandleAdminViewingClient();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Caregiver":
                        switch (_selectedUserRole)
                        {
                            case "Admin":
                                HandleCaregiverViewingAdmin();
                                break;
                            case "Caregiver":
                                break;
                            case "Client":
                                HandleCaregiverViewingClient();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Client":
                        switch (_selectedUserRole)
                        {
                            case "Admin":
                                break;
                            case "Caregiver":
                                break;
                            case "Client":
                                HandleClientViewingClient();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void HideControls()
        {
            btnCompleteRoutines.Visibility = Visibility.Hidden;
            btnViewRoutines.Visibility = Visibility.Hidden;
            btnViewIncidents.Visibility = Visibility.Hidden;
            btnViewRewards.Visibility = Visibility.Hidden;
            btnViewAwards.Visibility = Visibility.Hidden;
            btnViewGoals.Visibility = Visibility.Hidden;
            btnViewIncidents.Visibility = Visibility.Hidden;
            btnViewJournals.Visibility = Visibility.Hidden;
            btnViewPerformances.Visibility = Visibility.Hidden;
            btnAssignRoles.Visibility = Visibility.Hidden;
        }
        private void HandleAdminViewingAdmin()
        {
            btnCompleteRoutines.Visibility = Visibility.Visible;
            btnViewRoutines.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewRewards.Visibility = Visibility.Visible;
            btnViewAwards.Visibility = Visibility.Visible;
            btnViewGoals.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewJournals.Visibility = Visibility.Visible;
            btnViewPerformances.Visibility = Visibility.Visible;
            btnAssignRoles.Visibility = Visibility.Visible;
        }
        private void HandleAdminViewingClient()
        {
            btnCompleteRoutines.Visibility = Visibility.Visible;
            btnViewRoutines.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewRewards.Visibility = Visibility.Visible;
            btnViewAwards.Visibility = Visibility.Visible;
            btnViewGoals.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewPerformances.Visibility = Visibility.Visible;
            btnAssignRoles.Visibility = Visibility.Visible;
        }
        private void HandleAdminViewingCaregiver()
        {
            btnAssignRoles.Visibility = Visibility.Visible;
        }
        private void HandleCaregiverViewingClient()
        {
            btnCompleteRoutines.Visibility = Visibility.Visible;
            btnViewRoutines.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewRewards.Visibility = Visibility.Visible;
            btnViewAwards.Visibility = Visibility.Visible;
            btnViewGoals.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewJournals.Visibility = Visibility.Visible;
            btnViewPerformances.Visibility = Visibility.Visible;
        }
        private void HandleCaregiverViewingAdmin()
        {
            btnCompleteRoutines.Visibility = Visibility.Visible;
            btnViewRoutines.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewRewards.Visibility = Visibility.Visible;
            btnViewAwards.Visibility = Visibility.Visible;
            btnViewGoals.Visibility = Visibility.Visible;
            btnViewIncidents.Visibility = Visibility.Visible;
            btnViewJournals.Visibility = Visibility.Visible;
            btnViewPerformances.Visibility = Visibility.Visible;
        }
        private void HandleClientViewingClient()
        {
            if (_isViewingSelf)
            {
                btnCompleteRoutines.Visibility = Visibility.Visible;
                btnViewRoutines.Visibility = Visibility.Visible;
                btnViewIncidents.Visibility = Visibility.Visible;
                btnViewRewards.Visibility = Visibility.Visible;
                btnViewAwards.Visibility = Visibility.Visible;
                btnViewGoals.Visibility = Visibility.Visible;
                btnViewIncidents.Visibility = Visibility.Visible;
                btnViewJournals.Visibility = Visibility.Visible;
                btnViewPerformances.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        ///
        /// Event Handler to view active routines for a UserAccount and complete them
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void btnCompleteRoutines_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ActiveRoutines(new RoutineManager(), _selectedUser));
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        ///
        /// Event Handler to view all routines for a UserAccount
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void btnViewRoutines_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pgViewRoutines(_selectedUser, _controllingUser.UserAccountID));
        }

        private void btnViewIncidents_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pgViewIncidents(_selectedUser));
        }

        private void btnViewRewards_Click(object sender, RoutedEventArgs e)
        {
            var viewRewards = new pgRewardpgViewRewards(_selectedUser);

            this.NavigationService.Navigate(viewRewards);
        }


        private void btnViewJournals_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PgViewJournals(_selectedUser.UserAccountID, _selectedUser));
        }

        private void btnViewAwards_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pgViewAwards(_selectedUser.UserAccountID));
        }

        private void btnViewGoals_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PgGoals(_controllingUser, _selectedUser));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// Event Handler to view a person's performances
        /// </summary>
        private void btnViewPerformances_Click(object sender, RoutedEventArgs e)
        {
            var viewPerformances = new pgViewPerformances(_selectedUser, _controllingUser);
            this.NavigationService.Navigate(viewPerformances);
        }



        /// <summary>
        /// Mitchell Pual
        /// Created: 2021/04/01
        ///
        /// Event Handler to view owned user groups. 
        /// </summary>


        private void btnAssignRoles_Click(object sender, RoutedEventArgs e)
        {
            var viewAssignRoles = new pgAddEditUserMembershipRoles(_selectedUser.UserAccountID);

            this.NavigationService.Navigate(viewAssignRoles);
        }
    }
}