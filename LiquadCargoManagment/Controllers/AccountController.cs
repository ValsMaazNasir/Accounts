using EntityFramework.Filters;
using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using static LiquadCargoManagment.Helpers.ApplicationHelper;

namespace LiquadCargoManagment.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //private readonly LCMEntities context;
        //AdminController ctr;
        //SecurityTokenIdentifier _security;
        //public AccountController()
        //{
        //    context = new LCMEntities();
        //    ctr = new AdminController();
        //    _security = new SecurityTokenIdentifier();

        //}

        #region Login
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserAccount model)
        {
            string Validation = "success";
            if (model.UserName == null)
                Validation = "Enter valid username";
            else if (model.UserPassword == null)
                Validation = "Enter Valid Password";
            else
            {
                using (var context = new LCMEntities())
                {
                    UserAccount isAuthenticate = await context.UserAccounts.Where(x => x.UserName.Equals(model.
                        UserName, StringComparison.Ordinal) && x.UserPassword.Equals(model.UserPassword) && x.Active == true).FirstOrDefaultAsync();
                    if (isAuthenticate != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        string path = UserProfileImagePath + isAuthenticate.Image;
                        AddCookie("ProfileImage", path.Replace("~", ""));
                        AddCookie("Username", isAuthenticate.FullName);
                        AddCookie("UserID", isAuthenticate.UserID.ToString());
                        AddCookie("OwnCompanyID", isAuthenticate.OwnCompanyId.ToString());
                        AddCookie("SubcriptionID", context.OwnCompanies.Where(x => x.ID == isAuthenticate.OwnCompanyId)
                            .FirstOrDefault().SubcriptionID.ToString());
                        AddCookie("RoleID", isAuthenticate.RoleID.ToString());                               
                        GetOwnCompanies();
                        using(LiquadCargoManagment.Areas.Accounts.Models.AccountingEntities db
                            = new Areas.Accounts.Models.AccountingEntities())
                        {
                            try
                            {
                                model.UserPassword = Encrypt(model.UserPassword);
                                var record = db.Users.FirstOrDefault(x => x.EmailAddress.Equals(model.UserName) && x.Password.Equals(model.UserPassword));
                                if(record == null)
                                {
                                    record = db.Users.Create();
                                    record.Name = isAuthenticate.FullName;
                                    record.EmailAddress = isAuthenticate.UserName;
                                    record.Password = Encrypt(isAuthenticate.UserPassword);
                                    record.CreatedBy = 1;
                                    record.CreatedDateTime = GetDateTime();
                                    record.RoleID = 1;
                                    record.Status = "Enable";
                                    db.Users.Add(record);
                                    db.SaveChanges();
                                }                                
                                AddSession(Session_User_Login, record);

                            }
                            catch (Exception ex)
                            {

                                Validation = ex.Message;
                            }
                            
                        }
                        //AddCookie("BiltyRole", context.BiltyRoles.Where(x => x.UserId == isAuthenticate.UserID).ToList());
                        
                    }
                    else
                    {
                        Validation = "The providing credential is not valid Or account may be deactivated";
                    }
                }
            }
            return Json(new { error = Validation }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();          
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
         
            return RedirectToAction("Login");
        }

        public ActionResult LogoutWithoutSessionExpire()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        #region Registration
        public ActionResult Registration()
        {
            var OwnConanies = context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();
            ViewBag.OwnCompanies = OwnConanies;
            ViewBag.OwnCompany = new SelectList(OwnConanies, "ID", "Name");
            ViewBag.Roles = new SelectList(context.Roles.Where(x => x.isDeleted == false || x.isDeleted == null).ToList(), "RoleID", "RoleName");
            ViewBag.BiltyRole = new List<string>() { Helpers.EnumBiltyRole.User1, Helpers.EnumBiltyRole.User2, Helpers.EnumBiltyRole.User3 };
            return View(new UserAccount());
        }
        public ActionResult addRegistration(UserAccount model,string[] BiltyRole, HttpPostedFileBase file, List<UserAssignedCompany> Users)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                long UserID = 0;
                if (model.OwnCompanyId > 0)
                {
                    if (model.UserName != null && model.UserPassword != null)
                    {
                        var Duplicated = context.UserAccounts.Where(x => x.UserName == model.UserName).ToList();
                        if (model.UserID > 0)
                        {
                            if (!(Duplicated.Count > 1))
                            {
                                var local = context.Set<UserAccount>().Local.FirstOrDefault(x => x.UserID == model.UserID);
                                if (local != null)
                                {
                                    context.Entry(local).State = EntityState.Detached;
                                }
                                model.Image = file == null ? "" : file.FileName;
                                model.ModifiedDate = DateTime.Now;
                                model.ModifiedBy = ApplicationHelper.UserID;
                                model.Active = true;
                                UserID = model.UserID;
                                context.Entry(model).State = EntityState.Modified;
                                Message = "User Profile Updated";

                            }
                            else
                            {
                                Message = "The username is already taken try onther one";
                                Status = "Duplicate";
                            }
                        }
                        else
                        {
                            if (!(Duplicated.Count > 0))
                            {
                                model.Image = file == null ? "" : file.FileName;
                                model.Active = true;
                                model.CreatedDate = DateTime.Now;
                                model.CreatedBy = ApplicationHelper.UserID;
                                context.UserAccounts.Add(model);
                                Message = "User Registerd successfully";
                                if (Users != null)
                                {
                                    foreach (var item in Users)
                                    {                                    
                                        item.UserID = UserID;
                                        item.IsActive = true;
                                        context.UserAssignedCompanies.Add(item);
                                    }
                                }
                                else
                                {
                                    Message = "Error occured";
                                    Status = "Empty";
                                }
                            }
                            else
                            {
                                Message = "The username is already taken try onther one";
                                Status = "Duplicate";
                            }
                        }
                        if (file != null)
                        {
                            UploadFile(file, UserProfileImagePath, Server);
                        }

                        foreach (var Role in BiltyRole)
                        {
                            BiltyRole role = new BiltyRole();
                            role.UserId = model.UserID;
                            role.BiltyRole1 = Role;
                            context.BiltyRoles.Add(role);
                        }
                       
                        context.SaveChanges();
                    }
                    else
                    {
                        Message = "Username and Password is Required";
                        Status = "Required";
                    }
                }
                else
                {
                    Message = "In which company you want to register this user? please select a company";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            return Json(new { Status = Status, Message = Message }, JsonRequestBehavior.AllowGet);


        }

        public async Task<ActionResult> UsersList(string auth)
        {

            return View(await context.UserAccounts.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToListAsync());

        }

        public async Task<ActionResult> EditUserList(long id)
        {
            return View("Registration", await context.UserAccounts.Where(x => x.UserID == id).FirstOrDefaultAsync());
        }
        public JsonResult DeleteRegistration(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.UserAccounts.Where(model => model.UserID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        context.SaveChanges();
                        Message = "User Deleted Successfully";

                    }
                    else
                    {
                        Message = "Application cannot find this record in database please refresh the browser";
                        Status = "Exception";
                    }
                }
                else
                {
                    Message = "Something Went Wrong";
                    Status = "Exception";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            string View = RenderPartialToString("~/Views/Shared/_Registration.cshtml", context.UserAccounts.OrderByDescending(x => x.UserID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public RedirectToRouteResult ExpireToken()
        {
            LogoutWithoutSessionExpire();
            TempData["unauth"] = "you are trying to change authentication token please login again for security purpose";
            return RedirectToAction("Login", "Account");
        }


        public void GetOwnCompanies()
        {
            long SubcriptionID = Convert.ToInt64(GetCookie("SubcriptionID"));
            ApplicationHelper.AddWithTimeout(Session,"OwnCompanies", context.OwnCompanies
                           .Where(x => x.SubcriptionID == SubcriptionID).ToList(),TimeSpan.FromMinutes(100));
        }

     
    }
  

}