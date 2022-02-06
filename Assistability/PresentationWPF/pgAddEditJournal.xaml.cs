/// <summary>
/// Jory A. Wernette
/// Created: 2021/03/03
/// 
/// This page is what the View Journal Page
/// will direct to when the user clicks 
/// "Add Journal" or "Edit Journal"
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
    /// Interaction logic for pgAddEditJournal.xaml
    /// </summary>
    public partial class pgAddEditJournal : Page
    {
        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// Instantiating some objects and variables I will need to manipulate between methods
        /// </summary>
        private IJournalManager _journalManager = new JournalManager();
        private int userAccountID;
        private Journal journal = new Journal();
        private Journal newJournal = new Journal();
        private UserAccount _user;

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// The default constructor for the AddEditJournal Page
        /// </summary>
        public pgAddEditJournal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// The constructor for the AddEditJournal Page that will be used when a user would like to edit a Journal
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in and would like to add a Journal</param>
        /// <exception>UserID not found</exception>
        /// <returns>An empty form to create a Journal</returns>
        /// </summary>
        public pgAddEditJournal(int userAccountID, UserAccount User)
        {
            this.userAccountID = userAccountID;
            _user = User;

            InitializeComponent();
            OnPageLoad(userAccountID);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// The constructor for the AddEditJournal Page that will be used when a user would like to add a Journal
        /// The user can change the name or the description of the Journal
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in and would like to edit a Journal</param>
        /// <param name="journalName"> The name of the Journal the user would like to edit.</param>
        /// <exception>UserID not found</exception>
        /// <returns>An filled out form to edit a selected Journal</returns>
        /// </summary>
        public pgAddEditJournal(int userAccountID, string journalName)
        {
            this.userAccountID = userAccountID;

            InitializeComponent();
            OnPageLoad(userAccountID);

            //Fill the  txtJournalName and txtJournalDescription text boxes with the existing info
            txtJournalName.Text = journalName;
            txtJournalDescription.Text = _journalManager.SelectJournalByUserIDAndJournalName(userAccountID, journalName).JournalDescription;

            // Storing this inside a journal object in case the user updates
            journal.UserID_Client = userAccountID;
            journal.JournalName = journalName;
            journal.JournalDescription = txtJournalDescription.Text;

            btnAddNewJournal.Visibility = Visibility.Hidden;
            btnUpdateJournal.Visibility = Visibility.Visible;
            btnDeleteJournal.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This is a method that runs when the page loads to save the user's ID
        /// 
        /// <param name="userID"> The user ID of the user who is logged in</param>
        /// <exception>UserID not found</exception>
        /// <returns></returns>
        /// </summary>
        public void OnPageLoad(int userID)
        {
            this.userAccountID = userID;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This is a method to close this window. It will return to the View Journals page
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in</param>
        /// <exception>UserID not found</exception>
        /// <returns>The viewJournalPage</returns>
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            reloadViewJournalsPage(userAccountID);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This is a helper method for the addEditJournal page to help reload the viewJournals page 
        /// if the user leaves this page from clicking cancel, successfully adding a new Journal,
        /// or when successfully updating a Journal
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in</param>
        /// <exception>UserID not found</exception>
        /// <returns>The viewJournalPage</returns>
        /// </summary>
        private void reloadViewJournalsPage(int userID)
        {
            var viewJournals = new PgViewJournals(this.userAccountID, _user);

            NavigationService.Navigate(viewJournals);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This button saves a new Journal created by the user after filling out the Add Journal Form
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in and would like to add a Journal</param>
        /// <param name="journalName"> The name of the Journal</param>
        /// <param name="journalDescription"> The description of the Journal</param>
        /// <exception>UserID not found</exception>
        /// <returns>An filled out form to edit a selected Journal</returns>
        /// </summary>
        private void btnAddNewJournal_Click(object sender, RoutedEventArgs e)
        {
            //call created Journal
            int rowsAffected;
            rowsAffected = _journalManager.CreateAJournalWithUserID(userAccountID, txtJournalName.Text, txtJournalDescription.Text);
            if (rowsAffected == 1)
            {
                // reload view Journals Page
                reloadViewJournalsPage(userAccountID);
            }
            else
            {
                // throw error onto the screen that thing was not saved
                txtErrorTextBox.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// This is the method that overwrites a Journal in the Database.
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in</param>
        /// <param name="journalName"> The name of the Journal</param>
        /// <param name="journalDescription"> The description of the Journal</param>
        /// <exception>UserID not found</exception>
        /// <exception>Journal Name is empty</exception>
        /// <exception>Journal Description is empty</exception>
        /// <returns>The viewJournalPage</returns>
        /// </summary>
        private void btnUpdateJournal_Click(object sender, RoutedEventArgs e)
        {
            newJournal.UserID_Client = userAccountID;
            newJournal.JournalName = txtJournalName.Text;
            newJournal.JournalDescription = txtJournalDescription.Text;
            int rowsAffected;
            rowsAffected = _journalManager.UpdateJournal(newJournal, journal);
            if (rowsAffected == 1)
            {
                // reload view Journals Page
                reloadViewJournalsPage(userAccountID);
            }
            else
            {
                // throw error onto the screen that thing was not saved
                txtErrorTextBox.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// The method to delete a Journal when the user clicks delete after choosing to edit the Journal
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in and would like to edit a Journal</param>
        /// <param name="journalName"> The name of the Journal the user would like to edit.</param>
        /// <exception>UserID not found</exception>
        /// <returns>The View Journal Page</returns>
        /// </summary>
        private void btnDeleteJournal_Click(object sender, RoutedEventArgs e)
        {
            //delete any associated entries within the journal
            //for (int i = 0; i < _journalManager.SelectAllJournalEntriesByUserIDAndJournalName.Count; i++)
            //{
            //    deleteJournalEntryByUserIDAndJournalName(userAccountID, txtJournalName.Text);
            //}

            //delete the current journal
            int result = _journalManager.DeleteJournalByUserIDAndJournalName(userAccountID, txtJournalName.Text);
            if (result == 1)
            {
                //if success, reload view Journal Page
                reloadViewJournalsPage(userAccountID);
            }
            else
            {
                // throw error onto the screen that thing was not saved
                txtErrorTextBox.Visibility = Visibility.Visible;
            }
        }
    }
}
