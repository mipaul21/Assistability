/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/03/29
/// 
/// The manager interface for
/// the PerformanceEvent.
/// 
/// </summary>
///
/// <remarks>
/// </remarks>

using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IPerformanceEventManager
    {
        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The interface method to 
        /// insert a performance event
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newPerformanceEvent">The performance event that is being inserted</param>
        /// <exception cref="ApplicationException">Insert Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>
        
        bool AddNewPerformanceEvent(string performanceName, int clientID, int adminID,
                                            PerformanceEvent newPerformanceEvent);

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The interface method to edit
        /// a PerformanceEvent.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldPerformanceEvent">The old performance event that is being replaced</param>
        /// <param name="newPerformanceEvent">The new performance event that is being inserted</param>
        /// <exception cref="ApplicationException">Update Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>

        bool EditPerformanceEvent(PerformanceEvent oldPerformanceEvent,
                            PerformanceEvent newPerformanceEvent);

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The method to retrieve all 
        /// PerformanceEvents by UserID(client).
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="userIDClient">The new performance event that is being inserted</param>
        /// <exception cref="ApplicationException">Retrieval failed ("Data not Available")</exception>
        /// <returns>List of Client's Performance Events</returns>

        List<PerformanceEvent> RetrieveAllPerformanceEventsByUserID(int userIDClient);

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
        /// <param name="performanceName">The new performance event that is being inserted</param>
        /// <exception cref="ApplicationException">Retrieval failed ("Data not Available")</exception>
        /// <returns>List of Client's Performance Events</returns>

        List<PerformanceEvent> RetrieveAllPerformanceEventsByPerformanceName(string performanceName);

    }
}
