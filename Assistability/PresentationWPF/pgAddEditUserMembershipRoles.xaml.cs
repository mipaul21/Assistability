/// <summary>
///  Mitchell Paul 
/// Created: 2021/04/03
/// 
/// This page holds the list of users inside of the group. 
/// 
/// 
/// </summary>


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
using DataAccessLayer;
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataStorageModels;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for pgAddEditUserMembershipRoles.xaml
    /// </summary>
    public partial class pgAddEditUserMembershipRoles : Page
    {
        private IUserGroupAccessor _userGroupAccessor;
        private List<UserGroup> userGroup;

        public pgAddEditUserMembershipRoles(int userID)
        {

            _userGroupAccessor = new UserGroupAccessor();
            InitializeComponent();

            dgRoleGrid.ItemsSource = _userGroupAccessor.SelectOwnedUserGroupsByUserAccountID(userID);






        }

        private void dgRoleGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (UserGroup)dgRoleGrid.SelectedItem;

            if(selectedItem == null)
            {
                return;
            }

            var addViewSelectedGroup = new pgViewGroup(selectedItem);

            NavigationService.Navigate(addViewSelectedGroup);
        }
    }
}
