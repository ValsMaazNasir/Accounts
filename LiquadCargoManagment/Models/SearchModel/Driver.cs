using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Drivers
    {
        private readonly LCMEntities context;
        public Drivers(LCMEntities _context)
        {
            context = _context;
        }
        public List<Driver> getSearchDriver(DateTime DateFrom, DateTime DateTo)
        {
            return context.Drivers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> getSearchDriver(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Drivers.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Drivers.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<Driver> SearchDriverName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Drivers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDriverCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Drivers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Drivers.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Drivers.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Drivers.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Drivers.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchContactEmail(int? ContactNo, string NIC)
        {
            return context.Drivers.Where(x => x.CellNo == ContactNo && x.NIC == NIC && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<Driver> SearchDriverAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code, int? ContactNo, string NIC , string LicenseNo)
        {
            return context.Drivers.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.CellNo == ContactNo && x.NIC == NIC && x.LicenseNo == LicenseNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<Driver> SearchDriverDateNameCodeContact(DateTime DateFrom, DateTime DateTo, string Name, string Code, int? ContactNo , string NIC)
        {
            return context.Drivers.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.CellNo == ContactNo && x.NIC == NIC && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDriverDateNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code, int? ContactNo)
        {
            return context.Drivers.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.CellNo == ContactNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDriverDateName(DateTime DateFrom, DateTime DateTo, string Name , string Code)
        {
            return context.Drivers.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDriverDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Drivers.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        
        public List<Driver> SearchDriverNameCodeContactNICLicense(string Name, string Code, int? ContactNo, string NIC, string LicenseNo)
        {
            return context.Drivers.Where(x => x.Name == Name && x.Code == Code && x.CellNo == ContactNo && x.NIC == NIC && x.LicenseNo == LicenseNo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDriverNameCodeContactNIC(string Name, string Code, int? ContactNo, string NIC)
        {
            return context.Drivers.Where(x => x.Name == Name && x.Code == Code && x.CellNo == ContactNo && x.NIC == NIC  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDriverNameCodeContact(string Name, string Code, int? ContactNo)
        {
            return context.Drivers.Where(x => x.Name == Name && x.Code == Code && x.CellNo == ContactNo  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Driver> SearchDriverNameCode(string Name, string Code)
        {
            return context.Drivers.Where(x => x.Name == Name && x.Code == Code  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
       

    }
}