using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class CompactBilty
    {
        private readonly LCMEntities context;
        public CompactBilty(LCMEntities _context)
        {
            context = _context;
        }
        public List<Bilty> getSearchBilty(DateTime DateFrom, DateTime DateTo)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> getSearchBilty(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Bilties.Where(x => x.OrderDate >= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Bilties.Where(x => x.OrderDate <= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<Bilty> SearchBiltyDateVehicle(DateTime DateFrom, DateTime DateTo, int? Vehicle)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateBillTo(DateTime DateFrom, DateTime DateTo, int? BillTo)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateReceiver(DateTime DateFrom, DateTime DateTo, int? Receiver)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateOwnCompany(DateTime DateFrom, DateTime DateTo, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateSender(DateTime DateFrom, DateTime DateTo, int? Sender)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.PickLocation == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        //Multiple Selected Search
        public List<Bilty> SearchBiltyDateVehicleBillTo(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateVehicleBillToReceiver(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateVehicleBillToReceiverOwnCompany(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateVehicleBillToReceiverOwnCompanySender(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, int? Sender)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.PickLocation == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyAllFilter(DateTime DateFrom, DateTime DateTo, DateTime DeliveryDate, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, int? OrderNo)
        {
            return context.Bilties.Where(x => x.OrderDate == DateFrom && x.OrderDate == DateTo && x.DeliveryDate == DeliveryDate && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.OrderNo == OrderNo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyAllFilters(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, int? OrderNo, int? Sender)
        {
            return context.Bilties.Where(x => x.OrderDate == DateFrom && x.OrderDate == DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.OrderNo == OrderNo && x.PickLocation == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownVB(int? Vehicle, int? BillTo)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        //public List<Bilty> SearchBiltyDropDownVBD(int? Vehicle, int? BillTo,)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        public List<Bilty> SearchBiltyDropDownVR(int? Vehicle, int? Receiver)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownVO(int? Vehicle, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownVS(int? Vehicle, int? Sender)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.PickLocation == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownVBR(int? Vehicle, int? BillTo, int? Reciever)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Reciever && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownVBRO(int? Vehicle, int? BillTo, int? Reciever, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Reciever && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownVBROS(int? Vehicle, int? BillTo, int? Reciever, int? OwnCompany, int? Sender)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Reciever && x.OwnCompany == OwnCompany && x.PickLocation == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownBR(int? BillTo, int? Receiver)
        {
            return context.Bilties.Where(x => x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownBO(int? BillTo, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.BillTo == BillTo && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownBS(int? BillTo, int? Sender)
        {
            return context.Bilties.Where(x => x.BillTo == BillTo && x.PickLocation == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownRO(int? Receiver, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownRS(int? Receiver, int? Sender)
        {
            return context.Bilties.Where(x => x.Reciever == Receiver && x.PickLocation == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
    }
}