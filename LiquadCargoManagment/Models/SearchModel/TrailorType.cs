using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class TrailorType
    {
        private readonly LCMEntities context;
        public TrailorType(LCMEntities _context)
        {
            context = _context;
        }
        public List<TrailerType> getSearchTrailorType(DateTime DateFrom, DateTime DateTo)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> getSearchTrailorType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.TrailerTypes.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.TrailerTypes.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }

        public List<TrailerType> SearchTrailorTypeDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateName(string Name, string Code)
        {
            return context.TrailerTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateToName(DateTime DateTo, string Name)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateToCode(DateTime DateTo, string Code)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateFromName(DateTime DateFrom, string Name)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeDateFromCode(DateTime DateFrom, string Code)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TrailerType> SearchTrailorTypeAllFilters(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.TrailerTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}