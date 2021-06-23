using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class CustomerCompanys
    {
        private readonly LCMEntities context;
        public CustomerCompanys(LCMEntities _context)
        {
            context = _context;
        }
        public List<CustomerCompany> getSearchEmployee(DateTime DateFrom, DateTime DateTo)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> getSearchEmployee(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.CustomerCompanies.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else
            {
                return context.CustomerCompanies.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
        }

        public List<CustomerCompany> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

        public List<CustomerCompany> SearchCustomerGroupAllFilter(DateTime DateFrom, DateTime DateTo, int? GroupID,string Name, string Code, string Email, string Contact)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.GroupID == GroupID && x.Name == Name && x.Code == Code && x.EmailAdd == Email && x.Contact == Contact && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupIDDateNameCodeEmail(DateTime DateFrom, DateTime DateTo,int? GroupID, string Name, string Code, string Email)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.GroupID == GroupID && x.Name == Name && x.Code == Code && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupIDDateNameCode(DateTime DateFrom, DateTime DateTo,int? GroupID,string Name, string Code)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.GroupID == GroupID && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupIDDateName(DateTime DateFrom, DateTime DateTo, int? GroupID,string Name)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.GroupID == GroupID &&  x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupID(DateTime DateFrom, DateTime DateTo, int? GroupID)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.GroupID == GroupID  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupNameCodeEmailContact(string Name, string Code, string Email, string Contact)
        {
            return context.CustomerCompanies.Where(x => x.Name == Name && x.Code == Code && x.EmailAdd == Email && x.Contact == Contact && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupNameCodeEmail(string Name, string Code, string Email)
        {
            return context.CustomerCompanies.Where(x => x.Name == Name && x.Code == Code && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupNameCode(string Name, string Code)
        {
            return context.CustomerCompanies.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

        public List<CustomerCompany> SearchCustomerGroupIDNameCodeEmailContact(int? GroupID,string Name, string Code, string Email, string Contact)
        {
            return context.CustomerCompanies.Where(x => x.GroupID == GroupID &&x.Name == Name && x.Code == Code && x.EmailAdd == Email && x.Contact == Contact && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupIDNameCodeEmail(int? GroupID,string Name, string Code, string Email)
        {
            return context.CustomerCompanies.Where(x => x.GroupID == GroupID &&  x.Name == Name && x.Code == Code && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupIDNameCode(int? GroupID,string Name, string Code)
        {
            return context.CustomerCompanies.Where(x => x.GroupID == GroupID &&  x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerCompany> SearchCustomerGroupIDNameCode(int? GroupID, string Name)
        {
            return context.CustomerCompanies.Where(x => x.GroupID == GroupID && x.Name == Name  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

        public List<CustomerCompany> SearchCustomerGroupDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.CustomerCompanies.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }


    }
}