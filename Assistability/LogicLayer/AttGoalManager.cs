/// <summary>
/// Becky Baeniger
/// Created: 2021/03/28
/// ///
/// Manage adding an attainment goal to the database
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
    public class AttGoalManager : IAttGoalManager
    {
        private IAttGoalAccessor _attGoalAccessor = null;

        public AttGoalManager()
        {
            _attGoalAccessor = new AttGoalAccessor();
        }

        public AttGoalManager(IAttGoalAccessor attGoalAccessor)
        {
            _attGoalAccessor = attGoalAccessor;
        }

        /// <summary>
        /// Becky Baeniger
        /// Created: 2021/03/28
        /// ///
        /// Manage adding an attainment goal to the database
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        public bool AddAttainmentGoal(AttGoalViewModel newAttGoal)
        {
            bool result = false;
            int rowAffected = 0;

            try
            {
                rowAffected = _attGoalAccessor.InsertAttainmentGoal(newAttGoal);
                if (rowAffected == 0)
                {
                    throw new ApplicationException("New Attainment Goal was not added.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Attainment Goal Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Manage editing an attainment goal
        /// </summary>
        /// <param name="oldAttGoal"></param>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        public bool EditAttainmentGoal(AttGoalViewModel oldAttGoal, AttGoalViewModel newAttGoal)
        {
            bool result = false;
            try
            {
                if (oldAttGoal.Active != newAttGoal.Active)
                {
                    if (newAttGoal.Active == true)
                    {
                        result = 1 == _attGoalAccessor.ReactivateAttainmentGoal(oldAttGoal.UserID_client, oldAttGoal.AttGoalName, oldAttGoal.AttGoalEntryDate);
                    }
                    else
                    {
                        result = 1 == _attGoalAccessor.DeactivateAttainmentGoal(oldAttGoal.UserID_client, oldAttGoal.AttGoalName, oldAttGoal.AttGoalEntryDate);
                    }
                }
                if (oldAttGoal.AwardName != newAttGoal.AwardName || oldAttGoal.PerformanceFrequency != newAttGoal.PerformanceFrequency 
                    || oldAttGoal.AttGoalDescription != newAttGoal.AttGoalDescription)
                {
                    result = (1 == _attGoalAccessor.UpdateAttainmentGoal(oldAttGoal, newAttGoal));
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
        /// Created: 2021/03/28
        /// ///
        /// Manages retrieving list of attainment goals by userID and actice
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<AttGoalViewModel> RetrieveAttainmentGoalsByActive(int userID_client, bool active)
        {
            List<AttGoalViewModel> attGoals = null;
            try
            {
                attGoals = _attGoalAccessor.SelectAttainmentGoalsByActive(userID_client, active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Attainment Goals list not available.", ex);
            }
            return attGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/03/28
        /// ///
        /// Manage retrieving list of attainment goals by user ID
        /// </summary>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        public List<AttGoalViewModel> RetrieveAttainmentGoalsByUserIDClient(int userID_client)
        {
            List<AttGoalViewModel> attGoals = null;
            try
            {
                attGoals = _attGoalAccessor.SelectAttainmentGoalsByUserIDClient(userID_client);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Attainment Goals list not available.", ex);
            }
            return attGoals;
        }
    }
}
