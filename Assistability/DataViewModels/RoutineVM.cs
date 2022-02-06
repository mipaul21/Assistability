/// <summary>
/// William Clark
/// Created: 2021/02/26
/// 
/// The RoutineVM object class
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

namespace DataViewModels
{
    public class RoutineVM : Routine
    {
        public List<RoutineStep> Steps { get; set; }
        public RoutineVM(string name, string description, int userAccountID_Client, int userAccountID_Admin, bool active, List<RoutineStep> routineSteps) : base(name, description, userAccountID_Client, userAccountID_Admin, active)
        {
            Steps = routineSteps;
        }

        public RoutineVM(Routine routine, List<RoutineStep> steps) : base(routine.Name, routine.Description, routine.UserAccountID_Client, routine.UserAccountID_Admin, routine.Active, routine.EntryDate, routine.EditDate, routine.RemovalDate)
        {
            this.Steps = steps;
        }



    }
    public class CompleteRoutineVM : RoutineVM
    {
        public List<int> completedStepIds { get; set; }

        public CompleteRoutineVM(RoutineVM routine, List<int> completedStepIds) : base(routine, routine.Steps)
        {
            this.completedStepIds = completedStepIds;
        }
    }
}
