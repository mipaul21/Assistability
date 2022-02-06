/// <summary>
/// William Clark
/// Created: 2021/02/04
/// 
/// The Routine object class
/// </summary>
/// 
///< remarks >
/// Ryan Taylor
/// Updated: 2021/04/16
/// added required to variables that needed user input
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataStorageModels
{
    public class Routine
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        public int UserAccountID_Client { get; private set; }
        public int UserAccountID_Admin { get; private set; }
        public DateTime EntryDate { get; private set; }
        public DateTime? EditDate { get; set; }
        public DateTime? RemovalDate { get; set; }
        public bool Active { get; set; }

        public Routine(string name, string description, int userAccountID_Client, int userAccountID_Admin, bool active)
        {
            Name = name;
            Description = description;
            UserAccountID_Client = userAccountID_Client;
            UserAccountID_Admin = userAccountID_Admin;
            EntryDate = DateTime.Now;
            Active = active;
        }

        public Routine(string name, string description, DateTime entryDate, int userAccountID_Client, int userAccountID_Admin, bool active)
        {
            Name = name;
            Description = description;
            EntryDate = entryDate;
            UserAccountID_Client = userAccountID_Client;
            UserAccountID_Admin = userAccountID_Admin;
            
            Active = active;
        }

        public Routine(string name, string description, int userAccountID_Client, int userAccountID_Admin, bool active, DateTime entryDate, DateTime? editDate, DateTime? removalDate)
        {
            Name = name;
            Description = description;
            EntryDate = entryDate;
            EditDate = editDate;
            RemovalDate = removalDate;
            UserAccountID_Client = userAccountID_Client;
            UserAccountID_Admin = userAccountID_Admin;
            Active = active;
        }
    }
}
