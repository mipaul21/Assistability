/// <summary>
/// Becky Baenizger
/// Created: 2021/03/28
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
    public class AttGoal
    {
        [Display(Name = "Selected User")]
        public int UserID_client { get; set; }  // foreign key
        [Display(Name = "Current User")]
        public int UserID_admin { get; set; }  // foreign key
        [Display(Name = "Goal Name")]
        public string AttGoalName { get; set; }
        [Display(Name = "Goal Description")]
        public string AttGoalDescription { get; set; }
        [Display(Name = "Target Completion Date")]
        [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime AttGoalTargetDate { get; set; }
        [Display(Name = "Goal Creation Date")]
        public DateTime AttGoalEntryDate { get; set; }
        [Display(Name = "Date Goal Edited")]
        public DateTime? AttGoalEditDate { get; set; }
        [Display(Name = "Date Goal Deactivated")]
        public DateTime? AttGoalRemovalDate { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Performance Frequency")]
        public int PerformanceFrequency { get; set; }
        [Display(Name = "Award Name")]
        public string AwardName { get; set; }  // foreign key
        [Display(Name = "Performance Name")]
        public string PerformanceName { get; set; } // foreign key

    }
}
