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
    
    public partial class CustomerCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerCompany()
        {
            this.Banks = new HashSet<Bank>();
            this.Bilties = new HashSet<Bilty>();
            this.Bilties1 = new HashSet<Bilty>();
            this.CustomerDepartments = new HashSet<CustomerDepartment>();
            this.CustomerProfileLMS = new HashSet<CustomerProfileLM>();
            this.CustomerProfiles = new HashSet<CustomerProfile>();
            this.PartialBilties = new HashSet<PartialBilty>();
            this.PartialBilties1 = new HashSet<PartialBilty>();
            this.SaleOrders = new HashSet<SaleOrder>();
            this.SaleOrderChilds = new HashSet<SaleOrderChild>();
            this.ParchoonBilties = new HashSet<ParchoonBilty>();
        }
    
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<long> GroupID { get; set; }
        public string Contact { get; set; }
        public string ContactOther { get; set; }
        public string EmailAdd { get; set; }
        public string WebAdd { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> OwnCompanyId { get; set; }
        public string NTN { get; set; }
        public string STN { get; set; }
        public Nullable<double> Tax { get; set; }
        public string CustomerType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank> Banks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bilty> Bilties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bilty> Bilties1 { get; set; }
        public virtual OwnCompany OwnCompany { get; set; }
        public virtual OwnCompany OwnCompany1 { get; set; }
        public virtual OwnCompany OwnCompany2 { get; set; }
        public virtual OwnCompany OwnCompany3 { get; set; }
        public virtual OwnCompany OwnCompany4 { get; set; }
        public virtual OwnCompany OwnCompany5 { get; set; }
        public virtual OwnCompany OwnCompany6 { get; set; }
        public virtual OwnCompany OwnCompany7 { get; set; }
        public virtual OwnCompany OwnCompany8 { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDepartment> CustomerDepartments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerProfileLM> CustomerProfileLMS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerProfile> CustomerProfiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartialBilty> PartialBilties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartialBilty> PartialBilties1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderChild> SaleOrderChilds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParchoonBilty> ParchoonBilties { get; set; }
    }
}
