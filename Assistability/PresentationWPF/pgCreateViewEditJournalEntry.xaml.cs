/// <summary>
/// Ryan Taylor
/// Created: 2021/02/26
/// 
/// This is used to create a journal entry.
/// </summary>
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/04
/// Added the funtionality to handle viewing a journal entry
/// </remarks>
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/16
/// Added the funtionality to edit a journal entry
/// </remarks>

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
    /// Interaction logic for pgCreateViewEditJournalEntry.xaml
    /// </summary>
    public partial class pgCreateViewEditJournalEntry : Page
    {
        private DataStorageModels.JournalEntry _journalEntry;
        private UserAccount _user;
        private string _journalName;
        public pgCreateViewEditJournalEntry(DataStorageModels.JournalEntry journalEntry,
            string journalName, UserAccount user)
        {
            _journalEntry = journalEntry;
            _user = user;
            _journalName = journalName;
            InitializeComponent();
        }

        public pgCreateViewEditJournalEntry(DataStorageModels.JournalEntry journalEntry)
        {
            _journalEntry = journalEntry;
            InitializeComponent();
        }
        private IJournalManager _journalManager = new JournalManager();

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/26
        /// 
        /// This class is used to create a journal entry.
        /// </summary>
        /// 
        /// <param name="sender">information from the user to 
        /// create their account</param>
        /// <exception> something went wrong creating the account</exception>
        /// <remarks>
        /// Ryan Taylor
        /// Updated: 2021/03/16
        /// Added the funtionality to edit a journal entry
        /// </remarks>
        private void btnJournalEntryEdit_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnJournalEntryEdit.Content == "Add")
            {
                if (!txtJournalEntry.Text.IsValidJournalEntry())
                {
                    MessageBox.Show(txtJournalEntry.Text + " is not a valid journal entry" +
                        "(500 characters max)");
                    txtJournalEntry.Focus();
                    txtJournalEntry.SelectAll();
                    return;
                }
                _journalEntry = new DataStorageModels.JournalEntry()
                {
                    UserIDClient = _user.UserAccountID,
                    UserIDClientJournal = _user.UserAccountID,
                    JournalID = _journalName,
                    JournalEntryBody = txtJournalEntry.Text,
                    JournalEditDate = DateTime.Now
                };
                try
                {
                    _journalManager.AddJournalEntry(_journalEntry);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                MessageBox.Show("Journal Entry: " + _journalEntry.JournalEntryBody
                    + "\n for " + _journalEntry.JournalID + " was created at "
                    + DateTime.Now + ".");
                NavigationService.Navigate(null);
            }
            else if ((string)btnJournalEntryEdit.Content == "Edit")
            {
                btnJournalEntryEdit.Content = "Save";
                txtJournalEntry.IsReadOnly = false;
            }
            else //if ((string)btnJournalEntryEdit.Content == "Save")
            {
                if (!txtJournalEntry.Text.IsValidJournalEntry())
                {
                    MessageBox.Show(txtJournalEntry.Text + " is not a valid journal entry" +
                        "(500 characters max)");
                    txtJournalEntry.Focus();
                    txtJournalEntry.SelectAll();
                    return;
                }
                string newJournalEntryBody = txtJournalEntry.Text;
                try
                {
                    _journalManager.EditJournalEntry(_journalEntry, newJournalEntryBody);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                MessageBox.Show("Journal Entry: " + _journalEntry.JournalEntryBody
                    + "\n for " + _journalEntry.JournalID + " was edited at "
                    + _journalEntry.JournalEditDate + ".");
                NavigationService.Navigate(null);
            }
        }
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/04
        /// 
        /// This class is used to populate the page with the correct information.
        /// </summary>
        /// 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_journalEntry == null)
            {
                txtJournalTitle.Text = _journalName;
                lblJournalPourpuse.Content = "Create Journal Entry";
                btnJournalEntryEdit.Content = "Add";
                txtJournalEntry.IsReadOnly = false;
                btnJournalEntryDelete.IsEnabled = false;
                btnJournalEntryDelete.Visibility = Visibility.Hidden;
                btnJournalEntryEdit.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                txtJournalTitle.Text = _journalEntry.JournalID;
                lblJournalPourpuse.Content = "View Journal Entry";
                txtJournalEntry.Text = _journalEntry.JournalEntryBody;
                btnJournalEntryEdit.Content = "Edit";
                txtJournalEntry.IsReadOnly = true;
                btnJournalEntryDelete.IsEnabled = true;
                btnJournalEntryDelete.Visibility = Visibility.Visible;
                btnJournalEntryEdit.Margin = new Thickness(0, 0, 100, 0);

            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/11
        /// 
        /// This class is used to send perapeters of the journal entry to the method for
        /// deleting journal entries.
        /// </summary>
        private void btnJournalEntryDelete_Click(object sender, RoutedEventArgs e)
        {
            _journalManager.RemoveJournalEntry(_journalEntry.UserIDClient,
                _journalEntry.JournalID, _journalEntry.JournalEntryBody,
                _journalEntry.JournalEntryDate);
            MessageBox.Show("Journal Entry: " + _journalEntry.JournalEntryBody + "\n for "
                    + _journalEntry.JournalID + " was deleted");
            NavigationService.Navigate(null);
        }
    }
}
