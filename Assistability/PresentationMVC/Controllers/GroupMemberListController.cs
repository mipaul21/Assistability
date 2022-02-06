using DataStorageModels;
using DataViewModels;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PresentationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationMVC.Controllers
{
    public class GroupMemberListController : Controller
    {
        /// <summary>
        /// William Clark
        /// Created: 2021/04/25
        ///
        /// Action method for returning the GroupMemberList view. 
        /// </summary>
        /// 
        ///<returns>The GroupMemberList view with two lists composed of the UserAccounts which belong to the same groups as the currently authenticated user, and which are assigned roles that the UserAccount does not have.</returns>
        [HttpGet]
        public ActionResult GroupMemberList()
        {
            IUserGroupManager oldUserGroupManager = new UserGroupManager();
            IUserManager oldUserManager = new UserManager();
            try
            {
                var newUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var roles = newUserManager.GetRolesAsync(User.Identity.GetUserId()).Result;
                UserAccountVM oldUserVM = AccountController.GetAuthenticatedUserAccountVM(HttpContext);

                if (roles.Contains("Client"))
                {
                    List<UserAccount> firstList = new List<UserAccount>();
                    List<UserAccount> secondList = new List<UserAccount>();
                    foreach (var membership in oldUserVM.Memberships)
                    {
                        firstList.AddRange(oldUserGroupManager.GetUserAccountsInUserGroupByRole(oldUserManager, oldUserGroupManager.GetUserGroupByGroupID(membership.GroupID), "Admin"));
                        secondList.AddRange(oldUserGroupManager.GetUserAccountsInUserGroupByRole(oldUserManager, oldUserGroupManager.GetUserGroupByGroupID(membership.GroupID), "Caregiver"));
                    }
                    return PartialView(new GroupMemberListViewModel("Admin", firstList, "Caregiver", secondList));
                }
                else if (roles.Contains("Caregiver"))
                {
                    List<UserAccount> firstList = new List<UserAccount>();
                    List<UserAccount> secondList = new List<UserAccount>();
                    foreach (var membership in oldUserVM.Memberships)
                    {

                        firstList.AddRange(oldUserGroupManager.GetUserAccountsInUserGroupByRole(oldUserManager, oldUserGroupManager.GetUserGroupByGroupID(membership.GroupID), "Client"));
                        secondList.AddRange(oldUserGroupManager.GetUserAccountsInUserGroupByRole(oldUserManager, oldUserGroupManager.GetUserGroupByGroupID(membership.GroupID), "Admin"));
                    }
                    return PartialView(new GroupMemberListViewModel("Client", firstList, "Admin", secondList));
                }
                else // User is an Admin
                {
                    List<UserAccount> firstList = new List<UserAccount>();
                    List<UserAccount> secondList = new List<UserAccount>();
                    foreach (var membership in oldUserVM.Memberships)
                    {

                        firstList.AddRange(oldUserGroupManager.GetUserAccountsInUserGroupByRole(oldUserManager, oldUserGroupManager.GetUserGroupByGroupID(membership.GroupID), "Client"));
                        secondList.AddRange(oldUserGroupManager.GetUserAccountsInUserGroupByRole(oldUserManager, oldUserGroupManager.GetUserGroupByGroupID(membership.GroupID), "Caregiver"));
                    }
                    return PartialView(new GroupMemberListViewModel("Client", firstList, "Caregiver", secondList));
                }
            }
            catch
            {
                return PartialView(new GroupMemberListViewModel());
            }
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/25
        ///
        /// Controller method to invoke from views to alter selected user 
        /// </summary>
        
        public void ResolveSelectedGroupMember(string id)
        {
            IUserManager oldUserManager = new UserManager();
            UserAccount userAccount;
            try
            {
                userAccount = oldUserManager.GetUserAccountByUserAccountID(int.Parse(id));
            }
            catch (Exception)
            {
                userAccount = AccountController.GetAuthenticatedUserAccount(HttpContext);
            }
            if (Session["SelectedUser"] == null)
            {
                Session.Add("SelectedUser", userAccount.UserAccountID.ToString());
            }
            else
            {
                Session["SelectedUser"] = userAccount.UserAccountID.ToString();
            }
            if (!Request.UrlReferrer.OriginalString.Contains("Dashboard"))
            {
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                Response.Redirect(Request.UrlReferrer.AbsoluteUri.Trim("Dashboard".ToCharArray()));
            }
        }
    }
}