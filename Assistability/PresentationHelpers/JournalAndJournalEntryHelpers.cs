/// <summary>
/// Ryan Taylor
/// Created: 2021/02/25
/// 
/// This class file was created to help validate incoming data from the user
/// when they are making a journal or journal entry.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class JournalAndJournalEntryHelpers
    {
		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/25
		/// 
		/// used to make sure that the imput Journal Name will be 
		/// accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="journalName">the inputed journal name</param>
		///<returns>A bool sigifying the journal name wont 
		///break the data base</returns>
		public static bool IsValidJournalName(this string journalName)
		{
			bool result = false;

			if (journalName.Length >= 1 && journalName.Length <= 50)
			{
				result = true;
			}

			return result;
		}

		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/25
		/// 
		/// used to make sure that the imput Journal Entry will be 
		/// accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="firstName">the inputed journal entry</param>
		///<returns>A bool sigifying the journal entry wont 
		///break the data base</returns>
		public static bool IsValidJournalEntry(this string journalEntry)
		{
			bool result = false;

			if (journalEntry.Length >= 1 && journalEntry.Length <= 500)
			{
				result = true;
			}

			return result;
		}
	}
}
