using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
    public class JournalEntry
    {
        public int UserIDClient { get; set; }
        public int UserIDClientJournal { get; set; }
        [Display(Name = "Journal")]
        public string JournalID { get; set; }
        [Required]
        [Display(Name = "My Thoughts")]
        public string JournalEntryBody { get; set; }
        public DateTime JournalEntryDate { get; set; }
        public DateTime? JournalEditDate { get; set; }
    }
}
