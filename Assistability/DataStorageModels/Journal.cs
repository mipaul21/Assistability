/// <summary>
/// Jory A. Wernette
/// Created: 2021/02/23
/// 
/// Class for the creation of Journal Objects with set data fields
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
    public class Journal
    {
        [Required]
        [Display(Name = "Journal Name")]
        public string JournalName { get; set; }
        [Required]
        [Display(Name = "Journal Description")]
        public string JournalDescription { get; set; }
        public int UserID_Client { get; set; }
    }
}