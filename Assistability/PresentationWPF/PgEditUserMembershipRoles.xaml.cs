/// <summary>
///  Mitchell Paul 
/// Created: 2021/04/03
/// 
/// This page allows the manipulation of the selected users roles. 
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
using System.Windows.Shapes;
using DataStorageModels;
using DataAccessInterfaces;
using DataAccessLayer;
using LogicLayerInterfaces;
using DataViewModels;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for FrEditUserMembershipRoles.xaml
    /// </summary>
    public partial class PgEditUserMembershipRoles : Page
    {
        private IUserGroupAccessor _userGroupAccessor;
        private IMembershipRoleAccessor _membershipRoleAccessor;

        private List<Role> _assignedRoles;
        private List<Role> _unassignedRoles;
        private List<string> _displayedassignedRoles;
        private List<string> _displayedunassignedRoles;
        private UserAccount _user;
        private UserGroup _group;
        Membership membership;
        MembershipVM membershipVM;

        public PgEditUserMembershipRoles(UserAccount user, UserGroup group)
        {

            _user = user;
            _group = group;

            membership = new Membership(group.GroupID, user.UserAccountID);
            membershipVM = new MembershipVM(membership, _assignedRoles);

            _userGroupAccessor = new UserGroupAccessor();



            InitializeComponent();
            _unassignedRoles = new List<Role>();
            _assignedRoles = new List<Role>();

            _displayedassignedRoles = new List<string>();
            _displayedunassignedRoles = new List<string>();


            // this sets the roles for the listbox items source
            // _assignedRoles = _userGroupAccessor.SelectAllMembershipRolesByMembership(membership);
            _assignedRoles = _userGroupAccessor.SelectAllMembershipRoleNamesByMembership(membership);
            // this preserves the original roles for the employee being edited





            foreach (var r in _assignedRoles)
            {
                _displayedassignedRoles.Add(r.Name);
            }
            _unassignedRoles = _userGroupAccessor.SelectAllRoles();

            foreach (var r in _unassignedRoles)
            {
                _displayedunassignedRoles.Add(r.Name);
            }

            foreach (var r in _assignedRoles)
            {
                _displayedunassignedRoles.Remove(r.Name);
            }






            // this preserves the unassigned roles for convenience
            lstAssignedRoles.ItemsSource = _displayedassignedRoles;
            lstUnassignedRoles.ItemsSource = _displayedunassignedRoles;





        }

        private void lstAssignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstAssignedRoles.SelectedItem;
            _displayedassignedRoles.Remove(selectedRole.ToString());
            _displayedunassignedRoles.Add(selectedRole.ToString());



            lstAssignedRoles.ItemsSource = null;
            lstUnassignedRoles.ItemsSource = null;

            lstAssignedRoles.ItemsSource = _displayedassignedRoles;
            lstUnassignedRoles.ItemsSource = _displayedunassignedRoles;
        }

        private void lstUnassignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstUnassignedRoles.SelectedItem;
            _displayedunassignedRoles.Remove(selectedRole.ToString());
            _displayedassignedRoles.Add(selectedRole.ToString());





            lstAssignedRoles.ItemsSource = null;
            lstUnassignedRoles.ItemsSource = null;



            lstAssignedRoles.ItemsSource = _displayedassignedRoles;
            lstUnassignedRoles.ItemsSource = _displayedunassignedRoles;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            _membershipRoleAccessor = new MembershipRoleAccessor();


            // Removes all the roles from the user
            foreach (var r in _displayedassignedRoles)
            {
                _membershipRoleAccessor.DeleteUserMembershipRoles(_group.GroupID, _user.UserAccountID, r);
            }

            // Inserts of all of the selected roles into the user
            foreach (var r in _displayedassignedRoles)
            {
                _membershipRoleAccessor.InsertNewUserMembershipRoles(_group.GroupID, _user.UserAccountID, r);

            }

            // Populates the unassigned roles table with roles not added. 
            foreach (var r in _displayedunassignedRoles)
            {
                if (!_unassignedRoles.ToString().Contains(r))
                {
                    _membershipRoleAccessor.DeleteUserMembershipRoles(_group.GroupID, _user.UserAccountID, r);
                }
            }


            var viewUsers = new pgViewGroup(_group);

            NavigationService.Navigate(viewUsers);

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var viewUsers = new pgViewGroup(_group);

            NavigationService.Navigate(viewUsers);
        }
    }
}
