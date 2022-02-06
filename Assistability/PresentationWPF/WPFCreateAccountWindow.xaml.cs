/// <summary>
/// Ryan Taylor
/// Created: 2021/02/05
/// 
/// This takes the information the user put
/// into the interface to make an user account.
/// </summary>
/// 
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/19 
/// added better exeption handleing for specificaly 
/// unique emails and unique usernames
/// </remarks>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/23
/// Added the funtionality to handle the forgot your password option
/// </remarks>
/// 
/// <remarks>
/// William Clark
/// Updated: 2021/04/15
/// Added subsidiary account creation functionality
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
using System.Windows.Shapes;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for WPFCreateAccountWindow.xaml
    /// </summary>
    public partial class WPFCreateAccountWindow : Window
    {
        private string _purpose;
        private UserAccount _userAccount;
        public WPFCreateAccountWindow(string purpose)
        {
            _purpose = purpose;
            _userAccount = new UserAccount();
            InitializeComponent();
            if (_purpose == "forgot")
            {
                lblPassword.Visibility = Visibility.Hidden;
                lblReEnterPassword.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Hidden;
                txtReEnterPassword.Visibility = Visibility.Hidden;
                btnAddUserAccount.Content = "Find Account";
            }
        }

        public WPFCreateAccountWindow(UserAccount userAccount)
        {
            _purpose = "subsidiary";
            _userAccount = userAccount;
            InitializeComponent();
        }

        private IUserManager _userAccountsManager = new UserManager();

		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/10
		/// 
		/// This is used to give funtion to the create account button.
		/// </summary>
		/// /// <remarks>
		/// Ryan Taylor
		/// Updated: 2021/02/17 
		/// added a message box that tells the user that the account was made
		/// </remarks>
		/// <remarks>
		/// Ryan Taylor
		/// Updated: 2021/03/19 
		/// added better exeption handleing for specificaly 
		/// unique emails and unique usernames
		/// </remarks>
		/// <remarks>
		/// William Clark
		/// Updated: 2021/04/15
		/// Added subsidiary account creation functionality
		/// </remarks>
		/// 
		/// <param name="sender">information from the user to 
		/// create their account</param>
		/// <exception> something went wrong creating the account</exception>
		private void btnAddUserAccount_Click(object sender, RoutedEventArgs e)
        {
			if (_purpose == "create")
            {
				try
				{
					if (!txtEmail.Text.IsValidEmail())
					{
						MessageBox.Show(txtEmail.Text + " is not a valid email");
						txtEmail.Focus();
						txtEmail.SelectAll();
						return;
					}
	
					if (!txtFirstName.Text.IsValidFirstName())
					{
						MessageBox.Show(txtFirstName.Text + " is not a valid first name (too long)");
						txtFirstName.Focus();
						txtFirstName.SelectAll();
						return;
					}
	
					if (!txtLastName.Text.IsValidLastName())
					{
						MessageBox.Show(txtLastName.Text + " is not a valid last name (too long)");
						txtLastName.Focus();
						txtLastName.SelectAll();
						return;
					}
	
					if (!txtUsername.Text.IsValidUsername())
					{
						MessageBox.Show(txtUsername.Text + " is not a valid username (too long)");
						txtUsername.Focus();
						txtUsername.SelectAll();
						return;
					}
	
					if (!txtPassword.Password.IsValidPassword())
					{
						MessageBox.Show(txtPassword.Password + " is not a valid password");
						txtPassword.Focus();
						txtPassword.SelectAll();
						return;
					}
	
					if (txtReEnterPassword.Password != txtPassword.Password)
					{
						MessageBox.Show(txtReEnterPassword.Password + " does not match " + txtPassword.Password);
						txtReEnterPassword.Focus();
						txtReEnterPassword.SelectAll();
						return;
					}
	
					var newUserAccount = new UserAccount()
					{
						Email = txtEmail.Text,
						Active = true,
						FirstName = txtFirstName.Text,
						LastName = txtLastName.Text,
						UserName = txtUsername.Text
					};
	
					_userAccountsManager.AddNewUserAccount(newUserAccount, txtPassword.Password);
					MessageBox.Show("Account: " + newUserAccount.UserName + "\n for "
						+ newUserAccount.FirstName + " " + newUserAccount.LastName + " was created");
					this.DialogResult = true;
				}
				catch (Exception ex)
				{
					string emailError = "ak_Email";
					string usernameError = "ak_UserName";
					if (ex.InnerException.Message.Contains(emailError))
					{
						MessageBox.Show(ex.Message + "\n\n" + "That email is already in use"); 
						txtEmail.Focus();
						txtEmail.SelectAll();
						return;
	
					}
					else if (ex.InnerException.Message.Contains(usernameError))
					{
						MessageBox.Show(ex.Message + "\n\n" + "That username is already in use");
						txtUsername.Focus();
						txtUsername.SelectAll();
						return;
					}
					else
					{
						MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
					}
				}
			}
			else if (_purpose == "forgot") 
            {
                if (!txtFirstName.Text.IsValidFirstName())
                {
                    MessageBox.Show(txtFirstName.Text + " is not a valid first name (too long)");
                    txtFirstName.Focus();
                    txtFirstName.SelectAll();
                    return;
                }

                if (!txtLastName.Text.IsValidLastName())
                {
                    MessageBox.Show(txtLastName.Text + " is not a valid last name (too long)");
                    txtLastName.Focus();
                    txtLastName.SelectAll();
                    return;
                }

                if (!txtUsername.Text.IsValidUsername())
                {
                    MessageBox.Show(txtUsername.Text + " is not a valid username (too long)");
                    txtUsername.Focus();
                    txtUsername.SelectAll();
                    return;
                }

                if (!txtEmail.Text.IsValidEmail())
                {
                    MessageBox.Show(txtEmail.Text + " is not a valid email");
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return;
                }

                try
                {
                    UserAccount userSearch =
                        _userAccountsManager.GetUserAccountByUsername(txtUsername.Text);
                    if (userSearch == null) 
                    {
                        MessageBox.Show("We could not find account "
                            + txtUsername.Text + "\n for "+ txtFirstName.Text 
                            + " "+ txtLastName.Text + ".");
                    }
                    else //if(userSearch != null)
                    {
                        if (txtLastName.Text == userSearch.LastName && 
                            txtFirstName.Text == userSearch.FirstName &&
                            txtEmail.Text == userSearch.Email) 
                        {
                            try
                            {
                                _userAccountsManager.ResetPassword(userSearch.Email);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + "\n\n" + 
                                    ex.InnerException.Message);
                            }
                            MessageBox.Show("Your password was changed to newuser", 
                                "Password Changed");
                            this.DialogResult = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    txtFirstName.Focus();
                    txtFirstName.SelectAll();
                    return;
                }
            }
            else if (_purpose == "subsidiary")
            {
				try
				{
					if (!txtEmail.Text.IsValidEmail())
					{
						MessageBox.Show(txtEmail.Text + " is not a valid email");
						txtEmail.Focus();
						txtEmail.SelectAll();
						return;
					}

					if (!txtFirstName.Text.IsValidFirstName())
					{
						MessageBox.Show(txtFirstName.Text + " is not a valid first name (too long)");
						txtFirstName.Focus();
						txtFirstName.SelectAll();
						return;
					}

					if (!txtLastName.Text.IsValidLastName())
					{
						MessageBox.Show(txtLastName.Text + " is not a valid last name (too long)");
						txtLastName.Focus();
						txtLastName.SelectAll();
						return;
					}

					if (!txtUsername.Text.IsValidUsername())
					{
						MessageBox.Show(txtUsername.Text + " is not a valid username (too long)");
						txtUsername.Focus();
						txtUsername.SelectAll();
						return;
					}

					if (!txtPassword.Password.IsValidPassword())
					{
						MessageBox.Show(txtPassword.Password + " is not a valid password");
						txtPassword.Focus();
						txtPassword.SelectAll();
						return;
					}

					if (txtReEnterPassword.Password != txtPassword.Password)
					{
						MessageBox.Show(txtReEnterPassword.Password + " does not match " + txtPassword.Password);
						txtReEnterPassword.Focus();
						txtReEnterPassword.SelectAll();
						return;
					}

					var newUserAccount = new UserAccount()
					{
						Email = txtEmail.Text,
						Active = true,
						FirstName = txtFirstName.Text,
						LastName = txtLastName.Text,
						UserName = txtUsername.Text
					};

					_userAccount.UserAccountID = _userAccountsManager.AddSubsidiaryUserAccount(newUserAccount, txtPassword.Password);
					MessageBox.Show("Account: " + newUserAccount.UserName + "\n for "
						+ newUserAccount.FirstName + " " + newUserAccount.LastName + " was created");
					this.DialogResult = true;
				}
				catch (Exception ex)
				{
					string emailError = "ak_Email";
					string usernameError = "ak_UserName";
					if (ex.InnerException.Message.Contains(emailError))
					{
						MessageBox.Show(ex.Message + "\n\n" + "That email is already in use");
						txtEmail.Focus();
						txtEmail.SelectAll();
						return;

					}
					else if (ex.InnerException.Message.Contains(usernameError))
					{
						MessageBox.Show(ex.Message + "\n\n" + "That username is already in use");
						txtUsername.Focus();
						txtUsername.SelectAll();
						return;
					}
					else
					{
						MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
					}
				}
			}

        }
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/10
        /// 
        /// This is used to give funtion to the create cancel button.
        /// </summary>
		/// 
		/// <param name="sender">closes the create acount window</param>
        private void btnCancleAddingUserAccount_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
