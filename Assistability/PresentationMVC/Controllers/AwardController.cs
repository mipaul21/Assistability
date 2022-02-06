/// <summary>
/// Jory A. Wernette
/// Created: 2021/04/14
///
/// contains all the methods to work the website awards
/// </summary>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/21
/// Added create, delete award functions
/// </remarks>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/28
/// Added create, delete award functions
/// and function to let admin see deactivated awards
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataStorageModels;

namespace PresentationMVC.Controllers
{
    public class AwardController : Controller
    {
        private AwardManager _awardManager = new AwardManager();
        private UserAccount _selectedUser;
        private UserAccount _currentUser;
        private UserManager _userManager = new UserManager();

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/14
        ///
        /// a method for desplaying a client's awards
        /// I used the same methods as are in the WPF,
        /// retrieving all awards for the view awards page,
        /// using awardID and userID to edit an award
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active awards for a client
        ///or to an error screen if failed</returns>
        // GET: Award List
        public ActionResult Awards()
        {
            try
            {
                ViewBag.User = _selectedUser;
                ViewBag.Active = true;

                List<Award> awardList = new List<Award>();

                awardList = _awardManager.RetreiveAllAwards();

                if (User.IsInRole("Admin"))
                {
                    awardList = _awardManager.RetreiveEveryAward();
                }

                return View(awardList);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/21
        ///
        /// a method for editting an award
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active awards for a client
        ///or to an error screen if failed</returns>
        public ActionResult Edit(string awardName)
        {
            if (TempData.ContainsKey("Error"))
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            try
            {
                Award awardDetails = new Award();
                awardDetails = _awardManager.RetreiveAwardByAwardName(awardName);

                return View(awardDetails);
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });

            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/23
        ///
        /// a method for updating an award
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active awards for a client
        ///or to an error screen if failed</returns>
        ///
        /// <remarks>
        /// Jory A. Wernette
        /// Updated: 2021/04/29
        /// Changed to check for deactivate/reactivate on save
        /// </remarks>
        [HttpPost]
        public ActionResult Edit(string awardName, string awardDescription, bool active)
        {
            
            try
            {
                int rowsAffected;
                Award oldAward = new Award();
                Award newAward = new Award();

                oldAward = _awardManager.RetreiveAwardByAwardName(awardName);

                newAward.AwardName = awardName;
                newAward.AwardDescription = awardDescription;
                newAward.Active = active;

                if (active == true && oldAward.Active == false)
                {
                    rowsAffected = _awardManager.ReactivateAwardByAwardName(awardName);
                }
                if (active == false && oldAward.Active == true)
                {
                    rowsAffected = _awardManager.DeactivateAwardByAwardName(awardName);
                }

                rowsAffected = _awardManager.UpdateAward(newAward, oldAward);

                return Redirect("/Award/Awards");
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/28
        ///
        /// This is the method to get to the page for creating an award.
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a page for filling out to create an award
        ///or to an error screen if failed</returns>
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/28
        ///
        /// This is the Post method for a method creating an award. Will return the user to the View all awards page
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active awards for a client
        ///or to an error screen if failed</returns>
        [HttpPost]
        public ActionResult Create(string awardName, string awardDescription)
        {
            try
            {
                int rowsAffected;
                rowsAffected = _awardManager.CreateAward(awardName, awardDescription);


                return Redirect("/Award/Awards");
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/28
        ///
        /// a method for deleting an award. Will return the user to the View all awards page
        /// </summary>
        ///<exception>if something goes wrong</exception>
        ///<returns>a rederect to a list of active awards for a client
        ///or to an error screen if failed</returns>
        public ActionResult Delete(string awardName)
        {
            try
            {
                int rowsAffected;
                rowsAffected = _awardManager.DeleteAward(awardName);


                return Redirect("/Award/Awards");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "The Award couldn't be deleted because it is in use by a goal.";
                return RedirectToAction("Edit", "Award", new { awardName = awardName});
            }
        }
    }
}