/// <summary>
/// Nick Loesel
/// Created: 2021/04/22
/// 
/// Fake IncidentEventAccessor for testing
/// </summary>
///
/// <remarks>
/// 
/// </remarks>
using DataAccessInterfaces;
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Nick Loesel
    /// Created: 2021/04/22
    /// 
    /// This class creates a 'fake' incidentEvent to be used
    /// in the IncidentEventManager test class
    /// </summary>
    public class IncidentEventFake : IIncidentEventAccessor
    {
        private List<IncidentEvent> incidentEvent = new List<IncidentEvent>();

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// This is the Fake IncidentAccessor so I can test to see if its working
        /// </summary>
        public IncidentEventFake()
        {
            incidentEvent.Add(new IncidentEvent()
            {
                IncidentEventID = 1,
                IncidentName = "First Incident",
                DateOfOccurence = new DateTime(2021, 3, 18),
                PersonsInvolved = "Nick",
                EventDescription = "Nick bit someone.",
                EventConsequence = "has to eat vegetables.",
                EventEditDate = null,
                UserID_Client = 3,
                UserID_Admin = 1

            });

            incidentEvent.Add(new IncidentEvent()
            {
                IncidentEventID = 2,
                IncidentName = "Second Incident",
                DateOfOccurence = DateTime.Now,
                PersonsInvolved = "Ryan",
                EventDescription = "Ryan bit someone.",
                EventConsequence = "has to eat vegetables.",
                EventEditDate = null,
                UserID_Client = 3,
                UserID_Admin = 1

            });

            incidentEvent.Add(new IncidentEvent()
            {
                IncidentEventID = 3,
                IncidentName = "Third Incident",
                DateOfOccurence = new DateTime(2021, 3, 18),
                PersonsInvolved = "Nathenial",
                EventDescription = "Nathaniel locked someone is his room.",
                EventConsequence = "no locks on his door.",
                EventEditDate = null,
                UserID_Client = 3,
                UserID_Admin = 1

            });

        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/22
        /// 
        /// Tests the SelectIncidentEventsByUserID Method
        /// </summary>
        /// 
        /// <param name="incidentEventid">The id of the user to select a list of incidentEvents for</param>
        /// <exception>No incidentEvents Found</exception>
        /// <returns>A list of IncidentEVent objcts</returns>
        public bool DeleteIncidentEvent(int incidentEventid)
        {
             bool result = false;
            foreach (var incidentevent in incidentEvent.ToList())
            {
                if (incidentevent.IncidentEventID == incidentEventid)
                {
                    incidentEvent.Remove(incidentevent);
                }
                if (incidentEvent.Count == 2)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool InsertNewIncidentEvent(IncidentEvent newIncidentEvent)
        {
            incidentEvent.Add(newIncidentEvent);

            if (incidentEvent.Count == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IncidentEvent SelectIncidentEventById(int incidentEventId)
        {
            IncidentEvent newIncidentEvent = new IncidentEvent();
            for (int i = 0; i < incidentEvent.Count;)
            {
                foreach (var incidentevent in incidentEvent)
                {
                    if (incidentevent.IncidentEventID == incidentEventId)
                    {
                        newIncidentEvent = incidentEvent[i];
                        break;
                    }
                    break;
                }
                break;

            }
            return newIncidentEvent;
        }
        

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/22
        /// 
        /// Tests the SelectIncidentEventsByUserID Method
        /// </summary>
        /// 
        /// <param name="userID">The id of the user to select a list of incidentEvents for</param>
        /// <exception>No incidentEvents Found</exception>
        /// <returns>A list of IncidentEVent objcts</returns>

        public List<IncidentEvent> SelectIncidentEventsByIncidentName(string incidentName)
        {
            List<IncidentEvent> newIncidentEventList = new List<IncidentEvent>();

            for (int i = 0; i < incidentEvent.Count;)
            {
                foreach (var incidentevent in incidentEvent)
                {
                    if (incidentevent.IncidentName == incidentName)
                    {
                        newIncidentEventList.Add(incidentEvent[i]);
                    }
                    break;
                }
                break;

            }
            return newIncidentEventList;
        }

        public bool UpdateIncidentEvent(IncidentEvent oldIncidentEvent, IncidentEvent newIncidentEvent)
        {
            bool result = false;
            foreach (var incidentevent in incidentEvent)
            {
                if (incidentevent.IncidentName == oldIncidentEvent.IncidentName && incidentevent.IncidentEventID == oldIncidentEvent.IncidentEventID)
                {
                    oldIncidentEvent.PersonsInvolved = newIncidentEvent.PersonsInvolved;
                    oldIncidentEvent.EventDescription = newIncidentEvent.EventDescription;
                    oldIncidentEvent.EventConsequence = newIncidentEvent.EventConsequence;

                }

            }

            if (oldIncidentEvent.PersonsInvolved == newIncidentEvent.PersonsInvolved && oldIncidentEvent.EventDescription == newIncidentEvent.EventDescription && oldIncidentEvent.EventConsequence == newIncidentEvent.EventConsequence)
            {
                result = true;
            }
            return result;
        }
    }
}
