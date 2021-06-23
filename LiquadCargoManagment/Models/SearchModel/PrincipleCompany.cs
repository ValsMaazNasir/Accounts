using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class PrincipleCompanys
    {
        private readonly LCMEntities context;
        public PrincipleCompanys(LCMEntities _context)
        {
            context = _context;
        }
        public List<PrincipleCompany> getSearchPrincipleType(DateTime DateFrom, DateTime DateTo)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> getSearchPrincipleType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.PrincipleCompanies.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.PrincipleCompanies.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<PrincipleCompany> SearchPrincipleName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> SearchPrincipleCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate >= DateFrom && x. Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> SearchNameCode(string Name, string Code)
        {
            return context.PrincipleCompanies.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<PrincipleCompany> SearchPrincipleAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.PrincipleCompanies.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

       
    }
}