using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class VehicleStandard
    {
        private readonly LCMEntities context;
        public VehicleStandard(LCMEntities _context)
        {
            context = _context;
        }
        public List<StandardVehicle> getSearchVehicleStandard(DateTime DateFrom, DateTime DateTo)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> getSearchVehicleStandard(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.StandardVehicles.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.StandardVehicles.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<StandardVehicle> SearchVehicleStandardName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> SearchVehicleStandardCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> SearchNameCode(string Name, string Code)
        {
            return context.StandardVehicles.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<StandardVehicle> SearchVehicleStandardAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.StandardVehicles.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

     
    }
}