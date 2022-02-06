/// <summary>
/// Becky Baenziger
/// Created: 2021/04/03
/// ///
/// View model to extinction goal table information, implements extgoal and adds user and awards
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
    public class ExtGoalViewModel : ExtGoal, ICloneable
    {
        public object Clone()
        {
            return new ExtGoalViewModel()
            {
                UserID_client = this.UserID_client,
                UserID_admin = this.UserID_admin,
                ExtGoalName = this.ExtGoalName,
                ExtGoalDescription = this.ExtGoalDescription,
                ExtGoalTargetDate = this.ExtGoalTargetDate,
                ExtGoalEntryDate = this.ExtGoalEntryDate,
                ExtGoalEditDate = this.ExtGoalEditDate,
                ExtGoalRemovalDate = this.ExtGoalRemovalDate,
                IncidentFrequency = this.IncidentFrequency,
                Active = this.Active,
                AwardName = this.AwardName,
                IncidentName = this.IncidentName

            };
        }
    }
}
