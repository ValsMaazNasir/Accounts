using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Employees
    {
        private readonly LCMEntities context;
        public Employees(LCMEntities _context)
        {
            context = _context;
        }
        public List<Employee> getSearchEmployee(DateTime DateFrom, DateTime DateTo)
        {
            return context.Employees.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> getSearchEmployee(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Employees.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Employees.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
       
        public List<Employee> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Employees.Where(x => x.CreatedDate >= DateFrom && x.EmployeeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Employees.Where(x => x.CreatedDate >= DateTo && x.EmployeeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Employees.Where(x => x.CreatedDate >= DateFrom && x.EmployeeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Employees.Where(x => x.CreatedDate >= DateTo && x.EmployeeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchNameCode(string Name, string Code)
        {
            return context.Employees.Where(x => x.EmployeeName == Name && x.EmployeeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code, string FatherName, string Gender, string CNIC, string Contact, string OtherContact)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && x.CNIC == CNIC && x.ContactNo == Contact && x.OtherContactNo == OtherContact && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeDateNameCodeGenderNICContact(DateTime DateFrom, DateTime DateTo, string Name, string Code, string FatherName, string Gender, string CNIC, string Contact)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && x.CNIC == CNIC && x.ContactNo == Contact  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeDateNameCodeGenderNIC(DateTime DateFrom, DateTime DateTo, string Name, string Code, string FatherName, string Gender, string CNIC)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && x.CNIC == CNIC && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeDateNameCodeGender(DateTime DateFrom, DateTime DateTo, string Name, string Code, string FatherName, string Gender)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeDateNameFatherName(DateTime DateFrom, DateTime DateTo, string Name, string Code, string FatherName)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeDateNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeName == Name && x.EmployeeCode == Code  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeDateName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeName == Name  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeDateCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Employees.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.EmployeeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeNameCodeFatherGenderCNICContactOtherContact( string Name, string Code, string FatherName, string Gender, string CNIC, string Contact, string OtherContact)
        {
            return context.Employees.Where(x => x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && x.CNIC == CNIC && x.ContactNo == Contact && x.OtherContactNo == OtherContact && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeNameCodeFatherGenderCNICContact(string Name, string Code, string FatherName, string Gender, string CNIC, string Contact)
        {
            return context.Employees.Where(x => x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && x.CNIC == CNIC && x.ContactNo == Contact && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeNameCodeFatherGenderCNIC(string Name, string Code, string FatherName, string Gender, string CNIC)
        {
            return context.Employees.Where(x => x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && x.CNIC == CNIC  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeNameCodeFatherGender(string Name, string Code, string FatherName, string Gender)
        {
            return context.Employees.Where(x => x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName && x.Gender == Gender && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeNameCodeFather(string Name, string Code, string FatherName)
        {
            return context.Employees.Where(x => x.EmployeeName == Name && x.EmployeeCode == Code && x.FatherName == FatherName  && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Employee> SearchEmployeeNameCode(string Name, string Code)
        {
            return context.Employees.Where(x => x.EmployeeName == Name && x.EmployeeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
       
    }
}