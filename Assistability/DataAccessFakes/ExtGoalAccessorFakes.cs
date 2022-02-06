/// <summary>
/// Becky Baenziger
/// Created: 2021/04/27
/// ///
/// Fakes to use for unit testing of the extinction goal logic
/// </summary>
/// <remarks>
/// Updater Name:
/// Update Date:
/// 
/// </remarks>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;
using DataAccessInterfaces;
using DataAccessLayer;

namespace DataAccessFakes
{
    public class ExtGoalAccessorFakes : IExtGoalAccessor
    {
        List<ExtGoalViewModel> data = new List<ExtGoalViewModel>();

        public ExtGoalAccessorFakes()
        {
            data.Add(new ExtGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Thank You",
                ExtGoalDescription = "Say thank you",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 1,
                Active = false,
                AwardName = "Award number 3",
                IncidentName = "Say thank you"

            });

            data.Add(new ExtGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                ExtGoalName = "Sportsmanship",
                ExtGoalDescription = "Demonstrate good sportsmanship",
                ExtGoalTargetDate = new DateTime(2021, 04, 04),
                ExtGoalEntryDate = new DateTime(2021, 02, 02),
                ExtGoalEditDate = null,
                ExtGoalRemovalDate = null,
                IncidentFrequency = 1,
                Active = true,
                AwardName = "Award number 2",
                IncidentName = "sportsman like conduct"

            });
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Fake to test deactivating an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger with William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Adjusted fakes to deal with changes made the accessor
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="extGoalName"></param>
        /// <param name="extGoalEntryDate"></param>
        /// <returns></returns>
        public int DeactivateExtinctionGoal(int userID_client, string extGoalName, DateTime extGoalEntryDate)
        {
            var count = data.FindAll(d => d.ExtGoalName == extGoalName).Count; // how many rows are active now

            return count;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Fake to test deactivating an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="extGoal"></param>
        /// <returns></returns>
        public int InsertExtinctionGoal(ExtGoalViewModel extGoal)
        {
            var count = data.Count; // how many rows have now
            data.Add(extGoal);

            return this.data.Count - count;  // new total - old total should b 1
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Fake to test reactivating an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger with William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Adjusted fakes to deal with changes made the accessor
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="extGoalName"></param>
        /// <param name="extGoalEntryDate"></param>
        /// <returns></returns>
        public int ReactivateExtinctionGoal(int userID_client, string extGoalName, DateTime extGoalEntryDate)
        {
            var count = data.FindAll(d => d.ExtGoalName == extGoalName).Count; // how many rows are active now

            return count;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Fake to test deactivating an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<ExtGoalViewModel> SelectExtinctionGoalsByActive(int userID_client, bool active)
        {
            List<ExtGoalViewModel> clientGoals = data.FindAll(g => g.UserID_client == userID_client);
            List<ExtGoalViewModel> activeClientGoals = clientGoals.FindAll(g => g.Active == active);

            return activeClientGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Fake to test deactivating an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <returns></returns>
        public List<ExtGoalViewModel> SelectExtinctionGoalsByUserIDClient(int userID_client)
        {
            List<ExtGoalViewModel> clientGoals = data.FindAll(g => g.UserID_client == userID_client);
            return clientGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/27
        /// ///
        /// Fake to test updating an extinction goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger with William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Adjusted fakes to deal with changes made the accessor
        /// </remarks>
        /// 
        /// <param name="oldExtGoal"></param>
        /// <param name="newExtGoal"></param>
        /// <returns></returns>
        public int UpdateExtinctionGoal(ExtGoalViewModel oldExtGoal, ExtGoalViewModel newExtGoal)
        {
            try
            {
                data.Add(newExtGoal);
                data.Remove(oldExtGoal);

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
