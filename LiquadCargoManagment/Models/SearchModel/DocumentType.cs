using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class DocumentTypes
    {
        private readonly LCMEntities context;
        public DocumentTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<DocumentType> getSearchDocumentType(DateTime DateFrom, DateTime DateTo)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> getSearchDocumentType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.DocumentTypes.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.DocumentTypes.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }

        public List<DocumentType> SearchDocumentTypeDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeNameCode(string Name, string Code)
        {
            return context.DocumentTypes.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateFromCodeName(DateTime DateFrom, string Name, string Code)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateToNameCode(DateTime DateTo, string Name, string Code)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateToName(DateTime DateTo, string Name)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateToCode(DateTime DateTo, string Code)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateFromName(DateTime DateFrom, string Name)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeDateFromCode(DateTime DateFrom, string Code)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<DocumentType> SearchDocumentTypeAllFilters(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.DocumentTypes.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}