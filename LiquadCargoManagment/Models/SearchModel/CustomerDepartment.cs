using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class CustomerDepartments
    {
        private readonly LCMEntities context;
        public CustomerDepartments(LCMEntities _context)
        {
            context = _context;
        }
        public List<CustomerGroup> getSearchCustomerDepartment(DateTime DateFrom, DateTime DateTo)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<CustomerGroup> getSearchCustomerDepartment(DateTime Date, string type)
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

        public List<CustomerDepartment> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<CustomerDepartment> SearchCustomerDepartmentAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code, string Email, string Contact)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.Name == Name && x.Code == Code && x.EmailAdd == Email && x.Contact == Contact && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchCustomerDepartmentDateNameCodeEmail(DateTime DateFrom, DateTime DateTo, string Name, string Code, string Email)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.Name == Name && x.Code == Code && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchCustomerDepartmentDateNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchCustomerDepartmentDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.CustomerDepartments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchCustomerDepartmentNameCodeEmailContact(string Name, string Code, string Email, string Contact)
        {
            return context.CustomerDepartments.Where(x => x.Name == Name && x.Code == Code && x.EmailAdd == Email && x.Contact == Contact && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchCustomerDepartmentNameCodeEmail(string Name, string Code, string Email)
        {
            return context.CustomerDepartments.Where(x => x.Name == Name && x.Code == Code && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<CustomerDepartment> SearchCustomerDepartmentNameCode(string Name, string Code)
        {
            return context.CustomerDepartments.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }


        public List<CustomerGroup> SearchCustomerGroupDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.CustomerGroups.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }


    }
}