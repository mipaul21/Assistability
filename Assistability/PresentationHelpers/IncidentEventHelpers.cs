/// <summary>
/// Nick Loesel
/// Created: 2021/04/25
/// 
/// This class file was created to help validate incoming data from the user
/// when they are making their incident Event.
/// </summary>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/04/25
/// 
/// Added all validation helpers
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class IncidentEventHelpers
    {
		/// <summary>
		/// Nick Loesel
		/// Created: 2021/04/25
		/// 
		/// used to make sure that the input personsinvolved will be accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="personsinvolved">the persons involved</param>
		///<returns>true if it is a valid length</returns>

		public static bool IsValidPersonsInvolved(this string personsinvolved)
        {
			bool result = false;

            if (personsinvolved.Length >= 1 && personsinvolved.Length <= 250)
            {
				result = true;
            }

			return result;
        }

		/// <summary>
		/// Nick Loesel
		/// Created: 2021/04/25
		/// 
		/// used to make sure that the input EventDescription will be accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="eventdescription">the event description</param>
		///<returns>true if it is a valid length</returns>

		public static bool IsValidIncidentEventDescription(this string eventdescription)
		{
			bool result = false;

			if (eventdescription.Length >= 1 && eventdescription.Length <= 500)
			{
				result = true;
			}

			return result;
		}

		/// <summary>
		/// Nick Loesel
		/// Created: 2021/04/25
		/// 
		/// used to make sure that the input EventConsequence will be accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="eventconsequence">the event consequence</param>
		///<returns>true if it is a valid length</returns>

		public static bool IsValidEventConsquence(this string eventconsequence)
		{
			bool result = false;

			if (eventconsequence.Length >= 1 && eventconsequence.Length <= 250)
			{
				result = true;
			}

			return result;
		}
	}
}
