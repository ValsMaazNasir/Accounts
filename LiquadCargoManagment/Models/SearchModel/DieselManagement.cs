using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class DieselManagement
    {
        private readonly LCMEntities context;
        public DieselManagement(LCMEntities _context)
        {
            context = _context;
        }
        public List<Diesel> getSearchDiesel(DateTime DateFrom, DateTime DateTo)
        {
            return context.Diesels.Where(x => x.DieselDate >= DateFrom && x.DieselDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Diesel> getSearchDiesel(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Diesels.Where(x => x.DieselDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Diesels.Where(x => x.DieselDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<Diesel> SearchDieselDateVehicle(DateTime DateFrom, DateTime DateTo, int? Vehicle)
        {
            return context.Diesels.Where(x => x.DieselDate >= DateFrom && x.DieselDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID) && x.DieselExpenses.Where(s => s.RegNo == Vehicle).ToList().Count > 0).ToList();
        }
        

        //Multiple Selected Search
        public List<Diesel> SearchBiltyDateVehiclePump(DateTime DateFrom, DateTime DateTo, int? Vehicle, string PetrolPump)
        {
            return context.Diesels.Where(x => x.DieselDate >= DateFrom && x.DieselDate <= DateTo && x.PetrolPump == PetrolPump && lstAssignedCompanies.Contains(x.OwnCompanyID) && x.DieselExpenses.Where(s => s.RegNo == Vehicle).ToList().Count > 0).ToList();
        }
        public List<Bilty> SearchBiltyDateVehicleBillToReceiver(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDateVehicleBillToReceiverOwnCompany(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Diesel> SearchDieselAllFilter(DateTime DateFrom, DateTime DateTo, string PetrolPump, int? Vehicle, int? DieselRate, int? OilRate)
        {
            return context.Diesels.Where(x => x.DieselDate == DateFrom && x.DieselDate == DateTo && x.PetrolPump == PetrolPump && x.DieselRate == DieselRate && x.OilRate == OilRate && lstAssignedCompanies.Contains(x.OwnCompanyID) && x.DieselExpenses.Where(s => s.RegNo == Vehicle).ToList().Count > 0).ToList();
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
        public List<Bilty> SearchBiltyDropDownVBR(int? Vehicle, int? BillTo, int? Reciever)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Reciever && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownVBRO(int? Vehicle, int? BillTo, int? Reciever, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Reciever && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownBR(int? BillTo, int? Receiver)
        {
            return context.Bilties.Where(x => x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownBO(int? BillTo, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.BillTo == BillTo && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Bilty> SearchBiltyDropDownRO(int? Receiver, int? OwnCompany)
        {
            return context.Bilties.Where(x => x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
    }
}