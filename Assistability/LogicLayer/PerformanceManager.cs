/// <summary>
/// Ryan Taylor
/// Created: 2021/03/30
/// 
/// Implements the Performance Manager interface
/// </summary>
/// 
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
    public class PerformanceManager : IPerformanceManager
    {
        private IPerformanceAccessor _performanceAccessor = null;

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// Default constructor initializes an accessor
        /// </summary>
        public PerformanceManager()
        {
            //_performanceAccessor = new PerformanceFakes();
            _performanceAccessor = new PerformanceAccessor();
        }

        public PerformanceManager(IPerformanceAccessor performanceAccessor)
        {
            //_performanceAccessor = new PerformanceFakes();
            _performanceAccessor = performanceAccessor;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// Creates a new performance for a client
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="perfomanceDescription"> a detailed description 
        /// of the performance</param>
        /// <param name="userIdClient"> the id of the person who will 
        /// perform the performance</param>
        /// <param name="userIdCreator"> The UserID of the user who 
        /// created the performance</param>
        /// <exception>Add Performance Failed. New performance was not added.</exception>
        /// <returns>a bool signifying that the performance was created or not</returns>
        public bool CreatePerformance(string performanceName, string perfomanceDescription, 
            int userIdClient, int userIdCreator)
        {
            bool result = false;
            int newPerformance = 0;
            try
            {
                newPerformance = _performanceAccessor.InsertNewPerformance(performanceName, 
                    perfomanceDescription, userIdClient, userIdCreator);
                if (newPerformance == 0)
                {
                    throw new ApplicationException("New performance was not added.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Performance Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// deactivates or reactivates a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="UserIdClient"> the id of the person who will 
        /// perform the performance</param>
        /// <param name="oldActive">the original state of the performance</param>
        /// <param name="newActive">the new state of the performance</param>
        /// <exception>Performance Reactivation or Deactivation Failed. 
        /// Performance was not deactivated or reactivated. </exception>
        /// <returns>a bool signifying that the performance 
        /// was deactivated or reactivated</returns>
        public bool DeactivateReactivatePerformance(string performanceName, int UserIdClient, 
            bool oldActive, bool newActive)
        {
            bool result = false;
            int rowsAffected = 0;

            try
            {
                rowsAffected =
                    _performanceAccessor.DeactivateReactivatePerformance(performanceName,
                    UserIdClient, oldActive, newActive);
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Performance "+ performanceName 
                        + " was not deactivated or reactivated.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Performance Reactivation " +
                    "or Deactivation Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// edits a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="newPerfomanceDescription"> a new description for 
        /// the performance</param>
        /// <param name="newUserIdClient"> the new id of the person who will 
        /// perform the performance</param>
        /// <param name="oldPerfomanceDescription">the original description 
        /// for the performance</param>
        /// <param name="oldUserIdClient"> the original id of the person who 
        /// will perform the performance</param>
        /// <exception>Update Performance Failed. Performance was not eddited.</exception>
        /// <returns>a bool signifying that the performance was edited or not</returns>
        public bool EditePerformance(string performanceName, string newPerfomanceDescription, 
            int newUserIdClient, string oldPerfomanceDescription, int oldUserIdClient)
        {
            bool result = false;
            int rowsAffected = 0;

            try
            {
                rowsAffected =
                    _performanceAccessor.UpdatePerformance(performanceName, 
                    newPerfomanceDescription, newUserIdClient, 
                    oldPerfomanceDescription, oldUserIdClient);
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Performance " + performanceName
                        + " was not eddited.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Performance Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// retrieves all performances for a client
        /// </summary>
        /// 
        /// <param name="userIdClient"> the id of the person who will 
        /// perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of perfomance objects related to a client</returns>
        public List<Performance> RetrievePerformancesByClient(int userIdClient)
        {
            List<Performance> performances = null;

            try
            {
                performances = _performanceAccessor.SelectPerformancesByClient(userIdClient);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Performances Not Available.", ex);
            }

            return performances;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// retrieves all active or inactive performances for a client
        /// </summary>
        ///         
        /// <param name="active"> determines if a performance is deactiveated or not</param>
        /// <param name="userIdClient"> the id of the person who will
        /// perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of active or inactive perfomance objects related to a client</returns>
        public List<Performance> RetrievePerformancesByClientAndActive(int userIdClient, 
            bool active)
        {
            List<Performance> performances = new List<Performance>();

            try
            {
                performances = 
                    _performanceAccessor.SelectPerformancesByClientAndActive(userIdClient, 
                    active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Performances Not Available.", ex);
            }

            return performances;
        }
    }
}
