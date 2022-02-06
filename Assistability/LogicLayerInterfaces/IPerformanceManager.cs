/// <summary>
/// Ryan Taylor
/// Created: 2021/03/29
/// 
/// This is an interface for manager of perfomances
/// </summary>
/// 
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IPerformanceManager
    {
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// retrieves all performances for a client
        /// </summary>
        /// 
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of perfomance objects related to a client</returns>
        List<Performance> RetrievePerformancesByClient(int userIdClient);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// retrieves all active or inactive performances for a client
        /// </summary>
        ///         
        /// <param name="active"> deturmins if a performance is deactiveated or not</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <exception>Performance list not found</exception>
        /// <returns>list of active or inactive perfomance objects related to a client</returns>
        List<Performance> RetrievePerformancesByClientAndActive(int userIdClient, bool active);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// Creates a new performance for a client
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="perfomanceDescription"> a detailed description of the performance</param>
        /// <param name="userIdClient"> the id of the person who will perform the performance</param>
        /// <param name="userIdCreator"> The UserID of the user who created the performance</param>
        /// <exception>Performance not created</exception>
        /// <returns>a bool signifying that the performance was created or not</returns>
        bool CreatePerformance(string performanceName, string perfomanceDescription,
            int userIdClient, int userIdCreator);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// edits a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="newPerfomanceDescription"> a new description for the performance</param>
        /// <param name="newUserIdClient"> the new id of the person who will perform the performance</param>
        /// <param name="oldPerfomanceDescription">the original description for the performance</param>
        /// <param name="oldUserIdClient"> the original id of the person who will perform the performance</param>
        /// <exception>Performance not created</exception>
        /// <returns>a bool signifying that the performance was edited or not</returns>
        bool EditePerformance(string performanceName, string newPerfomanceDescription,
            int newUserIdClient, string oldPerfomanceDescription, int oldUserIdClient);

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/29
        /// 
        /// deactivates or reactivates a performance for a client.
        /// </summary>
        /// 
        /// <param name="performanceName"> The name of the performance</param>
        /// <param name="UserIdClient"> the id of the person who will perform the performance</param>
        /// <param name="oldActive">the original state of the performance</param>
        /// <param name="newActive">the new state of the performance</param>
        /// <exception>Performance not reactivated or deactivated</exception>
        /// <returns>a bool signifying that the performance was deactivated or reactivated</returns>
        bool DeactivateReactivatePerformance(string performanceName, int UserIdClient, 
            bool oldActive, bool newActive);
    }
}
