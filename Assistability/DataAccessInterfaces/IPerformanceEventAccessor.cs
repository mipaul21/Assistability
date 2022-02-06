/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/03/29
/// 
/// The interface methods for 
/// the Performance Event accessor.
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

namespace DataAccessInterfaces
{
    public interface IPerformanceEventAccessor
    {
        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The interface method to insert a performance event
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="performanceEvent">The performance event that is being inserted</param>
        /// <exception cref="ApplicationException">Insert Fails("Record not created")</exception>
        /// <returns>Returns rows affected</returns>

        int InsertNewPerformanceEvent(string performanceName, int clientID, int adminID,
                                                PerformanceEvent performanceEvent);

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The accessor method to update a performance event.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="oldPerformanceEvent">Old performance event</param>
        /// <param name="newPerformanceEvent">New performance event</param>
        /// <exception cref="ApplicationException">Update Fails("Record not updated")</exception>
        /// <returns>Returns rows affected</returns>

        int UpdatePerformanceEvent(PerformanceEvent oldPerformanceEvent,
                                PerformanceEvent newPerformanceEvent);

        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The interface method to select performance event by UserID(client)
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="userIDClient">The ID of the user</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>

        List<PerformanceEvent> SelectPerformanceEventsByUserID(int userIDClient);

        /// <summary>
        /// 
        /// Your Name: Whitney Vinson
        /// Created: 2021/03/29
        /// 
        /// The interface method to select 
        /// PerformanceEvent by PerformanceName.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="performanceName">The name of the Performance</param>
        /// <exception cref="ApplicationException">Retrieval Fails ("Data not Available")</exception>
        /// <returns>Rows edited</returns>

        List<PerformanceEvent> SelectPerformanceEventsByPerformanceName(string performanceName);
    }
}
