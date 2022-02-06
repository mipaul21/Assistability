/// <summary>
/// Nick Loesel
/// Created: 2021/03/17
/// 
/// This is an interface for data access of the incidents
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/08
/// Added SelectIncidentsByUserID method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/08
/// Added InsertNewIncident method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added SelectIncidentsByActive method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added ReactivateIncident method
/// </remarks>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added DeactivateIncident method
/// </remarks>

using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IIncidentAccessor
    {
        /// Nick Loesel
        /// Created: 2021/03/08
        /// 
        /// Implements the SelectIncidentsByUserID Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No incidents found</exception>
        /// <returns>A List of Incident objects</returns>
        List<Incident> SelectIncidentsByUserID(int userID);

        /// Nick Loesel
        /// Created: 2021/03/08
        /// 
        /// Implements the InsertNewIncident Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No Incident inserted</exception>
        /// <returns>True if the incident was inserted</returns>
        bool InsertNewIncident(Incident newIncident);



        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/19/2021
        /// Approver: 
        /// 
        /// a data access method for updating an incident
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        int UpdateIncident(Incident oldIncident, Incident newIncident);
		
		/// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// Implements the SelectIncidentByActive Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>No Incidents Found</exception>
        /// <returns>A list of incident objects</returns>
        List<Incident> SelectIncidentsByActive(int selectedUser, bool active);

        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// Implements the ReactivateIncidents Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>Incident not reactivated</exception>
        /// <returns>true if the incident was updated</returns>
        bool ReactivateIncident(UserAccount selectedUser, Incident incident);

        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// Implements the DeactivateIncidents Method
        /// </summary>
        /// 
        /// <param></param>
        /// <exception>Incident Not Deactivated</exception>
        /// <returns>A list of incident objects</returns>
        bool DeactivateIncident(UserAccount selectedUser, Incident incident);
    }
}
