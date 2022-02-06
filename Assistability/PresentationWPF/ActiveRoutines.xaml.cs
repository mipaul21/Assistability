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
using LogicLayer;
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

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for ActiveRoutines.xaml
    /// </summary>
    public partial class ActiveRoutines : Page
    {
        private RoutineManager _routineManager;
        private UserAccountVM _user;
        private List<Routine> _routineList;

        public ActiveRoutines()
        {
            InitializeComponent();
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Constructs an ActiveRoutines page
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routineManager">The RoutineManager Reference</param>
        /// <param name="selectedUser">The UserAccountVM for which to view active routines</param>
        public ActiveRoutines(RoutineManager routineManager, UserAccountVM selectedUser)
        {
            _routineManager = new RoutineManager();
            _user = selectedUser;
            InitializeComponent();
        }

        private void PopulateRoutineList()
        {
            try
            {
                _routineList = _routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime.Now, _user.UserAccountID);
                if (_routineList.Count == 0)
                {
                    MessageBox.Show("You've completed all of your routines!\nNice Work!");
                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();
                }
                else if (_routineList.Count == 1)
                {
                    HandleCompleteRoutine(_routineList[0]);
                }
                else
                {
                    dgActiveRoutines.ItemsSource = _routineList;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Routines could not be found.");
                NavigationService.GoBack();
                NavigationService.RemoveBackEntry();
            }
        }

        private void dgActiveRoutines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Routine routine = (Routine)dgActiveRoutines.SelectedItem;
            
            if (routine != null)
            {
                HandleCompleteRoutine(routine);
            }
        }

        private void HandleCompleteRoutine(Routine routine)
        {
            List<RoutineStep> routineSteps = new List<RoutineStep>();
            try
            {
                routineSteps = _routineManager.GetRoutineStepsByRoutine(routine).Where(step => step.Active == true).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("The Routine Steps could not be found.");
            }
            if (routineSteps.Count == 0)
            {
                MessageBox.Show("This routine doesn't have any steps. \nPlease add some before trying to complete it.");
                NavigationService.GoBack();
                NavigationService.RemoveBackEntry();
            }
            else
            {
                this.NavigationService.Navigate(new CompleteRoutine(_routineManager, routine, _user, routineSteps));
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateRoutineList();
        }
    }
}
