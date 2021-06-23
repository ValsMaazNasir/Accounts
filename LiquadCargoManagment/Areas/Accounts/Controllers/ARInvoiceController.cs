
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
    public class ARInvoiceController : BaseController
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
            IQueryable<ARInvoice> dataSource;
            if (CurrentUserRecord.RoleID == EnumRole.SuperAdministrator)
            {
                dataSource = Database.ARInvoices.Where(o => o.IsDeleted == false).AsQueryable();
            }
            else
            {
                dataSource = Database.ARInvoices.Where(o => o.IsDeleted == false && o.CreatedBy == CurrentUserRecord.ID).AsQueryable();
            }
            int TotalDataCount = dataSource.Count();
            if (!string.IsNullOrWhiteSpace(param.Search.Value))
            {
                string searchValue = param.Search.Value.ToLower().Trim();
                DateTime searchDate = ParseExactDateTime(searchValue);
                dataSource = dataSource.Where(p => (
                    p.CreatedDateTime != null && System.Data.Entity.DbFunctions.TruncateTime(p.CreatedDateTime) == System.Data.Entity.DbFunctions.TruncateTime(searchDate) ||
                    p.UpdatedDateTime != null && System.Data.Entity.DbFunctions.TruncateTime(p.UpdatedDateTime) == System.Data.Entity.DbFunctions.TruncateTime(searchDate))
                );
            }
            int FilteredDataCount = dataSource.Count();
            // dataSource = dataSource.SortBy(param.SortOrder).Skip(param.Start).Take(param.Length);
            var resultList = dataSource.ToList();
            var resultData = from x in resultList
                             select new { x.ID, x.Code, x.Description, PostingDate = x.PostingDate.ToString(Website_Date_Time_Format)  ,x.Discount, DocumentDate = x.DocumentDate.ToString(Website_Date_Time_Format), DueDate = x.DueDate.ToString(Website_Date_Time_Format), x.GrandTotal,x.TaxAmount ,x.Total, x.Status, CreatedDateTime = x.CreatedDateTime.ToString(Website_Date_Time_Format), UpdatedDateTime = (x.UpdatedDateTime.HasValue ? x.UpdatedDateTime.Value.ToString(Website_Date_Time_Format) : "") };

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
        }
        public ARInvoiceModel GetRecord(int? id)
        {
            FillDropDown();
            User CurrentUserRecord = GetUserData();
            ARInvoiceModel Model = new ARInvoiceModel();
            var Record = Database.ARInvoices.FirstOrDefault(o => o.ID == id && o.IsDeleted == false);
            if (Record != null)
            {
                Model.ID = Record.ID;
                Model.Code= Record.Code;
                Model.Description = Record.Description;
                Model.Discount = Record.Discount;
                Model.DocumentDate = Record.DocumentDate;
                Model.DueDate= Record.DueDate;
                Model.GrandTotal= Record.GrandTotal;
                Model.PostingDate= Record.PostingDate;
                Model.TaxAmount= Record.TaxAmount;
                Model.Total= Record.Total;
                Model.Status = Record.Status;

            }
            Model.ARInvoiceDetails = Database.ARInvoiceDetails.Where(x => x.ARInvoiceID == id).ToList();
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
                return Redirect(ViewBag.WebsiteURL + "arinvoice");
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
                return Redirect(ViewBag.WebsiteURL + "arinvoice");
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
                    var RecordToDelete = Database.ARInvoices.FirstOrDefault(o => o.ID == RecordID && o.IsDeleted == false);
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
        public JsonResult Save(ARInvoiceModel modelRecord)
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
                        var Record = Database.ARInvoices.FirstOrDefault(x => x.ID == modelRecord.ID && x.IsDeleted == false);
                        if (Record == null)
                        {
                            Record = Database.ARInvoices.Create();
                            isRecordWillAdded = true;
                        }

                        Record.Code = modelRecord.Code;
                        Record.Description = modelRecord.Description;
                        Record.Discount = modelRecord.Discount;
                        Record.DocumentDate = modelRecord.DocumentDate;
                        Record.DueDate = modelRecord.DueDate;
                        Record.GrandTotal = modelRecord.GrandTotal;
                        Record.PostingDate = modelRecord.PostingDate;
                        Record.TaxAmount = modelRecord.TaxAmount;
                        Record.Total = modelRecord.Total;
                        Record.Status = EnumStatus.Enable;

                        if (isRecordWillAdded)
                        {
                            Record.CreatedDateTime = GetDateTime();
                            Record.CreatedBy = CurrentUserRecord.ID;
                            Database.ARInvoices.Add(Record);
                        }
                        else
                        {
                            Record.UpdatedDateTime = GetDateTime();
                            Record.UpdatedBy = CurrentUserRecord.ID;
                        }

                        Database.ARInvoiceDetails.RemoveRange(Database.ARInvoiceDetails.Where(x => x.ARInvoiceID.Equals(modelRecord.ID)).ToList());
                        Database.ARInvoiceDetails.AddRange(modelRecord.ARInvoiceDetails);
                        Database.SaveChanges();
                        AjaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                        AjaxResponse.Message = "Successfully Added.";
                        AjaxResponse.TargetURL = ViewBag.WebsiteURL + "arinvoice";
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