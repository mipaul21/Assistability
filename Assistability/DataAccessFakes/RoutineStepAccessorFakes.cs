/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/02/19
/// 
/// The data fakes for the routine steps.
/// 
/// </summary>
///
/// <remarks>
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/19
/// </remarks>

using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class RoutineStepAccessorFake : IRoutineStepAccessor
    {
        List<RoutineStep> data = new List<RoutineStep>();

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The data and set up for the fakes info.
        /// 
        /// </summary>

        public RoutineStepAccessorFake()
        {
            data.Add(new RoutineStepViewModel()
            {
                StepID = 1,
                RoutineStepID = 1,
                RoutineName = "FirstRoutine",
                RoutineStepName = "Task1",
                RoutineStepDescription = "The first fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-01"),
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = 1,
                Active = true

            });
            data.Add(new RoutineStepViewModel()
            {
                StepID = 2,
                RoutineStepID = 2,
                RoutineName = "FirstRoutine",
                RoutineStepName = "Task2",
                RoutineStepDescription = "The second fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = DateTime.Parse("2021-02-02"),
                RoutineStepRemovalDate = DateTime.Parse("2021-03-01"),
                RoutineStepOrderNumber = 2,
                Active = true
            });
            data.Add(new RoutineStepViewModel()
            {
                StepID = 3,
                RoutineStepID = 3,
                RoutineName = "FirstRoutine",
                RoutineStepName = "Task3",
                RoutineStepDescription = "The third fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = null,
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = 3,
                Active = true
            });
            data.Add(new RoutineStepViewModel()
            {
                StepID = 4,
                RoutineStepID = 4,
                RoutineName = "FirstRoutine",
                RoutineStepName = "Task4",
                RoutineStepDescription = "The fourth fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = null,
                RoutineStepRemovalDate = DateTime.Parse("2021-03-03"),
                RoutineStepOrderNumber = 4,
                Active = false
            });
            data.Add(new RoutineStepViewModel()
            {
                StepID = 5,
                RoutineStepID = 5,
                RoutineName = "FirstRoutine",
                RoutineStepName = "Task5",
                RoutineStepDescription = "The fifth fake routine step.",
                RoutineStepEntryDate = DateTime.Parse("2021-01-01"),
                RoutineStepEditDate = null,
                RoutineStepRemovalDate = null,
                RoutineStepOrderNumber = 5,
                Active = false
            });
        }
        /// <summary>
		/// Your Name: Whitney Vinson
		/// Created: 2021/02/19
		/// 
		/// The fake method for inserting a routine step.
        /// 
		/// </summary>
        public int InsertNewRoutineStep(RoutineStep routineStep)
        {
            var count = data.Count;
            data.Add(routineStep);
            return this.data.Count - count;
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/02/19
        /// 
        /// The fake method for updating a routine step.
        /// 
        /// </summary>
        public int UpdateRoutineStep(RoutineStep oldRoutineStep,
            RoutineStep newRoutineStep)
        {
            int rowsAffected = 0;

            data.Remove(oldRoutineStep);
            data.Add(newRoutineStep);
            if (data.IndexOf(newRoutineStep) > -1)
            {
                rowsAffected = 1;
            }

            return rowsAffected;
        }
        /// <summary>
		/// Your Name: Whitney Vinson
		/// Created: 2021/02/19
		/// 
		/// The fake method for selecting all routine steps.
        /// 
		/// </summary>
        public List<RoutineStep> SelectAllRoutineSteps()
        {
            return this.data;
        }
        /// <summary>
		/// Your Name: Whitney Vinson
		/// Created: 2021/02/19
		/// 
		/// The fake method for selecting routine steps by active.
        /// 
		/// </summary>
        public List<RoutineStep> SelectRoutineStepsByActive(bool active = true)
        {
            List<RoutineStep> activeRoutineSteps = data.FindAll(rs => rs.Active == active);

            return activeRoutineSteps;
        }
        /// <summary>
		/// Your Name: Whitney Vinson
		/// Created: 2021/02/19
		/// 
		/// The fake method for selecting routine steps by active and by their routine name.
        /// 
		/// </summary>
        public List<RoutineStep> SelectActiveRoutineStepsByRoutineName(string name, bool active)
        {
            List<RoutineStep> routineName = data.FindAll(rn => rn.RoutineName == name);
            List<RoutineStep> routineActiveSteps = routineName.FindAll(ras => ras.Active == active);

            return routineActiveSteps;
        }

        public List<int> SelectRoutineStepCompletionsByDayByRoutineName(string name, DateTime date)
        {
            List<int> result = new List<int>();
            if (name == "Fake Routine Name")
            {
                if (date.Day == DateTime.Today.Date.Day)
                {

                    result = new List<int>() {1};
                }
                else
                {
                    return result;
                }
            }
            else
            {
                throw new ApplicationException("Data could not be found");
            }
            return result;
        }
    }
}
