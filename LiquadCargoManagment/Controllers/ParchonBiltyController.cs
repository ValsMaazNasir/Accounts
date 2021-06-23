using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using static LiquadCargoManagment.Helpers.ApplicationHelper;

namespace LiquadCargoManagment.Controllers
{
    public class ParchonBiltyController : BaseController
    {
        public long GetNewChallanNo()
        {
            long? ChallanNo = Parchoon.getChallan().Count <= 0 ? 0 : dml.getChallan()[0].ChallanNo;
            ChallanNo = ChallanNo + 1;
            return Convert.ToInt64(ChallanNo);
        }
        public long BillNo()
        {
            long? BillNo = Parchoon.BillNo().Count <= 0 ? 0 : dml.BillNo()[0].BillNo;
            BillNo = BillNo + 1;
            return Convert.ToInt64(BillNo);
        }
        // GET: ParchonBilty
        #region Parchon Bilty Bill 
        [HttpGet]

        public ActionResult ParchonBill()
        {
            ChallanView();
            BiltySystemDropDown();
            var bilty = context.ParchoonBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            return View(bilty);
        }

        public ActionResult ParchonBillList()
        {
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).ToList(), "ID", "Name");
            ViewBag.BillTo = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            var list = context.ParchoonBills.Where(x => x.IsDeleted != true).OrderByDescending(x => x.ID).ToList();
            return View(list);
        }
        public ActionResult SaveParchoonBill(List<ParchoonCheckBill> check, ParchoonBill Order)
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
                        var duplicate = context.ParchoonBills.Where(x => x.ReferenceNo == Order.ReferenceNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                        if (Order.ID > 0)
                        {
                            if (duplicate.Count <= 1)
                            {
                                var record = context.ParchoonBills.FirstOrDefault(x => x.ID == Order.ID);
                                if (record != null)
                                {
                                    Order.BillNo = record.BillNo;
                                    Order.CreatedDate = record.CreatedDate;
                                }
                                context.ParchoonCheckBills.RemoveRange(context.ParchoonCheckBills.Where(x => x.ParchoonBillID == Order.ID).ToList());
                                Order.ModifiedBy = ApplicationHelper.UserID;
                                Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                                Order.IsActive = true;
                                Order.ModifiedDate = DateTime.Now;
                                var local = context.Set<LiquadCargoManagment.Models.ParchoonBill>().Local.FirstOrDefault(x => x.ID == x.ID);
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
                                context.ParchoonBills.Add(Order);
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
                            foreach (ParchoonCheckBill item in check)
                            {
                                item.ParchoonBillID = OrderID;
                                context.ParchoonCheckBills.Add(item);
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
                    message = "Parchoon Bill has been created";
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

        public JsonResult EditParchoonBill(int id)
        {
            string Message = "";
            string Status = "OK";
            ParchoonBillModal Bill = new ParchoonBillModal();
            List<ParchoonBillMeta> ch = new List<ParchoonBillMeta>();
            try
            {
                if (id > 0)
                {
                    var z = context.ParchoonBills.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
                    Bill.ID = z.ID;
                    Bill.ReferenceNo = z.ReferenceNo;
                    Bill.ReportType = z.ReportType;
                    Bill.BillingDate = Convert.ToDateTime(z.BillingDate).ToShortDateString();

                    ch = context.ParchoonCheckBills.Where(x => x.ParchoonBillID == id).Select(x => new ParchoonBillMeta
                    {
                        ID = x.ParchoonBiltyID,
                        OrderNo = x.ParchoonBilty.ManualBiltyNo,
                        OrderDate = x.ParchoonBilty.Date,
                        BillTo = x.ParchoonBilty.CustomerCompany.Name,
                        CustomerType = x.ParchoonBilty.PaymentType
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
        public JsonResult DeleteParchoonBill(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.ParchoonBills.Where(x => x.ID == id && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new Models.ParchoonBill() { ID = id, IsDeleted = true, DeletedBy = ApplicationHelper.UserID, DeletedDate = DateTime.Now };
                        using (var db = new LCMEntities())
                        {
                            db.ParchoonBills.Attach(model);
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
            var Bill = context.ParchoonBills.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_ParchoonBill.cshtml", Bill);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Challan 
        [HttpGet]
        public ActionResult ParchonChallan()
        {
            ChallanView();
            BiltySystemDropDown();
            var bilty = context.ParchoonBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();
            return View(bilty);
        }

        public ActionResult SearchChallanBilty(ParchoonBiltyModel model)
        {
            string Status = "OK";
            List<ParchoonBilty> lstOrders = new List<ParchoonBilty>();
            if (model.DateFrom != null && model.DateTo != null && model.Sender != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.OrderNo != null)
            {
                lstOrders = Parchoon.SearchBiltyAllFilters(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.OrderNo, model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownVBROS(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownVBRO(model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownVBR(model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null)
            {
                lstOrders = Parchoon.SearchBiltyDateVehicle(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Sender != null)
            {
                lstOrders = Parchoon.SearchBiltyDateSender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Sender).ToList();
            }
            else if (model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownVB(model.Vehicle, model.BillTo).ToList();
            }
            else if (model.Vehicle != null && model.Receiver != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownVR(model.Vehicle, model.Receiver).ToList();
            }
            else if (model.Vehicle != null && model.OwnCompany != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownVO(model.Vehicle, model.OwnCompany).ToList();
            }
            else if (model.Vehicle != null && model.Sender != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownVS(model.Vehicle, model.Sender).ToList();
            }
            else if (model.BillTo != null && model.Receiver != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownBR(model.BillTo, model.Receiver).ToList();
            }
            else if (model.BillTo != null && model.OwnCompany != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownBO(model.BillTo, model.OwnCompany).ToList();
            }
            else if (model.BillTo != null && model.Sender != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownBS(model.BillTo, model.Sender).ToList();
            }
            else if (model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownRO(model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.Receiver != null && model.Sender != null)
            {
                lstOrders = Parchoon.SearchBiltyDropDownRS(model.Receiver, model.Sender).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.BillTo != null)
            {
                lstOrders = Parchoon.SearchBiltyDateBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Receiver != null)
            {
                lstOrders = Parchoon.SearchBiltyDateReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.OwnCompany != null)
            {
                lstOrders = Parchoon.SearchBiltyDateOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null)
            {
                lstOrders = Parchoon.SearchBiltyDateVehicleBillTo(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null)
            {
                lstOrders = Parchoon.SearchBiltyDateVehicleBillToReceiver(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null)
            {
                lstOrders = Parchoon.SearchBiltyDateVehicleBillToReceiverOwnCompany(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany).ToList();
            }
            else if (model.DateFrom != null && model.DateTo != null && model.Vehicle != null && model.BillTo != null && model.Receiver != null && model.OwnCompany != null && model.Sender != null)
            {
                lstOrders = Parchoon.SearchBiltyDateVehicleBillToReceiverOwnCompanySender(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo), model.Vehicle, model.BillTo, model.Receiver, model.OwnCompany, model.Sender).ToList();
            }

            else if (model.DateFrom != null && model.DateTo != null)
            {
                lstOrders = Parchoon.getSearchParchoonBilty(Convert.ToDateTime(model.DateFrom), Convert.ToDateTime(model.DateTo));
            }
            else if (model.DateFrom != null && model.DateTo == null)
            {
                lstOrders = Parchoon.getSearchParchoonBilty(Convert.ToDateTime(model.DateFrom), "from");
            }
            else if (model.DateFrom == null && model.DateTo != null)
            {
                lstOrders = Parchoon.getSearchParchoonBilty(Convert.ToDateTime(model.DateTo), "to");
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.CustomerCompanyID == model.BillTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.BillTo != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.CustomerCompanyID == x.CustomerCompanyID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.ReceiverID == model.Receiver && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Receiver != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.ReceiverID == x.ReceiverID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Sender != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.PickLocationID == model.Sender && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Sender != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.PickLocationID == x.PickLocationID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.OwnCompanyID == model.OwnCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OwnCompany != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.OwnCompanyID == x.OwnCompanyID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.VehicleTypeID == model.Vehicle && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.Vehicle != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.VehicleTypeID == x.VehicleTypeID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else if (model.OrderNo != null)
            {
                lstOrders = context.ParchoonBilties.Where(x => x.ManualBiltyNo == model.OrderNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            else
            {
                lstOrders = context.ParchoonBilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

            string view = RenderPartialToString("_SearchParchoonChallan", lstOrders.ToList());

            return Json(new { html = view, Status = Status }, JsonRequestBehavior.AllowGet);
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
        public void BiltySystemDropDown()
        {
            ViewBag.Stations = new SelectList(context.Transporters.ToList(), "ID", "Name");
            ViewBag.Receivers = new SelectList(context.Receivers.ToList(), "ID", "Name");
            ViewBag.CustomerCompany = new SelectList(context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
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
            ViewBag.ShipmentTypes = new SelectList(ShipmentTypes, "Value", "Text");
            ViewBag.DeliveryTypes = new SelectList(DeliveryType, "Value", "Text");
            ViewBag.PaymentTypes = new SelectList(PaymentType.ToList(), "Value", "Text");
        }
        #endregion


        #endregion

        #region Parchon Challan Grid
        public ActionResult ParchonChallanList()
        {
            ViewBag.CustomerType = new SelectList(context.Bilties.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)), "ID", "CustomerType");
            ViewBag.OwnCompany = new SelectList(context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID), "ID", "Name");
            var PickLocation = context.PickLocations.Where(x => x.LocationType == "Pick" || x.LocationType == "Both" && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationCode = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.PickLocation = new SelectList(PickLocation, "ID", "LocationCode");
            var DropLocation = context.PickLocations.Where(x => x.LocationType == "Drop" || x.LocationType == "Both" && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, LocationCode = string.Format("{0} | {1}", x.LocationCode, x.LocationName) }).ToList();
            ViewBag.DropLocation = new SelectList(DropLocation, "ID", "LocationCode");
            ViewBag.SearchDropLocation = new SelectList(DropLocation, "ID", "LocationCode");
            var CustomerComany = context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.CustomerComany = new SelectList(CustomerComany, "ID", "Code");
            ViewBag.Vehicle = new SelectList(context.Vehicles.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)), "VehicleID", "Code");
            var Broker = context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.OwnerContactName) }).ToList();
            ViewBag.Broker = new SelectList(Broker, "ID", "Code");
            var Driver = context.Drivers.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList().Select(x => new { ID = x.ID, Code = string.Format("{0} | {1}", x.Code, x.Name) }).ToList();
            ViewBag.Driver = new SelectList(Driver, "ID", "Code");
            var list = context.ParchoonBiltyChallans.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID) && x.IsDeleted != true).OrderByDescending(x => x.ID).ToList();

            return View(list);
        }

        public JsonResult EditParchoonChallan(int id)
        {
            string Message = "";
            string Status = "OK";
            ParchoonChallanModel challan = new ParchoonChallanModel();
            List<ParchoonBiltyMeta> ch = new List<ParchoonBiltyMeta>();
            try
            {
                if (id > 0)
                {
                    var z = context.ParchoonBiltyChallans.Where(model => model.ID == id && lstAssignedCompanies.Contains(model.OwnCompanyID)).FirstOrDefault();
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

                    ch = context.ParchoonBiltyChecks.Where(x => x.ParchoonChallanID == id).Select(x => new ParchoonBiltyMeta
                    {
                        ID = x.ParchoonBiltyID,
                        OrderNo = x.ParchoonBilty.ManualBiltyNo,
                        OrderDate = x.ParchoonBilty.Date,
                        BillTo = x.ParchoonBilty.CustomerCompany.Name,
                        CustomerType = x.ParchoonBilty.PaymentType
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

        public JsonResult DeleteParchoonChallan(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.ParchoonBiltyChallans.Where(x => x.ID == id && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                    if (inRow != null)
                    {
                        var model = new ParchoonBiltyChallan() { ID = id, IsDeleted = true, DeletedBy = ApplicationHelper.UserID, DeletedDate = DateTime.Now };
                        using (var db = new LCMEntities())
                        {
                            db.ParchoonBiltyChallans.Attach(model);
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
            var Challan = context.ParchoonBiltyChallans.Where(x => x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ID).ToList();

            string View = RenderPartialToString("~/Views/Shared/_ParchoonChallan.cshtml", Challan);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SaveChallan
        [HttpPost]
        public ActionResult SaveChallanParchon(List<ParchoonBiltyCheck> check, ParchoonBiltyChallan Order)
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
                            var record = context.ParchoonBiltyChallans.FirstOrDefault(x => x.ID == Order.ID);
                            if (record != null)
                            {
                                Order.ChallanNo = record.ChallanNo;
                                Order.CreatedDate = record.CreatedDate;
                            }
                            context.ParchoonBiltyChecks.RemoveRange(context.ParchoonBiltyChecks.Where(x => x.ParchoonChallanID == Order.ID).ToList());
                            Order.ModifiedBy = ApplicationHelper.UserID;
                            Order.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            Order.ModifiedDate = DateTime.Now;
                            var local = context.Set<LiquadCargoManagment.Models.ParchoonBiltyChallan>().Local.FirstOrDefault(x => x.ID == x.ID);
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
                            context.ParchoonBiltyChallans.Add(Order);
                            context.SaveChanges();
                            OrderID = Order.ID;
                        }

                        if (check != null)
                        {
                            foreach (ParchoonBiltyCheck item in check)
                            {
                                item.ParchoonChallanID = OrderID;
                                context.ParchoonBiltyChecks.Add(item);
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
        #endregion

        #region Parchoon Challan Cascade
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
        #endregion
    }
}