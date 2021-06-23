using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class ParchoonBilty_
    {
        private readonly LCMEntities context;
        public ParchoonBilty_(LCMEntities _context)
        {
            context = _context;
        }
        public List<ParchoonBilty> getSearchBilty(DateTime DateFrom, DateTime DateTo)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> getSearchBilty(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.ParchoonBilties.Where(x => x.Date >= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.ParchoonBilties.Where(x => x.Date <= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<ParchoonBilty> SearchBiltyDateVehicle(DateTime DateFrom, DateTime DateTo, int? Vehicle)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.VehicleTypeID == Vehicle && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDateBillTo(DateTime DateFrom, DateTime DateTo, int? BillTo)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.CustomerCompanyID == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDateReceiver(DateTime DateFrom, DateTime DateTo, int? Receiver)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.ReceiverID == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDateOwnCompany(DateTime DateFrom, DateTime DateTo, int? OwnCompany)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.OwnCompanyID == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDateSender(DateTime DateFrom, DateTime DateTo, int? Sender)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.PickLocationID == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        //Multiple Selected Search
        public List<ParchoonBilty> SearchBiltyDateVehicleBillTo(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDateVehicleBillToReceiver(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDateVehicleBillToReceiverOwnCompany(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Receiver && x.OwnCompanyID == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDateVehicleBillToReceiverOwnCompanySender(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, int? Sender)
        {
            return context.ParchoonBilties.Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Receiver && x.OwnCompanyID == OwnCompany && x.PickLocationID == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyAllFilter(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, string OrderNo)
        {
            return context.ParchoonBilties.Where(x => x.Date == DateFrom && x.Date == DateTo && x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Receiver && x.OwnCompanyID == OwnCompany && x.ManualBiltyNo == OrderNo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyAllFilters(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, string OrderNo, int? Sender)
        {
            return context.ParchoonBilties.Where(x => x.Date == DateFrom && x.Date == DateTo && x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Receiver && x.OwnCompanyID == OwnCompany && x.ManualBiltyNo == OrderNo && x.PickLocationID == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownVB(int? Vehicle, int? BillTo)
        {
            return context.ParchoonBilties.Where(x => x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        //public List<Bilty> SearchBiltyDropDownVBD(int? Vehicle, int? BillTo,)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        public List<ParchoonBilty> SearchBiltyDropDownVR(int? Vehicle, int? Receiver)
        {
            return context.ParchoonBilties.Where(x => x.VehicleTypeID == Vehicle && x.ReceiverID == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownVO(int? Vehicle, int? OwnCompany)
        {
            return context.ParchoonBilties.Where(x => x.VehicleTypeID == Vehicle && x.OwnCompanyID == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownVS(int? Vehicle, int? Sender)
        {
            return context.ParchoonBilties.Where(x => x.VehicleTypeID == Vehicle && x.PickLocationID == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownVBR(int? Vehicle, int? BillTo, int? Reciever)
        {
            return context.ParchoonBilties.Where(x => x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Reciever && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownVBRO(int? Vehicle, int? BillTo, int? Reciever, int? OwnCompany)
        {
            return context.ParchoonBilties.Where(x => x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Reciever && x.OwnCompanyID == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownVBROS(int? Vehicle, int? BillTo, int? Reciever, int? OwnCompany, int? Sender)
        {
            return context.ParchoonBilties.Where(x => x.VehicleTypeID == Vehicle && x.CustomerCompanyID == BillTo && x.ReceiverID == Reciever && x.OwnCompanyID == OwnCompany && x.PickLocationID == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownBR(int? BillTo, int? Receiver)
        {
            return context.ParchoonBilties.Where(x => x.CustomerCompanyID == BillTo && x.ReceiverID == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownBO(int? BillTo, int? OwnCompany)
        {
            return context.ParchoonBilties.Where(x => x.CustomerCompanyID == BillTo && x.OwnCompanyID == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownBS(int? BillTo, int? Sender)
        {
            return context.ParchoonBilties.Where(x => x.CustomerCompanyID == BillTo && x.PickLocationID == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownRO(int? Receiver, int? OwnCompany)
        {
            return context.ParchoonBilties.Where(x => x.ReceiverID == Receiver && x.OwnCompanyID == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> SearchBiltyDropDownRS(int? Receiver, int? Sender)
        {
            return context.ParchoonBilties.Where(x => x.ReceiverID == Receiver && x.PickLocationID == Sender && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }


        public List<ParchoonBilty> getSearchParchoonBilty(DateTime DateFrom, DateTime DateTo)
        {
            return context.ParchoonBilties
                .Where(x => x.Date >= DateFrom && x.Date <= DateTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ParchoonBilty> getSearchParchoonBilty(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.ParchoonBilties
                               .Where(x => x.Date >= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.ParchoonBilties
                    .Where(x => x.Date <= Date && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

        }

        public List<ParchoonBiltyChallan> getChallan()
        {
            return context.ParchoonBiltyChallans.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.ChallanNo).ToList();
        }
        public List<ParchoonBill> BillNo()
        {
            return context.ParchoonBills.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyID)).OrderByDescending(x => x.BillNo).ToList();
        }
        public Bilty getChallanPickLocation(long Id)
        {
            return context.Bilties
                .Where(x => x.PickLocation == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
        }
        public Bilty getChallanDropLocation(long Id)
        {
            return context.Bilties
                .Where(x => x.Reciever == Id && lstAssignedCompanies.Contains(x.OwnCompanyID)).FirstOrDefault();
        }
    }
}