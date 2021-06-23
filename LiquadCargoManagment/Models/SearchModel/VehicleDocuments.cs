using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class VehicleDocuments
    {
        private readonly LCMEntities context;
        public VehicleDocuments(LCMEntities _context)
        {
            context = _context;
        }
        public List<VehicleDocument> getSearchVehicleDocument(DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        //public List<VehicleDocument> getSearchVehicleDates(DateTime DateFrom, DateTime DateTo , DateTime RenewalDate, DateTime ExpiryDate)
        //{
        //    return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate <= ExpiryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        public List<VehicleDocument> getSearchVehicleDocument(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.VehicleDocuments.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.VehicleDocuments.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<VehicleDocument> SearchVehicleRenewalExpiry(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.VehicleDocuments.Where(x => x.RenewalDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.VehicleDocuments.Where(x => x.ExpiryDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<VehicleDocument> SearchVehicleDateEmailAlert(DateTime DateFrom, DateTime DateTo, string EmailAlert)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.EmailAlert == x.EmailAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateSmsAlert(DateTime DateFrom, DateTime DateTo, int? SmsAlert)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SMSAlert == SmsAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        //public List<VehicleDocument> SearchVehicleSmsAlert(int? VehicleRegNo, int? SmsAlert)
        //{
        //    return context.VehicleDocuments.Where(x => x.VehicleRegNo == VehicleRegNo && x.SMSAlert == SmsAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<VehicleDocument> SearchVehicleEmailAlert(int? VehicleRegNo, string EmailAlert)
        //{
        //    return context.VehicleDocuments.Where(x => x.VehicleRegNo == VehicleRegNo && x.EmailAlert == EmailAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<VehicleDocument> SearchVehicleRegDate(int? VehicleRegNo, DateTime DateFrom, DateTime DateTo)
        //{
        //    return context.VehicleDocuments.Where(x => x.VehicleRegNo == VehicleRegNo && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        public List<VehicleDocument> SearchVehicleRenewalExpiry(DateTime RenewalDate, DateTime ExpiryDate)
        {
            return context.VehicleDocuments.Where(x => x.RenewalDate >= RenewalDate && x.CreatedDate <= ExpiryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        //public List<VehicleDocument> SearchVehicleDoucumentAllFilter(int? VehicleRegNo, DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate, int? SmsAlert, string EmailAlert, string AlertType, int? AlertBefore)
        //{
        //    return context.VehicleDocuments.Where(x => x.VehicleRegNo == VehicleRegNo && x.CreatedDate == DateTo && x.CreatedDate == DateFrom && x.RenewalDate == RenewalDate && x.ExpiryDate == ExpiryDate &&  x.SMSAlert == SmsAlert && x.EmailAlert == EmailAlert && x.AlertType == AlertType && x.AlertBefore == AlertBefore && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<VehicleDocument> SearchVehicleRegNo(int? VehicleRegNo)
        //{
        //    return context.VehicleDocuments.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID) && x.VehicleRegNo == VehicleRegNo).ToList();
        //}


        
        public List<VehicleDocument> SearchVehicleDateFromExpiry(DateTime DateFrom, DateTime ExpiryDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateFromRenewal(DateTime DateFrom, DateTime RenewalDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= RenewalDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateFromRenewalExpiry(DateTime DateFrom, DateTime RenewalDate,DateTime ExpiryDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate == DateFrom && x.CreatedDate >= RenewalDate && x.ExpiryDate <= ExpiryDate &&  lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentSmsEmail(int? SmsAlert, string EmailAlert)
        {
            return context.VehicleDocuments.Where(x => x.SMSAlert == SmsAlert && x.EmailAlert == EmailAlert  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentSmsAlertType(int? SmsAlert, string AlertType)
        {
            return context.VehicleDocuments.Where(x => x.SMSAlert == SmsAlert && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleEmailAlertType(string EmailAlert, string AlertType)
        {
            return context.VehicleDocuments.Where(x => x.EmailAlert == EmailAlert && x.AlertType == AlertType  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleSmsEmailAlertType(string EmailAlert, string AlertType , int? SmsAlert)
        {
            return context.VehicleDocuments.Where(x => x.EmailAlert == EmailAlert && x.AlertType == AlertType && x.SMSAlert == SmsAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<VehicleDocument> SearchVehicleDocumentAlertTypeBefore(string AlertType, int? AlertBefore)
        {
            return context.VehicleDocuments.Where(x => x.AlertType == AlertType && x.AlertBefore == AlertBefore && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchBiltyDropDownVP(int? VehicleType, int? PrincipleCompany)
        {
            return context.VehicleDocuments.Where(x => x.VehicleType == VehicleType && x.PrincipleCompany == PrincipleCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchBiltyDropDownVDT(int? Vehicle, int? DocumentType)
        {
            return context.VehicleDocuments.Where(x => x.VehicleOwnership == Vehicle && x.VehicleDocumentType == DocumentType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentDropDownVD(int? Vehicle, int? Documents)
        {
            return context.VehicleDocuments.Where(x => x.VehicleOwnership == Vehicle && x.VehicleDocuments == Documents && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
       
        public List<VehicleDocument> SearchVehicleDocumentDropDownVDT(int? VehicleType, int? DocumentType)
        {
            return context.VehicleDocuments.Where(x => x.VehicleType == VehicleType && x.VehicleDocumentType == DocumentType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentDropDownVDD(int? VehicleType, int? Documents)
        {
            return context.VehicleDocuments.Where(x => x.VehicleType == VehicleType && x.VehicleDocuments == Documents && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentDropDownVP(int? VehicleType, int? PrincipleCompany)
        {
            return context.VehicleDocuments.Where(x => x.VehicleType == VehicleType && x.PrincipleCompany == PrincipleCompany && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentAll(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate,DateTime ExpiryDate, int? SmsAlert,string EmailAlert, string AlertType, int? AlertBefore)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate && x.SMSAlert == SmsAlert && x.EmailAlert == EmailAlert && x.AlertType == AlertType && x.AlertBefore == AlertBefore  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateSmsEmailAlertType(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate, int? SmsAlert, string EmailAlert, string AlertType)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate && x.SMSAlert == SmsAlert && x.EmailAlert == EmailAlert && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateSmsEmail(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate, int? SmsAlert, string EmailAlert )
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate && x.SMSAlert == SmsAlert && x.EmailAlert == EmailAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateSms(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate, int? SmsAlert)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate && x.SMSAlert == SmsAlert  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateRenewalExpiry(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateRenewal(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate )
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
      


        //public List<Bilty> SearchBiltyDropDownBR(int? BillTo, int? Receiver)
        //{
        //    return context.Bilties.Where(x => x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownBO(int? BillTo, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.BillTo == BillTo && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownRO(int? Receiver, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
    }
}