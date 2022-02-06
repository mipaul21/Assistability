/// <summary>
/// Nick Loesel
/// Created: 2021/04/22
/// 
/// This interface has the methods that will be
/// used in the IncidentEvent class
/// </summary>
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IIncidentEventAccessor
    {
        /// Nick Loesel
        /// Created: 2021/04/22
        /// 
        /// Implements the SelectIncidentEventsByIncidentName Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No incident events found</exception>
        /// <returns>A List of Incident objects</returns>
        List<IncidentEvent> SelectIncidentEventsByIncidentName(string incidentName);

        /// Nick Loesel
        /// Created: 2021/04/23
        /// 
        /// The interface for the InsertNewIncidentEvent Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>no incident events added</exception>
        /// <returns>true if the incident event was succsessfully added</returns>
        bool InsertNewIncidentEvent(IncidentEvent newIncidentEvent);

        /// <summary>
        /// Creator: Nick Loesel
        /// Created: 2021/04/25
        /// 
        /// a data access method for updating an incident event
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>an int</returns>
        bool UpdateIncidentEvent(IncidentEvent oldIncidentEvent, IncidentEvent newIncidentEvent);

        /// Nick Loesel
        /// Created: 2021/04/26
        /// 
        /// Implements the DeleteIncidentEvent Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No incident event deleted</exception>
        /// <returns>true if the incident event was deleted</returns>

        bool DeleteIncidentEvent(int incidentEventid);

        /// Nick Loesel
        /// Created: 2021/04/30
        /// 
        /// Implements the select incident event by id method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No incident event deleted</exception>
        /// <returns>an incident event object</returns>

        IncidentEvent SelectIncidentEventById(int incidentEventId);

    }
}
