using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class PackageTypes
    {
        private readonly LCMEntities context;
        public PackageTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<PackageType> getSearchPackageType(DateTime DateFrom, DateTime DateTo)
        {
            return context.PackageTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> getSearchPackageType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.PackageTypes.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.PackageTypes.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<PackageType> SearchPackageName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.PackageTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.PackageTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> SearchPackageCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.PackageTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.PackageTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.PackageTypes.Where(x => x.DateCreated >= DateFrom && x.PackageTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.PackageTypes.Where(x => x.DateCreated >= DateTo && x.PackageTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.PackageTypes.Where(x => x.DateCreated >= DateFrom && x.PackageTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.PackageTypes.Where(x => x.DateCreated >= DateTo && x.PackageTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> SearchNameCode(string Name, string Code)
        {
            return context.PackageTypes.Where(x => x.PackageTypeName == Name && x.PackageTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PackageType> SearchPackageTypeAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.PackageTypes.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.PackageTypeName == Name && x.PackageTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
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