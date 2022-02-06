/// <summary>
/// Becky Baenziger
/// Created: 2021/02/18
/// ///
/// The habgoal logic manager
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
using DataAccessInterfaces;
using DataAccessLayer;
using DataViewModels;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class HabGoalManager : IHabGoalManager
    {
        private IHabGoalAccessor _habGoalAccessor = null;

        public HabGoalManager()
        {
            _habGoalAccessor = new HabGoalAccessor();
        }

        public HabGoalManager(IHabGoalAccessor habGoalAccessor)  // dependency inversion
        {
            _habGoalAccessor = habGoalAccessor;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Logic for adding a habitual goal to the database
        /// </summary>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        public bool AddHabitualGoal(HabGoalViewModel newHabGoal)
        {
            bool result = false;
            int rowAffected = 0;

            try
            {
                rowAffected = _habGoalAccessor.InsertHabitualGoal(newHabGoal);
                if (rowAffected == 0)
                {
                    throw new ApplicationException("New Habitual Goal was not added.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Habitual Goal Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Logic for editing a habitual goal
        /// </summary>
        /// <param name="oldHabGoal"></param>
        /// <param name="newHabGoal"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        public bool EditHabitualGoal(HabGoalViewModel oldHabGoal,
                                        HabGoalViewModel newHabGoal)
        {
            bool result = false;
            try
            {
                if (oldHabGoal.Active != newHabGoal.Active)
                {
                    if (newHabGoal.Active == true)
                    {
                        result = 1 == _habGoalAccessor.ReactivateHabitualGoal(oldHabGoal.UserID_client, oldHabGoal.HabGoalName, oldHabGoal.HabGoalEntryDate);
                    }
                    else
                    {
                        result = 1 == _habGoalAccessor.DeactivateHabitualGoal(oldHabGoal.UserID_client, oldHabGoal.HabGoalName, oldHabGoal.HabGoalEntryDate);
                    }
                }
                if(oldHabGoal.AwardName != newHabGoal.AwardName || oldHabGoal.RoutineFrequency != newHabGoal.RoutineFrequency ||
                    oldHabGoal.HabGoalDescription != newHabGoal.HabGoalDescription)
                {
                    result = (1 == _habGoalAccessor.UpdateHabitualGoal(oldHabGoal, newHabGoal));
                    if (result == false)
                    {
                        throw new ApplicationException("Goal not changed");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Logic for getting list of hab goals by userID_client and active
        /// </summary>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<HabGoalViewModel> RetrieveHabitualGoalsByActive(int userID_client, bool active)
        {
            List<HabGoalViewModel> habGoals = null;
            try
            {
                habGoals = _habGoalAccessor.SelectHabitualGoalsByActive(userID_client, active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Habitual Goals list not available.", ex);
            }
            return habGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// Logic for viewing a list of goals by userId_client
        /// </summary>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        public List<HabGoalViewModel> RetrieveHabitualGoalsByUserIDClient(int userID_client)
        {

            List<HabGoalViewModel> habGoals = null;
            try
            {
                habGoals = _habGoalAccessor.SelectHabitualGoalsByUserIDClient(userID_client);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Habitual Goals list not available.", ex);
            }
            return habGoals;
        }
    }
}
