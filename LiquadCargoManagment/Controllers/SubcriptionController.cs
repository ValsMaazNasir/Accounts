using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiquadCargoManagment.Models;
using LiquadCargoManagment.Helpers;
using System.Data.Entity;

namespace LiquadCargoManagment.Controllers
{
    public class SubcriptionController : BaseController
    {
        #region Subcription
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Menus = context.Menus.OrderBy(x => x.SequenceOrder).ToList();
            return View("Index", context.Subcriptions.OrderByDescending(x => x.ID).ToList());

        }
        [HttpPost]
        public ActionResult addIndex(Subcription model, List<long> Menus)
        {
            string Message = "";
            string Status = "OK";
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.Name != null)
                    {
                        var Duplicated = context.Subcriptions.Where(x => x.Name == model.Name).ToList();
                        if (model.ID > 0)
                        {
                            if (Duplicated.Count > 1)
                            {

                                Message = "The record with this code and name is already exist";
                                Status = "Duplicate";
                            }
                            else
                            {
                                model.ModifiedDate = DateTime.Now;
                                model.ModifiedBy = ApplicationHelper.UserID;
                                model.Status = true;
                                context.Entry(model).State = EntityState.Modified;
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
                                model.Status = true;
                                model.EndDate = Convert.ToDateTime(model.StartDate)
                                    .AddMonths(model.NumberOfMonths == null ? 0 : Convert.ToInt16(model.NumberOfMonths));
                                model.CreatedDate = DateTime.Now;
                                model.CreatedBy = ApplicationHelper.UserID;
                                context.Subcriptions.Add(model);
                            }

                        }
                        context.SaveChanges();
                        if (Menus.Count > 0)
                        {
                            for (int i = 0; i < Menus.Count; i++)
                            {
                                context.SubcriptionModules.Add(new SubcriptionModule { MenuID = Menus[i], SubcriptionID = model.ID });
                            }
                            context.SaveChanges();
                        }
                        transaction.Commit();
                        Message = $"New Subcription Created at {DateTime.UtcNow}";
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
                    transaction.Rollback();
                }
            }

            return Json(new { Status = Status, Message = Message }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteIndex(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Subcriptions.Where(model => model.ID == id).FirstOrDefault();
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

            return Json(new { Status = Status, Message = Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeSubcriptionStatus(long Id, bool status)
        {
            string Message = "";
            string Status = "OK";
            if (Id > 0)
            {
                var record = context.Subcriptions.Where(x => x.ID == Id).FirstOrDefault();
                record.Status = status;
                context.SaveChanges();
            }
            string View = RenderPartialToString("~/Views/Shared/_SubcriptionList.cshtml", context.Subcriptions.OrderByDescending(x => x.ID).ToList());
            return Json(new { Message = Message, Status = Status, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}