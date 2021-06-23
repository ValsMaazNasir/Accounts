using LiquadCargoManagment.Helpers;
using static LiquadCargoManagment.Areas.Accounts.Helpers.ApplicationHelper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LiquadCargoManagment.Areas.Accounts.Models;
using System;
using System.Web.Security;


namespace LiquadCargoManagment.Areas.Accounts.Controllers
{
    public class BaseController : Controller
    {
        protected AccountingEntities Database { get; set; }
        protected readonly SecurityTokenIdentifier _security;
        protected readonly LiquadCargoManagment.Models.LCMEntities context;

        public BaseController()
        {
            Database = new AccountingEntities();
            context = new LiquadCargoManagment.Models.LCMEntities();
            _security = new SecurityTokenIdentifier(context);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User CurrentUserRecord = GetUserData();
            var TodayDate = GetDateTime();
            //if(CurrentUserRecord !=null)
            //{
            //    ViewBag.Notification = Database.Leads.Where(x => x.IsDeleted.Equals(false) && x.UserID.Equals(CurrentUserRecord.ID) && System.Data.Entity.DbFunctions.TruncateTime(x.ReminderDate) == System.Data.Entity.DbFunctions.TruncateTime(TodayDate)).ToList();
            //}
            ViewBag.WebsiteURL = GetSettingContentByName("Website URL");
            ViewBag.jQuery_Date_Time_Format = jQuery_Date_Time_Format;
            ViewBag.jQuery_Date_Format = jQuery_Date_Format;
            ViewBag.Website_Date_Time_Format = Website_Date_Time_Format;
            ViewBag.Website_Date_Format = Website_Date_Format;
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string[] requestURL = filterContext.HttpContext.Request.Path.ToString().Split('/');
                string controllerURL = requestURL[2].ToLower();
                if (!IsUserLogin())
                {                   
                    filterContext.Result = new RedirectResult("/");
                }
                else
                {
                    ViewBag.ControllerName = UpperCaseWords(controllerURL);
                    ViewBag.ControllerURL = controllerURL;
                    string menuURL = controllerURL;
                    string actionURL = string.Empty;
                    if (requestURL.Length > 3)
                    {
                        actionURL = requestURL[3].ToLower();
                        if (actionURL != "importexcel" && actionURL != "exportempty" && actionURL != "export" && actionURL != "add" && actionURL != "edit" && actionURL != "views" && actionURL != "sorting")
                        {
                            menuURL += "/" + actionURL;
                        }
                    }
                    User UserRecord = GetUserData();
                    User userCurrentRecord = Database.Users.FirstOrDefault(x => x.ID == UserRecord.ID);
                    ViewBag.ProfileImage = userCurrentRecord.ProfileImage == null ? "1.png" : userCurrentRecord.ProfileImage;
                    var UserRolePermissionRecords = Database.RolePermissions.Where(o => o.RoleID == UserRecord.RoleID).OrderBy(o => o.SequenceOrder).ToList();

                    if (!AllowedUrlList().Contains(menuURL))
                    {
                        filterContext.Result = new RedirectResult("/dashboard/accessunauthorized");
                        if (UserRolePermissionRecords.Count > 0)
                        {
                            var UserRolePermissionRecord = UserRolePermissionRecords.FirstOrDefault(o => o.Menu.AccessURL.ToLower().Equals(menuURL));
                            if (UserRolePermissionRecord != null)
                            {
                                if (UserRolePermissionRecord.Permissions.ToLower().Equals("all"))
                                {
                                    filterContext.Result = null;
                                    if (UserRolePermissionRecord.Menu.MenuPermissions.Any(o => o.Name.ToLower().Equals("add")))
                                    {
                                        ViewBag.AllowAdding = true;
                                    }
                                    if (UserRolePermissionRecord.Menu.MenuPermissions.Any(o => o.Name.ToLower().Equals("edit")))
                                    {
                                        ViewBag.AllowEditing = true;
                                    }
                                    if (UserRolePermissionRecord.Menu.MenuPermissions.Any(o => o.Name.ToLower().Equals("delete")))
                                    {
                                        ViewBag.AllowDeleting = true;
                                    }
                                    if (UserRolePermissionRecord.Menu.MenuPermissions.Any(o => o.Name.ToLower().Equals("view")))
                                    {
                                        ViewBag.AllowViewing = true;
                                    }
                                    if (UserRolePermissionRecord.Menu.MenuPermissions.Any(o => o.Name.ToLower().Equals("sorting")))
                                    {
                                        ViewBag.AllowSorting = true;
                                    }
                                }
                                else
                                {
                                    string[] UserPermissionArray = UserRolePermissionRecord.Permissions.ToLower().Split('|');
                                    if (string.IsNullOrWhiteSpace(actionURL) || UserPermissionArray.Contains(actionURL))
                                    {
                                        filterContext.Result = null;
                                    }
                                    if (filterContext.Result == null)
                                    {
                                        if (UserPermissionArray.Contains("add"))
                                        {
                                            ViewBag.AllowAdding = true;
                                        }
                                        if (UserPermissionArray.Contains("edit"))
                                        {
                                            ViewBag.AllowEditing = true;
                                        }
                                        if (UserPermissionArray.Contains("delete"))
                                        {
                                            ViewBag.AllowDeleting = true;
                                        }
                                        if (UserPermissionArray.Contains("view"))
                                        {
                                            ViewBag.AllowViewing = true;
                                        }
                                        if (UserPermissionArray.Contains("sorting"))
                                        {
                                            ViewBag.AllowSorting = true;
                                        }
                                    }
                                }
                                if (filterContext.Result == null)
                                {
                                    ViewBag.ControllerName = UserRolePermissionRecord.Menu.Name;
                                }
                            }
                        }
                    }

                    if (filterContext.Result == null)
                    {
                        ViewBag.UserRecord = UserRecord;
                        if (UserRolePermissionRecords.Count > 0)
                        {
                            ViewBag.UserRolePermissionRecords = UserRolePermissionRecords;
                            ViewBag.UserRoleParentPermissionRecords = UserRolePermissionRecords.Where(o => o.Menu.Menu1 != null).Select(o => o.Menu.Menu1).Distinct().ToList();
                        }
                        List<BreadCrumbMenu> breadCrumbList = new List<BreadCrumbMenu>();
                        if (!string.IsNullOrWhiteSpace(actionURL))
                        {
                            ViewBag.ActionName = UpperCaseFirstLetter(actionURL);
                            ViewBag.ActionURL = controllerURL;
                            if (!controllerURL.Equals("dashboard"))
                            {
                                breadCrumbList.Add(new BreadCrumbMenu { Name = ViewBag.ControllerName, AccessURL = ConvertToWebURL(controllerURL), ClassName = "" });
                            }
                            breadCrumbList.Add(new BreadCrumbMenu { Name = ViewBag.ActionName, AccessURL = "#", ClassName = "active" });
                        }
                        else
                        {
                            breadCrumbList.Add(new BreadCrumbMenu { Name = ViewBag.ControllerName, AccessURL = "#", ClassName = "active" });
                        }
                        ViewBag.BreadCrumbMenus = breadCrumbList;
                        ViewBag.PageURL = ViewBag.WebsiteURL + controllerURL;
                        ViewBag.PageName = controllerURL;
                    }
                }
            }



