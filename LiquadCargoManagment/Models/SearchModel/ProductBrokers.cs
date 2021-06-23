using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class ProductBrokers
    {

        private readonly LCMEntities context;
        public ProductBrokers(LCMEntities _context)
        {
            context = _context;
        }
        public List<ProductBroker> getSearchProductBroker(DateTime DateFrom, DateTime DateTo)
        {
            return context.ProductBrokers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ProductBroker> getSearchProductBroker(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.ProductBrokers.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.ProductBrokers.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        
        public List<ProductBroker> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.ProductBrokers.Where(x => x.CreatedDate >= DateFrom && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ProductBroker> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.ProductBrokers.Where(x => x.CreatedDate >= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
       
        //public List<ProductBroker> SearchProductCode(DateTime DateFrom, DateTime DateTo, string Code)
        //{
        //    return context.ProductBrokers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}

        public List<ProductBroker> SearchProductBrokerAllFilter(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.ProductBrokers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
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
        //public List<ProductBroker> SearchProductByName(string Name)
        //{
        //    return context.ProductBrokers.Where(x => x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Nature> SearchProductByCode(string Code)
        //{
        //    return context.Natures.Where(x => x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
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