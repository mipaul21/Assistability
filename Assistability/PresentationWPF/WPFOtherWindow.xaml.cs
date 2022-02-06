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
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/04
/// Added the funtionality to handle viewing a journal entry
/// </remarks>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/04
/// Added the funtionality to handle viewing a journal entry
/// </remarks>

using DataStorageModels;
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
    /// Interaction logic for WPFOtherWindow.xaml
    /// </summary>
    public partial class WPFOtherWindow : Window
    {
        private DataStorageModels.JournalEntry _journalEntry;
        private UserAccount _user;
        private string _journalName;
        private string porpuse;
        public WPFOtherWindow(DataStorageModels.JournalEntry journalEntry,
            UserAccount user, string JournalName, string Porpuse)
        {
            _journalEntry = journalEntry;
            _user = user;
            _journalName = JournalName;
            porpuse = Porpuse;
            InitializeComponent();
        }
        public WPFOtherWindow(DataStorageModels.JournalEntry journalEntry, string Porpuse)
        {
            _journalEntry = journalEntry;
            porpuse = Porpuse;
            InitializeComponent();
        }
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        /// 
        /// This method is used for calling the other window and telling it to 
        /// populate it.
        /// </summary>
        ///<param name="userID">The id of the creator of the journal</param>
        ///<param name="JournalName">The name the journal</param>
        ///<param name="_journalEntry">A journal entry object that may be null</param>
        ///<exception>any exeptions passed down from 
        ///the logic or data access layer</exception>
        ///<returns>A list of Journal Entries for the journal</returns>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (porpuse == "AddJournalEntry")
            {
                _journalEntry = null;
                var addJournalEntryWindow = new pgCreateViewEditJournalEntry(
                    _journalEntry, _journalName, _user);
                frmOtherWindow.Navigate(addJournalEntryWindow);
                frmOtherWindow.NavigationUIVisibility = NavigationUIVisibility.Hidden;
                if (!frmOtherWindow.Navigate(addJournalEntryWindow))
                {
                    this.DialogResult = false;
                }
            }
            else if (porpuse == "ViewEditJournalEntry")
            {
                var viewEditJournalEntryWindow = new pgCreateViewEditJournalEntry(
                    _journalEntry);
                frmOtherWindow.Navigate(viewEditJournalEntryWindow);
                frmOtherWindow.NavigationUIVisibility = NavigationUIVisibility.Hidden;
                if (!frmOtherWindow.Navigate(viewEditJournalEntryWindow))
                {
                    this.DialogResult = false;
                }
            }
        }
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/05
        /// 
        /// This method is used for closing the other window.
        /// </summary>
        private void btnOtherWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
