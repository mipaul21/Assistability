/// <summary>
/// Ryan Taylor
/// Created: 2021/03/29
/// 
/// This is a storage model for the Performance Objects
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataStorageModels
{
    public class Performance
    {
        [Required]
        [Display(Name = "Performance Name")]
        public string PerformanceName { get; set; }
        [Required]
        [Display(Name = "Performance Description")]
        public string PerformanceDescription { get; set; }
        [Display(Name = "Client ID")]
        public int UserID_client { get; set; }
        [Display(Name = "Creator ID")]
        public int UserIDCreator { get; set; }
        [Display(Name = "Entry Date")]
        public DateTime PerformanceEntryDate { get; set; }
        [Display(Name = "Edit Date")]
        public DateTime? PerformanceEditDate { get; set; }
        [Display(Name = "Removal Date")]
        public DateTime? PerformanceRemovalDate { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
