/// <summary>
/// Becky Baenziger
/// Created: 2021/02/18
/// ///
/// View model to hab goal table information, implements habgoal and adds user and awards
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
    public class HabGoalViewModel : HabGoal, ICloneable
    {
        public object Clone()
        {
            return new HabGoalViewModel() { 
                UserID_client = this.UserID_client,
                UserID_admin = this.UserID_admin,
                HabGoalName = this.HabGoalName,
                HabGoalDescription = this.HabGoalDescription,
                HabGoalTargetDate = this.HabGoalTargetDate,
                HabGoalEntryDate = this.HabGoalEntryDate,
                HabGoalEditDate = this.HabGoalEditDate,
                HabGoalRemovalDate = this.HabGoalRemovalDate,
                RoutineFrequency = this.RoutineFrequency,
                Active = this.Active,
                AwardName = this.AwardName,
                RoutineName = this.RoutineName
                
            };
        }


    }
}
