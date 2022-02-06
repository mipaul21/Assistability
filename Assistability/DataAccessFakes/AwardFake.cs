/// Nathaniel Webber
/// Created: 2021/03/08
/// All necessary Fakes for testing 
/// </remarks>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/29
/// Added function to get active and not active awards
/// and to reactivate awards
/// </remarks>

using DataAccessInterfaces;
using DataStorageModels;
using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class AwardFake : IAwardAccessor
    {
        int rowsAffected;
        private List<Award> fakeAward = new List<Award>();
        private Award fakeSingleAward = new Award();

        public AwardFake()
        {
            fakeAward.Add(new Award() 
            { 
                AwardName = "Fake Award 1",
                AwardDescription = "Fake Award Description 1",
                Active = true
            });

            fakeAward.Add(new Award()
            {
                AwardName = "Fake Award 2",
                AwardDescription = "Fake Award Description 2",
                Active = true
            });

            fakeAward.Add(new Award()
            {
                AwardName = "Fake Award 3",
                AwardDescription = "Fake Award Description 3",
                Active = true
            });

            fakeAward.Add(new Award()
            {
                AwardName = "Fake Award 4",
                AwardDescription = "Fake Award Description 4",
                Active = true
            });
            
            fakeSingleAward.AwardName = "Fake Award 1";
            fakeSingleAward.AwardDescription = "Fake Award Description 1";
            fakeSingleAward.Active = true;

        }

        public int CreateNewAward(string awardName, string awardDescription)
        {
            return rowsAffected;
        }

        public int DeleteAward(string awardName)
        {
            return rowsAffected;
        }

        public int SafelyDeactivateAwardByAwardName(string awardName)
        {
            return rowsAffected;
        }

        public List<Award> SelectAllAwards(bool active = true)
        {
            
            return this.fakeAward;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// a fake method for testing when every award needs to be retrieved
        /// </summary>
        ///<exception></exception>
        ///<returns>List of award objects
        ///</returns>
        public List<Award> SelectEveryAward()
        {

            return this.fakeAward;
        }

        /// <summary>
        /// 
        /// </summary>
        /// Updater Name: Becky Baenziger
        /// Updage Date: 2021/04/24
        /// Changed fake to work for finding a single award v. a list of awards.
        /// <param name="awardName"></param>
        /// <returns></returns>
        public Award SelectAwardByAwardName(string awardName)
        {
            return fakeAward.Find(a => a.AwardName == awardName);
        }

        public int UpdateAward(Award newAward, Award oldAward)
        {
            return rowsAffected;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// a fake method for testing when every award needs to be reactivated
        /// </summary>
        ///<exception></exception>
        ///<returns>Rows affected
        ///</returns>
        public int ReactivateAwardByAwardName(string awardName)
        {
            return rowsAffected;
        }
    }
}
