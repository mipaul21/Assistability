/// <summary>
/// Nick Loesel
/// Created: 2021/03/05
/// 
/// The validation helpers for routines
/// </summary>
///
/// <remarks>
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class RoutineValidationHelpers
    {
        public static bool IsValidRoutineName(this string routineName)
        {
            bool result = false;

            if (routineName.Length != 0 && routineName.Length <= 50)
            {
                result = true;
            }

            return result;
        }
        public static bool IsValidRoutineDescription(this string description)
        {
            bool result = false;

            if (description.Length != 0 && description.Length <= 150)
            {
                result = true;
            }

            return result;
        }
    }
}
