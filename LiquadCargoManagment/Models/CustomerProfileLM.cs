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
    
    public partial class CustomerProfileLM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerProfileLM()
        {
            this.LocalDropUniversalBilties = new HashSet<LocalDropUniversalBilty>();
            this.LocalDropUniversalBilties1 = new HashSet<LocalDropUniversalBilty>();
            this.LocalDropUniversalBilties2 = new HashSet<LocalDropUniversalBilty>();
            this.LocalPickUniversalBilties = new HashSet<LocalPickUniversalBilty>();
            this.LocalPickUniversalBilties1 = new HashSet<LocalPickUniversalBilty>();
            this.LocalPickUniversalBilties2 = new HashSet<LocalPickUniversalBilty>();
        }
    
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<long> CustomerGroupID { get; set; }
        public Nullable<long> CustomerDepartmentID { get; set; }
        public Nullable<long> CustomerCompanyID { get; set; }
        public Nullable<long> OwnCompanyID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual CustomerCompany CustomerCompany { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual OwnCompany OwnCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalDropUniversalBilty> LocalDropUniversalBilties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalDropUniversalBilty> LocalDropUniversalBilties1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalDropUniversalBilty> LocalDropUniversalBilties2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalPickUniversalBilty> LocalPickUniversalBilties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalPickUniversalBilty> LocalPickUniversalBilties1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalPickUniversalBilty> LocalPickUniversalBilties2 { get; set; }
    }
}
