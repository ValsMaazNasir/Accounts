using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class MaintenanceTypes
    {
        private readonly LCMEntities context;
        public MaintenanceTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<MaintenanceType> getSearchMaintenanceType(DateTime DateFrom, DateTime DateTo)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> getSearchMaintenanceType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.MaintenanceTypes.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.MaintenanceTypes.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }

        public List<MaintenanceType> SearchMaintenanceTypeDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeNameCode(string Name, string Code)
        {
            return context.MaintenanceTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeDateToName(DateTime DateTo, string Name)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeDateToCode(DateTime DateTo, string Code)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeDateFromName(DateTime DateFrom, string Name)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateFromCode(DateTime DateFrom, string Code)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<MaintenanceType> SearchMaintenanceTypeAllFilters(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.MaintenanceTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}