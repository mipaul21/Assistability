/// <summary>
/// William Clark
/// Created: 2021/02/26
/// 
/// The RoutineStep object class
/// </summary>
///
/// <remarks>
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/12
/// Added correct date names to constructor.
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/19
/// Added empty constructor
/// </remarks>
/// 
///< remarks >
/// Ryan Taylor
/// Updated: 2021/04/22
/// added required to variables that needed user input along with Display
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataStorageModels
{
    public class RoutineStep
    {
        [Required]
        [Display(Name = "Routine Step ID")]
        public int RoutineStepID { get; set; }
        [Required]
        [Display(Name = "Routine Name")]
        public String RoutineName { get; set; }
        [Required]
        [Display(Name = "Routine Step Name")]
        public String RoutineStepName { get; set; }
        [Required]
        [Display(Name = "Routine Step Description")]
        public String RoutineStepDescription { get; set; }
        [Required]
        [Display(Name = "Routine Step Entry Date")]
        public DateTime RoutineStepEntryDate { get; set; }
        [Display(Name = "Routine Step Edit Date")]
        public DateTime? RoutineStepEditDate { get; set; }
        [Display(Name = "Routine Step Removal Date")]
        public DateTime? RoutineStepRemovalDate { get; set; }
        [Required]
        [Display(Name = "Routine Step Order")]
        public int RoutineStepOrderNumber { get; set; }
        [Required]
        public bool Active { get; set; }

        public RoutineStep()
        {
        }

        public RoutineStep(int routineStepID, string routineName, 
            string routineStepName, string routineStepDescription, 
            DateTime routineStepEntryDate, int routineStepOrderNumber, 
            bool active)
        {
            RoutineStepID = routineStepID;
            RoutineName = routineName;
            RoutineStepName = routineStepName;
            RoutineStepDescription = routineStepDescription;
            RoutineStepEntryDate = routineStepEntryDate;
            RoutineStepOrderNumber = routineStepOrderNumber;
            Active = active;
        }

        public RoutineStep(int routineStepID, string routineName, 
            string routineStepName, string routineStepDescription, 
            DateTime routineStepEntryDate, DateTime? routineStepEditDate, 
            DateTime? routineStepRemovalDate, int routineStepOrderNumber, 
            bool active)
        {
            RoutineStepID = routineStepID;
            RoutineName = routineName;
            RoutineStepName = routineStepName;
            RoutineStepDescription = routineStepDescription;
            RoutineStepEntryDate = routineStepEntryDate;
            RoutineStepEditDate = routineStepEditDate;
            RoutineStepRemovalDate = routineStepRemovalDate;
            RoutineStepOrderNumber = routineStepOrderNumber;
            Active = active;
        }

        public RoutineStep(string routineName,
            string routineStepName, string routineStepDescription,
            DateTime routineStepEntryDate, int routineStepOrderNumber,
            bool active)
        {
            RoutineName = routineName;
            RoutineStepName = routineStepName;
            RoutineStepDescription = routineStepDescription;
            RoutineStepEntryDate = routineStepEntryDate;
            RoutineStepOrderNumber = routineStepOrderNumber;
            Active = active;
        }

        public RoutineStep(RoutineStep oldRoutineStep)
        {
            this.RoutineStepID = oldRoutineStep.RoutineStepID;
            this.RoutineName = oldRoutineStep.RoutineName;
            this.RoutineStepName = oldRoutineStep.RoutineStepName;
            this.RoutineStepDescription = oldRoutineStep.RoutineStepDescription;
            this.RoutineStepEntryDate = oldRoutineStep.RoutineStepEntryDate;
            this.RoutineStepEditDate = oldRoutineStep.RoutineStepEditDate;
            this.RoutineStepRemovalDate = oldRoutineStep.RoutineStepRemovalDate;
            this.RoutineStepOrderNumber = oldRoutineStep.RoutineStepOrderNumber;
            this.Active = oldRoutineStep.Active;
        }
    }
}
