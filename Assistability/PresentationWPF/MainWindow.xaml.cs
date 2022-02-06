/// <summary>
/// Nathaniel Webber
/// Created: 2021/02/05
///
/// This page is what the Login page will direct to
/// which is a general dashboard of all other components
/// </summary>
///
/// <remarks>
/// Nathaniel Webber
/// Updated: 2021/02/18
/// First MVP delivered
/// </remarks>
///
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/23
/// Adding the logout button, sending the user
/// to the login page
/// </remarks>

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
using DataStorageModels;
using DataViewModels;
using DataAccessFakes;
using DataAccessLayer;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // The needed UserAccount manager
        private IUserManager _userAccountManager = new UserManager(new UserAccessor());
        //private IUserManager _userAccountManager = new UserManager(new UserFakes());

        private IRoutineManager _routineManager = new RoutineManager();

        // Instantiates a UserAccount with default values,
        // to be filled with proper information as an access token,
        // following true DialogResult from the CreateLoginUserAccount window
        private UserAccount _user = new UserAccount();

        // The view model of the authenticated user, which is not created until the window loads
        private UserAccountVM _userAccountVM;

        // Flag for a newly created UserAccount
        private bool _isNewUserAccount;

        public MainWindow()
        {
            var loginWindow = new CreateLoginUserAccount(_user);
            loginWindow.ShowDialog();
            if (loginWindow.DialogResult.Value)
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
        }

        public MainWindow(UserAccount _user, IUserManager _userAccountManager, bool _isNewUserAccount)
        {
            this._user = _user;
            this._userAccountManager = _userAccountManager;
            this._isNewUserAccount = _isNewUserAccount;

        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        ///
        /// Event handler for Window_Loaded event
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Check to see if the UserAccount access token is still set to the 'empty' UserAccount
            if(_user.UserAccountID == 0)
            {
                // The user has not properly authenticated, close window
                Close();
            }

            // Create the UserAccountVM from the authenticated user
            try
            {
                _userAccountVM = _userAccountManager.GetUserAccountVMByUserAccount(_user);
            }
            catch (Exception)
            {

                MessageBox.Show("Additional User Account information could not be loaded.");
            }
            txtUsername.Text = _user.UserName;
            txtUsername.IsEnabled = false;
            frmMainFrame.NavigationUIVisibility = NavigationUIVisibility.Visible;
            SelectDashboard(_userAccountVM);

            if (_routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime.Today, _user.UserAccountID).Count == 0)
            {
                txtTodaysRoutines.Text = "You completed your routines today.\nGood Job!";
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        ///
        /// Selects dashboard that matches the role with the most authority that a UserAccountVM has in any of their groups
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        ///
        /// <param name="userAccountVM">The UserAccountVM for which a dashboard is to be selected</param>
        private void SelectDashboard(UserAccountVM userAccountVM)
        {
            // The interface to present
            // This defaults to the "Admin" dashboard, as UserAccounts will be an "Admin" of a group for themselves unless
            // The account was created by an "Admin"
            String interfaceRole = "Admin";
            bool searching = true;
            foreach (var membership in userAccountVM.Memberships)
            {
                while (searching)
                {
                    foreach (var role in membership.Roles)
                    {
                        if (role.Name == "Client")
                        {
                            interfaceRole = "Client";
                            searching = false;
                            break;
                        }
                        else if (role.Name == "Caregiver")
                        {
                            interfaceRole = "Caregiver";
                            searching = false;
                            break;
                        }
                    }
                    searching = false;
                }
            }
            switch (interfaceRole)
            {
                case "Admin":
                    frmMainFrame.Navigate(new AdministratorDashboard(_userAccountVM));
                    break;
                case "Caregiver":
                    frmMainFrame.Navigate(new CaregiverDashboard(_userAccountVM));
                    break;
                case "Client":
                    frmMainFrame.Navigate(new ClientDashboard(_userAccountVM));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        ///
        /// Event handler for btnViewUserDashboard_Click event
        /// </summary>
        ///
        /// <remarks>
        /// Nathaniel Webber
        /// 2021/04/09
        /// 
        /// Added event handler for refreshing the Calendar
        /// </remarks>
        private void btnViewUserDashboard_Click(object sender, RoutedEventArgs e)
        {
            SelectDashboard(_userAccountVM);

            if (_routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime.Today, _user.UserAccountID).Count == 0)
            {
                txtTodaysRoutines.Text = "You completed your routines today.\nGood Job!";
            }
        }
        private void mnuAccountOptions_Click(object sender, RoutedEventArgs e)
        {
            var userAccountDetailWindow = new PgEditUserAccount(_user);

            frmMainFrame.Navigate(userAccountDetailWindow);
        }

        private void mnuAccountLogOff_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult logOff = MessageBox.Show("Are you sure you want to log off?");
            if (logOff == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/04/07
        /// 
        /// Event handler for Selecting a date on the Calendar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdrCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Shows the date selected on the calender
            lblCalendarLabel.Content = "Routines for: ";
            lblDateLabel.Content = cdrRoutineCalendar.SelectedDate.Value.ToString("D");

            // Shows if the Routines have been completed for the day
            if (_routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime.Today, _user.UserAccountID).Count == 0)
            {
                txtTodaysRoutines.Text = "You completed your routines today.\nGood Job!";
            }
            foreach (var routine in _routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime.Today, _user.UserAccountID))
            {
                txtTodaysRoutines.Text = String.Join(Environment.NewLine, routine.Name + ": " + routine.Description).ToString();
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/12
        /// 
        /// Gets a User and sets all of the UserAccount fields to default 
        /// and send user to login screen or close the application since
        /// closing the dashboard would close the application.
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Jory A. Wernette
        /// Updated: 2021/04/23
        /// Update: Was in the old PgEditUserAccount page, but now moving it here into existence again
        /// </remarks>
        /// 
        /// <param name="_user">The UserAccount of the User to log out</param>
        /// <exception cref="ApplicationException">No UserAccount found</exception>
        /// <returns>Bool confirming logout success</returns>
        private void mnuAccountLogoffReturnToLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult logOff = MessageBox.Show("You are about to be logged out");
            if (logOff == MessageBoxResult.OK)
            {
                _user = _userAccountManager.LogoutUser(_user);

                Application.Current.Windows[0].Hide();
                MainWindow main = new MainWindow();
                Application.Current.Windows[0].Close();

                main.Show();
            }

        }
    }
}
