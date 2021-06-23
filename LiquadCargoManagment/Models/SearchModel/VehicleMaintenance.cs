using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class VehicleMaintenances
    {
        private readonly LCMEntities context;
        public VehicleMaintenances(LCMEntities _context)
        {
            context = _context;
        }
        public List<VehicleMaintenance> getSearchVehicleMaintenance(DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
       
        public List<VehicleMaintenance> getSearchVehicleMaintenance(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.VehicleMaintenances.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.VehicleMaintenances.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<VehicleMaintenance> SearchVehicleStartFromExpiry(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.VehicleMaintenances.Where(x => x.StartFrom >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.VehicleMaintenances.Where(x => x.ExpiryDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<VehicleMaintenance> SearchVehicleDateAlertType(DateTime DateFrom, DateTime DateTo, string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.AlertType == x.AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
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
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= ExpiryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateFromRenewal(DateTime DateFrom, DateTime RenewalDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= RenewalDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateFromRenewalExpiry(DateTime DateFrom, DateTime RenewalDate, DateTime ExpiryDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate == DateFrom && x.CreatedDate >= RenewalDate && x.ExpiryDate <= ExpiryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentSmsEmail(int? SmsAlert, string EmailAlert)
        {
            return context.VehicleDocuments.Where(x => x.SMSAlert == SmsAlert && x.EmailAlert == EmailAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDocumentSmsAlertType(int? SmsAlert, string AlertType)
        {
            return context.VehicleDocuments.Where(x => x.SMSAlert == SmsAlert && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleEmailAlertType(string EmailAlert, string AlertType)
        {
            return context.VehicleDocuments.Where(x => x.EmailAlert == EmailAlert && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleSmsEmailAlertType(string EmailAlert, string AlertType, int? SmsAlert)
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
        public List<VehicleMaintenance> SearchVehicleMaintenanceAll( int? VehicleRegNo, int? VehicleType, int? Status , int? OMC, int? MaintenanceTypeID,DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate,  string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo && x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID &&  x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceRegVehicleTypeStatusOMCTypeIDDateFromDateToStartFromExpiry(int? VehicleRegNo, int? VehicleType, int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo && x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceRegVehicleTypeStatusOMCTypeIDDateFromDateToStartFrom(int? VehicleRegNo, int? VehicleType, int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo && x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceRegVehicleTypeStatusOMCTypeIDDateFromDateToStartFrom(int? VehicleRegNo, int? VehicleType, int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo && x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceVehicleTypeStatusOMCTypeIDDateFromDateToStartFromExpiryType(int? VehicleType, int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceVehicleTypeStatusOMCTypeIDDateFromDateToStartFromExpiry(int? VehicleType, int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceVehicleTypeStatusOMCTypeIDDateFromDateToStartFrom(int? VehicleType, int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceVehicleTypeStatusOMCTypeIDDateFromDateTo(int? VehicleType, int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusOMCTypeIDDateFromDateToStartFromExpiryType( int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x =>  x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusOMCTypeIDDateFromDateToStartFromExpiry(int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusOMCTypeIDDateFromDateToStartFrom(int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusOMCTypeIDDateFromDateToStartFrom(int? Status, int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == Status && x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCTypeIDDateFromDateToStartFromExpiryType( int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x =>  x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCTypeIDDateFromDateToStartFromExpiry(int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCTypeIDDateFromDateToStartFrom(int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCTypeIDDateFromDateTo(int? OMC, int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == OMC && x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeIDDateFromDateToStartFromExpiryType( int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x =>  x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeIDDateFromDateToStartFromExpiry(int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeIDDateFromDateToStartFrom(int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeIDDateFromDateToStartFrom(int? MaintenanceTypeID, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.MaintenanceTypeID == MaintenanceTypeID && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceRegDateFromToStartExpiryType(int? VehicleRegNo, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo &&  x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceRegDateFromToStartExpiry(int? VehicleRegNo, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceRegDateFromToStart(int? VehicleRegNo, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceRegDateFromTo(int? VehicleRegNo, DateTime DateFrom, DateTime DateTo )
        {
            return context.VehicleMaintenances.Where(x => x.VehicleRegNo == VehicleRegNo && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeDateFromToStartExpiryType(int? VehicleType, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeDateFromToStartExpiry(int? VehicleType, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeDateFromToStart(int? VehicleType, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceTypeDateFromTo(int? VehicleType, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.VehicleType == VehicleType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusDateFromToStartExpiryType(int? Status, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == Status && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusDateFromToStartExpiry(int? Status, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == Status && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusDateFromToStart(int? Status, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == Status && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceStatusDateFromTo(int? Status, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.OwnerShip == Status && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCDateFromToStartExpiryType(int? OMC, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.PrincipleCompany == OMC && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCDateFromToStartExpiry(int? OMC, DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate)
        {
            return context.VehicleMaintenances.Where(x => x.PrincipleCompany == OMC && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCDateFromToStart(int? OMC, DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.PrincipleCompany == OMC && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceOMCDateFromTo(int? OMC, DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleMaintenances.Where(x => x.PrincipleCompany == OMC && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceDateFromDateToStartFromExpiryAlertType(DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate, string AlertType)
        {
            return context.VehicleMaintenances.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate && x.AlertType == AlertType && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceDateFromDateToStartFromExpiry(DateTime DateFrom, DateTime DateTo, DateTime StartFrom, DateTime ExpiryDate )
        {
            return context.VehicleMaintenances.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom && x.ExpiryDate == ExpiryDate  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleMaintenance> SearchVehicleMaintenanceDateFromDateToStartFrom(DateTime DateFrom, DateTime DateTo, DateTime StartFrom)
        {
            return context.VehicleMaintenances.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.StartFrom >= StartFrom  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
       
        public List<VehicleDocument> SearchVehicleDateSmsEmail(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate, int? SmsAlert, string EmailAlert)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate && x.SMSAlert == SmsAlert && x.EmailAlert == EmailAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateSms(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate, int? SmsAlert)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate && x.SMSAlert == SmsAlert && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateRenewalExpiry(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate, DateTime ExpiryDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && x.ExpiryDate == ExpiryDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<VehicleDocument> SearchVehicleDateRenewal(DateTime DateFrom, DateTime DateTo, DateTime RenewalDate)
        {
            return context.VehicleDocuments.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.RenewalDate >= RenewalDate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
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