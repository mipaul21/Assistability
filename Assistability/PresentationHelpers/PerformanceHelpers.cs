/// <summary>
/// Ryan Taylor
/// Created: 2021/02/25
/// 
/// This class file was created to help validate incoming data from the user
/// when they are making a performance.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class PerformanceHelpers
    {
		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/04/02
		/// 
		/// used to make sure that the imput performance Name will be 
		/// accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="performanceName">the inputed performance name</param>
		///<returns>A bool sigifying the performance name wont 
		///break the data base</returns>
		public static bool IsValidPerformanceName(this string performanceName)
		{
			bool result = false;

			if (performanceName.Length >= 1 && performanceName.Length <= 50)
			{
				result = true;
			}

			return result;
		}


		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/04/02
		/// 
		/// used to make sure that the imput performance Name will be 
		/// accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="performanceName">the inputed performance description</param>
		///<returns>A bool sigifying the performance description wont 
		///break the data base</returns>
		public static bool IsValidPerformanceDescription(this string performanceDescription)
		{
			bool result = false;

			if (performanceDescription.Length >= 1 && performanceDescription.Length <= 255)
			{
				result = true;
			}

			return result;
		}
	}
}
