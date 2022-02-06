/// <summary>
/// William Clark
/// Created: 2021/02/26
/// 
/// Interface for the UserGroupAccessor
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

namespace DataAccessInterfaces
{
    public interface IRoutineAccessor
    {

        /// <summary>
        /// William Clark
        /// Created: 2021/02/26
        /// 
        /// Selects a list of RoutineSteps objects from the database assigned to the Routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The Routine for which to select all RoutineSteps assigned</param>
        /// <exception>No RoutineSteps found</exception>
        /// <returns>A list of RoutineStep objects</returns>
        List<RoutineStep> SelectRoutineStepsByRoutine(Routine routine);

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
        /// <param name="userAccount">The User who completed the step</param>
        /// <exception cref="ApplicationException">RoutineStep could not be completed</exception>
        /// <returns>True if completion stored</returns>
        bool CreateRoutineStepCompletion(RoutineStep routineStep, UserAccount userAccount);

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
        /// <param name="userAccount">The User who completed the step</param>
        /// <exception cref="ApplicationException">Routine could not be completed</exception>
        /// <returns>True if completion stored</returns>
        bool CreateRoutineCompletion(Routine routine, UserAccount userAccount);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/11
        /// 
        /// Selects routines by the supplied userID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The ID of the user</param>
        /// <exception cref="ApplicationException">Routines could not be retrieved</exception>
        /// <returns>List of routines</returns>
        List<Routine> SelectRoutinesByUserID(int userAccountID);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/11
        /// 
        /// inserts the supplied routine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The routine to add</param>
        /// <exception cref="ApplicationException">Routine could not be added</exception>
        /// <returns>The name of the added routine</returns>
        bool InsertNewRoutine(Routine routine);

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
