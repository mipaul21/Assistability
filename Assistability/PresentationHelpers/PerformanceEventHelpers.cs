/// <summary>
/// Your Name: Whitney Vinson
/// Created: 2021/04/15
/// 
/// The helper methods for 
/// PerformanceEvent.
/// 
/// </summary>
///
/// <remarks>
/// 
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class PerformanceEventHelpers
    {
        public static bool IsValidEventDescription(this string eventDescription)
        {
            bool result = false;

            if (eventDescription.Length > 0 && eventDescription.Length < 500)
            {
                result = true;
            }
            return result;
        }

        public static bool IsValidEventResult(this string eventResult)
        {
            bool result = false;

            if (eventResult.Length > 0 && eventResult.Length < 250)
            {
                result = true;
            }
            return result;
        }
    }
}
