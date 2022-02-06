/// <summary>
/// Ryan Taylor
/// Created: 2021/04/14
///
/// contains all the methods to work the website Routine
/// </summary>
using DataStorageModels;
using DataViewModels;
using LogicInterfaces;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.Ajax.Utilities;
using PresentationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PresentationMVC.Controllers
{
	[Authorize]
    public class RoutineController : Controller
    {
        private RoutineManager _routineManager = new RoutineManager();
        private UserManager _userManager = new UserManager();
        private RoutineStepManager _routineStepManager = new RoutineStepManager();

        /// <summary>
        /// Nathaniel Webber (W/ lots of help from Jim)
        /// Created: 2021/04/30
        ///
        /// a method for desplaying a User routines
        /// </summary>
        [ChildActionOnly]
        public PartialViewResult RoutinePartial()
        {
            List<Routine> routines = new List<Routine>();
            UserAccount user = AccountController.GetSelectedUserAccount(HttpContext);
            // routines.Add(new Routine("Test", "This is making me testy.", 25, 35, true));
            foreach (var routine in 
                _routineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(DateTime.Today, user.UserAccountID))
            {
                routines.Add(new Routine(
                    routine.Name, 
                    routine.Description, 
                    routine.UserAccountID_Client, 
                    routine.UserAccountID_Admin, 
                    routine.Active)
                    );
            }

            return PartialView(routines);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        ///
        /// a method for desplaying a clients routines
        /// </summary>
        ///<returns>a rederect to a list of routines for a client</returns>
        public ActionResult Routines()
        {
            UserAccount user = AccountController.GetSelectedUserAccount(HttpContext);
            return View(_routineManager.GetRoutinesByuserID(user.UserAccountID));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        ///
        /// a method for setting up a form to crate a new routine
        /// </summary>
        ///<returns>a view to create a routine</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        ///
        /// a method for creating a new routine
        /// </summary>
        ///<param name="Name">the name of the routine</param>
        ///<param name="Description">the description of the routine</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of the selected users routines
        ///or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult CreateRoutine(string Name, string Description)
        {
            UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            UserAccount currentUser = AccountController.GetAuthenticatedUserAccount(HttpContext);
            Routine routine = new Routine(Name, Description, DateTime.Now, selectedUser.UserAccountID, currentUser.UserAccountID, true);
            if (!routine.Name.IsValidRoutineName()) 
            {
                return RedirectToAction("Error", "Home", new { errorMessage = Name+ " is too long (50 characters max)." });
            }
            if (!routine.Description.IsValidRoutineDescription())
            {
                return RedirectToAction("Error", "Home", new { errorMessage = Description + " is too long (150 characters max)." });
            }
            try
            {
                _routineManager.AddNewRoutine(routine);
                return RedirectToAction("Routines", "Routine");//, new { clientID = routine.UserAccountID_Client }
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (error.Contains("pk_RoutineName")) 
                {
                    error = "routine "+Name+" already exists for this client";
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        ///
        /// a method for seting up an edit from for a routine
        /// </summary>
        ///<param name="Name">the name of the routine</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a form for editing a routine
        ///or to an error screen if failed</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string Name) 
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                List<Routine> routines =
                    _routineManager.GetRoutinesByuserID(selectedUser.UserAccountID);
                List<Routine> oneRoutine = routines.Where(r => r.Name == Name).ToList();
                Routine theRoutine = oneRoutine[0];
                return View(theRoutine);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/16
        ///
        /// a method for editing a routine
        /// </summary>
        ///<returns>to the list of routines showing that the routine is edited</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditRoutine(string Description, bool Active, string OldName)
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                List<Routine> routines = 
                    _routineManager.GetRoutinesByuserID(selectedUser.UserAccountID);
                List<Routine> oneRoutine = routines.Where(r => r.Name == OldName).ToList();
                Routine theOldRoutine = oneRoutine[0];
                Routine updatedRoutine = new Routine(OldName, Description, theOldRoutine.UserAccountID_Client, theOldRoutine.UserAccountID_Admin, Active);
                updatedRoutine.EditDate = DateTime.Now;
                theOldRoutine.EditDate = DateTime.Now;
                if (!updatedRoutine.Description.IsValidRoutineDescription())
                {
                    return RedirectToAction("Error", "Home", new { errorMessage = Description + " is too long (150 characters max)." });
                }
                if (updatedRoutine.Active != theOldRoutine.Active) 
                {
                    updatedRoutine.RemovalDate = DateTime.Now;
                    theOldRoutine.RemovalDate = DateTime.Now;
                }
                _routineManager.UpdateRoutine(theOldRoutine, updatedRoutine);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            return RedirectToAction("Routines", "Routine");
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        ///
        /// a method for showing the details of a routine
        /// </summary>
        ///<param name="Name">the name of the routine</param>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a form for editing a routine
        ///or to an error screen if failed</returns>
        public ActionResult Details(string Name)
        {
            UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            List<Routine> routines =
                    _routineManager.GetRoutinesByuserID(selectedUser.UserAccountID);
            List<Routine> oneRoutine = routines.Where(r => r.Name == Name).ToList();
            Routine theRoutine = oneRoutine[0];
            return View(theRoutine);

        }
		
		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/21
        ///
        /// a method for showing the a routines steps
        /// </summary>
        ///<param name="Name">the name of the routine</param>
        ///<returns>a rederect to a list of the routines steps</returns>
        public ActionResult RoutineSteps(string Name)
        {
            UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            List<Routine> routines =
                    _routineManager.GetRoutinesByuserID(selectedUser.UserAccountID);
            List<Routine> oneRoutine = routines.Where(r => r.Name == Name).ToList();
            Routine theRoutine = oneRoutine[0];
            List<RoutineStep> routinesSteps = _routineManager.GetRoutineStepsByRoutine(theRoutine);
            ViewBag.Routine = theRoutine;
            return View(routinesSteps);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/22
        ///
        /// a method for seting up the creation of a routine step
        /// </summary>
        ///<param name="Name">the name of the routine</param>
        ///<returns>a rederect to a form to create a routine step</returns>
        public ActionResult CreateRoutineStep(string Name) 
        {
            RoutineStep step = new RoutineStep() { RoutineName = Name};
            return View(step);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/22
        ///
        /// a method for creating a routine step
        /// </summary>
        ///<param name="RoutineName">the name of the routine</param>
        ///<param name="RoutineStepName">the name of the routine step</param>
        ///<param name="RoutineStepDescription">the description of the routine step</param>
        ///<exception>if something goes wrong or if the step already exists</exception>
        ///<returns>a rederect the routines steps with the new step included</returns>
        [HttpPost]
        public ActionResult AddRoutineStep(string RoutineName, 
            string RoutineStepName, string RoutineStepDescription)
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                List<Routine> routines =
                        _routineManager.GetRoutinesByuserID(selectedUser.UserAccountID);
                List<Routine> oneRoutine = routines.Where(r => r.Name == RoutineName).ToList();
                Routine theRoutine = oneRoutine[0];
                List<RoutineStep> routinesSteps = _routineManager.GetRoutineStepsByRoutine(theRoutine);
                int order;
                if (routinesSteps == null)
                {
                    order = 1;
                }
                else
                {
                    order = routinesSteps.Count() + 1;
                }

                RoutineStep newStep = new RoutineStep()
                {
                    RoutineStepOrderNumber = order,
                    RoutineName = RoutineName,
                    RoutineStepDescription = RoutineStepDescription,
                    RoutineStepName = RoutineStepName
                };
                _routineStepManager.AddNewRoutineStep(newStep);
                return RedirectToAction("RoutineSteps", "Routine", new { Name = newStep.RoutineName });
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }
		
        /// <summary>
        /// William Clark
        /// Created: 2021/04/22
        /// View a list of routines that have not been completed for a given day
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        // GET: Routine
        public ActionResult Index(DateTime? date)
        {
            DateTime selectedDate = DateTime.Today;
            if(date != null)
            {
                selectedDate = date.GetValueOrDefault();
            }
            IEnumerable<Routine> activeRoutines = new List<Routine>();
            try
            {
                UserAccount oldUser = AccountController.GetSelectedUserAccount(HttpContext);

                var RoutineManager = new RoutineManager();

                int routines = RoutineManager.SelectActiveRoutinesByUserAccountIDClient(oldUser.UserAccountID).Count();
                if (routines > 0)
                {
                    activeRoutines = RoutineManager.SelectActiveRoutinesWithoutCompletionByUserAccountIDClient(selectedDate, oldUser.UserAccountID);
                }
                else
                {
                    ViewBag.HasRoutines = false;
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View("ActiveIncompleteRoutines", activeRoutines);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/22
        /// Redirects to the Index
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        // GET: Routine
        public ActionResult Routine()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/22
        /// Redirects to the Index
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        // GET: Routine
        public ActionResult ActiveIncompleteRoutines()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/28
        /// The controller method to request the CompleteRoutine View
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        // GET: Routine
        public ActionResult Complete(string id)
        {
            IRoutineManager routineManager = new RoutineManager();
            IRoutineStepManager routineStepManager = new RoutineStepManager();
            try
            {
                // Get the selected routine
                Routine selected = routineManager.GetRoutinesByuserID(AccountController.GetSelectedUserAccount(HttpContext).UserAccountID).Find(r => r.Name == id);
                RoutineVM routine = new RoutineVM(selected, routineManager.GetRoutineStepsByRoutine(selected).Where(step => step.Active == true).ToList());

                // Get the completed steps
                List<int> completedStepIds = routineStepManager.SelectRoutineStepCompletionsByDayByRoutineName(routine.Name, DateTime.Now);

                ViewBag.CompletedStepIds = completedStepIds;

                bool routineCompleted = false;
                int completedSteps = 0;
                foreach (var step in routine.Steps)
                {
                    if (completedStepIds.Contains(step.RoutineStepID))
                    {
                        completedSteps++;
                    }
                }
                routineCompleted = completedSteps == routine.Steps.Count;
                if (routineCompleted)
                {
                    return RedirectToAction("Index");
                }

                return View("Complete", routine);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/29
        /// The controller method for the post request of CompleteStep
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [HttpPost]
        public ActionResult CompleteStep()
        {
            IRoutineManager routineManager = new RoutineManager();
            IRoutineStepManager routineStepManager = new RoutineStepManager();
            UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            
            if (Request.Form.Get("stepId") != null)
            {
                try
                {
                    Routine routine = routineManager.SelectActiveRoutinesByUserAccountIDClient(selectedUser.UserAccountID).Find(r => r.Name == Request.Form.Get("routineName"));
                    RoutineVM routineVM = new RoutineVM(routine, routineManager.GetRoutineStepsByRoutine(routine));
                    routineManager.CompleteRoutineStep(routineVM.Steps.Find(step => step.RoutineStepID == int.Parse(Request.Form.Get("stepId"))), selectedUser);

                    // Get the completed steps
                    List<int> completedStepIds = routineStepManager.SelectRoutineStepCompletionsByDayByRoutineName(routine.Name, DateTime.Now);

                    ViewBag.CompletedStepIds = completedStepIds;

                    // Determine if the routine has been completed and if so, store the completion
                    bool routineCompleted = false;
                    int completedSteps = 0;
                    foreach (var step in routineVM.Steps)
                    {
                        if (completedStepIds.Contains(step.RoutineStepID))
                        {
                            completedSteps++;
                        }
                    }
                    routineCompleted = completedSteps == routineVM.Steps.Count;
                    if (routineCompleted)
                    {
                        routineManager.CompleteRoutine(routineVM, selectedUser);
                    }
                }
                catch (Exception)
                {
                    // Return to the routine
                    return Redirect(Request.UrlReferrer.OriginalString);
                }
            }

            return Redirect(Request.UrlReferrer.OriginalString);
        }
    }
}