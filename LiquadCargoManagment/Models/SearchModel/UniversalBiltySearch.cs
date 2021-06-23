using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class UniversalBiltySearch
    {
        private readonly LCMEntities context;
        public UniversalBiltySearch(LCMEntities _context)
        {
            context = _context;
        }
        public List<UniversalBilty> getSearchBilty(DateTime DateFrom, DateTime DateTo)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> getSearchBilty(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.UniversalBilties.Where(x => x.BiltyDate >= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.UniversalBilties.Where(x => x.BiltyDate <= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<UniversalBilty> SearchBiltyDateVehicle(DateTime DateFrom, DateTime DateTo, int? Vehicle)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDateBillTo(DateTime DateFrom, DateTime DateTo, int? BillTo)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDateReceiver(DateTime DateFrom, DateTime DateTo, int? Receiver)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerReceiverCustomerCode == Receiver).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDateOwnCompany(DateTime DateFrom, DateTime DateTo, int? OwnCompany)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.OwnCompanyID == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDateSender(DateTime DateFrom, DateTime DateTo, int? Sender)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerSenderCustomerCode == Sender).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        //Multiple Selected Search
        public List<UniversalBilty> SearchBiltyDateVehicleBillTo(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDateVehicleBillToReceiver(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Receiver).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDateVehicleBillToReceiverOwnCompany(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.OwnCompanyID == OwnCompany && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Receiver).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDateVehicleBillToReceiverOwnCompanySender(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, int? Sender)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate >= DateFrom && x.BiltyDate <= DateTo && x.OwnCompanyID == OwnCompany && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Receiver && s.ConsignerSenderCustomerCode == Sender).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyAllFilter(DateTime DateFrom, DateTime DateTo, DateTime DeliveryDate, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, string OrderNo)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate == DateFrom && x.BiltyDate == DateTo && x.DeliveryDate == DeliveryDate && x.ManualBiltyNo == OrderNo && x.OwnCompanyID == OwnCompany && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Receiver).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyAllFilters(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, string OrderNo, int? Sender)
        {
            return context.UniversalBilties.Where(x => x.BiltyDate == DateFrom && x.BiltyDate == DateTo && x.OwnCompanyID == OwnCompany && x.ManualBiltyNo == OrderNo && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Receiver && s.ConsignerSenderCustomerCode == Sender).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownVB(int? Vehicle, int? BillTo)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        //public List<Bilty> SearchBiltyDropDownVBD(int? Vehicle, int? BillTo,)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        public List<UniversalBilty> SearchBiltyDropDownVR(int? Vehicle, int? Receiver)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerReceiverCustomerCode == Receiver).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownVO(int? Vehicle, int? OwnCompany)
        {
            return context.UniversalBilties.Where(x => x.OwnCompanyID == OwnCompany && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownVS(int? Vehicle, int? Sender)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerSenderCustomerCode == Sender).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownVBR(int? Vehicle, int? BillTo, int? Reciever)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Reciever).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownVBRO(int? Vehicle, int? BillTo, int? Reciever, int? OwnCompany)
        {
            return context.UniversalBilties.Where(x => x.OwnCompanyID == OwnCompany && x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Reciever).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownVBROS(int? Vehicle, int? BillTo, int? Reciever, int? OwnCompany, int? Sender)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyVehicleInformations.Where(s => s.VehicleID == Vehicle).ToList().Count > 0 && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Reciever && s.ConsignerSenderCustomerCode == Sender).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownBR(int? BillTo, int? Receiver)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerReceiverCustomerCode == Receiver).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownBO(int? BillTo, int? OwnCompany)
        {
            return context.UniversalBilties.Where(x => x.CompanyID == OwnCompany && x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownBS(int? BillTo, int? Sender)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ClientCode == BillTo && s.ConsignerSenderCustomerCode == Sender).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownRO(int? Receiver, int? OwnCompany)
        {
            return context.UniversalBilties.Where(x => x.OwnCompanyID == OwnCompany && x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerReceiverCustomerCode == Receiver).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<UniversalBilty> SearchBiltyDropDownRS(int? Receiver, int? Sender)
        {
            return context.UniversalBilties.Where(x => x.UniversalBiltyCustomerInformations.Where(s => s.ConsignerReceiverCustomerCode == Receiver && s.ConsignerSenderCustomerCode == Sender).ToList().Count > 0 && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
    }
}