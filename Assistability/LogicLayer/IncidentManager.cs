/// <summary>
/// Nick Loesel
/// Created: 2021/03/17
/// 
/// Implements the Incident manager interface.
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/18
/// Added insert a incident
/// </remarks>
///
/// <remarks>
/// Nick Loesel
/// Updated: 2021/03/25
/// Added select incidents by active
/// </remarks>
using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class IncidentManager : IIncidentManager
    {
        private IIncidentAccessor _incidentAccessor;

        public IncidentManager()
        {
            _incidentAccessor = new IncidentAccessor();
        }

        public IncidentManager(IIncidentAccessor incidentAccessor)
        {
            this._incidentAccessor = incidentAccessor;
        }
        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/8
        /// 
        /// This is the method that will insert a new incident object.
        /// </summary>
        /// 
        /// <param name="newIncident">The new incident object.</param>
        /// <exception>No Incident inserted</exception>
        /// <returns>True if it was inserted</returns>
        public bool AddNewIncident(Incident newIncident)
        {
            bool result = false;


            try
            {
                result = _incidentAccessor.InsertNewIncident(newIncident);
                if (result == false)
                {
                    throw new ApplicationException("New Incident was not added.");
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add new incdent failed.", ex);
            }
            return result;
        }
        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/7
        /// 
        /// This is the method that will select the incident by userID.
        /// </summary>
        /// 
        /// <param name="userID">The ID of the user to select</param>
        /// <exception>No Incidents found.</exception>
        /// <returns>A list of incident objects.</returns>
        public List<Incident> SelectIncidentsByUserID(int userID)
        {
            List<Incident> incidents = new List<Incident>();
            try
            {
                incidents = _incidentAccessor.SelectIncidentsByUserID(userID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Incidents could not be found" + ex.InnerException.Message);
            }
            return incidents;
        }


        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/03/19
        /// 
        /// This is the method that will update the selected Incident
        /// </summary>
        /// 
        /// <param name="oldIncident, newIncident">The unedited version, and the newly created version</param>
        /// <exception>Data not updated.</exception>
        /// <returns>An updated Incident object.</returns>


        public bool UpdateIncident(Incident oldIncident, Incident newIncident)
        {
            bool result = false;
            try
            {
                result = (1 == _incidentAccessor.UpdateIncident(oldIncident, newIncident));


                if (result == false)
                {
                    throw new ApplicationException("Data not updated.");
                }

            }


            catch (Exception ex)
            {
                throw new ApplicationException("Incident not updated.", ex);
            }
            return result;

        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the method that will update an incident to active.
        /// </summary>
        /// 
        /// <param name="incident">The active deactivate</param>
        /// <param name="selectedUser">The Selected user to view incidents for</param>
        /// <exception>No Incidents found</exception>
        /// <returns>true if the incident was updated</returns>
        public bool Reactivateincident(UserAccount selectedUser, Incident incident)
        {
            bool result = false;


            try
            {
                result = _incidentAccessor.ReactivateIncident(selectedUser, incident);
                if (result == false)
                {
                    throw new ApplicationException("New Incident was not reactivated.");
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("reactivate incident failed.", ex);
            }
            return result;
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the method that will update an incident to not active.
        /// </summary>
        /// 
        /// <param name="incident">The active deactivate</param>
        /// <param name="selectedUser">The Selected user to view incidents for</param>
        /// <exception>No Incidents found</exception>
        /// <returns>true if the incident was updated</returns>
        public bool DeactivateIncident(UserAccount selectedUser, Incident incident)
        {
            bool result = false;


            try
            {
                result = _incidentAccessor.DeactivateIncident(selectedUser, incident);
                if (result == false)
                {
                    throw new ApplicationException("New Incident was not deactivated.");
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("deactivate incident failed.", ex);
            }
            return result;
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// This is the method that select incidents by active.
        /// </summary>
        /// 
        /// <param name="active">The active incidents</param>
        /// <param name="selectedUser">The Selected user to view incidents for</param>
        /// <exception>No Incidents found</exception>
        /// <returns>A list of active incidents</returns>
        public List<Incident> SelectIncidentsByActive(int selectedUser, bool active)
        {
            List<Incident> incidents = new List<Incident>();

            try
            {
                incidents = _incidentAccessor.SelectIncidentsByActive(selectedUser, active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Incidents could not be found" + ex.InnerException.Message);
            }
            return incidents;
        }
    }
}
