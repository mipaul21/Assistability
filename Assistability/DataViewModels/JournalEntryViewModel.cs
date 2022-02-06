using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModels
{
    public class JournalEntryViewModel
    {
        public string JournalID { get; set; }
        public string JournalEntryBody { get; set; }
        public DateTime JournalEntryDate { get; set; }
        public DateTime? JournalEditDate { get; set; }
    }
}
