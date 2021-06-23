
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Expenses
    {
        private readonly LCMEntities context;
        public Expenses(LCMEntities _context)
        {
            context = _context;
        }
        public List<Expense> getSearchExpense(DateTime DateFrom, DateTime DateTo)
        {
            return context.Expenses.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> getSearchExpense(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Expenses.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.Expenses.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<Expense> SearchExpenseName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.Expenses.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> SearchExpenseCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.Expenses.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.Expenses.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.Expenses.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.Expenses.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.Expenses.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> SearchNameCode(string Name, string Code)
        {
            return context.Expenses.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<Expense> SearchExpenseAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.Expenses.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }


    }
}