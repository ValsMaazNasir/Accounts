//using Bilty.Data;
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
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Controllers
{
    public class ParchoonBiltiesController : BaseController
    {
        private LCMEntities db = new LCMEntities();

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
        public ActionResult Index()
        {
           
            var parchoonBilties = db.ParchoonBilties.Include(p => p.City).Include(p => p.City1).Include(p => p.OwnCompany).Include(p => p.Transporter).Include(p => p.Transporter1).Include(p => p.ProductBroker).Include(p => p.Receiver).Include(p => p.ShipmentType).Include(p => p.VehicleType);
            return View(parchoonBilties.ToList());
        }

        public ActionResult Details(long? id)
        {
            BiltySystemDropDown();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParchoonBilty parchoonBilty = db.ParchoonBilties.Find(id);
            if (parchoonBilty == null)
            {
                return HttpNotFound();
            }
            return View(parchoonBilty);
        }

        public ActionResult Create()
        {
            BiltySystemDropDown();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,ShipmentTypeID,PaymentType,Date,BiltyNo,ManualBiltyNo,DNNo,VehicleTypeID,BrokerID,CellNo,Status,TotalQty," +
            "AdditionalFreight,TotalFreight,NetWeight,AdditionalWeight,TotalGrossWeight,TotalExpense,LocalFreight,FreightRs,Remarks,IsActive,CreatedBy,CreatedDate," +
            "ModifiedBy,ModifiedDate,IsDeleted,DeletedBy,DeletedDate,OwnCompanyID,DeliverType,PickLocationID,DropLocationID,PickStationID,DropStationID,PickAddress,DropAddress,ReceiverID")]
        ParchoonBilty parchoonBilty, ParchoonBiltyDocumentType[] documentType , ParchoonBiltyProduct[] product
            ,ParchoonBiltyExpense[] expenseTypes)
        {
            long OrderID = 0;
            if (parchoonBilty != null)
            {
                parchoonBilty.CreatedBy = ApplicationHelper.UserID;
                parchoonBilty.CreatedDate = DateTime.Now;
                parchoonBilty.IsActive = true;
                parchoonBilty.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                parchoonBilty.ID = OrderID;
                db.ParchoonBilties.Add(parchoonBilty);


                if (documentType != null)
                {
                    foreach (var item in documentType)
                    {
                        item.ParchoonBiltyID = OrderID;
                        db.ParchoonBiltyDocumentTypes.Add(item);
                    }
                }

                if (product != null)
                {
                    foreach (var item in product)
                    {
                        item.ParchoonBiltyID = OrderID;
                        db.ParchoonBiltyProducts.Add(item);
                    }
                }

                if (expenseTypes != null)
                {
                    foreach (var item in expenseTypes)
                    {
                        item.ParchoonBiltyID = OrderID;
                        db.ParchoonBiltyExpenses.Add(item);
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParchoonBilty parchoonBilty = db.ParchoonBilties.Find(id);
            if (parchoonBilty == null)
            {
                return HttpNotFound();
            }
            BiltySystemDropDown();
            return View(parchoonBilty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ShipmentTypeID,PaymentType,Date,BiltyNo,ManualBiltyNo,DNNo," +
            "VehicleTypeID,BrokerID,CellNo,Status,TotalQty,AdditionalFreight,TotalFreight,NetWeight,AdditionalWeight" +
            ",TotalGrossWeight,TotalExpense,LocalFreight,FreightRs,Remarks,IsActive,CreatedBy,CreatedDate,ModifiedBy," +
            "ModifiedDate,IsDeleted,DeletedBy,DeletedDate,OwnCompanyID,DeliverType,PickLocationID,DropLocationID," +
            "PickStationID,DropStationID,PickAddress,DropAddress,ReceiverID")] ParchoonBilty parchoonBilty, 
            List<ParchoonBiltyDocumentType> documentType,
            List<ParchoonBiltyProduct> product
            , List<ParchoonBiltyExpense> expenseTypes)
        {
            var record = context.ParchoonBilties.FirstOrDefault(x => x.ID == parchoonBilty.ID);
            if (record != null)
            {
                parchoonBilty.CreatedDate = record.CreatedDate;
            }
            parchoonBilty.ModifiedBy = ApplicationHelper.UserID;
            parchoonBilty.ModifiedDate = DateTime.Now;
            parchoonBilty.IsActive = true;
            parchoonBilty.OwnCompanyID = ApplicationHelper.OwnCompanyID;
            var local = context.Set<ParchoonBilty>().Local.FirstOrDefault(x => x.ID == x.ID);
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            db.Entry(parchoonBilty).State = EntityState.Modified;
            if (documentType != null && documentType.Count > 0)
            {
                foreach (var item in documentType)
                {
                    item.ParchoonBiltyID = parchoonBilty.ID;
                    db.ParchoonBiltyDocumentTypes.Add(item);
                }
            }

            if (product != null && product.Count > 0)
            {
                foreach (var item in product)
                {
                    item.ParchoonBiltyID = parchoonBilty.ID;
                    db.ParchoonBiltyProducts.Add(item);
                }
            }

            if (expenseTypes != null && expenseTypes.Count > 0)
            {
                foreach (var item in expenseTypes)
                {
                    item.ParchoonBiltyID = parchoonBilty.ID;
                    db.ParchoonBiltyExpenses.Add(item);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParchoonBilty parchoonBilty = db.ParchoonBilties.Find(id);
            if (parchoonBilty == null)
            {
                return HttpNotFound();
            }
            return View(parchoonBilty);
        }

        // POST: ParchoonBilties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ParchoonBilty parchoonBilty = db.ParchoonBilties.Find(id);
            db.ParchoonBilties.Remove(parchoonBilty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
