using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static LiquadCargoManagment.Helpers.ApplicationHelper;

namespace LiquadCargoManagment.Controllers
{
    public class BaseController : Controller
    {
        protected readonly LCMEntities context;
        protected readonly SecurityTokenIdentifier _security;
        protected readonly ModelDML dml;
        protected readonly CompactBilty CompactBilty;
        protected readonly ParchoonBilty_ Parchoon;
        protected readonly UniversalBiltySearch Universal;
        protected readonly ProductTypes productType;
        protected readonly ProductNature productNature;
        protected readonly ProductBrokers productBroker;
        protected readonly VendorTypes vendorType;
        //protected readonly DieselManagement diesel;
        protected readonly Vendors vendor;
        protected readonly BankAc BankAcc;
        protected readonly VehicleDocuments vehicleDoc;
        protected readonly TypeVehicle vehicleType;
        protected readonly TrailorType trailorType;
        protected readonly Trailor trailor;
        protected readonly containerType ContainType;
        //protected readonly Container containers;
        protected readonly contain container;
        //protected readonly SearchModal.BankAc BankAc;
        protected readonly Banks BankClass;
        protected readonly DocumentTypes documentType;
        protected readonly PackageTypes packageType;
        protected readonly VehicleStandard vehicleStandard;
        protected readonly OwnCompanys ownCompany;
        protected readonly OwnDepartment ownDepartment;
        protected readonly Designations designation;
        protected readonly Employees employee;
        protected readonly CustomerGroups customerGroup;
        protected readonly CustomerCompanys customerCompany;
        protected readonly CustomerDepartments customerDepartment;
        protected readonly PrincipleCompanys principleCompany;
        protected readonly Citys city;
        protected readonly AreaSearch area;
        protected readonly ShipmentTypes shipmentType;
        protected readonly ExpenseTypes expenseType;
        protected readonly Expenses expenses;
        protected readonly DamageTypes damageType;
        protected readonly Damages damage;
        protected readonly DriverTypes driverType;
        protected readonly Drivers driver;
        protected readonly RevenueTypes revenueTypes;
        protected readonly Revenues revenues;
        protected readonly Documents documents;
        protected readonly GeneralTax generalTax;
        protected readonly TaxForTransportation taxTransportation;
        protected readonly Vehicles VehicleSearch;
        protected readonly MaintenanceTypes maintenanceType;
        protected readonly VehicleMaintenances vehicleMaintenance;
        public BaseController()
        {
            context = new LCMEntities();
            _security = new SecurityTokenIdentifier(context);
            dml = new ModelDML(context);
            CompactBilty = new CompactBilty(context);
            Parchoon = new ParchoonBilty_(context);
            Universal = new UniversalBiltySearch(context);
            ContainType = new containerType(context);
            trailor = new Trailor(context);
            trailorType = new TrailorType(context);
            vehicleType = new TypeVehicle(context);
            container = new contain(context);
            packageType = new PackageTypes(context);
            productType = new ProductTypes(context);
            vehicleStandard = new VehicleStandard(context);
            ownCompany = new OwnCompanys(context);
            ownDepartment = new OwnDepartment(context);
            designation = new Designations(context);
            employee = new Employees(context);
            customerGroup = new CustomerGroups(context);
            customerCompany = new CustomerCompanys(context);
            customerDepartment = new CustomerDepartments(context);
            principleCompany = new PrincipleCompanys(context);
            city = new Citys(context);
            area = new AreaSearch(context);
            shipmentType = new ShipmentTypes(context);
            expenseType = new ExpenseTypes(context);
            expenses = new Expenses(context);
            damageType = new DamageTypes(context);
            damage = new Damages(context);
            driverType = new DriverTypes(context);
            driver = new Drivers(context);
            revenueTypes = new RevenueTypes(context);
            BankAcc = new BankAc(context);
            BankClass = new Banks(context);
            documentType = new DocumentTypes(context);
            documents = new Documents(context);
            generalTax = new GeneralTax(context);
            taxTransportation = new TaxForTransportation(context);
            VehicleSearch = new Vehicles(context);
            container = new contain(context);
            maintenanceType = new MaintenanceTypes(context);
            vehicleMaintenance = new VehicleMaintenances(context);
            var sc = context.SoftwareSetups.FirstOrDefault();
            SoftwareFormatting.DateFormat = sc.DateType.Format;
            SoftwareFormatting.ContactNoFormat = sc.ContactNoType.Format;
            SoftwareFormatting.NumberFormat = sc.NoType.Format;
            SoftwareFormatting.Logo = sc.Logo;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ProfileImage = GetCookie("ProfileImage");
            Username = GetCookie("Username");
            UserID = GetCookie("UserID") != string.Empty ? Convert.ToInt64(GetCookie("UserID")) : 0;
            OwnCompanyID = GetCookie("OwnCompanyID") != string.Empty ? Convert.ToInt64(GetCookie("OwnCompanyID")) : 0;
            SubcriptionID = GetCookie("SubcriptionID") != string.Empty ? Convert.ToInt64(GetCookie("SubcriptionID")) : 0;
            ApplicationHelper.RoleID = GetCookie("RoleID") != string.Empty ? Convert.ToInt64(GetCookie("RoleID")) : 0;
            lstRolePerm = context.RolePermissions
                            .Where(x => x.RoleID == RoleID && x.Parameter != "None").ToList();
            var assignedCompanies = context.UserAssignedCompanies.Where(x => x.UserID == UserID).ToList();
            lstAssignedCompanies = new List<long?>();
            foreach (var item in assignedCompanies)
            {
                lstAssignedCompanies.Add(item.OwnCompanyID);
            }
            lstBiltyRole = new List<string>();
            var BiltyRolesList = context.BiltyRoles.Where(x => x.UserId == UserID).ToList();
            foreach (var item in BiltyRolesList)
            {
                lstBiltyRole.Add(item.BiltyRole1);
            }
            var ActionName = filterContext
                .ActionDescriptor.ActionName.ToLower();
            var formName = context.NavMenus.Where(x => x.ActionName.Equals(ActionName)).FirstOrDefault();
            if (formName != null)
            {
                string _params = _security.IsUserAuthenticFor(formName.FormID);
                if (_params == string.Empty)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();

                }
                else
                {
                    Session["Allow"] = _params;                                                    
                    
                }
            }
            base.OnActionExecuting(filterContext);
        }
        public string RenderPartialToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        public RedirectToRouteResult UserNotValidate()
        {
            FormsAuthentication.SignOut();
            TempData["unauth"] = "something went wrong please login again for security purpose";
            return RedirectToAction("Login", "Account");
        }
    }
}