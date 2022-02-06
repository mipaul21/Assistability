/// <summary>
/// 
/// Your Name: Whitney Vinson
/// Created: 2021/03/29
/// 
/// The view model for accessing
/// the Performance Events.
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

namespace DataViewModels
{
	/// <summary>
	/// 
	/// Your Name: Whitney Vinson
	/// Created: 2021/03/29
	/// 
	/// The view model to the 
	/// Performance Event data.
	/// 
	/// </summary>
	/// 
	/// <remarks>
	/// </remarks>

	public class PerformanceEventViewModel : PerformanceEvent
	{
		public int EventID { get; set; }

		public PerformanceEventViewModel(PerformanceEvent performanceEvent) :
			base(performanceEvent.PerformanceName, performanceEvent.DateOfOccurance,
				performanceEvent.EventDescription, performanceEvent.EventResult,
				performanceEvent.EventEditDate, performanceEvent.UserIDClient, performanceEvent.UserIDReporter)
		{
			this.EventID = performanceEvent.PerformanceEventID;
		}
		public PerformanceEventViewModel()
		{

		}
	}
}
