/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/02/19
/// 
/// The interface for managing the Routine Steps.
/// 
/// </summary>
///
/// <remarks>
/// </remarks>
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IRoutineStepManager
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
        List<RoutineStep> RetrieveAllRoutineSteps();
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The interface method to retrieve all active routine steps.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Whitney Vinson
        /// Updated: 2021/03/21
        /// Added Comments
        /// </remarks>
        /// <param name="active">The active status of the routine step</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Selects all routine steps by active</returns>
        List<RoutineStep> RetrieveRoutineStepsByActive(bool active = true);
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The interface method to retrieve routine step by routine name
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Whitney Vinson
        /// Updated: 2021/03/21
        /// Added Comments
        /// </remarks>
        /// <param name="name">The routine name that is being pulled from the database</param>
        /// <param name="active">The routine step active status</param>
        /// <exception cref="ApplicationException">Retieval Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>
        List<RoutineStep> RetrieveActiveRoutineStepsByRoutineName(string name, bool active = true);
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
        /// 
        /// </remarks>
        /// <param name="routineStep">The routine step that is being inserted</param>
        /// <exception cref="ApplicationException">Insert Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>
        bool AddNewRoutineStep(RoutineStep newRoutineStep);
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The interface method to update a routine step
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Whitney Vinson
        /// Updated: 2021/03/21
        /// 
        /// </remarks>
        /// <param name="oldRoutineStep">The old routine step that is being replaced</param>
        /// <param name="newRoutineStep">The routine step that is being inserted</param>
        /// <exception cref="ApplicationException">Update Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>
        bool EditRoutineStep(RoutineStep oldRoutineStep,
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
