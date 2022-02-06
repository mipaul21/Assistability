/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/08
/// <remarks>
/// Updater Name: Becky Baenziger
/// Update Date: 2021/04/23
/// ///
/// Updated to relfect changes to the award table.  Removed AwardID, GoalID, GoalTypeID
/// </remarks>
/// This is a storage model for the Award Objects
/// </summary>
/// 
/// <remarks>
/// Jory Wernette
/// Updated: 2021/04/27
/// Added display names
/// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
    public class Award
    {
        [Display(Name = "Award Name")]
        public string AwardName { get; set; }

        [Display(Name = "Award Description")]
        public string AwardDescription { get; set; }
        public bool? Active { get; set; }
    }
}
