
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static LiquadCargoManagment.Areas.Accounts.Helpers.ApplicationHelper;
using LiquadCargoManagment.Areas.Accounts.Models;

namespace LiquadCargoManagment.Areas.Accounts.Controllers
{
    public class SaleOrderController : BaseController
    {
        // GET: Management
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Listener(DTParameters param)
        {
            User CurrentUserRecord = GetUserData();
            IQueryable<ChartOfAccount> dataSource;
            if (CurrentUserRecord.RoleID == EnumRole.SuperAdministrator)
            {
                dataSource = Database.ChartOfAccounts.Where(o => o.IsDeleted == false).AsQueryable();
            }
            else
            {
                dataSource = Database.ChartOfAccounts.Where(o => o.IsDeleted == false && o.CreatedBy == CurrentUserRecord.ID).AsQueryable();
            }
            int TotalDataCount = dataSource.Count();
            if (!string.IsNullOrWhiteSpace(param.Search.Value))
            {
                string searchValue = param.Search.Value.ToLower().Trim();
                DateTime searchDate = ParseExactDateTime(searchValue);
                dataSource = dataSource.Where(p => (
                    p.Name.ToString().ToLower().Contains(searchValue) ||
                    p.CreatedDateTime != null && System.Data.Entity.DbFunctions.TruncateTime(p.CreatedDateTime) == System.Data.Entity.DbFunctions.TruncateTime(searchDate) ||
                    p.UpdatedDateTime != null && System.Data.Entity.DbFunctions.TruncateTime(p.UpdatedDateTime) == System.Data.Entity.DbFunctions.TruncateTime(searchDate))
                );
            }
            int FilteredDataCount = dataSource.Count();
            // dataSource = dataSource.SortBy(param.SortOrder).Skip(param.Start).Take(param.Length);
            var resultList = dataSource.ToList();
            var resultData = from x in resultList
                             select new { x.ID, TotalBalance = x.AccountsLedgers.OrderByDescending(i => i.ID).FirstOrDefault()?.Balance, x.AccountsHeadId, AccountsHead = x.AccountsHead.Name, Code = string.Format("{0}{1}", x.ParentCode, x.Code), x.Name, x.Status, CreatedDateTime = x.CreatedDateTime.ToString(Website_Date_Time_Format), UpdatedDateTime = (x.UpdatedDateTime.HasValue ? x.UpdatedDateTime.Value.ToString(Website_Date_Time_Format) : "") };

            var result = new
            {
                draw = param.Draw,
                data = resultData,
                recordsFiltered = FilteredDataCount,
                recordsTotal = TotalDataCount
            };
            return Json(result);
        }


