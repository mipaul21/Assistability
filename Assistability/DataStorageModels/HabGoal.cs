/// <summary>
/// Becky Baenizger
/// Created: 2021/02/02
/// Attainment goal
/// </summary>
/// <remarks>
///  Updater Name:
///  Update Date:
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static DataStorageModels.CustomDataAnnotations;

namespace DataStorageModels
{
    public class HabGoal
    {
        [Display(Name = "Selected User")]
        public int UserID_client { get; set; }  // foreign key
        [Display(Name = "Current User")]
        public int UserID_admin { get; set; }  // foreign key
        [Display(Name = "Goal Name")]
        public string HabGoalName { get; set; }
        [Display(Name = "Goal Description")]
        public string HabGoalDescription { get; set; }
        [Display(Name = "Target Completion Date")]
        [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime HabGoalTargetDate { get; set; }
        [Display(Name = "Goal Created Date")]
        public DateTime HabGoalEntryDate { get; set; }
        [Display(Name = "Date Goal Edited")]
        public DateTime? HabGoalEditDate { get; set; }
        [Display(Name = "Date Goal Deactivated")]
        public DateTime? HabGoalRemovalDate { get; set; }
        [Display(Name = "Routine Frequency")]
        public int RoutineFrequency { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Award Name")]
        public string AwardName { get; set; }  // foreign key
        [Display(Name = "Routine Name")]
        public string RoutineName { get; set; } // foreign key
    }
}
