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
    
    public partial class CustomerGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerGroup()
        {
            this.CustomerCompanies = new HashSet<CustomerCompany>();
        }
    
        public long GroupID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerCompany> CustomerCompanies { get; set; }
        public virtual OwnCompany OwnCompany { get; set; }
        public virtual OwnCompany OwnCompany1 { get; set; }
        public virtual OwnCompany OwnCompany2 { get; set; }
        public virtual OwnCompany OwnCompany3 { get; set; }
        public virtual OwnCompany OwnCompany4 { get; set; }
        public virtual OwnCompany OwnCompany5 { get; set; }
        public virtual OwnCompany OwnCompany6 { get; set; }
        public virtual OwnCompany OwnCompany7 { get; set; }
        public virtual OwnCompany OwnCompany8 { get; set; }
    }
}
