/// <summary>
/// William Clark
/// Created: 2021/02/18
/// 
/// Interface for viewing of Routines
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessFakes;
using DataAccessLayer;
using DataStorageModels;
using DataViewModels;
using LogicInterfaces;
using LogicLayer;
using LogicLayerInterfaces;
using PresentationHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for AddEditDetailRoutine.xaml
    /// </summary>
    public partial class AddEditDetailRoutine : Page
    {
        private RoutineVM _routine;
        private IRoutineManager _routineManager;
        private IRoutineStepManager _routineStepManager;
        private bool _addRoutine = true;
        private bool _active = true;
        private UserAccount _selectedUser;
        private int _loggedInUserId;


        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Constructer that accepts an IRoutineManager and a RoutineVM
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="routineManager">The IRoutineManager Logic Layer class</param>
        /// <param name="selectedRoutine">The RoutineVM for which to view details</param>
        public AddEditDetailRoutine(IRoutineManager routineManager, RoutineVM selectedRoutine)
        {
            // USING FAKE
            //_routine = selectedRoutine;
            _routineManager = routineManager;
            _routineStepManager = new RoutineStepManager();
            _addRoutine = false;
            _routine = selectedRoutine;
            InitializeComponent();
        }

        public AddEditDetailRoutine(IRoutineManager routineManager, UserAccount selectedUser, int loggedInUserID)
        {
            _selectedUser = selectedUser;
            _loggedInUserId = loggedInUserID;
            _routineManager = routineManager;
            InitializeComponent();
        }


        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// On page load, populate form
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void Page_Initialized(object sender, EventArgs e)
        {
            PopulatePage();
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// 
        /// Populates the form with the Routine data
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void PopulatePage()
        {
            if (_addRoutine == true)
            {
                txtRoutineName.Text = "";
                txtRoutineDescription.Text = "";
                txtEntryDate.IsReadOnly = true;
                txtEditDate.IsReadOnly = true;
                txtRemovalDate.IsReadOnly = true;
                txtRoutineName.BorderBrush = Brushes.Black;
                txtRoutineDescription.BorderBrush = Brushes.Black;
                btnEditRoutineDescription.Visibility = Visibility.Hidden;
                btnActive.Visibility = Visibility.Hidden;
                dgRoutineSteps.Visibility = Visibility.Hidden;
                btnAddRoutineStep.Visibility = Visibility.Hidden;
                btnMoveStepOrderUp.Visibility = Visibility.Hidden;
                btnMoveStepOrderDown.Visibility = Visibility.Hidden;
                txtRoutineName.Focus();
            }
            else
            {
                _addRoutine = false;
                txtRoutineName.Text = _routine.Name;
                txtRoutineName.IsReadOnly = true;
                txtRoutineDescription.Text = _routine.Description;
                txtRoutineDescription.IsReadOnly = true;
                btnEditRoutineDescription.Visibility = Visibility.Visible;
                txtEntryDate.Text = _routine.EntryDate.ToString();
                if (_routine.EditDate != null)
                {
                    lblEditDate.Visibility = Visibility.Visible;
                    txtEditDate.Visibility = Visibility.Visible;
                    txtEditDate.Text = _routine.EditDate.ToString();
                }
                else
                {
                    lblEditDate.Visibility = Visibility.Hidden;
                    txtEditDate.Visibility = Visibility.Hidden;
                }
                if (_routine.RemovalDate != null)
                {
                    lblRemovalDate.Visibility = Visibility.Visible;
                    txtRemovalDate.Visibility = Visibility.Visible;
                    txtRemovalDate.Text = _routine.RemovalDate.ToString();
                }
                else
                {
                    lblRemovalDate.Visibility = Visibility.Hidden;
                    txtRemovalDate.Visibility = Visibility.Hidden;
                }
                btnActive.Visibility = Visibility.Hidden;
                btnSaveRoutineDescription.Visibility = Visibility.Hidden;
                ShowDeactivateReactivateButton(_routine.Active);
                btnMoveStepOrderUp.Visibility = Visibility.Hidden;
                btnMoveStepOrderDown.Visibility = Visibility.Hidden;
                
                RefreshRoutineSteps();
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Refreshes the Routines RoutineSteps list
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void RefreshRoutineSteps()
        {
            try
            {
                _routine.Steps = _routineManager.GetRoutineStepsByRoutine(_routine);
                dgRoutineSteps.ItemsSource = _routine.Steps;
                dgRoutineSteps.Items.SortDescriptions.Clear();
                dgRoutineSteps.Items.SortDescriptions.Add(new SortDescription("RoutineStepOrderNumber", ListSortDirection.Ascending));
                dgRoutineSteps.Items.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("The Steps of the routine could not be loaded.");
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Shows the Deactivate/Reactivate button depending on the Active status of the Routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void ShowDeactivateReactivateButton(bool active)
        {
            if (active)
            {
                btnActive.Visibility = Visibility.Visible;
                btnActive.Content = "Deactivate";
            }
            else
            {
                btnActive.Visibility = Visibility.Visible;
                btnActive.Content = "Reactivate";
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Updates interface to allow updating of Routine Description
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void btnEditRoutineDescription_Click(object sender, RoutedEventArgs e)
        {
            txtRoutineDescription.IsReadOnly = false;
            btnSaveRoutineDescription.Visibility = Visibility.Visible;
            btnEditRoutineDescription.Visibility = Visibility.Hidden;
            txtRoutineDescription.Clear();
            txtRoutineDescription.Focus();
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Updates the current routine in the data store, then updates the interface to reflect the changes
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void btnSaveRoutineDescription_Click(object sender, RoutedEventArgs e)
        {
            if (_addRoutine == true)
            {
                if (!txtRoutineName.Text.IsValidRoutineName())
                {
                    MessageBox.Show("Please enter a valid routine name.");
                    txtRoutineName.Focus();
                    txtRoutineName.SelectAll();
                    return;

                }

                if (!txtRoutineDescription.Text.IsValidRoutineDescription())
                {
                    MessageBox.Show("Please enter a valid routine Description.");
                    txtRoutineDescription.Focus();
                    txtRoutineDescription.SelectAll();
                    return;

                }
                Routine newRoutine = new Routine(txtRoutineName.Text, txtRoutineDescription.Text, _selectedUser.UserAccountID, _loggedInUserId, _active, DateTime.Now, null, null);
                try
                {
                    _routineManager.AddNewRoutine(newRoutine);

                }
                catch (Exception)
                {
                    MessageBox.Show("The Routine could not be added.");
                }
                NavigationService.GoBack();


            }
            else
            {
                Routine newRoutine = new Routine(txtRoutineName.Text, txtRoutineDescription.Text, _routine.UserAccountID_Client, _routine.UserAccountID_Admin, _routine.Active, _routine.EntryDate, DateTime.Now, null);

                try
                {
                    if (_routineManager.UpdateRoutine(_routine, newRoutine))
                    {
                        try
                        {
                            _routine = new RoutineVM(newRoutine, _routineManager.GetRoutineStepsByRoutine(newRoutine));

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("The Steps of the routine could not be loaded.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("The Routine could not be updated.");
                    }
                    PopulatePage();

                }
                catch (Exception)
                {
                    MessageBox.Show("The routine could not be updated.");
                }
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Deactivates/Reactivates Routine in the data store
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void btnActive_Click(object sender, RoutedEventArgs e)
        {

            switch (btnActive.Content)
            {
                case "Deactivate":
                    try
                    {
                        Routine newRoutine = new Routine(txtRoutineName.Text, txtRoutineDescription.Text, _routine.UserAccountID_Client, _routine.UserAccountID_Admin, false, _routine.EntryDate, _routine.EditDate, DateTime.Now);
                        _routine = new RoutineVM(newRoutine, _routineManager.GetRoutineStepsByRoutine(newRoutine));
                        PopulatePage();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Routine could not be deactivated.");
                    }
                    break;
                case "Reactivate":
                    try
                    {
                        Routine newRoutine = new Routine(txtRoutineName.Text, txtRoutineDescription.Text, _routine.UserAccountID_Client, _routine.UserAccountID_Admin, true, _routine.EntryDate, _routine.EditDate, null);
                        _routine = new RoutineVM(newRoutine, _routineManager.GetRoutineStepsByRoutine(newRoutine));
                        PopulatePage();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Routine could not be reactivated.");
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnAddRoutineStep_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pgAddEditDetailRoutineStep(_routineManager, _routineStepManager, _routine));
        }

        private void btnMoveStepOrderUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoutineStep stepMovingBack = (RoutineStep)dgRoutineSteps.Items[dgRoutineSteps.Items.IndexOf((RoutineStep)dgRoutineSteps.SelectedItem) - 1];
                RoutineStep stepMovingForward = (RoutineStep)dgRoutineSteps.SelectedItem;

                _routineManager.SwapRoutineStepOrder(stepMovingBack, stepMovingForward);
                btnMoveStepOrderUp.Visibility = Visibility.Hidden;
                btnMoveStepOrderDown.Visibility = Visibility.Hidden;
                RefreshRoutineSteps();

            }
            catch (Exception)
            {
                MessageBox.Show("Routine Step could not be updated.");
            }
        }

        private void btnMoveStepOrderDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoutineStep stepMovingBack = (RoutineStep)dgRoutineSteps.SelectedItem;
                RoutineStep stepMovingForward = (RoutineStep)dgRoutineSteps.Items[dgRoutineSteps.Items.IndexOf((RoutineStep)dgRoutineSteps.SelectedItem) + 1];

                _routineManager.SwapRoutineStepOrder(stepMovingBack, stepMovingForward);
                btnMoveStepOrderUp.Visibility = Visibility.Hidden;
                btnMoveStepOrderDown.Visibility = Visibility.Hidden;
                RefreshRoutineSteps();

            }
            catch (Exception)
            {
                MessageBox.Show("Routine Step could not be updated.");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_addRoutine)
            {
                RefreshRoutineSteps();

            }
            
        }

        private void dgRoutineSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoutineStep step = (RoutineStep)dgRoutineSteps.SelectedItem;
            if (step != null)
            {
                btnMoveStepOrderUp.Visibility = Visibility.Visible;
                btnMoveStepOrderDown.Visibility = Visibility.Visible;
                if (step.RoutineStepOrderNumber == 1)
                {
                    btnMoveStepOrderUp.Visibility = Visibility.Hidden;
                    btnMoveStepOrderDown.Visibility = Visibility.Visible;
                }
                if (step.RoutineStepOrderNumber == _routine.Steps.Count)
                {
                    btnMoveStepOrderUp.Visibility = Visibility.Visible;
                    btnMoveStepOrderDown.Visibility = Visibility.Hidden;
                }
            }
            
        }

        private void dgRoutineSteps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgRoutineSteps.SelectedItem != null)
            {
                try
                {
                    RoutineStep selectedStep = (RoutineStep)dgRoutineSteps.SelectedItem;
                    var editStep = new pgAddEditDetailRoutineStep(_routineManager, _routineStepManager, _routine, selectedStep);
                    NavigationService.Navigate(editStep);
                }
                catch (Exception)
                {
                    MessageBox.Show("The Steps could not be loaded.");
                }
            }
        }
    }
}
