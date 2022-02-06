/// <summary>
/// Ryan Taylor
/// Created: 2021/04/01
///
/// gives functionality to pgPerformanceAddEditView
/// </summary>
/// <remarks>
/// Whitney Vinson
/// Updated: 2021/04/15
/// Added PerformanceEvent functionality.
/// </remarks>
using DataStorageModels;
using DataViewModels;
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
    /// Interaction logic for pgPerformanceAddEditView.xaml
    /// </summary>
    public partial class pgPerformanceAddEditView : Page
    {
        private Performance _performance;
        private List<PerformanceEvent> _performanceEvents;
        private UserAccount _selectedUser;
        private UserAccount _curentUser;
        private bool oldBool;
        private bool newBool;
        private IUserManager _userManager = new UserManager(); 
        private IPerformanceManager _performanceManager = new PerformanceManager();
        private IPerformanceEventManager _performanceEventManager = new PerformanceEventManager();
        public pgPerformanceAddEditView(UserAccount selectedUserClient, UserAccount curentUser)
        {
            _selectedUser = _userManager.GetUserAccountByUsername(selectedUserClient.UserName);
            _curentUser = _userManager.GetUserAccountByUsername(curentUser.UserName);
            InitializeComponent();
        }
        public pgPerformanceAddEditView(Performance performance, 
            UserAccount selectedUserClient, UserAccount currentUser)
        {
            _performance = performance;
            _selectedUser = _userManager.GetUserAccountByUsername(selectedUserClient.UserName);
            _curentUser = _userManager.GetUserAccountByUsername(currentUser.UserName);
            
            InitializeComponent();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// runs code after the page is loaded
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Whitney Vinson
        /// Updated: 2021/04/15
        /// Added viewing Performance Event display
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_performance != null)
            {
                lblDescription.Content = "View Performance";
                if (_performance.Active)
                {
                    btnActivatePerformance.Content = "Deactivate";
                }
                txtPerformanceDescription.IsReadOnly = true;
                txtPerformanceName.IsReadOnly = true;
                txtPerformanceDescription.Text = _performance.PerformanceDescription;
                txtPerformanceName.Text = _performance.PerformanceName;
                _performanceEvents = _performanceEventManager.RetrieveAllPerformanceEventsByPerformanceName(
                _performance.PerformanceName);
                if (_performanceEvents != null)
                {
                    PerformanceEvent performanceEvent = (PerformanceEvent)dgEventList.SelectedItem;
                    dgEventList.ItemsSource = _performanceEvents;

                    dgEventList.Columns.Remove(dgEventList.Columns[0]);
                    dgEventList.Columns.Remove(dgEventList.Columns[1]);
                    dgEventList.Columns[0].Header = "Date of Occurrence";
                    dgEventList.Columns[1].Header = "Event Description";
                    dgEventList.Columns[2].Header = "Event Result";
                    dgEventList.Columns.Remove(dgEventList.Columns[3]);
                    dgEventList.Columns.Remove(dgEventList.Columns[4]);

                }
            }
            else
            {
                btnActivatePerformance.Visibility = Visibility.Hidden;
                lblDescription.Content = "Add Performance";
                btnEditPerformance.Content = "Create Performance";
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// Activates an inactive performance or deactivates an activated performance
        /// </summary>
        private void btnActivatePerformance_Click(object sender, RoutedEventArgs e)
        {
            oldBool = _performance.Active;
            newBool = !_performance.Active;
            try
            {
                _performanceManager.DeactivateReactivatePerformance(_performance.PerformanceName,
                    _performance.UserID_client, oldBool, newBool);
                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }


        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// edits and saves a performance
        /// </summary>
        private void btnEditPerformance_Click(object sender, RoutedEventArgs e)
        {
            //set an if for create
            if ((string)btnEditPerformance.Content == "Create Performance")
            {
                try
                {
                    if (!txtPerformanceDescription.Text.IsValidPerformanceDescription())
                    {
                        MessageBox.Show(txtPerformanceDescription.Text +
                            " is not a valid Performance descriotion (255 characters max)");
                        txtPerformanceDescription.Focus();
                        txtPerformanceDescription.SelectAll();
                        return;
                    }
                    if (!txtPerformanceName.Text.IsValidPerformanceName())
                    {
                        MessageBox.Show(txtPerformanceName.Text +
                            " is not a valid Performance name (50 characters max)");
                        txtPerformanceName.Focus();
                        txtPerformanceName.SelectAll();
                        return;
                    }
                    _performanceManager.CreatePerformance(txtPerformanceName.Text, 
                        txtPerformanceDescription.Text, _selectedUser.UserAccountID,
                        _curentUser.UserAccountID);
                    this.NavigationService.GoBack();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else if ((string)btnEditPerformance.Content == "Edit Performance") 
            {
                lblDescription.Content = "Edit Performance";
                btnEditPerformance.Content = "Save Performance";
                txtPerformanceDescription.IsReadOnly = false;
                txtPerformanceName.IsReadOnly = true;
            }
            else if ((string)btnEditPerformance.Content == "Save Performance") 
            {
                try
                {
                    if (!txtPerformanceDescription.Text.IsValidPerformanceDescription())
                    {
                        MessageBox.Show(txtPerformanceDescription.Text + 
                            " is not a valid Performance descriotion (255 characters max)");
                        txtPerformanceDescription.Focus();
                        txtPerformanceDescription.SelectAll();
                        return;
                    }
                    _performanceManager.EditePerformance(_performance.PerformanceName, 
                        txtPerformanceDescription.Text, _selectedUser.UserAccountID, 
                        _performance.PerformanceDescription, _selectedUser.UserAccountID);
                    this.NavigationService.GoBack();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// sends the user back to view performances
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/04/15
        /// 
        /// Click to view or edit 
        /// Performance Event details.
        /// </summary>
        ///
        /// <remarks>
        /// 
        /// </remarks>

        private void dgEventList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editEvent();
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/04/15
        /// 
        /// Edit Helper Method
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// 
        /// </remarks>
        private void editEvent()
        {
            var selectedItem = (PerformanceEvent)dgEventList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            var pgAddEditViewPerformanceEvent = new pgAddEditViewPerformanceEvent(selectedItem);
            this.NavigationService.Navigate(pgAddEditViewPerformanceEvent);
        }
    }
}
