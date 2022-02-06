/// <summary>
/// Ryan Taylor
/// Created: 2021/02/20
/// 
/// This is the code used to run the WPFpJournalsJournalEntries page.
/// </summary>
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/04
/// Added the funtionality to handle viewing a journal entry
/// </remarks>
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/19
/// fixed an arangement problem within the UpdateDGJournalEntries method
/// </remarks>

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
    /// Interaction logic for pgJournalsJournalEntries.xaml
    /// </summary>
    public partial class pgJournalsJournalEntries : Page
    {
        private bool _journalUpdated = false;
        private UserAccount _user;
        private string journalName;
        private DataStorageModels.JournalEntry _journalEntry;
        private IJournalManager _journalManager = new JournalManager();
        private string purpose;

        public pgJournalsJournalEntries(UserAccount User, string JournalName)
        {
            _user = User;
            journalName = JournalName;
            InitializeComponent();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/26
        /// 
        /// This method is used to call a window for creating jounral entries.
        /// </summary>
        /// <param name="_user">The userAccount object of the person logged in</param>
        ///<param name="journalName">The name the journal</param>
        ///<param name="_journalEntry"></param>
        ///<exception></exception>
        ///<returns>A window for creating a new journal entry</returns>
        private void btnAddJournalEntry_Click(object sender, RoutedEventArgs e)
        {
            purpose = "AddJournalEntry";
            var CreateJournalEntry = new WPFOtherWindow(_journalEntry, _user, journalName, purpose);
            CreateJournalEntry.ShowDialog();
            _journalUpdated = true;
            UpdateDGJournalEntries();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// This method is used for populating the data grid with journal entries of a
        /// specific journal.
        /// </summary>
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception>any exeptions passed down from 
        ///the logic or data access layer</exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        private void WindowJournalEntries_Loaded(object sender, RoutedEventArgs e)
        {
            lblJournalName.Content = journalName;
            lblJournalOwnerName.Content = _user.FirstName + " " + _user.LastName;
            try
            {
                if (dgJournalEntries.ItemsSource == null || _journalUpdated == true)
                {
                    dgJournalEntries.ItemsSource = _journalManager.RetrieveJournalEntreisFromJournal(_user.UserAccountID, journalName);

                    dgJournalEntries.Columns[3].Header = "Entry";
                    dgJournalEntries.Columns[4].Header = "Date";
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[5]);
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[2]);
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[1]);
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[0]);


                    _journalUpdated = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// This method is used for udpadting the data grid if a new journal 
        /// entry is added to the journal.
        /// </summary>
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception>any exeptions passed down from 
        ///the logic or data access layer</exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        private void UpdateDGJournalEntries()
        {
            try
            {
                if (dgJournalEntries.ItemsSource == null || _journalUpdated == true)
                {
                    dgJournalEntries.ItemsSource = _journalManager.RetrieveJournalEntreisFromJournal(_user.UserAccountID, journalName);

                    dgJournalEntries.Columns[3].Header = "Entry";
                    dgJournalEntries.Columns[4].Header = "Date";
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[5]);
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[2]);
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[1]);
                    dgJournalEntries.Columns.Remove(dgJournalEntries.Columns[0]);

                    _journalUpdated = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/04
        /// 
        /// This method is used to give funtionality to the View button by having it
        /// pass the selected journals information into the other window containing 
        /// the WPFpCreateViewEditJouranlEntry.
        /// </summary>
        ///<param name="JournalEntry">The selected journal entry object</param>
        ///<param name="JournalName">The name the journal</param>
        private void btnViewJournalEntry_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (DataStorageModels.JournalEntry)dgJournalEntries.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            _journalEntry = selectedItem;
            purpose = "ViewEditJournalEntry";
            var ViewJournalEntry = new WPFOtherWindow(_journalEntry, purpose);
            ViewJournalEntry.ShowDialog();
            _journalUpdated = true;
            UpdateDGJournalEntries();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/04
        /// 
        /// This method is used to pass the selected journals information 
        /// into the other window containing the WPFpCreateViewEditJouranlEntry.
        /// </summary>
        ///<param name="JournalEntry">The selected journal entry object</param>
        ///<param name="JournalName">The name the journal</param>
        ///<exception>if nothing is selected</exception>
        private void dgJournalEntries_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (DataStorageModels.JournalEntry)dgJournalEntries.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            _journalEntry = selectedItem;
            purpose = "ViewEditJournalEntry";
            var ViewJournalEntry = new WPFOtherWindow(_journalEntry, purpose);
            ViewJournalEntry.ShowDialog();
            _journalUpdated = true;
            UpdateDGJournalEntries();
        }
    }
}
