///<summary>
///Becky Baenziger
///2021/02/18
/// ///
/// Page for frmCreateGoal which will all the user to create a goal and either submit
/// cancel.
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
using LogicLayerInterfaces;
using DataStorageModels;
using PresentationHelpers;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for PgfrmCreateGoal.xaml
    /// </summary>
    public partial class PgfrmCreateGoal : Page
    {
        private IHabGoalManager _habGoalManager = new HabGoalManager();
        private IAttGoalManager _attGoalManager = new AttGoalManager();
        private IExtGoalManager _extGoalManager = new ExtGoalManager();
        private UserAccount _clientUser = new UserAccount();
        private UserAccount _adminUser = new UserAccount();
        private AwardManager awardManager = new AwardManager();
        private RoutineManager routineManager = new RoutineManager();
        private PerformanceManager performanceManager = new PerformanceManager();
        private IncidentManager incidentManager = new IncidentManager();
        private int days;
        private int frequency;

        public PgfrmCreateGoal(UserAccount controllingUser, UserAccount selectedUser)
        {
            _clientUser = selectedUser;
            _adminUser = controllingUser;

            InitializeComponent();
        }

        public PgfrmCreateGoal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Method submits the entered data, storing in database
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // data validation

            if (!cboGoalType.Text.IsValidGoalType())
            {
                MessageBox.Show("Make a Goal Type Selection.");
                cboGoalType.Focus();
                return;
            }

            if (!txtGoalName.Text.IsValidGoalName())
            {
                MessageBox.Show("Goal Name must be between 1 and 50 characters long.");
                txtGoalName.Focus();
                txtGoalName.SelectAll();
            }

            if (!txtGoalDescription.Text.IsValidGoalDescription())
            {
                MessageBox.Show("Goal Name must be between 1 and 500 characters long.");
                txtGoalDescription.Focus();
            }
            
            if(!int.TryParse(txtGoalSubtypeFrequency.Text, out frequency))
            {
                MessageBox.Show("The Frequency must be a number");
                return;
            }

            if(cboDaysWeeks.SelectedItem.ToString() == "Day(s)")
            {
                days = int.Parse(cboNumberofDays.Text);
            }
            else
            {
                days = int.Parse(cboNumberofDays.Text) * 7;
            }

            // assign form data
            switch (cboGoalType.SelectedIndex)
            {
                case (1):
                    var newHabGoal = new HabGoalViewModel()
                    {
                        UserID_client = _clientUser.UserAccountID,
                        UserID_admin = _adminUser.UserAccountID,
                        HabGoalName = txtGoalName.Text,
                        HabGoalDescription = txtGoalDescription.Text,
                        HabGoalTargetDate = DateTime.Today.AddDays(days),
                        HabGoalEntryDate = DateTime.Today,
                        RoutineFrequency = frequency,
                        AwardName = cboGoalAward.SelectedItem.ToString(),
                        RoutineName = cboGoalRoutine.SelectedItem.ToString()

                    };

                    // add new goal, return error if doenst work
                    try
                    {
                        _habGoalManager.AddHabitualGoal(newHabGoal);

                        var pgGoals = new PgGoals(_adminUser, _clientUser);

                        NavigationService.Navigate(pgGoals);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Goal was not saved.", "Invalid Operation",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    break;

                case (2):
                    var newAttGoal = new AttGoalViewModel()
                    {
                        UserID_client = _clientUser.UserAccountID,
                        UserID_admin = _adminUser.UserAccountID,
                        AttGoalName = txtGoalName.Text,
                        AttGoalDescription = txtGoalDescription.Text,
                        AttGoalTargetDate = DateTime.Today.AddDays(days),
                        AttGoalEntryDate = DateTime.Today,
                        PerformanceFrequency = frequency,
                        AwardName = cboGoalAward.SelectedItem.ToString(),
                        PerformanceName = cboGoalRoutine.SelectedItem.ToString()
                    };
                    // add new goal, return error if doenst work
                    try
                    {
                        _attGoalManager.AddAttainmentGoal(newAttGoal);

                        var pgGoals = new PgGoals(_adminUser, _clientUser);

                        NavigationService.Navigate(pgGoals);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Goal was not saved.", "Invalid Operation",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;

                case (3):
                    var newExtGoal = new ExtGoalViewModel()
                    {
                        UserID_client = _clientUser.UserAccountID,
                        UserID_admin = _adminUser.UserAccountID,
                        ExtGoalName = txtGoalName.Text,
                        ExtGoalDescription = txtGoalDescription.Text,
                        ExtGoalTargetDate = DateTime.Today.AddDays(days),
                        ExtGoalEntryDate = DateTime.Today,
                        IncidentFrequency = frequency,
                        AwardName = cboGoalAward.SelectedItem.ToString(),
                        IncidentName = cboGoalRoutine.SelectedItem.ToString()
                    };
                    // add new goal, return error if doenst work
                    try
                    {
                        _extGoalManager.AddExtinctionGoal(newExtGoal);

                        var pgGoals = new PgGoals(_adminUser, _clientUser);

                        NavigationService.Navigate(pgGoals);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Goal was not saved.", "Invalid Operation",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;

                default:
                    MessageBox.Show("Select a Goal Type.", "Invalid Operation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Method returns the user to the Goals page when the button btnCancel
        /// is clicked
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
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
        /// Created: 2021/02/18
        /// ///
        /// Method handles selection change in the combo box cboGoalType
        /// altering the page based on selection.
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGoalType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (cboGoalType.SelectedIndex)
            {
                case (1):
                    lblGoalTypeDescrip.Content = "** Helps form habits through routines.";
                    lblGoalRoutine.Content = "Routine Name";
                    lblGoalSubTypeFrequency.Content = "Routine Frequency";
                    // lblGoalTargetDate.Content = "Complete " + txtGoalSubtypeFrequency + " routines by " + DateTime.Today.AddDays((int)cboNumberofDays.SelectedItem) + ".";

                    // populate the routines cboGoalRoutine
                    List<Routine> routines = routineManager.SelectActiveRoutinesByUserAccountIDClient(_clientUser.UserAccountID);
                    List<string> routineNames = routines.OrderBy(r => r.Name).Select(r => r.Name).ToList();
                    cboGoalRoutine.ItemsSource = routineNames;
                    break;

                case (2):
                    lblGoalTypeDescrip.Content = "** Works to reward positive events.";
                    lblGoalRoutine.Content = "Performance Name";
                    lblGoalSubTypeFrequency.Content = "Performance Frequency";
                    // lblGoalTargetDate.Content = "Complete " + txtGoalSubtypeFrequency + " performances or more by " + DateTime.Today.AddDays((int)cboNumberofDays.SelectedItem) + ".";

                    // populate the performance option in cboGoalRoutine
                    List<Performance> performances = performanceManager.RetrievePerformancesByClientAndActive(_clientUser.UserAccountID, true);
                    List<string> performanceNames = performances.OrderBy(p => p.PerformanceName).Select(p => p.PerformanceName).ToList();
                    cboGoalRoutine.ItemsSource = performanceNames;
                    break;

                case (3):
                    lblGoalTypeDescrip.Content = "** Works towards eliminating events.";
                    lblGoalRoutine.Content = "Incident Name";
                    lblGoalSubTypeFrequency.Content = "Incident Frequency";
                    // lblGoalTargetDate.Content = "Have no more than " + txtGoalSubtypeFrequency + " incidents by " + DateTime.Today.AddDays((int)cboNumberofDays.SelectedItem) + ".";

                    
                    // populate the incident options in cboGoalRoutine
                    List<Incident> incidents = incidentManager.SelectIncidentsByActive(_clientUser.UserAccountID, true);
                    List<string> incidentNames = incidents.OrderBy(i => i.IncidentName).Select(i => i.IncidentName).ToList();
                    cboGoalRoutine.ItemsSource = incidentNames;
                    
                    break;
                    

                default:
                    lblGoalTypeDescrip.Content = "** Please select a goal type.";
                    lblGoalRoutine.Content = "";
                    lblGoalSubTypeFrequency.Content = "";
                    lblGoalTargetDate.Content = "";
                    break;

            }


        }

        /// <summary>
        /// Becky Baenziger
        /// Created 2021/04/16
        /// ///
        /// Sets whats in the page when it loads, Fills cboAwards and cbo Routines
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // populate the awards cboGoalAward
            List<Award> awards = awardManager.RetreiveAllAwards();
            List<string> awardNames = awards.OrderBy(a=> a.AwardName).Select(a => a.AwardName).ToList();
            cboGoalAward.ItemsSource = awardNames;

            
        }

    }
}
