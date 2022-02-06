/// <summary>
/// Nick Loesel
/// Created: 2021/04/23
/// 
/// Implements the Incident Event manager interface.
/// </summary>
/// 
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/23
/// Added insert a new incident evet
/// </remarks>
///
/// <remarks>
/// Nick Loesel
/// Updated: 2021/04/23
/// Added select incident event by incident name
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
    public class IncidentEventManager : IIncidentEventManager
    {
        private IIncidentEventAccessor _incidentEventAccessor;

        public IncidentEventManager()
        {
            _incidentEventAccessor = new IncidentEventAccessor();
        }

        public IncidentEventManager(IIncidentEventAccessor incidentEventAccessor)
        {
            this._incidentEventAccessor = incidentEventAccessor;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/26
        /// 
        /// This is the method that will select delete an incident event
        /// </summary>
        /// 
        /// <param name="incidentEventId">The incident event id to delete</param>
        /// <exception>No Incident events found</exception>
        /// <returns>a list of incident events</returns>
        public bool DeleteIncidentEvent(int incidentEventId)
        {
            bool result = false;
            try
            {

                result = _incidentEventAccessor.DeleteIncidentEvent(incidentEventId);
                if (result == false)
                {
                    throw new ApplicationException("New performance event was not added.");
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Performance Event Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/25
        /// 
        /// This is the method that will insert a new incident event
        /// </summary>
        /// 
        /// <param name="newIncidentEvent">The new incident event</param>
        /// <exception>No Incident event inserted</exception>
        /// <returns>true if the incident event was inserted</returns>
        public bool InsertNewIncidentEvent(IncidentEvent newIncidentEvent)
        {
            bool result = false;
            try
            {

                result = _incidentEventAccessor.InsertNewIncidentEvent(newIncidentEvent);
                if (result == false)
                {
                    throw new ApplicationException("New performance event was not added.");
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Performance Event Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/30
        /// 
        /// This is the method that will select an incident event by id
        /// </summary>
        /// 
        /// <param name="incidentEventId">The incident event id to select</param>
        /// <exception>No Incident events found</exception>
        /// <returns>an incident event object</returns>
        public IncidentEvent SelectIncidentEventById(int incidentEventId)
        {
            IncidentEvent result = new IncidentEvent();
            try
            {

                result = _incidentEventAccessor.SelectIncidentEventById(incidentEventId);
                if (result.IncidentEventID != incidentEventId)
                {
                    throw new ApplicationException("New incident event was not found.");
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Performance Event Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/25
        /// 
        /// This is the method that will select incident events by incident name
        /// </summary>
        /// 
        /// <param name="incidentName">The incident name to select by</param>
        /// <exception>No Incident events found</exception>
        /// <returns>a list of incident events</returns>
        public List<IncidentEvent> SelectIncidentsEventsByIncidentName(string incidentName)
        {
            List<IncidentEvent> incidentEvents = new List<IncidentEvent>();
            try
            {
                incidentEvents = _incidentEventAccessor.SelectIncidentEventsByIncidentName(incidentName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Incident events could not be found" + ex.InnerException.Message);
            }
            return incidentEvents;
        }


        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/25
        /// 
        /// This is the method that will update a selected incident event
        /// </summary>
        /// 
        /// <param name="oldIncidentEvent">The old incident event</param>
        /// <param name="newIncidentEvent">The new incident event</param>
        /// <exception>No Incident events updated</exception>
        /// <returns>true if the incdient event was updated</returns>
        public bool UpdateIncidentEvent(IncidentEvent oldIncidentEvent, IncidentEvent newIncidentEvent)
        {
            bool result = false;
            try
            {
                result =  _incidentEventAccessor.UpdateIncidentEvent(oldIncidentEvent, newIncidentEvent);


                if (result == false)
                {
                    throw new ApplicationException("Data not updated.");
                }

            }


            catch (Exception ex)
            {
                throw new ApplicationException("Incident event not updated.", ex);
            }
            return result;
        }
    }
}
