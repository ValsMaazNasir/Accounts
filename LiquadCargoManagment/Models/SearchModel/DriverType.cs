using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class DriverTypes
    {
        private readonly LCMEntities context;
        public DriverTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<DriverType> getSearchDriverType(DateTime DateFrom, DateTime DateTo)
        {
            return context.DriverTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> getSearchDriverType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.DriverTypes.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.DriverTypes.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<DriverType> SearchDriverTypeName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.DriverTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> SearchDriverTypeCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.DriverTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.DriverTypes.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.DriverTypes.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.DriverTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.DriverTypes.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> SearchNameCode(string Name, string Code)
        {
            return context.DriverTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DriverType> SearchDriverTypeAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.DriverTypes.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

      
      
    }
}