        public void FillDropDown()
        {
            var StatusItems = new List<StatusModel>();
            StatusItems.Add(new StatusModel() { Value = "Enable", Text = "Enable" });
            StatusItems.Add(new StatusModel() { Value = "Disable", Text = "Disable" });
            ViewBag.StatusRecords = StatusItems;
            ViewBag.AccountsHead = Database.AccountsHeads.Where(x => x.ID == 1).ToList();
            ViewBag.GLTypes = Database.GLTypes.Where(x => x.ID == 1).ToList();
            ViewBag.SubAccounts = Database.SubAccounts.Where(x => x.ID == 10).ToList();
        }
        public ChartOfAccountsModel GetRecord(int? id)
        {
            FillDropDown();
            User CurrentUserRecord = GetUserData();
            ChartOfAccountsModel Model = new ChartOfAccountsModel();
            var Record = Database.ChartOfAccounts.FirstOrDefault(o => o.ID == id && o.IsDeleted == false);
            if (Record != null)
            {
                Model.ID = Record.ID;
                Model.AccountsHeadId = Record.SubAccountsId;
                Model.GLTypeId = Record.GLType;
                Model.SubAccountsId = Record.SubAccountsId;
                Model.Name = Record.Name;
                Model.Status = Record.Status;

            }
            return Model;
        }
        public ActionResult Add()
        {
            ViewBag.PageType = "Add";
            return View("Form", GetRecord(0));
        }
        public ActionResult Edit(int? id)
        {
            var Record = GetRecord(id);
            if (Record != null)
            {
                ViewBag.PageType = "Edit";
                return View("Form", Record);
            }
            else
            {
                return Redirect(ViewBag.WebsiteURL + "chartofaccounts");
            }
        }
        public ActionResult Views(int? id)
        {
            var Record = GetRecord(id);
            if (Record != null)
            {
                ViewBag.PageType = EnumPageType.View;
                return View("Form", Record);
            }
            else
            {
                return Redirect(ViewBag.WebsiteURL + "chartofaccounts");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(string _value)
        {
            AjaxResponse AjaxResponse = new AjaxResponse();
            AjaxResponse.Success = false;
            AjaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            AjaxResponse.Message = "Data not found in our records";
            int RecordID = ParseInt(_value);
            if (IsUserLogin())
            {
                User CurrentUserRecord = GetUserData();
                if (RecordID == 0)
                {
                    AjaxResponse.Message = "ID is not in numeric format";
                }
                else
                {
                    var RecordToDelete = Database.ChartOfAccounts.FirstOrDefault(o => o.ID == RecordID && o.IsDeleted == false);
                    if (RecordToDelete != null)
                    {
                        if (RecordToDelete.ID == 0)
                        {
                            AjaxResponse.Message = "Unable to delete this record";
                        }
                        else
                        {
                            RecordToDelete.IsDeleted = true;
                            RecordToDelete.DeletedDateTime = GetDateTime();
                            Database.SaveChanges();
                            AjaxResponse.Success = true;
                            AjaxResponse.Message = "Record Deleted Successfully";
                        }
                    }
                }
            }
            else
            {
                AjaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                AjaxResponse.Message = "Session Expired";
                AjaxResponse.TargetURL = ViewBag.WebsiteURL;
            }
            return Json(AjaxResponse, "json");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save(ChartOfAccountsModel modelRecord)
        {
            AjaxResponse AjaxResponse = new AjaxResponse();
            AjaxResponse.Success = false;
            AjaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            AjaxResponse.Message = "Post Data Not Found";
            try
            {
                if (IsUserLogin())
                {
                    User CurrentUserRecord = GetUserData();
                    bool isAbleToUpdate = true;
                    if (isAbleToUpdate)
                    {
                        bool isRecordWillAdded = false;
                        var Record = Database.ChartOfAccounts.FirstOrDefault(x => x.ID == modelRecord.ID && x.IsDeleted == false);
                        if (Record == null)
                        {
                            Record = Database.ChartOfAccounts.Create();
                            isRecordWillAdded = true;
                        }

                        string ParentCode = string.Empty;
                        ParentCode += GetParentTableCode(Database, EnumParentTables.AccountsHead, modelRecord.AccountsHeadId);
                        ParentCode += GetParentTableCode(Database, EnumParentTables.GLTypes, modelRecord.GLTypeId);
                        ParentCode += GetParentTableCode(Database, EnumParentTables.SubAccounts, modelRecord.SubAccountsId);
                        Record.ParentCode = ParentCode;
                        Record.Code = GenerateNewCode(Database);
                        Record.AccountsHeadId = modelRecord.AccountsHeadId;
                        Record.GLType = modelRecord.GLTypeId;
                        Record.SubAccountsId = modelRecord.SubAccountsId;
                        Record.Name = modelRecord.Name;
                        Record.Status = modelRecord.Status;

                        if (isRecordWillAdded)
                        {
                            Record.CreatedDateTime = GetDateTime();
                            Record.CreatedBy = CurrentUserRecord.ID;
                            Database.ChartOfAccounts.Add(Record);
                        }
                        else
                        {
                            Record.UpdatedDateTime = GetDateTime();
                            Record.UpdatedBy = CurrentUserRecord.ID;
                        }

                        Database.SaveChanges();
                        AjaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                        AjaxResponse.Message = "Successfully Added.";
                        AjaxResponse.TargetURL = ViewBag.WebsiteURL + "chartofaccounts";
                        AjaxResponse.Success = true;
                    }
                }
                else
                {
                    AjaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                    AjaxResponse.Message = "Session Expired";
                    AjaxResponse.TargetURL = ViewBag.WebsiteURL;
                }
            }
            catch (Exception ex)
            {
                string _catchMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    _catchMessage += "<br/>" + ex.InnerException.Message;
                }
                AjaxResponse.Message = _catchMessage;
            }
            return Json(AjaxResponse);
        }



    }
}