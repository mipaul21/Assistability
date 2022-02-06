/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/18
/// 
/// </summary>

using DataAccessFakes;
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
    /// Interaction logic for pgRewardpgViewRewards.xaml
    /// </summary>
    public partial class pgRewardpgViewRewards : Page
    {
        // private IRewardManager _rewardManager = new RewardManager(new RewardFake());
        private UserAccount _selectedUser;
        private IRewardManager _rewardManager = new RewardManager();
        private int userAccountID;
        private List<Reward> rewardList = null;

        public pgRewardpgViewRewards(UserAccount selectedUser)
        {
            _selectedUser = selectedUser;
            userAccountID = selectedUser.UserAccountID;
            InitializeComponent();
            OnPageLoad(userAccountID);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This is the logic that will happen when this page loads. It will fill the grid with the user's Awards.
        /// </summary>
        private void OnPageLoad(int userAccountID)
        {
            try
            {
                rewardList = _rewardManager.RetreiveAllRewards(userAccountID);
                dgRewardDisplay.ItemsSource = rewardList;

            }
            catch (Exception)
            {
                MessageBox.Show("Rewards could not be found");
                NavigationService.GoBack();
                NavigationService.RemoveBackEntry();
            }
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/25
        /// 
        /// This is what the user will click when they want to create a new Reward
        /// </summary>
        private void btnCreateReward_Click(object sender, RoutedEventArgs e)
        {
            var addReward = new pgRewardpgAddEditReward(_selectedUser);
            NavigationService.Navigate(addReward);
        }

        private void btnEditReward_Click(object sender, RoutedEventArgs e)
        {
            Reward oldReward = null;
            if (dgRewardDisplay.SelectedItem == null)
            {
                // dont crash please
            }
            oldReward = (Reward)dgRewardDisplay.SelectedItem;
            var editReward = new pgRewardpgAddEditReward(_selectedUser, oldReward);
            NavigationService.Navigate(editReward);
        }

        private void btnActivateReactivateReward_Click(object sender, RoutedEventArgs e)
        {

            if (dgRewardDisplay.SelectedItem != null)
            {
                Reward reward = (Reward)dgRewardDisplay.SelectedItem;
                bool changeStatus = (bool)reward.Active;

                if (changeStatus == true)
                {
                    _rewardManager.DeactivateReward(_selectedUser, reward);
                    OnPageLoad(_selectedUser.UserAccountID);

                }
                else
                {
                    _rewardManager.ReactivateReward(_selectedUser, reward);
                    OnPageLoad(_selectedUser.UserAccountID);
                }
            }

        }

        private void dgRewardDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRewardDisplay.SelectedItem != null)
            {
                Reward reward = (Reward)dgRewardDisplay.SelectedItem;
                bool changeStatus = (bool)reward.Active;
                if (changeStatus == true)
                {
                    btnActivateReactivateReward.Content = "Deactivate reward";
                }
                else
                {
                    btnActivateReactivateReward.Content = "Reactivate reward";
                }
            }
        }
    }
}
