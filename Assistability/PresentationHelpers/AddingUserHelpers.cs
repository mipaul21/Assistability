/// <summary>
/// Ryan Taylor
/// Created: 2021/01/31
/// 
/// This class file was created to help validate incoming data from the user
/// when they are making their account so that it won't harm the database.
/// </summary>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/02/12
/// 
/// Added IsValidTitle
/// </remarks>
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/02/25
/// 
/// removed IsValidTitle
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class AddingUserHelpers
    {
		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/01
		/// 
		/// used to make sure that the imput First Name will be accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="firstName">the inputed first name</param>
		///<returns>A bool sigifying the first name wont 
		///break the data base</returns>		
		public static bool IsValidFirstName(this string firstName)
		{
			bool result = false;

			if (firstName.Length >= 1 && firstName.Length <= 50)
			{
				result = true;
			}

			return result;
		}
		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/01
		/// 
		/// used to make sure that the imput Last Name will be accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="lastName">the inputed last name</param>
		///<returns>A bool sigifying the last name wont 
		///break the data base</returns>
		public static bool IsValidLastName(this string lastName)
		{
			bool result = false;

			if (lastName.Length >= 1 && lastName.Length <= 50)
			{
				result = true;
			}

			return result;
		}
		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/01
		/// 
		/// used to make sure that the imput Username will be accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="username">the inputed username</param>
		///<returns>A bool sigifying the username wont 
		///break the data base</returns>
		public static bool IsValidUsername(this string username)
		{
			bool result = false;

			if (username.Length >= 1 && username.Length <= 50)
			{
				result = true;
			}

			return result;
		}
		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/01
		/// 
		/// used to make sure that the imput Email will be accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="email">the inputed email</param>
		///<returns>A bool sigifying the email is an email and wont 
		///break the data base</returns>
		public static bool IsValidEmail(this string email)
		{
			bool result = false;

			if (email.Contains("@") && email.Length >= 7 &&
				email.Length <= 250)
			{
				result = true;
			}

			return result;
		}
		/// <summary>
		/// Ryan Taylor
		/// Created: 2021/02/07
		/// 
		/// used to make sure that the imput password will be 
		/// accepted by the data dictionary.
		/// </summary>
		/// 
		/// <param name="password">the inputed password</param>
		///<returns>A bool sigifying the password wont 
		///break the data base</returns>
		public static bool IsValidPassword(this string password)
		{
			bool result = false;

			if (password.Length >= 7 &&
				password.Length <= 100)
			{
				result = true;
			}

			return result;
		}
	}
}
