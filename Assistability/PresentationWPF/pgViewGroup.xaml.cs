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
using DataStorageModels;
using DataAccessLayer;
using DataAccessInterfaces;
namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for FrViewGroup.xaml
    /// </summary>
    public partial class pgViewGroup : Page
    {
        private IUserGroupAccessor _userGroupAccessor;
        private UserGroup _selectedGroup;

        public pgViewGroup(UserGroup selectedItem)
        {
            InitializeComponent();

            _selectedGroup = selectedItem;
            _userGroupAccessor = new UserGroupAccessor();
            dgRoleSelectedGrid.ItemsSource = _userGroupAccessor.SelectUserAccountsByUserGroup(_selectedGroup);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dgRoleSelectedGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {




            var selectedItem = (UserAccount)dgRoleSelectedGrid.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }

            var group = _selectedGroup;

            var viewSelectedUser = new PgEditUserMembershipRoles(selectedItem, group);
            NavigationService.Navigate(viewSelectedUser);

        }
    }
}
