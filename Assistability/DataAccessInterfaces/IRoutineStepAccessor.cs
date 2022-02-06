/// <summary>
/// Your Name: Whitney Vinson
/// Created: 2021/02/19
/// 
/// The interface for accessing the Routine Steps.
/// 
/// </summary>
///
/// <remarks>
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/19
/// </remarks>
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IRoutineStepAccessor
    {
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The interface method to retrieve all routine steps.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>

        /// <returns>all routine steps</returns>

        List<RoutineStep> SelectAllRoutineSteps();

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The interface method to retrieve all routine steps.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="active">The active status of the routine step</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Selects all routine steps by active</returns>

        List<RoutineStep> SelectRoutineStepsByActive(bool active = true);

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The interface method to select routine steps by routine name
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Whitney Vinson
        /// Updated: 2021/03/19
        /// 
        /// </remarks>
        /// <param name="routineName">The name of the routine</param>
        /// <param name="active">The active status of the routine step</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>

        List<RoutineStep> SelectActiveRoutineStepsByRoutineName(string routineName, bool active = true);

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The interface method to insert a routine step
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Whitney Vinson
        /// Updated: 2021/03/21
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="routineStep">The routine step that is being inserted</param>
        /// <exception cref="ApplicationException">Insert Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>

        int InsertNewRoutineStep(RoutineStep routineStep);

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The accessor method to update a routine step.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldRoutineStep">Old routine step</param>
        /// <param name="newRoutineStep">New routine step</param>
        /// <exception cref="ApplicationException">Update Fails("Record not created")</exception>
        /// <returns>Returns rows affected</returns>
        /// 
        int UpdateRoutineStep(RoutineStep oldRoutineStep,
                                RoutineStep newRoutineStep);

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
        List<int> SelectRoutineStepCompletionsByDayByRoutineName(string name, DateTime date);

    }
}
