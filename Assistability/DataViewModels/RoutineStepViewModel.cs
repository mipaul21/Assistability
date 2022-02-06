/// <summary>
/// Your Name: Whitney Vinson
/// Created: 2021/02/19
/// 
/// The view model for managing the Routine Steps.
/// 
/// </summary>
///
/// <remarks>
/// Updater Name: Whitney Vinson
/// Updated: 2021/03/19
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
	/// Your Name: Whitney Vinson
	/// Created: 2021/02/19
	/// 
	/// The view model to the routine step data.
	/// </summary>
	///
	/// <remarks>
	/// </remarks>
	public class RoutineStepViewModel : RoutineStep
	{
		public int StepID { get; set; }

		public RoutineStepViewModel(RoutineStep routineStep) : 
			base(routineStep.RoutineName, routineStep.RoutineStepName, 
				routineStep.RoutineStepDescription, routineStep.RoutineStepEntryDate, 
				routineStep.RoutineStepOrderNumber, routineStep.Active)
		{
			this.StepID = routineStep.RoutineStepID;
		}
		public RoutineStepViewModel()
		{

		}
	}
}
