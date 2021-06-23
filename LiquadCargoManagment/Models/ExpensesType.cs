//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiquadCargoManagment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExpensesType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExpensesType()
        {
            this.Expenses = new HashSet<Expense>();
            this.SaleOrderExpenses = new HashSet<SaleOrderExpens>();
            this.SalesOrderChildExpenses = new HashSet<SalesOrderChildExpens>();
            this.VehicleExpenses = new HashSet<VehicleExpens>();
            this.ParchoonBiltyExpenses = new HashSet<ParchoonBiltyExpense>();
        }
    
        public long ExpensesTypeID { get; set; }
        public string ExpensesTypeName { get; set; }
        public string ExpensesTypeCode { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedByUserID { get; set; }
        public Nullable<long> ModifiedByUserID { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> OwnCompanyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual OwnCompany OwnCompany { get; set; }
        public virtual OwnCompany OwnCompany1 { get; set; }
        public virtual OwnCompany OwnCompany2 { get; set; }
        public virtual OwnCompany OwnCompany3 { get; set; }
        public virtual OwnCompany OwnCompany4 { get; set; }
        public virtual OwnCompany OwnCompany5 { get; set; }
        public virtual OwnCompany OwnCompany6 { get; set; }
        public virtual OwnCompany OwnCompany7 { get; set; }
        public virtual OwnCompany OwnCompany8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderExpens> SaleOrderExpenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrderChildExpens> SalesOrderChildExpenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleExpens> VehicleExpenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParchoonBiltyExpense> ParchoonBiltyExpenses { get; set; }
    }
}
