/// <summary>
/// William Clark
/// Created: 2021/02/18
/// 
/// Interface for viewing of UserAccounts
/// </summary>
///
/// <remarks>
/// </remarks>
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
    /// Interaction logic for AddEditDetailUserAccount.xaml
    /// </summary>
    public partial class AddEditDetailUserAccount : Window
    {
        /// <summary>
        /// William Clark
        /// Created: 2021/02/18
        /// 
        /// Takes a UserAccount and constructs a AddEditDetailUserAccount with text boxes filled with the UserAccount fields
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// /// 
        /// <param name="_user">The _user for which the account information will be displayed</param>
        public AddEditDetailUserAccount(DataStorageModels.UserAccount _user)
        {
            InitializeComponent();
            tbkUserAccountName.Text = _user.FirstName + " " + _user.LastName;
            txtUserAccountUserName.Text = _user.UserName;
            txtUserAccountFirstName.Text = _user.FirstName;
            txtUserAccountLastName.Text = _user.LastName;
            txtUserAccountEmail.Text = _user.Email;
            pwdUserAccountPassword.Password = "";
            pwdUserAccountPassword.IsEnabled = false;
            chkUserAccountActive.IsChecked = _user.Active;
        }
    }
}
