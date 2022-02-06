/// <summary>
/// Ryan Taylor
/// Created: 2021/04/01
///
/// gives functionality to pgViewPerformance
/// </summary>
using DataStorageModels;
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
    /// Interaction logic for pgViewPerformances.xaml
    /// </summary>
    public partial class pgViewPerformances : Page
    {
        private UserAccount _selectedUser;
        private UserAccount _curentUser;
        private IPerformanceManager _performanceMangager = new PerformanceManager();
        private bool _updatePerformances = false;
        public pgViewPerformances(UserAccount selectedUser, UserAccount curentUser)
        {
            _selectedUser = selectedUser;
            _curentUser = curentUser;
            InitializeComponent();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// runs code after the page is loaded
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _updatePerformances = true;
            updateDgPerformances();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// updates the dgPerformances when called
        /// </summary>
        private void updateDgPerformances()
        {
            try
            {
                if (dgPerformanceList.ItemsSource == null || _updatePerformances == true)
                {
                    dgPerformanceList.ItemsSource =
                        _performanceMangager.RetrievePerformancesByClientAndActive(
                        _selectedUser.UserAccountID, (bool)chkActive.IsChecked);

                    dgPerformanceList.Columns[0].Header = "Performance Name";
                    dgPerformanceList.Columns[1].Header = "Performance Description";
                    dgPerformanceList.Columns.Remove(dgPerformanceList.Columns[7]);
                    dgPerformanceList.Columns.Remove(dgPerformanceList.Columns[6]);
                    dgPerformanceList.Columns.Remove(dgPerformanceList.Columns[5]);
                    dgPerformanceList.Columns.Remove(dgPerformanceList.Columns[4]);
                    dgPerformanceList.Columns.Remove(dgPerformanceList.Columns[3]);
                    dgPerformanceList.Columns.Remove(dgPerformanceList.Columns[2]);

                    _updatePerformances = false;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("reference not set to"))
                {
                    MessageBox.Show(_selectedUser.FirstName + " " + _selectedUser.LastName + 
                        " has no performances on record.", "No Performances");
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// alows the user to create a performance.
        /// </summary>
        private void btnCreatePerformance_Click(object sender, RoutedEventArgs e)
        {
            var PerfomanceAddEditView = new pgPerformanceAddEditView(_selectedUser, _curentUser);
            this.NavigationService.Navigate(PerfomanceAddEditView);
            _updatePerformances = true;
            updateDgPerformances();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// gives the user more detial about the selected performance.
        /// </summary>
        private void dgPerformanceList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Performance)dgPerformanceList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            var PerfomanceAddEditView = new pgPerformanceAddEditView(selectedItem, _selectedUser,
                _curentUser);
            this.NavigationService.Navigate(PerfomanceAddEditView);
            _updatePerformances = true;
            updateDgPerformances();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// used to constantly see the value of chkActive to show 
        /// the user shat kind of perfomances they want to see.
        /// </summary>
        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            _updatePerformances = true;
            updateDgPerformances();
        }
    }
}
