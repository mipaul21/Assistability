/// <summary>
/// William Clark
/// Created: 2021/02/26
/// 
/// Fake UserGroupAccessor for testing
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class RoutineFakes : IRoutineAccessor
    {
        private Routine fakeRoutine = new Routine("FirstRoutine", "First Routine", new DateTime(2021, 2, 26), 3, 1, true);
        private RoutineStep fakeRoutineStep1 = new RoutineStep(1, "FirstRoutine", "FirstStep", "The First Step", new DateTime(2021, 2, 26), 1, true);
        private RoutineStep fakeRoutineStep2 = new RoutineStep(2, "FirstRoutine", "SecondStep", "The Second Step", new DateTime(2021, 2, 26), 2, true);
        private RoutineStep fakeRoutineStep3 = new RoutineStep(3, "FirstRoutine", "ThirdStep", "The Third Step", new DateTime(2021, 2, 26), 3, true);
        private RoutineStep fakeRoutineStep4 = new RoutineStep(4, "FirstRoutine", "FourthStep", "The Fourth Step", new DateTime(2021, 2, 26), 3, true);

        private RoutineVM fakeRoutineVM;

        private Dictionary<string, DateTime> fakeRoutineCompletions = new Dictionary<string, DateTime>();
        private Dictionary<string, DateTime> fakeRoutineStepCompletions = new Dictionary<string, DateTime>();

        public RoutineFakes()
        {
            fakeRoutineVM = new RoutineVM(fakeRoutine, new List<RoutineStep>());
            fakeRoutineVM.Steps.Add(fakeRoutineStep1);
            fakeRoutineVM.Steps.Add(fakeRoutineStep2);
            fakeRoutineVM.Steps.Add(fakeRoutineStep3);
            fakeRoutineVM.Steps.Add(fakeRoutineStep4);
        }
        public List<Routine> SelectActiveRoutinesByUserAccountIDClient(int userAccountID)
        {
            if (userAccountID == 3)
            {
                return new List<Routine>() {
                    new Routine("FirstRoutine", "First Routine", new DateTime(2021, 2, 26), 3, 1, true)
                };
            }
            else
            {
                throw new ApplicationException("User not found");
            }
            
        }
        public List<RoutineStep> SelectRoutineStepsByRoutine(Routine routine)
        {
            
            if (routine.Name.Equals(fakeRoutine.Name))
            {
                return fakeRoutineVM.Steps;
                
            }
            else
            {
                throw new ApplicationException("Routine could not be found.");
            }
        }

        public bool UpdateRoutine(Routine oldRoutine, Routine newRoutine)
        {
            bool result = false;
            if (oldRoutine.Name == "FirstRoutine" && newRoutine.Name == "FirstRoutine")
            {
                fakeRoutine.Description = newRoutine.Description;
                fakeRoutine.EditDate = newRoutine.EditDate;
                fakeRoutine.Active = newRoutine.Active;
                if (!fakeRoutine.Active)
                {
                    fakeRoutine.RemovalDate = DateTime.Now;
                }
                result = true;
            }
            else
            {
                throw new ApplicationException("Routine not found");
            }
            return result;
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
        /// <param name="userAccount">The User who completed the step</param>
        /// <exception cref="ApplicationException">RoutineStep could not be completed</exception>
        /// <returns>True if completion stored</returns>
        public bool CreateRoutineStepCompletion(RoutineStep routineStep, UserAccount userAccount)
        {
            if (userAccount.UserAccountID != 3)
            {
                throw new ApplicationException("User not found");
            }
            else if (routineStep.RoutineStepOrderNumber > 5 || routineStep.RoutineStepID < 0 || routineStep.RoutineStepID > 4)
            {
                throw new ApplicationException("Routine step not found");
            }
            else
            {
                fakeRoutineStepCompletions.Add(routineStep.RoutineStepName, DateTime.Now);
                return true;
                
            }
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
        /// <param name="userAccount">The User who completed the step</param>
        /// <exception cref="ApplicationException">User not found</exception>
        /// <exception cref="ApplicationException">Routine not found</exception>
        /// <returns>True if completion stored</returns>
        public bool CreateRoutineCompletion(Routine routine, UserAccount userAccount)
        {
            if (routine.Name.Equals("FirstRoutine"))
            {
                if (userAccount.UserAccountID  < 4)
                {
                    fakeRoutineCompletions.Add(routine.Name, DateTime.Now);
                    return true;
                }
                else
                {
                    throw new ApplicationException("User not found");
                }
                
            }
            else
            {
                throw new ApplicationException("Routine not found");
            }
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// Tests the method for the selectRoutinesByUserID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="userAccountID">The Routine to complete</param>
        /// <exception cref="ApplicationException">Routine not found</exception>
        /// <returns>A list of Routine objects</returns>
        public List<Routine> SelectRoutinesByUserID(int userAccountID)
        {
            List<Routine> routines = new List<Routine>();
            List<Routine> newroutinelist = new List<Routine>();
            routines.Add(fakeRoutine);
            for (int i = 0; i < routines.Count; i++)
            {
                foreach (var routine in routines)
                {
                    if (routine.UserAccountID_Client == userAccountID)
                    {
                        newroutinelist.Add(routines[i]);
                        i++;
                    }
                    break;
                }

            }
            return newroutinelist;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// Tests the method for the InsertNewRoutine
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="routine">The Routine to add</param>
        /// <exception cref="ApplicationException">Routine not added</exception>
        /// <returns> True if the the routine was added.</returns>
        public bool InsertNewRoutine(Routine routine)
        {
            List<Routine> routines = new List<Routine>();
            routines.Add(routine);
            if (routines[0].Name == "SecondRoutine")
            {
                return true;
            }
            return false;
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
            RoutineStep stepToUpdate = fakeRoutineVM.Steps.Find(step => step.RoutineStepID == oldRoutineStep.RoutineStepID);
            if (stepToUpdate != null)
            {
                for (int i = 0; i < fakeRoutineVM.Steps.Count; i++)
                {
                    if (fakeRoutineVM.Steps[i].RoutineStepOrderNumber == newRoutineStep.RoutineStepOrderNumber)
                    {
                        fakeRoutineVM.Steps[i].RoutineStepOrderNumber = oldRoutineStep.RoutineStepOrderNumber;
                    }

                }
                for (int i = 0; i < fakeRoutineVM.Steps.Count; i++)
                {
                    if (fakeRoutineVM.Steps[i].RoutineStepID == oldRoutineStep.RoutineStepID)
                    {
                        fakeRoutineVM.Steps[i] = newRoutineStep;
                    }

                }
                result = true;
            }
            else
            {
                throw new ApplicationException("Step not found.");
            }
            return result;
        }

        public bool SwapRoutineStepOrder(RoutineStep stepMovingBack, RoutineStep stepMovingForward)
        {
            bool result = false;
            for (int i = 0; i < fakeRoutineVM.Steps.Count; i++)
            {
                if (fakeRoutineVM.Steps[i].RoutineStepID == stepMovingBack.RoutineStepID)
                {
                    fakeRoutineVM.Steps[i].RoutineStepOrderNumber = stepMovingForward.RoutineStepOrderNumber;
                    result = true;
                }
                if (fakeRoutineVM.Steps[i].RoutineStepID == stepMovingForward.RoutineStepID)
                {
                    fakeRoutineVM.Steps[i].RoutineStepOrderNumber = stepMovingBack.RoutineStepOrderNumber - 1;
                    result = true;
                }

            }
            if (!result)
            {
                throw new ApplicationException("Steps not found");
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
            if (userAccountID == 3)
            {
                if (!fakeRoutineCompletions.ContainsKey("FirstRoutine"))
                {
                    return new List<Routine>() {
                        new Routine("FirstRoutine", "First Routine", new DateTime(2021, 2, 26), 3, 1, true)
                    };
                }
                else
                {
                    foreach (var completion in fakeRoutineCompletions)
                    {
                        if (completion.Value >= selectedDate.AddDays(-1) && selectedDate <= selectedDate.AddDays(1))
                        {
                            return new List<Routine>();
                        }
                    }
                    return new List<Routine>() {
                        new Routine("FirstRoutine", "First Routine", new DateTime(2021, 2, 26), 3, 1, true)
                    };
                }
            }
            else
            {
                throw new ApplicationException("User not found");
            }
        }
    }
}
