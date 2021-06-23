
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class ExpenseTypes
    {
        private readonly LCMEntities context;
        public ExpenseTypes(LCMEntities _context)
        {
            context = _context;
        }
        public List<ExpensesType> getSearchExpenseType(DateTime DateFrom, DateTime DateTo)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> getSearchExpenseType(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.ExpensesTypes.Where(x => x.DateCreated >= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
            else
            {
                return context.ExpensesTypes.Where(x => x.DateCreated <= Date && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
            }
        }
        public List<ExpensesType> SearchExpenseName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.ExpensesTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> SearchExpenseCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated >= DateFrom && x.DateCreated <= DateTo && x.ExpensesTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated >= DateFrom && x.ExpensesTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated >= DateTo && x.ExpensesTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated >= DateFrom && x.ExpensesTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated >= DateTo && x.ExpensesTypeName == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> SearchNameCode(string Name, string Code)
        {
            return context.ExpensesTypes.Where(x => x.ExpensesTypeName == Name && x.ExpensesTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<ExpensesType> SearchExpenseAllFilter(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.ExpensesTypes.Where(x => x.DateCreated == DateFrom && x.DateCreated == DateTo && x.ExpensesTypeName == Name && x.ExpensesTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }


    }
}