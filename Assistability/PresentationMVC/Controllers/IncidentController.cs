/// <summary>
/// Nick Loesel
/// Created: 2021/04/016
///
/// contains all the methods to work the website incidents
/// </summary>
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
    public class IncidentController : Controller
    {
        private IncidentManager _incidentManager = new IncidentManager();
        private UserAccount _selectedUser = new UserAccount();
        private UserAccount _currentUser = new UserAccount();
        private UserManager _userManager = new UserManager();


        public IncidentController()
        {

        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/15
        ///
        /// a method for displaying a clients incidents
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active incidents for a client
        ///or to an error screen if failed</returns>
        public ActionResult Incidents()
        {
            try
            {

                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                ViewBag.User = _selectedUser;
                ViewBag.Active = true;
                return View(
                    _incidentManager.SelectIncidentsByActive(
                        _selectedUser.UserAccountID, true));
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/15
        ///
        /// a method for desplaying a clients incidents by active or not active
        /// </summary>
        ///<param name="selectedUserId">the user id of the client</param>
        ///<param name="activeInactive">the active feild</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active or inative performances for a client
        ///or to an error screen if failed</returns>
        public ActionResult IncidentsByActive(bool activeInactive)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                ViewBag.Active = activeInactive;
                ViewBag.User = _userManager.GetUserAccountByUserAccountID(_selectedUser.UserAccountID);
                return View(
                    _incidentManager.SelectIncidentsByActive(
                     _selectedUser.UserAccountID, activeInactive));
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/15
        ///
        /// the method for deactivating an incident
        /// </summary>
        ///<param name="performanceName">the name of the performance to be edited</param>
        ///<param name="clientId">the id of the client the perfomance is for</param>
        ///<param name="oldActive">the original active variable</param>
        ///<param name="newActive">the new active variable</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to the editable list of performances 
        ///or to an error screen if failed</returns>
        public ActionResult Deactivate(string incidentName, string incidentDescription,
            string desiredConsequence, DateTime incidentEntryDate, DateTime? incidentEditDate,
            DateTime? incidentRemovalDate)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                ViewBag.Active = false;
                bool activeInactive = false;
                ViewBag.User = _userManager.GetUserAccountByUserAccountID(_selectedUser.UserAccountID);
                Incident incident = new Incident(incidentName,incidentDescription, desiredConsequence, incidentEntryDate, incidentEditDate, incidentRemovalDate, true, _selectedUser.UserAccountID, _currentUser.UserAccountID);
                _incidentManager.DeactivateIncident(_selectedUser, incident);
                return RedirectToAction("IncidentsByActive", new {_selectedUser.UserAccountID, activeInactive});
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/04/15
        ///
        /// a method for reactivating an incident
        /// </summary>
        ///<param name="performanceName">the name of the performance to be edited</param>
        ///<param name="clientId">the id of the client the perfomance is for</param>
        ///<param name="oldActive">the original active variable</param>
        ///<param name="newActive">the new active variable</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to the editable list of performances 
        ///or to an error screen if failed</returns>
        public ActionResult Reactivate( string incidentName, string incidentDescription, string desiredConsequence, DateTime incidentEntryDate, DateTime? incidentEditDate, DateTime? incidentRemovalDate)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                ViewBag.Active = true;
                ViewBag.User = _userManager.GetUserAccountByUserAccountID(_selectedUser.UserAccountID);
                Incident incident = new Incident(incidentName, incidentDescription, desiredConsequence, incidentEntryDate, incidentEditDate, incidentRemovalDate, true, _selectedUser.UserAccountID, _currentUser.UserAccountID);
                _incidentManager.Reactivateincident(_selectedUser, incident);
                return RedirectToAction("Incidents");
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }



        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/15
        ///
        /// a method for setting up a new incident
        /// </summary>
        ///<returns>a view to create a performance</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/15
        ///
        /// a method for setting up the edit for an incident
        /// </summary>
        ///<returns>a view to create a performance</returns>
        public ActionResult Edit(string incidentName)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                var incidentList = _incidentManager.SelectIncidentsByUserID(_selectedUser.UserAccountID);
                var oneIncident =
                    incidentList.Where(i => i.IncidentName == incidentName).ToList();
                Incident incident = oneIncident[0];
                return View(incident);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/15
        ///
        /// a method for creating a new incident
        /// </summary>
        ///<param name="model">information fit into a incident model from the form 
        ///to create an incident</param>
        ///<exception>if the incident name is already in use by the same client and 
        ///if the name or description is to long or short</exception>
        ///<returns>a rederect to perfomance list on compeation or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult AddNewIncident(Incident model)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                if (!model.IncidentName.IsValidIncidentName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.IncidentName +
                        " is not a valid incident name (50 characters max)"
                    });
                }
                if (!model.IncidentDescription.IsValidIncidentDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.IncidentDescription +
                        " is not a valid incident description (250 characters max)"
                    });
                }
                if (!model.DesiredConsequence.IsValidDesiredConsequence())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.DesiredConsequence +
                        " is not a valid desired consequence (250 characters max)"
                    });
                }
                Incident incident = new Incident(model.IncidentName, model.IncidentDescription, model.DesiredConsequence,
                    DateTime.Now, null, null, true, _selectedUser.UserAccountID, _currentUser.UserAccountID);
                _incidentManager.AddNewIncident(incident);
                return RedirectToAction("Incidents", "Incident");
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (ex.InnerException.Message.Contains("pk_IncidentName"))
                {
                    error = "You already have an incident "
                        + model.IncidentName + " for this client.";
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// NickLoesel
        /// Created: 2021/04/15
        ///
        /// a method for editing an incident
        /// </summary>
        ///<param name="updatedincident">the new incident to edit</param>
        ///<exception>if the incident name is already in use by the same client and 
        ///if the name or description is to long or short</exception>
        ///<returns>a rederect to perfomance list on compeation or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult EditIncident(Incident updatedincident)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                UserAccount _currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
                if (!updatedincident.IncidentDescription.IsValidIncidentDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        updatedincident.IncidentDescription +
                        " is not a valid Performance descriotion (255 characters max)"
                    });
                }
                if (!updatedincident.DesiredConsequence.IsValidDesiredConsequence())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        updatedincident.DesiredConsequence +
                        " is not a valid desired consequence (255 characters max)"
                    });
                }
                var incidentList =
                    _incidentManager.SelectIncidentsByUserID(_selectedUser.UserAccountID);
                var oneIncident = incidentList.Where(i => i.IncidentName ==
                    updatedincident.IncidentName).ToList();
                Incident oldIncident = oneIncident[0];

                _incidentManager.UpdateIncident(oldIncident, updatedincident);

                return RedirectToAction("Incidents");
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

    }
}