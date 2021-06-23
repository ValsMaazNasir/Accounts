using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class ShipmentTypes
    {
        private readonly LCMEntities context;
        public ShipmentTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<ShipmentType> getSearchPackageType(DateTime DateFrom, DateTime DateTo)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> getSearchPackageType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.ShipmentTypes.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.ShipmentTypes.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<ShipmentType> SearchShipmentName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> SearchShipmenCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> SearchNameCode(string Name, string Code)
        {
            return context.ShipmentTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ShipmentType> SearchShipmentAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.ShipmentTypes.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        
    }
}