using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Revenues
    {
        private readonly LCMEntities context;
        public Revenues(LCMEntities _context)
        {
            context = _context;
        }
        public List<Revenue> getSearchRevenue(DateTime DateFrom, DateTime DateTo)
        {
            return context.Revenues.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> getSearchRevenue(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Revenues.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Revenues.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<Revenue> SearchRevenueName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Revenues.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> SearchRevenueCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Revenues.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Revenues.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Revenues.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Revenues.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Revenues.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> SearchNameCode(string Name, string Code)
        {
            return context.Revenues.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Revenue> SearchRevenueAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Revenues.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }


    }
}