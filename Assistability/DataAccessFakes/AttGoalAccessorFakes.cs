/// <summary>
/// Becky Baenziger
/// Created: 2021/04/02
/// ///
/// Fakes to use for unit testing of the attainment goal logic
/// </summary>
/// <remarks>
/// Updater Name:
/// Update Date:
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class AttGoalAccessorFakes : IAttGoalAccessor
    {
        List<AttGoalViewModel> data = new List<AttGoalViewModel>();

        public AttGoalAccessorFakes()
        {
            data.Add(new AttGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                AttGoalName = "Thank You",
                AttGoalDescription = "Say thank you",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 1,
                Active = false,
                AwardName = "Award number 3",
                PerformanceName = "Say thank you"

            });

            data.Add(new AttGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                AttGoalName = "Sportsmanship",
                AttGoalDescription = "Demonstrate good sportsmanship",
                AttGoalTargetDate = new DateTime(2021, 04, 04),
                AttGoalEntryDate = new DateTime(2021, 02, 02),
                AttGoalEditDate = null,
                AttGoalRemovalDate = null,
                PerformanceFrequency = 1,
                Active = true,
                AwardName = "Award number 2",
                PerformanceName = "sportsman like conduct"

            });
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Fake to test deactivating an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger with William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Adjusted fakes to deal with changes made the accessor
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="attGoalName"></param>
        /// <param name="attGoalEntryDate"></param>
        /// <returns></returns>
        public int DeactivateAttainmentGoal(int userID_client, string attGoalName, DateTime attGoalEntryDate)
        {

            var count = data.FindAll(d => d.AttGoalName == attGoalName).Count; // how many rows are active now

            return count;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Fake to test inserting an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="attGoal"></param>
        /// <returns></returns>
        public int InsertAttainmentGoal(AttGoalViewModel attGoal)
        {
            var count = data.Count; // how many rows have now
            data.Add(attGoal);

            return this.data.Count - count;  // new total - old total should b 1
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Fake to test reactivating an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger with William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// Adjusted fakes to deal with changes made the accessor
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="attGoalName"></param>
        /// <param name="attGoalEntryDate"></param>
        /// <returns></returns>
        public int ReactivateAttainmentGoal(int userID_client, string attGoalName, DateTime attGoalEntryDate)
        {
            var count = data.FindAll(d => d.AttGoalName == attGoalName).Count; ; // how many rows are active now

            return count;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Fake to test getting a list of attainment goal by the userID_client and active.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<AttGoalViewModel> SelectAttainmentGoalsByActive(int userID_client, bool active)
        {
            List<AttGoalViewModel> clientGoals = data.FindAll(g => g.UserID_client == userID_client);
            List<AttGoalViewModel> activeClientGoals = clientGoals.FindAll(g => g.Active == active);

            return activeClientGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Fake to test getting a list of attainment goals by userID_client.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <returns></returns>
        public List<AttGoalViewModel> SelectAttainmentGoalsByUserIDClient(int userID_client)
        {
            List<AttGoalViewModel> clientGoals = data.FindAll(g => g.UserID_client == userID_client);
            return clientGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Fake to test updating an attainment goal.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger with William Clark
        /// Update Date: 2021/04/27
        /// Adjusted fakes to deal with changes made the accessor
        /// </remarks>
        /// 
        /// <param name="oldAttGoal"></param>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        public int UpdateAttainmentGoal(AttGoalViewModel oldAttGoal, AttGoalViewModel newAttGoal)
        {
            try
            {
                data.Add(newAttGoal);
                data.Remove(oldAttGoal);

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
