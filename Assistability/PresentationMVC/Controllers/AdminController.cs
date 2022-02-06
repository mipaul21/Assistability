/// <summary>
/// Mitchell Paul
/// Created: 2021/04/20
///
/// contains all the methods to work the website user identities
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LogicLayer;
using DataStorageModels;
using PresentationMVC.Models;

namespace PresentationMVC.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        [Authorize(Roles = "Admin")]


        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/20
        ///
        /// Method to display the list of user identities, allowing you to edit or view details on them.
        /// </summary>

        public ActionResult Index()
        {
            UserAccount _updateUser = new UserAccount();
            UserManager _userManager = new UserManager();


            var _testingUsers = _userManager.RetrieveUserAccounts();

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var usersUpdate = userManager.Users.ToList();

            // List of the users within the database system
            foreach (var items in _testingUsers)
            {
                // List of the users within the Identity System.
                foreach (var item in usersUpdate)
                {
                    // if the identity email matches the client db email, sync them on firstname, and lastname.
                    if (items.Email == item.Email)
                    {
                        try
                        {
                            _updateUser = _userManager.SelectUserAccountByEmail(item.Email);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        if (_updateUser.FirstName != item.FirstName || _updateUser.LastName != item.LastName)
                        {

                            item.FirstName = _updateUser.FirstName;
                            item.LastName = _updateUser.LastName;
                            userManager.Update(item);
                        }
                    }
                }
            }
            // Display the Entity Users.

            var users = userManager.Users.OrderBy(n => n.FirstName);
            return View(users);
        }



        

    

    // GET: Admin/Details/5
    /// <summary>
    /// Mitchell Paul
    /// Created: 2021/04/20
    ///
    /// Method to display details of the selected user.
    /// </summary>
    public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }


            return View(user);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(ApplicationUser model)
        {
            try
            { 


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/20
        ///
        /// Method to populate a form with the selected user identities information.
        /// </summary>
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            return View(user);
        }

        // POST: Admin/Edit/5
        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/20
        ///
        /// Method to deal with the submitted form data, updating the selected user identity.
        /// Also updates the corresponding user in the client database according to their matching email.
        /// 
        /// </summary>
        [HttpPost]
        public ActionResult Edit(ApplicationUser model)
        {



            UserAccount _updateUser = new UserAccount();
            UserManager _userManager = new UserManager();

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var usersUpdate = userManager.Users.ToList();
            var _testingUsers = _userManager.RetrieveUserAccounts();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = userManager.FindById(model.Id);
                    ApplicationUser newUser = new ApplicationUser();
                    user.Id = model.Id;
                    user.FirstName = model.FirstName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;








                    UserAccount _oldUser = _userManager.SelectUserAccountByEmail(user.Email);
                    UserAccount _newUser = new UserAccount();
                    _newUser.Email = model.Email;
                    _newUser.FirstName = model.FirstName;
                    _newUser.LastName = model.LastName;
                    _newUser.UserName = _oldUser.UserName;
                    _newUser.UserAccountID = _oldUser.UserAccountID;
                    _newUser.Active = true;


                    _userManager.UpdateUserAccount(_oldUser, _newUser);






                    userManager.Update(user);
                    context.SaveChanges();






                    // TODO: Add update logic here


                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
