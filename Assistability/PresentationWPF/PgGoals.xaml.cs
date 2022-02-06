///<summary>
///Becky Baenziger
///2021/02/18
/// ///
/// Page to represent Goals page where they would click a create goals button to go to
/// the Create Goal page.
///</summary>
///
///<remarks>
///</remarks>

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
using LogicLayerInterfaces;
using LogicLayer;
using DataAccessFakes;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for PgGoals.xaml
    /// </summary>
    public partial class PgGoals : Page
    {
        private UserAccount _clientUser = new UserAccount();
        private UserAccount _adminUser = new UserAccount();
        private IHabGoalManager _habGoalManager = new HabGoalManager();
        private IAttGoalManager _attGoalManager = new AttGoalManager();
        private IExtGoalManager _extGoalManager = new ExtGoalManager();

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/12
        /// ///
        /// Constructor that brings in the the controllinguser and selected user ids
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="controllingUser"></param>
        /// <param name="selectedUser"></param>
        public PgGoals(UserAccount controllingUser, UserAccount selectedUser)
        {
            _clientUser = selectedUser;
            _adminUser = controllingUser;

            InitializeComponent();
        }

        public PgGoals()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// Click event on Create Goal button to take the user to frmCreatGoal() and supplies the proper user ids
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateGoal_Click(object sender, RoutedEventArgs e)
        {

            var frmCreateGoal = new PgfrmCreateGoal(_adminUser, _clientUser);

            // takes user to the Create Goal form when clicked
            NavigationService.Navigate(frmCreateGoal);

        }

        /*
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/02
        /// ///
        /// Send the selected goal and copy of selected go with active changed to the correct
        /// editGoal.  Changes assignment
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAssignGoal_Click(object sender, RoutedEventArgs e)
        {
            if (dgHabGoalList.IsVisible)
            {
                bool assign;
                var selectedItem = (HabGoalViewModel)dgHabGoalList.SelectedItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Select a goal which goal you want to view.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    if (selectedItem.Active == true)
                    {
                        assign = false;
                    }
                    else
                    {
                        assign = true;
                    }
                    
                    var newSelectedItem = new HabGoalViewModel()
                    {
                        UserID_client = selectedItem.UserID_client,
                        UserID_admin = selectedItem.UserID_admin,
                        HabGoalName = selectedItem.HabGoalName,
                        HabGoalDescription = selectedItem.HabGoalDescription,
                        HabGoalTargetDate = selectedItem.HabGoalTargetDate,
                        HabGoalEntryDate = selectedItem.HabGoalEntryDate,
                        HabGoalRemovalDate = DateTime.Now,
                        Active = assign,
                        AwardName = selectedItem.AwardName,
                        RoutineName = selectedItem.RoutineName
                    };

                    try
                    {
                        _habGoalManager.EditHabitualGoal(selectedItem, newSelectedItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to Assign or Unassign a goal.", "Invalid Operation", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
                

            }

            if (dgAttGoalList.IsVisible)
            {
                bool assign;
                var selectedItem = (AttGoalViewModel)dgAttGoalList.SelectedItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Select a goal which goal you want to view.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    if (selectedItem.Active == true)
                    {
                        assign = false;
                    }
                    else
                    {
                        assign = true;
                    }
                    var newSelectedItem = new AttGoalViewModel()
                    {
                        UserID_client = selectedItem.UserID_client,
                        UserID_admin = selectedItem.UserID_admin,
                        AttGoalName = selectedItem.AttGoalName,
                        AttGoalDescription = selectedItem.AttGoalDescription,
                        AttGoalTargetDate = selectedItem.AttGoalTargetDate,
                        AttGoalEntryDate = selectedItem.AttGoalEntryDate,
                        AttGoalRemovalDate = DateTime.Now,
                        Active = assign,
                        AwardName = selectedItem.AwardName,
                        PerformanceName = selectedItem.PerformanceName
                    };
                    try
                    {
                        _attGoalManager.EditAttainmentGoal(selectedItem, newSelectedItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to Assign or Unassign a goal.", "Invalid Operation", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
                
            }

            if (dgExtGoalList.IsVisible)
            {
                bool assign;
                var selectedItem = (ExtGoalViewModel)dgExtGoalList.SelectedItem;
                if(selectedItem == null)
                {
                    MessageBox.Show("Select a goal which goal you want to view.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    if (selectedItem.Active == true)
                    {
                        assign = false;
                    }
                    else
                    {
                        assign = true;
                    }
                    var newSelectedItem = new ExtGoalViewModel()
                    {
                        UserID_client = selectedItem.UserID_client,
                        UserID_admin = selectedItem.UserID_admin,
                        ExtGoalName = selectedItem.ExtGoalName,
                        ExtGoalDescription = selectedItem.ExtGoalDescription,
                        ExtGoalTargetDate = selectedItem.ExtGoalTargetDate,
                        ExtGoalEntryDate = selectedItem.ExtGoalEntryDate,
                        ExtGoalRemovalDate = DateTime.Now,
                        Active = assign,
                        AwardName = selectedItem.AwardName,
                        IncidentName = selectedItem.IncidentName
                    };
                    try
                    {
                        _extGoalManager.EditExtinctionGoal(selectedItem, newSelectedItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to Assign or Unassign a goal.", "Invalid Operation", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
               
            }
        }
        */
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/12
        /// ///
        /// Button to make the Habitual goal list visible.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHabGoal_Click(object sender, RoutedEventArgs e)
        {
            dgHabGoalList.Visibility = Visibility.Visible;
            dgHabGoalList.IsEnabled = true;
            dgAttGoalList.Visibility = Visibility.Hidden;
            dgAttGoalList.IsEnabled = false;
            dgExtGoalList.Visibility = Visibility.Hidden;
            dgExtGoalList.IsEnabled = false;

            try
            {
                var habGoalManager = new HabGoalManager();
                List<HabGoalViewModel> habGoal = new List<HabGoalViewModel>();

                if (chkActiveGoals.IsChecked == true)
                {
                    dgHabGoalList.ItemsSource = habGoalManager.RetrieveHabitualGoalsByActive(_clientUser.UserAccountID, true);
                    
                }
                else
                {
                    dgHabGoalList.ItemsSource = habGoalManager.RetrieveHabitualGoalsByUserIDClient(_clientUser.UserAccountID);
                }
            }
            catch (Exception ex)
            {
                if(dgHabGoalList == null && chkActiveGoals.IsChecked == true)
                {
                    MessageBox.Show("There are no active habitual goals.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(dgHabGoalList == null && chkActiveGoals.IsChecked != true)
                {
                    MessageBox.Show("There are no inactive habitual goals.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                
            }


        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/12
        /// ///
        /// Button make sthe attention goal list visible.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttGoal_Click(object sender, RoutedEventArgs e)
        {
            dgHabGoalList.Visibility = Visibility.Hidden;
            dgHabGoalList.IsEnabled = false;
            dgAttGoalList.Visibility = Visibility.Visible;
            dgAttGoalList.IsEnabled = true;
            dgExtGoalList.Visibility = Visibility.Hidden;
            dgExtGoalList.IsEnabled = false;

            try
            {
                var attGoalManager = new AttGoalManager();
                List<AttGoalViewModel> attGoal = new List<AttGoalViewModel>();

                if (chkActiveGoals.IsChecked == true)
                {
                    dgAttGoalList.ItemsSource = attGoalManager.RetrieveAttainmentGoalsByActive(_clientUser.UserAccountID, true);
                }
                else
                {
                    dgAttGoalList.ItemsSource = attGoalManager.RetrieveAttainmentGoalsByUserIDClient(_clientUser.UserAccountID);
                }
            }
            catch (Exception ex)
            {
                if (dgAttGoalList == null && chkActiveGoals.IsChecked == true)
                {
                    MessageBox.Show("There are no active attainment goals.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (dgAttGoalList == null && chkActiveGoals.IsChecked != true)
                {
                    MessageBox.Show("There are no inactive attainment goals.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }



        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/12
        /// ///
        /// Button to click to make the Extinction Goal List visible.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExtGoal_Click(object sender, RoutedEventArgs e)
        {
            dgHabGoalList.Visibility = Visibility.Hidden;
            dgHabGoalList.IsEnabled = false;
            dgAttGoalList.Visibility = Visibility.Hidden;
            dgAttGoalList.IsEnabled = false;
            dgExtGoalList.Visibility = Visibility.Visible;
            dgExtGoalList.IsEnabled = true;

            try
            {
                var extGoalManager = new ExtGoalManager();
                List<ExtGoalViewModel> extGoal = new List<ExtGoalViewModel>();

                if (chkActiveGoals.IsChecked == true)
                {
                    dgExtGoalList.ItemsSource = extGoalManager.RetreiveAllExtGoals(_clientUser.UserAccountID, true);
                }
                else
                {
                    dgExtGoalList.ItemsSource = extGoalManager.RetrieveExtinctionGoalsByUserIDClient(_clientUser.UserAccountID);
                }
            }
            catch (Exception ex)
            {
                if (dgExtGoalList == null && chkActiveGoals.IsChecked == true)
                {
                    MessageBox.Show("There are no active extinctionl goals.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (dgExtGoalList == null && chkActiveGoals.IsChecked != true)
                {
                    MessageBox.Show("There are no inactive extinction goals.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }

            }


        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/16
        /// ///
        /// When a goal is selected click to view details and be given option to edit.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewGoal_Click(object sender, RoutedEventArgs e)
        {
            if (dgHabGoalList.IsVisible)
            {
                
                var selectedItem = (HabGoalViewModel)dgHabGoalList.SelectedItem;

                if (selectedItem == null)
                {
                    MessageBox.Show("Select a goal which goal you want to view.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    var pgViewEditGoal = new PgGoalViewEdit(selectedItem, _adminUser, _clientUser);

                    // takes user to the Create Goal form when clicked
                    NavigationService.Navigate(pgViewEditGoal);
                }
            }

            if (dgAttGoalList.IsVisible)
            {
                var selectedItem = (AttGoalViewModel)dgAttGoalList.SelectedItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Select a goal which goal you want to view.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    var pgViewEditGoal = new PgGoalViewEdit(selectedItem, _adminUser, _clientUser);

                    // takes user to the Create Goal form when clicked
                    NavigationService.Navigate(pgViewEditGoal);
                }
            }

            if (dgExtGoalList.IsVisible)
            {
                var selectedItem = (ExtGoalViewModel)dgExtGoalList.SelectedItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Select a goal which goal you want to view.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    var pgViewEditGoal = new PgGoalViewEdit(selectedItem, _adminUser, _clientUser);

                    // takes user to the Create Goal form when clicked
                    NavigationService.Navigate(pgViewEditGoal);
                }
            }

        }

        private void chkActiveGoals_Click(object sender, RoutedEventArgs e)
        {
            dgHabGoalList.ItemsSource = null;
            dgExtGoalList.ItemsSource = null;
            dgAttGoalList.ItemsSource = null;

            dgHabGoalList.Visibility = Visibility.Hidden;
            dgHabGoalList.IsEnabled = false;
            dgAttGoalList.Visibility = Visibility.Hidden;
            dgAttGoalList.IsEnabled = false;
            dgExtGoalList.Visibility = Visibility.Hidden;
            dgExtGoalList.IsEnabled = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dgHabGoalList.ItemsSource = null;
            dgExtGoalList.ItemsSource = null;
            dgAttGoalList.ItemsSource = null;

            dgHabGoalList.Visibility = Visibility.Hidden;
            dgHabGoalList.IsEnabled = false;
            dgAttGoalList.Visibility = Visibility.Hidden;
            dgAttGoalList.IsEnabled = false;
            dgExtGoalList.Visibility = Visibility.Hidden;
            dgExtGoalList.IsEnabled = false;
        }
    }
}
