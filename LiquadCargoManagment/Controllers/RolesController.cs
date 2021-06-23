using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiquadCargoManagment.Controllers
{
    public class RolesController : BaseController
    {
       
        #region Roles
        [HttpGet]
        public ActionResult Index(string Search)
        {          
            ViewModel vm = new ViewModel();
            vm.lstForms = context.NavMenus.OrderBy(x => x.SequenceOrder).ToList();
            return View("Index", vm);            
        }
        [HttpPost]
        public ActionResult Index(Role model,List<RolePermission> lstRole)
        {
            string Message = "";
            string Status = "OK";

            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.RoleName != null)
                    {

                        var Duplicated = context.Roles.Where(x => x.RoleName == model.RoleName && x.isDeleted == true).ToList();
                        if (model.RoleID > 0)
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
                                context.Entry(model).State = EntityState.Modified;
                                Message = "Role Updated";
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
                                model.CreatedDate = DateTime.Now;
                                model.CreatedBy = ApplicationHelper.UserID;
                                context.Roles.Add(model);
                                context.SaveChanges();
                                try
                                {
                                    foreach (var item in lstRole)
                                    {
                                        item.RoleID = model.RoleID;
                                        item.CreatedDate = DateTime.Now;
                                        item.CreatedBy = ApplicationHelper.UserID;
                                        context.RolePermissions.Add(item);
                                    }
                                    context.SaveChanges();
                                    transaction.Commit();
                                    Message = "New role Created Successfully";

                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    Message = ex.Message;
                                    Status = "Exception";
                                }
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Message = $"An error occured due to: {ex.Message}";
                    Status = "Exception";
                }
            }

            return Json(new { Status = Status, Message = Message }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteRole(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Roles.Where(model => model.RoleID == id && model.isDeleted == true).FirstOrDefault();
                    if (inRow != null)
                    {
                        inRow.isDeleted = true;
                        context.Roles.Attach(inRow);
                        context.Entry(inRow).Property(x => x.isDeleted).IsModified = true;
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
            return Json(new { Status = Status, Message = Message}, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}