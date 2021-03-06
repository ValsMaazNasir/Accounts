using LiquadCargoManagment.Helpers;
﻿//using Bilty.Data;
using EntityFramework.Filters;
//using LiquadCargoManagment.DataAccessLayer;
using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static LiquadCargoManagment.Helpers.ApplicationHelper;

namespace LiquadCargoManagment.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        #region VendorType
        [HttpGet]
        public ActionResult VendorType(string auth)
        {
            return View(context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addVendorType(VendorType model)
        {

        //LCMEntities context;
        //SecurityTokenIdentifier _security;
        //ModelDML dml;
        //public AdminController()
        //{
        //    context = new LCMEntities();
        //    _security = new SecurityTokenIdentifier();
        //    dml = new ModelDML(context);
        //    var sc = context.SoftwareSetups.FirstOrDefault();
        //    SoftwareFormatting.DateFormat = sc.DateType.Format;
        //    SoftwareFormatting.ContactNoFormat = sc.ContactNoType.Format;
        //    SoftwareFormatting.NumberFormat = sc.NoType.Format;
        //    SoftwareFormatting.Logo = sc.Logo;
        //}

        #region Order No
        public long GetNewSalesOrderNo()
        {
            long? LastSaleOrderNo = dml.getBilty().Count <= 0 ? 0 : dml.getBilty()[0].OrderNo;
            LastSaleOrderNo = LastSaleOrderNo + 1;
            return Convert.ToInt64(LastSaleOrderNo);
        }

        public long GetNewDieselNo()
        {
            long? DieselNo = dml.getDiesel().Count <= 0 ? 0 : dml.getDiesel()[0].SerialNo;
            DieselNo = DieselNo + 1;
            return Convert.ToInt64(DieselNo);
        }

        public long GetNewChallanNo()
        {
            long? ChallanNo = dml.getChallan().Count <= 0 ? 0 : dml.getChallan()[0].ChallanNo;
            ChallanNo = ChallanNo + 1;
            return Convert.ToInt64(ChallanNo);
        }

        public long GetNewRowDieselNo()
        {
            long? DieselNo = dml.getDieselExpense().Count <= 0 ? 0 : dml.getDieselExpense()[0].SerialNo;
            DieselNo = DieselNo + 1;
            return Convert.ToInt64(DieselNo);
        }

        public long GetNewSalesOrderNoPart()
        {
            long? LastSaleOrderNo = dml.getBiltyPart().Count <= 0 ? 0 : dml.getBiltyPart()[0].OrderNo;
            LastSaleOrderNo = LastSaleOrderNo + 1;
            return Convert.ToInt64(LastSaleOrderNo);
        }

        public long GetUniversalBiltyNo()
        {
            long? OrderNo = dml.getUniversalBilty().Count <= 0 ? 0 : dml.getUniversalBilty()[0].AutoBiltyNo;
            OrderNo = OrderNo + 1;
            return Convert.ToInt64(OrderNo);
        }
        public long PackageTypeNo()
        {
            long? GenerateCode = dml.PackageTypeOrderNo().Count <= 0 ? 0 : dml.PackageTypeOrderNo()[0].OrderNo;
            GenerateCode = GenerateCode + 1;
            return Convert.ToInt64(GenerateCode);
        }
        public long ProductCodeNo()
        {
            long? GenerateCode = dml.ProductOrderNo().Count <= 0 ? 0 : dml.ProductOrderNo()[0].OrderNo;
            GenerateCode = GenerateCode + 1;
            return Convert.ToInt64(GenerateCode);
        }
        #endregion

        #region RevenueType
        [HttpGet]
        public ActionResult RevenueType()
        {
            var records = context.RevenueTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId))
                .OrderByDescending(x => x.RevenueTypeID).ToList();
            return View(records);

        }
        [HttpPost]
        public ActionResult addRevenueType(RevenueType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.VendorTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ID > 0)
                    var Duplicated = context.RevenueTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.RevenueTypeID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            if (Duplicated.RevenueTypeID == model.RevenueTypeID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            var local = context.Set<VendorType>().Local.FirstOrDefault(x => x.ID == model.ID);
                            var record = context.RevenueTypes.FirstOrDefault(x => x.RevenueTypeID == model.RevenueTypeID);
                            if (record != null)
                            {
                                model.DateCreated = record.DateCreated;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedByID = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.isActive = true;
                            var local = context.Set<RevenueType>().Local.FirstOrDefault(x => x.RevenueTypeID == model.RevenueTypeID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.VendorTypes.Add(model);
                            model.CreatedById = ApplicationHelper.UserID;
                            model.isActive = true;
                            model.DateCreated = DateTime.Now;
                            context.RevenueTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_VendorType.cshtml", context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_RevenueType.cshtml", context.RevenueTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.RevenueTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVendorType(int id)
        public JsonResult DeleteRevenueType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.VendorTypes.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    var inRow = context.RevenueTypes.Where(model => model.RevenueTypeID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_VendorType.cshtml", context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Vendor
        [HttpGet]
        public ActionResult Vendor(string auth)
        {
            ViewBag.VendorTypes = new SelectList(context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList(), "ID", "Name");
            return View(context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addVendor(Vendor model)
        {

            string View = RenderPartialToString("~/Views/Shared/_RevenueType.cshtml", context.RevenueTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.RevenueTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeRevenueTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.RevenueTypes.Where(x => x.RevenueTypeID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.isActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_RevenueType.cshtml", context.RevenueTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.RevenueTypeID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Revenue
        [HttpGet]
        public ActionResult Revenue()
        {
            ViewBag.RevenueType = new SelectList(context.RevenueTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "RevenueTypeID", "Name");
            var RevenueList = context.Revenues.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            return View(RevenueList);
        }
        [HttpPost]
        public ActionResult addRevenue(Revenue model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Vendors.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                    var Duplicated = context.Revenues.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            var local = context.Set<Vendor>().Local.FirstOrDefault(x => x.ID == model.ID);
                            var record = context.Revenues.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Revenue>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.Vendors.Add(model);
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Revenues.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Vendor.cshtml", dml.getVendor());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVendor(int id)
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Revenue.cshtml", context.Revenues.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRevenue(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Vendors.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    var inRow = context.Revenues.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Vendor.cshtml", dml.getVendor());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OwnCompany
        [HttpGet]
        public ActionResult OwnCompany(string auth)
        {

            ViewBag.OwnGroups = new SelectList(context.OwnGroups.ToList(), "GroupID", "Name");
            ViewBag.Roles = new SelectList(context.Roles.ToList(), "RoleID", "RoleName");
            ViewBag.Subcriptions = new SelectList(context.Subcriptions.ToList(), "ID", "Name");
            return View(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult AddOwnCompany(OwnCompany model, UserAccount UserAcc, HttpPostedFileBase file)
        {
            string Message = "";
            string Status = "OK";
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.Code != null && model.Name != null /*&& model.GroupID > 0*/)
                    {

                        var Duplicated = dml.getOwnCompany(model.Code, model.Name);
                        var AllCompanies = new List<OwnCompany>();
                        if (model.SubcriptionID > 0)
                        {
                            AllCompanies = context.OwnCompanies.Where(x => x.SubcriptionID == model.SubcriptionID).ToList();
                        }
                        else
                        {
                            AllCompanies = context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();

                        }
                        model.Code = model.Code.ToUpper();
                        if (file != null)
                        {
                            model.CompanyLogo = ApplicationHelper.UploadFile(file, ApplicationHelper.OwnCompanyLogo, Server);
                        }
                        model.Code = model.Code.ToUpper();
                        if (model.ID > 0)
                        {
                            if (Duplicated.Count > 1)
                            {
                                Message = "The record with this code and name is already exist";
                                Status = "Duplicate";
                            }
                            else
                            {
                                if (model.SubcriptionID <= 0)
                                {
                                    model.SubcriptionID = ApplicationHelper.SubcriptionID;
                                }
                                model.ModifiedBy = ApplicationHelper.UserID;
                                model.ModifiedDate = DateTime.Now;
                                model.IsActive = true;
                                var local = context.Set<OwnCompany>().Local.FirstOrDefault(x => x.ID == model.ID);
                                if (local != null)
                                {
                                    context.Entry(local).State = System.Data.Entity.EntityState.Detached;

                                }
                                context.Entry(model).State = EntityState.Modified;
                                if (AllCompanies.Count <= 0)
                                {
                                    if (UserAcc.UserName != null && UserAcc.UserPassword != null && UserAcc.RoleID != null)
                                    {

                                        var account = context.UserAccounts.Where(x => x.OwnCompanyId == model.ID).FirstOrDefault();
                                        account.UserName = UserAcc.UserName;
                                        account.UserPassword = UserAcc.UserPassword;
                                        account.ModifiedDate = DateTime.Now;
                                        account.ModifiedBy = ApplicationHelper.UserID;
                                        account.Active = true;
                                        context.Entry(account).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        throw new Exception("if you create first own company so Username,Password and Role is required");
                                    }
                                }
                            }


                        }
                        else
                        {
                            if (Duplicated.Count > 0)
                            {
                                Message = "The record with this code and name is already exist";
                                Status = "Duplicate";
                            }
                            else
                            {
                                if (model.SubcriptionID == null)
                                {
                                    model.SubcriptionID = ApplicationHelper.SubcriptionID;
                                }
                                model.CreatedBy = ApplicationHelper.UserID;
                                model.IsActive = true;
                                model.CreatedDate = DateTime.Now;
                                context.OwnCompanies.Add(model);
                                context.SaveChanges();
                                if (AllCompanies.Count <= 0)
                                {
                                    if (UserAcc.UserName != null && UserAcc.UserPassword != null && UserAcc.RoleID != null)
                                    {
                                        UserAcc.Active = true;
                                        UserAcc.OwnCompanyId = model.ID;
                                        UserAcc.CreatedBy = ApplicationHelper.UserID;
                                        UserAcc.CreatedDate = DateTime.Now;
                                        context.UserAccounts.Add(UserAcc);
                                    }
                                    else
                                    {
                                        throw new Exception("if you create first own company so Username,Password and Role is required");
                                    }

                                }

                            }
                            context.SaveChanges();
                            transaction.Commit();
                            AccountController ctrl = new AccountController();
                            ctrl.GetOwnCompanies();
                        }


                    }
                    else
                    {
                        Message = "Code Name and Group is Required";
                        Status = "Required";
                    }
                }
                catch (Exception ex)
                {

                    Message = $"An error occured due to: {ex.Message}";
                    Status = "Exception";
                    transaction.Rollback();
                }
            }
            List<OwnCompany> data = dml.getOwnCompany();
            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteOwnCompany(int id)
            string View = RenderPartialToString("~/Views/Shared/_Revenue.cshtml", context.Revenues.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeRevenueStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Revenues.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Revenue.cshtml", context.Revenues.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ExpenseType
        [HttpGet]
        public ActionResult ExpenseType(string auth)
        {
            return View(context.ExpensesTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ExpensesTypeID).ToList());
        }
        [HttpPost]
        public ActionResult addExpenseType(ExpensesType model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.ExpensesTypeCode != null || model.ExpensesTypeName != null)
                {
                    model.ExpensesTypeCode = model.ExpensesTypeCode.ToUpper();
                    var Duplicated = context.ExpensesTypes.Where(x => (x.ExpensesTypeCode == model.ExpensesTypeCode || x.ExpensesTypeName == model.ExpensesTypeName) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ExpensesTypeID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ExpensesTypeID == model.ExpensesTypeID)
                            {
                                isEditable = true;

                            }
                        }
                        else
                        {
                            isEditable = true;
                        }
                        if (isEditable)
                        {
                            var record = context.ExpensesTypes.FirstOrDefault(x => x.ExpensesTypeID == model.ExpensesTypeID);
                            if (record != null)
                            {
                                model.DateCreated = record.DateCreated;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedByUserID = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<ExpensesType>().Local.FirstOrDefault(x => x.ExpensesTypeID == model.ExpensesTypeID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedByUserID = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.DateCreated = DateTime.Now;
                            context.ExpensesTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_ExpenseType.cshtml", context.ExpensesTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ExpensesTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteExpenseType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.OwnCompanies.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {
                        context.OwnCompanies.Remove(inRow);
                        DbModelBuilder modelBuilder = new DbModelBuilder();
                        modelBuilder.Entity<OwnCompany>()
                .HasMany<UserAccount>(c => c.UserAccounts)
                .WithOptional(x => x.OwnCompany)
                .WillCascadeOnDelete(true);
                        context.SaveChanges();
                    var inRow = context.ExpensesTypes.Where(model => model.ExpensesTypeID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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
                Status = ex.GetType().Name;
            }

            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", context.OwnCompanies
                .Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeOwnCompanyStatus(long Id, bool status)
                Status = "Exception";
            }

            string View = RenderPartialToString("~/Views/Shared/_ExpenseType.cshtml", context.ExpensesTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ExpensesTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeExpenseTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.OwnCompanies.Where(x => x.ID == Id).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", context.OwnCompanies.OrderByDescending(x => x.ID).ToList());
                var record = context.ExpensesTypes.Where(x => x.ExpensesTypeID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_ExpenseType.cshtml", context.ExpensesTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ExpensesTypeID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        #endregion

        #region CustomMethod

        public RedirectToRouteResult ExpireToken()
        {
            AccountController _account = new AccountController();
            _account.LogoutWithoutSessionExpire();
            TempData["unauth"] = "you are trying to change authentication token please login again for security purpose";
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region CustomerGroup
        [HttpGet]
        public ActionResult CustomerGroup(string auth)
        {

            return View(context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.GroupID).ToList());

        }
        [HttpPost]
        public ActionResult AddCustomerGroup(CustomerGroup model)
        {


        #region Expense
        [HttpGet]
        public ActionResult Expense()
        {
            ViewBag.ExpenseType = new SelectList(context.ExpensesTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ExpensesTypeID", "ExpensesTypeName");
            var ExpenseList = context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            return View(ExpenseList);
        }
        [HttpPost]
        public ActionResult addExpense(Expense model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.CustomerGroups.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.GroupID > 0)
                    var Duplicated = context.Expenses.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.GroupID == model.GroupID)
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<CustomerGroup>().Local.FirstOrDefault(x => x.GroupID == model.GroupID);
                            var record = context.Expenses.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Expense>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.CustomerGroups.Add(model);
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Expenses.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<CustomerGroup> data = context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", data);

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Expense.cshtml", context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerGroup(int id)
        public JsonResult DeleteExpense(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerGroups.Where(model => model.GroupID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    var inRow = context.Expenses.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.GroupID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeCustomerGroupStatus(long Id, bool status)
            string View = RenderPartialToString("~/Views/Shared/_Expense.cshtml", context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeExpenseStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.CustomerGroups.Where(x => x.GroupID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.GroupID).ToList());
                var record = context.Expenses.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Expense.cshtml", context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CustomerCompany
        [HttpGet]
        public ActionResult CustomerCompany(string auth)
        {

            ViewBag.CustomerGroup = new SelectList(context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "GroupID", "Name");
            return View(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());

        }
        [HttpPost]
        public ActionResult AddCustomerCompany(CustomerCompany model)
        {
        #region VendorType
        [HttpGet]
        public ActionResult VendorType(string auth)
        {
            return View(context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addVendorType(VendorType model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.Name != null && model.GroupID > 0)
                {
                    var Duplicated = context.CustomerCompanies.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.VendorTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.VendorTypes.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            var local = context.Set<VendorType>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.CustomerCompanies.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }

                    }

                }
                else
                {
                    Message = "Code Name and Group is Required";
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.VendorTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<CustomerCompany> data = dml.getCustomerCompany();
            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", data);

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_VendorType.cshtml", context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerCompany(int id)
        public JsonResult DeleteVendorType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerCompanies.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    var inRow = context.VendorTypes.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeCustomerCompanyStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.CustomerCompanies.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

            string View = RenderPartialToString("~/Views/Shared/_VendorType.cshtml", context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Vendor
        [HttpGet]
        public ActionResult Vendor(string auth)
        {
            ViewBag.VendorTypes = new SelectList(context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList(), "ID", "Name");
            return View(context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addVendor(Vendor model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Vendors.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Vendors.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            var local = context.Set<Vendor>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.Vendors.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Vendor.cshtml", dml.getVendor());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVendor(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Vendors.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Vendor.cshtml", dml.getVendor());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DamageType
        [HttpGet]
        public ActionResult DamageType()
        {
            var DamageTypeList = context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DamageTypeID).ToList();
            return View(DamageTypeList);
        }
        [HttpPost]
        public ActionResult addDamageType(DamageType model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.DamageTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.DamageTypeID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.DamageTypeID == model.DamageTypeID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.DamageTypes.FirstOrDefault(x => x.DamageTypeID == model.DamageTypeID);
                            if (record != null)
                            {
                                model.DateCreated = record.DateCreated;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedByID = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.isActive = true;
                            var local = context.Set<DamageType>().Local.FirstOrDefault(x => x.DamageTypeID == model.DamageTypeID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedByID = ApplicationHelper.UserID;
                            model.isActive = true;
                            model.DateCreated = DateTime.Now;
                            context.DamageTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_DamageType.cshtml", context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DamageTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteDamageType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.DamageTypes.Where(model => model.DamageTypeID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_DamageType.cshtml", context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DamageTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDamageTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.DamageTypes.Where(x => x.DamageTypeID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.isActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_DamageType.cshtml", context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DamageTypeID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Damage
        [HttpGet]
        public ActionResult Damage()
        {
            ViewBag.DamageType = new SelectList(context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "DamageTypeID", "Name");
            var DamageList = context.Damages.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            return View(DamageList);
        }
        [HttpPost]
        public ActionResult addDamage(Damage model)
        {


            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Damages.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Damages.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Damage>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Damages.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Damage.cshtml", context.Damages.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteDamage(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Damages.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Damage.cshtml", context.Damages.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDamageStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Damages.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Damage.cshtml", context.Damages.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ProductType
        [HttpGet]
        public ActionResult ProductType()
        {
            var List = context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList();
            return View(List);
        }
        [HttpPost]
        public ActionResult addProductType(Models.Category model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Categories.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Categories.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            var local = context.Set<Models.Category>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.Categories.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            var list = context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList();
            string View = RenderPartialToString("~/Views/Shared/_ProductType.cshtml", list);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteProductType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Categories.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_ProductType.cshtml", context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Product
        public ActionResult Product()
        {
            ViewBag.GenerateCode = ProductCodeNo();
            ViewBag.ProductTypes = new SelectList(context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.PackageType = new SelectList(context.PackageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "PackageTypeID", "PackageTypeName");
            ViewBag.ProductNature = new SelectList(context.Natures.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            return View(context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
        }


        [HttpPost]
        public ActionResult addProduct(Product model)
        {


            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Products.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Products.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Product>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.Products.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();

            string View = RenderPartialToString("~/Views/Shared/_Product.cshtml", dml.getProduct());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteProduct(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Products.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Product.cshtml", context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeProductStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Products.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Product.cshtml", context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchProduct(DateTime? DateTo, DateTime? DateFrom, long? ProductType, string Name)
        {
            List<Product> lstOrders = new List<Product>();

            if (DateFrom != null && DateTo != null)
            {
                lstOrders = dml.getSearchProduct(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstOrders = dml.getSearchProduct(Convert.ToDateTime(DateFrom), "from");
            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = dml.getSearchProduct(Convert.ToDateTime(DateTo), "to");
            }
            else if (ProductType != null)
            {
                lstOrders = context.Products.Where(x => x.Category == ProductType && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else if (ProductType != null)
            {
                lstOrders = context.Products.Where(x => x.Category == x.Category && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else if (Name != null)
            {
                lstOrders = context.Products.Where(x => x.Name.StartsWith(Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            else
            {
                lstOrders = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_Product", lstOrders.ToList());

            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //#region OwnGroup
        //[HttpGet]
        //public ActionResult OwnGroup(string auth)
        //{
        //    return View(context.OwnGroups.OrderByDescending(x => x.GroupID).ToList());
        //}
        //[HttpPost]
        //public ActionResult AddOwnGroup(OwnGroup model)
        //{
        //    string Message = "";
        //    string Status = "OK";
        //    try
        //    {
        //        if (model.Code != null)
        //        {
        //            var Duplicated = context.OwnGroups.Where(x => x.Code == model.Code || x.Name == model.Name).ToList();
        //            model.Code = model.Code.ToUpper();
        //            if (model.GroupID > 0)
        //            {
        //                if (Duplicated.Count <= 1)
        //                {
        //                    model.ModifiedBy = ApplicationHelper.UserID;
        //                    model.ModifiedDate = DateTime.Now;
        //                    model.IsActive = true;
        //                    context.Entry(model).State = EntityState.Modified;
        //                }
        //                else
        //                {
        //                    Message = "The record with this code and name is already exist";
        //                    Status = "Duplicate";
        //                }

        //            }
        //            else
        //            {
        //                if (Duplicated.Count <= 0)
        //                {
        //                    model.CreatedBy = ApplicationHelper.UserID;
        //                    model.IsActive = true;
        //                    model.CreatedDate = DateTime.Now;
        //                    context.OwnGroups.Add(model);
        //                }
        //                else
        //                {
        //                    Message = "The record with this code and name is already exist";
        //                    Status = "Duplicate";
        //                }

        //            }

        //        }
        //        else
        //        {
        //            Message = "Code and Name is Required";
        //            Status = "Required";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Message = $"An error occured due to: {ex.Message}";
        //        Status = "Exception";
        //    }

        //    context.SaveChanges();
        //    List<OwnGroup> data = context.OwnGroups.ToList();
        //    string View = RenderPartialToString("~/Views/Shared/_OwnGroup.cshtml", data);
        //    return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        //}
        //public JsonResult DeleteOwnGroup(int id)
        //{
        //    string Message = "";
        //    string Status = "OK";
        //    try
        //    {
        //        if (id > 0)
        //        {
        //            var inRow = context.OwnGroups.Where(model => model.GroupID == id).FirstOrDefault();
        //            if (inRow != null)
        //            {

        //                context.Entry(inRow).State = EntityState.Deleted;
        //                int a = context.SaveChanges();
        //                Message = "Record Deleted Successfully";

        //            }
        //            else
        //            {
        //                Message = "Application cannot find this record in database please refresh the browser";
        //                Status = "Exception";
        //            }
        //        }
        //        else
        //        {
        //            Message = "Something Went Wrong";
        //            Status = "Exception";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Message = $"An error occured due to: {ex.Message}";
        //        Status = "Exception";
        //    }

        //    string View = RenderPartialToString("~/Views/Shared/_OwnGroup.cshtml", context.OwnGroups.OrderByDescending(x => x.GroupID).ToList());
        //    return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult ChangeOwnGroupStatus(long Id, bool status)
        //{
        //    string Message = "";
        //    string Status = "OK";
        //    if (Id > 0)
        //    {
        //        var record = context.OwnGroups.Where(x => x.GroupID == Id).FirstOrDefault();
        //        record.IsActive = status;
        //        context.SaveChanges();
        //    }
        //    string View = RenderPartialToString("~/Views/Shared/_OwnGroup.cshtml", context.OwnGroups.OrderByDescending(x => x.GroupID).ToList());
        //    return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #region OwnCompany
        [HttpGet]
        public ActionResult OwnCompany(string auth)
        {

            ViewBag.OwnGroups = new SelectList(context.OwnGroups.ToList(), "GroupID", "Name");
            ViewBag.Roles = new SelectList(context.Roles.ToList(), "RoleID", "RoleName");
            ViewBag.Subcriptions = new SelectList(context.Subcriptions.ToList(), "ID", "Name");
            return View(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult AddOwnCompany(OwnCompany model, UserAccount UserAcc, HttpPostedFileBase file)
        {
            string Message = "";
            string Status = "OK";
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.Code != null && model.Name != null /*&& model.GroupID > 0*/)
                    {

                        var Duplicated = dml.getOwnCompany(model.Code, model.Name);
                        var AllCompanies = new List<OwnCompany>();
                        if (model.SubcriptionID > 0)
                        {
                            AllCompanies = context.OwnCompanies.Where(x => x.SubcriptionID == model.SubcriptionID).ToList();
                        }
                        else
                        {
                            AllCompanies = context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();

                        }
                        model.Code = model.Code.ToUpper();
                        if (file != null)
                        {
                            model.CompanyLogo = ApplicationHelper.UploadFile(file, ApplicationHelper.OwnCompanyLogo, Server);
                        }
                        model.Code = model.Code.ToUpper();
                        if (model.ID > 0)
                        {
                            if (Duplicated.Count > 1)
                            {
                                Message = "The record with this code and name is already exist";
                                Status = "Duplicate";
                            }
                            else
                            {
                                if (model.SubcriptionID <= 0)
                                {
                                    model.SubcriptionID = ApplicationHelper.SubcriptionID;
                                }
                                var record = context.OwnCompanies.FirstOrDefault(x => x.ID == model.ID);
                                if (record != null)
                                {
                                    model.CreatedDate = record.CreatedDate;
                                }
                                model.ModifiedBy = ApplicationHelper.UserID;
                                model.ModifiedDate = DateTime.Now;
                                model.IsActive = true;
                                if (record != null)
                                {
                                    context.Entry(record).State = System.Data.Entity.EntityState.Detached;

                                }
                                context.Entry(model).State = EntityState.Modified;
                                if (AllCompanies.Count <= 0)
                                {
                                    if (UserAcc.UserName != null && UserAcc.UserPassword != null && UserAcc.RoleID != null)
                                    {

                                        var account = context.UserAccounts.Where(x => x.OwnCompanyId == model.ID).FirstOrDefault();
                                        account.UserName = UserAcc.UserName;
                                        account.UserPassword = UserAcc.UserPassword;
                                        account.ModifiedDate = DateTime.Now;
                                        account.ModifiedBy = ApplicationHelper.UserID;
                                        account.Active = true;
                                        context.Entry(account).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        throw new Exception("if you create first own company so Username,Password and Role is required");
                                    }
                                }
                            }


                        }
                        else
                        {
                            if (Duplicated.Count > 0)
                            {
                                Message = "The record with this code and name is already exist";
                                Status = "Duplicate";
                            }
                            else
                            {
                                if (model.SubcriptionID == null)
                                {
                                    model.SubcriptionID = ApplicationHelper.SubcriptionID;
                                }
                                model.CreatedBy = ApplicationHelper.UserID;
                                model.IsActive = true;
                                model.CreatedDate = DateTime.Now;
                                context.OwnCompanies.Add(model);
                                context.SaveChanges();
                                if (AllCompanies.Count <= 0)
                                {
                                    if (UserAcc.UserName != null && UserAcc.UserPassword != null && UserAcc.RoleID != null)
                                    {
                                        UserAcc.Active = true;
                                        UserAcc.OwnCompanyId = model.ID;
                                        UserAcc.CreatedBy = ApplicationHelper.UserID;
                                        UserAcc.CreatedDate = DateTime.Now;
                                        context.UserAccounts.Add(UserAcc);
                                    }
                                    else
                                    {
                                        throw new Exception("if you create first own company so Username,Password and Role is required");
                                    }

                                }

                            }
                            context.SaveChanges();
                            transaction.Commit();
                            AccountController ctrl = new AccountController();
                            ctrl.GetOwnCompanies();
                        }


                    }
                    else
                    {
                        Message = "Code Name and Group is Required";
                        Status = "Required";
                    }
                }
                catch (Exception ex)
                {

                    Message = $"An error occured due to: {ex.Message}";
                    Status = "Exception";
                    transaction.Rollback();
                }
            }
            List<OwnCompany> data = dml.getOwnCompany();
            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteOwnCompany(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.OwnCompanies.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {
                        context.OwnCompanies.Remove(inRow);
                        DbModelBuilder modelBuilder = new DbModelBuilder();
                        modelBuilder.Entity<OwnCompany>()
                .HasMany<UserAccount>(c => c.UserAccounts)
                .WithOptional(x => x.OwnCompany)
                .WillCascadeOnDelete(true);
                        context.SaveChanges();
                        Message = "Record Deleted Successfully";

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
                Status = ex.GetType().Name;
            }

            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", context.OwnCompanies
                .Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeOwnCompanyStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.OwnCompanies.Where(x => x.ID == Id).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", context.OwnCompanies.OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region VehicleType
        [HttpGet]
        public ActionResult VehicleType(string auth)
        {

            return View(context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());

        }
        [HttpPost]
        public ActionResult addVehicleType(VehicleType model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.VehicleTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.VehicleTypes.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<VehicleType>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.VehicleTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_VehicleType.cshtml", context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVehicleType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.VehicleTypes.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_VehicleType.cshtml", context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeVehicleTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.VehicleTypes.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_VehicleType.cshtml", context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Vehicle
        [HttpGet]
        public ActionResult Vehicle(string auth)
        {
            ViewBag.VehicleTypes = new SelectList(context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.VehicleStandard = new SelectList(context.StandardVehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            ViewBag.Drivers = new SelectList(context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            ViewBag.PrincipleCompany = new SelectList(context.PrincipleCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            return View(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.VehicleID).ToList());
        }
        [HttpPost]
        public ActionResult addVehicle(Vehicle model, HttpPostedFileBase file)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.RegNo != null && model.VehicleTypeID > 0)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Vehicles.Where(x => (x.Code == model.Code || x.RegNo == model.RegNo) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                    if (file != null)
                    {
                        model.Documents = ApplicationHelper.UploadFile(file, ApplicationHelper.VehicleDocuments, Server);
                    }
                    if (model.VehicleID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.VehicleID == model.VehicleID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Vehicles.FirstOrDefault(x => x.VehicleID == model.VehicleID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Vehicle>().Local.FirstOrDefault(x => x.VehicleID == model.VehicleID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Vehicles.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Vehicle.cshtml", context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVehicle(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Vehicles.Where(model => model.VehicleID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Vehicle.cshtml", context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDriverDetails(long Id)
        {
            var data = context.Drivers.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
            return Json(new { Driver = data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeVehicleStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Vehicles.Where(x => x.VehicleID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Vehicle.cshtml", context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.VehicleID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchVehicle(DateTime? DateTo, DateTime? DateFrom, long? VehicleType, string RegNo)
        {
            List<Vehicle> lstOrders = new List<Vehicle>();

            if (DateFrom != null && DateTo != null)
            {
                lstOrders = dml.getSearchVehicle(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstOrders = dml.getSearchVehicle(Convert.ToDateTime(DateFrom), "from");
            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = dml.getSearchVehicle(Convert.ToDateTime(DateTo), "to");
            }
            else if (VehicleType != null)
            {
                lstOrders = context.Vehicles.Where(x => x.VehicleTypeID == VehicleType && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else if (VehicleType != null)
            {
                lstOrders = context.Vehicles.Where(x => x.VehicleTypeID == x.VehicleTypeID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else if (RegNo != null)
            {
                lstOrders = context.Vehicles.Where(x => x.RegNo.StartsWith(RegNo) && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            else
            {
                lstOrders = context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_Vehicle", lstOrders.ToList());

            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        #endregion

        #region Ledgers
        public ActionResult Ledgers()
        {
            return View(context.VendorTypes.ToList());
        }
        #endregion

        #region City
        [HttpGet]
        public ActionResult City()
        {
            ViewBag.Province = new SelectList(context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ProvinceID", "ProvinceName");
            return View(context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID))
                .OrderByDescending(x => x.CityID).ToList());
        }
        [HttpPost]
        public ActionResult addCity(City model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.CityCode != null || model.CityName != null)
                {
                    model.CityCode = model.CityCode.ToUpper();
                    var Duplicated = context.Cities.Where(x => (x.CityCode == model.CityCode || x.CityName == model.CityName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.CityID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.CityID == model.CityID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Cities.FirstOrDefault(x => x.CityID == model.CityID);
                            if (record != null)
                            {
                                model.DateCreated = record.DateCreated;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedByUserId = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<City>().Local.FirstOrDefault(x => x.CityID == model.CityID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedByUserId = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.DateCreated = DateTime.Now;
                            context.Cities.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_City.cshtml", dml.getCity());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }

        //public ActionResult addCity(City model)
        //{
        //    string Message = "";
        //    string Status = "OK";
        //    try
        //    {
        //        if (model.CityCode != null || model.CityName != null)
        //        {
        //            var Duplicated = context.Cities.Where(x => x.CityCode == model.CityCode && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
        //            if (Duplicated == null)
        //            {
        //                if (model.CityID > 0)
        //                {
        //                    model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
        //                    model.ModifiedBy = ApplicationHelper.UserID;
        //                    model.ModifiedDate = DateTime.Now;
        //                    model.IsActive = true;
        //                    context.Entry(model).State = EntityState.Modified;
        //                }
        //                else
        //                {
        //                    model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
        //                    model.CreatedBy = ApplicationHelper.UserID;
        //                    model.IsActive = true;
        //                    model.CreatedDate = DateTime.Now;
        //                    context.Cities.Add(model);
        //                }
        //            }
        //            else
        //            {
        //                Message = "The record with this code and name is already exist";
        //                Status = "Duplicate";
        //            }
        //        }
        //        else
        //        {
        //            Message = "Code and Name is Required";
        //            Status = "Required";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Message = $"An error occured due to: {ex.Message}";
        //        Status = "Exception";
        //    }

        //    context.SaveChanges();
        //    string View = RenderPartialToString("~/Views/Shared/_City.cshtml", context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.CityID).ToList());
        //    return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        //}
        public JsonResult DeleteCity(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Cities.Where(model => model.CityID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";
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

            string View = RenderPartialToString("~/Views/Shared/_City.cshtml", context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.CityID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeCityStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Cities.Where(x => x.CityID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_City.cshtml", context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.CityID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Area
        [HttpGet]
        public ActionResult Area()
        {
            ViewBag.Cities = new SelectList(context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "CityID", "CityName");
            var GridView = context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList();
            return View(GridView);
        }
        [HttpPost]
        public ActionResult addArea(Area model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Areas.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Areas.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.DateCreated = record.DateCreated;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.Status = true;
                            var local = context.Set<Area>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.DateCreated = DateTime.Now;
                            context.Areas.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Area.cshtml", context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteArea(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Areas.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Area.cshtml", context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeAreaStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Areas.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Area.cshtml", context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchArea(DateTime? DateTo, DateTime? DateFrom, int? City, string Name)
        {
            List<Area> lstOrders = new List<Area>();

            if (DateFrom != null && DateTo != null)
            {
                lstOrders = dml.getSearchArea(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstOrders = dml.getSearchArea(Convert.ToDateTime(DateFrom), "from");
            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = dml.getSearchArea(Convert.ToDateTime(DateTo), "to");
            }
            else if (City != null)
            {
                lstOrders = context.Areas.Where(x => x.CityID == City && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else if (City != null)
            {
                lstOrders = context.Areas.Where(x => x.CityID == x.CityID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else if (Name != null)
            {
                lstOrders = context.Areas.Where(x => x.Name.StartsWith(Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else if (Name != null)
            {
                lstOrders = context.Areas.Where(x => x.Name.StartsWith(Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            else
            {
                lstOrders = context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_Area", lstOrders.ToList());

            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CustomMethod

        public RedirectToRouteResult ExpireToken()
        {
            AccountController _account = new AccountController();
            _account.LogoutWithoutSessionExpire();
            TempData["unauth"] = "you are trying to change authentication token please login again for security purpose";
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region CustomerGroup
        [HttpGet]
        public ActionResult CustomerGroup(string auth)
        {

            return View(context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.GroupID).ToList());

        }
        [HttpPost]
        public ActionResult AddCustomerGroup(CustomerGroup model)
        {


            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.CustomerGroups.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();

                    if (model.GroupID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.GroupID == model.GroupID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.CustomerGroups.FirstOrDefault(x => x.GroupID == model.GroupID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<CustomerGroup>().Local.FirstOrDefault(x => x.GroupID == model.GroupID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.CustomerGroups.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<CustomerGroup> data = context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerGroup(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerGroups.Where(model => model.GroupID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.GroupID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeCustomerGroupStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.CustomerGroups.Where(x => x.GroupID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.GroupID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CustomerCompany
        [HttpGet]
        public ActionResult CustomerCompany(string auth)
        {
            ViewBag.CustomerType = new List<string>() { "BillTo", "Sender","Receiver" };

            ViewBag.CustomerGroup = new SelectList(context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "GroupID", "Name");
            return View(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());

        }
        [HttpPost]
        public ActionResult AddCustomerCompany(CustomerCompany model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.Name != null && model.GroupID > 0)
                {
                    var Duplicated = context.CustomerCompanies.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.GroupID == model.GroupID)
                            {
                                isEditable = true;
                            }
                            else
                            {
                                isEditable = true;
                            }
                        }
                        else
                        {
                            isEditable = true;
                        }
                        if (isEditable)
                        {
                            var record = context.CustomerCompanies.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.ModifiedDate = DateTime.Now;
                            var local = context.Set<CustomerCompany>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.OwnCompanyId = ApplicationHelper.OwnCompanyID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.CustomerCompanies.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }

                    }

                }
                else
                {
                    Message = "Code Name and Group is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<CustomerCompany> data = dml.getCustomerCompany();
            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerCompany(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerCompanies.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyId)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeCustomerCompanyStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.CustomerCompanies.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Province
        [HttpGet]
        public ActionResult Province()
        {
            return View(context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ProvinceID).ToList());
        }
        [HttpPost]
        public ActionResult addProvince(Province model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.ProvinceName != null)
                {
                    model.ProvinceName = model.ProvinceName.ToUpper();
                    var Duplicated = context.Provinces.Where(x => (x.ProvinceName == model.ProvinceName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ProvinceID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ProvinceID == model.ProvinceID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            //model.ModifiedByID = ApplicationHelper.UserID;
                            //model.DateModified = DateTime.Now;
                            //model.isActive = true;
                            var local = context.Set<Province>().Local.FirstOrDefault(x => x.ProvinceID == model.ProvinceID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            //model.Cre = ApplicationHelper.UserID;
                            //model.isActive = true;
                            //model.DateCreated = DateTime.Now;
                            context.Provinces.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Province.cshtml", context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ProvinceID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteProvince(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Provinces.Where(model => model.ProvinceID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Province.cshtml", context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ProvinceID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Bank
        [HttpGet]
        public ActionResult Bank()
        {
            return View(context.Banks.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.BankID).ToList());
        }
        [HttpPost]
        public ActionResult addBank(Bank model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Banks.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.BankID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.BankID == model.BankID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Banks.FirstOrDefault(x => x.BankID == model.BankID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Bank>().Local.FirstOrDefault(x => x.BankID == model.BankID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Banks.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Bank.cshtml", context.Banks.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.BankID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteBank(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Banks.Where(model => model.BankID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Bank.cshtml", context.Banks.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.BankID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeBankStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Banks.Where(x => x.BankID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Bank.cshtml", context.Banks.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.BankID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Bank Accounts
        [HttpGet]
        public ActionResult BankAccounts()
        {
            ViewBag.Bank = new SelectList(context.Banks.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "BankID", "Name");
            return View(context.BankAccounts.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addBankAccounts(BankAccount model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.BankAccounts.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.BankAccounts.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<BankAccount>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.BankAccounts.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_BankAccounts.cshtml", context.BankAccounts.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteBankAccounts(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.BankAccounts.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_BankAccounts.cshtml", context.BankAccounts.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeBankAccountsStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.BankAccounts.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_BankAccounts.cshtml", context.BankAccounts.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Principle Company
        [HttpGet]
        public ActionResult Principle(string auth)
        {
            return View(context.PrincipleCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addPrinciple(PrincipleCompany model)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.PrincipleCompanies.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.PrincipleCompanies.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            //model.isActive = true;
                            var local = context.Set<PrincipleCompany>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            //model.Isactive = true;
                            model.CreatedDate = DateTime.Now;
                            context.PrincipleCompanies.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Principle.cshtml", context.PrincipleCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeletePrinciple(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.PrincipleCompanies.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Principle.cshtml", context.PrincipleCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Driver
        [HttpGet]
        public ActionResult Driver(string auth)
        {
            ViewBag.DriverType = new SelectList(context.DriverTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            return View(context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addDriver(Driver model, HttpPostedFileBase file)
        {

            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.Name != null && model.FatherName != null && model.Type != null
                     && model.DateOfBirth != null && model.Gender != null && model.CellNo != null && model.NIC != null
                     && model.NICExpiryDate != null && model.NICIssueDate != null)
                {

                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Drivers.Where(x => (x.Code == model.Code || x.CellNo == model.CellNo || x.NIC == model.NIC) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (file != null)
                    {
                        model.Document = ApplicationHelper.UploadFile(file, ApplicationHelper.DriverDocuments, Server);
                    }
                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Drivers.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            //model.St = true;
                            var local = context.Set<Driver>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            //model.isActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Drivers.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Driver.cshtml", context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteDriver(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Drivers.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Driver.cshtml", context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DriverDetails(long Id)
        {
            var data = context.Drivers.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
            return Json(new { Driver = data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDriverStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Drivers.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.Active = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Driver.cshtml", context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region WorkOrderGrid
        public ActionResult WorkOrderGrid()
        {
            return View(context.WorkOrders.OrderByDescending(x => x.WorkorderId).ToList());
        }
        [HttpPost]
        public JsonResult DeleteWorkOrderGrid(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.WorkOrders.Where(model => model.WorkorderId == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_WorkOrderGrid.cshtml", context.WorkOrders.OrderByDescending(x => x.WorkorderId).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #region Search
        public ActionResult Search(DateTime? DateFrom, DateTime? DateTo)
        {
            List<WorkOrder> lstOrders = new List<WorkOrder>();
            if (DateFrom != null && DateTo == null)
            {
                lstOrders = context.WorkOrders.Where(x => x.OrderDate >= DateFrom).ToList();
            }
            else if (DateFrom != null && DateTo != null)
            {
                lstOrders = context.WorkOrders.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo).ToList();

            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = context.WorkOrders.Where(x => x.OrderDate <= DateTo).ToList();

            }
            string view = RenderPartialToString("_WorkOrderGrid", lstOrders);
            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region WorkOrder
        public ActionResult WorkOrder()
        {
            List<OwnCompany> Billto = context.OwnCompanies.ToList();
            ViewBag.bill = new SelectList(Billto, "ID", "Name");

            List<OwnCompany> Sender = context.OwnCompanies.ToList();
            ViewBag.Sender = new SelectList(Sender, "ID", "Name");

            List<OwnCompany> Oc = context.OwnCompanies.ToList();
            ViewBag.li = new SelectList(Oc, "ID", "Name");

            List<ProductBroker> li = context.ProductBrokers.ToList();
            ViewBag.ProductBroker = new SelectList(li, "Id", "Name");

            List<PackageType> Pt = context.PackageTypes.ToList();
            ViewBag.pt = new SelectList(Pt, "PackageTypeID", "PackageTypeName");

            List<Product> P = context.Products.ToList();
            ViewBag.p = new SelectList(P, "ID", "Name");


            return View();
        }
        [HttpPost]

        public JsonResult InsertCustomers(List<OrderDetail> Details, WorkOrder Order, ProductBroker pro)
        {
            string message = "";
            string status = "OK";
            using (LCMEntities lcm = new LCMEntities())
            {
                using (var transaction = lcm.Database.BeginTransaction())
                {
                    try
                    {

                        long OrderID = 0;

                        if (Order != null)
                        {
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.CreatedDate = DateTime.Now;
                            lcm.WorkOrders.Add(Order);
                            lcm.SaveChanges();
                            OrderID = Order.WorkorderId;
                        }
                        if (Details == null)
                        {
                            Details = new List<OrderDetail>();
                        }


                        foreach (OrderDetail customers in Details)
                        {
                            customers.WorkOrderID = OrderID;
                            lcm.OrderDetails.Add(customers);
                        }
                        lcm.SaveChanges();
                        transaction.Commit();
                        message = "Work order successfully created";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        status = ex.GetType().Name;
                        message = ex.Message;
                        if (ex.InnerException != null)
                        {
                            message += " Inner Exception: " + ex.InnerException.Message;
                        }
                    }
                }

            }

            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Product Broker

        public ActionResult ProductBroker()
        {
            var GridView = context.ProductBrokers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.Id).ToList();
            return View(GridView);
        }

        [HttpPost]
        public ActionResult SaveProductBroker(ProductBroker model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Name != null)
                {
                    model.Name = model.Name.ToUpper();
                    var Duplicated = context.ProductBrokers.Where(x => (x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.Id > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.Id == model.Id)
                            {
                                isEditable = true;
                            }
                        }
                        else
                        {
                            isEditable = true;
                        }
                        if (isEditable)
                        {
                            var record = context.ProductBrokers.FirstOrDefault(x => x.Id == model.Id);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            //model.is = true;
                            var local = context.Set<ProductBroker>().Local.FirstOrDefault(x => x.Id == model.Id);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            //model.isActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.ProductBrokers.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_ProductBroker.cshtml", context.ProductBrokers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.Id).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult DeleteProductBroker(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.ProductBrokers.Where(model => model.Id == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_ProductBroker.cshtml", context.ProductBrokers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Bilty

        [HttpGet]
        public ActionResult Bilty(long? id)
        {
            var Model = new OrderViewModel();
            SetCompactBiltyDropDown();

            if (id != null && id > 0)
            {
                long _id = (long)id;

                Model.listBilty = context.Bilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                Model.listBiltyDetail = context.BiltyDetails.Where(x => x.BiltyID == _id).ToList();

                return View(Model);
            }

            else
            {
                Model.listBilty = new Models.Bilty();
                Model.listBiltyDetail = new List<BiltyDetail>();
            }
            return View(Model);
        }

        [HttpPost]
        public JsonResult InsertBilty(Models.Bilty Order, List<BiltyDetail> Details)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    long OrderID = 0;

                    if (Order != null)
                    {
                        if (Order.ID > 0)
                        {
                            var record = context.Bilties.FirstOrDefault(x => x.ID == Order.ID);
                            if (record != null)
                            {
                                Order.OrderNo = record.OrderNo;
                                Order.CreatedDate = record.CreatedDate;
                            }
                            context.BiltyDetails.RemoveRange(context.BiltyDetails.Where(x => x.BiltyID == Order.ID).ToList());
                            Order.ModifiedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.ModifiedDate = DateTime.Now;
                            var local = context.Set<LiquadCargoManagment.Models.Bilty>().Local.FirstOrDefault(x => x.ID == x.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(Order).State = EntityState.Modified;
                            OrderID = Order.ID;

                            //PARTIAL BILTY
                            
                            //double totalFrieght = 0;
                            //totalFrieght += Order.TotalFreight > 0 ? (double)Order.TotalFreight : 0;
                            //var halfFrieght = totalFrieght / 2;
                            //string[] array = { "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z" };
                            //for (int i = 0; i < 2; i++)
                            //{
                            //    PartialBilty ch = new PartialBilty();
                            //    ch.BiltyID = Order.ID;
                            //    ch.OrderDate = Order.OrderDate;
                            //    ch.DeliveryDate = Order.DeliveryDate;
                            //    ch.OwnCompanyID = Order.OwnCompanyID;
                            //    ch.OwnCompany = Order.OwnCompany;
                            //    ch.PickLocation = Order.PickLocation;
                            //    ch.CustomerType = Order.CustomerType;
                            //    ch.Reciever = Order.Reciever;
                            //    ch.VehicleID = Order.VehicleID;
                            //    ch.ExternalVehicleNo = Order.ExternalVehicleNo;
                            //    ch.Remarks = Order.Remarks;
                            //    ch.BillTo = Order.BillTo;
                            //    ch.Labour = Order.Labour;
                            //    ch.TotalAdditionalFreight = Order.TotalAdditionalFreight;
                            //    ch.TotalWeighbridge = Order.TotalWeighbridge;
                            //    ch.OrderDetailsTotalQTY = Order.OrderDetailsTotalQTY;
                            //    ch.OrderDetailsTotalWeight = Order.OrderDetailsTotalWeight;
                            //    ch.WeightQTYTotal = Order.WeightQTYTotal;
                            //    ch.OrderNo = record.OrderNo;
                            //    ch.PartialNo = record.OrderNo + "-" + array[i];
                            //    ch.TotalFreight = halfFrieght;

                            //    context.PartialBilties.Add(ch);
                            //}
                        }
                        else
                        {
                            Order.OrderNo = GetNewSalesOrderNo();
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.CreatedDate = DateTime.Now;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            context.Bilties.Add(Order);
                            context.SaveChanges();
                            OrderID = Order.ID;
                        }
                        foreach (BiltyDetail item in Details)
                        {
                            item.BiltyID = OrderID;
                            context.BiltyDetails.Add(item);
                            context.SaveChanges();
                        }

                        //for (int s = 0; s < 2; s++)
                        //{
                        //    foreach (var items in Details)
                        //    {
                        //        PartialBiltyDetail pd = new PartialBiltyDetail();
                        //        double Frieght = 0;
                        //        Frieght += items.Freight > 0 ? (double)items.Freight : 0;
                        //        var half = Frieght / 2;
                        //        pd.ProductTypeId = items.ProductTypeId;
                        //        pd.ProductId = items.ProductId;
                        //        pd.Weight = items.Weight;
                        //        pd.QTY = items.QTY;
                        //        pd.TotalWeight = items.TotalWeight;
                        //        pd.Freight = half;
                        //        pd.PartialBiltyID = items.ID;
                        //    }
                        //    context.PartialBiltyDetails.Add(pd);
                        //    context.SaveChanges();
                        //}
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    message = "Bilty has been created";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }
                }
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public void SetCompactBiltyDropDown()
        {
            ViewBag.City = new SelectList(context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "CityID", "CityName");
            ViewBag.Area = new SelectList(context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.ShipmentType = new SelectList(context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.OwnCompany = context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).OrderByDescending(x => x.ID).ToList();
            ViewBag.Products = new SelectList(context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderBy(x => x.Name), "ID", "Name");
            ViewBag.BillTo = context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderBy(x => x.Name).ToList();
            ViewBag.Sender = context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderBy(x => x.LocationName).ToList();
            ViewBag.Vehicle = context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderBy(x => x.RegNo).ToList();
            ViewBag.Reciever = context.PickLocations.Where(x => (x.LocationType == "Drop" || x.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderBy(x => x.LocationName).ToList();
            ViewBag.PickLocation = context.PickLocations.Where(x => (x.LocationType == "Pick" || x.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderBy(x => x.LocationName).ToList();
            ViewBag.ProductBroker = new SelectList(context.ProductBrokers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderBy(x => x.Name), "Id", "Name");
            ViewBag.ProductType = new SelectList(context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderBy(x => x.Name), "ID", "Name");
            var ProductType = context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Name = string.Format("{0} | {1}", x.Code, x.Name) }).OrderBy(x => x.Name).ToList();
            ViewBag.ProductType = new SelectList(ProductType, "ID", "Name");
            var Category = context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Name = string.Format("{0} | {1}", x.Code, x.Name) }).OrderBy(x => x.Name).ToList();
            ViewBag.Category = new SelectList(ProductType, "ID", "Name");
            var Product = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Name = string.Format("{0} | {1}", x.Code, x.Name) }).OrderBy(x => x.Name).ToList();
            ViewBag.Product = new SelectList(Product, "ID", "Name");
            ViewBag.City = new SelectList(context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderBy(x => x.CityName), "CityID", "CityName");
            ViewBag.Area = new SelectList(context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).OrderBy(x => x.CityID), "ID", "Name");
            ViewBag.PackageType = new SelectList(context.PackageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "PackageTypeID", "PackageTypeName");
            ViewBag.ProductNature = new SelectList(context.Natures.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
        }

        #endregion

        #region Bilty Cascading Function

        public JsonResult getProductList(int ID)
        {
            List<Product> ProductList = context.Products.Where(x => x.Category == ID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            var pList = new SelectList(ProductList, "ID", "Name");
            return Json(pList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAccount(int ID)
        {
            context.Configuration.ProxyCreationEnabled = false;
            List<Product> ProductList = context.Products.Where(x => x.ID == ID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            return Json(ProductList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnit(long id)
        {
            var data = context.Products.Where(x => x.ID == id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
            return Json(new { unit = data.Unit }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Bilty Grid
        public ActionResult BiltyGrid()
        {
            string dateString = DateTime.Now.ToString("yyyy/mm/dd");
            ViewBag.BillTo = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).ToList(), "ID", "Name");
            ViewBag.Vehicle = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "VehicleID", "RegNo");
            ViewBag.Receiver = new SelectList(context.PickLocations.Where(x => (x.LocationType == "Drop" || x.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "LocationName");
            return View(context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.OrderNo).ToList());
        }

        [HttpPost]

        public JsonResult DeleteBilty(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Bilties.Where(x => x.ID == id && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new Models.Bilty() { ID = id, IsDeleted = true, DeletedBy = ApplicationHelper.UserID, DeletedDate = DateTime.Now };
                        using (var db = new LCMEntities())
                        {
                            db.Bilties.Attach(model);
                            db.Entry(model).Property(x => x.IsDeleted).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedBy).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedDate).IsModified = true;
                            db.SaveChanges();
                            Message = "Record Deleted Successfully";
                        }
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
            var Biltylist = context.Bilties.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_BiltyGrid.cshtml", Biltylist);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchBilty

        [HttpGet]
        public ActionResult SearchCompactBilty(CompactBiltyModel model)
        {
            string Status = "OK";
            List<Models.Bilty> lstOrders = new List<Models.Bilty>();
            if (model.DateFrom != null && model.DateTo != null && model.DeliveryDate != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.OrderNo != null)
            {
                lstOrders = CompactBilty.SearchBiltyAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), Convert.ToDateTime(model.DeliveryDate), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.OrderNo).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBRO(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBR(model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicle(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVB(model.Vehicle, model.BillTo).ToList();
            }
            else if (model.Vehicle != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVR(model.Vehicle, model.Receiver).ToList();
            }
            else if (model.Vehicle != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVO(model.Vehicle, model.OwnCompany).ToList();
            }
            else if (model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBR(model.BillTo, model.Receiver).ToList();
            }
            else if (model.BillTo != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBO(model.BillTo, model.OwnCompany).ToList();
            }
            else if (model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownRO(model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiverOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.DeliveryDate != null)
            {
                lstOrders = context.Bilties.Where(x => x.DeliveryDate == model.DeliveryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.Bilties.Where(x => x.BillTo == model.BillTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.Bilties.Where(x => x.BillTo == x.BillTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.Bilties.Where(x => x.Reciever == model.Receiver && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.Bilties.Where(x => x.Reciever == x.Reciever && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.Bilties.Where(x => x.OwnCompany == model.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.Bilties.Where(x => x.OwnCompany == x.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.Bilties.Where(x => x.VehicleID == model.Vehicle && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.Bilties.Where(x => x.VehicleID == x.VehicleID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OrderNo != null)
            {
                lstOrders = context.Bilties.Where(x => x.OrderNo == model.OrderNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            else
            {
                lstOrders = context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_BiltyGrid", lstOrders.ToList());

            return Json(new { html = view , Status = Status}, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Partial Bilty

        public ActionResult CreatePartialBilty(long? id)
        {
            var Model = new OrderViewModel();
            SetCompactBiltyDropDown();

            if (id != null && id > 0)
            {
                long _id = (long)id;

                Model.listBilty = context.Bilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                Model.listBiltyDetail = context.BiltyDetails.Where(x => x.BiltyID == _id).ToList();

                return View(Model);
            }

            else
            {

                Model.listBilty = new Models.Bilty();

                Model.listBiltyDetail = new List<BiltyDetail>();

            }

            return View(Model);
        }

        public JsonResult InsertPartialBilty(Models.Bilty Order, List<BiltyDetail> Details)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    double totalFrieght = 0;
                    foreach (var item in Details)
                    {
                        totalFrieght += item.Freight > 0 ? (double)item.Freight : 0;
                    }
                    var halfFrieght = totalFrieght / 2;
                    string[] array = { "A", "B", "C", "D", "E" };
                    for (int i = 0; i < 2; i++)
                    {
                        PartialBilty ch = new PartialBilty();
                        ch.BiltyID = Order.ID;
                        ch.OrderDate = Order.OrderDate;
                        ch.DeliveryDate = Order.DeliveryDate;
                        ch.OwnCompanyID = Order.OwnCompanyID;
                        ch.PickLocation = Order.PickLocation;
                        ch.CustomerType = Order.CustomerType;
                        ch.OrderNo = Order.OrderNo;
                        //ch.PartialNo = Order.OrderNo + "-" + array[i];
                        ch.TotalFreight = Order.TotalFreight;

                        context.PartialBilties.Add(ch);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }

                }
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PickLocation
        [HttpGet]
        public ActionResult PickLocation()
        {
            var Area = context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Name = string.Format("{0} | {1}", x.Code, x.Name) }).OrderBy(x => x.Name).ToList();
            ViewBag.Area = new SelectList(Area, "ID", "Name");
            ViewBag.City = new SelectList(context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "CityID", "CityName");
            //ViewBag.Area = new SelectList(context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.ShipmentType = new SelectList(context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            return View(context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addPickLocation(PickLocation model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.LocationCode != null || model.LocationName != null)
                {
                    model.LocationCode = model.LocationCode.ToUpper();
                    var Duplicated = context.PickLocations.Where(x => (x.LocationCode == model.LocationCode || x.LocationName == model.LocationName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.PickLocations.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<PickLocation>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.PickLocations.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_PickLocation.cshtml", context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.OwnCompanyID == ApplicationHelper.OwnCompanyID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeletePickLocation(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.PickLocations.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_PickLocation.cshtml", context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePickLocationStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.PickLocations.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_PickLocation.cshtml", context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchPickAndDropLocation(DateTime? DateTo, DateTime? DateFrom, int? City, int? Area, string Code, string LocationName)
        {
            List<PickLocation> lstOrders = new List<PickLocation>();

            if (DateFrom != null && DateTo != null)
            {
                lstOrders = dml.getSearchLocation(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstOrders = dml.getSearchLocation(Convert.ToDateTime(DateFrom), "from");
            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = dml.getSearchLocation(Convert.ToDateTime(DateTo), "to");
            }
            else if (City != null)
            {
                lstOrders = context.PickLocations.Where(x => x.CityID == City && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (Area != null)
            {
                lstOrders = context.PickLocations.Where(x => x.AreaID == Area && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (City != null)
            {
                lstOrders = context.PickLocations.Where(x => x.CityID == x.CityID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (Code != null)
            {
                lstOrders = context.PickLocations.Where(x => x.LocationCode.StartsWith(Code) && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (LocationName != null)
            {
                lstOrders = context.PickLocations.Where(x => x.LocationCode.StartsWith(LocationName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            else
            {
                lstOrders = context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_PickLocation", lstOrders.ToList());

            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Location Cascading
        public JsonResult GetLocation(int ID)
        {
            List<Area> Area = context.Areas.Where(x => x.CityID == ID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            var pList = new SelectList(Area, "ID", "Name");
            return Json(pList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Bilty PrintView
        [HttpGet]
        public ActionResult PrintViewBilty(int id)
        {
            //var Model = new OrderViewModel();
            //Model.listBilty = new Bilty();
            var Data = context.Bilties.Where(x => x.ID == id).ToList();
            return View(Data);
        }

        #endregion
        public JsonResult GetVehicle(long id)
        {
            var data = dml.getVehicle(id);
            return Json(new { ownership = data.OwnershipStatus, owner = data.OwnerName, type = data.VehicleType.Name, capacity = data.MaximumLoadingCapacity }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProduct(long id)
        {
            var data = dml.getProduct(id);
            return Json(new { unit = data.Unit, type = data.Category1.Name }, JsonRequestBehavior.AllowGet);
        }

        #region Package Type
        [HttpGet]
        public ActionResult PackageType()
        {
            ViewBag.GenerateCode = PackageTypeNo();
            return View(context.PackageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.PackageTypeID).ToList());
        }
        [HttpPost]
        public ActionResult addPackageType(PackageType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.PackageTypeCode != null && model.PackageTypeName != null)
                {
                    var Duplicated = context.PackageTypes.Where(x => (x.PackageTypeCode == model.PackageTypeCode || x.PackageTypeName == model.PackageTypeName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    model.PackageTypeCode = model.PackageTypeCode.ToUpper();
                    if (model.PackageTypeID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.PackageTypeID == model.PackageTypeID)
                            {
                                isEditable = true;
                            }
                        }
                        else
                        {
                            isEditable = true;
                        }
                        if (isEditable)
                        {
                            var record = context.PackageTypes.FirstOrDefault(x => x.PackageTypeID == model.PackageTypeID);
                            if (record != null)
                            {
                                model.DateCreated = record.DateCreated;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedByUserID = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<PackageType>().Local.FirstOrDefault(x => x.PackageTypeID == model.PackageTypeID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedByUserID = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.DateCreated = DateTime.Now;
                            context.PackageTypes.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }


                    }

                }
                else
                {
                    Message = "Code , Name and Vehicle Type is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_PackageType.cshtml", context.PackageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.PackageTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeletePackageType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.PackageTypes.Where(model => model.PackageTypeID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_PackageType.cshtml", context.PackageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.PackageTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePackageTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.PackageTypes.Where(x => x.PackageTypeID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_PackageType.cshtml", context.PackageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.PackageTypeID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Container Type
        [HttpGet]
        public ActionResult ContainerType()
        {
            return View(context.ContainerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ContainerTypeID).ToList());
        }
        [HttpPost]
        public ActionResult addContainerType(ContainerType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.ContainerTypeName != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.ContainerTypes.Where(x => (x.Code == model.Code || x.ContainerTypeName == model.ContainerTypeName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ContainerTypeID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ContainerTypeID == model.ContainerTypeID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.ContainerTypes.FirstOrDefault(x => x.ContainerTypeID == model.ContainerTypeID);
                            if (record != null)
                            {
                                model.DateCreated = record.DateCreated;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedByUserID = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<ContainerType>().Local.FirstOrDefault(x => x.ContainerTypeID == model.ContainerTypeID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedByUserID = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.DateCreated = DateTime.Now;
                            context.ContainerTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_ContainerType.cshtml", context.ContainerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult DeleteContainerType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.ContainerTypes.Where(model => model.ContainerTypeID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_ContainerType.cshtml", context.ContainerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ContainerTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeContainerTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.ContainerTypes.Where(x => x.ContainerTypeID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_ContainerType.cshtml", context.ContainerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ContainerTypeID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Container
        [HttpGet]
        public ActionResult Container()
        {
            ViewBag.ContainerType = new SelectList(context.ContainerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ContainerTypeID", "ContainerTypeName");
            var List = context.Containers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            return View(List);
        }
        [HttpPost]
        public ActionResult addContainer(Container model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Containers.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Containers.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.isActive = true;
                            var local = context.Set<Container>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.isActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Containers.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Container.cshtml", context.Containers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteContainer(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Containers.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Container.cshtml", context.Containers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeContainerStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Containers.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.isActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Container.cshtml", context.Containers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Trailer Type
        [HttpGet]
        public ActionResult TrailerType()
        {
            return View(context.TrailerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addTrailerType(TrailerType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.TrailerTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.TrailerTypes.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<TrailerType>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.TrailerTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_TrailerType.cshtml", context.TrailerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteTrailerType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.TrailerTypes.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_TrailerType.cshtml", context.TrailerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeTrailerTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.TrailerTypes.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_TrailerType.cshtml", context.TrailerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Trailer
        [HttpGet]
        public ActionResult Trailer()
        {
            ViewBag.TrailerType = new SelectList(context.TrailerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            return View(context.Trailers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addTrailer(Trailer model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Trailers.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Trailers.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Trailer>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Trailers.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Trailer.cshtml", context.Trailers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteTrailer(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Trailers.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Trailer.cshtml", context.Trailers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeTrailerStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Trailers.Where(x => x.ID == Id && x.OwnCompanyID == ApplicationHelper.OwnCompanyID).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Trailer.cshtml", context.Trailers.Where(x => x.OwnCompanyID == ApplicationHelper.OwnCompanyID).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Documents Type
        [HttpGet]
        public ActionResult DocumentType()
        {
            return View(context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DocumentTypeID).ToList());
        }
        [HttpPost]
        public ActionResult addDocumentType(DocumentType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.DocumentTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.DocumentTypeID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.DocumentTypeID == model.DocumentTypeID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.DocumentTypes.FirstOrDefault(x => x.DocumentTypeID == model.DocumentTypeID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.isActive = true;
                            var local = context.Set<DocumentType>().Local.FirstOrDefault(x => x.DocumentTypeID == model.DocumentTypeID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.isActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.DocumentTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_DocumentType.cshtml", context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DocumentTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteDocumentType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.DocumentTypes.Where(model => model.DocumentTypeID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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
            catch (Exception)
            {

                Message = $"An error occured Delete first Document";
                Status = "Exception";
            }

            string View = RenderPartialToString("~/Views/Shared/_DocumentType.cshtml", context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DocumentTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDocumentTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.DocumentTypes.Where(x => x.DocumentTypeID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.isActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_DocumentType.cshtml", context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DocumentTypeID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Document
        [HttpGet]
        public ActionResult Document()
        {
            ViewBag.DocumentType = new SelectList(context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "DocumentTypeID", "Name");
            return View(context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addDocument(Document model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Documents.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Documents.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Document>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Documents.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Document.cshtml", context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteDocument(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Documents.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Document.cshtml", context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDocumentStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Documents.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Document.cshtml", context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Product Nature
        [HttpGet]
        public ActionResult ProductNature()
        {
            return View(context.Natures.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addProductNature(Models.Nature model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.Natures.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Natures.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Models.Nature>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Natures.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_ProductNature.cshtml", context.Natures.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteProductNature(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Natures.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_ProductNature.cshtml", context.Natures.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeProductNatureStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Natures.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_ProductNature.cshtml", context.Natures.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Driver Type
        [HttpGet]
        public ActionResult DriverType()
        {
            return View(context.DriverTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addDriverType(DriverType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.DriverTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.DriverTypes.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<DriverType>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.DriverTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }


            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_DriverType.cshtml", context.DriverTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteDriverType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.DriverTypes.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_DriverType.cshtml", context.DriverTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDriverTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.DriverTypes.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_DriverType.cshtml", context.DriverTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region General Taxes
        public ActionResult GeneralTaxes()
        {
            ViewBag.Province = new SelectList(context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ProvinceID", "ProvinceName");
            return View(context.TaxRateRegistrations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public JsonResult InsertGeneralTax(TaxRateRegistration TRR)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (TRR != null)
                {
                    context.TaxRateRegistrations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID));
                    TRR.TaxRateName = TRR.TaxRateName.ToUpper();
                    if (TRR.ID > 0)
                    {
                        TRR.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        TRR.ModifiedBy = ApplicationHelper.UserID;
                        TRR.ModifiedDate = DateTime.Now;
                        TRR.IsActive = true;
                        context.Entry(TRR).State = EntityState.Modified;
                    }
                    else
                    {
                        TRR.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        TRR.CreatedBy = ApplicationHelper.UserID;
                        TRR.IsActive = true;
                        TRR.CreatedDate = DateTime.Now;
                        context.TaxRateRegistrations.Add(TRR);
                    }
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<TaxRateRegistration> data = context.TaxRateRegistrations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_GeneralTaxes.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteGeneralTaxes(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.TaxRateRegistrations.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_GeneralTaxes.cshtml", context.TaxRateRegistrations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeGeneralTaxStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.TaxRateRegistrations.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_GeneralTaxes.cshtml", context.TaxRateRegistrations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Tax For Transportation
        public ActionResult TaxForTransportation()
        {
            ViewBag.Province = new SelectList(context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ProvinceID", "ProvinceName");
            ViewBag.ProvinceDestination = new SelectList(context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ProvinceID", "ProvinceName");
            return View(context.TaxRateTransportations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public JsonResult InsertTaxForTransportation(TaxRateTransportation model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model != null)
                {
                    context.TaxRateTransportations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID));
                    model.Name = model.Name.ToUpper();
                    if (model.ID > 0)
                    {
                        model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        model.ModifiedBy = ApplicationHelper.UserID;
                        model.ModifiedDate = DateTime.Now;
                        model.IsActive = true;
                        context.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        model.CreatedBy = ApplicationHelper.UserID;
                        model.IsActive = true;
                        model.CreatedDate = DateTime.Now;
                        context.TaxRateTransportations.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<TaxRateTransportation> data = context.TaxRateTransportations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_TaxForTransportation.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteTaxForTransportation(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.TaxRateTransportations.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_TaxForTransportation.cshtml", context.TaxRateTransportations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeTaxForTransportStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.TaxRateTransportations.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_TaxForTransportation.cshtml", context.TaxRateTransportations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Own Group Department
        public ActionResult Department()
        {
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList(), "ID", "Name");
            return View(context.Departments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DepartID).ToList());

        }
        [HttpPost]
        public ActionResult AddDepartment(Department department)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (department.DepartCode != null || department.DepartName != null)
                {
                    department.DepartCode = department.DepartCode.ToUpper();
                    var Duplicated = context.Departments.Where(x => (x.DepartCode == department.DepartCode || x.DepartName == department.DepartName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (department.DepartID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.DepartID == department.DepartID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Departments.FirstOrDefault(x => x.DepartID == department.DepartID);
                            if (record != null)
                            {
                                department.DateCreated = record.DateCreated;
                            }
                            department.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            department.ModifiedByUser = ApplicationHelper.UserID;
                            department.DateModified = DateTime.Now;
                            department.IsActive = true;
                            var local = context.Set<Department>().Local.FirstOrDefault(x => x.DepartID == department.DepartID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(department).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            department.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            department.CreatedByUserID = ApplicationHelper.UserID;
                            department.IsActive = true;
                            department.DateCreated = DateTime.Now;
                            context.Departments.Add(department);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<Department> data = context.Departments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_OwnGroupDepartmentList.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteOwnGroupDepartment(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Departments.Where(model => model.DepartID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_OwnGroupDepartmentList.cshtml", context.Departments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DepartID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDepartmentStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Departments.Where(x => x.DepartID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_OwnGroupDepartmentList.cshtml", context.Departments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DepartID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Own Group Designation
        public ActionResult Designation()
        {
            return View(context.Designations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DesignationID).ToList());

        }
        [HttpPost]
        public ActionResult AddDesignation(Designation designation)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (designation.DesignationCode != null || designation.DesignationName != null)
                {
                    designation.DesignationCode = designation.DesignationCode.ToUpper();
                    var Duplicated = context.Designations.Where(x => (x.DesignationCode == designation.DesignationCode || x.DesignationName == designation.DesignationName) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (designation.DesignationID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.DesignationID == designation.DesignationID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Designations.FirstOrDefault(x => x.DesignationID == designation.DesignationID);
                            if (record != null)
                            {
                                designation.CreatedDate = record.CreatedDate;
                            }
                            designation.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            designation.ModifiedBy = ApplicationHelper.UserID;
                            designation.ModifiedDate = DateTime.Now;
                            designation.IsActive = true;
                            var local = context.Set<Designation>().Local.FirstOrDefault(x => x.DesignationID == designation.DesignationID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(designation).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            designation.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            designation.CreatedBy = ApplicationHelper.UserID;
                            designation.IsActive = true;
                            designation.CreatedDate = DateTime.Now;
                            context.Designations.Add(designation);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<Designation> data = context.Designations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_OwnGroupDesignationList.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteOwnGroupDesignation(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Designations.Where(model => model.DesignationID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_OwnGroupDesignationList.cshtml", context.Designations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DesignationID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDesignationStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Designations.Where(x => x.DesignationID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_OwnGroupDesignationList.cshtml", context.Designations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.DesignationID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Employees

        [HttpGet]
        public ActionResult Employee()
        {
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList(), "ID", "Name");
            ViewBag.OwnGroup = new SelectList(context.OwnGroups.ToList(), "GroupID", "Name");
            ViewBag.Designation = new SelectList(context.Designations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "DesignationId", "DesignationName");
            ViewBag.Department = new SelectList(context.Departments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "DepartID", "DepartName");
            return View(context.Employees.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.EmployeeID).ToList());
        }
        [HttpPost]
        public ActionResult addEmployee(Employee model, HttpPostedFileBase file, HttpPostedFileBase NIC)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.EmployeeCode != null && model.EmployeeName != null && model.FatherName != null
                     && model.DateOfBirth != null && model.Gender != null && model.ContactNo != null && model.CNIC != null
                     && model.NICExpiryDate != null && model.NICIssueDate != null)
                {


                    var Duplicated = context.Employees.Where(x => (x.EmployeeCode == model.EmployeeCode || x.EmployeeName == model.EmployeeName
                   || x.ContactNo == model.ContactNo || x.CNIC == model.CNIC) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    model.EmployeeCode = model.EmployeeCode.ToUpper();


                    if (file != null)
                    {
                        model.UploadDocument = ApplicationHelper.UploadFile(file, ApplicationHelper.EmployeeDocuments, Server);
                    }
                    if (file != null)
                    {
                        model.CNICImagePath = ApplicationHelper.UploadFile(NIC, ApplicationHelper.EmployeeNICDocuments, Server);
                    }

                    if (model.EmployeeID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.EmployeeID == model.EmployeeID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.Employees.FirstOrDefault(x => x.EmployeeID == model.EmployeeID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<Employee>().Local.FirstOrDefault(x => x.EmployeeID == model.EmployeeID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Employees.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Employee.cshtml", context.Employees.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.EmployeeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteEmployee(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Employees.Where(model => model.EmployeeID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Employee.cshtml", context.Employees.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.EmployeeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeEmployeeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Employees.Where(x => x.EmployeeID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_Employee.cshtml", context.Employees.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.EmployeeID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Employee Cascading
        public JsonResult GetDepartment(int ID)
        {
            List<Department> Department = context.Departments.Where(x => x.OwnCompany.GroupID == ID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            var pList = new SelectList(Department, "DepartID", "DepartName");
            return Json(pList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOwnCompany(int ID)
        {
            List<OwnCompany> OwnCompany = context.OwnCompanies.Where(x => x.GroupID == ID && x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();
            var pList = new SelectList(OwnCompany, "ID", "Name");
            return Json(pList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOwnGroup(long id)
        {
            var data = dml.getOwnCompany(id);
            return Json(new { Group = data.OwnGroup.Name }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Customer Department
        public ActionResult CustomerDepartment()
        {
            ViewBag.CustomerDepartment = new SelectList(context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "Name");
            ViewBag.CustomerCompany = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            return View(context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult AddCustomerDepartment(CustomerDepartment department)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (department.Code != null || department.Name != null)
                {
                    department.Code = department.Code.ToUpper();
                    var Duplicated = context.CustomerDepartments.Where(x => (x.Code == department.Code || x.Name == department.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (department.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == department.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.CustomerDepartments.FirstOrDefault(x => x.ID == department.ID);
                            if (record != null)
                            {
                                department.DateCreated = record.DateCreated;
                            }
                            department.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            department.ModifiedByUser = ApplicationHelper.UserID;
                            department.DateModified = DateTime.Now;
                            department.IsActive = true;
                            var local = context.Set<CustomerDepartment>().Local.FirstOrDefault(x => x.ID == department.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(department).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            department.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            department.CreatedByUserID = ApplicationHelper.UserID;
                            department.IsActive = true;
                            department.DateCreated = DateTime.Now;
                            context.CustomerDepartments.Add(department);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_CustomerDepartment.cshtml", context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerDepartment(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerDepartments.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_CustomerDepartment.cshtml", context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeCustomerDepartmentStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.CustomerDepartments.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_CustomerDepartment.cshtml", context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Shipment Type
        public ActionResult ShipmentType()
        {
            return View(context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());

        }
        [HttpPost]
        public ActionResult ShipmentType(ShipmentType shipmenttype)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (shipmenttype.Code != null || shipmenttype.Name != null)
                {
                    shipmenttype.Code = shipmenttype.Code.ToUpper();
                    var Duplicated = context.ShipmentTypes.Where(x => (x.Code == shipmenttype.Code || x.Name == shipmenttype.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (shipmenttype.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == shipmenttype.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.ShipmentTypes.FirstOrDefault(x => x.ID == shipmenttype.ID);
                            if (record != null)
                            {
                                shipmenttype.CreatedDate = record.CreatedDate;
                            }
                            shipmenttype.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            shipmenttype.ModifiedBy = ApplicationHelper.UserID;
                            shipmenttype.ModifiedDate = DateTime.Now;
                            shipmenttype.IsActive = true;
                            var local = context.Set<ShipmentType>().Local.FirstOrDefault(x => x.ID == shipmenttype.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(shipmenttype).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            shipmenttype.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            shipmenttype.CreatedBy = ApplicationHelper.UserID;
                            shipmenttype.IsActive = true;
                            shipmenttype.CreatedDate = DateTime.Now;
                            context.ShipmentTypes.Add(shipmenttype);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<ShipmentType> data = context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_ShipmentType.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteShipmentType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.ShipmentTypes.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_ShipmentType.cshtml", context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeShipmentTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.ShipmentTypes.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_ShipmentType.cshtml", context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult UniversalBilty()
        {
            return View();
        }

        #region Vehicle Documents
        [HttpGet]
        public ActionResult VehicleDocuments(string auth)
        {
            ViewBag.VehicleRegistration = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "VehicleID", "RegNo");
            ViewBag.VehicleType = new SelectList(context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.VehicleOwnership = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "VehicleID", "OwnershipStatus");
            ViewBag.PrincipleCompany = new SelectList(context.PrincipleCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            ViewBag.VehicleDocumentType = new SelectList(context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "DocumentTypeID", "Name");
            ViewBag.VehicleDocuments = new SelectList(context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            return View(context.VehicleDocuments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addVehicleDocuments(VehicleDocument model, HttpPostedFileBase file)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.VehicleRegNo != null && model.VehicleDocumentType != null && model.VehicleDocuments != null)
                {
                    context.VehicleDocuments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID));
                    if (file != null)
                    {
                        model.DocumentUpload = ApplicationHelper.UploadFile(file, ApplicationHelper.FleetVehicleDocument, Server);
                    }
                    if (model.ID > 0)
                    {
                        var record = context.VehicleDocuments.FirstOrDefault(x => x.ID == model.ID);
                        if (record != null)
                        {
                            model.CreatedDate = record.CreatedDate;
                        }
                        model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        model.ModifiedBy = ApplicationHelper.UserID;
                        model.ModifiedDate = DateTime.Now;
                        model.IsActive = true;
                        var local = context.Set<VehicleDocument>().Local.FirstOrDefault(x => x.ID == model.ID);
                        if (local != null)
                        {
                            context.Entry(local).State = EntityState.Detached;
                        }
                        context.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        model.CreatedBy = ApplicationHelper.UserID;
                        model.CreatedDate = DateTime.Now;
                        model.IsActive = true;
                        context.VehicleDocuments.Add(model);
                    }

                }
                else
                {
                    Message = "Vehicle Reg No & Vehicle Document Type is required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_VehicleDocuments.cshtml", context.VehicleDocuments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVehicleDocuments(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.VehicleDocuments.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_VehicleDocuments.cshtml", context.VehicleDocuments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ChangeVehicleDocumentsStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.VehicleDocuments.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_VehicleDocuments.cshtml", context.VehicleDocuments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Vehicle Documents Cascading
        public JsonResult GetVehicleReg(int ID)
        {
            List<Vehicle> Vehicle = context.Vehicles.Where(x => x.VehicleType.ID == ID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            var pList = new SelectList(Vehicle, "VehicleID", "RegNo");
            return Json(pList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Maintenance Type
        [HttpGet]
        public ActionResult MaintenanceType()
        {
            return View(context.MaintenanceTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addMaintenanceType(MaintenanceType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.MaintenanceTypes.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            var record = context.MaintenanceTypes.FirstOrDefault(x => x.ID == model.ID);
                            if (record != null)
                            {
                                model.CreatedDate = record.CreatedDate;
                            }
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<MaintenanceType>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.MaintenanceTypes.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }



                    }



                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_MaintenanceType.cshtml", context.MaintenanceTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteMaintenance(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.MaintenanceTypes.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_MaintenanceType.cshtml", context.MaintenanceTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeMaintenanceTypeStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.MaintenanceTypes.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_MaintenanceType.cshtml", context.MaintenanceTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Vehicle Maintenance
        public ActionResult VehicleMaintenance()
        {
            ViewBag.VehRegNo = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "VehicleID", "RegNo");
            ViewBag.VehicleType = new SelectList(context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "ID", "Name");
            ViewBag.ownership = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "VehicleID", "OwnershipStatus");
            ViewBag.principlecompany = new SelectList(context.PrincipleCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "Name");
            ViewBag.MaintenanceType = new SelectList(context.MaintenanceTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "Name");

            return View(context.VehicleMaintenances.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public ActionResult addVehicleMaintenance(VehicleMaintenance model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.VehicleRegNo != null)
                {
                    context.VehicleMaintenances.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID));
                    if (model.ID > 0)
                    {
                        var record = context.VehicleMaintenances.FirstOrDefault(x => x.ID == model.ID);
                        if (record != null)
                        {
                            model.CreatedDate = record.CreatedDate;
                        }
                        model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        model.ModifiedBy = ApplicationHelper.UserID;
                        model.ModifiedDate = DateTime.Now;
                        model.IsActive = true;
                        context.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        model.CreatedBy = ApplicationHelper.UserID;
                        model.IsActive = true;
                        model.CreatedDate = DateTime.Now;
                        context.VehicleMaintenances.Add(model);
                    }

                }
                else
                {
                    Message = "Vehicle Reg No is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_VehicleMaintenance.cshtml", context.VehicleMaintenances.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteVehicleMaintenance(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.VehicleMaintenances.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_VehicleMaintenance.cshtml", context.VehicleMaintenances.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeVehicleMaintenanceStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.VehicleMaintenances.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_VehicleMaintenance.cshtml", context.VehicleMaintenances.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Vehicle Maintenance Cascading
        public JsonResult GetVehicleRegistration(int ID)
        {
            List<Vehicle> Vehicle = context.Vehicles.Where(x => x.VehicleType.ID == ID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            var pList = new SelectList(Vehicle, "VehicleID", "RegNo");
            return Json(pList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Universal Bilty

        public void UniversalDropdown()
        {
            var OwnCompany = context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.OwnCompany = new SelectList(OwnCompany, "ID", "Code");

            var PickLocation = context.PickLocations.Where(x => (x.LocationType == "Pick" || x.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationName = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.PickLocation = new SelectList(PickLocation, "ID", "LocationName");

            var DropLocation = context.PickLocations.Where(x => (x.LocationType == "Drop" || x.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationName = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.DropLocation = new SelectList(DropLocation, "ID", "LocationName");

            var City = context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.CityID, CityName = string.Format("{0} | {1}", x.CityCode, x.CityName) }).ToList();
            ViewBag.City = new SelectList(City, "ID", "CityName");

            var Area = context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Name = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Area = new SelectList(Area, "ID", "Name");

            var CustomerCode = context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.CustomerCode = new SelectList(CustomerCode, "ID", "Code");

            var ShipmentType = context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.ShipmentType = new SelectList(ShipmentType, "ID", "Code");

            var VehicleCode = context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.VehicleCode = new SelectList(VehicleCode, "ID", "Code");

            var ContainerCode = context.ContainerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ContainerTypeID, Code = string.Format("{0} | {1}", x.Code, x.ContainerTypeName) }).ToList();
            ViewBag.ContainerCode = new SelectList(ContainerCode, "ID", "Code");

            var Product = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Product = new SelectList(Product, "ID", "Code");

            var DamageItem = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.DamageItem = new SelectList(DamageItem, "ID", "Code");

            var DamageType = context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.DamageTypeID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.DamageType = new SelectList(DamageType, "ID", "Code");

            var DocumentType = context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.DocumentTypeID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.DocumentType = new SelectList(DocumentType, "ID", "Code");

            var Expense = context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Expense = new SelectList(Expense, "ID", "Code");

            var DispatchDocument = context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.DispatchDocument = new SelectList(DispatchDocument, "ID", "Code");

            var ReceivedBy = context.Employees.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.EmployeeID, Code = string.Format("{0} | {1}", x.EmployeeCode, x.EmployeeName) }).ToList();
            ViewBag.ReceivedBy = new SelectList(ReceivedBy, "ID", "Code");
        }

        public ActionResult CreateUniversalBilty(string id)
        {
            var Model = new OrderViewModel();
            Model.UniversalBilty = new UniversalBilty();

            UniversalDropdown();

            ViewBag.Id = 0;

            if (id != null && id != string.Empty)
            {
                long _id = Convert.ToInt64(id);

                Model.UniversalBilty = context.UniversalBilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                Model.lstCustomerinformation = context.UniversalBiltyCustomerInformations.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstContainerInformation = context.UniversalBiltyContainerInformations.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstLocationInformation = context.UniversalBiltyLocationInformations.Where(x => x.UniversalBiltyID == _id).FirstOrDefault();
                Model.lstProductInformation = context.UniversalBiltyProductInformations.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstVehicleInformation = context.UniversalBiltyVehicleInformations.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstDispatchInformation = context.UniversalBiltyDispatchDocuments.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstReceivingInformation = context.UniversalBiltyReceivingInformations.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstReceivingDocumentInformation = context.UniversalBiltyReceivingDocumentDetails.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstDamageDetail = context.UniversalBiltyDamageDetails.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstExpenseDetail = context.UniversalBiltyExpenseDetails.Where(x => x.UniversalBiltyID == _id).ToList();
                Model.lstLoadingLocation = context.UniversalBiltyVehicleLoadingReportings.Where(x => x.UniversalBiltyID == _id).FirstOrDefault();
                Model.lstUnLoadingLocation = context.UniversalBiltyVehicleUnLoadingReportings.Where(x => x.UniversalBiltyID == _id).FirstOrDefault();
                ViewBag.Id = _id;
            }

            else
            {
                ViewBag.BiltyNo = GetUniversalBiltyNo();

                Model.UniversalBilty = new UniversalBilty();
                Model.lstCustomerinformation = new List<UniversalBiltyCustomerInformation>();
                Model.lstVehicleInformation = new List<UniversalBiltyVehicleInformation>();
                Model.lstProductInformation = new List<UniversalBiltyProductInformation>();
                Model.lstContainerInformation = new List<UniversalBiltyContainerInformation>();
                Model.lstDispatchInformation = new List<UniversalBiltyDispatchDocument>();
                Model.lstReceivingInformation = new List<UniversalBiltyReceivingInformation>();
                Model.lstReceivingDocumentInformation = new List<UniversalBiltyReceivingDocumentDetail>();
                Model.lstLocationInformation = new UniversalBiltyLocationInformation();
                Model.lstDamageDetail = new List<UniversalBiltyDamageDetail>();
                Model.lstExpenseDetail = new List<UniversalBiltyExpenseDetail>();
                Model.lstLoadingLocation = new UniversalBiltyVehicleLoadingReporting();
                Model.lstUnLoadingLocation = new UniversalBiltyVehicleUnLoadingReporting();
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult InsertUniversalBilty(
            List<UniversalBiltyCustomerInformation> Customer,
            UniversalBiltyLocationInformation Location,
            UniversalBilty Universal,
            List<UniversalBiltyVehicleInformation> Vehicle,
            List<UniversalBiltyContainerInformation> Container,
            List<UniversalBiltyProductInformation> Product,
            List<UniversalBiltyDispatchDocument> DispatchDocument,
            List<UniversalBiltyReceivingInformation> ReceivingInformation,
            List<UniversalBiltyReceivingDocumentDetail> ReceivingDocument,
            List<UniversalBiltyDamageDetail> DamageDetail,
            List<UniversalBiltyExpenseDetail> ExpenseDetail,
            UniversalBiltyVehicleLoadingReporting LoadingDetail,
            UniversalBiltyVehicleUnLoadingReporting UnLoadingDetail,
            List<UniversalVehicleDetail> VehicleDetails,
            List<UniversalContainerDetail> ContainerDetails,
            List<UniversalBiltyFreightDetail> FreightDetail
            )
        {
            string message = "";
            string status = "OK";

            try
            {
                long OrderID = 0;

                if (Universal != null)
                {
                    if (Universal.ID > 0)
                    {
                        var record = context.UniversalBilties.FirstOrDefault(x => x.ID == Universal.ID);
                        if (record != null)
                        {
                            Universal.AutoBiltyNo = record.AutoBiltyNo;
                            Universal.CreatedDate = record.CreatedDate;
                        }
                        //if (Duplicated.Count <= 1)
                        //{
                        context.UniversalBiltyCustomerInformations.RemoveRange(context.UniversalBiltyCustomerInformations.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyProductInformations.RemoveRange(context.UniversalBiltyProductInformations.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyVehicleInformations.RemoveRange(context.UniversalBiltyVehicleInformations.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyLocationInformations.RemoveRange(context.UniversalBiltyLocationInformations.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyContainerInformations.RemoveRange(context.UniversalBiltyContainerInformations.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyDispatchDocuments.RemoveRange(context.UniversalBiltyDispatchDocuments.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyReceivingInformations.RemoveRange(context.UniversalBiltyReceivingInformations.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyReceivingDocumentDetails.RemoveRange(context.UniversalBiltyReceivingDocumentDetails.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyDamageDetails.RemoveRange(context.UniversalBiltyDamageDetails.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyExpenseDetails.RemoveRange(context.UniversalBiltyExpenseDetails.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyVehicleLoadingReportings.RemoveRange(context.UniversalBiltyVehicleLoadingReportings.Where(x => x.UniversalBiltyID == Universal.ID).ToList());
                        context.UniversalBiltyVehicleUnLoadingReportings.RemoveRange(context.UniversalBiltyVehicleUnLoadingReportings.Where(x => x.UniversalBiltyID == Universal.ID).ToList());


                        Universal.IsActive = true;
                        Universal.ModifiedBy = ApplicationHelper.UserID;
                        Universal.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        Universal.ModifiedDate = DateTime.Now;
                        var local = context.Set<LiquadCargoManagment.Models.UniversalBilty>().Local.FirstOrDefault(x => x.ID == x.ID);
                        if (local != null)
                        {
                            context.Entry(local).State = EntityState.Detached;
                        }
                        context.Entry(Universal).State = EntityState.Modified;
                        OrderID = Universal.ID;
                        //}
                        //else
                        //{
                        //    message = "Manual Bilty No Already Exist";
                        //    status = "Duplicate";
                        //}
                    }
                    else
                    {
                        Universal.AutoBiltyNo = GetUniversalBiltyNo();
                        Universal.IsActive = true;
                        Universal.CreatedBy = ApplicationHelper.UserID;
                        Universal.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        Universal.CreatedDate = DateTime.Now;
                        OrderID = Universal.ID;
                        context.UniversalBilties.Add(Universal);
                    }
                    if (VehicleDetails != null)
                    {
                        foreach (UniversalVehicleDetail item in VehicleDetails)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalVehicleDetails.Add(item);
                        }
                    }
                    if (ContainerDetails != null)
                    {
                        foreach (UniversalContainerDetail item in ContainerDetails)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalContainerDetails.Add(item);
                        }
                    }
                    if (Customer != null)
                    {
                        foreach (UniversalBiltyCustomerInformation item in Customer)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyCustomerInformations.Add(item);
                        }
                    }

                    if (Location != null)
                    {
                        if (Location.LocalPickStatus == true && Location.LocalDropStatus == true)
                        {
                            if (Location.LocalPickStatus == true && Location.LocalDropStatus != true)
                            {
                                Location.LocalPickStatus = true;
                            }
                            else
                            {
                                Location.LocalDropStatus = true;
                            }
                            Location.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyLocationInformations.Add(Location);
                        }
                        else
                        {
                            Location.UniversalBiltyID = Universal.ID;
                            Location.LocalPickStatus = false;
                            Location.LocalDropStatus = false;
                            context.UniversalBiltyLocationInformations.Add(Location);
                        }
                    }

                    if (LoadingDetail != null)
                    {
                        context.UniversalBiltyVehicleLoadingReportings.Add(LoadingDetail);
                        LoadingDetail.UniversalBiltyID = Universal.ID;
                    }
                    if (UnLoadingDetail != null)
                    {
                        UnLoadingDetail.UniversalBiltyID = Universal.ID;
                        context.UniversalBiltyVehicleUnLoadingReportings.Add(UnLoadingDetail);
                    }
                    if (Vehicle != null)
                    {
                        foreach (UniversalBiltyVehicleInformation item in Vehicle)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyVehicleInformations.Add(item);
                        }
                    }
                    if (Container != null)
                    {
                        foreach (UniversalBiltyContainerInformation item in Container)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyContainerInformations.Add(item);
                        }
                    }
                    if (Product != null)
                    {
                        foreach (UniversalBiltyProductInformation item in Product)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyProductInformations.Add(item);
                        }
                    }
                    if (DispatchDocument != null)
                    {
                        foreach (UniversalBiltyDispatchDocument item in DispatchDocument)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyDispatchDocuments.Add(item);
                        }
                    }
                    if (ReceivingInformation != null)
                    {
                        foreach (UniversalBiltyReceivingInformation item in ReceivingInformation)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyReceivingInformations.Add(item);
                        }
                    }
                    if (ReceivingDocument != null)
                    {
                        foreach (UniversalBiltyReceivingDocumentDetail item in ReceivingDocument)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyReceivingDocumentDetails.Add(item);
                        }
                    }
                    if (DamageDetail != null)
                    {
                        foreach (UniversalBiltyDamageDetail item in DamageDetail)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyDamageDetails.Add(item);
                        }
                    }
                    if (ExpenseDetail != null)
                    {
                        foreach (UniversalBiltyExpenseDetail item in ExpenseDetail)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyExpenseDetails.Add(item);
                        }
                    }
                    if (FreightDetail != null)
                    {
                        foreach (UniversalBiltyFreightDetail item in FreightDetail)
                        {
                            item.UniversalBiltyID = Universal.ID;
                            context.UniversalBiltyFreightDetails.Add(item);
                        }
                    }
                }
                context.SaveChanges();
                message = "Bilty has been created";
            }
            catch (Exception ex)
            {
                status = ex.GetType().Name;
                message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += " Inner Exception: " + ex.InnerException.Message;
                }
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UniversalBiltyList()
        {
            ViewBag.BillTo = new SelectList(context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            ViewBag.Vehicle = new SelectList(context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).ToList(), "ID", "Name");
            var list = context.UniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            return View(list);
        }
        //[HttpGet]
        //public JsonResult SearchvehicleBilty(string _value)
        //{
        //    var list = context.Vehicles.Where(x => x.Code.StartsWith(_value)).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult DeleteUniversalBilty(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.UniversalBilties.Where(x => x.ID == id && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new UniversalBilty() { ID = id, IsDeleted = true, DeletedBy = ApplicationHelper.UserID, DeletedDate = DateTime.Now };
                        using (var db = new LCMEntities())
                        {
                            db.UniversalBilties.Attach(model);
                            db.Entry(model).Property(x => x.IsDeleted).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedBy).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedDate).IsModified = true;
                            db.SaveChanges();
                            Message = "Record Deleted Successfully";
                        }
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
            var Biltylist = context.UniversalBilties.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_UniversalBiltyList.cshtml", Biltylist);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UniversalBiltyManualNo(string data)
        {
            string Status = "OK";
            string Message = "";
            try
            {
                if (data != null)
                {
                    var row = context.UniversalBilties.Where(x => x.ManualBiltyNo == data).FirstOrDefault();
                    if (row != null)
                    {
                        Message = "Already Exist";
                        Status = "Duplicated";
                        //Message = "No Record";
                    }
                    else
                    {
                        Status = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "An error occured" + ex;
            }

            return Json(new { Message = Message, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Universal Cascade
        public JsonResult GetArea(long id)
        {
            var data = dml.getPickLocation(id);
            return Json(new { City = data.City.CityName, Area = data.Area.Name, LocationName = data.LocationName, Address = data.Address }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUniversalLocation(long id)
        {
            var data = dml.getPickLocation(id);
            return Json(new { City = data.City.CityName, Address = data.Address }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAreas(long id)
        {
            var data = dml.getPickLocation(id);
            return Json(new { Area = data.Area.Name }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomer(long id)
        {
            var data = dml.getCustomerDepart(id);
            return Json(new { CompanyName = data.CustomerCompany.Name, Department = data.Name }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerClient(long id)
        {
            var data = dml.getCustomerDepart(id);
            return Json(new { CustomerName = data.CustomerCompany.Name, DepartName = data.Name }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVehicleType(long id)
        {
            var data = dml.getVehicle(id);
            return Json(new { VehicleType = data.VehicleType.Name }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContainerType(long id)
        {
            var data = dml.getContainer(id);
            return Json(new { ContainerType = data.ContainerType.ContainerTypeName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductCascade(long id)
        {
            var data = dml.getProduct(id);
            return Json(new { ProductName = data.Name, PackageType = data.PackageType.PackageTypeName, ProductNature = data.Nature.Name }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDispatchDocument(long id)
        {
            var data = dml.getDocument(id);
            return Json(new { DocumentType = data.DocumentType.Name }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchUniversalBilty

        [HttpGet]
        public ActionResult SearchUniversalBilty(UniversalBiltyModel model)
        {
            string Status = "OK";
            List<UniversalBilty> lstOrders = new List<UniversalBilty>();
            if (model.DateFrom != null && model.DateTo != null && model.DeliveryDate != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.OrderNo != null)
            {
                lstOrders = Universal.SearchBiltyAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), Convert.ToDateTime(model.DeliveryDate), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.OrderNo).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDropDownVBRO(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = Universal.SearchBiltyDropDownVBR(model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null)
            {
                lstOrders = Universal.SearchBiltyDateVehicle(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = Universal.SearchBiltyDropDownVB(model.Vehicle, model.BillTo).ToList();
            }
            else if (model.Vehicle != null && model.Receiver != null)
            {
                lstOrders = Universal.SearchBiltyDropDownVR(model.Vehicle, model.Receiver).ToList();
            }
            else if (model.Vehicle != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDropDownVO(model.Vehicle, model.OwnCompany).ToList();
            }
            else if (model.BillTo != null && model.Receiver != null)
            {
                lstOrders = Universal.SearchBiltyDropDownBR(model.BillTo, model.Receiver).ToList();
            }
            else if (model.BillTo != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDropDownBO(model.BillTo, model.OwnCompany).ToList();
            }
            else if (model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDropDownRO(model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.BillTo != null)
            {
                lstOrders = Universal.SearchBiltyDateBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Receiver != null)
            {
                lstOrders = Universal.SearchBiltyDateReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDateOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = Universal.SearchBiltyDateVehicleBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = Universal.SearchBiltyDateVehicleBillToReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDateVehicleBillToReceiverOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.DeliveryDate != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.DeliveryDate == model.DeliveryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == model.BillTo).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == s.ClientCode).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerReceiverCustomerCode == model.Receiver).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerReceiverCustomerCode == s.ConsignerReceiverCustomerCode).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.OwnCompanyID == model.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.OwnCompanyID == model.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == model.Vehicle).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == s.VehicleID).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OrderNo != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.ManualBiltyNo == model.OrderNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                lstOrders = context.UniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_UniversalBiltyList", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult SearchUniversalBilty(DateTime? DateTo, DateTime? DateFrom, DateTime? DeliveryDate, int? BillTo, int? Sender, int? OrderNo, int? PickLocation)
        //{
        //    List<UniversalBilty> lstOrders = new List<UniversalBilty>();

        //    if (DateFrom != null && DateTo != null)
        //    {
        //        lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
        //    }
        //    else if (DateFrom != null && DateTo == null)
        //    {
        //        lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(DateFrom), "from");
        //    }
        //    else if (DateFrom == null && DateTo != null)
        //    {
        //        lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(DateTo), "to");
        //    }
        //    else if (DeliveryDate != null)
        //    {
        //        lstOrders = context.UniversalBilties.Where(x => x.DeliveryDate == DeliveryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //    }
        //    else if (PickLocation != null)
        //    {
        //        lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyLocationInformations.Where(o => o.LocationCode == PickLocation).ToList().Count > 0).ToList();
        //    }

        //    else
        //    {
        //        lstOrders = context.UniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //    }

        //    string view = RenderPartialToString("_UniversalBiltyList", lstOrders.ToList());
        //    return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region Challan 
        [HttpGet]
        public ActionResult Challan()
        {
            ChallanView();
            var bilty = context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            ViewBag.ChallanNo = GetNewChallanNo();
            return View(bilty);
        }

        public ActionResult SearchChallanBilty(CompactBiltyModel model)
        {
            string Status = "OK";
            List<Models.Bilty> lstOrders = new List<Models.Bilty>();
            if (model.DateFrom != null && model.DateTo != null && model.Sender != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.OrderNo != null)
            {
                lstOrders = CompactBilty.SearchBiltyAllFilters(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.OrderNo, model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBROS(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBRO(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBR(model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicle(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateSender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVB(model.Vehicle, model.BillTo).ToList();
            }
            else if (model.Vehicle != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVR(model.Vehicle, model.Receiver).ToList();
            }
            else if (model.Vehicle != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVO(model.Vehicle, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVS(model.Vehicle, model.Sender).ToList();
            }
            else if (model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBR(model.BillTo, model.Receiver).ToList();
            }
            else if (model.BillTo != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBO(model.BillTo, model.OwnCompany).ToList();
            }
            else if (model.BillTo != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBS(model.BillTo, model.Sender).ToList();
            }
            else if (model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownRO(model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Receiver != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownRS(model.Receiver, model.Sender).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiverOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiverOwnCompanySender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.Bilties.Where(x => x.BillTo == model.BillTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.Bilties.Where(x => x.BillTo == x.BillTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.Bilties.Where(x => x.Reciever == model.Receiver && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.Bilties.Where(x => x.PickLocation == x.PickLocation && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Sender != null)
            {
                lstOrders = context.Bilties.Where(x => x.PickLocation == model.Sender && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Sender != null)
            {
                lstOrders = context.Bilties.Where(x => x.Reciever == x.Reciever && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.Bilties.Where(x => x.OwnCompany == model.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.Bilties.Where(x => x.OwnCompany == x.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.Bilties.Where(x => x.VehicleID == model.Vehicle && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.Bilties.Where(x => x.VehicleID == x.VehicleID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OrderNo != null)
            {
                lstOrders = context.Bilties.Where(x => x.OrderNo == model.OrderNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            else
            {
                lstOrders = context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_SearchChallan", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPickLocationChallan(long id)
        {
            var data = dml.getChallanPickLocation(id);
            return Json(new { City = data.PickLocation1.City.CityName, Area = data.PickLocation1.Area.Name, Address = data.PickLocation1.Address }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDropLocationChallan(long id)
        {
            var data = dml.getChallanDropLocation(id);
            return Json(new { City = data.PickLocation2.City.CityName, Area = data.PickLocation2.Area.Name, Address = data.PickLocation2.Address }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVehicleChallan(long id)
        {
            var data = dml.getVehicle(id);
            return Json(new { Reg = data.RegNo, Type = data.VehicleType.Name }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDriverChallan(long id)
        {
            var data = dml.getDriver(id);
            return Json(new { Contact = data.CellNo }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVendorChallan(long id)
        {
            var data = dml.getVendor(id);
            return Json(new { Name = data.OwnerContactName, Contact = data.OwnerContactNo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveChallanCompact(List<CompactCheckBilty> check, Challan Order)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    long OrderID = 0;

                    if (Order != null)
                    {

                        if (Order.ID > 0)
                        {
                            var record = context.Challans.FirstOrDefault(x => x.ID == Order.ID);
                            if (record != null)
                            {
                                Order.ChallanNo = record.ChallanNo;
                                Order.CreatedDate = record.CreatedDate;
                            }
                            context.CompactCheckBilties.RemoveRange(context.CompactCheckBilties.Where(x => x.CompactChallanID == Order.ID).ToList());
                            Order.ModifiedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.ModifiedDate = DateTime.Now;
                            var local = context.Set<LiquadCargoManagment.Models.Challan>().Local.FirstOrDefault(x => x.ID == x.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(Order).State = EntityState.Modified;
                            OrderID = Order.ID;
                        }

                        else
                        {
                            Order.ChallanNo = GetNewChallanNo();
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.CreatedDate = DateTime.Now;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            context.Challans.Add(Order);
                            context.SaveChanges();
                            OrderID = Order.ID;
                        }

                        if (check != null)
                        {
                            foreach (CompactCheckBilty item in check)
                            {
                                item.CompactChallanID = OrderID;
                                context.CompactCheckBilties.Add(item);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            message = "Bilty not selected";
                        }
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    message = "Challan has been created";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }

                }
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditChallan(int id)
        {
            string Message = "";
            string Status = "OK";
            challanModel challan = new challanModel();
            List<CompactBiltyMeta> ch = new List<CompactBiltyMeta>();
            try
            {
                if (id > 0)
                {
                    var z = context.Challans.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    challan.ID = z.ID;
                    challan.ChallanNo = z.ChallanNo;
                    challan.ChallanManualNo = z.ChallanManualNo;
                    challan.ChallanDate = Convert.ToDateTime(z.ChallanDate).ToShortDateString();
                    challan.DropLocationID = (long?)z.DropLocationID;
                    challan.City = z.PickLocation1.City.CityName;
                    challan.Area = z.PickLocation1.Area.Name;
                    challan.Address = z.PickLocation1.Address;
                    challan.PickLocationID = (long?)z.PickLocationID;
                    challan.DropCity = z.PickLocation.City.CityName;
                    challan.DropArea = z.PickLocation.Area.Name;
                    challan.DropAddress = z.PickLocation.Address;
                    challan.VehicleID = (long?)z.VehicleID;
                    
                    challan.Reg = z.Vehicle.RegNo;
                    challan.VehicleType = z.Vehicle.VehicleType.Name;
                    if (z.ExternalVehicleNo != null)
                    {
                        challan.ExternalVehicleNo = z.ExternalVehicleNo;
                    }
                    if (z.Driver != null)
                    {
                        challan.DriverID = (long?)z.DriverID;
                        challan.DriverContact = (long?)z.Driver.CellNo;
                    }
                    if (z.Vendor != null)
                    {
                        challan.VendorID = (long?)z.VendorID;
                        challan.BrokerContact = z.Vendor.OwnerContactNo;
                    }

                    ch = context.CompactCheckBilties.Where(x => x.CompactChallanID == id).Select(x => new CompactBiltyMeta
                    {
                        ID = x.CompactBiltyID,
                        OrderNo = x.Bilty.OrderNo,
                        OrderDate = x.Bilty.OrderDate,
                        BillTo = x.Bilty.CustomerCompany.Name,
                        CustomerType = x.Bilty.CustomerType,
                        Quantity = x.Bilty.OrderDetailsTotalQTY,
                        Weight = x.Bilty.OrderDetailsTotalWeight,
                        TotalFreight = x.Bilty.TotalFreight
                    }).ToList();

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
            return Json(new { challan = challan, bilty = ch, Message = Message, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #region Compact Bilty Selected Location
        [HttpPost]
        public JsonResult GetSelectedLocation(long?[] id)
        {
            var DropLocation = context.Bilties.Where(x => id.Contains(x.ID) && lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.PickLocation2.ID, x.PickLocation2.LocationCode, x.PickLocation2.LocationName }).Distinct().ToList();
            var Location = context.Bilties.Where(x => id.Contains(x.ID) && lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.PickLocation1.ID, x.PickLocation1.LocationCode, x.PickLocation1.LocationName }).Distinct().ToList();
            return Json(new { DropLocation = DropLocation, Location = Location }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ChallanView
        public void ChallanView()
        {
            ViewBag.CustomerType = new SelectList(context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "CustomerType");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID), "ID", "Name");
            ViewBag.NewSalesOrderNo = GetNewSalesOrderNo();
            var PickLocation = context.Bilties.Where(x => (x.PickLocation1.LocationType == "Pick" || x.PickLocation1.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.PickLocation1.ID, LocationCode = string.Format("{0} | {1}", x.PickLocation1.LocationCode, x.PickLocation1.LocationName) }).Distinct().ToList();
            ViewBag.PickLocation = new SelectList(PickLocation, "ID", "LocationCode");
            var DropLocation = context.Bilties.Where(x => (x.PickLocation2.LocationType == "Drop" || x.PickLocation2.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.PickLocation2.ID, LocationCode = string.Format("{0} | {1}", x.PickLocation2.LocationCode, x.PickLocation2.LocationName) }).Distinct().ToList();
            ViewBag.DropLocation = new SelectList(DropLocation, "ID", "LocationCode");
            ViewBag.SearchDropLocation = new SelectList(DropLocation, "ID", "LocationCode");

            var CustomerComany = context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.CustomerComany = new SelectList(CustomerComany, "ID", "Code");

            var Vehicle = context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.VehicleID, Code = string.Format("{0} | {1}", x.Code, x.RegNo) }).ToList();
            ViewBag.Vehicle = new SelectList(Vehicle, "ID", "Code");

            var Broker = context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.OwnerContactName) }).ToList();
            ViewBag.Broker = new SelectList(Broker, "ID", "Code");

            var Driver = context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Driver = new SelectList(Driver, "ID", "Code");
        }
        #endregion

        #endregion

        #region Challan Grid
        public ActionResult ChallanList()
        {
            ViewBag.CustomerType = new SelectList(context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "CustomerType");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID), "ID", "Name");
            ViewBag.NewSalesOrderNo = GetNewSalesOrderNo();
            var PickLocation = context.PickLocations.Where(x => x.LocationType == "Pick" || x.LocationType == "Both" && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationCode = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.PickLocation = new SelectList(PickLocation, "ID", "LocationCode");
            var DropLocation = context.PickLocations.Where(x => x.LocationType == "Drop" || x.LocationType == "Both" && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationCode = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.DropLocation = new SelectList(DropLocation, "ID", "LocationCode");
            ViewBag.SearchDropLocation = new SelectList(DropLocation, "ID", "LocationCode");
            //ViewBag.CustomerComany = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "ID", "Name");
            var CustomerComany = context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.CustomerComany = new SelectList(CustomerComany, "ID", "Code");
            ViewBag.Vehicle = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "VehicleID", "Code");
            //ViewBag.Broker = new SelectList(context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "ID", "Code");
            var Broker = context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.OwnerContactName) }).ToList();
            ViewBag.Broker = new SelectList(Broker, "ID", "Code");
            //ViewBag.Driver = new SelectList(context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "Name");
            var Driver = context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Driver = new SelectList(Driver, "ID", "Code");
            var list = context.Challans.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID) && x.IsDeleted != true).OrderByDescending(x => x.ID).ToList();

            return View(list);
        }

        [HttpPost]

        public JsonResult DeleteCompactChallan(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Challans.Where(x => x.ID == id && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new Models.Challan() { ID = id, IsDeleted = true, DeletedBy = ApplicationHelper.UserID, DeletedDate = DateTime.Now };
                        using (var db = new LCMEntities())
                        {
                            db.Challans.Attach(model);
                            db.Entry(model).Property(x => x.IsDeleted).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedBy).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedDate).IsModified = true;
                            db.SaveChanges();
                            Message = "Record Deleted Successfully";
                        }
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
            var Challan = context.Challans.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_Challan.cshtml", Challan);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DieselManagement

        [HttpGet]
        public ActionResult DieselManagement(string id)
        {

            ViewBag.RegNo = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "VehicleID", "RegNo");
            var Model = new OrderViewModel();
            Model.lstDiesel = new Diesel();

            ViewBag.Id = 0;

            if (id != null && id != string.Empty)
            {
                long _id = Convert.ToInt64(id);

                Model.lstDiesel = context.Diesels.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                Model.lstDieselExpense = context.DieselExpenses.Where(x => x.DieselID == _id).ToList();

                ViewBag.Id = _id;
            }

            else
            {
                ViewBag.DieselNo = GetNewDieselNo();
                ViewBag.RowDieselNo = GetNewRowDieselNo();
                Model.lstDiesel = new Diesel();

                Model.lstDieselExpense = new List<DieselExpense>();
            }
            return View(Model);
        }

        [HttpPost]
        public JsonResult addDieselManagement(Diesel Order, List<DieselExpense> Details)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    long OrderID = 0;

                    if (Order != null)
                    {
                        if (Order.ID > 0)
                        {
                            var record = context.Diesels.FirstOrDefault(x => x.ID == Order.ID);
                            if (record != null)
                            {
                                Order.CreatedDate = record.CreatedDate;
                            }
                            context.DieselExpenses.RemoveRange(context.DieselExpenses.Where(x => x.DieselID == Order.ID).ToList());
                            Order.ModifiedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.ModifiedDate = DateTime.Now;
                            var local = context.Set<LiquadCargoManagment.Models.Diesel>().Local.FirstOrDefault(x => x.ID == x.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(Order).State = EntityState.Modified;
                            OrderID = Order.ID;
                        }

                        else
                        {
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.CreatedDate = DateTime.Now;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            context.Diesels.Add(Order);
                            context.SaveChanges();
                            OrderID = Order.ID;
                        }

                        foreach (DieselExpense item in Details)
                        {
                            item.DieselID = OrderID;
                            context.DieselExpenses.Add(item);
                            context.SaveChanges();
                        }
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    message = "Bilty has been created";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }

                }
            }

            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDieselManagement(int Id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (Id > 0)
                {
                    var inRow = context.Diesels.Where(model => model.ID == Id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";
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

            string View = RenderPartialToString("~/Views/Shared/_DieselManagement.cshtml", context.Diesels.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DieselManagementList()
        {
            return View(context.Diesels.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }

        public ActionResult SearchDiesel(DateTime? DateTo, DateTime? DateFrom, int? DieselRate, int? OilRate, int? OrderNo, string PetrolPump)
        {
            List<Diesel> lstOrders = new List<Diesel>();

            if (DateFrom != null && DateTo != null)
            {
                lstOrders = dml.getSearchDiesel(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstOrders = dml.getSearchDiesel(Convert.ToDateTime(DateFrom), "from");
            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = dml.getSearchDiesel(Convert.ToDateTime(DateTo), "to");
            }
            else if (DieselRate != null)
            {
                lstOrders = context.Diesels.Where(x => x.DieselRate == DieselRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (OilRate != null)
            {
                lstOrders = context.Diesels.Where(x => x.OilRate == OilRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (PetrolPump == "Awan PSO")
            {
                lstOrders = context.Diesels.Where(x => x.PetrolPump == "Awan PSO" && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (PetrolPump == "Taj Petrolium")
            {
                lstOrders = context.Diesels.Where(x => x.PetrolPump == "Taj Petrolium" && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (PetrolPump == "Shah Hascol")
            {
                lstOrders = context.Diesels.Where(x => x.PetrolPump == "Shah Hascol" && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (OrderNo != null)
            {
                lstOrders = context.Diesels.Where(x => x.SerialNo == OrderNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                lstOrders = context.Diesels.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_DieselManagement", lstOrders.ToList());

            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CustomerProfile
        public ActionResult CustomerProfileLms()
        {
            ViewBag.CustomerCompany = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "ID", "Name");
            ViewBag.CustomerDepartment = new SelectList(context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "Name");
            return View(context.CustomerProfileLMS.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
        }

        public ActionResult InsertCustomerProfileLms(CustomerProfileLM model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.ID > 0)
                {
                    model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                    model.ModifiedBy = ApplicationHelper.UserID;
                    model.ModifiedDate = DateTime.Now;
                    model.IsActive = true;
                    var local = context.Set<CustomerProfileLM>().Local.FirstOrDefault(x => x.ID == model.ID);
                    if (local != null)
                    {
                        context.Entry(local).State = EntityState.Detached;
                    }
                    context.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                    model.CreatedBy = ApplicationHelper.UserID;
                    model.IsActive = true;
                    model.CreatedDate = DateTime.Now;
                    context.CustomerProfileLMS.Add(model);
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            var list = context.CustomerProfileLMS.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            string View = RenderPartialToString("~/Views/Shared/_CustomerProfile.cshtml", list);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerProfile(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerProfileLMS.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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
            var list = context.CustomerProfileLMS.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            string View = RenderPartialToString("~/Views/Shared/_CustomerProfile.cshtml", list);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region OwnGroup List
        public ActionResult OwnGroupList()
        {
            return View(context.Subcriptions.Where(x => x.ID == SubcriptionID).ToList());
        }
        #endregion

        #region UniversalBilty Challan

        public ActionResult UniversalChallan()
        {
            ViewBag.Vehicle = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "VehicleID", "Code");
            ViewBag.Driver = new SelectList(context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            ViewBag.Vendor = new SelectList(context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "OwnerContactName");
            ViewBag.Sender = new SelectList(context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "Name");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.ID == SubcriptionID).ToList(), "ID", "Name");
            ViewBag.PickLocation = new SelectList(context.PickLocations.Where(x => x.LocationType == "Pick" || x.LocationType == "Both" || lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "LocationName");
            ViewBag.DropLocation = new SelectList(context.PickLocations.Where(x => x.LocationType == "Drop" || x.LocationType == "Both" || lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "ID", "LocationName");
            return View(context.UniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
        }
        public ActionResult SearchUniversalChallanBilty(DateTime? DateTo, DateTime? DateFrom, long? OrderNo, int? BillTo, long? Sender, int? Receiver, int? PickLocation, string Customer, long? DropLocation, string ManualBiltyNo)
        {
            List<UniversalBilty> lstOrders = new List<UniversalBilty>();
            List<UniversalBiltyCustomerInformation> lstCustomer = new List<UniversalBiltyCustomerInformation>();
            List<UniversalBiltyLocationInformation> lstLocation = new List<UniversalBiltyLocationInformation>();

            foreach (var item in lstOrders)
            {

                if (DateFrom != null && DateTo != null)
                {
                    lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
                }
                else if (DateFrom != null && DateTo == null)
                {
                    lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(DateFrom), "from");
                }
                else if (DateFrom == null && DateTo != null)
                {
                    lstOrders = dml.getUniversalSearchBilty(Convert.ToDateTime(DateTo), "to");
                }

                else if (BillTo != null)
                {
                    lstCustomer = context.UniversalBiltyCustomerInformations.Where(x => x.ClientCode == BillTo).ToList();
                }
                else if (Sender != null)
                {
                    lstCustomer = context.UniversalBiltyCustomerInformations.Where(x => x.ConsignerSenderCustomerCode == Sender).ToList();
                }
                else if (Receiver != null)
                {
                    lstCustomer = context.UniversalBiltyCustomerInformations.Where(x => x.ConsignerReceiverCustomerCode == Receiver).ToList();
                }
                else if (OrderNo != null)
                {
                    lstOrders = context.UniversalBilties.Where(x => x.AutoBiltyNo == OrderNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                }
                else if (ManualBiltyNo != null)
                {
                    lstOrders = context.UniversalBilties.Where(x => x.ManualBiltyNo.StartsWith(ManualBiltyNo) && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                }
                else if (BillTo != null)
                {
                    lstCustomer = context.UniversalBiltyCustomerInformations.Where(x => x.ClientCode == x.ClientCode).ToList();
                }
                else if (PickLocation != null)
                {
                    lstLocation = context.UniversalBiltyLocationInformations.Where(x => x.LocationCode == PickLocation).ToList();
                }
                else if (DropLocation != null)
                {
                    lstLocation = context.UniversalBiltyLocationInformations.Where(x => x.DropLocationCode == DropLocation).ToList();
                }
                //else if (Receiver != null)
                //{
                //    lstOrders = context.Bilties.Where(x => x.PickLocation == Receiver).ToList();
                //}
                else if (Customer == "Paid")
                {
                    lstCustomer = context.UniversalBiltyCustomerInformations.Where(x => x.BillingType == "Paid").ToList();
                }
                else if (Customer == "To Pay")
                {
                    lstCustomer = context.UniversalBiltyCustomerInformations.Where(x => x.BillingType == "To Pay").ToList();
                }

                else
                {
                    lstOrders = context.UniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                }
            }

            string view = RenderPartialToString("_SearchUniversalChallan", lstOrders.ToList());
            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveUniversalChallan(List<UniversalCheckBilty> check, UniversalChallan Order)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    long OrderID = 0;

                    if (Order != null)
                    {

                        if (Order.ID > 0)
                        {
                            context.UniversalCheckBilties.RemoveRange(context.UniversalCheckBilties.Where(x => x.UniversalChallanID == Order.ID).ToList());
                            Order.ModifiedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.ModifiedDate = DateTime.Now;
                            context.Entry(Order).State = EntityState.Modified;
                            OrderID = Order.ID;
                        }

                        else
                        {
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.CreatedDate = DateTime.Now;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            context.UniversalChallans.Add(Order);
                            context.SaveChanges();
                            OrderID = Order.ID;
                        }

                        if (check != null)
                        {
                            foreach (UniversalCheckBilty item in check)
                            {
                                item.UniversalChallanID = OrderID;
                                context.UniversalCheckBilties.Add(item);
                                context.SaveChanges();
                            }
                        }
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    message = "Challan has been created";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }

                }
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditUniversalChallan(int id)
        {
            string Message = "";
            string Status = "OK";
            UniversalChallanModel challan = new UniversalChallanModel();
            List<UniversalCheckBilty> ch = new List<UniversalCheckBilty>();
            try
            {
                if (id > 0)
                {
                    var z = context.UniversalChallans.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    challan.ID = z.ID;
                    challan.ChallanNo = z.ChallanNo;
                    challan.ChallanManualNo = z.ChallanManualNo;
                    challan.ChallanDate = Convert.ToDateTime(z.ChallanDate).ToShortDateString();
                    challan.DriverID = (long)z.DriverID;
                    challan.DropLocationID = (long)z.DropLocationID;
                    challan.PickLocationID = (long)z.PickLocationID;
                    challan.VendorID = (long)z.VendorID;
                    challan.VehicleID = (long)z.VehicleID;
                    challan.ExternalVehicleNo = z.ExternalVehicleNo;
                    ViewBag.list = context.UniversalCheckBilties.Where(x => x.UniversalChallanID == id).Select(x => new { x.UniversalBiltyID, x.UniversalChallan }).ToList();
                    //ViewBag.Compact = context.Bilties.Where(x => x.ID == id).Select(x => new { x.ID }).ToList();
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
            return Json(new { challan = challan, bilty = ViewBag.list, Message = Message, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Standard Vehicle
        public ActionResult VehicleStandard()
        {
            return View(context.StandardVehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList());
        }
        [HttpPost]
        public ActionResult addVehicleStandard(StandardVehicle model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    model.Code = model.Code.ToUpper();
                    var Duplicated = context.StandardVehicles.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                    if (model.ID > 0)
                    {
                        bool isEditable = false;
                        if (Duplicated != null)
                        {
                            if (Duplicated.ID == model.ID)
                            {
                                isEditable = true;

                            }

                        }
                        else
                        {
                            isEditable = true;
                        }

                        if (isEditable)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            var local = context.Set<StandardVehicle>().Local.FirstOrDefault(x => x.ID == model.ID);
                            if (local != null)
                            {
                                context.Entry(local).State = EntityState.Detached;
                            }
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }

                    }
                    else
                    {
                        if (Duplicated == null)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.StandardVehicles.Add(model);
                        }
                        else
                        {
                            Message = "Already exist";
                            Status = "Duplicated";
                        }
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<StandardVehicle> data = context.StandardVehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            string View = RenderPartialToString("~/Views/Shared/_VehicleStandard.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVehicleStandard(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.StandardVehicles.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_VehicleStandard.cshtml", context.StandardVehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeVehicleStandardStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.StandardVehicles.Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                record.IsActive = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_VehicleStandard.cshtml", context.StandardVehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LocalPick Universal Bilty
        public ActionResult LocalPick(int id)
        {
            var Model = new OrderViewModel();
            //Model.lstLocalPick = new LocalPickUniversalBilty();
            Model.UniversalBilty = new UniversalBilty();

            var Customer = context.CustomerProfileLMS.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Customer = new SelectList(Customer, "ID", "Code");
            var PickLocation = context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationName = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.PickLocation = new SelectList(PickLocation, "ID", "LocationName");
            var Product = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Product = new SelectList(Product, "ID", "Code");
            var Vehicle = context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.VehicleID, Code = string.Format("{0} | {1}", x.Code, x.RegNo) }).OrderBy(x => x.Code).ToList();
            ViewBag.Vehicle = new SelectList(Vehicle, "ID", "Code");

            //ViewBag.Id = 0;

            //if (id != 0 && id != 0)
            //{
            //    long _id = Convert.ToInt64(id);

            Model.UniversalBilty = context.UniversalBilties.Where(x => x.ID == id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
            //Model.UniversalBilty = context.UniversalBilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

            //Model.lstLocalPickProduct = context.LocalPickProducts.Where(x => x.LocalPickID == _id).ToList();

            //    ViewBag.Id = _id;
            //}

            //else
            //{
            //    Model.lstLocalPick = new LocalPickUniversalBilty();

            //    Model.lstLocalPickProduct = new List<LocalPickProduct>();
            //}

            return View(Model);
        }
        [HttpPost]
        public ActionResult AddLocalPick(List<LocalPickProduct> Details, LocalPickUniversalBilty Order)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    long OrderID = 0;

                    if (Order != null)
                    {
                        if (Order.ID > 0)
                        {
                            context.LocalPickProducts.RemoveRange(context.LocalPickProducts.Where(x => x.LocalPickID == Order.ID).ToList());

                            Order.IsActive = true;
                            Order.ModifiedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.ModifiedDate = DateTime.Now;
                            context.Entry(Order).State = EntityState.Modified;
                            Order.UniversalBiltyID = OrderID;
                        }
                        else
                        {
                            Order.IsActive = true;
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.CreatedDate = DateTime.Now;
                            context.LocalPickUniversalBilties.Add(Order);
                            context.SaveChanges();
                            Order.UniversalBiltyID = OrderID;
                        }

                        foreach (LocalPickProduct item in Details)
                        {
                            item.LocalPickID = OrderID;
                            context.LocalPickProducts.Add(item);
                            context.SaveChanges();
                        }
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    message = "Local Pick has been created";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }
                }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LocalPickList()
        {
            var list = context.LocalPickUniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            return View(list);
        }
        public JsonResult DeleteLocalPick(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.LocalPickUniversalBilties.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Admin/LocalPickList.cshtml", context.LocalPickUniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region LocalDrop Universal Bilty
        public ActionResult LocalDrop(string id)
        {
            var Model = new OrderViewModel();
            Model.lstLocalPick = new LocalPickUniversalBilty();

            var Customer = context.CustomerProfileLMS.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Customer = new SelectList(Customer, "ID", "Code");
            var PickLocation = context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationName = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.PickLocation = new SelectList(PickLocation, "ID", "LocationName");
            var Product = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Product = new SelectList(Product, "ID", "Code");
            var Vehicle = context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.VehicleID, Code = string.Format("{0} | {1}", x.Code, x.RegNo) }).OrderBy(x => x.Code).ToList();
            ViewBag.Vehicle = new SelectList(Vehicle, "ID", "Code");

            ViewBag.Id = 0;

            if (id != null && id != string.Empty)
            {
                long _id = Convert.ToInt64(id);

                Model.lstLocalDrop = context.LocalDropUniversalBilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();

                Model.lstLocalDropProduct = context.LocalDropProducts.Where(x => x.LocalDropID == _id).ToList();

                ViewBag.Id = _id;
            }

            else
            {
                Model.lstLocalDrop = new LocalDropUniversalBilty();

                Model.lstLocalDropProduct = new List<LocalDropProduct>();
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult AddLocalDrop(List<LocalDropProduct> Details, LocalDropUniversalBilty Order)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    long OrderID = 0;

                    if (Order != null)
                    {
                        if (Order.ID > 0)
                        {
                            context.LocalDropProducts.RemoveRange(context.LocalDropProducts.Where(x => x.LocalDropID == Order.ID).ToList());

                            Order.IsActive = true;
                            Order.ModifiedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.ModifiedDate = DateTime.Now;
                            context.Entry(Order).State = EntityState.Modified;
                            OrderID = Order.ID;
                        }
                        else
                        {
                            Order.IsActive = true;
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.CreatedDate = DateTime.Now;
                            context.LocalDropUniversalBilties.Add(Order);
                            context.SaveChanges();
                            OrderID = Order.ID;
                        }

                        foreach (LocalDropProduct item in Details)
                        {
                            item.LocalDropID = OrderID;
                            context.LocalDropProducts.Add(item);
                            context.SaveChanges();
                        }
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    message = "Local Pick has been created";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }
                }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LocalDropList()
        {
            var list = context.LocalDropUniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            return View(list);
        }
        public JsonResult DeleteLocalDrop(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.LocalDropUniversalBilties.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Admin/LocalDropList.cshtml", context.LocalDropUniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Filling Station
        public ActionResult FillingStation()
        {
            var list = context.FillingStations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult AddFillingStation(FillingStation model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.Name != null)
                {
                    var Duplicated = context.FillingStations.Where(x => (x.Code == model.Code || x.Name == model.Name) && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }

                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.CreatedDate = DateTime.Now;
                            context.FillingStations.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }


                    }

                }
                else
                {
                    Message = "Code , Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_FillingStation.cshtml", context.FillingStations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFillingStation(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.FillingStations.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_FillingStation.cshtml", context.FillingStations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region UniversalBilty Vehicle Information
        [HttpPost]
        public ActionResult AddVehicleInfo(List<UniversalVehicleDetail> Details)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    foreach (UniversalVehicleDetail item in Details)
                    {
                        //OrderID = item.VehicleID;
                        item.CreatedBy = ApplicationHelper.UserID;
                        item.CreatedDate = DateTime.Now;
                        item.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        item.IsActive = true;
                        context.UniversalVehicleDetails.Add(item);
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }
                }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UniversalBilty Container Information
        [HttpPost]
        public ActionResult AddContainerInfo(List<UniversalContainerDetail> Details)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    foreach (UniversalContainerDetail item in Details)
                    {
                        //OrderID = item.VehicleID;
                        item.CreatedBy = ApplicationHelper.UserID;
                        item.CreatedDate = DateTime.Now;
                        item.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                        context.UniversalContainerDetails.Add(item);
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }
                }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region  Universal Bilty Reloading
        [HttpGet]
        public JsonResult Reload()
        {
            var Location = context.PickLocations.Where(x => (x.LocationType == "Pick" || x.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.ID, x.LocationCode, x.LocationName }).ToList();
            var DropLocation = context.PickLocations.Where(x => (x.LocationType == "Drop" || x.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.ID, x.LocationCode, x.LocationName }).ToList();
            var City = context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.CityID, x.CityCode, x.CityName }).ToList();
            var Area = context.Areas.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).Select(x => new { x.ID, x.Code, x.Name }).ToList();
            var Customer = context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.ID, x.Code, x.Name }).ToList();
            var Shipment = context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.ID, x.Code, x.Name }).ToList();
            var Vehicle = context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).Select(x => new { x.ID, x.Code, x.Name }).ToList();
            var Container = context.ContainerTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.ContainerTypeID, x.Code, x.ContainerTypeName }).ToList();
            var Product = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).Select(x => new { x.ID, x.Code, x.Name }).ToList();
            var Document = context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.ID, x.Code, x.Name }).ToList();
            var Received = context.Employees.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.EmployeeID, x.EmployeeCode, x.EmployeeName }).ToList();
            var Damage = context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.DamageTypeID, x.Code, x.Name }).ToList();
            var DocumentType = context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.DocumentTypeID, x.Code, x.Name }).ToList();
            var Expense = context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).Select(x => new { x.ID, x.Code, x.Name }).ToList();

            return Json(new
            {
                Location = Location,
                City = City,
                Area = Area,
                DropLocation = DropLocation,
                Customer = Customer,
                Shipment = Shipment,
                Vehicle = Vehicle,
                Container = Container,
                Product = Product,
                Document = Document,
                Received = Received,
                Damage = Damage,
                Expense = Expense,
                DocumentType = DocumentType
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region NewSalesOrder
        public ActionResult SalesOrderLastUserList(SearchOrder param)
        {
            var Orderlist = SearchSalesOrder(context, param);
            ViewBag.VehicleReg = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "VehicleID", "RegNo");
            ViewBag.LoadingPoint = new SelectList(context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "LocationName");
            ViewBag.ShipmentType = new SelectList(context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "Name");
            return View(Orderlist);
        }
        public ActionResult NewSalesOrderUserLast(long? id)
        {
            SetSalesOrderMetas();
            Meta meta = new Meta();
            if (id != null && id > 0)
            {
                long _id = (long)id;
                meta.SaleOrder = context.SaleOrders.Where(x => x.ID == id).FirstOrDefault();
                if (meta.SaleOrder.SaleOrderNo == null)
                    meta.SaleOrder.SaleOrderNo = meta.SaleOrder.InvoiceNo;
                meta.Expenses = context.SaleOrderExpenses.Where(x => x.SaleOrderID == _id).ToList();
                meta.DiesalExpense = context.SaleOrderDieselExpenses.Where(x => x.SaleOrderID == _id).ToList();
                meta.Destinations = context.SaleOrdeDestinations.Where(x => x.SaleOrderID == _id).ToList();
                return View(meta);
            }
            else
            {
                meta.SaleOrder = new SaleOrder();
                meta.Destinations = new List<SaleOrdeDestination>();
                meta.DiesalExpense = new List<SaleOrderDieselExpense>();
                meta.Expenses = new List<SaleOrderExpens>();
            }
            return View(meta);
        }

        public void SetSalesOrderMetas()
        {
            ViewBag.Payment = new List<string>() { "Bank", "Cash" };
            //ViewBag.GST = new List<string>() { "Sindh", "Punjab" };
            ViewBag.ShipmentType = context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { Value = x.ID, Text = x.Name });
            ViewBag.VehicleRegNo = context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { Value = x.VehicleID, Text = x.RegNo });
            ViewBag.LoadingLocation = context.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { Value = x.ID, Text = string.Format("{0} | {1} - {2}", x.LocationCode, x.LocationName, x.City.CityName) });
            ViewBag.Products = context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { Value = x.ID, Text = string.Format("{0} | {1}", x.Code, x.Name) });
            ViewBag.Expenses = context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { Value = x.ID, Text = string.Format("{0} | {1}", x.Name, x.Code) });
            ViewBag.Vendors = context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { Value = x.ID, Text = string.Format("{0} | {1}", x.Name, x.Code) });
            ViewBag.BankBranchId = context.Banks.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { Value = x.BankID, Text = string.Format("{0} | {1}", x.Name, x.Code) });     
            ViewBag.GST = context.TaxRateRegistrations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { Value = x.ID, Text = string.Format("{0} {1}{2}", x.TaxRateName, x.TaxRatePercent,"%") });
            ViewBag.Taxs = context.Provinces.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { Value = x.ProvinceID, Text = x.ProvinceName });
            ViewBag.customerCompany = context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { Value = x.ID, Text = x.Name });
        }

        public ActionResult save(SaleOrder saleOrder, List<SaleOrdeDestination> Destination, List<SaleOrderDieselExpense> Diesal, List<SaleOrderExpens> Expenses, string type)
        {
            string status = "OK";
            try
            {
                var dest = context.SaleOrdeDestinations.FirstOrDefault();
                if (context.ShipmentTypes.Where(x => x.ID == saleOrder.ShipmentTypeId).FirstOrDefault().Name.Contains("local"))
                {
                    saleOrder.InvoiceNo = saleOrder.SaleOrderNo;
                    saleOrder.SaleOrderNo = null;
                }
                //List<string> lstGSTType = new List<string>();
                //if (GetProvinceName((long)saleOrder.LoadingLocationId).ToLower()
                //    == GetProvinceName((long)Destination[0].DestinationLocation).ToLower())
                //{
                //    if (GetCityName((long)saleOrder.LoadingLocationId).ToLower()
                //        == GetCityName((long)Destination[0].DestinationLocation).ToLower())
                //    {
                //        if (GetShipmentType((long)Destination[0].DestinationLocation) == "local")
                //            saleOrder.GSTType = EnumGSTType.Inter_Province_Secondary_ZeroGST;
                //        else
                //            saleOrder.GSTType = EnumGSTType.Inter_Province_Primary_ZeroGST;
                //    }
                //    else
                //    {
                //        if (GetShipmentType((long)Destination[0].DestinationLocation) == "local")
                //        {
                //            lstGSTType.Add(EnumGSTType.Intra_Province_Secondary.Replace("Province", GetProvinceName((long)saleOrder.LoadingLocationId)));
                //            lstGSTType.Add(EnumGSTType.Intra_Province_Secondary.Replace("Province", GetProvinceName((long)Destination[0].DestinationLocation)));
                //        }
                //        else
                //        {

                //            lstGSTType.Add(EnumGSTType.Intra_Province_Primary.Replace("Province", GetProvinceName((long)saleOrder.LoadingLocationId)));
                //            lstGSTType.Add(EnumGSTType.Intra_Province_Primary.Replace("Province", GetProvinceName((long)Destination[0].DestinationLocation)));
                //        }


                //    }
                //}
                //else
                //{
                //    if (GetShipmentType((long)Destination[0].DestinationLocation) == "local")
                //    {
                //        lstGSTType.Add(EnumGSTType.Inter_Province_Secondary.Replace("Province", GetProvinceName((long)saleOrder.LoadingLocationId)));
                //        lstGSTType.Add(EnumGSTType.Inter_Province_Secondary.Replace("Province", GetProvinceName((long)Destination[0].DestinationLocation)));
                //    }
                //    else
                //    {
                //        lstGSTType.Add(EnumGSTType.Inter_Province_Primary.Replace("Province", GetProvinceName((long)saleOrder.LoadingLocationId)));
                //        lstGSTType.Add(EnumGSTType.Inter_Province_Primary.Replace("Province", GetProvinceName((long)Destination[0].DestinationLocation)));
                //    }
                //}


                saleOrder.OwnCompanyId = OwnCompanyID;
                if (saleOrder.ID > 0)
                {
                    saleOrder.ModifiedDate = DateTime.Now;
                    saleOrder.ModifiedBy = ApplicationHelper.UserID;
                    context.Entry(saleOrder).State = EntityState.Modified;
                    if (lstBiltyRole.Contains(ApplicationHelper.EnumBiltyRole.User1))
                    {
                        //if (saleOrder.GSTType.Contains("intra province"))
                        //{

                            //if (saleOrder.GSTType != EnumGSTType.Inter_Province_Primary_ZeroGST &&
                            //    saleOrder.GSTType != EnumGSTType.Inter_Province_Secondary_ZeroGST)
                            //{
                            double totalFrieght = 0;
                            foreach (var item in Destination)
                            {
                                totalFrieght += item.Frieght > 0 ? (double)item.Frieght : 0;
                            }
                            var halfFrieght = totalFrieght / 2;
                            string[] array = {"A","B","C","D","E" };
                            for (int i = 0; i < 2; i++)
                            {
                                context.SaleOrderChilds.RemoveRange(context.SaleOrderChilds.Where(x => x.SaleOrderID == saleOrder.ID).ToList());
                                SaleOrderChild ch = new SaleOrderChild();
                                ch.SaleOrderID = saleOrder.ID;
                                ch.AreaIdForDestinationPoint = saleOrder.AreaIdForDestinationPoint;
                                ch.AreaIdForLoadingPoint = saleOrder.AreaIdForLoadingPoint;
                                ch.CityId = saleOrder.CityId;
                                ch.CustomerCompanyId = saleOrder.CustomerCompanyId;
                                ch.DecantationDate = saleOrder.DecantationDate;
                                ch.GSTType = saleOrder.GSTType;
                                ch.GrandTotal = saleOrder.GrandTotal;
                                ch.InvoiceNo = saleOrder.InvoiceNo;
                                ch.LoadingCode = saleOrder.LoadingCode;
                                ch.LoadingDate = saleOrder.LoadingDate;
                                ch.LoadingLocationId = saleOrder.LoadingLocationId;
                                ch.ManualNo = saleOrder.ManualNo + "-" + array[i];
                                ch.OrderDate = saleOrder.OrderDate;
                                ch.OwnCompanyId = saleOrder.OwnCompanyId;
                                ch.ProvinceId = saleOrder.ProvinceId;
                                ch.QueNo = saleOrder.QueNo;
                                ch.SaleOrderNo = saleOrder.SaleOrderNo ;
                                ch.ShipmentTypeId = saleOrder.ShipmentTypeId;
                                ch.Status = saleOrder.Status;
                                ch.TaxID = saleOrder.TaxID;
                                ch.TokenNumber = saleOrder.TokenNumber;
                                ch.VehicleId = saleOrder.VehicleId;
                                ch.DeliveryDate = saleOrder.DeliveryDate;
                                ch.CityId = saleOrder.CityId;
                                ch.Freight = halfFrieght;

                                context.SaleOrderChilds.Add(ch);
                                //List<SalesOrderChildDestination> socd = new List<SalesOrderChildDestination>();
                                //foreach (var item in Destination)
                                //{
                                //    socd.Add(new SalesOrderChildDestination()
                                //    {
                                //        CreatedBy = item.CreatedBy,
                                //        CreatedDate = item.CreatedDate,
                                //        DestinationCode = item.DestinationCode,
                                //        DestinationLocation = item.DestinationLocation,
                                //        Frieght = halfFrieght,
                                //        GSTType = item.GSTType,
                                //        OwnCompanyId = item.OwnCompanyId,
                                //        Product = item.Product,
                                //        ProductQty = item.ProductQty,
                                //        IsActive = item.IsActive,
                                //        ModifiedBy = item.ModifiedBy,
                                //        ModifiedDate = item.ModifiedDate

                                //    } );
                                //}
                                //foreach (var item in socd)
                                //{
                                //    item.SaleOrderChildID = ch.ID;
                                //    item.Frieght = item.Frieght / 2;
                                //    context.SalesOrderChildDestinations.Add(item);
                                //}


                            }

                            //}
                        //}
                    }
                    context.SaleOrdeDestinations.RemoveRange(context.SaleOrdeDestinations
                        .Where(x => x.SaleOrderID == saleOrder.ID).ToList());
                    foreach (var item in Destination)
                    {
                        item.SaleOrderID = saleOrder.ID;
                        context.SaleOrdeDestinations.Add(item);
                    }
                    context.SaleOrderDieselExpenses.RemoveRange(context.SaleOrderDieselExpenses
                        .Where(x => x.SaleOrderID == saleOrder.ID).ToList());
                    if (Diesal != null)
                    {
                        foreach (var item in Diesal)
                        {
                            item.SaleOrderID = saleOrder.ID;
                            context.SaleOrderDieselExpenses.Add(item);
                        }
                    }
                    context.SaleOrderExpenses.RemoveRange(context.SaleOrderExpenses
                        .Where(x => x.SaleOrderID == saleOrder.ID).ToList());
                    if (Expenses != null)
                    {
                        foreach (var item in Expenses)
                        {
                            item.SaleOrderID = saleOrder.ID;
                            context.SaleOrderExpenses.Add(item);
                        }
                    }

                }
                else
                {
                    saleOrder.CreatedDate = DateTime.Now;
                    saleOrder.CreatedBy = ApplicationHelper.UserID;
                    context.SaleOrders.Add(saleOrder);
                    foreach (var item in Destination)
                    {
                        item.SaleOrderID = saleOrder.ID;
                        context.SaleOrdeDestinations.Add(item);
                    }

                    if (Expenses != null)
                    {
                        foreach (var item in Expenses)
                        {
                            item.SaleOrderID = saleOrder.ID;
                            context.SaleOrderExpenses.Add(item);
                        }
                    }
                    if (Diesal != null)
                    {
                        foreach (var item in Diesal)
                        {
                            item.SaleOrderID = saleOrder.ID;
                            context.SaleOrderDieselExpenses.Add(item);
                        }
                    }

                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                status = ex.GetType().Name + ex.Message;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public string GetProvinceName(long LocationId)
        {
            string ProvinceName = string.Empty;
            var record = context.PickLocations.Where(x => x.ID == LocationId).FirstOrDefault();
            if (record != null)
            {
                var city = context.Cities.FirstOrDefault(x => x.CityID == record.CityID);
                var province = context.Provinces.FirstOrDefault(x => x.ProvinceID == record.City.ProvinceID);
                ProvinceName = province.ProvinceName;
            }
            return ProvinceName;
        }
        public string GetShipmentType(long LocationId)
        {
            string Name = string.Empty;
            var record = context.PickLocations.Where(x => x.ID == LocationId).FirstOrDefault();
            if (record != null)
            {
                Name = record.ShipmentType.Name;
            }
            return Name;
        }

        public string GetCityName(long LocationId)
        {
            string Name = string.Empty;
            var record = context.PickLocations.Where(x => x.ID == LocationId).FirstOrDefault();
            if (record != null)
            {
                var City = context.Cities.FirstOrDefault(x => x.CityID == record.CityID);
                Name = City.CityName;
            }
            return Name;
        }
        #endregion

        #region SalesOrder Bill
        public ActionResult GenerateBill(SearchOrder model)
        {
            SetSalesOrderMetas();
            BillsMeta bm = new BillsMeta();
            List<SaleOrderChild> bills = new List<SaleOrderChild>();
            if (model != null)
            {
                bills = SearchBill(context, model);
            }
            bm.lstBills = context.Bills.Where(x => x.isDeleted != true).ToList();
            var list = bm.lstBills.Select(x => new SaleOrderChild { ID = x.ID }).ToList();
            foreach (var item in list)
            {
                bills.Remove(item);
            }
            bm.SearchBills = bills;
            return View(bm);
        }

        public ActionResult SaveBill(int[] ChildIds)
        {
            Bill bill = new Bill();
            bill.CreatedBy = ApplicationHelper.UserID;
            bill.CreatedDate = DateTime.Now;
            bill.isDeleted = false;
            context.Bills.Add(bill);
            foreach (var item in ChildIds)
            {
                BillChild bchild = new BillChild();
                bchild.BillId = bill.ID;
                bchild.SaleOrderChildId = item;
                context.BillChilds.Add(bchild);
            }
            context.SaveChanges();
            return RedirectToAction("GenerateBill");
        }
        public JsonResult EditLcmBill(int id)
        {
            string Message = "";
            string Status = "OK";
            List<LcmBillMeta> ch = new List<LcmBillMeta>();
            try
            {
                if (id > 0)
                {
                    ch = context.BillChilds.Where(x => x.BillId == id).Select(x => new LcmBillMeta
                    {
                        ID = x.SaleOrderChildId,
                        Customer = x.SaleOrderChild.CustomerCompany.Name,
                        OrderDate = x.SaleOrderChild.OrderDate,
                        OrderNo = x.SaleOrderChild.SaleOrderNo,
                        Vehicle = x.SaleOrderChild.Vehicle.RegNo,
                        Loading = x.SaleOrderChild.PickLocation.LocationName,
                        GST = x.SaleOrderChild.GSTType,
                        Shipment = x.SaleOrderChild.ShipmentType.Name,
                        Freight = x.SaleOrderChild.Freight
                    }).ToList();

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
            return Json(new { Check = ch, Message = Message, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLcmBill(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Bills.Where(x => x.ID == id && x.isDeleted != true).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new Models.Bill() { ID = id, isDeleted = true};
                        using (var db = new LCMEntities())
                        {
                            db.Bills.Attach(model);
                            db.Entry(model).Property(x => x.isDeleted).IsModified = true;
                            db.SaveChanges();
                            Message = "Record Deleted Successfully";
                        }
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
            var Bill = context.CompactBills.Where(x => x.IsDeleted != true).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_Bill.cshtml", Bill);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //#region ngb mcb branches
        //public actionresult mcbbranches()
        //{
        //    adminmodel model = new adminmodel();
        //    model.lstmcbbranches = dataaccess.getdatebysp<usp_mcbbranches>("usp_mcbbranches");
        //    return view(model);
        //}
        //[httppost]
        //public actionresult mcbbranches(usp_mcbbranches model)
        //{
        //    string message = "record inserted successfully!";
        //    string status = "ok";
        //    string form = "mcbbranch";
        //    string html = string.empty;
        //    list<usp_mcbbranches> _branches = new list<usp_mcbbranches>();
        //    sqlparameter[] paramstostore = new sqlparameter[13];
        //    if (model.id > 0)
        //    {

        //        paramstostore[10] = new sqlparameter("@createddate", sqldbtype.datetime);
        //        paramstostore[10].value = dbnull.value;
        //        paramstostore[12] = new sqlparameter("@modifieddate", sqldbtype.datetime);
        //        paramstostore[12].value = datetime.now;
        //        paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //        paramstostore[0].value = operations.update;
        //        paramstostore[3] = new sqlparameter("@id", sqldbtype.bigint);
        //        paramstostore[3].direction = parameterdirection.input;
        //        paramstostore[3].value = model.id;

        //        paramstostore[9] = new sqlparameter("@createdby", sqldbtype.bigint);
        //        paramstostore[9].value = dbnull.value;
        //        paramstostore[11] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //        paramstostore[11].value = 0;// requesthelper.myprofile.userid;

        //    }
        //    else
        //    {
        //        paramstostore[10] = new sqlparameter("@createddate", sqldbtype.datetime);
        //        paramstostore[10].value = datetime.now;
        //        paramstostore[12] = new sqlparameter("@modifieddate", sqldbtype.datetime);
        //        paramstostore[12].value = dbnull.value;
        //        paramstostore[3] = new sqlparameter("@id", sqldbtype.bigint);
        //        paramstostore[3].direction = parameterdirection.output;
        //        paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //        paramstostore[0].value = operations.insert;
        //        paramstostore[9] = new sqlparameter("@createdby", sqldbtype.bigint);
        //        paramstostore[9].value = 0; ;// requesthelper.myprofile.userid;
        //        paramstostore[11] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //        paramstostore[11].value = dbnull.value;
        //    }

        //    paramstostore[1] = new sqlparameter("@p_success", sqldbtype.bit);
        //    paramstostore[1].direction = parameterdirection.output;
        //    paramstostore[8] = new sqlparameter("@active", sqldbtype.bit);
        //    paramstostore[8].value = model.active;

        //    paramstostore[2] = new sqlparameter("@p_error_message", sqldbtype.varchar);
        //    paramstostore[2].direction = parameterdirection.output;
        //    paramstostore[2].size = -1;
        //    paramstostore[4] = new sqlparameter("@br_code", sqldbtype.varchar);
        //    paramstostore[4].value = model.br_code;
        //    paramstostore[4].size = 10;
        //    paramstostore[6] = new sqlparameter("@contactno", sqldbtype.varchar);
        //    paramstostore[6].value = model.contactno;
        //    paramstostore[6].size = 50;
        //    paramstostore[7] = new sqlparameter("@emailid", sqldbtype.varchar);
        //    paramstostore[7].value = model.emailid;
        //    paramstostore[7].size = 50;
        //    paramstostore[5] = new sqlparameter("@branchname", sqldbtype.nvarchar);
        //    paramstostore[5].value = model.branchname;
        //    paramstostore[5].size = 500;
        //    dataaccess.getdatebysp<usp_mcbbranches>("usp_mcbbranches", paramstostore);
        //    if (convert.toboolean(paramstostore[1].value) == true)
        //    {
        //        if (model.id > 0)
        //        {
        //            message = configurationmanager.appsettings["successmessage"].tostring();
        //        }
        //    }
        //    else
        //    {

        //    }
        //    _branches = dataaccess.getdatebysp<usp_mcbbranches>("usp_mcbbranches");

        //    html = renderrazorviewtostring("admin/_branches", _branches, this);
        //    var jsonresult = json(new { status = status, html = html, message = message, form = form }, jsonrequestbehavior.allowget);
        //    jsonresult.maxjsonlength = int.maxvalue;
        //    return jsonresult;
        //}

        //private string renderrazorviewtostring(string viewname, object model, controller currentcontroller)
        //{
        //    viewdatadictionary viewdata = new viewdatadictionary();
        //    try
        //    {


        //        viewdata.model = model;
        //        using (var sw = new stringwriter())
        //        {
        //            var viewresult = viewengines.engines.findpartialview(currentcontroller.controllercontext, viewname);
        //            var viewcontext = new viewcontext(currentcontroller.controllercontext, viewresult.view, viewdata, currentcontroller.tempdata, sw);
        //            viewresult.view.render(viewcontext, sw);
        //            viewresult.viewengine.releaseview(currentcontroller.controllercontext, viewresult.view);
        //            return sw.getstringbuilder().tostring();
        //        }
        //    }
        //    catch
        //    {


        //    }
        //    return "";
        //}

        //#endregion

        //#region organizaiton...

        //public actionresult organizationview()
        //{
        //    if (request.isajaxrequest())
        //        return partialview("~/views/admin/organizationview.cshtml", null);

        //    return view();
        //}
        //#endregion

        //#region customer profile...

        //public jsonresult postcities(list<int> cities)
        //{

        //    return json("");
        //}
        //public actionresult customerprofile(string guidorderid = "0")
        //{
        //    //using (biltysystementities context = new biltysystementities())
        //    //{
        //    //    var cities = context.cities.tolist();
        //    //    viewbag.cities = cities;
        //    //}
        //    int64 profileid = 0;
        //    if (guidorderid != "0")
        //        profileid = convert.toint64(utility.base64decode(guidorderid));
        //    adminmodel model = new adminmodel();
        //    try
        //    {
        //        model.customerprofilelist = dataaccess.getdatafromspbyid<usp_customerprofile>("usp_customerprofile", "profileid", profileid);
        //        if (model.customerprofilelist == null)
        //        {
        //            model.customerprofilelist = new list<usp_customerprofile>();
        //            model.customerprofile.profileid = 0;
        //            model.customerprofile.customerid = 0;
        //            model.customerprofile.owncompanyid = 0;
        //            model.customerprofile.paymentterm = "0";
        //            model.customerprofile.creditterm = "0";
        //            model.customerprofile.invoiceformat = "0";

        //        }
        //        else
        //            model.customerprofile = model.customerprofilelist[0];
        //        model.customerprofiledetaillist = dataaccess.getdatafromspbyid<usp_customerprofiledetail>("usp_customerprofiledetail", "profileid", profileid);
        //        if (model.customerprofiledetaillist == null)
        //            model.customerprofiledetaillist = new list<usp_customerprofiledetail>();
        //        model.lstowncompany = dataaccess.getdatebysp<usp_owncompany>("usp_owncompany");

        //        model.lstprduct = dataaccess.getdatebysp<usp_product>("usp_product");
        //        model.lstcity = dataaccess.getdatebysp<usp_city>("usp_city");
        //        if (model.lstcity == null)
        //            model.lstcity = new list<usp_city>();
        //        model.lstcity.insert(0, new usp_city { cityname = "select", cityid = 0 });
        //        model.companylist = dataaccess.getdatebysp<usp_company>("usp_company");
        //        if (model.lstcity == null)
        //            model.lstcity = new list<usp_city>();
        //        if (model.companylist == null)
        //            model.companylist = new list<usp_company>();
        //        if (model.lstprduct == null)
        //            model.lstprduct = new list<usp_product>();
        //        if (model.lstowncompany == null)
        //            model.lstowncompany = new list<usp_owncompany>();
        //        model.lstowncompany.insert(0, new usp_owncompany { name = "select", id = 0 });
        //        if (guidorderid != "0")
        //        {
        //            viewbag.profileid = model.customerprofile.profileid;
        //            viewbag.customerid = model.customerprofile.customerid;
        //        }
        //        else
        //        {
        //            viewbag.profileid = "0";
        //            viewbag.customerid = "0";
        //        }
        //    }
        //    catch (exception)
        //    {

        //        throw;
        //    }
        //    return view(model);
        //}
        //[authorize]
        //[httppost]
        //public actionresult addcustomerprofile(usp_customerprofile model, string customername = "")
        //{
        //    string message = "record inserted successfully!";
        //    string status = "ok";
        //    string form = "companyprofile";
        //    string html = string.empty;
        //    string query = "  select * from customerprofile where customerid=" + model.customerid + " and owncompanyid=" + model.owncompanyid + "";

        //    datatable exist = dataaccess.getdatatablebyquery(query);
        //    if (exist != null)
        //    {
        //        if (exist.rows.count > 0)

        //            if (exist.rows.count == 1)
        //            {
        //                if (exist.rows[0]["profileid"].tostring() != model.profileid.tostring().trim())
        //                {
        //                    message = "record already exists!";
        //                    status = "found";
        //                    return json(new { status = status, html = html, masterid = model.profileid, message = message });
        //                }
        //            }


        //    }
        //    sqlparameter[] paramstostore = new sqlparameter[18];
        //    try
        //    {

        //        paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //        paramstostore[3] = new sqlparameter("@profileid", sqldbtype.bigint);
        //        if (model.profileid == 0)
        //        {
        //            paramstostore[0].value = operations.insert;
        //            paramstostore[3].direction = parameterdirection.output;

        //            paramstostore[12] = new sqlparameter("@createddate", sqldbtype.date);
        //            paramstostore[12].value = datetime.now;
        //            paramstostore[13] = new sqlparameter("@modifieddate", sqldbtype.date);
        //            paramstostore[13].value = dbnull.value;
        //            paramstostore[14] = new sqlparameter("@createdby", sqldbtype.bigint);
        //            paramstostore[14].value = applicationhelper.userid;
        //            paramstostore[15] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //            paramstostore[15].value = dbnull.value;
        //        }
        //        else if (model.profileid > 0)
        //        {
        //            paramstostore[0].value = operations.update;
        //            paramstostore[3].direction = parameterdirection.inputoutput;
        //            paramstostore[3].value = model.profileid;

        //            paramstostore[12] = new sqlparameter("@createddate", sqldbtype.date);
        //            paramstostore[12].value = dbnull.value;
        //            paramstostore[13] = new sqlparameter("@modifieddate", sqldbtype.date);
        //            paramstostore[13].value = datetime.now;
        //            paramstostore[14] = new sqlparameter("@createdby", sqldbtype.bigint);
        //            paramstostore[14].value = dbnull.value;
        //            paramstostore[15] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //            paramstostore[15].value = applicationhelper.userid;
        //        }
        //        paramstostore[1] = new sqlparameter("@p_success", sqldbtype.bit);
        //        paramstostore[1].direction = parameterdirection.output;
        //        paramstostore[2] = new sqlparameter("@p_error_message", sqldbtype.varchar);
        //        paramstostore[2].direction = parameterdirection.output;
        //        paramstostore[2].size = -1;



        //        paramstostore[4] = new sqlparameter("@customername", sqldbtype.nvarchar);
        //        paramstostore[4].value = model.customername;
        //        paramstostore[4].size = 100;
        //        paramstostore[5] = new sqlparameter("@customercode", sqldbtype.nvarchar);
        //        paramstostore[5].value = model.customercode;
        //        paramstostore[5].size = 100;
        //        paramstostore[6] = new sqlparameter("@customerid", sqldbtype.bigint);
        //        paramstostore[6].value = model.customerid;
        //        paramstostore[7] = new sqlparameter("@owncompanyid", sqldbtype.bigint);
        //        paramstostore[7].value = model.owncompanyid;
        //        paramstostore[8] = new sqlparameter("@paymentterm", sqldbtype.nvarchar);
        //        paramstostore[8].value = model.paymentterm;
        //        paramstostore[8].size = 100;
        //        paramstostore[9] = new sqlparameter("@creditterm", sqldbtype.nvarchar);
        //        paramstostore[9].value = model.creditterm;
        //        paramstostore[9].size = 100;
        //        paramstostore[10] = new sqlparameter("@invoiceformat", sqldbtype.nvarchar);
        //        paramstostore[10].value = model.invoiceformat;
        //        paramstostore[10].size = 500;
        //        paramstostore[11] = new sqlparameter("@ishide", sqldbtype.bit);
        //        paramstostore[11].value = model.ishide;
        //        paramstostore[16] = new sqlparameter("@islaborcharges", sqldbtype.bit);
        //        paramstostore[16].value = model.islaborcharges == null ? false : model.islaborcharges;
        //        paramstostore[17] = new sqlparameter("@isadditionalcharges", sqldbtype.bit);
        //        paramstostore[17].value = model.isadditionalcharges == null ? false : model.isadditionalcharges;

        //        dataaccess.getdatebysp<usp_customerprofile>("usp_customerprofile", paramstostore);
        //        if (convert.toboolean(paramstostore[1].value) == true)
        //        {
        //            if (model.profileid > 0)
        //            {
        //                message = configurationmanager.appsettings["successmessage"].tostring();
        //            }


        //        }
        //        else
        //        {
        //            message = configurationmanager.appsettings["failedmessage"].tostring();
        //            status = "failed";

        //        }

        //    }
        //    catch (exception ex)
        //    {
        //        status = "failed";
        //        // throw;
        //    }
        //    return json(new { status = status, html = html, masterid = paramstostore[3].value, message = message });
        //}
        //[authorize]
        //[httppost]
        //public actionresult addcustomerprofiledetail(usp_customerprofiledetail model, int64 customerid)
        //{
        //    string message = "record inserted successfully!";
        //    string status = "ok";
        //    string form = "customerprofilelist";
        //    string html = string.empty;
        //    string query = "  select cp.customername,cpd.profiledetail  from customerprofile cp  inner join customerprofiledetail cpd on cp.profileid = cpd.profileid";
        //    query = query + " where cp.customerid = " + customerid + "   and cpd.productid = " + model.productid + "   and cpd.locationfrom = " + model.locationfrom + "   and cpd.locationto = " + model.locationto + "";
        //    datatable exist = dataaccess.getdatatablebyquery(query);
        //    if (exist != null)
        //    {
        //        if (exist.rows.count > 0)
        //        {
        //            if (exist.rows.count == 1)
        //            {
        //                if (exist.rows[0][1].tostring() != model.profiledetail.tostring().trim())
        //                {
        //                    message = "record already exists!";
        //                    status = "found";
        //                    return json(new { status = status, html = html, masterid = model.profileid, message = message });
        //                }
        //            }
        //            //message = "record already exists!";
        //            //status = "found";
        //            //return json(new { status = status, html = html, masterid = model.profileid, message = message });
        //        }
        //    }

        //    sqlparameter[] paramstostore = new sqlparameter[21];

        //    try
        //    {

        //        paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //        if (model.profiledetail == 0)
        //        {
        //            paramstostore[0].value = operations.insert;
        //            paramstostore[3] = new sqlparameter("@profiledetail", sqldbtype.bigint);
        //            paramstostore[3].direction = parameterdirection.output;

        //            paramstostore[13] = new sqlparameter("@createddate", sqldbtype.datetime);
        //            paramstostore[13].value = datetime.now;
        //            paramstostore[14] = new sqlparameter("@modifieddate", sqldbtype.datetime);
        //            paramstostore[14].value = dbnull.value;
        //            paramstostore[15] = new sqlparameter("@createdby", sqldbtype.bigint);
        //            paramstostore[15].value = applicationhelper.userid;
        //            paramstostore[16] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //            paramstostore[16].value = dbnull.value;
        //        }
        //        else
        //        {
        //            paramstostore[0].value = operations.update;
        //            paramstostore[3] = new sqlparameter("@profiledetail", sqldbtype.bigint);
        //            paramstostore[3].direction = parameterdirection.inputoutput;
        //            paramstostore[3].value = model.profiledetail;

        //            paramstostore[13] = new sqlparameter("@createddate", sqldbtype.datetime);
        //            paramstostore[13].value = dbnull.value;
        //            paramstostore[14] = new sqlparameter("@modifieddate", sqldbtype.datetime);
        //            paramstostore[14].value = datetime.now;
        //            paramstostore[15] = new sqlparameter("@createdby", sqldbtype.bigint);
        //            paramstostore[15].value = dbnull.value;
        //            paramstostore[16] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //            paramstostore[16].value = applicationhelper.userid;

        //        }

        //        paramstostore[1] = new sqlparameter("@p_success", sqldbtype.bit);
        //        paramstostore[1].direction = parameterdirection.output;
        //        paramstostore[2] = new sqlparameter("@p_error_message", sqldbtype.varchar);
        //        paramstostore[2].direction = parameterdirection.output;
        //        paramstostore[2].size = -1;

        //        paramstostore[4] = new sqlparameter("@profileid", sqldbtype.bigint);
        //        paramstostore[4].value = model.profileid;

        //        paramstostore[5] = new sqlparameter("@productcode", sqldbtype.nvarchar);
        //        paramstostore[5].value = model.productcode;
        //        paramstostore[5].size = 100;
        //        paramstostore[6] = new sqlparameter("@productid", sqldbtype.bigint);
        //        paramstostore[6].value = model.productid;
        //        paramstostore[7] = new sqlparameter("@locationfrom", sqldbtype.bigint);
        //        paramstostore[7].value = model.locationfrom;
        //        paramstostore[8] = new sqlparameter("@locationto", sqldbtype.bigint);
        //        paramstostore[8].value = model.locationto;

        //        paramstostore[9] = new sqlparameter("@ratetype", sqldbtype.nvarchar);
        //        paramstostore[9].value = model.ratetype;
        //        paramstostore[10] = new sqlparameter("@doorsteprate", sqldbtype.bigint);
        //        paramstostore[10].value = model.doorsteprate;


        //        paramstostore[11] = new sqlparameter("@total", sqldbtype.float);
        //        paramstostore[11].value = model.rate + model.doorsteprate;
        //        paramstostore[12] = new sqlparameter("@active", sqldbtype.bigint);
        //        paramstostore[12].value = 0;
        //        paramstostore[17] = new sqlparameter("@rate", sqldbtype.float);
        //        paramstostore[17].value = model.rate;
        //        paramstostore[18] = new sqlparameter("@weightperunit", sqldbtype.float);
        //        paramstostore[18].value = model.weightperunit;
        //        paramstostore[19] = new sqlparameter("@additioncharges", sqldbtype.float);
        //        paramstostore[19].value = model.additioncharges;
        //        paramstostore[20] = new sqlparameter("@labourcharges", sqldbtype.float);
        //        paramstostore[20].value = model.labourcharges;
        //        dataaccess.getdatebysp<usp_customerprofiledetail>("usp_customerprofiledetail", paramstostore);
        //        if (convert.toboolean(paramstostore[1].value) == true)
        //        {
        //            list<usp_customerprofiledetail> detail = new list<usp_customerprofiledetail>();
        //            detail = dataaccess.getdatafromspbyid<usp_customerprofiledetail>("usp_customerprofiledetail", "profileid", convert.toint64(model.profileid));
        //            if (model.profiledetail > 0)
        //            {
        //                message = configurationmanager.appsettings["successmessage"].tostring();
        //            }
        //            html = renderrazorviewtostring("admin/_customerprofiledetail", detail, this);

        //        }
        //        else
        //        {
        //            message = configurationmanager.appsettings["failedmessage"].tostring();
        //            status = "failed";

        //        }


        //    }
        //    catch (exception)
        //    {

        //        throw;
        //    }
        //    return json(new { status = status, form = form, html = html, masterid = paramstostore[3].value, message = message });
        //}
        //public actionresult customerprofilelist()
        //{
        //    list<usp_customerprofile> model = new list<usp_customerprofile>();
        //    try
        //    {
        //        model = dataaccess.getdatebysp<usp_customerprofile>("usp_customerprofile");
        //        if (model == null)
        //            model = new list<usp_customerprofile>();
        //    }
        //    catch (exception)
        //    {

        //        throw;
        //    }
        //    return view(model);
        //}
        //[authorize]
        //[httpget]
        //public actionresult customerprofilelistaudit(string guidid)
        //{
        //    int64 auditlogid = 0;
        //    auditlogid = convert.toint64(utility.base64decode(guidid));
        //    list<usp_customerprofiledetail_audit> model = new list<usp_customerprofiledetail_audit>();
        //    try
        //    {
        //        model = dataaccess.getdatafromspbyid<usp_customerprofiledetail_audit>("usp_customerprofiledetail_audit", "profiledetailid", auditlogid);
        //        if (model == null)
        //            model = new list<usp_customerprofiledetail_audit>();
        //    }
        //    catch (exception)
        //    {

        //        throw;
        //    }
        //    return view(model);
        //}
        //#endregion

        //#region receiver
        //public actionresult receiverindex()
        //{
        //    list<usp_mcbbranches> model = new list<usp_mcbbranches>();
        //    try
        //    {
        //        model = dataaccess.getdatebysp<usp_mcbbranches>("usp_mcbbranches");
        //        if (model == null)
        //            model = new list<usp_mcbbranches>();
        //    }
        //    catch (exception)
        //    {

        //        throw;
        //    }
        //    return view(model);
        //}
        //[authorize]
        //[httppost]
        //public actionresult updatereceiver(usp_mcbbranches model)
        //{
        //    string html = "";
        //    string status = "ok";
        //    string message = "record inserted successfully!.";
        //    list<usp_mcbbranches> _model = new list<usp_mcbbranches>();
        //    try
        //    {
        //        sqlparameter[] paramstostore = new sqlparameter[13];
        //        paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //        if (model.id > 0)
        //            paramstostore[0].value = operations.update;
        //        else
        //            paramstostore[0].value = operations.insert;
        //        paramstostore[1] = new sqlparameter("@p_success", sqldbtype.bit);
        //        paramstostore[1].direction = parameterdirection.output;
        //        paramstostore[2] = new sqlparameter("@p_error_message", sqldbtype.varchar);
        //        paramstostore[2].direction = parameterdirection.output;
        //        paramstostore[2].size = -1;
        //        paramstostore[3] = new sqlparameter("@id", sqldbtype.bigint);
        //        if (model.id > 0)
        //        {
        //            paramstostore[3].direction = parameterdirection.input;
        //            paramstostore[3].value = model.id;
        //        }
        //        else
        //        {
        //            paramstostore[3].direction = parameterdirection.output;
        //            paramstostore[3].value = model.id;
        //        }
        //        paramstostore[4] = new sqlparameter("@br_code", sqldbtype.varchar);
        //        paramstostore[4].value = model.br_code;
        //        paramstostore[4].size = 10;
        //        paramstostore[5] = new sqlparameter("@branchname", sqldbtype.nvarchar);
        //        paramstostore[5].value = model.branchname;
        //        paramstostore[5].size = 500;
        //        paramstostore[6] = new sqlparameter("@contactno", sqldbtype.varchar);
        //        paramstostore[6].value = model.contactno;
        //        paramstostore[6].size = 50;
        //        paramstostore[7] = new sqlparameter("@emailid", sqldbtype.varchar);
        //        paramstostore[7].value = model.emailid;
        //        paramstostore[7].size = 50;
        //        paramstostore[8] = new sqlparameter("@active", sqldbtype.bit);
        //        paramstostore[8].value = model.active;
        //        if (model.id == 0)
        //        {
        //            paramstostore[9] = new sqlparameter("@createdby", sqldbtype.bigint);
        //            paramstostore[9].value = applicationhelper.userid;
        //            paramstostore[10] = new sqlparameter("@createddate", sqldbtype.datetime);
        //            paramstostore[10].value = datetime.now;
        //            paramstostore[11] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //            paramstostore[11].value = dbnull.value;
        //            paramstostore[12] = new sqlparameter("@modifieddate", sqldbtype.datetime);
        //            paramstostore[12].value = dbnull.value;
        //        }
        //        else
        //        {
        //            paramstostore[9] = new sqlparameter("@createdby", sqldbtype.bigint);
        //            paramstostore[9].value = dbnull.value;
        //            paramstostore[10] = new sqlparameter("@createddate", sqldbtype.datetime);
        //            paramstostore[10].value = dbnull.value;
        //            paramstostore[11] = new sqlparameter("@modifiedby", sqldbtype.bigint);
        //            paramstostore[11].value = applicationhelper.userid;
        //            paramstostore[12] = new sqlparameter("@modifieddate", sqldbtype.datetime);
        //            paramstostore[12].value = datetime.now;
        //        }

        //        dataaccess.getdatebysp<usp_mcbbranches>("usp_mcbbranches", paramstostore);
        //        _model = dataaccess.getdatebysp<usp_mcbbranches>("usp_mcbbranches");


        //    }
        //    catch (exception ex)
        //    {
        //        status = "";
        //        utility.applogentry("update receiver" + ex.message.tostring());
        //    }
        //    html = renderrazorviewtostring("admin/_receiverlist", _model, this);
        //    var jsonresult = json(new { html = html, form = "receiverform", status = status, message = message });
        //    jsonresult.maxjsonlength = int.maxvalue;
        //    return jsonresult;
        //}

        //#endregion

        //#region person


        //public actionresult departmentperson()
        //{
        //    usp_departmentperson model = new usp_departmentperson();
        //    model.personalid = 0;
        //    if (request.isajaxrequest())
        //    {
        //        return partialview("~/views/admin/departmentperson.cshtml", model);
        //    }
        //    return view(model);
        //}

        //[httppost]
        //public actionresult personlist()
        //{
        //    list<usp_departmentperson> _person = new list<usp_departmentperson>();
        //    string message = "record inserted successfully!";
        //    string status = "ok";
        //    string form = "person";
        //    string html = string.empty;
        //    try
        //    {
        //        _person = dataaccess.getdatebysp<usp_departmentperson>("usp_departmentperson");
        //    }
        //    catch (exception)
        //    {
        //        status = "fail";
        //        message = "record fetching failed.";
        //    }
        //    html = renderrazorviewtostring("admin/_departmentperson", _person, this);

        //    return json(new { status = status, html = html, message = message });
        //}

        //[httppost]

        //public jsonresult personcreate(departmentperson model)
        //{
        //    string message = "record inserted successfully!";
        //    string status = "ok";
        //    string form = "person";
        //    list<usp_departmentperson> model = new list<usp_departmentperson>();
        //    string html = string.empty;// renderrazorviewtostring("_personlist", department, this);
        //    try
        //    {
        //        if (modelstate.isvalid)
        //        {

        //            sqlparameter[] paramstostore = new sqlparameter[20];

        //            paramstostore[1] = new sqlparameter("@p_success", sqldbtype.bit);
        //            paramstostore[1].direction = parameterdirection.output;
        //            paramstostore[18] = new sqlparameter("@isindividual", sqldbtype.bit);
        //            paramstostore[18].value = model.isindividual;
        //            paramstostore[19] = new sqlparameter("@active", sqldbtype.bit);
        //            paramstostore[19].value = model.active;
        //            if (model.personalid == 0)
        //            {
        //                paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //                paramstostore[0].value = operations.insert; paramstostore[3] = new sqlparameter("@personalid", sqldbtype.bigint);
        //                paramstostore[3].direction = parameterdirection.output;
        //                paramstostore[3] = new sqlparameter("@personalid", sqldbtype.bigint);
        //                paramstostore[3].direction = parameterdirection.output;
        //            }
        //            else
        //            {
        //                paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //                paramstostore[0].value = operations.update;
        //                paramstostore[3] = new sqlparameter("@personalid", sqldbtype.bigint);
        //                paramstostore[3].direction = parameterdirection.inputoutput;
        //                paramstostore[3].value = model.personalid;
        //            }

        //            paramstostore[8] = new sqlparameter("@groupid", sqldbtype.bigint);
        //            paramstostore[8].value = model.groupid;
        //            paramstostore[9] = new sqlparameter("@companyid", sqldbtype.bigint);
        //            paramstostore[9].value = model.companyid;
        //            paramstostore[10] = new sqlparameter("@departmentid", sqldbtype.bigint);
        //            paramstostore[10].value = model.departmentid;
        //            paramstostore[2] = new sqlparameter("@p_error_message", sqldbtype.varchar);
        //            paramstostore[2].direction = parameterdirection.output;
        //            paramstostore[2].size = -1;
        //            paramstostore[4] = new sqlparameter("@code", sqldbtype.nvarchar);
        //            paramstostore[4].value = model.code;
        //            paramstostore[4].size = 100;
        //            paramstostore[5] = new sqlparameter("@name", sqldbtype.nvarchar);
        //            paramstostore[5].value = model.name;
        //            paramstostore[5].size = 100;
        //            paramstostore[6] = new sqlparameter("@email", sqldbtype.nvarchar);
        //            paramstostore[6].value = model.email;
        //            paramstostore[6].size = 100;
        //            paramstostore[7] = new sqlparameter("@businessemail", sqldbtype.nvarchar);
        //            paramstostore[7].value = model.businessemail;
        //            paramstostore[7].size = 100;
        //            paramstostore[11] = new sqlparameter("@designation", sqldbtype.nvarchar);
        //            paramstostore[11].value = model.designation;
        //            paramstostore[11].size = 100;
        //            paramstostore[12] = new sqlparameter("@cell", sqldbtype.nvarchar);
        //            paramstostore[12].value = model.cell;
        //            paramstostore[12].size = 100;
        //            paramstostore[13] = new sqlparameter("@phoneno", sqldbtype.nvarchar);
        //            paramstostore[13].value = model.phoneno;
        //            paramstostore[13].size = 100;
        //            paramstostore[14] = new sqlparameter("@othercontact", sqldbtype.nvarchar);
        //            paramstostore[14].value = model.othercontact;
        //            paramstostore[14].size = 100;
        //            paramstostore[15] = new sqlparameter("@addressoffice", sqldbtype.nvarchar);
        //            paramstostore[15].value = model.addressoffice;
        //            paramstostore[15].size = 510;
        //            paramstostore[16] = new sqlparameter("@addressother", sqldbtype.nvarchar);
        //            paramstostore[16].value = model.addressother;
        //            paramstostore[16].size = 510;
        //            paramstostore[17] = new sqlparameter("@description", sqldbtype.nvarchar);
        //            paramstostore[17].value = model.description;
        //            paramstostore[17].size = 510;
        //            dataaccess.getdatebysp<usp_departmentperson>("usp_departmentperson", paramstostore);
        //            if (convert.toboolean(paramstostore[1].value) == true)
        //            {
        //                if (model.personalid > 0)
        //                {

        //                }
        //            }
        //            model = dataaccess.getdatebysp<usp_departmentperson>("usp_departmentperson");

        //            //return redirecttoaction("personview");
        //        }
        //        list<getallperson_result> department = new list<getallperson_result>();
        //        using (var db = new lcmentities())
        //        {
        //            department = db.getallperson().tolist();
        //        }
        //        html = renderrazorviewtostring("admin/_departmentperson", model, this);
        //    }
        //    catch (exception)
        //    {
        //        status = "fail";

        //    }
        //    return json(new { status = status, form = form, html = html, message = message });
        //}

        //[httppost]
        //public actionresult persondelete(long? id)
        //{
        //    string html = string.empty;
        //    string message = "record deleted successfully!.";
        //    string status = "ok";
        //    string form = "person";
        //    list<getallperson_result> department = new list<getallperson_result>();
        //    try
        //    {
        //        using (var db = new lcmentities())
        //        {
        //            departmentperson person = db.departmentpersons.firstordefault(p => p.personalid == id);
        //            if (person != null)
        //            {
        //                person.active = false;
        //            }
        //            db.savechanges();
        //        }
        //        using (var db = new lcmentities())
        //        {
        //            department = db.getallperson().tolist();
        //        }
        //        html = renderrazorviewtostring("_personlist", department, this);

        //    }
        //    catch (exception)
        //    {
        //        status = "fail";
        //        message = "record deleted failed.please try again.";
        //    }
        //    return json(new { status = status, form = form, html = html, message = message });


        //}

        ////public static string renderrazorviewtostring(string viewname, object model, controller currentcontroller)
        ////{
        ////    viewdatadictionary viewdata = new viewdatadictionary();
        ////    try
        ////    {


        ////        viewdata.model = model;
        ////        using (var sw = new stringwriter())
        ////        {
        ////            var viewresult = viewengines.engines.findpartialview(currentcontroller.controllercontext, viewname);
        ////            var viewcontext = new viewcontext(currentcontroller.controllercontext, viewresult.view, viewdata, currentcontroller.tempdata, sw);
        ////            viewresult.view.render(viewcontext, sw);
        ////            viewresult.viewengine.releaseview(currentcontroller.controllercontext, viewresult.view);
        ////            return sw.getstringbuilder().tostring();
        ////        }
        ////    }
        ////    catch
        ////    {


        ////    }
        ////    return "";
        ////}
        //#endregion

        //#region transporter
        //public actionresult transporter()
        //{
        //    adminmodel model = new adminmodel();

        //    model.lststation = dataaccess.getdatebysp<usp_stations>("usp_stations");
        //    if (model.lststation == null)
        //    {
        //        model.lststation = new list<usp_stations>();
        //    }
        //    model.lstcity = dataaccess.getdatebysp<usp_city>("usp_city");
        //    selectlist list = new selectlist(model.lstcity, "cityid", "cityname");
        //    viewbag.city = list;
        //    return view(model);
        //}
        //[httppost]
        //public actionresult transporter(usp_stations model)
        //{
        //    string message = "record inserted successfully!";
        //    string status = "ok";
        //    string form = "station";
        //    string html = string.empty;
        //    list<usp_stations> _station = new list<usp_stations>();
        //    sqlparameter[] paramstostore = new sqlparameter[18];

        //    if (model.id > 0)
        //    {
        //        paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //        paramstostore[0].value = operations.update;
        //        paramstostore[3] = new sqlparameter("@id", sqldbtype.bigint);
        //        paramstostore[3].direction = parameterdirection.input;
        //        paramstostore[3].value = model.id;
        //        paramstostore[8] = new sqlparameter("@datecreated", sqldbtype.datetime);
        //        paramstostore[8].value = dbnull.value;
        //        paramstostore[9] = new sqlparameter("@datemodified", sqldbtype.datetime);
        //        paramstostore[9].value = datetime.now;
        //        paramstostore[14] = new sqlparameter("@createdbyid", sqldbtype.bigint);
        //        paramstostore[14].value = dbnull.value;
        //        paramstostore[15] = new sqlparameter("@modifiedbyid", sqldbtype.bigint);
        //        paramstostore[15].value = applicationhelper.userid;
        //    }
        //    else
        //    {
        //        paramstostore[0] = new sqlparameter("@action_type", sqldbtype.int);
        //        paramstostore[0].value = operations.insert;
        //        paramstostore[3] = new sqlparameter("@id", sqldbtype.bigint);
        //        paramstostore[3].direction = parameterdirection.output;
        //        paramstostore[8] = new sqlparameter("@datecreated", sqldbtype.datetime);
        //        paramstostore[8].value = datetime.now;
        //        paramstostore[9] = new sqlparameter("@datemodified", sqldbtype.datetime);
        //        paramstostore[9].value = dbnull.value;
        //        paramstostore[14] = new sqlparameter("@createdbyid", sqldbtype.bigint);
        //        paramstostore[14].value = applicationhelper.userid;
        //        paramstostore[15] = new sqlparameter("@modifiedbyid", sqldbtype.bigint);
        //        paramstostore[15].value = dbnull.value;

        //    }
        //    paramstostore[1] = new sqlparameter("@p_success", sqldbtype.bit);
        //    paramstostore[1].direction = parameterdirection.output;
        //    paramstostore[13] = new sqlparameter("@isactive", sqldbtype.bit);
        //    paramstostore[13].value = model.isactive == null ? false : model.isactive;
        //    paramstostore[6] = new sqlparameter("@cityid", sqldbtype.bigint);
        //    paramstostore[6].value = model.cityid;
        //    paramstostore[17] = new sqlparameter("@contactno", sqldbtype.bigint);
        //    paramstostore[17].value = model.contactno;
        //    paramstostore[10] = new sqlparameter("@secondarycontactno", sqldbtype.bigint);
        //    paramstostore[10].value = model.secondarycontactno;

        //    paramstostore[2] = new sqlparameter("@p_error_message", sqldbtype.varchar);
        //    paramstostore[2].direction = parameterdirection.output;
        //    paramstostore[2].size = -1;
        //    paramstostore[4] = new sqlparameter("@code", sqldbtype.nvarchar);
        //    paramstostore[4].value = model.code;
        //    paramstostore[4].size = 100;
        //    paramstostore[5] = new sqlparameter("@name", sqldbtype.nvarchar);
        //    paramstostore[5].value = model.name;
        //    paramstostore[5].size = 100;
        //    paramstostore[7] = new sqlparameter("@contactperson", sqldbtype.nvarchar);
        //    paramstostore[7].value = model.contactperson;
        //    paramstostore[7].size = 100;
        //    paramstostore[16] = new sqlparameter("@secondarycontactperson", sqldbtype.nvarchar);
        //    paramstostore[16].value = model.secondarycontactperson;
        //    paramstostore[16].size = 100;
        //    paramstostore[11] = new sqlparameter("@address", sqldbtype.nvarchar);
        //    paramstostore[11].value = model.address == null ? "" : model.address;
        //    paramstostore[11].size = 200;
        //    paramstostore[12] = new sqlparameter("@description", sqldbtype.nvarchar);
        //    paramstostore[12].value = model.description == null ? "" : model.description.tostring();
        //    paramstostore[12].size = 200;
        //    dataaccess.getdatebysp<usp_stations>("usp_stations", paramstostore);
        //    if (convert.toboolean(paramstostore[1].value) == true)
        //    {
        //        if (model.id > 0)
        //        {
        //            message = configurationmanager.appsettings["successmessage"].tostring();
        //        }
        //    }
        //    else
        //    {

        //    }
        //    _station = dataaccess.getdatebysp<usp_stations>("usp_stations");

        //    html = renderrazorviewtostring("admin/_transporter", _station, this);
        //    var jsonresult = json(new { status = status, html = html, message = message, form = form }, jsonrequestbehavior.allowget);
        //    jsonresult.maxjsonlength = int.maxvalue;
        //    return jsonresult;
        //    // return json();
        //}
        //#endregion

        #region Compact Bill 
        [HttpGet]
        public ActionResult CompactBill()
        {
            ViewBag.Vehicle = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "VehicleID", "RegNo");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID), "ID", "Name");
            ViewBag.NewSalesOrderNo = GetNewSalesOrderNo();
            var PickLocation = context.Bilties.Where(x => (x.PickLocation1.LocationType == "Pick" || x.PickLocation1.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.PickLocation1.ID, LocationCode = string.Format("{0} | {1}", x.PickLocation1.LocationCode, x.PickLocation1.LocationName) }).Distinct().ToList();
            ViewBag.PickLocation = new SelectList(PickLocation, "ID", "LocationCode");
            var DropLocation = context.Bilties.Where(x => (x.PickLocation2.LocationType == "Drop" || x.PickLocation2.LocationType == "Both") && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.PickLocation2.ID, LocationCode = string.Format("{0} | {1}", x.PickLocation2.LocationCode, x.PickLocation2.LocationName) }).Distinct().ToList();
            ViewBag.DropLocation = new SelectList(DropLocation, "ID", "LocationCode");
            var BillTo = context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.BillTo = new SelectList(BillTo, "ID", "Code");
            var bilty = context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            ViewBag.ChallanNo = GetNewChallanNo();
            return View(bilty);
        }

        public ActionResult SearchCompactBill(CompactBiltyModel model)
        {
            string Status = "OK";
            List<Models.Bilty> lstOrders = new List<Models.Bilty>();
            if (model.DateFrom != null && model.DateTo != null && model.Sender != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.OrderNo != null)
            {
                lstOrders = CompactBilty.SearchBiltyAllFilters(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.OrderNo, model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBROS(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBRO(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVBR(model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicle(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateSender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVB(model.Vehicle, model.BillTo).ToList();
            }
            else if (model.Vehicle != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVR(model.Vehicle, model.Receiver).ToList();
            }
            else if (model.Vehicle != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVO(model.Vehicle, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownVS(model.Vehicle, model.Sender).ToList();
            }
            else if (model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBR(model.BillTo, model.Receiver).ToList();
            }
            else if (model.BillTo != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBO(model.BillTo, model.OwnCompany).ToList();
            }
            else if (model.BillTo != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownBS(model.BillTo, model.Sender).ToList();
            }
            else if (model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownRO(model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Receiver != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDropDownRS(model.Receiver, model.Sender).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiverOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = CompactBilty.SearchBiltyDateVehicleBillToReceiverOwnCompanySender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBilty(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.Bilties.Where(x => x.BillTo == model.BillTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.Bilties.Where(x => x.BillTo == x.BillTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.Bilties.Where(x => x.Reciever == model.Receiver && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.Bilties.Where(x => x.PickLocation == x.PickLocation && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Sender != null)
            {
                lstOrders = context.Bilties.Where(x => x.PickLocation == model.Sender && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Sender != null)
            {
                lstOrders = context.Bilties.Where(x => x.Reciever == x.Reciever && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.Bilties.Where(x => x.OwnCompany == model.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.Bilties.Where(x => x.OwnCompany == x.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.Bilties.Where(x => x.VehicleID == model.Vehicle && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.Bilties.Where(x => x.VehicleID == x.VehicleID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OrderNo != null)
            {
                lstOrders = context.Bilties.Where(x => x.OrderNo == model.OrderNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            else
            {
                lstOrders = context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_SearchCompactBill", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        public long BillNo()
        {
            long? BillNo = dml.BillNo().Count <= 0 ? 0 : dml.BillNo()[0].BillNo;
            BillNo = BillNo + 1;
            return Convert.ToInt64(BillNo);
        }
        [HttpPost]
        public ActionResult SaveCompactBill(List<CompactCheckBill> check, CompactBill Order)
        {
            string message = "";
            string status = "OK";

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    long OrderID = 0;

                    if (Order != null)
                    {
                        var duplicate = context.CompactBills.Where(x => x.ReferenceNo == Order.ReferenceNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                        if (Order.ID > 0)
                        {
                            if (duplicate.Count <= 1)
                            {
                                var record = context.CompactBills.FirstOrDefault(x => x.ID == Order.ID);
                                if (record != null)
                                {
                                    Order.BillNo = record.BillNo;
                                    Order.CreatedDate = record.CreatedDate;
                                }
                                context.CompactCheckBills.RemoveRange(context.CompactCheckBills.Where(x => x.CompactBillID == Order.ID).ToList());
                                Order.ModifiedBy = ApplicationHelper.UserID;
                                Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                                Order.IsActive = true;
                                Order.ModifiedDate = DateTime.Now;
                                var local = context.Set<LiquadCargoManagment.Models.CompactBill>().Local.FirstOrDefault(x => x.ID == x.ID);
                                if (local != null)
                                {
                                    context.Entry(local).State = EntityState.Detached;
                                }
                                context.Entry(Order).State = EntityState.Modified;
                                OrderID = Order.ID;
                            }
                            else
                            {
                                message = "Reference No already exist";
                                status = "Duplicate";
                            }
                        }

                        else
                        {
                            if (duplicate.Count <= 0)
                            {
                                Order.CreatedBy = ApplicationHelper.UserID;
                                Order.CreatedDate = DateTime.Now;
                                Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                                Order.BillNo = BillNo();
                                Order.IsActive = true;
                                context.CompactBills.Add(Order);
                                context.SaveChanges();
                                OrderID = Order.ID;
                            }
                            else
                            {
                                message = "Reference No already exist";
                                status = "Duplicate";
                            }
                        }

                        if (check != null)
                        {
                            foreach (CompactCheckBill item in check)
                            {
                                item.CompactBillID = OrderID;
                                context.CompactCheckBills.Add(item);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            message = "Bilty not selected";
                        }
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    message = "Compact Bill has been created";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    status = ex.GetType().Name;
                    message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }

                }
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditCompactBill(int id)
        {
            string Message = "";
            string Status = "OK";
            CompactBillModal Bill = new CompactBillModal();
            List<CompactBillMeta> ch = new List<CompactBillMeta>();
            try
            {
                if (id > 0)
                {
                    var z = context.CompactBills.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    Bill.ID = z.ID;
                    Bill.ReferenceNo = z.ReferenceNo;
                    Bill.ReportType = z.ReportType;
                    Bill.BillingDate = Convert.ToDateTime(z.BillingDate).ToShortDateString();

                    ch = context.CompactCheckBills.Where(x => x.CompactBillID == id).Select(x => new CompactBillMeta
                    {
                        ID = x.BiltyID,
                        OrderNo = x.Bilty.OrderNo,
                        OrderDate = x.Bilty.OrderDate,
                        BillTo = x.Bilty.CustomerCompany.Name,
                        CustomerType = x.Bilty.CustomerType,
                        Quantity = x.Bilty.OrderDetailsTotalQTY,
                        Weight = x.Bilty.OrderDetailsTotalWeight,
                        TotalFreight = x.Bilty.TotalFreight
                    }).ToList();

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
            return Json(new { Bill = Bill, Check = ch, Message = Message, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompactBillList()
        {
            //ViewBag.BillTo = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).ToList(), "ID", "Name");
            ViewBag.BillTo = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            var list = context.CompactBills.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID) && x.IsDeleted != true).OrderByDescending(x => x.ID).ToList();
            return View(list);
        }

        public JsonResult DeleteCompactBill(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CompactBills.Where(x => x.ID == id && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new Models.CompactBill() { ID = id, IsDeleted = true, DeletedBy = ApplicationHelper.UserID, DeletedDate = DateTime.Now };
                        using (var db = new LCMEntities())
                        {
                            db.CompactBills.Attach(model);
                            db.Entry(model).Property(x => x.IsDeleted).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedBy).IsModified = true;
                            db.Entry(model).Property(x => x.DeletedDate).IsModified = true;
                            db.SaveChanges();
                            Message = "Record Deleted Successfully";
                        }
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
            var Bill = context.CompactBills.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_CompactBill.cshtml", Bill);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public ActionResult SearchCompactBillList(DateTime? DateTo, DateTime? DateFrom, int? BillTo, int? OwnCompany)
        {
            List<CompactBill> lstOrders = new List<CompactBill>();

            if (DateFrom != null && DateTo != null)
            {
                lstOrders = dml.DateSearchBill(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstOrders = dml.DateSearchBill(Convert.ToDateTime(DateFrom), "from");
            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = dml.DateSearchBill(Convert.ToDateTime(DateTo), "to");
            }
            else if (BillTo != null)
            {
                lstOrders = context.CompactBills.Where(x => x.IsDeleted != true && x.CompactCheckBills.Where(s => s.Bilty.BillTo == s.Bilty.BillTo).ToList().Count > 0).ToList();
            }
            else if (BillTo != null)
            {
                lstOrders = context.CompactBills.Where(x => x.IsDeleted != true && x.CompactCheckBills.Where(o => o.Bilty.BillTo == BillTo).ToList().Count > 0).ToList();
            }
            else if (OwnCompany != null)
            {
                lstOrders = context.CompactBills.Where(x => x.IsDeleted != true && x.CompactCheckBills.Where(s => s.Bilty.OwnCompany == OwnCompany).ToList().Count > 0).ToList();
            }
            else if (OwnCompany != null)
            {
                lstOrders = context.CompactBills.Where(x => x.IsDeleted != true && x.CompactCheckBills.Where(s => s.Bilty.OwnCompany == s.Bilty.OwnCompany).ToList().Count > 0).ToList();
            }
            else
            {
                lstOrders = context.CompactBills.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_CompactBill", lstOrders.ToList());

            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GET GST LOCATION SALESORDER
        public JsonResult GetLoadingInformation(int Loading, int Destination)
        {
            string Status = "OK";
            string Message = "";

            string Name = string.Empty;
            var Load = context.PickLocations.Where(x => x.ID == Loading).FirstOrDefault();
            var Dest = context.PickLocations.Where(x => x.ID == Destination).FirstOrDefault();
            var General = context.TaxRateRegistrations.Where(x => x.ProvinceID == Load.City.Province.ProvinceID).FirstOrDefault();

            var LoadingCity = Load.City.CityName;
            var DestCity = Dest.City.CityName;
            var Province = Load.City.Province.ProvinceName;
            var ProvinceDes = Dest.City.Province.ProvinceName;
            var Tax = General.TaxRateName;
            var Percent = General.TaxRatePercent;

            if (Load.City.Province.ProvinceName == Dest.City.Province.ProvinceName && Load.City.CityName == Load.City.CityName)
            {
                Status = "Intra";
            }
            else
            {
                Status = "Inter";
            }
            return Json(new { Message = Message , Status = Status , Province = Province, ProvinceDes = ProvinceDes, LoadingCity = LoadingCity, DestCity = DestCity, Tax = Tax, Percent = Percent},JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDestinationLoadingInformation(int Loading, int Destination)
        {
            string Status = "OK";
            string Message = "";

            string Name = string.Empty;
            var Load = context.PickLocations.Where(x => x.ID == Loading).FirstOrDefault();
            var Dest = context.PickLocations.Where(x => x.ID == Destination).FirstOrDefault();
            var General = context.TaxRateRegistrations.Where(x => x.ProvinceID == Load.City.Province.ProvinceID).FirstOrDefault();

            var LoadingCity = Load.City.CityName;
            var LoadingProvince = Load.City.Province.ProvinceName;
            var Tax = General.TaxRateName;
            var Percent = General.TaxRatePercent;

            if (Load.City.Province.ProvinceName == Dest.City.Province.ProvinceName && Load.City.CityName == Load.City.CityName)
            {
                Status = "Intra";
            }
            else
            {
                Status = "Inter";
            }

            return Json(new { Message = Message, Status = Status, LoadingProvince = LoadingProvince, LoadingCity = LoadingCity, Tax = Tax, Percent = Percent }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProvinceCity(long id)
        {
            var data = dml.getPickLocation(id);
            return Json(new { Province = data.City.Province.ProvinceName ,City = data.City.CityName}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchBankAccounts
        [HttpGet]
        public ActionResult SearchBankAccounts(BankAccountModel model)
        {
            string Status = "OK";
            List<Models.BankAccount> lstOrders = new List<Models.BankAccount>();
            if (model.Bank != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.AccountTitle != null && model.AccountNo != null)
            {
                lstOrders = BankAcc.SearchBankAcAllFilter(model.Bank, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.AccountTitle, model.AccountNo).ToList();
            }
            else if (model.Bank != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.AccountTitle != null)
            {
                lstOrders = BankAcc.SearchBankDateFromToNameCodeAccountTitle(model.Bank, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.AccountTitle).ToList();
            }
            else if (model.Bank != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = BankAcc.SearchBankDateFromToNameCode(model.Bank, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.Bank != null && model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = BankAcc.SearchBankDateFromToName(model.Bank, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Bank != null && model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = BankAcc.SearchBankDateFromToName(model.Bank, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo)).ToList();
            }
            else if (model.Bank != null && model.DateFrom != null)
            {
                lstOrders = BankAcc.SearchBankDateFrom(model.Bank, Convert.ToDateTime(model.DateFrom)).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.AccountTitle != null && model.AccountNo != null)
            {
                lstOrders = BankAcc.SearchBankAcDateFromDateToNameCodeAccountTitleAccountNo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.AccountTitle, model.AccountNo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.AccountTitle != null)
            {
                lstOrders = BankAcc.SearchBankAcDateFromDateToNameCodeAccountTitle(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.AccountTitle).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = BankAcc.SearchBankAcDateFromDateToNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = BankAcc.SearchBankAccountName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = BankAcc.SearchBankAccountCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = BankAcc.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = BankAcc.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = BankAcc.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = BankAcc.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {

                lstOrders = context.BankAccounts.Where(x => x.Name == model.Name && x.Code == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBankAc(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchBankAc(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBankAc(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Name != null)
            {

                lstOrders = context.BankAccounts.Where(x => x.Name == model.Name).ToList();
            }
            else if (model.AccountNo != null)
            {

                lstOrders = context.BankAccounts.Where(x => x.AccountNo == model.AccountNo).ToList();
            }
            else if (model.AccountTitle != null)
            {

                lstOrders = context.BankAccounts.Where(x => x.AccountTitle == model.AccountTitle).ToList();
            }


            else if (model.Code != null)
            {
                lstOrders = context.BankAccounts.Where(x => x.Code == model.Code).ToList();
            }
            else if (model.Bank != null)
            {
                lstOrders = context.BankAccounts.Where(x => x.BankID == model.Bank).ToList();

            }
            else
            {
                lstOrders = context.BankAccounts.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_BankAccounts", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchBank
        [HttpGet]
        public ActionResult SearchBanks(BanksModel model)
        {
            string Status = "OK";
            List<Models.Bank> lstOrders = new List<Models.Bank>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = BankClass.SearchBankAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = BankClass.SearchBankName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = BankClass.SearchBankCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = BankClass.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = BankClass.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = BankClass.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = BankClass.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {

                lstOrders = context.Banks.Where(x => x.Name == model.Name && x.Code == model.Code).ToList();
            }
            else if (model.Name != null)
            {

                lstOrders = context.Banks.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBank(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchBank(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchBank(Convert.ToDateTime(model.DateTo), "to");
            }


            else if (model.Code != null)
            {
                lstOrders = context.Banks.Where(x => x.Code == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Banks.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_Bank", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchRevenueType
        [HttpGet]
        public ActionResult SearchRevenueType(RevenueTypeModel model)
        {
            string Status = "OK";
            List<Models.RevenueType> lstOrders = new List<Models.RevenueType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = revenueTypes.SearchRevenueTypeAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = revenueTypes.SearchRevenueTypeName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = revenueTypes.SearchRevenueTypeCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = revenueTypes.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = revenueTypes.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = revenueTypes.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = revenueTypes.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {

                lstOrders = context.RevenueTypes.Where(x => x.Name == model.Name && x.Code == model.Code).ToList();
            }
            else if (model.Name != null)
            {

                lstOrders = context.RevenueTypes.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchRevenueType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchRevenueType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchRevenueType(Convert.ToDateTime(model.DateTo), "to");
            }


            else if (model.Code != null)
            {
                lstOrders = context.RevenueTypes.Where(x => x.Code == model.Code).ToList();
            }
            else
            {
                lstOrders = context.RevenueTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_RevenueType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchRevenue
        [HttpGet]
        public ActionResult SearchRevenue(RevenueModel model)
        {
            string Status = "OK";
            List<Models.Revenue> lstOrders = new List<Models.Revenue>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = revenues.SearchRevenueAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = revenues.SearchRevenueName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = revenues.SearchRevenueCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = revenues.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = revenues.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = revenues.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = revenues.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {

                lstOrders = context.Revenues.Where(x => x.Name == model.Name && x.Code == model.Code).ToList();
            }
            else if (model.Name != null)
            {

                lstOrders = context.Revenues.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchRevenue(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchRevenue(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchRevenue(Convert.ToDateTime(model.DateTo), "to");
            }


            else if (model.Code != null)
            {
                lstOrders = context.Revenues.Where(x => x.Code == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Revenues.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_Revenue", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchProductType
        [HttpGet]
        public ActionResult SearchProductType(ProductTypeModel model)
        {
            string Status = "OK";
            List<Models.Category> lstOrders = new List<Models.Category>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = productType.SearchProductTypeAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = productType.SearchProductName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = productType.SearchProductCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = productType.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = productType.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = productType.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = productType.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {

                lstOrders = context.Categories.Where(x => x.Name == model.Name && x.Code == model.Code).ToList();
            }
            else if (model.Name != null)
            {

                lstOrders = context.Categories.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchProductType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchProductType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchProductType(Convert.ToDateTime(model.DateTo), "to");
            }


            else if (model.Code != null)
            {
                lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Categories.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_ProductType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchDocumentType
        [HttpGet]
        public ActionResult SearchDocumentType(DocumentTypeModel model)
        {
            string Status = "OK";
            List<Models.DocumentType> lstOrders = new List<Models.DocumentType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {

                lstOrders = documentType.SearchDocumentTypeAllFilters(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = documentType.SearchDocumentTypeDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = documentType.SearchDocumentTypeDateCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = documentType.SearchDocumentTypeDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = documentType.SearchDocumentTypeDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = documentType.SearchDocumentTypeDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = documentType.SearchDocumentTypeDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {

                lstOrders = context.DocumentTypes.Where(x => x.Name == model.Name && x.Code == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDocumentType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchDocumentType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDocumentType(Convert.ToDateTime(model.DateTo), "to");
            }

            else if (model.Name != null)
            {

                lstOrders = context.DocumentTypes.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.DocumentTypes.Where(x => x.Code == model.Code).ToList();
            }
            else
            {
                lstOrders = context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_DocumentType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchDocument
        [HttpGet]
        public ActionResult SearchDocument(DocumentModel model)
        {
            string Status = "OK";
            List<Models.Document> lstOrders = new List<Models.Document>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {

                lstOrders = documents.SearchDocumentAllFilters(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = documents.SearchDocumentDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = documents.SearchDocumentDateCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = documents.SearchDocumentDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = documents.SearchDocumentDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = documents.SearchDocumentDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = documents.SearchDocumentDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = documents.SearchDocumentNameCode(model.Name, model.Code).ToList();

            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDocument(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchDocument(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDocument(Convert.ToDateTime(model.DateTo), "to");
            }

            else if (model.Name != null)
            {

                lstOrders = context.Documents.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.Documents.Where(x => x.Code == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Documents.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_Document", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchVehicleStandard
        public ActionResult SearchVehicleStandard(VehicleStandardModel model)
        {
            string Status = "OK";
            List<Models.StandardVehicle> lstOrders = new List<Models.StandardVehicle>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = vehicleStandard.SearchVehicleStandardAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = vehicleStandard.SearchVehicleStandardName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = vehicleStandard.SearchVehicleStandardCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = vehicleStandard.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = vehicleStandard.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = vehicleStandard.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = vehicleStandard.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = vehicleStandard.SearchNameCode(model.Name, model.Code).ToList();

            }


            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchStandardVehicle(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchStandardVehicle(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchStandardVehicle(Convert.ToDateTime(model.DateTo), "to");
            }


            else if (model.Code != null)
            {
                lstOrders = context.StandardVehicles.Where(x => x.Code == model.Code).ToList();
            }
            else if (model.Name != null)
            {

                lstOrders = context.StandardVehicles.Where(x => x.Name == model.Name).ToList();
            }
            else
            {
                lstOrders = context.StandardVehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_VehicleStandard", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchPackageType
        [HttpGet]
        public ActionResult SearchPackageType(PackageTypeModel model)
        {
            string Status = "OK";
            List<Models.PackageType> lstOrders = new List<Models.PackageType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = packageType.SearchPackageTypeAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = packageType.SearchPackageName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = packageType.SearchPackageCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = packageType.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = packageType.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = packageType.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = packageType.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = packageType.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = packageType.getSearchPackageType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.PackageTypes.Where(x => x.PackageTypeName == model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchPackageType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchPackageType(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.PackageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_PackageType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchGeneralTax 
        [HttpGet]
        public ActionResult SearchGeneralTax(GeneralTaxModel model)
        {
            string Status = "OK";
            List<Models.TaxRateRegistration> lstOrders = new List<Models.TaxRateRegistration>();
            if (model.ProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRate != null)
            {
                lstOrders = generalTax.SearchGeneralTaxAllFilter(model.ProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRate).ToList();
            }
            else if (model.ProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = generalTax.SearchGeneralTaxProvinceIDDateFromToName(model.ProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.ProvinceID != null && model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = generalTax.SearchGeneralTaxProvinceIDDateFromTo(model.ProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo)).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRate != null)
            {
                lstOrders = generalTax.SearchGeneralTaxDateFromDateToNameTaxRate(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRate).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = generalTax.SearchGeneralTaxDateFromDateToName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = generalTax.SearchPackageName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.TaxRate != null)
            {
                lstOrders = generalTax.SearchPackageCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.TaxRate).ToList();
            }
            else if (model.DateFrom != null && model.TaxRate != null)
            {
                lstOrders = generalTax.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.TaxRate).ToList();
            }
            else if (model.DateTo != null && model.TaxRate != null)
            {
                lstOrders = generalTax.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.TaxRate).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = generalTax.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = generalTax.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.TaxRate != null)
            {
                lstOrders = generalTax.SearchNameCode(model.Name, model.TaxRate).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = generalTax.getSearchGeneralTax(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchGeneralTaxReg(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchGeneralTaxReg(Convert.ToDateTime(model.DateTo), "to");
            }


            else if (model.Name != null)
            {
                lstOrders = context.TaxRateRegistrations.Where(x => x.TaxRateName == model.Name).ToList();
            }

            else if (model.TaxRate != null)
            {
                lstOrders = context.TaxRateRegistrations.Where(x => x.TaxRateName == model.Name).ToList();
            }
            else if (model.ProvinceID != null)
            {
                lstOrders = context.TaxRateRegistrations.Where(x => x.ProvinceID == model.ProvinceID).ToList();
            }


            else
            {
                lstOrders = context.TaxRateRegistrations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_GeneralTaxes", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchPrincipleCompany
        [HttpGet]
        public ActionResult SearchPrincipleCompany(PrincipleCompanyModel model)
        {
            string Status = "OK";
            List<Models.PrincipleCompany> lstOrders = new List<Models.PrincipleCompany>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = principleCompany.SearchPrincipleAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = principleCompany.SearchPrincipleName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = principleCompany.SearchPrincipleCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = principleCompany.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = principleCompany.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = principleCompany.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = principleCompany.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = principleCompany.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = principleCompany.getSearchPrincipleType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.PrincipleCompanies.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchPrincipleCompany(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchPrincipleCompany(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.PrincipleCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_Principle", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchCity
        [HttpGet]
        public ActionResult SearchCity(CityModel model)
        {
            string Status = "OK";
            List<Models.City> lstOrders = new List<Models.City>();
            if (model.DateFrom != null && model.DateTo != null && model.ProvinceID != null && model.CityName != null && model.CityCode != null)
            {
                lstOrders = city.SearchCityAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.ProvinceID, model.CityName, model.CityCode).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.ProvinceID != null && model.CityName != null)
            {
                lstOrders = city.SearchCityIDName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.ProvinceID, model.CityName).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.ProvinceID != null)
            {
                lstOrders = city.SearchCityID(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.ProvinceID).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.CityName != null)
            {
                lstOrders = city.SearchCityName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.CityName).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.CityCode != null)
            {
                lstOrders = city.SearchCityCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.CityCode).ToList();
            }
            else if (model.ProvinceID != null && model.CityName != null && model.CityCode != null)
            {
                lstOrders = city.SearchCityIDNameCode(model.ProvinceID, model.CityName, model.CityCode).ToList();
            }
            else if (model.ProvinceID != null && model.CityName != null)
            {
                lstOrders = city.SearchCityIDName(model.ProvinceID, model.CityName).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.CityName != null && model.CityCode != null)
            {
                lstOrders = city.SearchCityNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.CityName, model.CityCode).ToList();
            }

            else if (model.DateFrom != null && model.CityCode != null)
            {
                lstOrders = city.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.CityCode).ToList();
            }
            else if (model.DateTo != null && model.CityCode != null)
            {
                lstOrders = city.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.CityCode).ToList();
            }
            else if (model.DateFrom != null && model.CityName != null)
            {
                lstOrders = city.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.CityName).ToList();
            }
            else if (model.DateTo != null && model.CityName != null)
            {
                lstOrders = city.SearchDateToName(Convert.ToDateTime(model.DateTo), model.CityName).ToList();
            }
            else if (model.CityName != null && model.CityCode != null)
            {
                lstOrders = city.SearchNameCode(model.CityName, model.CityCode).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = city.getSearchCity(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.CityName != null)
            {
                lstOrders = context.Cities.Where(x => x.CityName == model.CityName).ToList();
            }
            else if (model.CityCode != null)
            {
                lstOrders = context.Cities.Where(x => x.CityCode == model.CityName).ToList();
            }
            else if (model.ProvinceID != null)
            {
                lstOrders = context.Cities.Where(x => x.ProvinceID == model.ProvinceID).ToList();
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchCity(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchCity(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_City", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
        #region SearchShipmentType 
        [HttpGet]
        public ActionResult SearchShipmentType(ShipmentTypeModel model)
        {
            string Status = "OK";
            List<Models.ShipmentType> lstOrders = new List<Models.ShipmentType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = shipmentType.SearchShipmentAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = shipmentType.SearchShipmentName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = shipmentType.SearchShipmenCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = shipmentType.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = shipmentType.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = shipmentType.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = shipmentType.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = shipmentType.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = shipmentType.getSearchPackageType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.ShipmentTypes.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.ShipmentTypes.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchShipment(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchShipment(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.ShipmentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_PackageType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchExpenseType 
        [HttpGet]
        public ActionResult SearchExpenseType(ExpenseTypeModel model)
        {
            string Status = "OK";
            List<Models.ExpensesType> lstOrders = new List<Models.ExpensesType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = expenseType.SearchExpenseAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = expenseType.SearchExpenseName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = expenseType.SearchExpenseCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = expenseType.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = expenseType.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = expenseType.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = expenseType.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = expenseType.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = expenseType.getSearchExpenseType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.ExpensesTypes.Where(x => x.ExpensesTypeName == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.ExpensesTypes.Where(x => x.ExpensesTypeCode == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchExpenseType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchExpenseType(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.ExpensesTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_ExpenseType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchExpense
        [HttpGet]

        public ActionResult SearchExpense(ExpenseModel model)
        {
            string Status = "OK";
            List<Models.Expense> lstOrders = new List<Models.Expense>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = expenses.SearchExpenseAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = expenses.SearchExpenseName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = expenses.SearchExpenseCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = expenses.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = expenses.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = expenses.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = expenses.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = expenses.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = expenses.getSearchExpense(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.Expenses.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.Expenses.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchExpense(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchExpense(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.Expenses.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_Expense", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchDamageType 
        [HttpGet]

        public ActionResult SearchDamageType(DamageTypeModel model)
        {
            string Status = "OK";
            List<Models.DamageType> lstOrders = new List<Models.DamageType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = damageType.SearchDamageTypeAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = damageType.SearchDamageTypeName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = damageType.SearchDamageTypeCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = damageType.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = damageType.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = damageType.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = damageType.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = damageType.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = damageType.getSearchDamageType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.DamageTypes.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.DamageTypes.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchDamageType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDamageType(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.DamageTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_DamageType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchDamage
        [HttpGet]

        public ActionResult SearchDamage(DamageTypeModel model)
        {
            string Status = "OK";
            List<Models.Damage> lstOrders = new List<Models.Damage>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = damage.SearchDamageAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = damage.SearchDamageName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = damage.SearchDamageCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = damage.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = damage.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = damage.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = damage.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = damage.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = damage.getSearchDamage(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.Damages.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.Damages.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchDamage(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDamage(Convert.ToDateTime(model.DateTo), "to");
            }


            //else if (model.Code != null)
            //{
            //    lstOrders = context.Categories.Where(x => x.Code == model.Code).ToList();
            //}
            else
            {
                lstOrders = context.Damages.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_Damage", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchDriverType
        [HttpGet]

        public ActionResult SearchDriverType(DriverTypeModel model)
        {
            string Status = "OK";
            List<Models.DriverType> lstOrders = new List<Models.DriverType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = driverType.SearchDriverTypeAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = driverType.SearchDriverTypeName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = driverType.SearchDriverTypeCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = driverType.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = driverType.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = driverType.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = driverType.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = driverType.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = driverType.getSearchDriverType(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.Name != null)
            {
                lstOrders = context.DriverTypes.Where(x => x.Name == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.DriverTypes.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchDriverType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDriverType(Convert.ToDateTime(model.DateTo), "to");
            }

            else
            {
                lstOrders = context.DriverTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_DriverType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchOwnCompany
        public ActionResult SearchOwnCompany(OwnCompanyModel model)
        {

            string Status = "OK";
            List<Models.OwnCompany> lstOrders = new List<Models.OwnCompany>();
            try
            {
                if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null && model.WebAdd != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID && x.GroupID == model.GroupID && x.Code == model.Code && x.Name == model.Name && x.Contact == model.Contact && x.ContactOther == model.ContactOther && x.EmailAdd == model.EmailAdd && x.WebAdd == model.WebAdd).ToList();
                }

                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID && x.GroupID == model.GroupID && x.Code == model.Code && x.Name == model.Name && x.Contact == model.Contact && x.ContactOther == model.ContactOther && x.EmailAdd == model.EmailAdd).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID && x.GroupID == model.GroupID && x.Code == model.Code && x.Name == model.Name && x.Contact == model.Contact && x.ContactOther == model.ContactOther).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID && x.GroupID == model.GroupID && x.Code == model.Code && x.Name == model.Name && x.Contact == model.Contact).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID && x.GroupID == model.GroupID && x.Code == model.Code && x.Name == model.Name).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID && x.GroupID == model.GroupID && x.Code == model.Code).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID && x.GroupID == model.GroupID).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo && x.SubcriptionID == model.SubcriptionID).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null && model.DateTo != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom && x.CreatedDate == model.DateTo).ToList();
                }
                else if (model.RoleId != null && model.DateFrom != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0 && x.CreatedDate == model.DateFrom).ToList();
                }

                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null && model.WebAdd != null)
                {
                    lstOrders = ownCompany.SearchDateIDCodeNameContactEmailWeb(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther, model.EmailAdd, model.WebAdd).ToList();

                }
                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null)
                {
                    lstOrders = ownCompany.SearchDateIDCodeNameContactEmail(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther, model.EmailAdd).ToList();

                }
                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null)
                {
                    lstOrders = ownCompany.SearchDateIDCodeNameContactOther(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther).ToList();
                }
                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null)
                {
                    lstOrders = ownCompany.SearchDateIDCodeNameContact(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact).ToList();
                }
                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null)
                {
                    lstOrders = ownCompany.SearchDateIDCodeName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID, model.GroupID, model.Code, model.Name).ToList();
                }
                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null && model.Code != null)
                {
                    lstOrders = ownCompany.SearchDateIDCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID, model.GroupID, model.Code).ToList();
                }
                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null && model.GroupID != null)
                {
                    lstOrders = ownCompany.SearchDateGroupID(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID, model.GroupID).ToList();
                }
                else if (model.DateFrom != null && model.DateTo != null && model.SubcriptionID != null)
                {
                    lstOrders = ownCompany.SearchDateSubscriptionID(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.SubcriptionID).ToList();
                }

                else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
                }
                else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
                }

                else if (model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null && model.WebAdd != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContactAdd(model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther, model.EmailAdd, model.WebAdd).ToList();

                }
                else if (model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContactEmailAdd(model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther, model.EmailAdd).ToList();
                }
                else if (model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContactOther(model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther).ToList();
                }
                else if (model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContact(model.SubcriptionID, model.GroupID, model.Code, model.Name, model.Contact).ToList();
                }
                else if (model.SubcriptionID != null && model.GroupID != null && model.Code != null && model.Name != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCodeName(model.SubcriptionID, model.GroupID, model.Code, model.Name).ToList();
                }
                else if (model.SubcriptionID != null && model.GroupID != null && model.Code != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCode(model.SubcriptionID, model.GroupID, model.Code).ToList();
                }
                else if (model.SubcriptionID != null && model.GroupID != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyID(model.SubcriptionID, model.GroupID).ToList();
                }

                else if (model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null && model.WebAdd != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContactAdd(model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther, model.EmailAdd, model.WebAdd).ToList();
                }
                else if (model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContact(model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther, model.EmailAdd).ToList();
                }
                else if (model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContactOther(model.GroupID, model.Code, model.Name, model.Contact, model.ContactOther).ToList();
                }
                else if (model.GroupID != null && model.Code != null && model.Name != null && model.Contact != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCodeContact(model.GroupID, model.Code, model.Name, model.Contact).ToList();
                }
                else if (model.GroupID != null && model.Code != null && model.Name != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDNameCode(model.GroupID, model.Code, model.Name).ToList();
                }
                else if (model.GroupID != null && model.Code != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCode(model.GroupID, model.Code).ToList();
                }

                else if (model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null && model.WebAdd != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCode(model.GroupID, model.Code).ToList();
                }
                else if (model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null && model.EmailAdd != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCode(model.GroupID, model.Code).ToList();
                }
                else if (model.Code != null && model.Name != null && model.Contact != null && model.ContactOther != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCode(model.GroupID, model.Code).ToList();
                }
                else if (model.Code != null && model.Name != null && model.Contact != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCode(model.GroupID, model.Code).ToList();
                }
                else if (model.Code != null && model.Name != null)
                {
                    lstOrders = ownCompany.SearchOwnComanyIDCode(model.GroupID, model.Code).ToList();
                }
                else if (model.DateFrom != null && model.DateTo == null)
                {
                    lstOrders = ownCompany.getSearchOwnCompany(Convert.ToDateTime(model.DateFrom), "from");
                }
                else if (model.DateFrom == null && model.DateTo != null)
                {
                    lstOrders = ownCompany.getSearchOwnCompany(Convert.ToDateTime(model.DateTo), "to");
                }
                else if (model.Code != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => x.Code == model.Code).ToList();
                }
                else if (model.Name != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => x.Name == model.Name).ToList();
                }
                else if (model.RoleId != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => lstAssignedCompanies.Contains(x.ID) && x.UserAccounts.Where(s => s.RoleID == model.RoleId).ToList().Count > 0).ToList();
                }
                else if (model.GroupID != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => x.GroupID == model.GroupID).ToList();
                }
                else if (model.SubcriptionID != null)
                {
                    lstOrders = context.OwnCompanies.Where(x => x.SubcriptionID == model.SubcriptionID).ToList();
                }
                else
                {
                    lstOrders = context.OwnCompanies.ToList();
                }

            }
            catch (Exception)
            {


                Status = "Exception";
            }

            string view = RenderPartialToString("_OwnCompany", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchDriver
        public ActionResult SearchDriver(DriverModel model)
        {
            string Status = "OK";
            List<Models.Driver> lstOrders = new List<Models.Driver>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.ContactNo != null && model.NIC != null && model.LicenseNo != null)
            {
                lstOrders = driver.SearchDriverAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.ContactNo, model.NIC, model.LicenseNo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.ContactNo != null && model.NIC != null)
            {
                lstOrders = driver.SearchDriverDateNameCodeContact(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.ContactNo, model.NIC).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.ContactNo != null)
            {
                lstOrders = driver.SearchDriverDateNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.ContactNo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = driver.SearchDriverDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = driver.SearchDriverDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null && model.ContactNo != null && model.NIC != null && model.LicenseNo != null)
            {
                lstOrders = driver.SearchDriverNameCodeContactNICLicense(model.Name, model.Code, model.ContactNo, model.NIC, model.LicenseNo).ToList();
            }
            else if (model.Name != null && model.Code != null && model.ContactNo != null && model.NIC != null)
            {
                lstOrders = driver.SearchDriverNameCodeContactNIC(model.Name, model.Code, model.ContactNo, model.NIC).ToList();
            }
            else if (model.Name != null && model.Code != null && model.ContactNo != null)
            {
                lstOrders = driver.SearchDriverNameCodeContact(model.Name, model.Code, model.ContactNo).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = driver.SearchDriverNameCode(model.Name, model.Code).ToList();
            }

            else if (model.ContactNo != null && model.NIC != null)
            {
                lstOrders = driver.SearchContactEmail(model.ContactNo, model.NIC).ToList();
            }

            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = driver.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = driver.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = driver.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = driver.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = driver.getSearchDriver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = driver.getSearchDriver(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = driver.getSearchDriver(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDriver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchDriver(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDriver(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Code != null)
            {
                lstOrders = context.Drivers.Where(x => x.Code == model.Code).ToList();
            }
            else if (model.Name != null)
            {
                lstOrders = context.Drivers.Where(x => x.Name == model.Name).ToList();
            }
            else if (model.NIC != null)
            {
                lstOrders = context.Drivers.Where(x => x.NIC == model.NIC).ToList();
            }
            else if (model.LicenseNo != null)
            {
                lstOrders = context.Drivers.Where(x => x.LicenseNo == model.LicenseNo).ToList();
            }
            else
            {
                lstOrders = context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_Driver", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region SearchOwnDepartment 
        public ActionResult SearchOwnDepartment(OwnDepartmentModel model)
        {
            string Status = "OK";
            List<Models.Department> lstOrders = new List<Models.Department>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.Contact != null && model.Email != null)
            {
                lstOrders = ownDepartment.SearchOwnDepartmentAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.Contact, model.Email).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.Contact != null)
            {
                lstOrders = ownDepartment.SearchOwnDepartmentDateNameCodeContact(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.Contact).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = ownDepartment.SearchOwnDepartmentDateNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = ownDepartment.SearchOwnDepartmentDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Contact != null && model.Email != null)
            {
                lstOrders = ownDepartment.SearchDepartmentNameCodeContactEmail(model.Name, model.Code, model.Contact, model.Email).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Contact != null)
            {
                lstOrders = ownDepartment.SearchDepartmentNameCodeContact(model.Name, model.Code, model.Contact).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = ownDepartment.SearchDepartmentNameCode(model.Name, model.Code).ToList();
            }
            else if (model.Contact != null && model.Email != null)
            {
                lstOrders = ownDepartment.SearchContactEmail(model.Contact, model.Email).ToList();
            }

            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = ownDepartment.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = ownDepartment.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = ownDepartment.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = ownDepartment.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = ownDepartment.getSearchOwnDepartment(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = ownDepartment.getSearchOwnDepartment(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = ownDepartment.getSearchOwnDepartment(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchOwnDepartment(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchOwnDepartment(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchOwnDepartment(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Contact != null)
            {
                lstOrders = context.Departments.Where(x => x.DepartCode == model.Code).ToList();
            }
            else if (model.Name != null)
            {
                lstOrders = context.Departments.Where(x => x.DepartName == model.Name).ToList();
            }
            else if (model.Email != null)
            {
                lstOrders = context.Departments.Where(x => x.EmailAdd == model.Email).ToList();
            }
            else if (model.Code != null)
            {
                lstOrders = context.Departments.Where(x => x.DepartCode == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Departments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_OwnGroupDepartmentList", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchDesignation
        [HttpGet]
        public ActionResult SearchDesignation(DesignationModel model)
        {
            string Status = "OK";
            List<Models.Designation> lstOrders = new List<Models.Designation>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = designation.SearchDesignationAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = designation.SearchDesignationName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = designation.SearchDesignationCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = designation.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = designation.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = designation.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = designation.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = designation.SearchNameCode(model.Name, model.Code).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = designation.getSearchDesignation(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = designation.getSearchDesignation(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = designation.getSearchDesignation(Convert.ToDateTime(model.DateTo), "to");
            }


            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchDesignation(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchDesignation(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Name != null)
            {
                lstOrders = context.Designations.Where(x => x.DesignationName == model.Name).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.Designations.Where(x => x.DesignationCode == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Designations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_OwnGroupDesignationList", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  SearchEmployee
        [HttpGet]
        public ActionResult SearchEmployee(EmployeeModel model)
        {
            string Status = "OK";
            List<Models.Employee> lstOrders = new List<Models.Employee>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null && model.CNIC != null && model.Contact != null && model.OtherContact != null)
            {
                lstOrders = employee.SearchEmployeeAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.FatherName, model.Gender, model.CNIC, model.Contact, model.OtherContact).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null && model.CNIC != null && model.Contact != null)
            {
                lstOrders = employee.SearchEmployeeDateNameCodeGenderNICContact(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.FatherName, model.Gender, model.CNIC, model.Contact).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null && model.CNIC != null)
            {
                lstOrders = employee.SearchEmployeeDateNameCodeGenderNIC(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.FatherName, model.Gender, model.CNIC).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null)
            {
                lstOrders = employee.SearchEmployeeDateNameCodeGender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.FatherName, model.Gender).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.FatherName != null)
            {
                lstOrders = employee.SearchEmployeeDateNameFatherName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.FatherName).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = employee.SearchEmployeeDateNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = employee.SearchEmployeeDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = employee.SearchEmployeeDateCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null && model.CNIC != null && model.Contact != null && model.OtherContact != null)
            {
                lstOrders = employee.SearchEmployeeNameCodeFatherGenderCNICContactOtherContact(model.Name, model.Code, model.FatherName, model.Gender, model.CNIC, model.Contact, model.OtherContact).ToList();
            }
            else if (model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null && model.CNIC != null && model.Contact != null)
            {
                lstOrders = employee.SearchEmployeeNameCodeFatherGenderCNICContact(model.Name, model.Code, model.FatherName, model.Gender, model.CNIC, model.Contact).ToList();
            }
            else if (model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null && model.CNIC != null)
            {
                lstOrders = employee.SearchEmployeeNameCodeFatherGenderCNIC(model.Name, model.Code, model.FatherName, model.Gender, model.CNIC).ToList();
            }
            else if (model.Name != null && model.Code != null && model.FatherName != null && model.Gender != null)
            {
                lstOrders = employee.SearchEmployeeNameCodeFatherGender(model.Name, model.Code, model.FatherName, model.Gender).ToList();
            }
            else if (model.Name != null && model.Code != null && model.FatherName != null)
            {
                lstOrders = employee.SearchEmployeeNameCodeFather(model.Name, model.Code, model.FatherName).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = employee.SearchEmployeeNameCode(model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = employee.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = employee.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = employee.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = employee.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = employee.SearchNameCode(model.Name, model.Code).ToList();

            }


            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchEmployee(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchEmployee(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchEmployee(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Gender != null)
            {
                lstOrders = context.Employees.Where(x => x.Gender == model.Gender).ToList();
            }
            else if (model.Name != null)
            {
                lstOrders = context.Employees.Where(x => x.EmployeeName == model.Name).ToList();
            }

            else if (model.FatherName != null)
            {
                lstOrders = context.Employees.Where(x => x.FatherName == model.Name).ToList();
            }
            else if (model.Contact != null)
            {
                lstOrders = context.Employees.Where(x => x.ContactNo == model.Contact).ToList();
            }
            else if (model.CNIC != null)
            {
                lstOrders = context.Employees.Where(x => x.CNIC == model.CNIC).ToList();
            }
            else if (model.OtherContact != null)
            {
                lstOrders = context.Employees.Where(x => x.OtherContactNo == model.OtherContact).ToList();
            }

            else if (model.Code != null)
            {
                lstOrders = context.Employees.Where(x => x.EmployeeCode == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Employees.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_ProductType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  SearchCustomerGroup
        [HttpGet]
        public ActionResult SearchCustomerGroup(CustomerGroupModel model)
        {
            string Status = "OK";
            List<Models.CustomerGroup> lstOrders = new List<Models.CustomerGroup>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.Email != null && model.Contact != null)
            {
                lstOrders = customerGroup.SearchCustomerGroupAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.Email, model.Contact).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.Email != null)
            {
                lstOrders = customerGroup.SearchCustomerGroupDateNameCodeEmail(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.Email).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = customerGroup.SearchCustomerGroupDateNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = customerGroup.SearchCustomerGroupDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Email != null && model.Contact != null)
            {
                lstOrders = customerGroup.SearchCustomerGroupNameCodeEmailContact(model.Name, model.Code, model.Email, model.Contact).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Email != null)
            {
                lstOrders = customerGroup.SearchCustomerGroupNameCodeEmail(model.Name, model.Code, model.Email).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = customerGroup.SearchCustomerGroupNameCode(model.Name, model.Code).ToList();
            }

            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = customerGroup.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = customerGroup.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = customerGroup.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = customerGroup.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchCustomerGroup(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchCustomerGroup(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchCustomerGroup(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Name != null)
            {
                lstOrders = context.CustomerGroups.Where(x => x.Name == model.Name).ToList();
            }
            else if (model.Code != null)
            {
                lstOrders = context.CustomerGroups.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.Email != null)
            {
                lstOrders = context.CustomerGroups.Where(x => x.EmailAdd == model.Email).ToList();
            }
            else if (model.Contact != null)
            {
                lstOrders = context.CustomerGroups.Where(x => x.Contact == model.Contact).ToList();
            }

            else
            {
                lstOrders = context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_CustomerGroup", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchCustomerDepartment
        [HttpGet]
        public ActionResult SearchCustomerDepartment(CustomerDepartmentModel model)
        {
            string Status = "OK";
            List<Models.CustomerDepartment> lstOrders = new List<Models.CustomerDepartment>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.Email != null && model.Contact != null)
            {
                lstOrders = customerDepartment.SearchCustomerDepartmentAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.Email, model.Contact).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null && model.Email != null)
            {
                lstOrders = customerDepartment.SearchCustomerDepartmentDateNameCodeEmail(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code, model.Email).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = customerDepartment.SearchCustomerDepartmentDateNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = customerDepartment.SearchCustomerDepartmentDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Email != null && model.Contact != null)
            {
                lstOrders = customerDepartment.SearchCustomerDepartmentNameCodeEmailContact(model.Name, model.Code, model.Email, model.Contact).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Email != null)
            {
                lstOrders = customerDepartment.SearchCustomerDepartmentNameCodeEmail(model.Name, model.Code, model.Email).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = customerDepartment.SearchCustomerDepartmentNameCode(model.Name, model.Code).ToList();
            }

            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = customerDepartment.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = customerDepartment.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = customerDepartment.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = customerDepartment.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchCustomerDepartment(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchCustomerDepartment(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchCustomerDepartment(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Name != null)
            {
                lstOrders = context.CustomerDepartments.Where(x => x.Name == model.Name).ToList();
            }
            else if (model.Code != null)
            {
                lstOrders = context.CustomerDepartments.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.Email != null)
            {
                lstOrders = context.CustomerDepartments.Where(x => x.EmailAdd == model.Email).ToList();
            }
            else if (model.Contact != null)
            {
                lstOrders = context.CustomerDepartments.Where(x => x.Contact == model.Contact).ToList();
            }

            else
            {
                lstOrders = context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_CustomerDepartment", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchCustomerCompany

        [HttpGet]
        public ActionResult SearchCustomerCompany(CustomerCompanyModel model)
        {
            string Status = "OK";
            List<Models.CustomerCompany> lstOrders = new List<Models.CustomerCompany>();
            if (model.DateFrom != null && model.DateTo != null && model.GroupID != null && model.Name != null && model.Code != null && model.Email != null && model.Contact != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.GroupID, model.Name, model.Code, model.Email, model.Contact).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.GroupID != null && model.Name != null && model.Code != null && model.Email != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupIDDateNameCodeEmail(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.GroupID, model.Name, model.Code, model.Email).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.GroupID != null && model.Name != null && model.Code != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupIDDateNameCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.GroupID, model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.GroupID != null && model.Name != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupIDDateName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.GroupID, model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.GroupID != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupID(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.GroupID).ToList();
            }
            else if (model.GroupID != null && model.Name != null && model.Code != null && model.Email != null && model.Contact != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupIDNameCodeEmailContact(model.GroupID, model.Name, model.Code, model.Email, model.Contact).ToList();
            }
            else if (model.GroupID != null && model.Name != null && model.Code != null && model.Email != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupIDNameCodeEmail(model.GroupID, model.Name, model.Code, model.Email).ToList();
            }
            else if (model.GroupID != null && model.Name != null && model.Code != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupIDNameCode(model.GroupID, model.Name, model.Code).ToList();
            }
            else if (model.GroupID != null && model.Name != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupIDNameCode(model.GroupID, model.Name).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Email != null && model.Contact != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupNameCodeEmailContact(model.Name, model.Code, model.Email, model.Contact).ToList();
            }
            else if (model.Name != null && model.Code != null && model.Email != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupNameCodeEmail(model.Name, model.Code, model.Email).ToList();
            }
            else if (model.Name != null && model.Code != null)
            {
                lstOrders = customerCompany.SearchCustomerGroupNameCode(model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchCustomerCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }

            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchCustomerCompany(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchCustomerCompany(Convert.ToDateTime(model.DateTo), "to");
            }

            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = customerCompany.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = customerCompany.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = customerCompany.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = customerCompany.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }

            else if (model.GroupID != null)
            {
                lstOrders = context.CustomerCompanies.Where(x => x.GroupID == model.GroupID).ToList();
            }
            else if (model.Name != null)
            {
                lstOrders = context.CustomerCompanies.Where(x => x.Name == model.Name).ToList();
            }
            else if (model.Code != null)
            {
                lstOrders = context.CustomerCompanies.Where(x => x.Code == model.Code).ToList();
            }

            else if (model.Email != null)
            {
                lstOrders = context.CustomerCompanies.Where(x => x.EmailAdd == model.Email).ToList();
            }
            else if (model.Contact != null)
            {
                lstOrders = context.CustomerCompanies.Where(x => x.Contact == model.Contact).ToList();
            }

            else
            {
                lstOrders = context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_CustomerCompany", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SearchProductNature

        [HttpGet]
        public ActionResult SearchProductNature(ProductNatureModel model)
        {
            string Status = "OK";
            List<Models.Nature> lstOrders = new List<Models.Nature>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = productNature.SearchProductAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = productNature.SearchProductName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = productNature.SearchProductCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = productNature.getSearchProductNature(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchProductNature(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchProductNature(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = productNature.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = productNature.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = productNature.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = productNature.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.Code != null && model.Name != null)
            {
                lstOrders = productNature.SearchCodeName(model.Code, model.Name).ToList();
            }
            else if (model.Name != null)
            {
                lstOrders = productNature.SearchProductByName(model.Name).ToList();
            }
            else if (model.Code != null)
            {
                lstOrders = context.Natures.Where(x => x.Code == model.Code).ToList();
            }
            else
            {
                lstOrders = context.Natures.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_ProductNature", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchProductBroker

        [HttpGet]
        public ActionResult SearchProductBroker(ProductBrokerModel model)
        {
            string Status = "OK";
            List<Models.ProductBroker> lstOrders = new List<Models.ProductBroker>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = productBroker.SearchProductBrokerAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = productBroker.getSearchProductBroker(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = productBroker.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = productBroker.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }


            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchProductBroker(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchProductBroker(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Name != null)
            {
                lstOrders = context.ProductBrokers.Where(x => x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            else
            {
                lstOrders = context.ProductBrokers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_ProductBroker", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchVendorType

        [HttpGet]
        public ActionResult SearchVendorType(VendorTypeModel model)
        {
            string Status = "OK";
            List<Models.VendorType> lstOrders = new List<Models.VendorType>();
            if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.Code != null)
            {
                lstOrders = vendorType.SearchVendorAllFilter(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = vendorType.SearchVendorName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Code != null)
            {
                lstOrders = vendorType.SearchVendorCode(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = vendorType.getSearchVendor(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.Name != null)
            {
                lstOrders = vendorType.SearchDateFromName(Convert.ToDateTime(model.DateFrom), model.Name).ToList();
            }
            else if (model.DateTo != null && model.Name != null)
            {
                lstOrders = vendorType.SearchDateToName(Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.Code != null && model.Name != null)
            {
                lstOrders = vendorType.SearchCodeName(model.Code, model.Name).ToList();
            }
            else if (model.DateFrom != null && model.Code != null)
            {
                lstOrders = vendorType.SearchDateFromCode(Convert.ToDateTime(model.DateFrom), model.Code).ToList();
            }
            else if (model.DateTo != null && model.Code != null)
            {
                lstOrders = vendorType.SearchDateToCode(Convert.ToDateTime(model.DateTo), model.Code).ToList();
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchVendorType(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchVendorType(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.Name != null)
            {
                lstOrders = context.VendorTypes.Where(x => x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();

            }
            else
            {
                lstOrders = context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }

            string view = RenderPartialToString("_VendorType", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchTaxForTransportation
        [HttpGet]
        public ActionResult SearchTaxForTransportation(TaxForTransportationModel model)
        {
            string Status = "OK";
            List<Models.TaxRateTransportation> lstOrders = new List<Models.TaxRateTransportation>();
            if (model.OriginProvinceID != null && model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null && model.TaxRatePercentDestination != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationAllFilter(model.OriginProvinceID, model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin, model.TaxRatePercentDestination).ToList();
            }
            else if (model.OriginProvinceID != null && model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationOriginIDDateFromDateToNameTaxPercentOrigin(model.OriginProvinceID, model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin).ToList();
            }
            else if (model.OriginProvinceID != null && model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationOriginIDDateFromDateToName(model.OriginProvinceID, model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.OriginProvinceID != null && model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationOriginIDDateFromDateTo(model.OriginProvinceID, model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo)).ToList();
            }
            else if (model.OriginProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null && model.TaxRatePercentDestination != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationIDDateFromDateToNameTaxPercentOrigin(model.OriginProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin, model.TaxRatePercentDestination).ToList();
            }
            else if (model.OriginProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationIDDateFromDateToNameTaxPercent(model.OriginProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin).ToList();
            }
            else if (model.OriginProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationIDDateFromDateToName(model.OriginProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.OriginProvinceID != null && model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationIDDateFromDateTo(model.OriginProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo)).ToList();
            }
            else if (model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null && model.TaxRatePercentDestination != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationDestinationIDDateFromDateToNameTaxPercentOrigin(model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin, model.TaxRatePercentDestination).ToList();
            }
            else if (model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationDestinationIDDateFromDateToNameTaxPercent(model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin).ToList();
            }
            else if (model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationDestinationIDDateFromDateToName(model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DestinationProvinceID != null && model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationDestinationIDDateFromDateTo(model.DestinationProvinceID, Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo)).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null && model.TaxRatePercentDestination != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationDateFromDateToNameTaxPercentOrigin(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin, model.TaxRatePercentDestination).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null && model.TaxRatePercentOrigin != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationDateFromDateToNameTaxPercent(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name, model.TaxRatePercentOrigin).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Name != null)
            {
                lstOrders = taxTransportation.SearchTaxTransportationDateFromDateToName(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = taxTransportation.getSearchTaxForTransportation(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo)).ToList();
            }

            else if (model.OriginProvinceID != null && model.Name != null)
            {
                lstOrders = taxTransportation.SearchTaxForTransportationName(model.OriginProvinceID, model.Name).ToList();

            }
            else if (model.DestinationProvinceID != null && model.Name != null)
            {
                lstOrders = taxTransportation.SearchTaxForTransportationDestinationName(model.DestinationProvinceID, model.Name).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.OriginProvinceID != null)
            {
                lstOrders = taxTransportation.SearchTaxForTransportationID(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.OriginProvinceID).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.DestinationProvinceID != null)
            {
                lstOrders = taxTransportation.SearchTaxForTransportationDestinationID(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.DestinationProvinceID).ToList();
            }
            else if (model.TaxRatePercentDestination != null && model.TaxRatePercentOrigin != null && model.Name != null)
            {
                lstOrders = taxTransportation.SearchTaxForTransportationPercentName(model.TaxRatePercentDestination, model.TaxRatePercentOrigin, model.Name).ToList();
            }
            else if (model.TaxRatePercentDestination != null && model.TaxRatePercentOrigin != null)
            {
                lstOrders = taxTransportation.SearchTaxForTransportationRatePercent(model.TaxRatePercentDestination, model.TaxRatePercentOrigin).ToList();

            }
            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = dml.getSearchTaxRateTransportation(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = dml.getSearchTaxRateTransportation(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = dml.getSearchTaxRateTransportation(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.OriginProvinceID != null && model.DestinationProvinceID != null)
            {
                lstOrders = taxTransportation.SearchTaxForTransportationID(model.OriginProvinceID, model.DestinationProvinceID).ToList();
            }

            else if (model.OriginProvinceID != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.OriginProvince == model.OriginProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.DestinationProvinceID != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.DestinationProvince == model.DestinationProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OriginProvinceID != null && model.Name != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.OriginProvince == model.OriginProvinceID && x.Name == model.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.DestinationProvinceID != null && model.Name != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.DestinationProvince == model.DestinationProvinceID && x.Name == model.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Name != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.Name == model.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.TaxRatePercentDestination != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.TaxRatePercentDestination == model.TaxRatePercentDestination && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.TaxRatePercentOrigin != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.TaxRatePercentOrigin == model.TaxRatePercentOrigin && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.TaxRatePercentOrigin != null)
            {
                lstOrders = context.TaxRateTransportations.Where(x => x.TaxRatePercentOrigin == model.TaxRatePercentOrigin && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                lstOrders = context.TaxRateTransportations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }


            string view = RenderPartialToString("_TaxForTransportation", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        public ActionResult BiltySystem()
        {
            BiltySystemDropDown();
            var data = context.ParchoonBilties.ToList();
            return View(data);
        }

        public void BiltySystemDropDown()
        {
            ViewBag.Stations = new SelectList(context.Transporters.ToList(), "ID", "Name");
            ViewBag.Receivers = new SelectList(context.Receivers.ToList(), "ID", "Name");
            ViewBag.Products = new SelectList(context.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.Brokers = new SelectList(context.ProductBrokers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "Id", "Name");
            ViewBag.ExpenseTypes = new SelectList(context.ExpensesTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ExpensesTypeID", "ExpensesTypeName");
            ViewBag.DocumentTypes = new SelectList(context.DocumentTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "DocumentTypeID", "Name");
            ViewBag.Cities = new SelectList(context.Cities.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList(), "CityID", "CityName");
            ViewBag.VehicleTypes = new SelectList(context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            List<SelectListItem> ShipmentTypes = new List<SelectListItem>();
            ShipmentTypes.Add(new SelectListItem { Text = "Full Truck Load", Value = "FTL" });
            ShipmentTypes.Add(new SelectListItem { Text = "Loos Cargo Load", Value = "LCL" });

            List<SelectListItem> PaymentType = new List<SelectListItem>();
            PaymentType.Add(new SelectListItem { Text = "Paid", Value = "Paid" });
            PaymentType.Add(new SelectListItem { Text = "ToPay", Value = "ToPay" });


            List<SelectListItem> DeliveryType = new List<SelectListItem>();
            DeliveryType.Add(new SelectListItem { Text = "Direct", Value = "Direct" });
            DeliveryType.Add(new SelectListItem { Text = "InDirect", Value = "InDirect" });
            ViewBag.ShipmentTypes = new SelectList(ShipmentTypes, "Value","Text");
            ViewBag.DeliveryTypes = new SelectList(DeliveryType, "Value","Text");
            ViewBag.PaymentTypes = new SelectList(PaymentType, "Value","Text");
        }
        public ActionResult Module()
        {
            return View(context.Menus.ToList());
        }
    }
}