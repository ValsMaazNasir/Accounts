using EntityFramework.Filters;
using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class ReportController : BaseController
    {
      
        // GET: Report
        public ActionResult Index(string id)
        {
            LCMEntities context = new LCMEntities();
            var Model = new OrderViewModel();

            if (id != null && id != string.Empty)
            {
                long _id = Convert.ToInt64(id);
                Model.lstOwnCompany = context.OwnCompanies.Where(x => x.ID == _id && x.SubcriptionID == SubcriptionID).ToList();
                Model.listBilty = context.Bilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                Model.listBiltyDetail = context.BiltyDetails.Where(x => x.BiltyID == _id).ToList();
                ViewBag.Id = _id;
            }
            else
            {
               
                Model.lstOwnCompany = new List<OwnCompany>();
                Model.listBilty = new Models.Bilty();
                Model.listBiltyDetail = new List<BiltyDetail>();

            }
            return View(Model);
        }

        public ActionResult ChallanPrint(string id)
        {
            LCMEntities context = new LCMEntities();
            var Model = new OrderViewModel();

            if (id != null && id != string.Empty)
            {
                long _id = Convert.ToInt64(id);
                Model.lstOwnCompany = context.OwnCompanies.Where(x => x.ID == _id && x.SubcriptionID == SubcriptionID).ToList();
                //Model.listBilty = context.Bilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                //Model.listBiltyDetail = context.BiltyDetails.Where(x => x.BiltyID == _id).ToList();
                Model.lstBiltyList = context.Bilties.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                Model.compactchecklist = context.CompactCheckBilties.Where(x => x.CompactChallanID == _id).ToList();
                Model.lstChallan = context.Challans.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
                Model.lstProductType = context.Categories.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
                Model.lstChallanList = context.Challans.Where(x => x.ID == _id && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
                ViewBag.Id = _id;
            }
            else
            {

                Model.lstOwnCompany = new List<OwnCompany>();
                //Model.listBilty = new Bilty();
                //Model.listBiltyDetail = new List<BiltyDetail>();
                Model.compactchecklist = new List<CompactCheckBilty>();
                Model.lstChallan = new Challan();


            }
            return View(Model);
        }

        //public ActionResult Download_PDF()
        //{
        //    try
        //    {
        //        LCMEntities context = new LCMEntities();

        //        ReportDocument rd = new ReportDocument();
        //        rd.Load(Path.Combine(Server.MapPath("~/Report"), "rptBillCharges.rpt"));
        //        rd.SetDataSource(context.Vehicles.Select(c => new
        //        {
        //            id = c.VehicleID,
        //            name = c.VehicleModel
        //        }).ToList());

        //        Response.Buffer = false;
        //        Response.ClearContent();
        //        Response.ClearHeaders();


        //        rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
        //        rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
        //        rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5;

        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);

        //        return File(stream, "application/pdf", "CustomerList.pdf");
        //    }

        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return View();

        //}
        [HttpPost]
        public ActionResult BillPreview(long[] BiltyIds)
        {
            Session["biltyid"] = BiltyIds;
            return Json("success");
        }
    }
}