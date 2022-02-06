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
    /// Interaction logic for pgViewJournals.xaml
    /// </summary>
    public partial class PgViewJournals : Page
    {
        private IJournalManager _journalManager = new JournalManager();
        private int userAccountID;
        private UserAccount _user;

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// The intial constructor for the AddEditJournal Page
        /// </summary>
        public PgViewJournals(DataStorageModels.UserAccount _user)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/03
        /// 
        /// A different constructor for the AddEditJournal Page
        /// So i can more easily get the userAccountID
        /// </summary>
        public PgViewJournals(int userAccountID, UserAccount User)
        {
            this.userAccountID = userAccountID;
            _user = User;

            InitializeComponent();
            OnPageLoad(userAccountID);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This is the logic that will happen when this page loads. It will fill the grid with the user's Journals.
        /// </summary>
        public void OnPageLoad(int userID)
        {
            try
            {
                dgJournalDisplay.ItemsSource = _journalManager.SelectAllJournalsByUserID(userAccountID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// When the User double clicks a Journal, it will pull the logic to show that Journal's entries
        /// This will lead into Ryan's Journal Entry implementation
        /// </summary>
        private void dgJournalDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (DataStorageModels.Journal)dgJournalDisplay.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            Journal theJournal = selectedItem;

            var JournalObject = new pgJournalsJournalEntries(_user, theJournal.JournalName);
            NavigationService.Navigate(JournalObject);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This is the button the User would click to add a new Journal.
        /// </summary>
        private void btnNewJournal_Click(object sender, RoutedEventArgs e)
        {
            var addJournal = new pgAddEditJournal(userAccountID, _user);

            NavigationService.Navigate(addJournal);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/02/23
        /// 
        /// This is the button the user would click to edit a selected Journal
        /// </summary>
        private void btnEditJournal_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Journal)dgJournalDisplay.SelectedItem;
            if (selectedItem == null)
            {
                lblError.Visibility = Visibility.Visible;
                return;
            }
            var editJournal = new pgAddEditJournal(userAccountID, selectedItem.JournalName);

            NavigationService.Navigate(editJournal);
        }
    }
}
