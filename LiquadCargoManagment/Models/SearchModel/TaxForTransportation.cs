using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class TaxForTransportation
    {
        private readonly LCMEntities context;
        public TaxForTransportation(LCMEntities _context)
        {
            context = _context;
        }
        public List<TaxRateTransportation> getSearchTaxForTransportation(DateTime DateFrom, DateTime DateTo)
        {
            return context.TaxRateTransportations.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo).ToList();
        }
        public List<TaxRateTransportation> getSearchTaxForTransportation(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.TaxRateTransportations.Where(x => x.CreatedDate >= Date).ToList();
            }
            else
            {
                return context.TaxRateTransportations.Where(x => x.CreatedDate <= Date).ToList();
            }
        }
        public List<TaxRateTransportation> SearchTaxForTransportationPercentName(string TaxRatePercentDestination, string TaxRatePercentOrigin, string Name)
        {
            return context.TaxRateTransportations.Where(x => x.TaxRatePercentDestination == TaxRatePercentDestination && x.TaxRatePercentOrigin == TaxRatePercentOrigin && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxForTransportationRatePercent(string TaxRatePercentDestination, string TaxRatePercentOrigin)
        {
            return context.TaxRateTransportations.Where(x => x.TaxRatePercentDestination == TaxRatePercentDestination && x.TaxRatePercentOrigin == TaxRatePercentOrigin && lstAssignedCompanies.Contains(x.OwnCompanyID) ).ToList();
        }
        public List<TaxRateTransportation> SearchTaxForTransportationID(int? OriginProvinceID, int? DestinationProvinceID)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.DestinationProvince == DestinationProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxForTransportationID(DateTime DateFrom, DateTime DateTo, int? OriginProvinceID)
        {
            return context.TaxRateTransportations.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.OriginProvince == OriginProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxForTransportationDestinationID(DateTime DateFrom, DateTime DateTo, int? DestinationProvinceID)
        {
            return context.TaxRateTransportations.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.DestinationProvince == DestinationProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxForTransportationName(int? OriginProvinceID, string Name)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID) && x.Name == Name).ToList();
        }
        public List<TaxRateTransportation> SearchTaxForTransportationDestinationName(int? DestinationProvinceID, string Name)
        {
            return context.TaxRateTransportations.Where(x => x.DestinationProvince == DestinationProvinceID && lstAssignedCompanies.Contains(x.OwnCompanyID) && x.Name == Name).ToList();
        }
      
        public List<TaxRateTransportation> SearchTaxTransportationAllFilter(int? OriginProvinceID,int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin, string TaxRatePercentDestination)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin && x.TaxRatePercentDestination == TaxRatePercentDestination  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationOriginIDDateFromDateToNameTaxPercentOrigin(int? OriginProvinceID, int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationOriginIDDateFromDateToName(int? OriginProvinceID, int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationOriginIDDateFromDateTo(int? OriginProvinceID, int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationIDDateFromDateToNameTaxPercentOrigin(int? OriginProvinceID, DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin, string TaxRatePercentDestination)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID  && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin && x.TaxRatePercentDestination == TaxRatePercentDestination && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationIDDateFromDateToNameTaxPercent(int? OriginProvinceID, DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationIDDateFromDateToName(int? OriginProvinceID, DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationIDDateFromDateTo(int? OriginProvinceID, DateTime DateFrom, DateTime DateTo)
        {
            return context.TaxRateTransportations.Where(x => x.OriginProvince == OriginProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationDestinationIDDateFromDateToNameTaxPercentOrigin( int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin, string TaxRatePercentDestination)
        {
            return context.TaxRateTransportations.Where(x => x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin && x.TaxRatePercentDestination == TaxRatePercentDestination && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationDestinationIDDateFromDateToNameTaxPercent(int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin)
        {
            return context.TaxRateTransportations.Where(x => x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationDestinationIDDateFromDateToName(int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TaxRateTransportations.Where(x => x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationDestinationIDDateFromDateTo(int? DestinationProvinceID, DateTime DateFrom, DateTime DateTo)
        {
            return context.TaxRateTransportations.Where(x => x.DestinationProvince == DestinationProvinceID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationDateFromDateToNameTaxPercentOrigin( DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin, string TaxRatePercentDestination)
        {
            return context.TaxRateTransportations.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin && x.TaxRatePercentDestination == TaxRatePercentDestination && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationDateFromDateToNameTaxPercent(DateTime DateFrom, DateTime DateTo, string Name, string TaxRatePercentOrigin)
        {
            return context.TaxRateTransportations.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.TaxRatePercentOrigin == TaxRatePercentOrigin  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<TaxRateTransportation> SearchTaxTransportationDateFromDateToName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.TaxRateTransportations.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
     
     

    }
}