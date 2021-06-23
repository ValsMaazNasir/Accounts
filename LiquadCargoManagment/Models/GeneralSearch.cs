using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{
    public class GeneralSearch
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? City { get; set; }
    }
    public class LocationSearch
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long? City { get; set; }
        public long? Area { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class CompactBiltyModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? OrderNo { get; set; }
        public int? Receiver  { get; set; }
        public int? BillTo { get; set; }
        public int? Vehicle { get; set; }
        public int? OwnCompany { get; set; }
        public int? Sender { get; set; }
    }

    public class ParchoonBiltyModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string OrderNo { get; set; }
        public int? Receiver { get; set; }
        public int? BillTo { get; set; }
        public int? Vehicle { get; set; }
        public int? OwnCompany { get; set; }
        public int? Sender { get; set; }
    }
    public class DieselModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Vehicle { get; set; }
        public string PetrolPump { get; set; }
        public int? DieselRate { get; set; }
        public int? OilRate { get; set; }
    }
    public class UniversalBiltyModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string OrderNo { get; set; }
        public int? Receiver { get; set; }
        public int? BillTo { get; set; }
        public int? Vehicle { get; set; }
        public int? OwnCompany { get; set; }
        public int? Sender { get; set; }
    }

    public class VehicleMaintenanceModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
        public int? VehicleRegNo { get; set; }

        public int? VehicleType { get; set; }

        public int? Status { get; set; }

        public int? OMC { get; set; }

        public int? MaintenanceTypeID { get; set; }

        public DateTime? StartFrom { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string AlertType { get; set; }
    }
    public class DesignationModel
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
    public class DriverModel
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int? ContactNo { get; set; }

        public string NIC { get; set; }

        public string LicenseNo { get; set; }
    }
    public class VehicleModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string RegNo { get; set; }
        public int? VehicleTypeID { get; set; }

    }
    public class EmployeeModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string FatherName { get; set; }

        public string Gender { get; set; }

        public string CNIC { get; set; }

        public string Contact { get; set; }

        public string OtherContact { get; set; }
    }
    public class RevenueTypeModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }


    }
    public class RevenueModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }


    }
    public class CustomerGroupModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }


    }
    public class CustomerDepartmentModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

    }
    public class CityModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? ProvinceID { get; set; }
        public string CityCode { get; set; }

        public string CityName { get; set; }
    }
    public class AreaModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? CityID { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
    }
    public class ShipmentTypeModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
    public class ExpenseTypeModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
    public class DriverTypeModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
    public class ExpenseModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
    public class DamageTypeModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
    public class PrincipleCompanyModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }




    }
    public class CustomerCompanyModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? GroupID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Contact { get; set; }
        public string Email { get; set; }
    }
    public class VendorModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int? VendorType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AccountNo { get; set; }
    }

    public class VehicleStandardModel
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }
    public class OwnCompanyModel
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int? GroupID { get; set; }

        public string Contact { get; set; }

        public string ContactOther { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string EmailAdd { get; set; }

        public string WebAdd { get; set; }
        public int? RoleId { get; set; }

        public int? SubcriptionID { get; set; }

    }

    public class OwnDepartmentModel
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }
    }
    public class BanksModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class ProductTypeModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class VehicleDocumentModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int? SmsAlert { get; set; }

        public string EmailAlert { get; set; }

        public string AlertType { get; set; }

        public int? AlertBefore { get; set; }
        public int? VehicleRegNo { get; set; }



    }
    public class VehicleTypeModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class TrailorTypeModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class TrailorModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class ContainerTypeModel

    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class ContainerModel

    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int? ContainerType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class DocumentTypeModel

    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class MaintenanceTypeModel

    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class DocumentModel

    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class GeneralTaxModel

    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }

        public int? ProvinceID { get; set; }
        public string TaxRate { get; set; }


    }

    public class TaxForTransportationModel

    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }

        public int? OriginProvinceID { get; set; }
        public int? DestinationProvinceID { get; set; }

        public string TaxRatePercentOrigin { get; set; }

        public string TaxRatePercentDestination { get; set; }


    }
    public class PackageTypeModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }
    public class DieselManagementModel
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string PetrolPump { get; set; }
        public int? OrderNo { get; set; }
        public int? DieselRate { get; set; }
        public int? OilRate { get; set; }
        public int? RegNo { get; set; }


    }
    public class ProductNatureModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }

    public class ProductBrokerModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }

    public class VendorTypeModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


    }
    public class BankAccountModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public int? Bank { get; set; }
        public string AccountTitle { get; set; }

        public string AccountNo { get; set; }

    }
}