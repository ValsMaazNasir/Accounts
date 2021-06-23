using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class BankAc
    {
        private readonly LCMEntities context;
        public BankAc(LCMEntities _context)
        {
            context = _context;
        }
        public List<BankAccount> getSearchBankAccount(DateTime DateFrom, DateTime DateTo)
        {
            return context.BankAccounts.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> getSearchBankAccount(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.BankAccounts.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
            else
            {
                return context.BankAccounts.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
            }
        }
        public List<BankAccount> SearchBankAccountName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.BankAccounts.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == x.Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchBankAccountCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.BankAccounts.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.BankAccounts.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.BankAccounts.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.BankAccounts.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.BankAccounts.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchNameCode(string Name, string Code)
        {
            return context.BankAccounts.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<BankAccount> SearchBankAcDateFromDateToNameCodeAccountTitleAccountNo( DateTime DateFrom, DateTime DateTo, string Name, string Code, string AccountTitle, string AccountNo)
        {
            return context.BankAccounts.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.AccountTitle == AccountTitle && x.AccountNo == AccountNo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchBankAcDateFromDateToNameCodeAccountTitle(DateTime DateFrom, DateTime DateTo, string Name, string Code, string AccountTitle)
        {
            return context.BankAccounts.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code  && x.AccountTitle == AccountTitle && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

        public List<BankAccount> SearchBankAcDateFromDateToNameCode(DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.BankAccounts.Where(x => x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code  && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }



        public List<BankAccount> SearchBankAcAllFilter(int? Bank,DateTime DateFrom, DateTime DateTo, string Name, string Code, string AccountTitle, string AccountNo)
        {
            return context.BankAccounts.Where(x => x.BankID == Bank && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.AccountTitle == AccountTitle && x.AccountNo == AccountNo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchBankDateFromToNameCodeAccountTitle(int? Bank, DateTime DateFrom, DateTime DateTo, string Name, string Code, string AccountTitle)
        {
            return context.BankAccounts.Where(x => x.BankID == Bank && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.AccountTitle == AccountTitle && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchBankDateFromToNameCode(int? Bank, DateTime DateFrom, DateTime DateTo, string Name, string Code)
        {
            return context.BankAccounts.Where(x => x.BankID == Bank && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.Code == Code && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchBankDateFromToName(int? Bank, DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.BankAccounts.Where(x => x.BankID == Bank && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.Name == Name && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchBankDateFromToName(int? Bank, DateTime DateFrom, DateTime DateTo)
        {
            return context.BankAccounts.Where(x => x.BankID == Bank && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }
        public List<BankAccount> SearchBankDateFrom(int? Bank, DateTime DateFrom)
        {
            return context.BankAccounts.Where(x => x.BankID == Bank && x.CreatedDate == DateFrom  && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        }

    }
}