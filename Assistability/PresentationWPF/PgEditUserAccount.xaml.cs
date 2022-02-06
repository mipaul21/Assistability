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
using DataStorageModels;
using LogicLayerInterfaces;
using LogicLayer;
using PresentationWPF;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for PgEditUserAccount.xaml
    /// </summary>
    public partial class PgEditUserAccount : Page
    {
        private UserAccount oldUserAccount;
        private IUserManager _userManager = new UserManager();
        private bool _addUser = false;

        // Places user information into PgEditUserAccount.xaml

        public PgEditUserAccount(DataStorageModels.UserAccount _user)
        {
            InitializeComponent();

            _user = _userManager.GetUserAccountByUserAccountID(_user.UserAccountID);
            lblUserID.Content = _user.UserAccountID;
            txtFirstName.Text = _user.FirstName;
            txtLastName.Text = _user.LastName;
            txtUserName.Text = _user.UserName;
            txtEmailAddress.Text = _user.Email;
            oldUserAccount = _user;
            

        }




        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// Click event that invokes either Save or Edit depending upon if 
        /// Edit Had been clicked. If data is changed while Edit has been selected,
        /// The save button will update the DB with that data after it has passed
        /// through the validators in ValidationHelpers.cs
        /// 
        /// After the update, oldAccount will be changed to the new iteration helping with further edits during the same session.
        /// 
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnEditSave.Content == "Edit")
            {
                this.Title = "Edit Employee Data";
                setupEdit();
            }
            else // save operation
            {
                if (!txtFirstName.Text.isValidFirstName())
                {
                    MessageBox.Show("Invalid First Name");
                    txtFirstName.Focus();
                    txtFirstName.SelectAll();
                    return;
                }
                if (!txtLastName.Text.isValidLastName())
                {
                    MessageBox.Show("Invalid Last Name");
                    txtLastName.Focus();
                    txtLastName.SelectAll();
                    return;
                }
                if (!txtEmailAddress.Text.isValidEmail())
                {
                    MessageBox.Show("Invalid Email Address");
                    txtEmailAddress.Focus();
                    txtEmailAddress.SelectAll();
                    return;
                }


                try
                {

                    var newUserAccount = new UserAccount()
                    {
                        UserAccountID = Convert.ToInt32(lblUserID.Content),
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        UserName = txtUserName.Text,
                        Email = txtEmailAddress.Text,
                        Active = (bool)chkDeactivateActiveAccount.IsChecked

                    };
                    if (!_addUser) // edit user
                    {
                        _userManager.UpdateUserAccount(oldUserAccount, newUserAccount);

                        btnEditSave.Content = "Edit";
                    } // add user
                    else
                    {
                    }
                    // TO DO:: logout user when they succesfully deactivate their account. 
                    if(newUserAccount.Active == false)
                    {
                       
                    }
                    setUpSave();
                    oldUserAccount = newUserAccount;
                    MessageBox.Show("Account Succesfully Updated.");
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }

            }
        }
        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// Helper method to Setup the edit.
        /// 
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        private void setupEdit()
        {
            btnEditSave.Content = "Save";
            txtFirstName.IsReadOnly = false;
            txtFirstName.BorderBrush = Brushes.Black;

            txtLastName.IsReadOnly = false;
            txtLastName.BorderBrush = Brushes.Black;



            txtUserName.IsReadOnly = false;
            txtUserName.BorderBrush = Brushes.Black;

            chkDeactivateActiveAccount.IsEnabled = true;
        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// Helper method to set up the save.
        /// 
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        private void setUpSave()
        {
            btnEditSave.Content = "Edit";
            txtFirstName.IsReadOnly = true;
            txtFirstName.BorderBrush = Brushes.Transparent;

            txtLastName.IsReadOnly = true;
            txtLastName.BorderBrush = Brushes.Transparent;

            txtEmailAddress.IsReadOnly = true;
            txtEmailAddress.BorderBrush = Brushes.Transparent;

            txtUserName.IsReadOnly = true;
            txtUserName.BorderBrush = Brushes.Transparent;

            chkDeactivateActiveAccount.IsEnabled = false;

        }


        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver: 
        /// Click event to bring the use back to homescreen upon completion of an edit
        /// or deciding they do not want to edit at all.
        /// 
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        // TO DO:: Logic to delete or archive an account due to primary keys being affected. 
        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/10/2021
        /// Approver: 
        /// Click event to delete the account if the account is deactivated.
        /// 
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
/*        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldUserAccount.Active == true)
                {
                    MessageBox.Show("Account must be deactivated in order to delete.");

                    NavigationService.GoBack();
                }


                if (oldUserAccount.Active == false)
                {
                    _userManager.DeleteUserAccount(oldUserAccount);
                    NavigationService.Refresh();
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }*/
    }
}