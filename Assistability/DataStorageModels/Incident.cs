using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
    public class Incident
    {
        public string IncidentName { get; set; }
        public string IncidentDescription { get; set; }
        public string DesiredConsequence { get; set; }
        public DateTime IncidentEntryDate { get; set; }
        public DateTime? IncidentEditDate { get; set; }
        public DateTime? IncidentRemovalDate { get; set; }
        public bool Active { get; set; }
        public int UserId_Client { get; set; }
        public int UserId_Creator { get; set; }

        public Incident(string incidentName, string incidentDescription, string desiredConsequence, DateTime incidentEntryDate, DateTime? incidentEditDate, DateTime? incidentRemovalDate, bool active, int userId_Client, int userId_Creator)
        {
            IncidentName = incidentName;
            IncidentDescription = incidentDescription;
            DesiredConsequence = desiredConsequence;
            IncidentEntryDate = incidentEntryDate;
            IncidentEditDate = incidentEditDate;
            IncidentRemovalDate = incidentRemovalDate;
            Active = active;
            UserId_Client = userId_Client;
            UserId_Creator = userId_Creator;
        }

        public Incident()
        {
        }
    }
}
