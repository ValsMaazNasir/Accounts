﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiquadCargoManagment.Areas.Accounts.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AccountingEntities : DbContext
    {
        public AccountingEntities()
            : base("name=AccountingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountsHead> AccountsHeads { get; set; }
        public virtual DbSet<AccountsLedger> AccountsLedgers { get; set; }
        public virtual DbSet<ARInvoice> ARInvoices { get; set; }
        public virtual DbSet<ARInvoiceDetail> ARInvoiceDetails { get; set; }
        public virtual DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public virtual DbSet<GLType> GLTypes { get; set; }
        public virtual DbSet<MenuPermission> MenuPermissions { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<SubAccount> SubAccounts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CustomerLedger> CustomerLedgers { get; set; }
        public virtual DbSet<VendorLedger> VendorLedgers { get; set; }
    }
}