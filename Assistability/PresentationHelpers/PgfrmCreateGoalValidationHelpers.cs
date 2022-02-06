///<summary>
/// Becky Baenziger
/// Created: 2021/02/18
/// ///
/// Created to help validate data entered into PgfrmCreateGoal
/// 
///</summary>
///
///<remarks>
/// Updater Name:
/// Update Date:

///</remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationHelpers
{
    public static class PgfrmCreateGoalValidationHelpers
    {
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// To validate that a goal type was selected
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="goalType"></param>
        /// <returns></returns>
        public static bool IsValidGoalType(this string goalType)
        {
            bool result = false;
            if (!goalType.Equals("Select a Goal Type"))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// To validate that text was entered and that it is at or under the 
        /// 50 character limit set by the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="goalName"></param>
        /// <returns></returns>
        public static bool IsValidGoalName(this string goalName)
        {
            bool result = false;
            if (goalName.Length >= 1 && goalName.Length <= 50)
            {
                result = true;
            }
            return result;

        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/02/18
        /// ///
        /// To validate that text was entered and that it is at or under
        /// 500 character limit set by the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="goalDescription"></param>
        /// <returns></returns>
        public static bool IsValidGoalDescription(this string goalDescription)
        {
            bool result = false;
            if (goalDescription.Length >= 1 && goalDescription.Length <= 500)
            {
                result = true;
            }
            return result;

        }

        public static bool IsValidNumber(this int frequency)
        {
            bool result = false;
            if(frequency > 0)
            {
                result = true;
            }
            return result;
        }

        public static bool IsValidNumberExtGoal(this int frequency)
        {
            bool result = false;
            if( frequency >= 0)
            {
                result = true;
            }
            return result;
        }
    }
}