            LiquadCargoManagment.Helpers.ApplicationHelper.ProfileImage = GetCookie("ProfileImage");
            LiquadCargoManagment.Helpers.ApplicationHelper.Username = GetCookie("Username");
            LiquadCargoManagment.Helpers.ApplicationHelper.UserID = GetCookie("UserID") != string.Empty ? Convert.ToInt64(GetCookie("UserID")) : 0;
            LiquadCargoManagment.Helpers.ApplicationHelper.OwnCompanyID = GetCookie("UserID") != string.Empty ? Convert.ToInt64(GetCookie("OwnCompanyID")) : 0;
            LiquadCargoManagment.Helpers.ApplicationHelper.SubcriptionID = GetCookie("UserID") != string.Empty ? Convert.ToInt64(GetCookie("SubcriptionID")) : 0;
            LiquadCargoManagment.Helpers.ApplicationHelper.RoleID = GetCookie("UserID") != string.Empty ? Convert.ToInt64(GetCookie("RoleID")) : 0;
            LiquadCargoManagment.Helpers.ApplicationHelper.lstRolePerm = context.RolePermissions
                            .Where(x => x.RoleID == LiquadCargoManagment.Helpers.ApplicationHelper.RoleID && x.Parameter != "None").ToList();
            var assignedCompanies = context.UserAssignedCompanies.Where(x => x.UserID == LiquadCargoManagment.Helpers.ApplicationHelper.UserID).ToList();
            LiquadCargoManagment.Helpers.ApplicationHelper.lstAssignedCompanies = new List<long?>();
            foreach (var item in assignedCompanies)
            {
                LiquadCargoManagment.Helpers.ApplicationHelper.lstAssignedCompanies.Add(item.OwnCompanyID);
            }
            
            var ActionName = filterContext
                .ActionDescriptor.ActionName.ToLower();
            var formName = context.NavMenus.Where(x => x.ActionName.Equals(ActionName)).FirstOrDefault();
            if (formName != null)
            {
                string _params = _security.IsUserAuthenticFor(formName.FormID);
                if (_params == string.Empty)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();

                }
                else
                {
                    Session["Allow"] = _params;

                }
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        protected override void Dispose(bool disposing)
        {
            Database.Dispose();
            base.Dispose(disposing);
        }
    }
}