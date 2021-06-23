using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class GeneralTax
    {
        private readonly LCMEntities context;
        public GeneralTax(LCMEntities _context)
        {
            context = _context;
        }
        public List<TaxRateRegistration> getSearchGeneralTax(DateTime DateFrom, DateTime DateTo)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> getSearchGeneralTax(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.TaxRateRegistrations.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.TaxRateRegistrations.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<TaxRateRegistration> SearchPackageName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.TaxRateName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchPackageCode(DateTime DateFrom, DateTime DateTo, string TaxRate)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.TaxRatePercent == TaxRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchDateFromCode(DateTime DateFrom, string TaxRate)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate >= DateFrom && x.TaxRateName == TaxRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchDateToCode(DateTime DateTo, string TaxRate)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate >= DateTo && x.TaxRatePercent == TaxRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate >= DateFrom && x.TaxRateName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate >= DateTo && x.TaxRateName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchNameCode(string Name, string TaxRate)
        {
            return context.TaxRateRegistrations.Where(x => x.TaxRateName == Name && x.TaxRatePercent == TaxRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchGeneralTaxAllFilter(int? ProvinceID,DateTime DateFrom, DateTime DateTo, string Name, string TaxRate)
        {
            return context.TaxRateRegistrations.Where(x => x.ProvinceID == ProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.TaxRateName == Name && x.TaxRatePercent == TaxRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchGeneralTaxProvinceIDDateFromToNameTaxRate(int? ProvinceID, DateTime DateFrom, DateTime DateTo, string Name, string TaxRate)
        {
            return context.TaxRateRegistrations.Where(x => x.ProvinceID == ProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.TaxRateName == Name && x.TaxRatePercent == TaxRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchGeneralTaxProvinceIDDateFromToName(int? ProvinceID, DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TaxRateRegistrations.Where(x => x.ProvinceID == ProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.TaxRateName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchGeneralTaxProvinceIDDateFromTo(int? ProvinceID, DateTime DateFrom, DateTime DateTo)
        {
            return context.TaxRateRegistrations.Where(x => x.ProvinceID == ProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchGeneralTaxDateFromDateToNameTaxRate( DateTime DateFrom, DateTime DateTo, string Name, string TaxRate)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.TaxRateName == Name && x.TaxRatePercent == TaxRate && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateRegistration> SearchGeneralTaxDateFromDateToName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TaxRateRegistrations.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.TaxRateName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
      
    }
}