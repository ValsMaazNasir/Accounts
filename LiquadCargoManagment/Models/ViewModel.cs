
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{
    public class ViewModel
    {
        public Role Role { get; set; }
        public List<NavMenu> lstForms { get; set; }
    }

    public class WorkOrderViewModel
    {
        public WorkOrder WorkOrder { get; set; }
        public List<WorkOrder> lstWorkOrder { get; set; }
        public List<OrderDetail> lstOrderDetail { get; set; }
    }
    public class OrderViewModel
    {
        public Bilty listBilty { get; set; }
        public List<BiltyDetail> listBiltyDetail { get; set; }
        public BiltyDetail lstbiltydetail { get; set; }

        public PartialBilty listPartialBilty { get; set; }
        public List<PartialBiltyDetail> listPartialBiltyDetail { get; set; }

        public List<TaxRateRegistration> listGeneralTax { get; set; }
        public List<OwnCompany> lstCompany { get; set; }

        public List<ExpensesType> listExpense { get; set; }

        public SaleOrder SaleOrder { get; set; }
        //public List<SaleOrderProduct> lstOrderProducts { get; set; }
        public List<SaleOrderExpens> lstOrderExpense { get; set; }
        public List<SaleOrderDieselExpense> lstOrderOtherExpense { get; set; }

        public UniversalBilty UniversalBilty { get; set; }
        public List<UniversalBiltyCustomerInformation> lstCustomerinformation { get; set; }
        public UniversalBiltyLocationInformation lstLocationInformation { get; set; }
        public List<UniversalBiltyVehicleInformation> lstVehicleInformation { get; set; }
        public List<UniversalBiltyContainerInformation> lstContainerInformation { get; set; }
        public List<UniversalBiltyProductInformation> lstProductInformation { get; set; }
        public List<UniversalBiltyDispatchDocument> lstDispatchInformation { get; set; }
        public List<UniversalBiltyReceivingInformation> lstReceivingInformation { get; set; }
        public List<UniversalBiltyReceivingDocumentDetail> lstReceivingDocumentInformation { get; set; }
        public List<UniversalBiltyDamageDetail> lstDamageDetail { get; set; }
        public List<UniversalBiltyExpenseDetail> lstExpenseDetail { get; set; }
        public UniversalBiltyVehicleLoadingReporting lstLoadingLocation { get; set; }
        public UniversalBiltyVehicleUnLoadingReporting lstUnLoadingLocation { get; set; }
        public List<OwnCompany> lstOwnCompany { get; set; }
        public Challan lstChallan { get; set; }
        public List <Challan> lstChallanList { get; set; }
        public List<CompactCheckBilty> lstCheckBilty { get; set; }
        public List<Bilty> lstBiltyList { get; set; }
        public List<CompactCheckBilty> compactchecklist { get; set; }

        public List<Category> lstProductType { get; set; }
        public Diesel lstDiesel { get; set; }
        public List<DieselExpense> lstDieselExpense { get; set; }
        public LocalPickUniversalBilty lstLocalPick { get; set; }
        public List<LocalPickProduct> lstLocalPickProduct { get; set; }
        public LocalDropUniversalBilty lstLocalDrop { get; set; }
        public List<LocalDropProduct> lstLocalDropProduct { get; set; }
    }
}