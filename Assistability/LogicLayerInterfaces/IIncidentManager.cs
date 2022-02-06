/// <summary>
/// Nick Loesel
/// Created: 2021/03/17
/// 
/// This is an interface for the management
/// of Incidents
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added select Incidents by active
/// </remarks>
/// 
/// /// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added Activate an Incident
/// </remarks>
/// 
/// /// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added deactivate an Incident
/// </remarks>
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IIncidentManager
    {
        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/17
        /// 
        /// This is the interface of the method that will grab a list of incidents for a  specific user.
        /// </summary>
        /// 
        /// <param name="userID">The UserID of the User who created this Journal</param>
        /// <exception>No Incident found</exception>
        /// <returns>A List of incident objects</returns>
        List<Incident> SelectIncidentsByUserID(int userID);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// This is the interface of the method that will insert a new incident.
        /// </summary>
        /// 
        /// <param name="newIncident">The Incident object of the new incident</param>
        /// <exception>No Incident created</exception>
        /// <returns>A true if it was inserted</returns>
        bool AddNewIncident(Incident newIncident);




        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/19/2021
        /// Approver: 
        /// 
        /// Logic layer method in order to update an incident
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        bool UpdateIncident(Incident oldIncident, Incident newIncident);
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the interface of the method that will select incidents by active.
        /// </summary>
        /// 
        /// <param name="active">The active incidents</param>
        /// <param name="selectedUser">The Selected user to view incidents for</param>
        /// <exception>No Incidents found</exception>
        /// <returns>A list of active incidents</returns>
        List<Incident> SelectIncidentsByActive(int selectedUser, bool active);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the interface of the method that will update an incident to active.
        /// </summary>
        /// 
        /// <param name="incident">The incident to activate</param>
        /// <param name="selectedUser">The Selected user to view incidents for</param>
        /// <exception>No Incidents activates</exception>
        /// <returns>true if the incident was update</returns>
        bool Reactivateincident(UserAccount selectedUser, Incident incident);

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the interface of the method that will update an incident to not active.
        /// </summary>
        /// 
        /// <param name="incident">The active deactivate</param>
        /// <param name="selectedUser">The Selected user to view incidents for</param>
        /// <exception>No Incidents found</exception>
        /// <returns>true if the incident was updated</returns>
        bool DeactivateIncident(UserAccount selectedUser, Incident incident);
    }
}
