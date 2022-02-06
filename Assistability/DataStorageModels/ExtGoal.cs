using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static DataStorageModels.CustomDataAnnotations;

namespace DataStorageModels
{
    public class ExtGoal
    {


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/02
        /// added IncidentCompletionCount, awardID
        /// </remarks>

        [Display(Name = "Selected User")]
        public int UserID_client { get; set; }
        [Display(Name = "Current User")]
        public int UserID_admin { get; set; }
        [Display(Name = "Goal Name")]
        public string ExtGoalName { get; set; }
        [Display(Name = "Goal Description")]
        public string ExtGoalDescription { get; set; }
        [Display(Name = "Target Completion Date")]
        [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime ExtGoalTargetDate { get; set; }
        [Display(Name = "Goal Creation Date")]
        public DateTime ExtGoalEntryDate { get; set; }
        [Display(Name = "Date Goal Edited")]
        public DateTime? ExtGoalEditDate { get; set; }
        [Display(Name = "Date Goal Deactivated")]
        public DateTime? ExtGoalRemovalDate { get; set; }
        [Display(Name = "Incident Frequency")]
        public int IncidentFrequency { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Award Name")]
        public string AwardName { get; set; }
        [Display(Name = "Incident Name")]
        public string IncidentName { get; set; }

        //public ExtGoal()
        //{
        //}

        //public ExtGoal(string ExtGoalName, string incidentName, string extGoalDescription, DateTime ExtGoalEntryDate, DateTime? extGoalEditDate, DateTime extGoalTargetDate, DateTime? extGoalRemovalDate, int UserIDCreator, int UserIDClient, bool active)
        //{
        //}

    }
}
