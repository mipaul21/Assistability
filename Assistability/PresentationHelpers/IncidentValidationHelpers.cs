using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class IncidentValidationHelpers
    {
        public static bool IsValidIncidentName(this string incidentName)
        {
            bool result = false;

            if (incidentName.Length <= 50)
            {
                result = true;
            }

            return result;
        }
        public static bool IsValidIncidentDescription(this string incidentDescription)
        {
            bool result = false;

            if (incidentDescription.Length <= 250)
            {
                result = true;
            }

            return result;
        }
        public static bool IsValidDesiredConsequence(this string desiredConsequence)
        {
            bool result = false;

            if (desiredConsequence.Length <= 250)
            {
                result = true;
            }

            return result;
        }
    }
}
