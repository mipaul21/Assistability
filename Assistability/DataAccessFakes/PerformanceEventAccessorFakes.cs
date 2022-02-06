/// <summary>
/// 
/// Whitney Vinson
/// Created: 2021/03/29
/// 
/// The PerformanceEvent
/// data access fakes.
/// 
/// </summary>
///
/// <remarks>
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
    public class PerformanceEventAccessorFakes : IPerformanceEventAccessor
    {
        private List<PerformanceEvent> data = new List<PerformanceEvent>();

        public PerformanceEventAccessorFakes()
        {
            data.Add(new PerformanceEvent()
            {
                PerformanceEventID = 1,
                PerformanceName = "FirstPerformance",
                DateOfOccurance = DateTime.Parse("2021-01-01"),
                EventDescription = "Good Event 1",
                EventResult = "Good Result 1",
                EventEditDate = null,
                UserIDClient = 1,
                UserIDReporter = 2

            });
            data.Add(new PerformanceEvent()
            {
                PerformanceEventID = 2,
                PerformanceName = "SecondPerformance",
                DateOfOccurance = DateTime.Parse("2021-01-02"),
                EventDescription = "Good Event 2",
                EventResult = "Good Result 2",
                EventEditDate = null,
                UserIDClient = 1,
                UserIDReporter = 2

            });
            data.Add(new PerformanceEvent()
            {
                PerformanceEventID = 3,
                PerformanceName = "ThirdPerformance",
                DateOfOccurance = DateTime.Parse("2021-01-03"),
                EventDescription = "Good Event 3",
                EventResult = "Good Result 3",
                EventEditDate = DateTime.Parse("2021-01-04"),
                UserIDClient = 1,
                UserIDReporter = 2

            });

        }
        public int InsertNewPerformanceEvent(string performanceName, int clientID, int adminID,
                                                PerformanceEvent performanceEvent)
        {
            var count = data.Count;
            data.Add(new PerformanceEvent()
            {
                PerformanceEventID = 4,
                PerformanceName = "FourthPerformance",
                DateOfOccurance = DateTime.Parse("2021-01-04"),
                EventDescription = "Good Event 4",
                EventResult = "Good Result 4",
                EventEditDate = null,
                UserIDClient = 1,
                UserIDReporter = 2
            });
            return this.data.Count - count;
        }

        public List<PerformanceEvent> SelectPerformanceEventsByPerformanceName(string performanceName)
        {
            List<PerformanceEvent> performanceEvents = data.FindAll(pn => pn.PerformanceName == performanceName);

            return performanceEvents;
        }

        public List<PerformanceEvent> SelectPerformanceEventsByUserID(int userIDClient)
        {
            List<PerformanceEvent> userPerformanceEvents = data.FindAll(pe => pe.UserIDClient == 1);

            return userPerformanceEvents;
        }

        public int UpdatePerformanceEvent(PerformanceEvent oldPerformanceEvent, PerformanceEvent newPerformanceEvent)
        {
            int rowsAffected = 0;

            data.Remove(oldPerformanceEvent);
            data.Add(newPerformanceEvent);
            if (data.IndexOf(newPerformanceEvent) > -1)
            {
                rowsAffected = 1;
            }

            return rowsAffected;
        }
    }
}
