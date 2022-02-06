/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/08
/// 
/// This class has the methods that will be used
/// </summary>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/29
/// Added function to get active and not active awards
/// and to reactivate awards
/// </remarks>

using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataStorageModels;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AwardManager : IAwardManager
    {
        private IAwardAccessor _awardAccessor;

        public AwardManager()
        {
            // _awardAccessor = new AwardFake();
            _awardAccessor = new AwardAccessor();
        }

        public AwardManager(IAwardAccessor awardAccessor )
        {
            _awardAccessor = awardAccessor;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to create an award in the database
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// removed the awardid requirement
        /// </remarks>
        public int CreateAward(string awardName, string awardDescription)
        {
            int rowsAffected;

            try
            {
                rowsAffected = _awardAccessor.CreateNewAward(awardName, awardDescription);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Award could not be created" + ex.InnerException);
            }

            return rowsAffected;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to deactivate an award in the database
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// changed from awardid to awardname
        /// </remarks>
        public int DeactivateAwardByAwardName(string awardName)
        {
            int rowsAffected;

            try
            {
                rowsAffected = _awardAccessor.SafelyDeactivateAwardByAwardName(awardName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Award could not be Deactivated" + ex.InnerException);
            }

            return rowsAffected;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to delete an award in the database
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// changed from awardid to awardname
        /// </remarks>
        public int DeleteAward(string awardName)
        {
            int rowsAffected = 0;

            try
            {
                rowsAffected = _awardAccessor.DeleteAward(awardName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Award could not be deleted" + ex.InnerException);
            }

            return rowsAffected;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to retreive all awards (active and not) in the database
        /// </summary>
        public List<Award> RetreiveEveryAward()
        {
            List<Award> awards;

            try
            {
                awards = _awardAccessor.SelectEveryAward();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Awards not found" + ex.InnerException);
            }

            return awards;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to retreive all awards in the database
        /// </summary>
        public List<Award> RetreiveAllAwards(bool active = true)
        {
            List<Award> awards;

            try
            {
                awards = _awardAccessor.SelectAllAwards(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Awards not found" + ex.InnerException);
            }

            return awards;
        }

        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to retreive all of a specific users awards in the database
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// Changed return type from List<Award> to Award
        /// </remarks>
        public Award RetreiveAwardByAwardName(string awardName)
        {
            Award award;

            try
            {
                award = _awardAccessor.SelectAwardByAwardName(awardName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Awards not found" + ex.InnerException);
            }

            return award;
        }


        /// <summary>
        /// Nathaniel Webber
        /// Created: 2021/03/10
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to update an award in the database
        /// </summary>
        public int UpdateAward(Award newAward, Award oldAward)
        {
            int rowsAffected;

            try
            {
                rowsAffected = _awardAccessor.UpdateAward(newAward, oldAward);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Award could not be edited" + ex.InnerException);
            }

            return rowsAffected;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This method grabs the corresponding stored procedure from the sql
        /// and uses it to reactivate an award in the database
        /// </summary>
        public int ReactivateAwardByAwardName(string awardName)
        {
            int rowsAffected;

            try
            {
                rowsAffected = _awardAccessor.ReactivateAwardByAwardName(awardName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Award could not be Reactivated" + ex.InnerException);
            }

            return rowsAffected;
        }
    }
}
