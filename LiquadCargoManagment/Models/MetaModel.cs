using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{

    public class SearchOrder
    {
        public DateTime? searchFrom { get; set; }
        public DateTime? searchTo { get; set; }
        public long? vehicleId { get; set; }
        public long? Orderno { get; set; }
        public long? Tax { get; set; }
        public long? LoadingPoint { get; set; }
        public long? DestinationPoint { get; set; }
        public string GSTType { get; set; }
        public long? ShipmentTypeId { get; set; }
    }
    public class SaleOrderDestination
    {
        public long ID { get; set; }
        public long[] DestinationLocation { get; set; }

        public long[] GSTType { get; set; }
        public string[] Tax { get; set; }
        public long[] Product { get; set; }
        public long[] ProductQty { get; set; }
        public long[] Frieght { get; set; }
        public long[] ShortageQty { get; set; }
        public long[] Shortage { get; set; }
    }
    public class SaleOrderExpenses
    {
        public long ID { get; set; }
        public long[] Instrument { get; set; }
        public long[] ExpenseTypeId { get; set; }
        public string[] RecievedBy { get; set; }
        public long[] PaymentMethod { get; set; }
        public long[] BankBranchId { get; set; }
        public string[] ChequeNo { get; set; }
        public DateTime[] ChequeDate { get; set; }
        public double[] Amount { get; set; }
    }

    public class SaleOrderDiesal
    {
        public long[] VendorID { get; set; }
        public int[] Liter { get; set; }
        public double[] Rate { get; set; }
        public double[] Amount { get; set; }
    }

    public class Meta
    {
        public SaleOrder SaleOrder { get; set; }
        public List<SaleOrderExpens> Expenses { get; set; }
        public List<SaleOrderDieselExpense> DiesalExpense { get; set; }
        public List<SaleOrdeDestination> Destinations { get; set; }
    }

    public class BillsMeta
    {
        public List<SaleOrderChild> SearchBills { get; set; }
        public List<Bill> lstBills { get; set; }
    }

    public class CompactBiltyMeta
    {
        public long? ID { get; set; }
        public long OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string BillTo { get; set; }
        public string CustomerType { get; set; }
        public double? Quantity { get; set; }
        public double? Weight { get; set; }
        public double? TotalFreight { get; set; }

    }

    public class challanModel
    {
        public long ID { get; set; }
        public string ChallanDate { get; set; }
        public long? ChallanNo { get; set; }
        public string ChallanManualNo { get; set; }
        public long? VehicleID { get; set; }
        public long? VendorID { get; set; }
        public string ExternalVehicleNo { get; set; }
        public long? PickLocationID { get; set; }
        public long? DropLocationID { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string DropCity { get; set; }
        public string DropArea { get; set; }
        public string DropAddress { get; set; }
        public long? DriverID { get; set; }
        public long? OwnCompanyID { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public long? BiltyID { get; set; }
        public string Reg { get; set; }
        public string VehicleType { get; set; }
        public long? DriverContact { get; set; }
        public string BrokerContact { get; set; }
    }

    public class UniversalChallanModel
    {
        public long ID { get; set; }
        public string ChallanDate { get; set; }
        public long ChallanNo { get; set; }
        public string ChallanManualNo { get; set; }
        public long VehicleID { get; set; }
        public long VendorID { get; set; }
        public string ExternalVehicleNo { get; set; }
        public long PickLocationID { get; set; }
        public long DropLocationID { get; set; }
        public long DriverID { get; set; }
        public long OwnCompanyID { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public long UniversalBiltyID { get; set; }
    }

    public class CompactBillModal
    {
        public long ID { get; set; }
        public string BillingDate { get; set; }
        public string ReferenceNo { get; set; }
        public string ReportType { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class CompactBillMeta
    {
        public long? ID { get; set; }
        public long OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string BillTo { get; set; }
        public string CustomerType { get; set; }
        public double? Quantity { get; set; }
        public double? Weight { get; set; }
        public double? TotalFreight { get; set; }
    }

    public class LcmBillMeta
    {
        public long? ID { get; set; }
        public DateTime? OrderDate { get; set; }
        public long? OrderNo { get; set; }
        public string Vehicle { get; set; }
        public string Loading { get; set; }
        public string Destination { get; set; }
        public string GST { get; set; }
        public string Shipment { get; set; }
        public long? Product { get; set; }
        public double? Freight { get; set; }
        public string Customer { get; set; }
    }
    public class UniversalBillModal
    {
        public long ID { get; set; }
        public string BillingDate { get; set; }
        public string ReferenceNo { get; set; }
        public string ReportType { get; set; }
    }
    public class UniversalBillMeta
    {
        public DateTime? CreatedDate { get; set; }
        public long? ID { get; set; }
        public long? OrderNo { get; set; }
        public string ManualNo { get; set; }
        public DateTime? BiltyDate { get; set; }
        public string OwnCompany { get; set; }
        public string PickLocation { get; set; }
        public string DropLocation { get; set; }
    }

    public class ParchoonChallanModel
    {
        public long ID { get; set; }
        public string ChallanDate { get; set; }
        public long? ChallanNo { get; set; }
        public string ChallanManualNo { get; set; }
        public long? VehicleID { get; set; }
        public long? VendorID { get; set; }
        public string ExternalVehicleNo { get; set; }
        public long? PickLocationID { get; set; }
        public long? DropLocationID { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string DropCity { get; set; }
        public string DropArea { get; set; }
        public string DropAddress { get; set; }
        public long? DriverID { get; set; }
        public long? OwnCompanyID { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public long? BiltyID { get; set; }
        public string Reg { get; set; }
        public string VehicleType { get; set; }
        public long? DriverContact { get; set; }
        public string BrokerContact { get; set; }
    }
    public class ParchoonBiltyMeta
    {
        public long? ID { get; set; }
        public string OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public string BillTo { get; set; }
        public string CustomerType { get; set; }
        public double? Quantity { get; set; }
        public double? Weight { get; set; }
        public double? TotalFreight { get; set; }

    }
    public class ParchoonBillModal
    {
        public long ID { get; set; }
        public string BillingDate { get; set; }
        public string ReferenceNo { get; set; }
        public string ReportType { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class ParchoonBillMeta
    {
        public long? ID { get; set; }
        public string OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public string BillTo { get; set; }
        public string CustomerType { get; set; }
        public double? Quantity { get; set; }
        public double? Weight { get; set; }
        public double? TotalFreight { get; set; }
    }
}