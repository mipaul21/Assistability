/// <summary>
/// Ryan Taylor
/// Created: 2021/04/07
///
/// contains all the methods to work the website performaces
/// </summary>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/04/24
/// Adding in performance event functions
/// </remarks>
/// 
using DataStorageModels;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PresentationHelpers;

namespace PresentationMVC.Controllers
{
    [Authorize]
    public class PerformanceController : Controller
    {
        private PerformanceManager _performanceManager = new PerformanceManager();
        private UserManager _userManager = new UserManager();
        private PerformanceEventManager _performanceEventManager = new PerformanceEventManager();

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for desplaying a clients performances
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active performances for a client
        ///or to an error screen if failed</returns>
        public ActionResult Performances()
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext); //User.Identity.Name
                ViewBag.User = _selectedUser;
                ViewBag.Active = true;
                return View(
                    _performanceManager.RetrievePerformancesByClientAndActive(
                        _selectedUser.UserAccountID, true));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for desplaying a clients performances by active or not active
        /// </summary>
        ///<param name="selectedUserId">the user id of the client</param>
        ///<param name="activeInactive">the active feild</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active or inative performances for a client
        ///or to an error screen if failed</returns>
        public ActionResult PerformancesByActive(int selectedUserId, bool activeInactive)
        {
            try
            {
                ViewBag.Active = activeInactive;
                ViewBag.User = _userManager.GetUserAccountByUserAccountID(selectedUserId);
                return View(
                    _performanceManager.RetrievePerformancesByClientAndActive(
                        selectedUserId, activeInactive));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for desplaying the details of a performance
        /// </summary>
        ///<param name="performanceName">the name of the performance to be edited</param>
        ///<param name="clientId">the id of the client the perfomance is for</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to performance details page  
        ///or to an error screen if failed</returns>
        public ActionResult Details(string performanceName, int clientId)
        {
            try
            {
                var performanceList = _performanceManager.RetrievePerformancesByClient(clientId);
                var onePerformance =
                    performanceList.Where(p => p.PerformanceName == performanceName).ToList();
                Performance performance = onePerformance[0];
                return View(performance);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for desplaying a list of a performance to edit
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a view of editable list of performances 
        ///or to an error screen if failed</returns>
        public ActionResult EditablePerformances(int SelectedId)
        {
            try
            {
                UserAccount _selectedUser = _userManager.GetUserAccountByUserAccountID(SelectedId); //SelectUserAccountByEmail(AccountController.GetSelectedUserAccountVM(HttpContext).Email);
                ViewBag.User = _selectedUser;
                return View(
                    _performanceManager.RetrievePerformancesByClient(
                        _selectedUser.UserAccountID));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for deactivating and reactivating a performance
        /// </summary>
        ///<param name="performanceName">the name of the performance to be edited</param>
        ///<param name="clientId">the id of the client the perfomance is for</param>
        ///<param name="oldActive">the original active variable</param>
        ///<param name="newActive">the new active variable</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to the editable list of performances 
        ///or to an error screen if failed</returns>
        public ActionResult ActiveDeactive(string performanceName, int clientId, 
            bool oldActive, bool newActive)
        {
            try
            {
                _performanceManager.DeactivateReactivatePerformance(performanceName, clientId,
                    oldActive, newActive);
                return RedirectToAction("EditablePerformances", new { SelectedId = clientId });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for setting up a performance editing page
        /// </summary>
        ///<param name="performanceName">the name of the performance to be edited</param>
        ///<param name="clientId">the id of the client the perfomance is for</param>
        ///<returns>a rederect to an edit view or to an error screen if failed</returns>
        public ActionResult PopulateEdit(string performanceName, int clientId)
        {
            try
            {
                var performanceList = _performanceManager.RetrievePerformancesByClient(clientId);
                var onePerformance =
                    performanceList.Where(p => p.PerformanceName == performanceName).ToList();
                Performance performance = onePerformance[0];
                return View(performance);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for editing a performance
        /// </summary>
        ///<param name="update">information fit into a perfomance model 
        ///from the form to update a performance</param>
        ///<exception>if the description is to long or short</exception>
        ///<returns>a rederect to the edit list on compeation or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult EditPerformance(Performance update) 
        {
            try
            {

                UserAccount _selectedUser = _userManager.GetUserAccountByUserAccountID(update.UserID_client); //.SelectUserAccountByEmail(AccountController.GetSelectedUserAccountVM(HttpContext).Email);
                if (!update.PerformanceDescription.IsValidPerformanceDescription())
                {
                    return RedirectToAction("Error", "Home", new { errorMessage =
                        update.PerformanceDescription +
                        " is not a valid Performance descriotion (255 characters max)"
                    });
                }
                var performanceList =
                    _performanceManager.RetrievePerformancesByClient(_selectedUser.UserAccountID);
                var onePerformance = performanceList.Where(p => p.PerformanceName ==
                    update.PerformanceName).ToList();
                Performance oldPerformance = onePerformance[0];

                _performanceManager.EditePerformance(oldPerformance.PerformanceName,
                update.PerformanceDescription, oldPerformance.UserID_client,
                oldPerformance.PerformanceDescription, oldPerformance.UserID_client);

                return RedirectToAction("EditablePerformances", new { SelectedId = update.UserID_client});
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for setting up a new performance
        /// </summary>
        ///<returns>a view to create a performance</returns>
        public ActionResult Create(int SelectedId) //int clientId, int creatorId
        {
            Performance performance = new Performance()
            {UserID_client = SelectedId};
            return View(performance);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for creating up a new performance
        /// </summary>
        ///<param name="model">information fit into a perfomance model from the form 
        ///to create a performance</param>
        ///<exception>if the perfomance name is already in use by the same client and 
        ///if the name or description is to long or short</exception>
        ///<returns>a rederect to perfomance list on compeation or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult AddNewPerformance(Performance model)
        {
            try
            {
                UserAccount _selectedUser = _userManager.GetUserAccountByUserAccountID(model.UserID_client);// (AccountController.GetSelectedUserAccountVM(HttpContext).Email);
                UserAccount _currentUser = _userManager.SelectUserAccountByEmail(User.Identity.Name);
                if (!model.PerformanceDescription.IsValidPerformanceDescription())
                {
                    return RedirectToAction("Error", "Home", new { errorMessage =
                        model.PerformanceDescription + 
                        " is not a valid Performance descriotion (255 characters max)"
                    });
                }
                if (!model.PerformanceName.IsValidPerformanceName())
                {
                    return RedirectToAction("Error", "Home", new { errorMessage =
                        model.PerformanceName +
                        " is not a valid Performance name (50 characters max)"
                    });
                }
                _performanceManager.CreatePerformance(model.PerformanceName,
                model.PerformanceDescription, _selectedUser.UserAccountID,
                _currentUser.UserAccountID);
                return RedirectToAction("Performances", "Performance");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (ex.InnerException.Message.Contains("pk_UserID_client_PerformanceName"))
                {
                    error = "You already have a performance "
                        +model.PerformanceName+" for this client.";
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error});
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/25
        ///
        /// a method for showing all the performance events for a performance
        /// </summary>
        ///<param name="Name">the name of the performance</param>
        ///<exception></exception>
        ///<returns>a rederect to a list of performance events on compeation or to 
        ///an error screen if failed</returns>
        public ActionResult PerformanceEvents(string Name, int SelectedId) 
        {
            try
            {
                UserAccount _selectedUser = _userManager.GetUserAccountByUserAccountID(SelectedId);  //AccountController.GetSelectedUserAccount(HttpContext);
                List<PerformanceEvent> events = _performanceEventManager.RetrieveAllPerformanceEventsByPerformanceName(Name);
                var performanceList = _performanceManager.RetrievePerformancesByClient(_selectedUser.UserAccountID);
                var onePerformance =
                    performanceList.Where(p => p.PerformanceName == Name).ToList();
                ViewBag.Performance = onePerformance[0];
                List<PerformanceEvent> performanceEvents = events.Where(e => e.UserIDClient == _selectedUser.UserAccountID).ToList();
                return View(performanceEvents);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/25
        ///
        /// a method for setting up a new performance event
        /// </summary>
        ///<param name="Name">the name of the performance geting the event</param>
        ///<returns>a view to create a performance event</returns>
        public ActionResult CreatePerformanceEvent(string Name, int SelectedId)
        {
            UserAccount _selectedUser = _userManager.GetUserAccountByUserAccountID(SelectedId); //AccountController.GetSelectedUserAccount(HttpContext);
            UserAccount _currentUser = _userManager.SelectUserAccountByEmail(User.Identity.Name);
            var performanceList = _performanceManager.RetrievePerformancesByClient(_selectedUser.UserAccountID);
            var onePerformance =
                performanceList.Where(p => p.PerformanceName == Name).ToList();
            Performance performance = onePerformance[0];
            PerformanceEvent newPerformanceEvent = new PerformanceEvent() 
            {
                PerformanceName = performance.PerformanceName,
                UserIDClient = _selectedUser.UserAccountID,
                UserIDReporter = _currentUser.UserAccountID,
            };
            return View(newPerformanceEvent);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/25
        ///
        /// a method for creating up a new performance event
        /// </summary>
        ///<param name="performanceEvent">information fit into a perfomance event model from
        ///the form to create a performance event</param>
        ///<exception>if the performance event can't be created</exception>
        ///<returns>a rederect to perfomance event list on compeation or 
        ///to an error screen if failed</returns>
        [HttpPost]
        public ActionResult AddPerformanceEvent(PerformanceEvent performanceEvent) 
        {
            try
            {
                _performanceEventManager.AddNewPerformanceEvent(performanceEvent.PerformanceName,
                    performanceEvent.UserIDClient, performanceEvent.UserIDReporter, 
                    performanceEvent);
                return RedirectToAction("PerformanceEvents", "Performance", 
                    new { Name = performanceEvent.PerformanceName, SelectedId = performanceEvent.UserIDClient });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/25
        ///
        /// a method for setting up a performance event editing page
        /// </summary>
        ///<param name="Name">the name of the performance the event belongs to</param>
        ///<param name="id">the id of the perfomance event being edited</param>
        ///<param name="SelectedUserId">the id of the person the perfomance event is for</param>
        ///<returns>a rederect to an edit view or to an error screen if failed</returns>
        public ActionResult EditPerformanceEvent(int id, string Name, int SelectedUserId) 
        {
            try
            {
                List<PerformanceEvent> events = 
                    _performanceEventManager.RetrieveAllPerformanceEventsByPerformanceName(Name);
                List<PerformanceEvent> oneEvent = events.Where(e => e.PerformanceEventID == id).ToList();
                PerformanceEvent performanceEvent = oneEvent[0];
                List<Performance> performances = _performanceManager.RetrievePerformancesByClient(SelectedUserId);
                List<string> performanceNames = new List<string>();
                foreach (var performance in performances)
                {
                    performanceNames.Add(performance.PerformanceName);
                }
                ViewBag.PerformanceNames = performanceNames;
                return View(performanceEvent);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/01
        ///
        /// a method for editing a performance
        /// </summary>
        ///<param name="update">information fit into a perfomance model 
        ///from the form to update a performance</param>
        ///<exception>if the description is to long or short</exception>
        ///<returns>a rederect to the edit list on compeation or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult UpdatePerformanceEvent(PerformanceEvent newPerfomanceEvent) 
        {
            try
            {
                List<PerformanceEvent> events =
                    _performanceEventManager.RetrieveAllPerformanceEventsByUserID(
                        newPerfomanceEvent.UserIDClient);
                List<PerformanceEvent> oneEvent = events.Where(e => e.PerformanceEventID == 
                newPerfomanceEvent.PerformanceEventID).ToList();
                PerformanceEvent oldPerformanceEvent = oneEvent[0];
                _performanceEventManager.EditPerformanceEvent(oldPerformanceEvent, newPerfomanceEvent);
                return RedirectToAction("PerformanceEvents", "Performance",
                    new { Name = newPerfomanceEvent.PerformanceName, SelectedId = newPerfomanceEvent.UserIDClient });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

    }
}