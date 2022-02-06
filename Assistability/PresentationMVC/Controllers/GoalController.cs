using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataViewModels;
using DataStorageModels;
using System.Dynamic;
using PresentationHelpers;


namespace PresentationMVC.Controllers
{
    public class GoalController : Controller
    {
        private HabGoalManager _habGoalManager = new HabGoalManager();
        private AttGoalManager _attGoalManager = new AttGoalManager();
        private ExtGoalManager _extGoalManager = new ExtGoalManager();
        private AwardManager _awardManager = new AwardManager();
        private RoutineManager _routineManager = new RoutineManager();
        private IncidentManager _incidentManager = new IncidentManager();
        private PerformanceManager _performanceManager = new PerformanceManager();
        private UserManager _userManager = new UserManager();
        private bool active;

        // -------------------- **** Main Goal Page **** -------------------- //
        // Main Goal Page to make selection on where to go and what to do
        public ActionResult Goals()
        {
            UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            return View(selectedUser);
        }

        // -------------------- **** View Goal(Active and all) **** -------------------- //

        // -------------------- **** View Habitual Goals(Active and all) **** -------------------- //

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Send List of Habitual Goal for the selected user.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns>
        /// A list of Habitual Goal by the selected users ID. If fails to load list returns to the
        /// main goal page.
        /// </returns>
        public ActionResult ViewHabGoals()
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                ViewBag.SelectedUser = selectedUser;
                return View(_habGoalManager.RetrieveHabitualGoalsByUserIDClient(selectedUser.UserAccountID));
            }
            catch (Exception)
            {
                
                ModelState.AddModelError(string.Empty, "Could not load a list Habitual Goals.");
                return RedirectToAction("Goals", "Goal");
            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Send list of active Habitual Goals for the selectedUser.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns>
        /// A list of active Habitual Goals by selected users ID.  If fails returns to
        /// the main goal page.
        /// </returns>
        public ActionResult ViewActiveHabGoals()
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                ViewBag.SelectedUser = selectedUser;
                return View(_habGoalManager.RetrieveHabitualGoalsByActive(selectedUser.UserAccountID, true));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Could not load active Habitual Goal list.");
                return RedirectToAction("Goals", "Goal");
            }
        }

        // -------------------- **** View Attainment Goals(Active and all) **** -------------------- //
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Sends list of attainment goals by selectedUser
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns></returns>
        public ActionResult ViewAttGoal()
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                ViewBag.SelectedUser = selectedUser;
                return View(_attGoalManager.RetrieveAttainmentGoalsByUserIDClient(selectedUser.UserAccountID));
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Could not load a list Attainment Goals.");
                return RedirectToAction("Goals", "Goal");
            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Send list of active Attainment Goals to selectedUser
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns></returns>
        public ActionResult ViewActiveAttGoal()
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                ViewBag.SelectedUser = selectedUser;
                return View(_attGoalManager.RetrieveAttainmentGoalsByActive(selectedUser.UserAccountID, true));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Could not load active Attainment Goal list.");
                return RedirectToAction("Goals", "Goal");
            }
        }

        // -------------------- **** View Extoinction Goals(Active and all) **** -------------------- //
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Send a list of Extinction Goal for selectedUser.
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/28
        /// 
        /// </remarks>
        /// <returns></returns>
        public ActionResult ViewExtGoal()
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                ViewBag.SelectedUser = selectedUser;
                return View(_extGoalManager.RetrieveExtinctionGoalsByUserIDClient(selectedUser.UserAccountID));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Could not load a list Extinction Goals.");
                return RedirectToAction("Goals", "Goal");
            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Send a list of active Extinction Goals for selectedUser.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns></returns>
        public ActionResult ViewActiveExtGoal()
        {
            try
            {
                UserAccount selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                ViewBag.SelectedUser = selectedUser;
                return View(_extGoalManager.RetreiveAllExtGoals(selectedUser.UserAccountID, true));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Could not load active Extinction Goal list.");
                return RedirectToAction("Goals", "Goal");
            }
        }

        

        // -------------------- **** Create Goals **** -------------------- //
        
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Handles view of form to create an Habitual Goal, sending the selected and current user via ViewBag.  The view will pass to AddHabGoal
        /// to validate and store the form data
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns></returns>
        public ActionResult CreateHabGoal()
        {
            int selectedID = AccountController.GetSelectedUserAccount(HttpContext).UserAccountID;

            List<Award> awards = _awardManager.RetreiveAllAwards(true);
            List<string> awardNames = awards.Select(a => a.AwardName).ToList();

            List<Routine> routines = _routineManager.GetRoutinesByuserID(selectedID);
            List<string> routineNames = routines.Select(r => r.Name).ToList();

            ViewBag.AwardNameList = awardNames;
            ViewBag.RoutineNameList = routineNames;
            return View();
        }
        
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Handles view of form to create an Attainment goal, sending the selected and current user via viewbaag.  The view will pass to AddAttGoal
        /// to validate and store the form data.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns></returns>
        public ActionResult CreateAttGoal()
        {
            int selectedID = AccountController.GetSelectedUserAccount(HttpContext).UserAccountID;

            List<Award> awards = _awardManager.RetreiveAllAwards(true);
            List<string> awardNames = awards.Select(a => a.AwardName).ToList();

            List<Performance> performances = _performanceManager.RetrievePerformancesByClient(selectedID);
            List<string> performanceNames = performances.Select(p => p.PerformanceName).ToList();

            ViewBag.AwardNameList = awardNames;
            ViewBag.PerformanceNameList = performanceNames;
            return View();
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Handles view of form to create an Extinction goal, sending the selected and current user via viewbaag.  The view will pass to AddExtGoal
        /// to validate and store the form data.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <returns></returns>
        public ActionResult CreateExtGoal()
        {
            int selectedID = AccountController.GetSelectedUserAccount(HttpContext).UserAccountID;

            List<Award> awards = _awardManager.RetreiveAllAwards(true);
            List<string> awardNames = awards.Select(a => a.AwardName).ToList();

            List<Incident> incidents = _incidentManager.SelectIncidentsByUserID(selectedID);
            List<string> incidentNames = incidents.Select(i => i.IncidentName).ToList();

            ViewBag.AwardNameList = awardNames;
            ViewBag.IncidentNameList = incidentNames;
            return View();
        }

        [HttpPost]
        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Gets form input from CreatHabGoal view, validates and adds an Habitual Goal for the selectedUser to the database
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="habGoal"></param>
        /// <returns></returns>
        public ActionResult AddHabGoal(HabGoalViewModel habGoal)
        {
            try
            {
                if (!habGoal.HabGoalName.IsValidGoalName())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = habGoal.HabGoalName +
                        " Habitual Goal name must be between 1 and 50 characters"
                    });
                }
                if (!habGoal.HabGoalDescription.IsValidGoalDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage = habGoal.HabGoalDescription +
                        " is not a valid description (250 characters max)"
                    });
                }
                if (!habGoal.RoutineFrequency.IsValidNumber())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = habGoal.HabGoalName +
                        " Routine Frequency name must be greater than 0."
                    });
                }

                int clientID = AccountController.GetSelectedUserAccount(HttpContext).UserAccountID;
                int adminID = AccountController.GetAuthenticatedUserAccount(HttpContext).UserAccountID;

                HabGoalViewModel newHabGoal = new HabGoalViewModel()
                {
                    UserID_client = clientID,
                    UserID_admin = adminID,
                    HabGoalName = habGoal.HabGoalName,
                    HabGoalDescription = habGoal.HabGoalDescription,
                    HabGoalTargetDate = habGoal.HabGoalTargetDate,
                    RoutineFrequency = habGoal.RoutineFrequency,
                    AwardName = habGoal.AwardName,
                    RoutineName = habGoal.RoutineName
                };
                _habGoalManager.AddHabitualGoal(newHabGoal);

                ModelState.AddModelError(string.Empty, "Habitual Goal was created");
                return RedirectToAction("CreateHabGoal", "Goal");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Habitual Goal was not created.");
                return View("CreateHabGoal", "Goal");
            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Gets form input from CreatAttGoal view, validates and adds an Attainment Goal for the selectedUser to the database
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="attGoal"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAttGoal(AttGoalViewModel attGoal)
        {
            try
            {
                if (!attGoal.AttGoalName.IsValidGoalName())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = attGoal.AttGoalName +
                        " Attainment Goal name must be between 1 and 50 characters"
                    });
                }
                if (!attGoal.AttGoalDescription.IsValidGoalDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage = attGoal.AttGoalDescription +
                        " is not a valid description (250 characters max)"
                    });
                }
                if (!attGoal.PerformanceFrequency.IsValidNumber())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = attGoal.AttGoalName +
                        " Performance Frequency name must be greater than 0."
                    });
                }

                int clientID = AccountController.GetSelectedUserAccount(HttpContext).UserAccountID;
                int adminID = AccountController.GetAuthenticatedUserAccount(HttpContext).UserAccountID;

                AttGoalViewModel newAttGoal = new AttGoalViewModel()
                {
                    UserID_client = clientID,
                    UserID_admin = adminID,
                    AttGoalName = attGoal.AttGoalName,
                    AttGoalDescription = attGoal.AttGoalDescription,
                    AttGoalTargetDate = attGoal.AttGoalTargetDate,
                    PerformanceFrequency = attGoal.PerformanceFrequency,
                    AwardName = attGoal.AwardName,
                    PerformanceName = attGoal.PerformanceName
                };
                _attGoalManager.AddAttainmentGoal(newAttGoal);

                ModelState.AddModelError(string.Empty, "Attainment Goal was created");
                return RedirectToAction("CreateAttGoal", "Goal");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Attainment Goal was not created.");
                return View("CreateAttGoal", "Goal");
            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Gets form input from CreatExtGoal view, validates and adds an Extinction Goal for the selectedUser to the database
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="extGoal"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddExtGoal(ExtGoalViewModel extGoal)
        {
            try
            {
                if (!extGoal.ExtGoalName.IsValidGoalName())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = extGoal.ExtGoalName +
                        " Extinction Goal name must be between 1 and 50 characters"
                    });
                }
                if (!extGoal.ExtGoalDescription.IsValidGoalDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage = extGoal.ExtGoalDescription +
                        " is not a valid description (250 characters max)"
                    });
                }
                if (!extGoal.IncidentFrequency.IsValidNumberExtGoal())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = extGoal.ExtGoalName +
                        " Incident Frequency name must be 0 or greater."
                    });
                }

                int clientID = AccountController.GetSelectedUserAccount(HttpContext).UserAccountID;
                int adminID = AccountController.GetAuthenticatedUserAccount(HttpContext).UserAccountID;

                ExtGoalViewModel newExtGoal = new ExtGoalViewModel()
                {
                    UserID_client = clientID,
                    UserID_admin = adminID,
                    ExtGoalName = extGoal.ExtGoalName,
                    ExtGoalDescription = extGoal.ExtGoalDescription,
                    ExtGoalTargetDate = extGoal.ExtGoalTargetDate,
                    IncidentFrequency = extGoal.IncidentFrequency,
                    AwardName = extGoal.AwardName,
                    IncidentName = extGoal.IncidentName
                };
                _extGoalManager.AddExtinctionGoal(newExtGoal);

                ModelState.AddModelError(string.Empty, "Extinction Goal was created");
                return RedirectToAction("CreateExtGoal", "Goal");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Extinction Goal was not created.");
                return RedirectToAction("CreateExtGoal", "Goal");
            }
        }


        // -------------------- **** Goal Details **** -------------------- //

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Displays the habitual Goal details of the selectedUser.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="habGoal"></param>
        /// <returns></returns>
        
        public ActionResult GoalHabDetails(int selectedID, string habGoalName, DateTime entryDate)
        {
            var habGoalList = _habGoalManager.RetrieveHabitualGoalsByUserIDClient(selectedID);
            var habGoals = habGoalList.Where(h => h.HabGoalName == habGoalName).ToList();
            if(habGoals.Count > 1)
            {
                var habGoal = habGoals.Where(h => h.HabGoalEntryDate == entryDate);
                return View(habGoal);
            }
            else
            {
                var habGoal = habGoals[0];
                return View(habGoal);
            }
            
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Displays an Attainment Goal details of the selectedUser.
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="attGoal"></param>
        /// <returns></returns>
        
        public ActionResult GoalAttDetails(int selectedID, string attGoalName, DateTime entryDate)
        {
            var attGoalList = _attGoalManager.RetrieveAttainmentGoalsByUserIDClient(selectedID);
            var attGoals = attGoalList.Where(h => h.AttGoalName == attGoalName).ToList();
            if (attGoals.Count > 1)
            {
                var attGoal = attGoals.Where(h => h.AttGoalEntryDate == entryDate);
                return View(attGoal);
            }
            else
            {
                var attGoal = attGoals[0];
                return View(attGoal);
            }
        }

        /// <summary>
        /// Becky Baeniger
        /// Created: 2021/04/28
        /// ///
        /// Displays an Extinction Goal details of selectedUser.
        /// </summary>
        /// <param name="extGoal"></param>
        /// <returns></returns>

        public ActionResult GoalExtDetails(int selectedID, string extGoalName, DateTime entryDate)
        {
            var extGoalList = _extGoalManager.RetrieveExtinctionGoalsByUserIDClient(selectedID);
            var extGoals = extGoalList.Where(h => h.ExtGoalName == extGoalName).ToList();
            if (extGoals.Count > 1)
            {
                var extGoal = extGoals.Where(h => h.ExtGoalEntryDate == entryDate);
                return View(extGoal);
            }
            else
            {
                var extGoal = extGoals[0];
                return View(extGoal);
            }
        }

            // -------------------- **** Edit Goal **** --------------------//

            /// <summary>
            /// Becky Baenziger
            /// Created: 2021/04/28
            /// ///
            /// Edits the selected habitual goal for the selectedUser
            /// </summary>
            /// <remarks>
            /// Updater Name:
            /// Update Date:
            /// 
            /// </remarks>
            /// <param name="oldHabGoal"></param>
            /// <param name="newHabGoal"></param>
            /// <returns></returns>

        public ActionResult EditHabGoal(int selectedID, string habGoalName, DateTime entryDate)
        {
            try
            {
                List<HabGoalViewModel> habGoalList = _habGoalManager.RetrieveHabitualGoalsByUserIDClient(selectedID);
                List<HabGoalViewModel> habGoalss = habGoalList.Where(h => h.HabGoalName == habGoalName).ToList();
                List<HabGoalViewModel> habGoals = habGoalss.Where(h => h.HabGoalEntryDate.ToLongDateString() == entryDate.ToLongDateString()).ToList();
                HabGoalViewModel habGoal = habGoals[0];

                List<Award> awards = _awardManager.RetreiveAllAwards(true);
                List<string> awardNames = awards.Select(a => a.AwardName).ToList();

                string currentAwardName = habGoal.AwardName;
                habGoal.AwardName = null;

                ViewBag.CurrentAwardName = currentAwardName;
                ViewBag.AwardNamesList = awardNames;
                return View(habGoal);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, habGoalName + " was not edited.");
                return RedirectToAction("ViewActiveHabGoals", "Goal");
            }
        }

        [HttpPost]
        public ActionResult UpdateHabGoal(HabGoalViewModel model)
        {
            try
            {
                if (!model.HabGoalName.IsValidGoalName())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = model.HabGoalName +
                        " Habitual Goal name must be between 1 and 50 characters"
                    });
                }
                if (!model.HabGoalDescription.IsValidGoalDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage = model.HabGoalDescription +
                        " is not a valid incident description (250 characters max)"
                    });
                }
                if (!model.RoutineFrequency.IsValidNumber())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = model.HabGoalName +
                        " Routine Frequency name must be greater than 0."
                    });
                }

                List<HabGoalViewModel> habGoalList = _habGoalManager.RetrieveHabitualGoalsByUserIDClient(model.UserID_client);
                List<HabGoalViewModel> habGoalss = habGoalList.Where(h => h.HabGoalName == model.HabGoalName).ToList();
                List<HabGoalViewModel> habGoals = habGoalss.Where(h => h.HabGoalEntryDate.ToLongDateString() == model.HabGoalEntryDate.ToLongDateString()).ToList();
                HabGoalViewModel habGoal = habGoals[0];

                HabGoalViewModel newHabGoal = new HabGoalViewModel()
                {
                    UserID_client = model.UserID_client,
                    UserID_admin = model.UserID_admin,
                    HabGoalName = model.HabGoalName,
                    HabGoalDescription = model.HabGoalDescription,
                    HabGoalTargetDate = model.HabGoalTargetDate,
                    RoutineFrequency = model.RoutineFrequency,
                    AwardName = model.AwardName,
                    RoutineName = model.RoutineName
                };

                _habGoalManager.EditHabitualGoal(habGoal, newHabGoal);

                

                ModelState.AddModelError(string.Empty, model.HabGoalName + " was edited");
                return RedirectToAction("GoalHabDetails", "Goal", new { selectedID = newHabGoal.UserID_client, habGoalName = newHabGoal.HabGoalName, entryDate = newHabGoal.HabGoalEntryDate });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, model.HabGoalName + " was not edited.");
                return RedirectToAction("EditHabGoal", "Goal", new { selectedID = model.UserID_client, habGoalName = model.HabGoalName, entryDate = model.HabGoalEntryDate });
            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Edits the selected Attainment Goal for the selectedUser
        /// </summary>
        /// <remarks>
        /// Updater Name:
        /// Update Date:
        /// 
        /// </remarks>
        /// <param name="oldAttGoal"></param>
        /// <param name="newAttGoal"></param>
        /// <returns></returns>
        
        public ActionResult EditAttGoal(int selectedID, string attGoalName, DateTime entryDate)
        {
            try
            {
                List<AttGoalViewModel> attGoalList = _attGoalManager.RetrieveAttainmentGoalsByUserIDClient(selectedID);
                List<AttGoalViewModel> attGoalss = attGoalList.Where(a => a.AttGoalName == attGoalName).ToList();
                List<AttGoalViewModel> attGoals = attGoalss.Where(a => a.AttGoalEntryDate.ToLongDateString() == entryDate.ToLongDateString()).ToList();
                AttGoalViewModel attGoal = attGoals[0];

                List<Award> awards = _awardManager.RetreiveAllAwards(true);
                List<string> awardNames = awards.Select(a => a.AwardName).ToList();

                string currentAwardName = attGoal.AwardName;
                attGoal.AwardName = null;

                ViewBag.CurrentAwardName = currentAwardName;
                ViewBag.AwardNamesList = awardNames;
                return View(attGoal);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, attGoalName + " was not edited.");
                return RedirectToAction("ViewActiveAttGoals", "Goal");
            }
        }

        [HttpPost]
        public ActionResult UpdateAttGoal(AttGoalViewModel model)
        {
            try
            {
                if (!model.AttGoalName.IsValidGoalName())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = model.AttGoalName +
                        " Habitual Goal name must be between 1 and 50 characters"
                    });
                }
                if (!model.AttGoalDescription.IsValidGoalDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage = model.AttGoalDescription +
                        " is not a valid incident description (250 characters max)"
                    });
                }
                if (!model.PerformanceFrequency.IsValidNumber())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = model.AttGoalName +
                        " Performance Frequency name must be greater than 0."
                    });
                }
                List<AttGoalViewModel> attGoalList = _attGoalManager.RetrieveAttainmentGoalsByUserIDClient(model.UserID_client);
                List<AttGoalViewModel> attGoalss = attGoalList.Where(h => h.AttGoalName == model.AttGoalName).ToList();
                List<AttGoalViewModel> attGoals = attGoalss.Where(h => h.AttGoalEntryDate.ToLongDateString() == model.AttGoalEntryDate.ToLongDateString()).ToList();
                AttGoalViewModel attGoal = attGoals[0];

                AttGoalViewModel newAttGoal = new AttGoalViewModel()
                {
                    UserID_client = model.UserID_client,
                    UserID_admin = model.UserID_admin,
                    AttGoalName = model.AttGoalName,
                    AttGoalDescription = model.AttGoalDescription,
                    AttGoalTargetDate = model.AttGoalTargetDate,
                    PerformanceFrequency = model.PerformanceFrequency,
                    AwardName = model.AwardName,
                    PerformanceName = model.PerformanceName
                };

                _attGoalManager.EditAttainmentGoal(attGoal, newAttGoal);

                ModelState.AddModelError(string.Empty, model.AttGoalName + " was edited");
                return RedirectToAction("GoalAttDetails", "Goal", new { selectedID = newAttGoal.UserID_client, attGoalName = newAttGoal.AttGoalName, entryDate = newAttGoal.AttGoalEntryDate });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, model.AttGoalName + " was not edited.");
                return RedirectToAction("EditAttGoal", "Goal", model);
            }
        }

        /// <summary>
        /// Becky Baenziger
        /// Created: 2021/04/28
        /// ///
        /// Edits the selected Extinction Goal for selectedUser
        /// </summary>
        /// <param name="oldExtGoal"></param>
        /// <param name="newExtGoal"></param>
        /// <returns></returns>
        
        public ActionResult EditExtGoal(int selectedID, string extGoalName, DateTime entryDate)
        {
            try
            {
                List<ExtGoalViewModel> extGoalList = _extGoalManager.RetrieveExtinctionGoalsByUserIDClient(selectedID);
                List<ExtGoalViewModel> extGoalss = extGoalList.Where(h => h.ExtGoalName == extGoalName).ToList();
                List<ExtGoalViewModel> extGoals = extGoalss.Where(h => h.ExtGoalEntryDate.ToLongDateString() == entryDate.ToLongDateString()).ToList();
                ExtGoalViewModel extGoal = extGoals[0];

                List<Award> awards = _awardManager.RetreiveAllAwards(true);
                List<string> awardNames = awards.Select(a => a.AwardName).ToList();

                string currentAwardName = extGoal.AwardName;
                extGoal.AwardName = null;

                ViewBag.CurrentAwardName = currentAwardName;
                ViewBag.AwardNamesList = awardNames;
                return View(extGoal);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, extGoalName + " was not edited.");
                return RedirectToAction("ViewActiveExtGoals", "Goal");
            }
        }

        [HttpPost]
        public ActionResult UpdateExtGoal(ExtGoalViewModel model)
        {
            try
            {
                if (!model.ExtGoalName.IsValidGoalName())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = model.ExtGoalName +
                        " Habitual Goal name must be between 1 and 50 characters"
                    });
                }
                if (!model.ExtGoalDescription.IsValidGoalDescription())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage = model.ExtGoalDescription +
                        " is not a valid incident description (250 characters max)"
                    });
                }
                if (!model.IncidentFrequency.IsValidNumberExtGoal())
                {
                    return RedirectToAction("Error", "Home", new // does home take back to form it came from?
                    {
                        errorMessage = model.ExtGoalName +
                        " Incident Frequency name must be 0 or greater."
                    });
                }
                List<ExtGoalViewModel> extGoalList = _extGoalManager.RetrieveExtinctionGoalsByUserIDClient(model.UserID_client);
                List<ExtGoalViewModel> extGoalss = extGoalList.Where(e => e.ExtGoalName == model.ExtGoalName).ToList();
                List<ExtGoalViewModel> extGoals = extGoalss.Where(e => e.ExtGoalEntryDate.ToLongDateString() == model.ExtGoalEntryDate.ToLongDateString()).ToList();
                ExtGoalViewModel extGoal = extGoals[0];

                ExtGoalViewModel newExtGoal = new ExtGoalViewModel()
                {
                    UserID_client = model.UserID_client,
                    UserID_admin = model.UserID_admin,
                    ExtGoalName = model.ExtGoalName,
                    ExtGoalDescription = model.ExtGoalDescription,
                    ExtGoalTargetDate = model.ExtGoalTargetDate,
                    IncidentFrequency = model.IncidentFrequency,
                    AwardName = model.AwardName,
                    IncidentName = model.IncidentName
                };

                _extGoalManager.EditExtinctionGoal(extGoal, newExtGoal);

                ModelState.AddModelError(string.Empty, model.ExtGoalName + " was edited.");
                return RedirectToAction("GoalExtDetails", "Goal", new { selectedID = newExtGoal.UserID_client, extGoalName = newExtGoal.ExtGoalName, entryDate = extGoal.ExtGoalEntryDate });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, model.ExtGoalName + " was not edited.");
                return RedirectToAction("EditExtGoal", "Goal", model);
            }
        }

        // -------------------- **** Deactivate/Reactivate Habitual Goals **** -------------------- //

        public ActionResult DeReHabGoal(int selectedID, string habGoalName, DateTime entryDate)
        {
            
            try
            {
                var habGoalList = _habGoalManager.RetrieveHabitualGoalsByUserIDClient(selectedID);

                HabGoalViewModel hgvm = null;
                foreach (var item in habGoalList)
                {
                    if(item.HabGoalName == habGoalName && item.HabGoalEntryDate.ToLongDateString() == entryDate.ToLongDateString())
                    {
                        hgvm = item;
                        break;
                    }
                }

                var habGoalss = habGoalList.Where(h => h.HabGoalName == habGoalName);

                HabGoalViewModel newHabGoal = (HabGoalViewModel)hgvm.Clone();
                if(hgvm.Active == true)
                {
                    newHabGoal.Active = false;
                    _habGoalManager.EditHabitualGoal(hgvm, newHabGoal);
                    return RedirectToAction("ViewHabGoals", "Goal");
                }
                else
                {
                    newHabGoal.Active = true;
                    _habGoalManager.EditHabitualGoal(hgvm, newHabGoal);
                    return RedirectToAction("ViewHabGoals", "Goal");
                }

            }
            catch(Exception)
            {
                ModelState.AddModelError(string.Empty, habGoalName + " was not edited.");
                return RedirectToAction("ViewHabGoals", "Goal");
            }
            
        }

        // -------------------- **** Deactivate/Reactivate Attainment Goals **** -------------------- //
        public ActionResult DeReAttGoal(int selectedID, string attGoalName, DateTime entryDate)
        {
            try
            {
                var attGoalList = _attGoalManager.RetrieveAttainmentGoalsByUserIDClient(selectedID);
                AttGoalViewModel attGoals = null;
                foreach (var item in attGoalList)
                {
                    if (item.AttGoalName == attGoalName && item.AttGoalEntryDate.ToLongDateString() == entryDate.ToLongDateString())
                    {
                        attGoals = item;
                        break;
                    }
                }
                
                AttGoalViewModel newAttGoal = (AttGoalViewModel)attGoals.Clone();
                if (attGoals.Active == true)
                {
                    newAttGoal.Active = false;
                    _attGoalManager.EditAttainmentGoal(attGoals, newAttGoal);
                    return RedirectToAction("ViewAttGoal", "Goal");
                }
                else
                {
                    newAttGoal.Active = true;
                    _attGoalManager.EditAttainmentGoal(attGoals, newAttGoal);
                    return RedirectToAction("ViewAttGoal", "Goal");
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, attGoalName + " was not edited.");
                return RedirectToAction("ViewAttGoal", "Goal");
            }
        }


        // -------------------- **** Deactivate/Reactivate Extinction Goals **** -------------------- //
        public ActionResult DeReExtGoal(int selectedID, string extGoalName, DateTime entryDate)
        {
            try
            {
                var extGoalList = _extGoalManager.RetrieveExtinctionGoalsByUserIDClient(selectedID);
                ExtGoalViewModel extGoal = null;
                foreach(var item in extGoalList)
                {
                    if(item.ExtGoalName == extGoalName && item.ExtGoalEntryDate.ToLongDateString()== entryDate.ToLongDateString())
                    {
                        extGoal = item;
                        break;
                    }
                }
                ExtGoalViewModel newExtGoal = (ExtGoalViewModel)extGoal.Clone();
                if (extGoal.Active == true)
                {
                    newExtGoal.Active = false;
                    _extGoalManager.EditExtinctionGoal(extGoal, newExtGoal);
                    return RedirectToAction("ViewExtGoal", "Goal");
                }
                else
                {
                    newExtGoal.Active = true;
                    _extGoalManager.EditExtinctionGoal(extGoal, newExtGoal);
                    return RedirectToAction("ViewExtGoal", "Goal");
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, extGoalName + " was not edited.");
                return RedirectToAction("ViewExtGoal", "Goal");
            }
        }
    }
}