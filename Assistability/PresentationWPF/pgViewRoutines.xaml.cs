using DataStorageModels;
using DataViewModels;
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
    /// Interaction logic for pgViewRoutines.xaml
    /// </summary>
    public partial class pgViewRoutines : Page
    {
        private IRoutineManager _routineManager = new RoutineManager();
        private UserAccount _selectedUser;
        private int loggedInUserID = 0;
        private RoutineVM routines;

        public pgViewRoutines(UserAccount selectedUser, int loggedInUserID)
        {
            _selectedUser = selectedUser;
            this.loggedInUserID = loggedInUserID;
            InitializeComponent();
        }

        /// <summary>
        ///
        /// Created:
        ///
        /// Event handler for Window_Loaded event
        /// </summary>
        ///
        /// <remarks>
        /// Updater: William Clark
        /// Updated: 2021/03/27
        /// Update: Fixed crash related to Exception Handling and provided a more user-friendly error message
        /// </remarks>
        private void onPageLoad(UserAccount selectedUser)
        {
            try
            {
                List<Routine> routines = _routineManager.GetRoutinesByuserID(selectedUser.UserAccountID);
                dgRoutineDisplay.ItemsSource = routines;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Routines could not be found.");
            }
        }

        private void btnNewRoutine_Click(object sender, RoutedEventArgs e)
        {
            var addRoutine = new AddEditDetailRoutine(_routineManager, _selectedUser, loggedInUserID);
            NavigationService.Navigate(addRoutine);

        }

        private void btnEditRoutine_Click(object sender, RoutedEventArgs e)
        {
            RoutineSelected();
        }

        private void RoutineSelected()
        {
            if (dgRoutineDisplay.SelectedItem != null)
            {
                try
                {
                    List<RoutineStep> routineSteps = _routineManager.GetRoutineStepsByRoutine((Routine)dgRoutineDisplay.SelectedItem);
                    routines = new RoutineVM((Routine)dgRoutineDisplay.SelectedItem, routineSteps);
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("The Steps of the routine could not be loaded.");
                }
                var addRoutine = new AddEditDetailRoutine(_routineManager, routines);
                NavigationService.Navigate(addRoutine);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            onPageLoad(_selectedUser);
        }

        private void dgRoutineDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RoutineSelected();
        }
    }
}
