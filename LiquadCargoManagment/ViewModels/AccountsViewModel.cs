using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.ViewModels
{
    public class AccountsViewModel
    {
        public List<account> accounts { get; set; }
        public List<accounts_transaction> accounts_transactions { get; set; }
        public string account_title { get; set; }
        public int account_id { get; set; }
        public decimal accBal { get; set; }
    }
}