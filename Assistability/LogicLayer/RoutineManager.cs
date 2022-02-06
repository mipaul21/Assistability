/// <summary>
/// William Clark
/// Created: 2021/02/24
/// 
/// Interface for management of UserGroup objects
/// </summary>
///
/// <remarks>
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

namespace LogicLayer
{
    public class RoutineManager : IRoutineManager
    {
        private IRoutineAccessor _routineAccessor;

        public RoutineManager()
        {
            _routineAccessor = new RoutineAccessor();
        }

        public RoutineManager(IRoutineAccessor routineAccessor)
        {
            this._routineAccessor = routineAccessor;
        }

        

        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        /// 
        /// Selects a List of RoutineSteps of which the Routine is composed
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The Routine for which to select all RoutineSteps</param>
        /// <exception cref="ApplicationException">No RoutineSteps found</exception>
        /// <returns>A List of RoutineStep</returns>
        public List<RoutineStep> GetRoutineStepsByRoutine(Routine routine)
        {
            List<RoutineStep> routineSteps = new List<RoutineStep>();
            try
            {
                routineSteps = _routineAccessor.SelectRoutineStepsByRoutine(routine);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Routine Steps could not be found" + ex.Message);
            }
            return routineSteps;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/04
        /// 
        /// Updates a specific Routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="oldRoutine">The Routine to update</param>
        /// <param name="newRoutine">The New Routine</param>
        /// <exception cref="ApplicationException">Routine could not be udpated</exception>
        /// <returns>If the routine was successfully updated</returns>
        public bool UpdateRoutine(Routine oldRoutine, Routine newRoutine)
        {
            bool result = false;
            try
            {
                result = _routineAccessor.UpdateRoutine(oldRoutine, newRoutine);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Routine could not be updated." + ex.Message);
            }
            return result;
        }


        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Selects all active routines for a UserAccount listed as the Client
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountId of the client</param>
        /// <exception cref="ApplicationException">Routines could not be found</exception>
        /// <returns>A list of Routines</returns>
        public List<Routine> SelectActiveRoutinesByUserAccountIDClient(int userAccountID)
        {
            List<Routine> routines = null;
            try
            {
                routines = _routineAccessor.SelectActiveRoutinesByUserAccountIDClient(userAccountID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Routines could not be found" + ex.Message);
            }
            return routines;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/11
        /// 
        /// Creates a RoutineStepCompletion
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routineStep">The RoutineStep to complete</param>
        /// <param name="userAccount">The User who has completed the routine step</param>
        /// <exception cref="ApplicationException">RoutineStep could not be completed</exception>
        /// <returns>True if completion stored</returns>
        public bool CompleteRoutineStep(RoutineStep routineStep, UserAccount userAccount)
        {
            bool result = false;
            try
            {
                result = _routineAccessor.CreateRoutineStepCompletion(routineStep, userAccount);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Routine step could not be completed" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Creates a RoutineCompletion
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The Routine to complete</param>
        /// <param name="userAccount">The User who has completed the routine</param>
        /// <exception cref="ApplicationException">Routine could not be completed</exception>
        /// <returns>True if completion stored</returns>
        public bool CompleteRoutine(Routine routine, UserAccount userAccount)
        {
            bool result = false;
            try
            {
                result = _routineAccessor.CreateRoutineCompletion(routine, userAccount);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Routine could not be completed" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/02/26
        /// 
        /// Selects a List of Routines by the selected userid
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The Routine for which to select all Routines</param>
        /// <exception cref="ApplicationException">No Routines found</exception>
        /// <returns>A List of Routines</returns>
        public List<Routine> GetRoutinesByuserID(int userAccountID)
        {
            List<Routine> routines = null;

            try
            {
                routines = _routineAccessor.SelectRoutinesByUserID(userAccountID);


                if (routines == null)
                {
                    routines = new List<Routine>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }

            return routines;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/11
        /// 
        /// inserts a new routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="newRoutine">The Routine to insert</param>
        /// <exception cref="ApplicationException">The Routine was not added.</exception>
        /// <returns>a bool if it was added or not</returns>
        public bool AddNewRoutine(Routine newRoutine)
        {
            bool result = false;
            ;

            try
            {
                result = _routineAccessor.InsertNewRoutine(newRoutine);
                if (result == false)
                {
                    throw new ApplicationException("New routine was not added.");
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add new routine failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/31
        /// 
        /// Update a routine step
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldRoutineStep">The routine step to be updated</param>
        /// <param name="newRoutineStep">The routine step to be updated</param>
        /// <exception cref="ApplicationException">RoutineStep could not be updated</exception>
        /// <returns>Rows affected</returns>
        public bool UpdateRoutineStep(RoutineStep oldRoutineStep, RoutineStep newRoutineStep)
        {
            bool result = false;
            try
            {
                result = _routineAccessor.UpdateRoutineStep(oldRoutineStep, newRoutineStep);
            }
            catch (Exception ex)
            {

                throw ex;

            }
            return result;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/31
        /// 
        /// Swaps the order of two routine steps
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="stepMovingBack">The routine step to be moved backward in the order</param>
        /// <param name="stepMovingForward">The routine step to be moved forward in the order</param>
        /// <exception cref="ApplicationException">RoutineStep could not be updated</exception>
        /// <returns>True if steps' order swapped</returns>
        public bool SwapRoutineStepOrder(RoutineStep stepMovingBack, RoutineStep stepMovingForward)
        {
            bool result = false;
            try
            {
                if (_routineAccessor.SwapRoutineStepOrder(stepMovingBack, stepMovingForward))
                {
                    result = _routineAccessor.SwapRoutineStepOrder(stepMovingForward, stepMovingBack);
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/01
        /// 
        /// Selects all active routines for a UserAccount listed as the Client
        /// which do not have a record of completion for the current day
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The UserAccountId of the client</param>
        /// <exception cref="ApplicationException">Routines could not be found</exception>
        /// <returns>A list of Routines</returns>
        public List<Routine> SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime selectedDate, int userAccountID)
        {
            List<Routine> result = new List<Routine>();
            try
            {
                result = _routineAccessor.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(selectedDate, userAccountID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}

