using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Documents
    {
        private readonly LCMEntities context;
        public Documents(LCMEntities _context)
        {
            context = _context;
        }
        public List<Document> getSearchDocument(DateTime DateFrom, DateTime DateTo)
        {
            return context.Documents.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> getSearchDocument(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Documents.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Documents.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }

        public List<Document> SearchDocumentDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Documents.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Documents.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentNameCode(string Name, string Code)
        {
            return context.Documents.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.Documents.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.Documents.Where(x => x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentDateToName(DateTime DateTo, string Name)
        {
            return context.Documents.Where(x => x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentDateToCode(DateTime DateTo, string Code)
        {
            return context.Documents.Where(x => x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentDateFromName(DateTime DateFrom, string Name)
        {
            return context.Documents.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Documents.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Document> SearchDocumentAllFilters(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Documents.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}