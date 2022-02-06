/// <summary>
/// Nick Loesel
/// Created: 2021/04/22
/// ///
/// Interface for the incidentEvent manager
/// </summary>
/// <remarks>
/// Updater Name:
/// Update Date:
/// </remarks>
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IIncidentEventManager
    {
        /// <summary>
        /// 
        /// Your Name: Nick Loesel
        /// Created: 2021/04/22
        /// 
        /// The interface method select a list of incident events.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="userID">The id of the user to select incident events for</param>
        /// <exception cref="ApplicationException">No incident Events found</exception>
        /// <returns>A list of incident events</returns>

        List<IncidentEvent> SelectIncidentsEventsByIncidentName(string incidentName);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/23
        /// 
        /// This is the interface of the method that will insert a new incident Event.
        /// </summary>
        /// 
        /// <param name="newIncident">The Incident object of the new incident</param>
        /// <exception>No Incident created</exception>
        /// <returns>A true if it was inserted</returns>

        bool InsertNewIncidentEvent(IncidentEvent newIncidentEvent);

        /// <summary>
        /// Creator: Nick Loesel
        /// Created: 2021/04/25
        /// 
        /// Logic layer method in order to update an incident event
        /// </summary>
        /// <remarks>
        /// </remarks>
        bool UpdateIncidentEvent(IncidentEvent oldIncidentEvent, IncidentEvent newIncidentEvent);

        /// <summary>
        /// Creator: Nick Loesel
        /// Created: 2021/04/26
        /// 
        /// The interface for the method to delete an incident event
        /// </summary>
        /// <remarks>
        /// </remarks>

        bool DeleteIncidentEvent(int incidentEventId);

        /// <summary>
        /// Creator: Nick Loesel
        /// Created: 2021/04/30
        /// 
        /// The interface for the method to select an incident event by id
        /// </summary>
        /// <remarks>
        /// </remarks>
        IncidentEvent SelectIncidentEventById(int incidentEventId);
    }
}
