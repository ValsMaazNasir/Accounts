using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class OwnDepartment
    {
        private readonly LCMEntities context;
        public OwnDepartment(LCMEntities _context)
        {
            context = _context;
        }
        public List<Department> getSearchOwnDepartment(DateTime DateFrom, DateTime DateTo)
        {
            return context.Departments.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> getSearchOwnDepartment(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Departments.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Departments.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<Category> SearchProductName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Categories.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Category> SearchProductCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Categories.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Department> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Departments.Where(x => x.DateCreated >= DateFrom && x.DepartCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Departments.Where(x => x.DateCreated >= DateTo && x.DepartCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Departments.Where(x => x.DateCreated >= DateFrom && x.DepartName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Departments.Where(x => x.DateCreated >= DateTo && x.DepartName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchContactEmail(string Contact, string Email)
        {
            return context.Departments.Where(x => x.Contact == Contact && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<Department> SearchOwnDepartmentAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code, string Contact,string Email)
        {
            return context.Departments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.DepartName == Name && x.DepartCode == Code  && x.Contact ==  Contact && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<Department> SearchOwnDepartmentDateNameCodeContact(DateTime DateFrom, DateTime DateTo, string Name, string Code, string Contact)
        {
            return context.Departments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.DepartName == Name && x.DepartCode == Code && x.Contact == Contact  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchOwnDepartmentDateNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code )
        {
            return context.Departments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.DepartName == Name && x.DepartCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchOwnDepartmentDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Departments.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.DepartName == Name  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchDepartmentNameCodeContactEmail(string Name, string Code, string Contact, string Email)
        {
            return context.Departments.Where(x => x.DepartName == Name && x.DepartCode == Code && x.Contact == Contact && x.EmailAdd == Email && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchDepartmentNameCodeContact(string Name, string Code, string Contact)
        {
            return context.Departments.Where(x => x.DepartName == Name && x.DepartCode == Code && x.Contact == Contact  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Department> SearchDepartmentNameCode(string Name, string Code)
        {
            return context.Departments.Where(x => x.DepartName == Name && x.DepartCode == Code  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        
    }
}