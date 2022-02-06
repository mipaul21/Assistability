/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface to be used to complete routines
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessFakes;
using DataStorageModels;
using DataViewModels;
using LogicInterfaces;
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
    /// Interaction logic for CompleteRoutine.xaml
    /// </summary>
    public partial class CompleteRoutine : Page
    {
        private RoutineVM _routine;
        private IRoutineManager _routineManager;
        private IRoutineStepManager _routineStepManager;
        private int _currentRoutineStep;
        private UserAccount _user;
        private List<int> _completedStepIds;

        public CompleteRoutine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// Constructs an CompleteRoutine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routineManager">The RoutineManager Reference</param>
        /// <param name="routine">The Routine to be completed</param>
        /// <param name="user">The UserAccount which will complete the Routine</param>
        public CompleteRoutine(IRoutineManager routineManager, Routine routine, UserAccount user, List<RoutineStep> routineSteps)
        {
            this._routineManager = routineManager;
            this._user = user;
            this._routine = new RoutineVM(routine, routineSteps);
            _completedStepIds = new List<int>();
            _routineStepManager = new RoutineStepManager();

            RefreshStepCompletions();
            InitializeComponent();
            SetInitialRoutineStep();
            
        }

        private void SetInitialRoutineStep()
        {
            _currentRoutineStep = _routine.Steps.IndexOf(_routine.Steps.First(step => !_completedStepIds.Contains(step.RoutineStepID)));
            if (_currentRoutineStep == 0)
            {
                btnGoBack.Visibility = Visibility.Hidden;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblRoutineName.Content = _routine.Name;
            lblRoutineDescription.Content = _routine.Description;
            UpdateRoutineStepInformation();
        }

        private void UpdateRoutineStepInformation()
        {
            RefreshStepCompletions();
            if (_routine.Steps.Count > 0)
            {
                if ( _completedStepIds.Contains(_routine.Steps[_currentRoutineStep].RoutineStepID))
                {
                    // The routine step has been completed
                    btnCompleteStep.IsEnabled = false;
                    btnCompleteStep.Content = "Complete!";
                    this.Background = Brushes.Green.Clone();
                    this.Background.Opacity = 0.2;
                }
                else
                {
                    if (btnCompleteStep.IsEnabled == false)
                    {
                        btnCompleteStep.IsEnabled = true;
                        btnCompleteStep.Content = "Complete";
                        this.Background = Brushes.White.Clone();
                        this.Background.Opacity = 1;
                    }
                }
                lblRoutineStepName.Content = _routine.Steps[_currentRoutineStep].RoutineStepName;
                lblRoutineStepDescription.Content = _routine.Steps[_currentRoutineStep].RoutineStepDescription;
            }
            else
            {
                MessageBox.Show("This routine doesn't have any steps. \nPlease add some before trying to complete it.");
                this.NavigationService.GoBack();
                this.NavigationService.RemoveBackEntry();
            }
            
        }

        private void RefreshStepCompletions()
        {
            try
            {
                _completedStepIds = _routineStepManager.SelectRoutineStepCompletionsByDayByRoutineName(_routine.Name, DateTime.Now);
            }
            catch (Exception)
            {
                MessageBox.Show("Routine step completions couldn't be loaded.");
            }
        }

        private void btnCompleteStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Attempt to complete the step
                if (_routineManager.CompleteRoutineStep(_routine.Steps[_currentRoutineStep], _user))
                {
                    RefreshStepCompletions();
                    if (_completedStepIds.Count == _routine.Steps.Count) // This should handle the case where all steps have not been completed
                    {
                        HandleCompleteRoutine();
                    }
                    else
                    {
                        // Handle moving to the next step
                        RefreshStepCompletions();
                        if (_currentRoutineStep == _routine.Steps.Count - 1)
                        {
                            SetInitialRoutineStep();
                            UpdateRoutineStepInformation();
                            if (_currentRoutineStep == 0)
                            {
                                btnGoForward.Visibility = Visibility.Visible;
                            }
                            else if (_currentRoutineStep == _routine.Steps.Count - 1)
                            {
                                btnGoBack.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            GoForward();
                            UpdateRoutineStepInformation();
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The routine step could not be completed.");
            }
        }

        private void HandleCompleteRoutine()
        {
            try
            {
                // Handle completing the routine
                if (_routineManager.CompleteRoutine(_routine, _user))
                {
                    MessageBox.Show("Nice work! You completed " + _routine.Name);

                    this.NavigationService.GoBack();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The routine could not be completed.");
            }
        }

        private void GoForward()
        {
            if (_currentRoutineStep == _routine.Steps.Count - 2)
            {
                btnGoForward.Visibility = Visibility.Hidden;
                _currentRoutineStep += 1;
                UpdateRoutineStepInformation();
            }
            else
            {
                if (btnGoBack.Visibility == Visibility.Hidden)
                {
                    btnGoBack.Visibility = Visibility.Visible;
                }
                _currentRoutineStep += 1;
                UpdateRoutineStepInformation();
            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (_currentRoutineStep == 1)
            {
                btnGoBack.Visibility = Visibility.Hidden;
                _currentRoutineStep -= 1;
                UpdateRoutineStepInformation();
            }
            else
            {
                if (btnGoForward.Visibility == Visibility.Hidden)
                {
                    btnGoForward.Visibility = Visibility.Visible;
                }
                _currentRoutineStep -= 1;
                UpdateRoutineStepInformation();
            }
        }

        private void btnGoForward_Click(object sender, RoutedEventArgs e)
        {
            GoForward();
        }
    }
}
