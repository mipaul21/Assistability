/// <summary>
/// Ryan Taylor
/// Created: 2021/03/30
/// 
/// This class is the 'fakes' class used
/// to test the Perfomance manager methods
/// </summary>
using DataAccessInterfaces;
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class PerformanceFakes : IPerformanceAccessor
    {
        public List<Performance> performances = new List<Performance>()
        {
            new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 3,
                PerformanceName = "keep swimming",
                PerformanceDescription = "Just keep swimming",
                PerformanceEntryDate = DateTime.Now,
                Active = true,
            },

            new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 3,
                PerformanceName = "Say thank you",
                PerformanceDescription = "Say thank you more often",
                PerformanceEntryDate = DateTime.Now,
                Active = false,
            },

            new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 3,
                PerformanceName = "Fake",
                PerformanceDescription = "Nothing more than a fake",
                PerformanceEntryDate = DateTime.Now,
                Active = true,
            },

            new Performance()
            {
                UserIDCreator = 1,
                UserID_client = 1,
                PerformanceName = "different",
                PerformanceDescription = "Something different",
                PerformanceEntryDate = DateTime.Now,
                Active = true,
            },
        };
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// a fake method used to edit a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="UserIdClient"> the id of the person who will perform the performance</param>
        /// <param name="oldActive">the original state of the performance</param>
        /// <param name="newActive">the new state of the performance</param>
        /// <exception>Performance not reactivated or deactivated</exception>
        /// <returns>an int signifying the rows affected</returns>
        public int DeactivateReactivatePerformance(string performanceName, int UserIdClient, 
            bool oldActive, bool newActive)
        {
            int rowsAffected = 0;
            Performance oldPerformance = new Performance();
            Performance reDeactivatedPerformance = new Performance();
            foreach (Performance performance in performances)
            {
                if (performance.UserID_client == UserIdClient
                    && performance.PerformanceName == performanceName
                    && performance.Active == oldActive)
                {
                    oldPerformance = performance;
                }
            }
            if (oldPerformance != null)
            {
                reDeactivatedPerformance = oldPerformance;
                reDeactivatedPerformance.Active = newActive;
                performances.Remove(oldPerformance);
                performances.Add(reDeactivatedPerformance);
            }
            if (performances.Contains(reDeactivatedPerformance))
            {
                rowsAffected = 1;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// a fake method used to inserts a new performance for a client
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="perfomanceDescription"> a detailed description of the performance</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <param name="userIdCreator"> The UserID of the user who created the performance</param>
        /// <exception>Performance not inserted</exception>
        /// <returns>an int signifying the rows affected</returns>
        public int InsertNewPerformance(string performanceName, string perfomanceDescription, 
            int userIdClient, int userIdCreator)
        {
            int rowsAffected = 0;

            int rowsBefore = performances.Count();
            Performance newPerformance = new Performance()
            {
                PerformanceName = performanceName,
                PerformanceDescription = perfomanceDescription,
                UserIDCreator = userIdClient,
                UserID_client = userIdClient,
            };
            performances.Add(newPerformance);
            int rowsAfter = performances.Count();

            if (rowsBefore < rowsAfter) 
            {
                rowsAffected = 1;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// a fake method used to select all performances for a client
        /// </summary>
        /// 
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of perfomance objects related to a client</returns>
        public List<Performance> SelectPerformancesByClient(int userIdClient)
        {
            List<Performance> clientPerformances = new List<Performance>();

            foreach (Performance performance in performances)
            {
                if (performance.UserID_client == userIdClient) 
                {
                    clientPerformances.Add(performance);
                }
            }

            return clientPerformances;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// a fake method used to select all active or inactive performances for a client
        /// </summary>
        ///         
        /// <param name="active"> deturmins if a performance is deactiveated or not</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of active or inactive perfomance objects related to a client</returns>
        public List<Performance> SelectPerformancesByClientAndActive(int userIdClient, 
            bool active)
        {
            List<Performance> clientPerformances = new List<Performance>();

            foreach (Performance performance in performances)
            {
                if (performance.UserID_client == userIdClient && performance.Active == active)
                {
                    clientPerformances.Add(performance);
                }
            }

            return clientPerformances;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/30
        /// 
        /// a fake method used to update a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="newPerfomanceDescription"> a new description for the performance</param>
        /// <param name="newUserIdClient"> the new id of the person who will perform the performance</param>
        /// <param name="oldPerfomanceDescription">the original description for the performance</param>
        /// <param name="oldUserIdClient"> the original id of the person who will perform the performance</param>
        /// <exception>Performance not created</exception>
        /// <returns>an int signifying the rows affected</returns>
        public int UpdatePerformance(string performanceName, string newPerfomanceDescription, 
            int newUserIdClient, string oldPerfomanceDescription, int oldUserIdClient)
        {
            int rowsAffected = 0;
            Performance oldPerformance = new Performance();
            Performance editedPerformance = new Performance();
            foreach (Performance performance in performances)
            {
                if (performance.UserID_client == oldUserIdClient 
                    && performance.PerformanceName == performanceName
                    && performance.PerformanceDescription == oldPerfomanceDescription)
                {
                    oldPerformance = performance;
                }
            }
            if (oldPerformance != null) 
            {
                editedPerformance = oldPerformance;
                editedPerformance.PerformanceDescription = newPerfomanceDescription;
                editedPerformance.UserID_client = newUserIdClient;
                performances.Remove(oldPerformance);
                performances.Add(editedPerformance);
            }
            if (performances.Contains(editedPerformance)) 
            {
                rowsAffected = 1;
            }

            return rowsAffected;
        }
    }
}
