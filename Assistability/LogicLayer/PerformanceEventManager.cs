/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/03/29
/// 
/// The PerformanceEvent
/// manager methods.
/// 
/// </summary>
///
/// <remarks>
/// </remarks>

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
    public class PerformanceEventManager : IPerformanceEventManager
    {
        private IPerformanceEventAccessor _performanceEventAccessor;

        public PerformanceEventManager()
        {
            _performanceEventAccessor = new PerformanceEventAccessor();
        }

        public PerformanceEventManager(IPerformanceEventAccessor performanceEventAccessor)
        {
            _performanceEventAccessor = performanceEventAccessor;

        }

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method to add a performance event.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="performanceName">The name of the associated PerformanceEvent.</param>
        /// <param name="clientID">The client who the event is about.</param>
        /// <param name="adminID">The reporting user.</param>
        /// <param name="newPerformanceEvent">The brand new performance event.</param>
        /// <exception cref="ApplicationException">Insert Fails ("New performance event was not added.")</exception>
        /// <returns>Rows added</returns>

        public bool AddNewPerformanceEvent(string performanceName, int clientID, int adminID,
                                            PerformanceEvent newPerformanceEvent)
        {
            bool result = false;
            int newRoutineStepID = 0;
            try
            {
                newRoutineStepID = _performanceEventAccessor.InsertNewPerformanceEvent(performanceName, clientID,
                                                                        adminID, newPerformanceEvent);
                if (newRoutineStepID == 0)
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
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method to update a 
        /// performance event.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldPerformanceEvent">The old performance event.</param>
        /// <param name="newPerformanceEvent">The brand new performance event.</param>
        /// <exception cref="ApplicationException">Update Failed ("Performance event was not updated.")</exception>
        /// <returns>Rows added</returns>

        public bool EditPerformanceEvent(PerformanceEvent oldPerformanceEvent, PerformanceEvent newPerformanceEvent)
        {
            bool result = false;
            try
            {
                result = (1 == _performanceEventAccessor.UpdatePerformanceEvent(oldPerformanceEvent, newPerformanceEvent));
                if (result == false)
                {
                    throw new ApplicationException("Performance Event data was not changed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method to retrieve all 
        /// performance events by PerformanceName.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="performanceName">The name of the associated performance.</param>
        /// <exception cref="ApplicationException">Operation Failed ("Performance event records not found.")</exception>
        /// <returns>Row Count</returns>

        public List<PerformanceEvent> RetrieveAllPerformanceEventsByPerformanceName(string performanceName)
        {
            List<PerformanceEvent> userEvents = null;
            try
            {
                userEvents = _performanceEventAccessor.SelectPerformanceEventsByPerformanceName(performanceName);
            }
            catch (Exception)
            {
                throw new ApplicationException("Performance event records not found.");
            }
            return userEvents;
        }

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method to retrieve all performance events by client id.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="userIDClient">The user's client id.</param>
        /// <exception cref="ApplicationException">Operation Failed ("Performance event records not found.")</exception>
        /// <returns>Rows Count</returns>
        
        public List<PerformanceEvent> RetrieveAllPerformanceEventsByUserID(int userIDClient)
        {
            List<PerformanceEvent> userEvents = null;
            try
            {
                userEvents = _performanceEventAccessor.SelectPerformanceEventsByUserID(userIDClient);
            }
            catch (Exception)
            {
                throw new ApplicationException("Performance event records not found.");
            }
            return userEvents;
        }
    }
}
