/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/25
/// 
/// This page is what the AddEdit Reward Page
/// will direct to when the user clicks 
/// "Add Reward"
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
    /// Interaction logic for pgRewardpgAddEditReward.xaml
    /// </summary>
    public partial class pgRewardpgAddEditReward : Page
    {
        private IRewardManager _rewardManager = new RewardManager();
        private int userAccountID;
        private UserAccount _selectedUser;
        private Reward reward = new Reward();
        private bool _addReward = true;
        private Reward oldReward;
        private Reward newReward = new Reward();

        public pgRewardpgAddEditReward(UserAccount selectedUser)
        {
            _selectedUser = selectedUser;
            userAccountID = selectedUser.UserAccountID;
            InitializeComponent();
            OnPageLoad(userAccountID);
        }
        public pgRewardpgAddEditReward(UserAccount selectedUser, Reward oldReward)
        {
            userAccountID = selectedUser.UserAccountID;
            _selectedUser = selectedUser;
            this.oldReward = oldReward;
            _addReward = false;
            InitializeComponent();
            setupEdit();
            OnPageLoad(userAccountID);
        }

        private void setupEdit()
        {
            txtRewardName.Text = oldReward.RewardName;
            txtRewardDescription.Text = oldReward.RewardDescription;
            lblAddNewAward.Content = "Edit Reward";
            btnAddNewReward.Content = "Save";
        }

        private void OnPageLoad(int userAccountID)
        {
            this.userAccountID = userAccountID;
        }

        private void ReloadRewardPage(UserAccount selectedUser)
        {
            var viewReward = new pgRewardpgViewRewards(selectedUser);
            NavigationService.Navigate(viewReward);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/25
        /// 
        /// This is what the user will click when they want to add a new Reward
        /// </summary>
        private void btnAddNewReward_Click(object sender, RoutedEventArgs e)
        {
            if (_addReward == true)
            {
                int rowsAffected;
                rowsAffected = _rewardManager.CreateReward(userAccountID, txtRewardName.Text, txtRewardDescription.Text);
                if (rowsAffected == 1)
                {
                    ReloadRewardPage(_selectedUser);
                }
                else
                {
                    txtErrorTextBox.Visibility = Visibility.Visible;
                }
            }
            if (_addReward == false)
            {
                try
                {
                    newReward.RewardName = txtRewardName.Text;
                    newReward.RewardDescription = txtRewardDescription.Text;
                    newReward.UserID = userAccountID;
                    newReward.Active = true;
                    newReward.RewardID = oldReward.RewardID;

                    bool successfullyUpdatedReward;

                    successfullyUpdatedReward = _rewardManager.EditReward(oldReward, newReward);
                    if (successfullyUpdatedReward == true)
                    {
                        ReloadRewardPage(_selectedUser);
                    }
                    else
                    {
                        txtErrorTextBox.Visibility = Visibility.Visible;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }

        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/25
        /// 
        /// Takes the user back to the Rewards page
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReloadRewardPage(_selectedUser);
        }
    }
}
