using DataStorageModels;
using DataViewModels;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PresentationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationMVC.Controllers
{
    public class HomeController : Controller
    {
        /// <remarks>
        /// Jory A. Wernette
        /// Updated: 2021/04/07
        /// Updated to redirect the user to the login page if user is not signed in
        /// </remarks>
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/09
        ///
        /// a method for showing errors
        /// </summary>
        /// 
        ///<param name="errorMessage">The error message to be desplayed</param>
        ///<returns>a view to display an error</returns>
        public ActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }

        public ActionResult Dashboard()
        {
            Session["SelectedUser"] = null;

            return View("Index");
        }
    }
}