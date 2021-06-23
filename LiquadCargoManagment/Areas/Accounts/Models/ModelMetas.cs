
using LiquadCargoManagment.Areas.Accounts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LiquadCargoManagment.Areas.Accounts.Models
{
    public class CustomerModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public class UsersLeadListModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public class StatusModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
  
    public class SourceModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class ServicesModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class BreadCrumbMenu
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string AccessURL { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
    public class ProfileModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
    public class RoleModel
    {
        public int? ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int BranchID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class UserModel
    {
        public int? ID { get; set; }
        public int? BranchID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Required")]


        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int DeletedBy { get; set; }
    }

    public class IncomeModel
    {
        public int ID { get; set; }
        public string Payee { get; set; }
        public int BankID { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
  
    }
    public class BankModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

    }
    public class ChartOfAccountsModel
    {
        public int ID { get; set; }
        public int AccountsHeadId { get; set; }
        public int GLTypeId { get; set; }
        public int SubAccountsId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

    }
    public class ARInvoiceModel
    {
        public ARInvoiceModel()
        {
            ARInvoiceDetails = new List<ARInvoiceDetail>();
        }
        public int ID { get; set; }
        public string Code { get; set; }
        public System.DateTime PostingDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public System.DateTime DocumentDate { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public double TaxAmount { get; set; }
        public double GrandTotal { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<ARInvoiceDetail> ARInvoiceDetails { get; set; }
    }

    //public class ARInvoiceDetails
    //{
    //    public int Product { get; set; }
    //    public int Qty { get; set; }
    //}
    public class WishModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Priority { get; set; }
        public DateTime ExpectadDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

    }


    public class UserDocumentModel
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string IsApprove { get; set; }
    }

    public class ExecutePayment
    {
        public int PaymentMethodId { get; set; }
        public string CustomerName { get; set; }
        public string DisplayCurrencyIso { get; set; }
        public string MobileCountryCode { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public int InvoiceValue { get; set; }
        public string CallBackUrl { get; set; }
        public string ErrorUrl { get; set; }
        public string Language { get; set; }
        public string CustomerReference { get; set; }
        public int CustomerCivilId { get; set; }
        public string UserDefinedField { get; set; }
        public string ExpiryDate { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public InvoiceItems[] InvoiceItems { get; set; }
        public Payment payment { get; set; }

       
    }

    public class InvoiceItems
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Weight { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
    }
    public class CustomerAddress
    {
        public string Block { get; set; }
        public string Street { get; set; }
        public string HouseBuildingNo { get; set; }
        public string Address { get; set; }
        public string AddressInstructions { get; set; }
    }

    public class Payment
    {
        public string paymentType { get; set; }
        public Card Card { get; set; }
        public bool saveToken { get; set; }
    }

    public class Card
    {
        public string Number { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string SecurityCode { get; set; }
        public string CardHolderName { get; set; }
    }



    public class PaymentStatus
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string PaymentId { get; set; }
    }

    public class BookingModel
    {
        public int ID { get; set; }
        public string PaymentId { get; set; }
        public int CutomerId { get; set; }
        public int CarsId { get; set; }
        public string CarName { get; set; }
        public string CarImage { get; set; }
        public int? PromoCodeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PickLocation { get; set; }
        public string DropLocation { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public int TotalAmount { get; set; }
        public string Car { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public string promo { get; set; }
    }
    public class CarsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double PricePerDay { get; set; }
        public string Persons { get; set; }
        public string Transmission { get; set; }
        public string MPG { get; set; }
        public string Luggage { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

    public class SearchModel
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Transmission { get; set; }
        public string Model { get; set; }
        public string Text { get; set; }


    }

    public class VoucherModel
    {
      
        public int ID { get; set; }
        public string Code { get; set; }
        public DateTime Expire { get; set; }
        public int Discount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }


    }
}