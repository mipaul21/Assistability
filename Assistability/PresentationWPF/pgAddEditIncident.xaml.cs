using DataStorageModels;
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
using LogicLayer;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for pgAddEditIncident.xaml
    /// </summary>
    public partial class pgAddEditIncident : Page
    {
        private bool _addIncident = true;
        private bool _active = true;
        private UserAccount _selectedUser;
        private Incident oldIncident;
        private int _loggedInUserId;
        private IIncidentManager _incidentManager = new IncidentManager();


        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/03/20
        /// Populates pgAddEditIncident with the selected incidents information. 
        /// </summary>
        public pgAddEditIncident(Incident _incidentEntry)
        {
            InitializeComponent();
            _addIncident = false;

            // Xaml page controls
            txtIncidentName.IsReadOnly = true;
            lblRemovalDate.Visibility = Visibility.Hidden;
            txtRemovalDate.Visibility = Visibility.Hidden;
            btnAddIncident.Content = "Save Edit";

            // Initiates oldIncident into _incidentEntry allowing the populating the page
            oldIncident = _incidentEntry;

            // Fills out the form using the selected entry.
            txtIncidentName.Text = _incidentEntry.IncidentName;
            txtIncidentDescription.Text = _incidentEntry.IncidentDescription;
            txtDesiredConsequence.Text = _incidentEntry.DesiredConsequence;
            txtEntryDate.Text = _incidentEntry.IncidentEntryDate.ToString();
            txtEditDate.Text = _incidentEntry.IncidentEditDate.ToString();

            chkActive.IsChecked = _incidentEntry.Active;




        }
        public pgAddEditIncident(IIncidentManager incidentManager, UserAccount selectedUser, int loggedInUserID)
        {
            _selectedUser = selectedUser;
            _loggedInUserId = loggedInUserID;
            _incidentManager = incidentManager;
            InitializeComponent();
        }
        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/03/20
        /// Add Incident and Edit Incident Logic, allows validation on new incidents and old. 
        /// </summary>
        private void btnAddIncident_Click(object sender, RoutedEventArgs e)
        {
           
            if (_addIncident == true)
            {
                if (!txtIncidentName.Text.IsValidIncidentName())
                {
                    MessageBox.Show("Please enter a shorter incident name.");
                    txtIncidentName.Focus();
                    txtIncidentName.SelectAll();
                    return;

                }

                if (!txtIncidentDescription.Text.IsValidIncidentDescription())
                {
                    MessageBox.Show("Please enter a shorter incident description.");
                    txtIncidentDescription.Focus();
                    txtIncidentDescription.SelectAll();
                    return;

                }
                if (!txtIncidentDescription.Text.IsValidDesiredConsequence())
                {
                    MessageBox.Show("Please enter a shorter incident description.");
                    txtIncidentDescription.Focus();
                    txtIncidentDescription.SelectAll();
                    return;

                }
                Incident newIncident = new Incident(txtIncidentName.Text, txtIncidentDescription.Text, txtDesiredConsequence.Text, DateTime.Now, null, null, _active, _selectedUser.UserAccountID, _loggedInUserId);
                try
                {
                    _incidentManager.AddNewIncident(newIncident);

                }
                catch (Exception)
                {
                    MessageBox.Show("The Incident could not be added.");
                }
                NavigationService.GoBack();

            }
                if(_addIncident == false)
                {
                    if (!txtIncidentName.Text.IsValidIncidentName())
                    {
                        MessageBox.Show("Please enter a shorter incident name.");
                        txtIncidentName.Focus();
                        txtIncidentName.SelectAll();
                        return;

                    }

                    if (!txtIncidentDescription.Text.IsValidIncidentDescription())
                    {
                        MessageBox.Show("Please enter a shorter incident description.");
                        txtIncidentDescription.Focus();
                        txtIncidentDescription.SelectAll();
                        return;

                    }
                    if (!txtIncidentDescription.Text.IsValidDesiredConsequence())
                    {
                        MessageBox.Show("Please enter a shorter incident description.");
                        txtIncidentDescription.Focus();
                        txtIncidentDescription.SelectAll();
                        return;

                    }
                    Incident newIncident = new Incident(txtIncidentName.Text, txtIncidentDescription.Text, txtDesiredConsequence.Text,
                        oldIncident.IncidentEntryDate, DateTime.Now, null, oldIncident.Active, oldIncident.UserId_Client, oldIncident.UserId_Creator);
                    try
                    {
                    oldIncident.IncidentEditDate = DateTime.Now;

                    _incidentManager.UpdateIncident(oldIncident, newIncident);
                    this.NavigationService.GoBack();

                }

                catch (Exception)
                    {
                        MessageBox.Show("The Incident could not be Updated.");
                    }
                    NavigationService.GoBack();
                

            }
        }



        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/03/20
        /// Brings the user to the previous page.
        /// Eventually should be replaced after solution for page closing found.
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
