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
    /// Interaction logic for pgIncidentEventView.xaml
    /// </summary>
    public partial class pgIncidentEventView : Page
    {
        private UserAccount _selectedUser;
        private Incident _selectedItem;
        private int _loggedInUserId;
        private IIncidentEventManager _incidentEventManager = new IncidentEventManager();
        private List<IncidentEvent> incidentEventList = null;
        private bool updateIncidentEvent = true;

        public pgIncidentEventView(Incident selecteditem, UserAccount selectedUser, int loggedInUserID)
        {
            _selectedItem = selecteditem;
            _selectedUser = selectedUser;
            _loggedInUserId = loggedInUserID;
            InitializeComponent();
            PopulateIncidentEventPage(selecteditem);
        }

        private void PopulateIncidentEventPage(Incident selecteditem)
        {
            txtIncidentName.Text = selecteditem.IncidentName;
            txtIncidentName.IsReadOnly = true;
            txtIncidentDescription.Text = selecteditem.IncidentDescription;
            txtIncidentDescription.IsReadOnly = true;
            try
            {
                incidentEventList = _incidentEventManager.SelectIncidentsEventsByIncidentName(_selectedItem.IncidentName);
                if (incidentEventList == null)
                {
                    MessageBox.Show("There are currently no incident events for this incident.");
                }
                dgIncidentEventList.ItemsSource = incidentEventList;
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void btnCreateIncidentEvent_Click(object sender, RoutedEventArgs e)
        {
            var IncidentEventAddEditView = new pgIncidentEventAddEdit(_selectedItem , _selectedUser, _loggedInUserId);
            NavigationService.Navigate(IncidentEventAddEditView);
        }

        private void btnEditIncidentEvent_Click(object sender, RoutedEventArgs e)
        {
            var _selectedIncidentEvent = (DataStorageModels.IncidentEvent)dgIncidentEventList.SelectedItem;
            if (_selectedIncidentEvent == null)
            {
                return;
            }
            var IncidentEventAddEditView = new pgIncidentEventAddEdit(_selectedItem, _selectedUser, _loggedInUserId, updateIncidentEvent, _selectedIncidentEvent);
            NavigationService.Navigate(IncidentEventAddEditView);
        }
    }
}
