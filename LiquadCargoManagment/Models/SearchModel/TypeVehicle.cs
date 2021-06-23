using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class TypeVehicle
    {
        private readonly LCMEntities context;
        public TypeVehicle(LCMEntities _context)
        {
            context = _context;
        }
        public List<VehicleType> getSearchVehicleType(DateTime DateFrom, DateTime DateTo)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> getSearchVehicleType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.VehicleTypes.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else
            {
                return context.VehicleTypes.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
        }

        public List<VehicleType> SearchVehicleTypeDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateName(string Name, string Code)
        {
            return context.VehicleTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateToName(DateTime DateTo, string Name)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateToCode(DateTime DateTo, string Code)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateFromName(DateTime DateFrom, string Name)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeDateFromCode(DateTime DateFrom, string Code)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<VehicleType> SearchVehicleTypeAllFilters(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.VehicleTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

       

    }
}