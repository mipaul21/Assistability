/// <summary>
/// Becky Baenziger
/// Created: 2021/03/28
/// ///
/// View model to attgoal table information, implements attgoal and adds user and awards
/// </summary>
/// <remarks>
/// Updater Name: Becky Baenziger
/// Update Date: 2021/04/27
/// ///
/// Removed attached classes because no longer needed.  Kept because that alot of code to go through and change
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorageModels;

namespace DataViewModels
{
    public class AttGoalViewModel : AttGoal, ICloneable
    {
        public object Clone()
    {
        return new AttGoalViewModel()
        {
            UserID_client = this.UserID_client,
            UserID_admin = this.UserID_admin,
            AttGoalName = this.AttGoalName,
            AttGoalDescription = this.AttGoalDescription,
            AttGoalTargetDate = this.AttGoalTargetDate,
            AttGoalEntryDate = this.AttGoalEntryDate,
            AttGoalEditDate = this.AttGoalEditDate,
            AttGoalRemovalDate = this.AttGoalRemovalDate,
            PerformanceFrequency = this.PerformanceFrequency,
            Active = this.Active,
            AwardName = this.AwardName,
            PerformanceName = this.PerformanceName

        };
    }
}
}
