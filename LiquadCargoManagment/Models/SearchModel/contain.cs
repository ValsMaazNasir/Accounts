using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class contain
    {
        private readonly LCMEntities context;
        public contain(LCMEntities _context)
        {
            context = _context;
        }
        public List<Container> getSearchContainer(DateTime DateFrom, DateTime DateTo)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> getSearchContainer(DateTime Date, string type)
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
        public List<Container> SearchContainerNameCode(string Name, string Code)
        {
            return context.Containers.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.Containers.Where(x => x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateToName(DateTime DateTo, string Name)
        {
            return context.Containers.Where(x => x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateToCode(DateTime DateTo, string Code)
        {
            return context.Containers.Where(x => x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateFromName(DateTime DateFrom, string Name)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateFromToNameCode( DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Containers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerAllFilters(int? ContainerType, DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Containers.Where(x => x.ContainerTypeID == ContainerType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateFromDateToName(int? ContainerType, DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Containers.Where(x => x.ContainerTypeID == ContainerType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Container> SearchContainerDateFromDateTo(int? ContainerType, DateTime DateFrom, DateTime DateTo)
        {
            return context.Containers.Where(x => x.ContainerTypeID == ContainerType && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}