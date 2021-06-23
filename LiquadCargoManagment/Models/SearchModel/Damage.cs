
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Damages
    {
        private readonly LCMEntities context;
        public Damages(LCMEntities _context)
        {
            context = _context;
        }
        public List<Damage> getSearchDamage(DateTime DateFrom, DateTime DateTo)
        {
            return context.Damages.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> getSearchDamage(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Damages.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Damages.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<Damage> SearchDamageName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Damages.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> SearchDamageCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Damages.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Damages.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Damages.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Damages.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Damages.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> SearchNameCode(string Name, string Code)
        {
            return context.Damages.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Damage> SearchDamageAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Damages.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }


    }
}