using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Trailor
    {
        private readonly LCMEntities context;
        public Trailor(LCMEntities _context)
        {
            context = _context;
        }
        public List<Trailer> getSearchTrailor(DateTime DateFrom, DateTime DateTo)
        {
            return context.Trailers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> getSearchTrailor(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Trailers.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Trailers.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }

        public List<Trailer> SearchTrailorDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Trailers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Trailers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateName(string Name, string Code)
        {
            return context.Trailers.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.Trailers.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.Trailers.Where(x => x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateToName(DateTime DateTo, string Name)
        {
            return context.Trailers.Where(x => x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateToCode(DateTime DateTo, string Code)
        {
            return context.Trailers.Where(x => x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateFromName(DateTime DateFrom, string Name)
        {
            return context.Trailers.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Trailers.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Trailer> SearchTrailorAllFilters(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Trailers.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}