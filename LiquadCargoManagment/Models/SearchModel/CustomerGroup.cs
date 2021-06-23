using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class CustomerGroups
    {
        private readonly LCMEntities context;
        public CustomerGroups(LCMEntities _context)
        {
            context = _context;
        }
        public List<CustomerGroup> getSearchEmployee(DateTime DateFrom, DateTime DateTo)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> getSearchEmployee(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.CustomerGroups.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else
            {
                return context.CustomerGroups.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
        }

        public List<CustomerGroup> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        
        public List<CustomerGroup> SearchCustomerGroupAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code, string Email,  string Contact)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.EmailAdd == Email && x.Contact == Contact && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchCustomerGroupDateNameCodeEmail(DateTime DateFrom, DateTime DateTo, string Name, string Code, string Email)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.EmailAdd == Email  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchCustomerGroupDateNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchCustomerGroupDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchCustomerGroupNameCodeEmailContact( string Name, string Code, string Email, string Contact)
        {
            return context.CustomerGroups.Where(x => x.Name == Name && x.Code == Code && x.EmailAdd == Email && x.Contact == Contact && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchCustomerGroupNameCodeEmail( string Name, string Code, string Email)
        {
            return context.CustomerGroups.Where(x => x.Name == Name && x.Code == Code && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> SearchCustomerGroupNameCode( string Name, string Code)
        {
            return context.CustomerGroups.Where(x =>  x.Name == Name && x.Code == Code  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
       
       
        public List<CustomerGroup> SearchCustomerGroupDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
      

    }
}