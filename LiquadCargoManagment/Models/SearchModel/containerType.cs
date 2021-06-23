using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class containerType
    {
        private readonly LCMEntities context;
        public containerType(LCMEntities _context)
        {
            context = _context;
        }
        public List<ContainerType> getSearchContainerType(DateTime DateFrom, DateTime DateTo)
        {
            return context.ContainerTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> getSearchContainerType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.ContainerTypes.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.ContainerTypes.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }

        public List<ContainerType> SearchContainerTypeDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.ContainerTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.ContainerTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.ContainerTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeNameCode(string Name, string Code)
        {
            return context.ContainerTypes.Where(x => x.ContainerTypeName == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.ContainerTypes.Where(x => x.DateCreated >= DateFrom && x.ContainerTypeName == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.ContainerTypes.Where(x => x.DateCreated <= DateTo && x.ContainerTypeName == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeDateToName(DateTime DateTo, string Name)
        {
            return context.ContainerTypes.Where(x => x.DateCreated <= DateTo && x.ContainerTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeDateToCode(DateTime DateTo, string Code)
        {
            return context.ContainerTypes.Where(x => x.DateCreated <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeDateFromName(DateTime DateFrom, string Name)
        {
            return context.ContainerTypes.Where(x => x.DateCreated >= DateFrom && x.ContainerTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeDateFromCode(DateTime DateFrom, string Code)
        {
            return context.ContainerTypes.Where(x => x.DateCreated >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<ContainerType> SearchContainerTypeAllFilters(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.ContainerTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.ContainerTypeName == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}