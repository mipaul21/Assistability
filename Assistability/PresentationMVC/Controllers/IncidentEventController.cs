using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataStorageModels;
using LogicLayer;
using PresentationHelpers;

namespace PresentationMVC.Controllers
{
    public class IncidentEventController : Controller
    {

        private IncidentEventManager _incidentEventManger = new IncidentEventManager();
        private IncidentManager _incidentManager = new IncidentManager();
        private UserManager _userManager = new UserManager();
        private Incident _selectedIncident = new Incident();
        private UserAccount _selectedUser = new UserAccount();
        private UserAccount _currentUser = new UserAccount();

        // GET: IncidentEvent
      public IncidentEventController()
        {

        }

        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/28
        ///
        /// a method for displaying incident events
        /// </summary>
        ///<param name="selectedUserId">the user id of the client</param>
        ///<param name="incidentName">the active feild</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active or inative performances for a client
        ///or to an error screen if failed</returns>
        public ActionResult IncidentEvents(string incidentName)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                ViewBag.User = _userManager.GetUserAccountByUserAccountID(_selectedUser.UserAccountID);
                ViewBag.IncidentName = incidentName;
                return View(_incidentEventManger.SelectIncidentsEventsByIncidentName(incidentName));
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Create(string incidentName) //int clientId, int creatorId
        {
            ViewBag.IncidentName = incidentName;
            return View();
        }

        public ActionResult Delete(int incidentEventId)
        {
            return View(_incidentEventManger.SelectIncidentEventById(incidentEventId));
        }
           
        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/15
        ///
        /// a method for creating a new incident event
        /// </summary>
        ///<param name="model">information fit into a incident event model from the form 
        ///to create an incident</param>
        ///<exception>if the incident name is already in use by the same client and 
        ///if the name or description is to long or short</exception>
        ///<returns>a rederect to perfomance list on compeation or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult AddNewIncidentEvent(IncidentEvent model)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                if (!model.EventConsequence.IsValidEventConsquence())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.EventConsequence +
                        " is not a valid event consequence. (500 characters max)"
                    });
                }
                if (!model.PersonsInvolved.IsValidPersonsInvolved())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.PersonsInvolved +
                        " is not a valid value for persons involved. (250 characters max)"
                    });
                }
                if (!model.EventDescription.IsValidEventDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.EventDescription +
                        " is not a valid desired consequence (250 characters max)"
                    });
                }
                IncidentEvent newIncidentEvent = new IncidentEvent(model.IncidentName, model.DateOfOccurence , model.PersonsInvolved,
                    model.EventDescription, model.EventConsequence, null, _selectedUser.UserAccountID,
                    _currentUser.UserAccountID);
                _incidentEventManger.InsertNewIncidentEvent(newIncidentEvent);
                return RedirectToAction("Incidents", "Incident");
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (ex.InnerException.Message.Contains("pk"))
                {
                    error = "You already have an incident "
                        + model.IncidentName + " for this client.";
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        [HttpPost]
        public ActionResult DeleteIncidentEvent(IncidentEvent model)
        {
            try
            {
                _incidentEventManger.DeleteIncidentEvent(model.IncidentEventID);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                error = "There was an error deleting the supplied incident event.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });

            }
            return RedirectToAction("Incidents", "Incident");
        }
    }
}