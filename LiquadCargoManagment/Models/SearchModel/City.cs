using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Citys
    {
        private readonly LCMEntities context;
        public Citys(LCMEntities _context)
        {
            context = _context;
        }
        public List<City> getSearchCity(DateTime DateFrom, DateTime DateTo)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> getSearchCity(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Cities.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Cities.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<City> SearchCityNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo  && x.CityName == Name && x.CityCode == Code  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchCityIDName(DateTime DateFrom, DateTime DateTo, int? ProvinceID, string Name)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.ProvinceID == ProvinceID && x.CityName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchCityID(DateTime DateFrom, DateTime DateTo, int? ProvinceID)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.ProvinceID == ProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchCityIDNameCode(int? ProvinceID, string Name, string Code)
        {
            return context.Cities.Where(x => x.ProvinceID == ProvinceID && x.CityName == Name && x.CityCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchCityIDName(int? ProvinceID, string Name)
        {
            return context.Cities.Where(x => x.ProvinceID == ProvinceID && x.CityName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchCityName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.CityName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchCityCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.CityCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.CityCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Cities.Where(x => x.DateCreated >= DateTo && x.CityCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Cities.Where(x => x.DateCreated >= DateFrom && x.CityName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Cities.Where(x => x.DateCreated >= DateTo && x.CityName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchNameCode(string Name, string Code)
        {
            return context.Cities.Where(x => x.CityName == Name && x.CityCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<City> SearchCityAllFilter(DateTime DateFrom, DateTime DateTo,int? ProvinceID, string Name, string Code)
        {
            return context.Cities.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo &&  x.ProvinceID == ProvinceID && x.CityName == Name && x.CityCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }


    }
}