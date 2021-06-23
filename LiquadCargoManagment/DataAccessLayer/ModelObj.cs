using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Bilty.Data
{

    public enum Operations
    {
        SELECT = 104,
        INSERT = 101,
        UPDATE = 102,
        UPDATE_CURRENT_CONTEXT = 4,
        DELETE = 103
    }
    public class usp_StockNotTransited
    {
        public long OrderDetailId { get; set; }
        public long? ParentId { get; set; }
        public long OrderID { get; set; }
        public string BiltyNo { get; set; }
        public DateTime? BiltyNoDate { get; set; }
        public string PaymentTerm { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CityName { get; set; }
        public string ReceiverName { get; set; }
        public double? Freight { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ChallanNo { get; set; }
        public DateTime ChallanDate { get; set; }
        public long? Quantity { get; set; }
        public bool? PartBilty { get; set; }
        public string ManualBiltyNo { get; set; }
        public string DA_NO { get; set; }
        public string VehicleCode { get; set; }
        public double NetWeight { get; set; }
        public double? AdditionalWeight { get; set; }
        public long LocalFreight { get; set; }
    }

    public class usp_GetVehicleWeightReport
    {
        public DateTime? BiltyNoDate { get; set; }
        public string BiltyNo { get; set; }
        public long? QTY { get; set; }
        public double? NetWeight { get; set; }
        public double? Amount { get; set; }
        public string ReceiverName { get; set; }
        public string Station { get; set; }
    }
   
    public class Nature
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    public class Category
    {

        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public partial class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    
    public class usp_DriverChallan
    {
        public decimal? ActionType { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public long? DriverNo { get; set; }
        public string BrokerName { get; set; }
        public string VehicleCode { get; set; }
        public long ChallanId { get; set; }
        public string BiltyNos { get; set; }
        public string ChallanNo { get; set; }
        public DateTime? ChallanDate { get; set; }
        public long? VehicleId { get; set; }
        public long? DriverId { get; set; }
        public string Mobile { get; set; }
        public long? BrokerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreadtedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class usp_GetPackageTypeTypeProductId
    {
        public string PackageTypeCode { get; set; }
        public string PackageTypeName { get; set; }
        public long PackageTypeID { get; set; }
        public double? Weight { get; set; }
    }

    public class usp_SearchProduct
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PackageTypeName { get; set; }
        public long? PackageTypeId { get; set; }
        public Double? Rate { get; set; }
        public long? DoorStepRate { get; set; }
        public string CustomerCode { get; set; }
        public string RateType { get; set; }
        public double? WeightPerUnit { get; set; }

        public Int64 ProfileDetail { get; set; }
        public double? AdditionCharges { get; set; }
        public double? LabourCharges { get; set; }
    }

    public class Response
    {
        public long ReturnValue { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }

        public long LastRowID { get; set; }
    }
    public class usp_Stations
    {
        public decimal? ActionType { get; set; }
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? CityID { get; set; }
        public string ContactPerson { get; set; }
        public string SecondaryContactPerson { get; set; }
        public long? ContactNo { get; set; }
        public long? SecondaryContactNo { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool? isActive { get; set; }
        public long? CreatedByID { get; set; }
        public long? ModifiedByID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string CityName { get; set; }
    }

    public class usp_ShipmentType
    {
        public decimal? ActionType { get; set; }
        public int ShipmentType_ID { get; set; }
        public string ShipmentTypeDetail { get; set; }
        public string ShipmentType { get; set; }
        public bool? Active { get; set; }
    }

    public class usp_InquiryAndOrders
    {
        public decimal? ActionType { get; set; }
        public long Order_ID { get; set; }
        public DateTime OrderDate { get; set; }
        public long CustomerID { get; set; }
        public bool IsForward { get; set; }
        public int? Department { get; set; }
        public bool? IsResponseBackToCustomer { get; set; }
        public bool IsInquiryToOrder { get; set; }
        public bool? IsComplete { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CompanyId { get; set; }
        public int? GroupID { get; set; }
        public int? DepartmentID { get; set; }
        public bool? ExistingCustomer { get; set; }
        public string CustomerGroupId { get; set; }
        public string CustomerGroupCode { get; set; }
        public string CustomerGroup { get; set; }
        public long? CustomerCompanyId { get; set; }
        public string CustomerCompany { get; set; }
        public string CustomerCompanyCode { get; set; }
        public long? CustomerDepartmentId { get; set; }
        public string CustomerDepartment { get; set; }
        public string CustomerDepartmentCode { get; set; }
        public long? CustomerPersonId { get; set; }
        public string CustomerPerson { get; set; }
        public string CustomerPersonCode { get; set; }
        public string CustomerContact { get; set; }
        public long? ExistingRefference { get; set; }
        public long? RefferenceGroupId { get; set; }
        public string RefferenceGroupCode { get; set; }
        public string RefferenceGroup { get; set; }
        public long? RefferenceCompanyId { get; set; }
        public string RefferenceCompany { get; set; }
        public string RefferenceCompanyCode { get; set; }
        public long? RefferenceDepartmentId { get; set; }
        public string RefferenceDepartment { get; set; }
        public string RefferenceDepartmentCode { get; set; }
        public long? RefferencePersonId { get; set; }
        public string RefferencePerson { get; set; }
        public string RefferencePersonCode { get; set; }
        public string RefferenceContact { get; set; }
        public long? ReceiverGroupId { get; set; }
        public string ReceiverGroupCode { get; set; }
        public string ReceiverGroup { get; set; }
        public long? ReceiverCompanyId { get; set; }
        public string ReceiverCompany { get; set; }
        public string ReceiverCompanyCode { get; set; }
        public long? ReceiverDepartmentId { get; set; }
        public string ReceiverDepartment { get; set; }
        public string ReceiverDepartmentCode { get; set; }
        public long? ReceiverPersonId { get; set; }
        public string ReceiverPerson { get; set; }
        public string ReceiverPersonCode { get; set; }
        public string ReceiverContact { get; set; }
        public long? ExistingBillTo { get; set; }
        public long? BillToGroupId { get; set; }
        public string BillToGroupCode { get; set; }
        public string BillToGroup { get; set; }
        public long? BillToCompanyId { get; set; }
        public string BillToCompany { get; set; }
        public string BillToCompanyCode { get; set; }
        public long? BillToDepartmentId { get; set; }
        public string BillToDepartment { get; set; }
        public string BillToDepartmentCode { get; set; }
        public long? BillToPersonId { get; set; }
        public string BillToPerson { get; set; }
        public string BillToPersonCode { get; set; }
        public string BillToContact { get; set; }
        public bool? IsCommunicatewithCustomer { get; set; }
        public string Status { get; set; }
        public bool? OrderCompleted { get; set; }
        public bool? Active { get; set; }
        public bool? AssessmentReponse { get; set; }
        public bool OrderPlacment { get; set; }
        public string BiltyNo { get; set; }
        public string ManualBiltyNo { get; set; }
        public DateTime? ManualBiltyDate { get; set; }
    }

    //public class usp_InquiryAndOrders
    //{
    //    public decimal? ActionType { get; set; }
    //    public long Order_ID { get; set; }
    //    public DateTime OrderDate { get; set; }
    //    public long CustomerID { get; set; }
    //    public bool? IsForward { get; set; }
    //    public int? Department { get; set; }
    //    public bool? IsResponseBackToCustomer { get; set; }
    //    public bool? IsInquiryToOrder { get; set; }
    //    public bool? IsComplete { get; set; }
    //    public long? CreatedBy { get; set; }
    //    public long? ModifiedBy { get; set; }
    //    public DateTime? CreatedDate { get; set; }
    //    public DateTime? ModifiedDate { get; set; }
    //    public int? CompanyId { get; set; }
    //    public int? GroupID { get; set; }
    //    public int? DepartmentID { get; set; }
    //    public bool? ExistingCustomer { get; set; }
    //    public string CustomerGroupId { get; set; }
    //    public string CustomerGroupCode { get; set; }
    //    public string CustomerGroup { get; set; }
    //    public long? CustomerCompanyId { get; set; }
    //    public string CustomerCompany { get; set; }
    //    public string CustomerCompanyCode { get; set; }
    //    public long? CustomerDepartmentId { get; set; }
    //    public string CustomerDepartment { get; set; }
    //    public string CustomerDepartmentCode { get; set; }
    //    public long? CustomerPersonId { get; set; }
    //    public string CustomerPerson { get; set; }
    //    public string CustomerPersonCode { get; set; }
    //    public string CustomerContact { get; set; }
    //    public long? ExistingRefference { get; set; }
    //    public long? RefferenceGroupId { get; set; }
    //    public string RefferenceGroupCode { get; set; }
    //    public string RefferenceGroup { get; set; }
    //    public long? RefferenceCompanyId { get; set; }
    //    public string RefferenceCompany { get; set; }
    //    public string RefferenceCompanyCode { get; set; }
    //    public long? RefferenceDepartmentId { get; set; }
    //    public string RefferenceDepartment { get; set; }
    //    public string RefferenceDepartmentCode { get; set; }
    //    public long? RefferencePersonId { get; set; }
    //    public string RefferencePerson { get; set; }
    //    public string RefferencePersonCode { get; set; }
    //    public string RefferenceContact { get; set; }
    //    public bool? IsCommunicatewithCustomer { get; set; }
    //    public string Status { get; set; }
    //    public bool? OrderCompleted { get; set; }
    //    public bool? Active { get; set; }
    //    public bool? AssessmentReponse { get; set; }
    //    public bool OrderPlacment { get; set; }
    //    public string BiltyNo { get; set; }
    //}


    public class usp_getAllCompanyByGroupId
    {
        public string CompanyName { get; set; }
        public long CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public long COMPANYID { get; set; }
        public string GroupCode { get; set; }
        public long GroupId { get; set; }
        public string GroupName { get; set; }
    }

    public class usp_Menu
    {
        public decimal? ActionType { get; set; }
        public long MenuID { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }

    public class usp_NavMenu
    {
        public Int64 FormID { get; set; }
        public decimal? ActionType { get; set; }
        public string MenuName { get; set; }
        public string FormName { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public string icon { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long MenuID { get; set; }
        public string formTarget { get; set; }
    }
    public class usp_InquiryAndOrdersDetail
    {
        public string ActionType { get; set; }
        public string StatusName { get; set; }
        public string Color { get; set; }
        public DateTime? VehicleAssignDateTime { get; set; }
        public string ShipmentType { get; set; }
        public string CONTAINER_PICKLOCATION { get; set; }
        public string CONTAINER_DROPLOCATION { get; set; }
        public string EXPORT_CARGOPICKLOCATION { get; set; }
        public string ContainerType { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleTypeName { get; set; }
        public string ExpensesTypeName { get; set; }
        public string ExpensesTypeCode { get; set; }
        public string OtherExpensesTypeName { get; set; }
        public string OtherExpensesTypeCode { get; set; }
        public long ImportExportID { get; set; }
        public int ShipmentTypeId { get; set; }
        public long OrderDetail_ID { get; set; }
        public long ContainerTypeID { get; set; }
        public long? ContainerTypeQuantity { get; set; }
        public double? TotalWeight { get; set; }
        public long ContainerPickupLocationID { get; set; }
        public string ContainerPickupAddress { get; set; }
        public long? ExportCargoPickLocationID { get; set; }
        public string ExportCargoPickAddress { get; set; }
        public long? ContainerDropOfLocationID { get; set; }
        public string ContainerDropOfAddress { get; set; }
        public string ImportContainerDropOption { get; set; }
        public DateTime? Dispatch_Date { get; set; }
        public string ShippingLine { get; set; }
        public string ContainerTerminalOrYeard { get; set; }
        public string BillingType { get; set; }
        public string Status { get; set; }
        //public long? VehicleID { get; set; }
        //public long? DriverID { get; set; }
        //public long? BrokerID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public int? VehicleTypeId { get; set; }
        public int? VehicleTypeQuantity { get; set; }
        public string ContainerToVehicle { get; set; }
        public string LoadingUnloadingLocationType { get; set; }
        public int? LoadingUnloadingExpenseTypeId { get; set; }
        public int? LoadingUnloadingExpenseTypeQty { get; set; }
        public string LoadingUnloadingExpenseTypeCapacity { get; set; }
        public int? OtherExpenseTypeId { get; set; }
        public int? OtherExpenseTypeQty { get; set; }
        public string OtherExpenseTypeCapacity { get; set; }
        public int? PackageTypeID { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? LoadQuantityWise { get; set; }
        public double? LoadWeightWise { get; set; }
        public double? Expenses_on_Consignment { get; set; }
        public double? Profit { get; set; }
        public double? Other_Charges { get; set; }
        public double? Tax { get; set; }
        public double? Total { get; set; }
        public string VehicleRegNo { get; set; }
        public Int64 VehicleId { get; set; }

        public Int64 VehicleAssignTypeID { get; set; }
        public string DriverCode { get; set; }
        public Int64 DriverId { get; set; }

        public string DriverName { get; set; }
        public string driverCell { get; set; }
        public string driverCNIC { get; set; }

        public string DriverLicenseNo { get; set; }
        public DateTime DriverLicenseExp { get; set; }
        public string LicenseStatus { get; set; }
        public string BrokerName { get; set; }
        public Int64 BrokerId { get; set; }
        public string BrokerCell { get; set; }
        public string BrokerCNIC { get; set; }

    }


    public class usp_ContainerToVehicle
    {
        public decimal? ActionType { get; set; }
        public int ContainerToVehicleid { get; set; }
        public string ContainerToVehicle { get; set; }
    }

    public class usp_PickDropLocation
    {
        public decimal? ActionType { get; set; }
        public long PickDropID { get; set; }
        public string PickDropCode { get; set; }
        public long? AreaID { get; set; }
        public string Address { get; set; }
        public long? LocationTypeID { get; set; }
        public long? OwnerID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public string PickDropLocationName { get; set; }
        public bool? IsPort { get; set; }
        public string LAT { get; set; }
        public string LON { get; set; }
        public long ProvinceID { get; set; }
        public long CityID { get; set; }
    }
    public class usp_RoleBaseFormRight
    {
        public decimal? ActionType { get; set; }
        public long Id { get; set; }
        public long FormId { get; set; }
        public long RoleId { get; set; }
        public bool Active { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string RoleName { get; set; }
        public string FormName { get; set; }
        public string Url { get; set; }
    }

    public class usp_GetCompanyName
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }
    public class usp_Vehicle
    {
        public string VehicleTypeName { get; set; }
        public decimal? ActionType { get; set; }
        public long VehicleID { get; set; }
        public string VehicleCode { get; set; }
        public string RegNo { get; set; }
        public string ChasisNo { get; set; }
        public string EngineNo { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string DimensionUnitType { get; set; }
        public long VehicleTypeID { get; set; }
        public string Type { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public string BodyType { get; set; }
        public string ManufacturingYear { get; set; }
        public string ManufacturerName { get; set; }
        public byte[] Picture { get; set; }
        public DateTime? PurchasingDate { get; set; }
        public long? PurchasingAmount { get; set; }
        public string PurchaseFromName { get; set; }
        public string PurchaseFromDetail { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string OwnerNIC { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public bool? IsOwnVehicle { get; set; }
    }

    public class usp_PackageType
    {
        public decimal? ActionType { get; set; }
        public long PackageTypeID { get; set; }
        public string PackageTypeCode { get; set; }
        public string PackageTypeName { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string DimensionUnit { get; set; }
        public double? Weight { get; set; }
        public string WeightUnit { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMaster { get; set; }
        public string Description { get; set; }
    }

    public class usp_BillingType
    {
        public decimal? ActionType { get; set; }
        public long BillingTypeId { get; set; }
        public string BillingTypeName { get; set; }
    }
    //public class ContainerDropOption
    //{
    //    public int ContainerDropId { get; set; }
    //    public string DropOption { get; set; }
    //    public bool? IsActive { get; set; }
    //}
    public class usp_Driver
    {
        public decimal? ActionType { get; set; }
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Type { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public long? CellNo { get; set; }
        public long? OtherContact { get; set; }
        public long? HomeNo { get; set; }
        public string Address { get; set; }
        public long? NIC { get; set; }
        public string IdentityMark { get; set; }
        public DateTime? NICIssueDate { get; set; }
        public DateTime? NICExpiryDate { get; set; }
        public string LicenseNo { get; set; }
        public string LicenseCategory { get; set; }
        public DateTime? LicenseIssueDate { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string LicenseIssuingAuthority { get; set; }
        public string LicenseStatus { get; set; }
        public string EmergencyContactName { get; set; }
        public long? EmergencyContactNo { get; set; }
        public string ContactRelation { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }


    public class usp_Containerized
    {
        public decimal? ActionType { get; set; }
        public long ContainerizedId { get; set; }
        public long? ShipmentTypeID { get; set; }
        public string ShipmentType { get; set; }
        public long? Order_ID { get; set; }
        public int? PackageTypeID { get; set; }
        public int? Quantity { get; set; }
        public double? TotalWeight { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? LoadQuantityWise { get; set; }
        public double? LoadWeightWise { get; set; }
        public long? PickLocationId { get; set; }
        public string PickAddress { get; set; }
        public long? DropLoctionId { get; set; }
        public string DropAddress { get; set; }
        public DateTime? Dispatch_date { get; set; }
        public string BillType { get; set; }
        public long? VehicleId { get; set; }
        public long? VehicleTypeId { get; set; }
        public int? VehicleTypeQuantity { get; set; }
        public long? ContainerTypeId { get; set; }
        public string ContainerName { get; set; }
        public int? ContainerTypeQuantity { get; set; }
        public string Loading { get; set; }
        public string LoadingType { get; set; }
        public int? LoadingQuantity { get; set; }
        public string LoadingCapacity { get; set; }
        public string OtherEquipmentType { get; set; }
        public int? OtherEquipmentQuantity { get; set; }
        public string OtherEquipmentCapacity { get; set; }
        public long? DriverID { get; set; }
        public long? BrokerID { get; set; }
        public DateTime? VehicleDispatchDate { get; set; }
        public DateTime? VehicleOutFormLoading { get; set; }
        public string Status { get; set; }
    }

    public class usp_ExpensesType
    {
        public decimal? ActionType { get; set; }
        public long ExpensesTypeID { get; set; }
        public string ExpensesTypeName { get; set; }
        public string ExpensesTypeCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public string Remarks { get; set; }
        public bool? IsActive { get; set; }
    }

    public class usp_ContainerType
    {
        public decimal? ActionType { get; set; }
        public long ContainerTypeID { get; set; }
        public string ContainerType { get; set; }
        public string Code { get; set; }
        public string DimensionUnitType { get; set; }
        public double? LowerDeckInnerLength { get; set; }
        public double? LowerDeckInnerWidth { get; set; }
        public double? LowerDeckInnerHeight { get; set; }
        public double? UpperDeckInnerLength { get; set; }
        public double? UpperDeckInnerWidth { get; set; }
        public double? UpperDeckInnerHeight { get; set; }
        public double? LowerDeckOuterLength { get; set; }
        public double? LowerDeckOuterWidth { get; set; }
        public double? LowerDeckOuterHeight { get; set; }
        public double? UpperDeckOuterLength { get; set; }
        public double? UpperDeckOuterWidth { get; set; }
        public double? UpperDeckOuterHeight { get; set; }
        public double? UpperPortionInnerLength { get; set; }
        public double? UpperPortionInnerwidth { get; set; }
        public double? UpperPortionInnerHeight { get; set; }
        public double? LowerPortionInnerLength { get; set; }
        public double? LowerPortionInnerWidth { get; set; }
        public double? LowerPortionInnerHeight { get; set; }
        public string Description { get; set; }
        public double? TareWeight { get; set; }
        public double? TareWeightUnit { get; set; }
        public double? CubicCapacity { get; set; }
        public string CubicCapacityUnit { get; set; }
        public double? PayLoadWeight { get; set; }
        public string PayLoadWeightUnit { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
    }

    public class usp_VehicleType
    {
        public decimal? ActionType { get; set; }
        public long VehicleTypeID { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleTypeName { get; set; }
        public string DimensionUnitType { get; set; }
        public double? LowerDeckInnerLength { get; set; }
        public double? LowerDeckInnerWidth { get; set; }
        public double? LowerDeckInnerHeight { get; set; }
        public double? UpperDeckInnerLength { get; set; }
        public double? UpperDeckInnerWidth { get; set; }
        public double? UpperDeckInnerHeight { get; set; }
        public double? LowerDeckOuterLength { get; set; }
        public double? LowerDeckOuterWidth { get; set; }
        public double? LowerDeckOuterHeight { get; set; }
        public double? UpperDeckOuterLength { get; set; }
        public double? UpperDeckOuterWidth { get; set; }
        public double? UpperDeckOuterHeight { get; set; }
        public double? UpperPortionInnerLength { get; set; }
        public double? UpperPortionInnerwidth { get; set; }
        public double? UpperPortionInnerHeight { get; set; }
        public double? LowerPortionInnerLength { get; set; }
        public double? LowerPortionInnerWidth { get; set; }
        public double? LowerPortionInnerHeight { get; set; }
        public double? PermisibleHeight { get; set; }
        public double? PermisibleLength { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
    }



    public class usp_LocationType
    {
        public decimal? ActionType { get; set; }
        public long LocationTypeID { get; set; }
        public string LocationTypeName { get; set; }
    }

    //public class ContainerToVehicle
    //{
    //    public string ContainerToVehilce { get; set; }
    //}

    public class usp_DepartmentPerson
    {
        public long PersonalID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BusinessEmail { get; set; }
        public long? GroupID { get; set; }
        public long? CompanyID { get; set; }
        public long? DepartmentID { get; set; }
        public string Designation { get; set; }
        public string Cell { get; set; }
        public string PhoneNo { get; set; }
        public string OtherContact { get; set; }
        public string AddressOffice { get; set; }
        public string AddressOther { get; set; }
        public string Description { get; set; }
        public bool? IsIndividual { get; set; }
        public bool? Active { get; set; }
        public string GroupName { get; set; }
        public string CompanyName { get; set; }
        public string DepartName { get; set; }
    }
    public class usp_GetCustomerInfo
    {
        public long GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public long CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public long DepartID { get; set; }
        public string DepartCode { get; set; }
        public string DepartName { get; set; }
        public Int64 PersonalID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Cell { get; set; }
    }


    public class usp_SearchDriver
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public long? CELLNO { get; set; }
        public long? NIC { get; set; }
        public DateTime? NICIssueDATE { get; set; }
        public DateTime? NICExpiryDate { get; set; }
        public string LicenseNo { get; set; }
        public string LicenseCategory { get; set; }
        public DateTime? LicenseIssueDate { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string LicenseStatus { get; set; }
        public Int64 ID { get; set; }
    }
    public class usp_SearchBroker
    {
        public long ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public long? PHONE { get; set; }
        public long? NIC { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class usp_SearchVehicle
    {
        public long VEHICLEID { get; set; }
        public string REGNO { get; set; }
        public string ENGINENO { get; set; }
        public double? LENGTH { get; set; }
        public double? WIDTH { get; set; }
        public double? HEIGHT { get; set; }
        public long VEHICLETYPEID { get; set; }
        public string VEHICLEMODEL { get; set; }
        public string VEHICLECOLOR { get; set; }
        public string BODYTYPE { get; set; }
    }


    public class AssignVehicle
    {
        public Int64 DetailID { get; set; }
        public string VehicleRegNo { get; set; }
        public Int64 VehicleId { get; set; }

        public Int64 VehicleTypeID1 { get; set; }
        public string DriverCode { get; set; }
        public Int64 DriverId { get; set; }

        public string DriverName { get; set; }
        public string driverCell { get; set; }
        public string driverCNIC { get; set; }

        public string DriverLicenseNo { get; set; }
        public DateTime DriverLicenseExp { get; set; }
        public string LicenseStatus { get; set; }
        public string BrokerName { get; set; }
        public Int64 BrokerId { get; set; }
        public string BrokerCell { get; set; }
        public string BrokerCNIC { get; set; }
        public Int64 MasterID { get; set; }
    }
    public class usp_OwnCompany
    {
        //public decimal? ActionType { get; set; }
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EmailAdd { get; set; }
        public string WebAdd { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Contact { get; set; }
        public string ContactOther { get; set; }
        public long? SubcriptionID { get; set; }
        public string Description { get; set; }
        public long GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupCode { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string NTN { get; set; }

    }

    public class usp_OwnDepartment
    {
        public decimal? ActionType { get; set; }
        public long DepartID { get; set; }
        public string DepartCode { get; set; }
        public string DepartName { get; set; }
        public string Contact { get; set; }
        public string ContactOther { get; set; }
        public string EmailAdd { get; set; }
        public string WebAdd { get; set; }
        public string Address { get; set; }
        public long? CustomerID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUser { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public long GROUPID { get; set; }
        public long COMPANYID { get; set; }
        public string GroupName { get; set; }
        public string CompanyName { get; set; }
    }



    public class usp_OwnGroups
    {
        public decimal? ActionType { get; set; }
        public long GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string Contact { get; set; }

        public string ContactOther { get; set; }
        public string EmailAdd { get; set; }
        public string WebAdd { get; set; }
        public string Address { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CompanyAccess { get; set; }
    }

    //public class Designation
    //{
    //    public int DesignationId { get; set; }
    //    public string DesignationName { get; set; }
    //    public Nullable<bool> Active { get; set; }
    //    public Nullable<System.DateTime> CreatedDate { get; set; }
    //    public Nullable<long> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> ModifiedDate { get; set; }
    //    public Nullable<long> ModifiedBy { get; set; }
    //}
    //public class Role
    //{
    //    public int RoleID { get; set; }
    //    public string RoleName { get; set; }
    //}
    public class usp_UserAccounts
    {
        public decimal? ActionType { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string DepartCode { get; set; }
        public string DepartName { get; set; }
        public string RoleName { get; set; }
        public string DesignationName { get; set; }
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public long GroupID { get; set; }
        public long? CompanyID { get; set; }
        public long? DepartmentID { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int DesignationID { get; set; }
        public int RoleID { get; set; }
    }
    public class usp_Company
    {
        public string NTN { get; set; }
        public string STN { get; set; }
        public decimal? ActionType { get; set; }
        public long CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebSite { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Contact { get; set; }
        public string OtherContact { get; set; }
        public string Description { get; set; }
        public long GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupCode { get; set; }
        public string CustomerType { get; set; }
        public double Tax { get; set; }
        public string PaymentTerm { get; set; }
    }
    public class usp_InvoiceExpenses
    {
        public decimal? ActionType { get; set; }
        public long InvoiceExpenseId { get; set; }
        public string ExpenseId { get; set; }
        public long AmountExpenses { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long InvoiceId { get; set; }
    }

    public class InvoiceExpenses
    {
        public Int64 ExpenseId { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
    }
    public class usp_InvoiceExpenseType
    {
        public decimal? ActionType { get; set; }
        public long InvoiceExpenseTypeId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
    }


    public class usp_Department
    {
        public decimal? ActionType { get; set; }
        public long DepartID { get; set; }
        public string DepartCode { get; set; }
        public string DepartName { get; set; }
        public string Contact { get; set; }
        public string ContactOther { get; set; }
        public string EmailAdd { get; set; }
        public string WebAdd { get; set; }
        public string Address { get; set; }
        public long? CustomerID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUser { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public long GROUPID { get; set; }
        public long COMPANYID { get; set; }
        public string GroupName { get; set; }
        public string CompanyName { get; set; }
    }

    public class usp_Groups
    {
        public decimal? ActionType { get; set; }
        public long GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string Contact { get; set; }
        public string ContactOther { get; set; }
        public string EmailAdd { get; set; }
        public string WebAdd { get; set; }
        public string Address { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CompanyAccess { get; set; }
    }


    public class usp_Customer
    {
        public Nullable<decimal> ActionType { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerAdd { get; set; }
        public string Description { get; set; }
        public string CustomerContact1 { get; set; }
        public string CustomerContact2 { get; set; }
        public string CustomerEmail { get; set; }
        public string WebAdd { get; set; }
        public Nullable<long> GroupID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string CreatedByUserID { get; set; }
        public string ModifiedByUserID { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
    public class usp_Item
    {
        public decimal? ActionType { get; set; }
        public long ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal? weight { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public bool? IsGeneralItem { get; set; }
        public long? OwnerID { get; set; }
    }

    public class usp_OrderDetailItem
    {

        public decimal? ActionType { get; set; }
        public long OrderDetailItemID { get; set; }
        public long OrderDetailID { get; set; }
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int? ItemQty { get; set; }
        public double? Weight { get; set; }
        public string Unit { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreadtedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class usp_Brokers
    {
        public decimal? ActionType { get; set; }
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? Phone { get; set; }
        public long? Phone2 { get; set; }
        public long? HomeNo { get; set; }
        public string Address { get; set; }
        public long? NIC { get; set; }
        public string Description { get; set; }
        public bool? isActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class usp_Order
    {
        public decimal? ActionType { get; set; }
        public long OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public long CustomerID { get; set; }
        public bool IsForward { get; set; }
        public int? Department { get; set; }
        public bool? IsResponseBackToCustomer { get; set; }
        public bool IsInquiryToOrder { get; set; }
        public bool? IsComplete { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CompanyId { get; set; }
        public int? GroupID { get; set; }
        public int? DepartmentID { get; set; }
        public bool? ExistingCustomer { get; set; }
        public string CustomerGroupId { get; set; }
        public string CustomerGroupCode { get; set; }
        public string CustomerGroup { get; set; }
        public long? CustomerCompanyId { get; set; }
        public string CustomerCompany { get; set; }
        public string CustomerCompanyCode { get; set; }
        public long? CustomerDepartmentId { get; set; }
        public string CustomerDepartment { get; set; }
        public string CustomerDepartmentCode { get; set; }
        public long? CustomerPersonId { get; set; }
        public string CustomerPerson { get; set; }
        public string CustomerPersonCode { get; set; }
        public string CustomerContact { get; set; }
        public long? ExistingRefference { get; set; }
        public long? RefferenceGroupId { get; set; }
        public string RefferenceGroupCode { get; set; }
        public string RefferenceGroup { get; set; }
        public long? RefferenceCompanyId { get; set; }
        public string RefferenceCompany { get; set; }
        public string RefferenceCompanyCode { get; set; }
        public long? RefferenceDepartmentId { get; set; }
        public string RefferenceDepartment { get; set; }
        public string RefferenceDepartmentCode { get; set; }
        public long? RefferencePersonId { get; set; }
        public string RefferencePerson { get; set; }
        public string RefferencePersonCode { get; set; }
        public string RefferenceContact { get; set; }
        public long? ReceiverGroupId { get; set; }
        public string ReceiverGroupCode { get; set; }
        public string ReceiverGroup { get; set; }
        public long? ReceiverCompanyId { get; set; }
        public string ReceiverCompany { get; set; }
        public string ReceiverCompanyCode { get; set; }
        public long? ReceiverDepartmentId { get; set; }
        public string ReceiverDepartment { get; set; }
        public string ReceiverDepartmentCode { get; set; }
        public long? ReceiverPersonId { get; set; }
        public string ReceiverPerson { get; set; }
        public string ReceiverPersonCode { get; set; }
        public string ReceiverContact { get; set; }
        public long? ExistingBillTo { get; set; }
        public long? BillToGroupId { get; set; }
        public string BillToGroupCode { get; set; }
        public string BillToGroup { get; set; }
        public long? BillToCompanyId { get; set; }
        public string BillToCompany { get; set; }
        public string BillToCompanyCode { get; set; }
        public long? BillToDepartmentId { get; set; }
        public string BillToDepartment { get; set; }
        public string BillToDepartmentCode { get; set; }
        public long? BillToPersonId { get; set; }
        public string BillToPerson { get; set; }
        public string BillToPersonCode { get; set; }
        public string BillToContact { get; set; }
        public string ChallanNo { get; set; }
        public string Status { get; set; }
        public bool? OrderCompleted { get; set; }
        public bool? Active { get; set; }
        public DateTime ChallanDate { get; set; }
        public bool OrderPlacment { get; set; }
        public string BiltyNo { get; set; }
        public string ManualBiltyNo { get; set; }
        public DateTime? ManualBiltyDate { get; set; }
        public Boolean PartBilty { get; set; }
    }

    public class usp_OrderDetail
    {
        public string ChallanNo { get; set; }
        public DateTime ChallanDate { get; set; }
        public decimal? ActionType { get; set; }
        public long OrderDetailId { get; set; }
        public long OrderID { get; set; }
        public string BiltyNo { get; set; }
        public string PaymentType { get; set; }
        public DateTime? BiltyNoDate { get; set; }
        public string ManualBiltyNo { get; set; }
        public DateTime? ManualBiltyDate { get; set; }
        public string CustomerCode { get; set; }
        public int ShipmentTypeId { get; set; }
        public int? VehicleTypeId { get; set; }
        public string ShipmentType { get; set; }
        public string FreightTypeName { get; set; }
        public string VehicleTypeName { get; set; }
        public int? BrokerId { get; set; }
        public string BrokerName { get; set; }
        public string BrokerContactNo { get; set; }
        public double? AdditionalWeight { get; set; }
        public double? NetWeight { get; set; }
        public double? TotalExpenses { get; set; }
        public int? FreightTypeId { get; set; }
        public int? FreightTypeQty { get; set; }
        public Double? Freight { get; set; }
        public long? LocalFreight { get; set; }
        public string DA_NO { get; set; }
        public long? ParentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public double AdditionalFreight { get; set; }
    }

    public class usp_GetCityByCustomerCode
    {
        public long CityID { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public long? ProvinceID { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public bool? IsActive { get; set; }
    }
    public class usp_City
    {
        public decimal? ActionType { get; set; }
        public long CityID { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public long? ProvinceID { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedByUserID { get; set; }
        public long? ModifiedByUserID { get; set; }
        public bool? IsActive { get; set; }
        public string ProvinceName { get; set; }
    }

    public class usp_DocumentType
    {
        public decimal? ActionType { get; set; }
        public long DocumentTypeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? isActive { get; set; }
        public long? CreatedByID { get; set; }
        public long? ModifiedByID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
    public class usp_OrderDocument
    {
        public decimal? ActionType { get; set; }
        public long OrderDocumentId { get; set; }
        public long OrderDetailId { get; set; }
        public long? DocumentTypeId { get; set; }
        public string DocumentNo { get; set; }
        public string Name { get; set; }
        public string Attachment { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }

    public class usp_OrderExpenses
    {
        public decimal? ActionType { get; set; }
        public long OrderExpenseId { get; set; }
        public long OrderDetailId { get; set; }
        public long ExpenseTypeId { get; set; }
        public string ExpensesTypeName { get; set; }
        public double? Amount { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }

    public class usp_OrderLocations
    {
        public decimal? ActionType { get; set; }
        public long LocationId { get; set; }
        public long OrderDetailId { get; set; }
        public long PickupLocationId { get; set; }
        public string PickupLocationAddress { get; set; }
        public long DropLocationId { get; set; }
        public string DropLocationAddress { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? StationFrom { get; set; }
        public long? StationTo { get; set; }
        public string DeliveryType { get; set; }
        public string PickupLocation { get; set; }
        public string DropLocation { get; set; }
        public string StationPick { get; set; }
        public string StationDrop { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverContact { get; set; }
        public Int64 ContactNumber { get; set; }

        public Int64 SecondaryContactNumber { get; set; }

    }

    public class usp_SearchManualBilty
    {
        public Int64 OrderDetailId { get; set; }
        public string PaymentTerm { get; set; }
        public long OrderID { get; set; }
        public Boolean PartBilty { get; set; }
        public string BiltyNo { get; set; }
        public DateTime? BiltyNoDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CityName { get; set; }
        public string ReceiverName { get; set; }
        public Double? Freight { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ChallanNo { get; set; }
        public DateTime ChallanDate { get; set; }
        public long Quantity { get; set; }
        public string ManualBiltyNo { get; set; }
        public string DA_No { get; set; }
        public Int64 ParentId { get; set; }
        public string VehicleCode { get; set; }
        public double? NetWeight { get; set; }
        public double? AdditionalWeight { get; set; }
        public Int64 LocalFreight { get; set; }
    }

    public class usp_OrderPackageTypes
    {
        public decimal? ActionType { get; set; }
        public long OrderPackageId { get; set; }
        public long OrderDetailId { get; set; }
        public long PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public long? Quantity { get; set; }
        public double? UnitWeight { get; set; }
        public double? UnitFreight { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string RateType { get; set; }
        public double? WeightPerUnit { get; set; }
        public Int64 ProfileDetailId { get; set; }
        public double? AdditionalCharges { get; set; }
        public double? LabourCharges { get; set; }
        public string DC_NO { get; set; }
    }
    public class usp_GetOrderPackageByOrderId
    {
        public string ActionType { get; set; }
        public string ItemName { get; set; }
        public double? AdditionalCharges { get; set; }
        public double? LabourCharges { get; set; }
        public long PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
        public long ItemId { get; set; }
        public long? Quantity { get; set; }
        public double? UnitWeight { get; set; }
        public double? UnitFreight { get; set; }
        //public long? ModifiedBy { get; set; }
        public string RateType { get; set; }
        public double WeightPerUnit { get; set; }
        public long ProfileDetailId { get; set; }
    }
    public class usp_FreightType
    {
        public decimal? ActionType { get; set; }
        public long FreightTypeID { get; set; }
        public string FreightTypeCode { get; set; }
        public string FreightTypeName { get; set; }
        public string Description { get; set; }
    }
    public class usp_Challan
    {
        public string DropCity { get; set; }
        public string Station { get; set; }
        public string PICKLOACTION { get; set; }
        public string DROPLOCATION { get; set; }
        public long OrderDetailId { get; set; }
        public string CustomerPerson { get; set; }
        public DateTime? BiltyDate { get; set; }
        public string BiltyNo { get; set; }
        public string ShipmentType { get; set; }
        public string DA { get; set; }
        public string ItemName { get; set; }
        public long? Quantity { get; set; }
        public double? Weight { get; set; }
        public string ExpensesTypeName { get; set; }
        public long? LocalFreight { get; set; }
        public Double? Freight { get; set; }
    }
    public class usp_getBiltiesByChallanID
    {
        public string DropCity { get; set; }
        public string Station { get; set; }
        public string PICKLOACTION { get; set; }
        public string DROPLOCATION { get; set; }
        public long OrderDetailId { get; set; }
        public string CustomerPerson { get; set; }
        public DateTime? BiltyDate { get; set; }
        public string BiltyNo { get; set; }
        public string ShipmentType { get; set; }
        public string DA { get; set; }
        public string ItemName { get; set; }
        public long? Quantity { get; set; }
        public double? Weight { get; set; }
        public string ExpensesTypeName { get; set; }
        public long? LocalFreight { get; set; }
        public Double? Freight { get; set; }
    }
    public class RequestParameter
    {
        public string BiltyNo { get; set; }
        public Int64 CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Int64 BiltyFrom { get; set; }
        public Int64 BiltyTo { get; set; }
        public Int64 CityFrom { get; set; }
        public Int64 CityTo { get; set; }
        public string ActionType { get; set; }
    }
    public class usp_CustomerProfile
    {
        public decimal? ActionType { get; set; }
        public long? ProfileId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public long? CustomerId { get; set; }
        public long? OwnCompanyId { get; set; }
        public string PaymentTerm { get; set; }
        public string CreditTerm { get; set; }
        public string InvoiceFormat { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public string OwnCompany { get; set; }
        public string OwnCompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public bool isHide { get; set; }
        public bool IsAdditionalCharges { get; set; }
        public bool IsLaborCharges { get; set; }
    }

    public class usp_CustomerProfileDetail
    {
        public decimal? ActionType { get; set; }
        public string LocationFromName { get; set; }
        public string LocationToName { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public long CustomerId { get; set; }
        public long ProfileDetail { get; set; }
        public long? ProfileId { get; set; }
        public string ProductCode { get; set; }
        public long? ProductId { get; set; }
        public long? LocationFrom { get; set; }
        public long? LocationTo { get; set; }
        public string RateType { get; set; }
        public long? DoorStepRate { get; set; }
        public double? Total { get; set; }
        public long? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public Double? Rate { get; set; }
        public double? WeightPerUnit { get; set; }
        public double? AdditionCharges { get; set; }
        public double? LabourCharges { get; set; }
        public string PackageTypeName { get; set; }
    }

    public class usp_Product
    {
        public decimal? ActionType { get; set; }
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? PackageTypeId { get; set; }
        public string Category { get; set; }
        public string Gener { get; set; }
        public string Nature { get; set; }
        public string DimensionUnit { get; set; }
        public double? Weight { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string Unit { get; set; }
        public double? Volume { get; set; }
        public double? Length { get; set; }
        public string PackageTypeName { get; set; }
        //public decimal? ActionType { get; set; }
        //public long ID { get; set; }
        //public string Code { get; set; }
        //public string Name { get; set; }
        //public long? PackageTypeId { get; set; }
        //public string Category { get; set; }
        //public string Gener { get; set; }
        //public string Nature { get; set; }
        //public string DimensionUnit { get; set; }
        //public double? Weight { get; set; }
        //public string Description { get; set; }
        //public bool? Status { get; set; }
        //public long? CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public long? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public double? Width { get; set; }
        //public double? Height { get; set; }
        //public double? Unit { get; set; }
        //public double? Volume { get; set; }
        //public double? Length { get; set; }
    }

    public class usp_SearchCompany
    {
        public long CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string PaymentTerm { get; set; }
        public string OwnCompany { get; set; }
    }
    public class usp_GetChallanReport
    {
        public string Transportor { get; set; }
        public string ProductName { get; set; }
        public string BiltyNo { get; set; }
        public string DA_No { get; set; }
        public string ReceiverCompany { get; set; }
        public string CustomerCompany { get; set; }
        public string Pickup { get; set; }
        public string Drop { get; set; }
        public long? Quantity { get; set; }
        public double? UnitWeight { get; set; }
        public long? LocalFreight { get; set; }
        public long? DoorStepRate { get; set; }
        public double? Freight { get; set; }
        public string PaymentTerm { get; set; }
        public Int64 ContactNumber { get; set; }

        public Int64 SecondaryContactNumber { get; set; }
    }

    public class usp_GetChallanList
    {
        public Int64 ChallanId { get; set; }
        public string DropStation { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string BiltyNo { get; set; }
        public DateTime? BiltyNoDate { get; set; }
        public string ChallanNo { get; set; }
        public DateTime? ChallanDate { get; set; }
        public string VehicleCode { get; set; }
        public string DriverName { get; set; }
        public string DriverCode { get; set; }
        public string BiltyType { get; set; }
        public string MobileNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class usp_SearchChallanList
    {
        public string DropStation { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string BiltyNo { get; set; }
        public DateTime? BiltyNoDate { get; set; }
        public string ChallanNo { get; set; }
        public DateTime? ChallanDate { get; set; }
        public string VehicleCode { get; set; }
        public string DriverName { get; set; }
        public string DriverCode { get; set; }
        public string BiltyType { get; set; }
        public string MobileNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class Report4
    {
        public string ShipmentTypeDetail { get; set; }
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DA_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public string WeightPerUnit { get; set; }
    }

    public class Report9
    {
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DC_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public string RegNo { get; set; }
        public double? Amount { get; set; }
        public string Weight { get; set; }
        public string Vehicle { get; set; }
        public Int64 Coil { get; set; }
        public Int64 Drum { get; set; }
    }
    public class Report10
    {
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DC_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public string RegNo { get; set; }
        public double? Amount { get; set; }
        public string Weight { get; set; }
        public string Vehicle { get; set; }
        public Int64 Coil { get; set; }
        public Int64 Drum { get; set; }
        public string ReceiverName { get; set; }
        public double Rate { get; set; }
        public double Charges { get; set; }
    }
    public class Report5
    {
        public string ShipmentTypeDetail { get; set; }
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DA_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public string WeightPerUnit { get; set; }
        public string ReceiverName { get; set; }
    }

    public class Report6
    {
        public string ShipmentTypeDetail { get; set; }
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DA_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public string WeightPerUnit { get; set; }
        public string ReceiverName { get; set; }
        public string VehicleNo { get; set; }
        public string ContainerType { get; set; }
    }

    public class Report2
    {
        public string ShipmentTypeDetail { get; set; }
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DA_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public string Weight { get; set; }
    }
    public class Report7
    {
        public string ShipmentTypeDetail { get; set; }
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DA_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public string Weight { get; set; }
        public string VehicleNo { get; set; }
        public string ContainerType { get; set; }
        public Int64 Quantity { get; set; }
    }
    public class Report3
    {
        public string ShipmentTypeDetail { get; set; }
        public string Date { get; set; }
        public string BiltyNo { get; set; }
        public string DA_No { get; set; }
        public string Station { get; set; }
        public string CustomerName { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public long? Quantity { get; set; }
        public string Weight { get; set; }
    }
    public class Invoices
    {

        public long InvoiceNo { get; set; }
        public int? ReportId { get; set; }
        public string ReportName { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public long? ModifiedBy { get; set; }
        public string BiltyNo { get; set; }
        public string Expenses { get; set; }
        public long? ExpensesAmount { get; set; }
        public long? SaleTax { get; set; }
    }

    public class usp_ReportsType
    {
        public Int32 ReportId { get; set; }
        public string ReportName { get; set; }
        public bool Active { get; set; }
    }
    public class usp_Invoices
    {
        public decimal? ActionType { get; set; }
        public long InvoiceNo { get; set; }
        public string ReportName { get; set; }
        public string CompanyName { get; set; }
        public int? ReportId { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public long? ModifiedBy { get; set; }
        public string BiltyNo { get; set; }
        public string BillCode { get; set; }
        public string Expenses { get; set; }
        public long ExpensesAmount { get; set; }
        public double? SaleTax { get; set; }
        public long InvoiceTotalAmount { get; set; }
    }
    public class usp_GetOrderForInvoice
    {
        public string PICKLOACTION { get; set; }
        public string DROPLOCATION { get; set; }
        public string DropCity { get; set; }
        public string Station { get; set; }
        public long OrderDetailId { get; set; }
        public string CustomerPerson { get; set; }
        public DateTime? BiltyDate { get; set; }
        public string BiltyNo { get; set; }
        public string ShipmentType { get; set; }
        public string DA { get; set; }
        public string ItemName { get; set; }
        public long? Quantity { get; set; }
        public double? Weight { get; set; }
        public long? LocalFreight { get; set; }
        public double? Freight { get; set; }
    }

    public class usp_CustomerProfileDetail_audit
    {
        public string UserName { get; set; }
        public string LocationFromName { get; set; }
        public string LocationToName { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public long? CustomerId { get; set; }
        public long ProfileDetail_AuditID { get; set; }
        public long? ProfileDetail { get; set; }
        public long? ProfileId { get; set; }
        public string ProductCode { get; set; }
        public long? ProductId { get; set; }
        public long? LocationFrom { get; set; }
        public long? LocationTo { get; set; }
        public string RateType { get; set; }
        public long? DoorStepRate { get; set; }
        public double? Total { get; set; }
        public long? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public double? Rate { get; set; }
        public double? WeightPerUnit { get; set; }
        public double? AdditionCharges { get; set; }
        public double? LabourCharges { get; set; }
        public string AuditDataState { get; set; }
        public string AuditDMLAction { get; set; }
        public string AuditUser { get; set; }
        public DateTime? AuditDateTime { get; set; }
        public string UpdateColumns { get; set; }
    }

    public class usp_MCBBranches
    {
        public decimal? ActionType { get; set; }
        public long ID { get; set; }
        public string BR_CODE { get; set; }
        public string BranchName { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }
    public class usp_BiltyPDF
    {
        public string CustomerCompany { get; set; }
        public string Remarks { get; set; }
        public double AdditionalWeight { get; set; }
        public long OrderDetailId { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PaymentType { get; set; }
        public string OwnCompany { get; set; }
        public string BiltyNo { get; set; }
        public DateTime? BiltyNoDate { get; set; }
        public string PickupCity { get; set; }
        public string DropCity { get; set; }
        public string DA_No { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverContact { get; set; }
        public string Name { get; set; }
        public long? SecondaryContactNo { get; set; }
        public long? ContactNo { get; set; }
        public long? OrderPackageId { get; set; }
        public string ProductName { get; set; }
        public long? PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
        public long? ItemId { get; set; }
        public long? Quantity { get; set; }
        public double? UnitWeight { get; set; }
        public double? UnitFreight { get; set; }
        public string RateType { get; set; }
        public double WeightPerUnit { get; set; }
        public double Freight { get; set; }
    }
    public class usp_GetBiltyPDFExpenses
    {
        public long OrderExpenseId { get; set; }
        public long OrderDetailId { get; set; }
        public long ExpenseTypeId { get; set; }
        public string ExpensesTypeName { get; set; }
        public double? Amount { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }

}
