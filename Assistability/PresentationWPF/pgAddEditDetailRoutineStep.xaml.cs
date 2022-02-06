/// <summary>
/// William Clark
/// Created: 2021/03/30
/// 
/// Interface for viewing of RoutineSteps
/// </summary>
///
/// <remarks>
/// </remarks>
using DataStorageModels;
using DataViewModels;
using LogicInterfaces;
using LogicLayer;
using LogicLayerInterfaces;
using PresentationHelpers;
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
    /// Interaction logic for pgAddEditDetailRoutineStep.xaml
    /// </summary>
    public partial class pgAddEditDetailRoutineStep : Page
    {
        private RoutineVM _routine;
        private IRoutineManager _routineManager;
        private RoutineStep _routineStep;
        private IRoutineStepManager _routineStepManager;
        private bool _addRoutineStep;

        /// <summary>
        /// William Clark
        /// Created: 2021/03/30
        /// 
        /// Constructer that accepts an IRoutineManager, a RoutineVM, and a RoutineStep
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="routineManager">The IRoutineManager Logic Layer class</param>
        /// <param name="selectedRoutine">The RoutineVM to which the step belongs</param>
        /// <param name="selectedStep">The Step for which to view details</param>
        public pgAddEditDetailRoutineStep(IRoutineManager routineManager, IRoutineStepManager routineStepManager, RoutineVM selectedRoutine, RoutineStep selectedStep)
        {
            _routine = selectedRoutine;
            _routineManager = routineManager;
            _routineStep = selectedStep;
            _routineStepManager = routineStepManager;
            _addRoutineStep = false;
            InitializeComponent();
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/30
        /// 
        /// Constructer that accepts an IRoutineManager, a RoutineVM, and a RoutineStep
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="routineManager">The IRoutineManager Logic Layer class</param>
        /// <param name="selectedRoutine">The RoutineVM to which the step belongs</param>
        /// <param name="selectedStep">The Step for which to view details</param>
        public pgAddEditDetailRoutineStep(IRoutineManager routineManager, IRoutineStepManager routineStepManager, RoutineVM selectedRoutine)
        {
            _routine = selectedRoutine;
            _routineManager = routineManager;
            _routineStep = new RoutineStep();
            _addRoutineStep = true;
            _routineStepManager = routineStepManager;
            InitializeComponent();
        }

        private void btnEditRoutineStep_Click(object sender, RoutedEventArgs e)
        {
            txtRoutineStepName.IsReadOnly = false;
            txtRoutineStepDescription.IsReadOnly = false;
            btnSaveRoutineStep.Visibility = Visibility.Visible;
            btnEditRoutineStep.Visibility = Visibility.Hidden;
            txtRoutineStepName.Clear();
            txtRoutineStepDescription.Clear();
            txtRoutineStepName.Focus();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addRoutineStep)
            {
                txtRoutineStepName.Text = "";
                txtRoutineStepDescription.Text = "";
                txtEntryDate.IsReadOnly = true;
                txtEditDate.IsReadOnly = true;
                txtRemovalDate.IsReadOnly = true;
                txtRoutineStepName.BorderBrush = Brushes.Black;
                txtRoutineStepDescription.BorderBrush = Brushes.Black;
                btnEditRoutineStep.Visibility = Visibility.Hidden;
            }
            else
            {
                txtRoutineStepName.Text = _routineStep.RoutineStepName;
                txtRoutineStepName.IsReadOnly = true;
                txtRoutineStepDescription.Text = _routineStep.RoutineStepDescription;
                txtRoutineStepDescription.IsReadOnly = true;
                btnEditRoutineStep.Visibility = Visibility.Visible;
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
                btnSaveRoutineStep.Visibility = Visibility.Hidden;
            }
        }

        private void btnSaveRoutineStep_Click(object sender, RoutedEventArgs e)
        {
            if (_addRoutineStep)
            {
                if (!txtRoutineStepName.Text.IsValidRoutineName())
                {
                    MessageBox.Show("Please enter a valid step name.");
                    txtRoutineStepName.Focus();
                    txtRoutineStepName.SelectAll();
                    return;

                }

                if (!txtRoutineStepDescription.Text.IsValidRoutineDescription())
                {
                    MessageBox.Show("Please enter a valid step description.");
                    txtRoutineStepDescription.Focus();
                    txtRoutineStepDescription.SelectAll();
                    return;

                }
                RoutineStep newRoutineStep = new RoutineStep(_routine.Name, txtRoutineStepName.Text, txtRoutineStepDescription.Text, DateTime.Now, _routine.Steps.Count + 1, true);
                try
                {
                    // store step
                    _routineStepManager.AddNewRoutineStep(new RoutineStepViewModel(newRoutineStep));
                    MessageBox.Show("Step Added!");
                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The step could not be added.");
                }

            }
            else
            {
                RoutineStep newRoutineStep = new RoutineStep(_routineStep.RoutineStepID, _routine.Name, txtRoutineStepName.Text, txtRoutineStepDescription.Text, _routineStep.RoutineStepEntryDate, DateTime.Now, _routineStep.RoutineStepRemovalDate, _routineStep.RoutineStepOrderNumber, true);

                try
                {
                    _routineManager.UpdateRoutineStep(_routineStep, newRoutineStep);
                    MessageBox.Show("Changes Saved!");
                    this.NavigationService.GoBack();
                    this.NavigationService.RemoveBackEntry();
                }
                catch (Exception)
                {
                    MessageBox.Show("The step could not be updated.");
                }
            }
        }
    }
}
