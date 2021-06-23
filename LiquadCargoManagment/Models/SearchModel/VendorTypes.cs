using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class VendorTypes
    {
        private readonly LCMEntities context;
        public VendorTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<VendorType> getSearchVendor(DateTime DateFrom, DateTime DateTo)
        {
            return context.VendorTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VendorType> getSearchVendor(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.VendorTypes.Where(x => x.CreatedDate >= Date  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else
            {
                return context.VendorTypes.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
        }
        public List<VendorType> SearchVendorName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.VendorTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        
        public List<VendorType> SearchDateFromName(DateTime DateFrom, string Name)            
        {
            return context.VendorTypes.Where(x => x.CreatedDate >= DateFrom &&  x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VendorType> SearchCodeName(string Code, string Name)
        {
            return context.VendorTypes.Where(x => x.Code == Code && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VendorType> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.VendorTypes.Where(x => x.CreatedDate >= DateFrom && x.Code == x.Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VendorType> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.VendorTypes.Where(x => x.CreatedDate >= DateTo && x.Code == x.Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VendorType> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.VendorTypes.Where(x => x.CreatedDate >= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VendorType> SearchVendorCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.VendorTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

        public List<VendorType> SearchVendorAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.VendorTypes.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

        //Multiple Selected Search
        //public List<Bilty> SearchBiltyDateVehicleBillTo(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo)
        //{
        //    return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDateVehicleBillToReceiver(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver)
        //{
        //    return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDateVehicleBillToReceiverOwnCompany(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyAllFilter(DateTime DateFrom, DateTime DateTo, DateTime DeliveryDate, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, int? OrderNo)
        //{
        //    return context.Bilties.Where(x => x.OrderDate == DateFrom && x.OrderDate == DateTo && x.DeliveryDate == DeliveryDate && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.OrderNo == OrderNo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bank> SearchBankDropDownVB(int? Bank)
        //{
        //    return context.BankAccounts.Where(x => x.BankID == Bank && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}


        //public List<Bilty> SearchBiltyDropDownVBD(int? Vehicle, int? BillTo,)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownVR(int? Vehicle, int? Receiver)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownVO(int? Vehicle, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownVBR(int? Vehicle, int? BillTo, int? Reciever)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Reciever && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        public List<VendorType> SearchVendorByName(string Name)
        {
            return context.VendorTypes.Where(x => x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VendorType> SearchVendorByCode(string Code)
        {
            return context.VendorTypes.Where(x => x.Code == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
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