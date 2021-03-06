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
    
    public partial class FillingStation
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OwnerContactName { get; set; }
        public string OwnerContactNo { get; set; }
        public string PrimaryContactPerson { get; set; }
        public string PrimaryContactNo { get; set; }
        public string SecContactPerson { get; set; }
        public string SecContactNo { get; set; }
        public string Address { get; set; }
        public string PTCLNo { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> OwnCompanyID { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNo { get; set; }
    
        public virtual OwnCompany OwnCompany { get; set; }
    }
}
