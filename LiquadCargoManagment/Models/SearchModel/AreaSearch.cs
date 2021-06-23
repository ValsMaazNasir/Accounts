using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class AreaSearch
    {
        private readonly LCMEntities context;
        public AreaSearch(LCMEntities _context)
        {
            context = _context;
        }
        public List<Area> getSearchArea(DateTime DateFrom, DateTime DateTo)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> getSearchArea(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Areas.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else
            {
                return context.Areas.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
        }
        public List<Area> SearchAreaNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchAreaIDName(DateTime DateFrom, DateTime DateTo, int? CityID, string Name)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.CityID == CityID && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchAreaID(DateTime DateFrom, DateTime DateTo, int? CityID)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.CityID == CityID && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchAreaIDNameCode(int? CityID, string Name, string Code)
        {
            return context.Areas.Where(x => x.CityID == CityID && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchAreaIDName(int? CityID, string Name)
        {
            return context.Areas.Where(x => x.CityID == CityID && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchAreaName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchAreaCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Areas.Where(x => x.DateCreated >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Areas.Where(x => x.DateCreated >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Areas.Where(x => x.DateCreated >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchNameCode(string Name, string Code)
        {
            return context.Areas.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Area> SearchAreaAllFilter(DateTime DateFrom, DateTime DateTo, int? CityID, string Name, string Code)
        {
            return context.Areas.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.CityID == CityID && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }


    }
}