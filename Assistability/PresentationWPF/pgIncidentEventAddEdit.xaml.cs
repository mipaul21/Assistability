using DataStorageModels;
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
    /// Interaction logic for pgIncidentEventAddEdit.xaml
    /// </summary>
    public partial class pgIncidentEventAddEdit : Page
    {
        private UserAccount _selectedUser;
        private Incident _selectedItem;
        private int _loggedInUserId;
        private IIncidentEventManager _incidentEventManager = new IncidentEventManager();
        private List<IncidentEvent> incidentEventList = null;
        private bool _updateIncidentEvent;
        private IncidentEvent oldIncidentEvent;

        public pgIncidentEventAddEdit(Incident selecteditem, UserAccount selectedUser, int loggedInUserID)
        {
            _selectedItem = selecteditem;
            _selectedUser = selectedUser;
            _loggedInUserId = loggedInUserID;
            InitializeComponent();
        }

        public pgIncidentEventAddEdit(Incident selecteditem, UserAccount selectedUser, int loggedInUserID, bool updateIncidentEvent, IncidentEvent _selectedIncidentEvent)
        {
            _selectedItem = selecteditem;
            _selectedUser = selectedUser;
            _loggedInUserId = loggedInUserID;
            _updateIncidentEvent = updateIncidentEvent;
            oldIncidentEvent = _selectedIncidentEvent;
            InitializeComponent();
            setUpEdit();
        }

        private void setUpEdit()
        {
            calendar.SelectedDate = oldIncidentEvent.DateOfOccurence;
            txtPersonsInvolved.Text = oldIncidentEvent.PersonsInvolved;
            txtEventDescription.Text = oldIncidentEvent.EventDescription;
            txtEventConsequence.Text = oldIncidentEvent.EventConsequence;
            btnAddNewIncidentEvent.Content = "Save";
            lblAddNewIncidentEvent.Content = "Edit Incident Event";
        }

        private void btnAddNewIncidentEvent_Click(object sender, RoutedEventArgs e)
        {
            if (_updateIncidentEvent != true)
            {
                if (!txtPersonsInvolved.Text.IsValidPersonsInvolved())
                {
                    MessageBox.Show("Please enter a shorter value for persons involved.");
                    txtPersonsInvolved.Focus();
                    txtPersonsInvolved.SelectAll();
                    return;
                }

                if (!txtEventConsequence.Text.IsValidEventConsquence())
                {
                    MessageBox.Show("Please enter a shorter event consequence.");
                    txtEventConsequence.Focus();
                    txtEventConsequence.SelectAll();
                    return;
                }

                if (!txtEventDescription.Text.IsValidEventDescription())
                {
                    MessageBox.Show("Please enter a shorter event description.");
                    txtEventDescription.Focus();
                    txtEventDescription.SelectAll();
                    return;
                }
                if (calendar.SelectedDate == null)
                {
                    MessageBox.Show("Please enter an event date.");
                    calendar.Focus();
                    return;
                }
                IncidentEvent incidentEvent = new IncidentEvent(_selectedItem.IncidentName, calendar.SelectedDate, txtPersonsInvolved.Text, txtEventDescription.Text, txtEventConsequence.Text, null, _selectedUser.UserAccountID, _loggedInUserId);

                try
                {
                    _incidentEventManager.InsertNewIncidentEvent(incidentEvent);

                }
                catch (Exception)
                {
                    MessageBox.Show("The Incident event could not be added.");
                }
                NavigationService.GoBack();
            }
            else
            {
                if (!txtPersonsInvolved.Text.IsValidPersonsInvolved())
                {
                    MessageBox.Show("Please enter a shorter value for persons involved.");
                    txtPersonsInvolved.Focus();
                    txtPersonsInvolved.SelectAll();
                    return;
                }

                if (!txtEventConsequence.Text.IsValidEventConsquence())
                {
                    MessageBox.Show("Please enter a shorter event consequence.");
                    txtEventConsequence.Focus();
                    txtEventConsequence.SelectAll();
                    return;
                }

                if (!txtEventDescription.Text.IsValidEventDescription())
                {
                    MessageBox.Show("Please enter a shorter event description.");
                    txtEventDescription.Focus();
                    txtEventDescription.SelectAll();
                    return;
                }
                if (calendar.SelectedDate == null)
                {
                    MessageBox.Show("Please enter an event date.");
                    calendar.Focus();
                    return;
                }
                IncidentEvent newincidentEvent = new IncidentEvent(_selectedItem.IncidentName, calendar.SelectedDate, txtPersonsInvolved.Text, txtEventDescription.Text, txtEventConsequence.Text, null, _selectedUser.UserAccountID, _loggedInUserId);

                try
                {
                    _incidentEventManager.UpdateIncidentEvent(oldIncidentEvent, newincidentEvent);

                }
                catch (Exception)
                {
                    MessageBox.Show("The Incident event could not be added.");
                }
                NavigationService.GoBack();
            }
        }

        private void btnDeleteIncidentEvent_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure you want to delete this incident event? It will be lost forever", "Delete Incident event?", MessageBoxButton.OKCancel);
            try
            {
                _incidentEventManager.DeleteIncidentEvent(oldIncidentEvent.IncidentEventID);
            }
            catch (Exception)
            {

                MessageBox.Show("The Incident event could not be deleted.");
            }
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}
