/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/10
/// 
/// This page is what the AddEdit Award Page
/// will direct to when the user clicks 
/// "Add Award" or "Edit Award"
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
    /// Interaction logic for pgAddEditAward.xaml
    /// </summary>
    public partial class pgAddEditAward : Page
    {
        private IAwardManager _awardManager = new AwardManager();
        private int userAccountID;
        private Award award = new Award();
        private Award newAward = new Award();
        private string awardName;

        public pgAddEditAward()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// The constructor for the AddEditAward Page that will be used when a user would like to add an award
        /// The user can change the name or the description of the Award
        /// 
        /// <param name="userAccountID"> The user ID of the user who is logged in and would like to edit an Award</param>
        /// <exception>UserID not found</exception>
        /// <returns>An filled out form to edit a selected Award</returns>
        /// </summary>
        public pgAddEditAward(int userAccountID)
        {
            this.userAccountID = userAccountID;
            InitializeComponent();
            OnPageLoad(userAccountID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/24
        /// Because of changes to table, no longer needed 2 different constructors so combined into one and changed from
        /// List<Award> situation to Award.
        /// </remarks>
        /// <param name="userAccountID"></param>
        /// <param name="awardName"></param>
        public pgAddEditAward(int userAccountID, string awardName)
        {
            this.userAccountID = userAccountID;
            this.awardName = awardName;
            InitializeComponent();
            OnPageLoad(userAccountID);

            award = _awardManager.RetreiveAwardByAwardName(awardName);

            txtAwardName.Text = awardName;
            txtAwardDescription.Text = award.AwardDescription;

            award.AwardName = awardName;
            award.AwardDescription = txtAwardDescription.Text;

            lblAddNewAward.Content = "Edit an Award";
            btnAddNewAward.Visibility = Visibility.Hidden;
            btnUpdateAward.Visibility = Visibility.Visible;
            btnDeleteAward.Visibility = Visibility.Visible;
        }

        private void OnPageLoad(int userAccountID)
        {
            this.userAccountID = userAccountID;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// Returns the user back to the pgViewAwardPage
        /// 
        /// </summary>
        private void ReloadViewAwardPage(int userID)
        {
            var viewAward = new pgViewAwards(this.userAccountID);
            NavigationService.Navigate(viewAward);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This is what the user will click when they want to add a new Award
        /// </summary>
        private void btnAddNewAward_Click(object sender, RoutedEventArgs e)
        {
            int rowsAffected;
            rowsAffected = _awardManager.CreateAward(txtAwardName.Text, txtAwardDescription.Text);
            if (rowsAffected == 1)
            {
                ReloadViewAwardPage(userAccountID);
            }
            else
            {
                txtErrorTextBox.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This is what the user will click when they want to update an Award
        /// </summary>
        private void btnUpdateAward_Click(object sender, RoutedEventArgs e)
        {
            newAward.AwardName = txtAwardName.Text;
            newAward.AwardDescription = txtAwardDescription.Text;
            int rowsAffected;

            rowsAffected = _awardManager.UpdateAward(newAward, award);
            if (rowsAffected == 1)
            {
                ReloadViewAwardPage(userAccountID);
            }
            else
            {
                txtErrorTextBox.Visibility = Visibility.Visible;
            }

        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// Returns the user back to the pgViewAwardPage
        /// 
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReloadViewAwardPage(userAccountID);
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This is what the user will click when they want to delete an Award
        /// </summary>
        private void btnDeleteAward_Click(object sender, RoutedEventArgs e)
        {
            int deleteAward = 0;
            try
            {
                deleteAward = _awardManager.DeleteAward(awardName);
                if (deleteAward == 1)
                {
                    ReloadViewAwardPage(userAccountID);
                }
                else
                {
                    txtErrorTextBox.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The Award couldn't be deleted because it is in use by a goal.");
            }
            
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This is what the user will click when they want to deactivate an Award
        /// </summary>
        private void btnDeactivateAward_Click(object sender, RoutedEventArgs e)
        {
            int deactivateAward = _awardManager.DeactivateAwardByAwardName(awardName);
            if (deactivateAward == 1)
            {
                ReloadViewAwardPage(userAccountID);
            }
            else
            {
                txtErrorTextBox.Visibility = Visibility.Visible;
            }
        }
    }
}
