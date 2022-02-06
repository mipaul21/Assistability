/// <summary>
///     Whitney Vinson
///     Created: 2021/04/29
///     The controller for the
///     Journal action methods.
/// </summary>
/// <remarks>
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataStorageModels;
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationMVC.Controllers
{
    public class JournalController : Controller
    {
        private IUserManager _userManager = new UserManager();
        private IJournalManager _journalManager = new JournalManager();

        /// <summary>
        ///     Matt Cinkan
        ///     Created: 2021/04/14
        ///     The user's list of Journals.
        /// </summary>
        /// <exception>
        ///     Journals for UserID not found.
        /// </exception>
        /// <returns>
        ///     The Journals associated with the UserID.
        /// </returns>
        /// <remarks>
        ///     Whitney Vinson
        ///     Updated: 2021/05/01
        ///     Added the logged in user.
        /// </remarks>

        public ActionResult Journals()
        {
            UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            return View(_journalManager.SelectAllJournalsByUserID(_selectedUser.UserAccountID));
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The user's list of JournalsEntries.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="userIDClient">The UserID of the Client.</param>
        /// <param name="journalName">The JournalName</param>
        /// <exception>
        ///     Journal entries for this Journal not found.
        /// </exception>
        /// <returns>
        ///     The JournalsEntries associated with the Journal.
        /// </returns>

        public ActionResult JournalEntries(int userIDClient, string journalName)
        {
            UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            var journalList = _journalManager.SelectAllJournalsByUserID(_selectedUser.UserAccountID);
            var oneJournal = journalList.Where(j => j.JournalName == journalName).ToList();
            ViewBag.Journal = oneJournal[0];
            return View(_journalManager.RetrieveJournalEntreisFromJournal(_selectedUser.UserAccountID, journalName));
        }

        /// <summary>
        ///     Matt Cinkan
        ///     Created: 2021/04/14
        ///     The form to create a Journal.
        /// </summary>
        /// <exception>
        ///     Journal not created.
        /// </exception>
        /// <returns>
        ///     View for the Create Form.
        /// </returns>
        /// <remarks>
        ///     Whitney Vinson
        ///     Updated: 2021/05/01
        ///     Added the logged in user,
        ///     and try/catch block for errors.
        /// </remarks>

        // GET: Create Journal
        public ActionResult Create()
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                return View();
            }
            catch (Exception e)
            {
                string error = e.Message;
                return RedirectToAction("Error", "Journals", new { errorMessage = error });
            
            }
        }

        /// <summary>
        ///     Matt Cinkan
        ///     Created: 2021/04/14
        ///     The method to create a Journal.
        /// </summary>
        /// <param name="journalName">The JournalName</param>
        /// <param name="journalDescription">The JournalDescription</param>
        /// <exception>
        ///     Journal not created.
        /// </exception>
        /// <returns>
        ///     Rows added.
        /// </returns>
        /// <remarks>
        ///     Whitney Vinson
        ///     Updated: 2021/05/01
        ///     Added the logged in user.
        ///     Took UserID out of parameters, only needing to
        ///     pass journalName and description to find Journal.
        /// </remarks>

        // POST: Create Journal
        [HttpPost]
        public ActionResult CreateJournal(string journalName, string journalDescription)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                _journalManager.CreateAJournalWithUserID(_selectedUser.UserAccountID, journalName, journalDescription);
                return RedirectToAction("Journals", "Journal");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Journals", new { errorMessage = error });
            }
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The form to update a Journal.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="journalName">The Journal's Name</param>
        /// <param name="userIDClient">The UserID of the editor.</param>
        /// <exception>
        ///     Journal not updated.
        /// </exception>
        /// <returns>
        ///     View for editing Journal.
        /// </returns>

        // GET: Update Journal
        public ActionResult Update(string journalName)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                var journalList = _journalManager.SelectAllJournalsByUserID(_selectedUser.UserAccountID);
                var journal = journalList.Where(r => r.JournalName == journalName).ToList();
                Journal oldJournal = journal[0];
                return View(oldJournal);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Journal", new { errorMessage = error });
            }
            
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The method to update a Journal.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="newJournal">The updated Journal</param>
        /// <exception>
        ///     Journal not updated.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>

        // POST: Update Journal
        [HttpPost]
        public ActionResult UpdateJournal(Journal newJournal)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                var journalList = _journalManager.SelectAllJournalsByUserID(_selectedUser.UserAccountID);
                var journal = journalList.Where(r => r.JournalName == newJournal.JournalName).ToList();
                Journal oldJournal = journal[0];
                newJournal.UserID_Client = _selectedUser.UserAccountID;
                oldJournal.UserID_Client = _selectedUser.UserAccountID;
                _journalManager.UpdateJournal(newJournal, oldJournal);

                return RedirectToAction("Journals");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Journal", new { errorMessage = error });
            }
        }

        /// <summary>
        ///     Matt Cinkan
        ///     Created: 2021/04/14
        ///     The form to delete a Journal.
        /// </summary>
        /// <param name="journalName">The Journal name.</param>
        /// <exception>
        ///     JournalEntry not updated.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>
        /// <remarks>
        ///     Whitney Vinson
        ///     Updated: 2021/05/01
        ///     Added the logged in user.
        /// </remarks>

        // GET - Delete Journal
        public ActionResult Delete(string journalName)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                return View(_journalManager.SelectJournalByUserIDAndJournalName(_selectedUser.UserAccountID, journalName));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Journal", new { errorMessage = error });
            }
        }

        /// <summary>
        ///     Matt Cinkan
        ///     Created: 2021/04/14
        ///     The method to delete a Journal.
        /// </summary>
        /// <param name="delete">The User's choice of deletion.</param>
        /// <param name="journalName">The Journal Name.</param>
        /// <exception>
        ///     Journal not found.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>
        /// <remarks>
        ///     Whitney Vinson 
        ///     Updated: 2021/05/01
        ///     Added deleting all JournalEntries
        ///     associated with the Journal.
        ///     Added the logged in user.
        ///     Fixed index issue, routes back to Journals.
        /// </remarks>

        // POST - Delete Journal
        [HttpPost]
        public ActionResult DeleteJournal(bool delete, string journalName)
        {
            if (delete)
            {
                try
                {
                    UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                    var journalEntries = _journalManager.RetrieveJournalEntreisFromJournal(_selectedUser.UserAccountID, journalName);
                    foreach (var entry in journalEntries)
                    {
                        try
                        {
                            _journalManager.RemoveJournalEntry(entry.UserIDClient, entry.JournalID,
                                entry.JournalEntryBody, entry.JournalEntryDate);
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            return RedirectToAction("Error", "Journal", new { errorMessage = error });
                        }
                    }
                    _journalManager.DeleteJournalByUserIDAndJournalName(_selectedUser.UserAccountID, journalName);
                    return RedirectToAction("Journals", "Journal");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return RedirectToAction("Error", "Journal", new { errorMessage = error });
                }
            }
            else
            {
                return RedirectToAction("Journals", "Journal");
            }
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The form to create a JournalEntry.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <exception>
        ///     JournalEntry not created.
        /// </exception>
        /// <returns>
        ///     Rows added.
        /// </returns>

        // GET - Create Journal Entry
        public ActionResult CreateEntry(string journalID)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                var journalList = _journalManager.SelectAllJournalsByUserID(_selectedUser.UserAccountID);
                var oneJournal = journalList.Where(j => j.JournalName == journalID).ToList();
                ViewBag.Journal = oneJournal[0];
                ViewBag.User = _selectedUser;
                return View();
            }
            catch (Exception e)
            {
                string error = e.Message;
                return RedirectToAction("Error", "JournalEntries", new { errorMessage = error });
            }
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The form to create a JournalEntry.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="journalEntry">A JournalEntry</param>
        /// <exception>
        ///     JournalEntry not created.
        /// </exception>
        /// <returns>
        ///     Rows added.
        /// </returns>

        // POST - Create Journal Entry
        public ActionResult CreateJournalEntry(JournalEntry journalEntry)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                journalEntry.UserIDClient = _selectedUser.UserAccountID;
                journalEntry.UserIDClientJournal = _selectedUser.UserAccountID;
                _journalManager.AddJournalEntry(journalEntry);
                return RedirectToAction("JournalEntries", "Journal", new { userIDClient = journalEntry.UserIDClient,
                    journalName = journalEntry.JournalID });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Journal", new { errorMessage = error });
            }
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The form to update a JournalEntry.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="oldEntry">The old JournalEntry</param>
        /// <param name="newEntryBody">The updated JournalEntryBody</param>
        /// <exception>
        ///     JournalEntry not updated.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>

        // GET - Update Journal Entry
        public ActionResult UpdateEntry(string journalName, DateTime dateCreated)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                var journalEntryList = _journalManager.RetrieveJournalEntreisFromJournal(_selectedUser.UserAccountID, journalName);
                var journalEntry = journalEntryList.Where(r => r.JournalEntryDate.ToLongDateString()
                                == dateCreated.ToLongDateString()).ToList();
                JournalEntry oldEntry = journalEntry[0];
                return View(oldEntry);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Journal", new { errorMessage = error });
            }
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The method to update a JournalEntry.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="oldEntry">The old JournalEntry</param>
        /// <param name="newEntryBody">The updated JournalEntryBody</param>
        /// <exception>
        ///     JournalEntry not updated.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>

        [HttpPost]
        public ActionResult UpdateJournalEntry(JournalEntry oldEntry)
        {
            try
            {
                UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                var entryList = _journalManager.RetrieveJournalEntreisFromJournal(
                                _selectedUser.UserAccountID, oldEntry.JournalID);
                var entry = entryList.Where(r => r.JournalEntryDate.ToLongDateString()
                                == oldEntry.JournalEntryDate.ToLongDateString()).ToList();
                JournalEntry anEntry = entry[0];
                _journalManager.EditJournalEntry(anEntry, oldEntry.JournalEntryBody);
                return RedirectToAction("JournalEntries", "Journal", new
                {
                    userIDClient = anEntry.UserIDClient,
                    journalName = anEntry.JournalID
                });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Journal", new { errorMessage = error });
            }
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The details of a JournalEntry.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="journalName">The JournalName.</param>
        /// <param name="body">The JournalEntry Body.</param>
        /// <param name="dateCreated">The JournalEntryDate.</param>
        /// <exception>
        ///     JournalEntry not updated.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>

        public ActionResult EntryDetails(string journalName, string body, DateTime dateCreated)
        {
            UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            var journalEntryList = _journalManager.RetrieveJournalEntreisFromJournal(_selectedUser.UserAccountID, journalName);
            var journalEntry = journalEntryList.Where(r => r.JournalEntryDate.ToLongDateString()
                            == dateCreated.ToLongDateString() && r.JournalEntryBody == body).ToList();
            JournalEntry oldEntry = journalEntry[0];
            return View(oldEntry);
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The form to delete a JournalEntry.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="journalName">The name of the JournalEntry.</param>
        /// <param name="body">The JournalEntry body.</param>
        /// <param name="dateCreated">The JournalEntryDate.</param>
        /// <exception>
        ///     JournalEntry not updated.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>

        public ActionResult DeleteEntry(string journalName, string body, DateTime dateCreated)
        {
            UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
            var journalEntryList = _journalManager.RetrieveJournalEntreisFromJournal(_selectedUser.UserAccountID, journalName);
            var journalEntry = journalEntryList.Where(r => r.JournalEntryDate.ToLongDateString()
                            == dateCreated.ToLongDateString() && r.JournalEntryBody == body).ToList();
            JournalEntry oldEntry = journalEntry[0];
            return View(oldEntry);
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The method to delete a JournalEntry.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="delete">The bool for the JournalEntry.</param>
        /// <param name="journalName">The Journal Name.</param>
        /// <param name="body">The JournalEntry body.</param>
        /// <param name="dateCreated">The JournalEntryDate.</param>
        /// <exception>
        ///     JournalEntry not updated.
        /// </exception>
        /// <returns>
        ///     Rows affected.
        /// </returns>
        [HttpPost]
        public ActionResult DeleteJournalEntry(bool delete, string journalName,
             string body, DateTime dateCreated)
        {
            if (delete)
            {
                try
                {
                    UserAccount _selectedUser = AccountController.GetSelectedUserAccount(HttpContext);
                    var journalEntryList = _journalManager.RetrieveJournalEntreisFromJournal(_selectedUser.UserAccountID, journalName);
                    var journalEntry = journalEntryList.Where(r => r.JournalEntryDate.ToLongDateString()
                                    == dateCreated.ToLongDateString() && r.JournalEntryBody == body).ToList();
                    JournalEntry oldEntry = journalEntry[0];
                    _journalManager.RemoveJournalEntry(_selectedUser.UserAccountID, 
                        oldEntry.JournalID, oldEntry.JournalEntryBody, oldEntry.JournalEntryDate);
                    return RedirectToAction("JournalEntries", "Journal"
                        , new { userIDClient = _selectedUser.UserAccountID, journalName = journalName});
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return RedirectToAction("Error", "Journal", new { errorMessage = error });
                }
            }
            else
            {
                return RedirectToAction("EntryDetails", "Journal", new { journalName, dateCreated});
            }
        }

        /// <summary>
        ///     Whitney Vinson
        ///     Created: 2021/04/29
        ///     The method for error messages.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>
        ///     Error Message
        /// </returns>

        public ActionResult Error(string errorMsg)
        {
            ViewBag.ErrorMessage = errorMsg;
            return View();
        }
    }
}