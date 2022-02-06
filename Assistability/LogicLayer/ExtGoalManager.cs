/// <summary>
/// Nick Loesel
/// Created: 2021/03/17
/// 
/// Implements the ExtGoal manager interface.
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/18
/// Added insert a extGoal
/// </remarks>
using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataViewModels;

namespace LogicLayer
{

    public class ExtGoalManager : IExtGoalManager
    {
        private IExtGoalAccessor _extGoalAccessor;

        public ExtGoalManager()
        {
            _extGoalAccessor = new ExtGoalAccessor();
        }

        public ExtGoalManager(IExtGoalAccessor extGoalAccessor)
        {
            _extGoalAccessor = extGoalAccessor;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/8
        /// 
        /// This is the method that will insert a new extGoal object.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// added the call to the InsertExtinctionGoal accessor and related code rowAffected, edited the messages
        /// 
        /// </remarks>
        /// <param name="newExtGoal">The new extGoal object.</param>
        /// <exception>No ExtGoal inserted</exception>
        /// <returns>True if it was inserted</returns>
        public bool AddExtinctionGoal(ExtGoalViewModel newExtGoal)
        {
            bool result = false;
            int rowAffected = 0;

            try
            {
                rowAffected = _extGoalAccessor.InsertExtinctionGoal(newExtGoal);
                if (rowAffected == 0)
                {
                    throw new ApplicationException("New Extinction Goal was not added.");
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add new Extinction Goal failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// changed to EditExtinctionGoal, added call to updateextinctiongoal, reactivateextinctiongoal and deactivate extinction goal
        /// fixed error msgs.
        /// 
        /// </remarks>
        /// <param name="oldExtGoal"></param>
        /// <param name="newExtGoal"></param>
        /// <returns></returns>
        public bool EditExtinctionGoal(ExtGoalViewModel oldExtGoal, ExtGoalViewModel newExtGoal)
        {
            bool result = false;

            try
            {
                if (oldExtGoal.Active != newExtGoal.Active)
                {
                    if (newExtGoal.Active == true)
                    {
                        result = 1 == _extGoalAccessor.ReactivateExtinctionGoal(oldExtGoal.UserID_client, oldExtGoal.ExtGoalName, oldExtGoal.ExtGoalEntryDate);
                    }
                    else
                    {
                        result = 1 == _extGoalAccessor.DeactivateExtinctionGoal(oldExtGoal.UserID_client, oldExtGoal.ExtGoalName, oldExtGoal.ExtGoalEntryDate);
                    }
                }
                if (oldExtGoal.AwardName != newExtGoal.AwardName || oldExtGoal.IncidentFrequency != newExtGoal.IncidentFrequency
                    || oldExtGoal.ExtGoalDescription != newExtGoal.ExtGoalDescription)
                {
                    result = (1 == _extGoalAccessor.UpdateExtinctionGoal(oldExtGoal, newExtGoal));
                    if (result == false)
                    {
                        throw new ApplicationException("Goal was not changed");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Extinction Goal could not be edited" + ex.InnerException);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// changed to extgoalviewmodel, added userID
        /// </remarks>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<ExtGoalViewModel> RetreiveAllExtGoals(int userID_client, bool active)
        {
            List<ExtGoalViewModel> extGoals = null;

            try
            {
                extGoals = _extGoalAccessor.SelectExtinctionGoalsByActive(userID_client, active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Extinction Goals not found" + ex.InnerException);
            }

            return extGoals;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/02
        /// ///
        /// Manage retrieving list of extinction goals by user ID
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="userID_client"></param>
        /// <returns></returns>
        public List<ExtGoalViewModel> RetrieveExtinctionGoalsByUserIDClient(int userID_client)
        {
            List<ExtGoalViewModel> extGoals = null;
            try
            {
                extGoals = _extGoalAccessor.SelectExtinctionGoalsByUserIDClient(userID_client);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Extinction Goals list not available.", ex);
            }
            return extGoals;
        }
    }
}

   
