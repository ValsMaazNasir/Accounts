using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class RevenueTypes
    {
        private readonly LCMEntities context;
        public RevenueTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<RevenueType> getSearchRevenueType(DateTime DateFrom, DateTime DateTo)
        {
            return context.RevenueTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> getSearchRevenueType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.RevenueTypes.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else
            {
                return context.RevenueTypes.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
        }
        public List<RevenueType> SearchRevenueTypeName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.RevenueTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> SearchRevenueTypeCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.RevenueTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.RevenueTypes.Where(x => x.DateCreated >= DateFrom && x. Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.RevenueTypes.Where(x => x.DateCreated >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.RevenueTypes.Where(x => x.DateCreated >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.RevenueTypes.Where(x => x.DateCreated >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> SearchNameCode(string Name, string Code)
        {
            return context.RevenueTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<RevenueType> SearchRevenueTypeAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.RevenueTypes.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

      
    }
}