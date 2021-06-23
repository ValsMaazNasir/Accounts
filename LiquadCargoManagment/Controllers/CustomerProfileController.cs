using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LiquadCargoManagment.Models;
using static LiquadCargoManagment.Helpers.ApplicationHelper;

namespace LiquadCargoManagment.Controllers
{
    public class CustomerProfileController : BaseController
    {
        private LCMEntities db = new LCMEntities();

        // GET: CustomerProfile
        public ActionResult Index()
        {
            return View(db.CustomerProfiles.ToList());
        }
        public void DropdownList()
        {
            ViewBag.PaymentTerm = new List<string>() {"Paid","To Pay" };
            ViewBag.CreditTerm = new List<string>() {"15 Days","30 Days" };
            ViewBag.InvoiceFormat = new List<string>() {"1","2" };
            ViewBag.Product = db.Products.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            ViewBag.Location = db.PickLocations.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            ViewBag.Customer = new SelectList(db.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList(), "ID", "Name");
            ViewBag.OwnCompany = new SelectList(db.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID).ToList(), "ID", "Name");
        }
        // GET: CustomerProfile/Details/5
        public ActionResult Details(long? id)
        {
            DropdownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = db.CustomerProfiles.Find(id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileId = id;

            return View(customerProfile);
        }

        // GET: CustomerProfile/Create
        public ActionResult Create()
        {
            DropdownList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfileId,CustomerName,CustomerCode,CustomerId,OwnCompanyId,PaymentTerm,CreditTerm,InvoiceFormat,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy,isHide,IsAdditionalCharges,IsLaborCharges")] CustomerProfile customerProfile)
        {
            if (ModelState.IsValid)
            {
                db.CustomerProfiles.Add(customerProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerProfile);
        }

        // GET: CustomerProfile/Edit/5
        public ActionResult Edit(long? id)
        {
            DropdownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = db.CustomerProfiles.Find(id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            return View(customerProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfileId,CustomerName,CustomerCode,CustomerId,OwnCompanyId,PaymentTerm,CreditTerm,InvoiceFormat,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy,isHide,IsAdditionalCharges,IsLaborCharges")] CustomerProfile customerProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerProfile);
        }

        // GET: CustomerProfile/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProfile customerProfile = db.CustomerProfiles.Find(id);
            if (customerProfile == null)
            {
                return HttpNotFound();
            }
            return View(customerProfile);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CustomerProfile customerProfile = db.CustomerProfiles.Find(id);
            db.CustomerProfiles.Remove(customerProfile);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetail(CustomerProfileDetail[] customerProfile)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in customerProfile)
                {
                    db.CustomerProfileDetails.Add(item);
                }
                
                db.SaveChanges();
                return RedirectToAction("details",new { id = customerProfile[0].ProfileId });
            }

            return View(customerProfile);
        }
    }
}
