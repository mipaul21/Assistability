/// <summary>
/// Becky Baenziger
/// Created: 2021/03/02
/// ///
/// Fakes to use for unit testing of the habitual goal logic
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
    public class HabGoalAccessorFakes : IHabGoalAccessor
    {
        List<HabGoalViewModel> data = new List<HabGoalViewModel>();
        public HabGoalAccessorFakes()
        {
            data.Add(new HabGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Mornings",
                HabGoalDescription = "establish a morning routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency =1,
                Active = false,
                AwardName = "Award number 1",
                RoutineName = "Morning Routine"

            });

            data.Add(new HabGoalViewModel()
            {
                UserID_client = 3,
                UserID_admin = 1,
                HabGoalName = "Bedtime",
                HabGoalDescription = "establish a bedtime routine",
                HabGoalTargetDate = new DateTime(2021, 04, 04),
                HabGoalEntryDate = new DateTime(2021, 02, 02),
                HabGoalEditDate = null,
                HabGoalRemovalDate = null,
                RoutineFrequency = 1,
                Active = true,
                AwardName = "Award number 2",
                RoutineName = "Bedtime Routine"
            });




        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/02
        /// ///
        /// Fakes to use to test for deactivating an habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// updated to reflect changes in the accessor
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="habGoalName"></param>
        /// <param name="habGoalEntryDate"></param>
        /// <returns></returns>
        public int DeactivateHabitualGoal(int userID_client, string habGoalName, DateTime habGoalEntryDate)
        {
            var count = data.FindAll(d => d.HabGoalName == habGoalName).Count; // how many rows are active now

            return count;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/02
        /// ///
        /// Fakes to use to test for deactivating an habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="habGoal"></param>
        /// <returns></returns>
        public int InsertHabitualGoal(HabGoalViewModel habGoal)
        {
            var count = data.Count; // how many rows have now
            data.Add(habGoal);

            return this.data.Count - count;  // new total - old total should b 1
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/02
        /// ///
        /// Fakes to use to test for deactivating an habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// updated to reflect changes in the accessor
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="habGoalName"></param>
        /// <param name="habGoalEntryDate"></param>
        /// <returns></returns>
        public int ReactivateHabitualGoal(int userID_client, string habGoalName, DateTime habGoalEntryDate)
        {
            var count = data.FindAll(d => d.HabGoalName == habGoalName).Count; // how many rows are active now

            return count;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/02
        /// ///
        /// Fakes to use to test for deactivating an habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<HabGoalViewModel> SelectHabitualGoalsByActive(int userID_client, bool active)
        {
            List<HabGoalViewModel> clientGoals = data.FindAll(g => g.UserID_client == userID_client);
            List<HabGoalViewModel> activeClientGoals = clientGoals.FindAll(g => g.Active == active);

            return activeClientGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/02
        /// ///
        /// Fakes to use to test for deactivating an habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// 
        /// <param name="userID_client"></param>
        /// <returns></returns>
        public List<HabGoalViewModel> SelectHabitualGoalsByUserIDClient(int userID_client)
        {
            List<HabGoalViewModel> clientGoals = data.FindAll(g => g.UserID_client == userID_client);
            return clientGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/02
        /// ///
        /// Fakes to use to test for deactivating an habitual goal
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger and William Clark
        /// Update Date: 2021/04/27
        /// ///
        /// updated to reflect changes in the accessor
        /// </remarks>
        /// 
        /// <param name="oldHabGoal"></param>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        public int UpdateHabitualGoal(HabGoalViewModel oldHabGoal, HabGoalViewModel newHabGoal)
        {
            try
            {
                data.Add(newHabGoal);
                data.Remove(oldHabGoal);

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
