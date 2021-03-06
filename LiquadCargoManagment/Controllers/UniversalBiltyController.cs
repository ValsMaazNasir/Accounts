using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using static LiquadCargoManagment.Helpers.ApplicationHelper;

namespace LiquadCargoManagment.Controllers
{
    [Authorize]
    public class UniversalBiltyController : BaseController
    {
        // GET: UniversalBilty
        #region Universal Bill 
        [HttpGet]
        public ActionResult UniversalBill()
        {
            ViewBag.Vehicle = new SelectList(context.VehicleTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "ID", "Name");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID), "ID", "Name");
            var BillTo = context.CustomerDepartments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.BillTo = new SelectList(BillTo, "ID", "Code");
            var bilty = context.UniversalBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            return View(bilty);
        }

        public ActionResult SearchUniversalBill(UniversalBiltyModel model)
        {
            string Status = "OK";
            List<UniversalBilty> lstOrders = new List<UniversalBilty>();
            if (model.DateFrom != null && model.DateTo != null && model.Sender != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.OrderNo != null)
            {
                lstOrders = Universal.SearchBiltyAllFilters(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.OrderNo, model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = Universal.SearchBiltyDropDownVBROS(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
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
            else if (model.DateFrom != null && model.DateTo != null && model.Sender != null)
            {
                lstOrders = Universal.SearchBiltyDateSender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Sender).ToList();
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
            else if (model.Vehicle != null && model.Sender != null)
            {
                lstOrders = Universal.SearchBiltyDropDownVS(model.Vehicle, model.Sender).ToList();
            }
            else if (model.BillTo != null && model.Receiver != null)
            {
                lstOrders = Universal.SearchBiltyDropDownBR(model.BillTo, model.Receiver).ToList();
            }
            else if (model.BillTo != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDropDownBO(model.BillTo, model.OwnCompany).ToList();
            }
            else if (model.BillTo != null && model.Sender != null)
            {
                lstOrders = Universal.SearchBiltyDropDownBS(model.BillTo, model.Sender).ToList();
            }
            else if (model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = Universal.SearchBiltyDropDownRO(model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Receiver != null && model.Sender != null)
            {
                lstOrders = Universal.SearchBiltyDropDownRS(model.Receiver, model.Sender).ToList();
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
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = Universal.SearchBiltyDateVehicleBillToReceiverOwnCompanySender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
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
            else if (model.Sender != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerReceiverCustomerCode == model.Sender).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Sender != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerSenderCustomerCode == s.ConsignerSenderCustomerCode).ToList().Count > 0 && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.CompanyID == model.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.UniversalBilties.Where(x => x.CompanyID == x.CompanyID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
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
                lstOrders = context.UniversalBilties.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_SearchUniversalBill", lstOrders.Where(x => x.IsDeleted != true).ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        public long BillNo()
        {
            long? BillNo = dml.UniversalBillNo().Count <= 0 ? 0 : dml.UniversalBillNo()[0].BillNo;
            BillNo = BillNo + 1;
            return Convert.ToInt64(BillNo);
        }
        [HttpPost]
        public ActionResult SaveUniversalBill(List<UniversalCheckBill> check, UniversalBill Order)
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
                        var duplicate = context.UniversalBills.Where(x => x.ReferenceNo == Order.ReferenceNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                        if (Order.ID > 0)
                        {
                            if (duplicate.Count <= 1)
                            {
                                var record = context.UniversalBills.FirstOrDefault(x => x.ID == Order.ID);
                                if (record != null)
                                {
                                    Order.BillNo = record.BillNo;
                                    Order.CreatedDate = record.CreatedDate;
                                }
                                context.UniversalCheckBills.RemoveRange(context.UniversalCheckBills.Where(x => x.UniversalBillID == Order.ID).ToList());
                                Order.ModifiedBy = ApplicationHelper.UserID;
                                Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                                Order.IsActive = true;
                                Order.ModifiedDate = DateTime.Now;
                                var local = context.Set<LiquadCargoManagment.Models.UniversalBill>().Local.FirstOrDefault(x => x.ID == x.ID);
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
                                context.UniversalBills.Add(Order);
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
                            foreach (UniversalCheckBill item in check)
                            {
                                item.UniversalBillID = OrderID;
                                context.UniversalCheckBills.Add(item);
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

        public JsonResult EditUniversalBill(int id)
        {
            string Message = "";
            string Status = "OK";
            UniversalBillModal Bill = new UniversalBillModal();
            List<UniversalBillMeta> ch = new List<UniversalBillMeta>();
            try
            {
                if (id > 0)
                {
                    var z = context.UniversalBills.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    Bill.ID = z.ID;
                    Bill.ReferenceNo = z.ReferenceNo;
                    Bill.ReportType = z.ReportType;
                    Bill.BillingDate = Convert.ToDateTime(z.BillingDate).ToShortDateString();

                    ch = context.UniversalCheckBills.Where(x => x.UniversalBillID == id).Select(x => new UniversalBillMeta
                    {
                        CreatedDate = x.UniversalBilty.CreatedDate,
                        ID = x.UniversalBiltyID,
                        OrderNo = x.UniversalBilty.AutoBiltyNo,
                        ManualNo = x.UniversalBilty.ManualBiltyNo,
                        BiltyDate = x.UniversalBilty.BiltyDate,
                        OwnCompany = x.UniversalBilty.OwnCompany.Name
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

        public ActionResult UniversalBillList()
        {
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).ToList(), "ID", "Name");
            ViewBag.BillTo = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            var list = context.UniversalBills.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID) && x.IsDeleted != true).OrderByDescending(x => x.ID).ToList();
            return View(list);
        }

        public JsonResult DeleteUniversalBill(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.UniversalBills.Where(x => x.ID == id && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new Models.UniversalBill() { ID = id, IsDeleted = true, DeletedBy = ApplicationHelper.UserID, DeletedDate = DateTime.Now };
                        using (var db = new LCMEntities())
                        {
                            db.UniversalBills.Attach(model);
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
            var Bill = context.UniversalBills.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_UniversalBill.cshtml", Bill);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public ActionResult SearchUniversalBillList(DateTime? DateTo, DateTime? DateFrom, int? BillTo, int? OwnCompany)
        {
            List<UniversalBill> lstOrders = new List<UniversalBill>();

            if (DateFrom != null && DateTo != null)
            {
                lstOrders = dml.DateUniversalSearchBill(Convert.ToDateTime(DateFrom), Convert.ToDateTime(DateTo));
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstOrders = dml.DateUniversalSearchBill(Convert.ToDateTime(DateFrom), "from");
            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = dml.DateUniversalSearchBill(Convert.ToDateTime(DateTo), "to");
            }
            else if (BillTo != null)
            {
                lstOrders = context.UniversalBills.Where(x => x.IsDeleted != true && x.UniversalCheckBills.Where(s => s.UniversalBilty.UniversalBiltyCustomerInformations.Where(m => m.ClientCode == m.ClientCode).ToList().Count > 0).ToList().Count > 0).ToList();
            }
            else if (BillTo != null)
            {
                lstOrders = context.UniversalBills.Where(x => x.IsDeleted != true && x.UniversalCheckBills.Where(o => o.UniversalBilty.UniversalBiltyCustomerInformations.Where(m => m.ClientCode == BillTo).ToList().Count > 0).ToList().Count > 0).ToList();
            }
            else if (OwnCompany != null)
            {
                lstOrders = context.UniversalBills.Where(x => x.IsDeleted != true && x.UniversalCheckBills.Where(s => s.UniversalBilty.CompanyID == OwnCompany).ToList().Count > 0).ToList();
            }
            else if (OwnCompany != null)
            {
                lstOrders = context.UniversalBills.Where(x => x.IsDeleted != true && x.UniversalCheckBills.Where(s => s.UniversalBilty.CompanyID == s.UniversalBilty.CompanyID).ToList().Count > 0).ToList();
            }
            else
            {
                lstOrders = context.UniversalBills.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_UniversalBill", lstOrders.ToList());

            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}