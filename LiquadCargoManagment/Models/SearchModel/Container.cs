using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Containers
    {
        private readonly LCMEntities context;
        public Containers(LCMEntities _context)
        {
            context = _context;
        }
        public List<Container> getSearchContainerType(DateTime DateFrom, DateTime DateTo)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> getSearchContainerType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Containers.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Containers.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }

        }
        public List<Container> SearchContainerDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerAllFilter(int? ContainerType,DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Containers.Where(x => x.ContainerTypeID == ContainerType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerNameCode(string Name, string Code)
        {
            return context.Containers.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
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