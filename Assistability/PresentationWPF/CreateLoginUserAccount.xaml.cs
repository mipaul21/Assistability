///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/11
/// Added the funtionality to create a new account
/// </remarks>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/23
/// Added the funtionality for the forgot your password option
/// </remarks>

﻿using DataAccessFakes;
using DataAccessLayer;
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
using System.Windows.Shapes;

namespace PresentationWPF 
{
    /// </summary>
    /// Interaction logic for CreateLoginUserAccount.xaml
    /// </summary>
    public partial class CreateLoginUserAccount : Window
    {
        private UserAccount _user;

        public CreateLoginUserAccount()
        {
            InitializeComponent();
        }

        public CreateLoginUserAccount(UserAccount user)
        {
            _user = user;
            InitializeComponent();
        }

        //private IUserManager _userManager = new UserManager(new UserFakes());
        private IUserManager _userManager = new UserManager();

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/02/05
        /// 
        /// This method will authenticate the user credentials
        /// for a potential login, on a success it will take them 
        /// to the dashboard. On a failure it will describe what
        /// is wrong, whether it be username or password
        /// </summary>
        ///
        /// <remarks>
        /// Nathaniel Webber
        /// Updated: 2021/02/18 
        /// First MVP delivered
        /// </remarks>
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
       {
            try
            {
                UserAccount authenticatedUser = _userManager.AuthenticateUser(txtUserName.Text, pwdPassword.Password);
                _user.UserAccountID = authenticatedUser.UserAccountID;
                _user.UserName = authenticatedUser.UserName;
                _user.FirstName = authenticatedUser.FirstName;
                _user.LastName = authenticatedUser.LastName;
                _user.Email = authenticatedUser.Email;
                _user.Active = authenticatedUser.Active;
                this.DialogResult = true;
                return;

            }
            catch (Exception ex)
            {
                txtUserName.Clear();
                pwdPassword.Clear();
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                txtUserName.Focus();
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/11
        /// 
        /// This method will call the create account window when the create account 
        /// button is pushed
        /// </summary>
        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            var CreateAccount = new WPFCreateAccountWindow("create");
            CreateAccount.ShowDialog();
        }

		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/11
        /// 
        /// This method will call the create account window to 
		/// help reset the password when the forgot password text is 
		/// clicked.
        /// </summary>
        private void lblForgotPassword_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var CreateAccount = new WPFCreateAccountWindow("forgot");
            CreateAccount.ShowDialog();
        }
    }
}
