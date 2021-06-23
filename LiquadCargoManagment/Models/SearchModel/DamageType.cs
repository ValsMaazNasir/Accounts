
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class DamageTypes
    {
        private readonly LCMEntities context;
        public DamageTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<DamageType> getSearchDamageType(DateTime DateFrom, DateTime DateTo)
        {
            return context.DamageTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> getSearchDamageType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.DamageTypes.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.DamageTypes.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<DamageType> SearchDamageTypeName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.DamageTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> SearchDamageTypeCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.DamageTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.DamageTypes.Where(x => x.DateCreated >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.DamageTypes.Where(x => x.DateCreated >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.DamageTypes.Where(x => x.DateCreated >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.DamageTypes.Where(x => x.DateCreated >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> SearchNameCode(string Name, string Code)
        {
            return context.DamageTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DamageType> SearchDamageTypeAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.DamageTypes.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }


    }
}