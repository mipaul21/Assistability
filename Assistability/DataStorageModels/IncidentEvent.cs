using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
    public class IncidentEvent
    {
        public int IncidentEventID { get; set; }
        public string IncidentName { get; set; }
        public DateTime? DateOfOccurence { get; set; }
        public string PersonsInvolved { get; set; }
        public string EventDescription { get; set; }
        public string EventConsequence { get; set; }
        public DateTime? EventEditDate { get; set; }
        public int UserID_Client { get; set; }
        public int UserID_Admin { get; set; }

        public IncidentEvent(int incidentEventID, string incidentName, DateTime dateOfOccurence, string personsInvolved, string eventDescription, string eventConsequence, DateTime? eventEditDate, int userId_Client, int userID_Admin)
        {
            IncidentEventID = incidentEventID;
            IncidentName = incidentName;
            DateOfOccurence = dateOfOccurence;
            PersonsInvolved = personsInvolved;
            EventDescription = eventDescription;
            EventConsequence = eventConsequence;
            EventEditDate = eventEditDate;
            UserID_Client = userId_Client;
            UserID_Admin = userID_Admin;
        }

        public IncidentEvent( string incidentName, DateTime? dateOfOccurence, string personsInvolved, string eventDescription, string eventConsequence, DateTime? eventEditDate, int userId_Client, int userID_Admin)
        {
            IncidentName = incidentName;
            DateOfOccurence = dateOfOccurence;
            PersonsInvolved = personsInvolved;
            EventDescription = eventDescription;
            EventConsequence = eventConsequence;
            EventEditDate = eventEditDate;
            UserID_Client = userId_Client;
            UserID_Admin = userID_Admin;
        }

        public IncidentEvent() { }

    }
}
