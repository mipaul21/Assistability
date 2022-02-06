///<summary>
///Becky Baenziger
///2021/04/13
/// ///
/// Page for PgGoalViewEdit which will allow users to view the details of a goal and edit certain parts of em. Brings in the 
/// selected goal from the dgLists on the PgGoals page when the user clicks view.
///
///</summary>
///
///<remarks>
/// Updater Name:
/// Update Date:
/// 
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
using DataViewModels;
using LogicLayer;
using DataStorageModels;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for PgGoalViewEdit.xaml
    /// </summary>
    public partial class PgGoalViewEdit : Page
    {
        private HabGoalViewModel _habGoal = new HabGoalViewModel();
        private AttGoalViewModel _attGoal = new AttGoalViewModel();
        private ExtGoalViewModel _extGoal = new ExtGoalViewModel();
        private UserAccount _clientUser = new UserAccount();
        private UserAccount _adminUser = new UserAccount();

        private HabGoalManager habGoalManager = new HabGoalManager();
        private AttGoalManager attGoalManager = new AttGoalManager();
        private ExtGoalManager extGoalManager = new ExtGoalManager();
        private AwardManager awardManager = new AwardManager();

        int frequency;

        public PgGoalViewEdit(HabGoalViewModel selectedItem, UserAccount controllingUser, UserAccount selectedUser)
        {
            _habGoal = selectedItem;
            _clientUser = selectedUser;
            _adminUser = controllingUser;

            InitializeComponent();

        }
        public PgGoalViewEdit(AttGoalViewModel selectedItem, UserAccount controllingUser, UserAccount selectedUser)
        {
            _attGoal = selectedItem;
            _clientUser = selectedUser;
            _adminUser = controllingUser;

            InitializeComponent();
        }
        public PgGoalViewEdit(ExtGoalViewModel selectedItem, UserAccount controllingUser, UserAccount selectedUser)
        {
            _extGoal = selectedItem;
            _clientUser = selectedUser;
            _adminUser = controllingUser;

            InitializeComponent();
        }

        public PgGoalViewEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Becky Baenziger
        /// Created 2021/04/13
        /// ///
        /// Decides what should be displayed based on the goal is brought into it.  Sets fields to read only.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {

            // display existing goal details
            if (_habGoal.HabGoalName != null)
            {
                lblGoalSubType.Content = "Routine Name";
                lblSubtypeCompleteStatus.Content = "routines completed so far.";
                lblGoalSubTypeFrequency.Content = "Routine Frequency";
                lblSubtypeCompleted.Content = "Routines Completed";

                txtGoalName.Text = _habGoal.HabGoalName;
                txtGoalName.IsReadOnly = true;
                txtGoalDescription.Text = _habGoal.HabGoalDescription;
                txtGoalDescription.IsReadOnly = true;
                cboGoalSubType.Items.Add(_habGoal.RoutineName);
                cboGoalSubType.SelectedItem = _habGoal.RoutineName;
                cboGoalSubType.AllowDrop = false;
                cboGoalSubType.IsReadOnly = true;
                txtGoalSubtypeFrequency.Text = _habGoal.RoutineFrequency.ToString();
                txtGoalSubtypeFrequency.IsReadOnly = true;
                txtTargetDate.Text = _habGoal.HabGoalTargetDate.ToShortDateString();
                txtTargetDate.IsReadOnly = true;
                //txtSubtypeComplete.Text = habGoalManager.;  // need to get completion into the program
                cboGoalAward.Items.Add(_habGoal.AwardName);
                cboGoalAward.SelectedItem = _habGoal.AwardName;
                cboGoalAward.IsReadOnly = true;
                cboGoalAward.AllowDrop = false;
                if (_habGoal.Active == true)
                {
                    chkActiveGoal.IsChecked = true;
                }
                chkActiveGoal.IsEnabled = false;
            }

            if (_attGoal.AttGoalName != null)
            {
                lblGoalSubType.Content = "Performance Name";
                lblSubtypeCompleteStatus.Content = "performances occured so far.";
                lblGoalSubTypeFrequency.Content = "Performance Frequency";
                lblSubtypeCompleted.Content = "Performances Occured";

                txtGoalName.Text = _attGoal.AttGoalName;
                txtGoalName.IsReadOnly = true;
                txtGoalDescription.Text = _attGoal.AttGoalDescription;
                txtGoalDescription.IsReadOnly = true;
                cboGoalSubType.Items.Add(_attGoal.PerformanceName);
                cboGoalSubType.SelectedItem = _attGoal.PerformanceName;
                cboGoalSubType.IsReadOnly = true;
                cboGoalSubType.AllowDrop = false;
                txtGoalSubtypeFrequency.Text = _attGoal.PerformanceFrequency.ToString();
                txtGoalSubtypeFrequency.IsReadOnly = true;
                txtTargetDate.Text = _attGoal.AttGoalTargetDate.ToShortDateString();
                txtTargetDate.IsReadOnly = true;
                //txtSubtypeComplete.Text = attGoalManager.;  // need to get completion into the program
                cboGoalAward.Items.Add(_attGoal.AwardName);
                cboGoalAward.SelectedItem = _attGoal.AwardName;
                cboGoalAward.IsReadOnly = true;
                cboGoalAward.AllowDrop = false;
                if (_attGoal.Active == true)
                {
                    chkActiveGoal.IsChecked = true;
                }
                chkActiveGoal.IsEnabled = false;
            }

            if (_extGoal.ExtGoalName != null)
            {
                lblGoalSubType.Content = "Incident Name";
                lblSubtypeCompleteStatus.Content = "incidents occured so far.";
                lblGoalSubTypeFrequency.Content = "Incident Frequency";
                lblSubtypeCompleted.Content = "Incidents Occured";

                txtGoalName.Text = _extGoal.ExtGoalName.ToString();
                txtGoalName.IsReadOnly = true;
                txtGoalDescription.Text = _extGoal.ExtGoalDescription.ToString();
                txtGoalDescription.IsReadOnly = true;
                cboGoalSubType.Items.Add(_extGoal.IncidentName);
                cboGoalSubType.SelectedItem = _extGoal.IncidentName.ToString();
                cboGoalSubType.IsReadOnly = true;
                cboGoalSubType.AllowDrop = false;
                txtGoalSubtypeFrequency.Text = _extGoal.IncidentFrequency.ToString();
                txtGoalSubtypeFrequency.IsReadOnly = true;
                txtTargetDate.Text = _extGoal.ExtGoalTargetDate.ToShortDateString();
                txtTargetDate.IsReadOnly = true;
                //txtSubtypeComplete.Text = extGoalManager.;  // need to get completion into the program
                cboGoalAward.Items.Add(_extGoal.AwardName);
                cboGoalAward.SelectedItem = _extGoal.AwardName;
                cboGoalAward.IsReadOnly = true;
                cboGoalAward.AllowDrop = false;
                if (_extGoal.Active == true)
                {
                    chkActiveGoal.IsChecked = true;
                }
                chkActiveGoal.IsEnabled = false;
            }


        }

        /// <summary>
        /// Becky Baenziger
        /// Created 2021/04/13
        /// ///
        /// Makes fields editable that are allowed to be changed.  Switched the edit button to the save button.  Populates the award drop down menu.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            
            txtGoalDescription.IsReadOnly = false;
            txtGoalSubtypeFrequency.IsReadOnly = false;
            cboGoalAward.IsReadOnly = false;
            chkActiveGoal.IsEnabled = true;

            btnSave.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;

            btnEdit.IsEnabled = false;
            btnEdit.Visibility = Visibility.Hidden;


            var savedAward = _habGoal.AwardName != null ? _habGoal.AwardName : _attGoal.AwardName != null ? _attGoal.AwardName : _extGoal.AwardName;

            // populate the awards cboGoalAward
            List<Award> awards = awardManager.RetreiveAllAwards();
            List<string> awardNames = awards.OrderBy(a => a.AwardName).Select(a => a.AwardName).ToList();
            if(cboGoalAward.Items.IsEmpty == false)
            {
                cboGoalAward.ItemsSource = null;
                cboGoalAward.Items.Clear();
            }
            cboGoalAward.ItemsSource = awardNames;
            cboGoalAward.SelectedItem = savedAward;
        }
        
        /// <summary>
        /// Becky Baenziger
        /// Created 2021/04/13
        /// ///
        /// Sends user back to the goals page.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Upate Date:
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var pgGoals = new PgGoals(_adminUser, _clientUser);
            
            // takes back to Goals page
            NavigationService.Navigate(pgGoals);
        }

        /// <summary>
        /// Becky Baenziger
        /// Created 2021/04/13
        /// ///
        /// Validates the value entered into the frequency.  Swithces fields back to uneditable.  Switches back to edit button and changes cancel
        /// btn to Exit.  Sends chnaged data to be updated in the database
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtGoalSubtypeFrequency.Text, out frequency))
            {
                MessageBox.Show("The Frequency must be a number");
                return;
            }
            
            if(frequency == 0)
            {
                MessageBox.Show("You must enter a number greater than 0 for Frequency");
                return;
            }

            txtGoalDescription.IsReadOnly = true;
            txtGoalSubtypeFrequency.IsReadOnly = true;
            cboGoalAward.IsReadOnly = true;
            chkActiveGoal.IsEnabled = false;

            btnSave.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;

            btnEdit.IsEnabled = true;
            btnEdit.Visibility = Visibility.Visible;

            btnCancel.Content = "Exit";

            if (_habGoal.HabGoalName != null)
            {
                var newHabGoal = new HabGoalViewModel()
                {
                    UserID_client = _clientUser.UserAccountID,
                    UserID_admin = _adminUser.UserAccountID,
                    HabGoalName = _habGoal.HabGoalName,
                    HabGoalDescription = txtGoalDescription.Text,
                    HabGoalTargetDate = _habGoal.HabGoalTargetDate,
                    HabGoalEntryDate = _habGoal.HabGoalEntryDate,
                    RoutineFrequency = frequency,
                    Active = chkActiveGoal.IsChecked.Value,
                    AwardName = cboGoalAward.SelectedItem.ToString(),
                    RoutineName = _habGoal.RoutineName,
                };
                try
                {
                    habGoalManager.EditHabitualGoal(_habGoal, newHabGoal);
                    MessageBox.Show(_habGoal.HabGoalName + " edited successfully.", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    _habGoal = newHabGoal;
                    var pgViewEditGoal = new PgGoalViewEdit(_habGoal, _adminUser, _clientUser);

                    // takes user to the View Goal form when clicked
                    NavigationService.Navigate(pgViewEditGoal);
                }
                catch (Exception)
                {
                    MessageBox.Show("Save was not successful", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            if (_attGoal.AttGoalName != null)
            {
                var newAttGoal = new AttGoalViewModel()
                {
                    UserID_client = _clientUser.UserAccountID,
                    UserID_admin = _adminUser.UserAccountID,
                    AttGoalName = _attGoal.AttGoalName,
                    AttGoalDescription = txtGoalDescription.Text,
                    AttGoalTargetDate = _attGoal.AttGoalTargetDate,
                    AttGoalEntryDate = _attGoal.AttGoalEntryDate,
                    PerformanceFrequency = frequency,
                    Active = chkActiveGoal.IsChecked.Value,
                    AwardName = cboGoalAward.SelectedItem.ToString(),
                    PerformanceName = _attGoal.PerformanceName,
                };
                try
                {
                    attGoalManager.EditAttainmentGoal(_attGoal, newAttGoal);
                    MessageBox.Show(_attGoal.AttGoalName + " edited successfully.", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    _attGoal = newAttGoal;
                    var pgViewEditGoal = new PgGoalViewEdit(_attGoal, _adminUser, _clientUser);

                    // takes user to the Create Goal form when clicked
                    NavigationService.Navigate(pgViewEditGoal);
                }
                catch (Exception)
                {
                    MessageBox.Show("Save was not successful", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            if (_extGoal.ExtGoalName != null)
            {
                var newExtGoal = new ExtGoalViewModel()
                {
                    UserID_client = _clientUser.UserAccountID,
                    UserID_admin = _adminUser.UserAccountID,
                    ExtGoalName = _extGoal.ExtGoalName,
                    ExtGoalDescription = txtGoalDescription.Text,
                    ExtGoalTargetDate = _extGoal.ExtGoalTargetDate,
                    ExtGoalEntryDate = _extGoal.ExtGoalEntryDate,
                    IncidentFrequency = frequency,
                    Active = chkActiveGoal.IsChecked.HasValue,
                    AwardName = cboGoalAward.SelectedItem.ToString(),
                    IncidentName = _extGoal.IncidentName,
                };
                try
                {
                    extGoalManager.EditExtinctionGoal(_extGoal, newExtGoal);
                    MessageBox.Show(_extGoal.ExtGoalName + " edited successfully.", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    _extGoal = newExtGoal;
                    var pgViewEditGoal = new PgGoalViewEdit(_extGoal, _adminUser, _clientUser);

                    // takes user to the Create Goal form when clicked
                    NavigationService.Navigate(pgViewEditGoal);
                }
                catch (Exception)
                {
                    MessageBox.Show("Save was not successful", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

        }
    }
}