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
    /// Interaction logic for pgViewIncidents.xaml
    /// </summary>
    public partial class pgViewIncidents : Page
    {
        private IIncidentManager _incidentManager = new IncidentManager();
        private UserAccount _selectedUser;
        private int loggedInUserID = 0;
        private bool active = true;
        private List<Incident> incidentsList = null;
        private DataStorageModels.Incident _incidentEntry;

        public pgViewIncidents(UserAccount selectedUser)
        {
            _selectedUser = selectedUser;
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
        /// Updated: 2021/03/29
        /// Update: Fixed crash related to Exception Handling and provided a more user-friendly error message
        /// </remarks>
        private void PopulateIncidentGrid(UserAccount selectedUser)
        {
            if (active == true)
            {
                try
                {
                    incidentsList = _incidentManager.SelectIncidentsByActive(selectedUser.UserAccountID, active);
                    if (incidentsList.Count == 0)
                    {
                        dgIncidentGrid.ItemsSource = null;
                        return;
                    }
                    else
                    {
                        loggedInUserID = incidentsList[0].UserId_Creator;
                        dgIncidentGrid.ItemsSource = incidentsList;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Incidents could not be found.");
                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();
                }
            }
            else
            {
                try
                {
                    incidentsList = _incidentManager.SelectIncidentsByActive(selectedUser.UserAccountID, active);
                    loggedInUserID = incidentsList[0].UserId_Creator;
                    dgIncidentGrid.ItemsSource = incidentsList;
                }
                catch (Exception)
                {
                    MessageBox.Show("Incidents could not be found.");
                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();
                }
            }
        }

        private void btnNewIncident_Click(object sender, RoutedEventArgs e)
        {
            var addIncident = new pgAddEditIncident(_incidentManager,_selectedUser, loggedInUserID);

            NavigationService.Navigate(addIncident);
        }


        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/03/20
        /// Clicking a single incident and then pressing the edit button will bring up a detailed
        /// view of the incident and allow editting on it.
        /// </summary>
        private void btnEditIncident_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (DataStorageModels.Incident)dgIncidentGrid.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            _incidentEntry = selectedItem;

            var editIncident = new pgAddEditIncident(_incidentEntry);

            NavigationService.Navigate(editIncident);

        }
        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/03/20
        /// Clicking Cancel will bring the user back to the incident selection view.
        /// </summary>

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateIncidentGrid(_selectedUser);
        }


        private void btnActivateDeactiveIncident_Click(object sender, RoutedEventArgs e)
        {

            if (dgIncidentGrid.SelectedItem != null)
            {
                Incident incident = (Incident)dgIncidentGrid.SelectedItem;
                bool changeStatus = incident.Active;
                incident.IncidentEditDate = DateTime.Now;

                if (changeStatus == true)
                {
                    _incidentManager.DeactivateIncident(_selectedUser, incident);
                    lblIncidentsPage.Content = "Inactive Incidents";
                    btnViewIncidents.Content = "View Active Incidents";
                    active = false;
                    PopulateIncidentGrid(_selectedUser);
                }
                else
                {
                    _incidentManager.Reactivateincident(_selectedUser, incident);
                    lblIncidentsPage.Content = "Active Incidents";
                    btnViewIncidents.Content = "View Inactive Incidents";
                    active = true;
                    PopulateIncidentGrid(_selectedUser);
                }
            }

        }

        private void dgIncidentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgIncidentGrid.SelectedItem != null)
            {
                Incident incident = (Incident)dgIncidentGrid.SelectedItem;
                bool changeStatus = incident.Active;
                if (changeStatus == true)
                {
                    btnActivateDeactiveIncident.Content = "Deactivate Incident";
                }
                else
                {
                    btnActivateDeactiveIncident.Content = "Reactivate Incident";
                }
            }
        }

        private void btnViewIncidents_Click(object sender, RoutedEventArgs e)
        {
            if (active == true)
            {
                try
                {
                    incidentsList = _incidentManager.SelectIncidentsByActive(_selectedUser.UserAccountID, false);
                    if (incidentsList.Count == 0)
                    {
                        MessageBox.Show("There are no inactive Incidents");
                    }
                    else
                    {
                        loggedInUserID = incidentsList[0].UserId_Creator;
                        dgIncidentGrid.ItemsSource = incidentsList;
                        lblIncidentsPage.Content = "Inactive Incidents";
                        btnViewIncidents.Content = "View Active Incidents";
                        active = false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Incidents could not be found.");
                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();
                }
            }
            else if (active == false)
            {
                try
                {
                    incidentsList = _incidentManager.SelectIncidentsByActive(_selectedUser.UserAccountID, true);
                    if (incidentsList.Count == 0)
                    {
                        MessageBox.Show("There are no inactive Incidents");
                        
                    }
                    else
                    {
                        loggedInUserID = incidentsList[0].UserId_Creator;
                        lblIncidentsPage.Content = "Active Incidents";
                        btnViewIncidents.Content = "View Inactive Incidents";
                        dgIncidentGrid.ItemsSource = incidentsList;
                        active = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Incidents could not be found.");
                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();
                }
            }
        }


        private void btnViewIncidentEvent_Click(object sender, RoutedEventArgs e)
        {
            var selecteditem = (Incident)dgIncidentGrid.SelectedItem;
            if (selecteditem == null)
            {
                return;
            }
            var IncidentEventAddEditView = new pgIncidentEventView(selecteditem, _selectedUser, loggedInUserID);
            NavigationService.Navigate(IncidentEventAddEditView);
        }
    }
}
