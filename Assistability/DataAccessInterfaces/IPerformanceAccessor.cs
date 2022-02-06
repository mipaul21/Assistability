/// <summary>
/// Ryan Taylor
/// Created: 2021/03/29
/// 
/// This is an interface for data access of perfomances
/// </summary>
/// 
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IPerformanceAccessor
    {
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// selects all performances for a client
        /// </summary>
        /// 
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of perfomance objects related to a client</returns>
        List<Performance> SelectPerformancesByClient(int userIdClient);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// selects all active or inactive performances for a client
        /// </summary>
        ///         
        /// <param name="active"> deturmins if a performance is deactiveated or not</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of active or inactive perfomance objects related to a client</returns>
        List<Performance> SelectPerformancesByClientAndActive(int userIdClient, bool active);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// Inserts a new performance for a client
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="perfomanceDescription"> a detailed description of the performance</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <param name="userIdCreator"> The UserID of the user who created the performance</param>
        /// <exception>Performance not inserted</exception>
        /// <returns>an int signifying the rows affected</returns>
        int InsertNewPerformance(string performanceName, string perfomanceDescription,
            int userIdClient, int userIdCreator);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// updates a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="newPerfomanceDescription"> a new description for the performance</param>
        /// <param name="newUserIdClient"> the new id of the person who will perform the performance</param>
        /// <param name="oldPerfomanceDescription">the original description for the performance</param>
        /// <param name="oldUserIdClient"> the original id of the person who will perform the performance</param>
        /// <exception>Performance not created</exception>
        /// <returns>an int signifying the rows affected</returns>
        int UpdatePerformance(string performanceName, string newPerfomanceDescription,
            int newUserIdClient, string oldPerfomanceDescription, int oldUserIdClient);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// edits a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="UserIdClient"> the id of the person who will perform the performance</param>
        /// <param name="oldActive">the original state of the performance</param>
        /// <param name="newActive">the new state of the performance</param>
        /// <exception>Performance not reactivated or deactivated</exception>
        /// <returns>an int signifying the rows affected</returns>
        int DeactivateReactivatePerformance(string performanceName, int UserIdClient,
            bool oldActive, bool newActive);
    }
}
