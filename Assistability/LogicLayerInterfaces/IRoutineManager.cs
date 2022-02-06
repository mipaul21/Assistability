/// <summary>
/// William Clark
/// Created: 2021/02/26
/// 
/// Interface for management of Routine objects
/// </summary>
///
/// <remarks>
/// </remarks>
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IRoutineManager
    {

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
        List<RoutineStep> GetRoutineStepsByRoutine(Routine routine);

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
        bool UpdateRoutine(Routine oldRoutine, Routine newRoutine);

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
        List<Routine> SelectActiveRoutinesByUserAccountIDClient(int userAccountID);

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
        bool CompleteRoutineStep(RoutineStep routineStep, UserAccount userAccount);

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
        bool CompleteRoutine(Routine routine, UserAccount userAccount);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/02/26
        /// 
        /// Selects a List of routines byuserid
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="user">The useraccountID for which to select all routines</param>
        /// <exception cref="ApplicationException">No Routines found</exception>
        /// <returns>A List of RoutineStep</returns>
        List<Routine> GetRoutinesByuserID(int userAccountID);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/11
        /// 
        /// Inserts a new routine object
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="newRoutine">The new routine to add</param>
        /// <exception cref="ApplicationException">No Routine added</exception>
        /// <returns>A List of RoutineStep</returns>
        bool AddNewRoutine(Routine newRoutine);

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
        /// <returns>True if routinestep updated</returns>
        bool UpdateRoutineStep(RoutineStep oldRoutineStep, RoutineStep newRoutineStep);

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
        bool SwapRoutineStepOrder(RoutineStep stepMovingBack, RoutineStep stepMovingForward);

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
        List<Routine> SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime selectedDate, int userAccountID);
    }
}
