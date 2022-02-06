/// <summary>
/// Your Name: Whitney Vinson
/// Created: 2021/02/19
/// 
/// The methods for managing the Routine Steps.
/// 
/// </summary>
///
/// <remarks>
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/19
/// Added Comments
/// </remarks>

using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class RoutineStepManager : IRoutineStepManager
    {
        private IRoutineStepAccessor _routineStepAccessor;

        public RoutineStepManager()
        {
            _routineStepAccessor = new RoutineStepAccessor();
        }

        public RoutineStepManager(IRoutineStepAccessor routineStepAccessor)
        {
            _routineStepAccessor = routineStepAccessor;

        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The method to add a routine step.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newRoutineStep">The brand new routine step.</param>
        /// <exception cref="ApplicationException">Insert Fails ("New routine step was not added.")</exception>
        /// <returns>Rows edited</returns>
        public bool AddNewRoutineStep(RoutineStep newRoutineStep)
        {
            bool result = false;
            int newRoutineStepID = 0;
            try
            {
                newRoutineStepID = _routineStepAccessor.InsertNewRoutineStep(newRoutineStep);
                if (newRoutineStepID == 0)
                {
                    throw new ApplicationException("New routine step was not added.");
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Routine Step Failed.", ex);
            }
            return result;
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The method to update a routine step.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldRoutineStep">The old routine step.</param>
        /// <param name="newRoutineStep">The brand new routine step.</param>
        /// <exception cref="ApplicationException">Update Failed ("New routine step was not updated.")</exception>
        /// <returns>Rows edited</returns>
        /// 
        public bool EditRoutineStep(RoutineStep oldRoutineStep, RoutineStep newRoutineStep)
        {
            bool result = false;
            try
            {
                result = (1 == _routineStepAccessor.UpdateRoutineStep(oldRoutineStep, newRoutineStep));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }
            /// <summary>
            /// Your Name: Whitney Vinson
            /// Created: 2021/02/19
            /// 
            /// The method to retrieve all routine steps.
            /// </summary>
            ///
            /// <remarks>
            /// </remarks>
            /// <exception cref="ApplicationException">Retrieval failed ("Data not Available")</exception>
            /// <returns>List of Routine Steps</returns>
            public List<RoutineStep> RetrieveAllRoutineSteps()
        {
            List<RoutineStep> data = null;

            try
            {
                data = _routineStepAccessor.SelectAllRoutineSteps();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available.", ex);
            }
            return data;
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The method to retrieve all active routine steps.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <exception cref="ApplicationException">Retrieval Fails ("No active steps found")</exception>
        /// <returns>Returns list of active steps</returns>
        public List<RoutineStep> RetrieveRoutineStepsByActive(bool active = true)
        {
            List<RoutineStep> activeSteps = null;

            try
            {
                activeSteps = _routineStepAccessor.SelectRoutineStepsByActive(active);
            }
            catch (Exception)
            {
                throw new ApplicationException("No active steps found.");
            }
            return activeSteps;
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The method to retrieve all active routine steps by routine name.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <exception cref="ApplicationException">Retrieval Fails ("No active steps found")</exception>
        /// <returns>Returns list of active steps by routine name</returns>
        public List<RoutineStep> RetrieveActiveRoutineStepsByRoutineName(string routineName, bool active = true)
        {
            List<RoutineStep> activeSteps = null;

            try
            {
                activeSteps = _routineStepAccessor.SelectActiveRoutineStepsByRoutineName(routineName, active);
            }
            catch (Exception)
            {
                throw new ApplicationException("No active steps found.");
            }
            return activeSteps;

        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/08
        /// 
        /// The interface method to routine step completions by day by routine name
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="name">The routine name that is being pulled from the database</param>
        /// <param name="date">The day for which to select completions</param>
        /// <exception cref="ApplicationException">Retieval Fails ("Data not Available")</exception>
        /// <returns>List of RoutineStepId's which have been completed on a given date</returns>
        public List<int> SelectRoutineStepCompletionsByDayByRoutineName(string name, DateTime date)
        {
            List<int> result = new List<int>();
            try
            {
                result = _routineStepAccessor.SelectRoutineStepCompletionsByDayByRoutineName(name, date);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
