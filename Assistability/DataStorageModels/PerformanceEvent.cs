/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/03/29
/// 
/// The data storage model for the 
/// PerformanceEvent object.
/// 
/// </summary>
/// 
/// <remarks>
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageModels
{
	public class PerformanceEvent
	{
		public int PerformanceEventID { get; set; }
		public string PerformanceName { get; set; }
		public DateTime DateOfOccurance { get; set; }
		public string EventDescription { get; set; }
		public string EventResult { get; set; }
		public DateTime? EventEditDate { get; set; }
		public int UserIDClient { get; set; }
		public int UserIDReporter { get; set; }

		public PerformanceEvent(string performanceName,
			DateTime dateOfOccurance, string eventDescription, string eventResult,
			DateTime? eventEditDate, int userIDClient, int userIDReporter)
		{
			PerformanceName = performanceName;
			DateOfOccurance = dateOfOccurance;
			EventDescription = eventDescription;
			EventResult = eventResult;
			EventEditDate = eventEditDate;
			UserIDClient = userIDClient;
			UserIDReporter = userIDReporter;
		}
		public PerformanceEvent(int performanceEventID,
			string eventDescription, string eventResult)
		{
			PerformanceEventID = performanceEventID;
			EventDescription = eventDescription;
			EventResult = eventResult;
		}
		public PerformanceEvent()
		{

		}
	}
}
