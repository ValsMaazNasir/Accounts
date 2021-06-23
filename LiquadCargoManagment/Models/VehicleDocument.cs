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
    
    public partial class VehicleDocument
    {
        public long ID { get; set; }
        public Nullable<long> VehicleRegNo { get; set; }
        public Nullable<long> VehicleType { get; set; }
        public Nullable<long> VehicleOwnership { get; set; }
        public Nullable<long> PrincipleCompany { get; set; }
        public Nullable<int> SMSAlert { get; set; }
        public string EmailAlert { get; set; }
        public string EmailCCAlert { get; set; }
        public Nullable<long> VehicleDocumentType { get; set; }
        public Nullable<long> VehicleDocuments { get; set; }
        public Nullable<System.DateTime> RenewalDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string AlertType { get; set; }
        public Nullable<int> AlertBefore { get; set; }
        public string AlertFrequency { get; set; }
        public string Remarks { get; set; }
        public string DocumentUpload { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> OwnCompanyID { get; set; }
    
        public virtual Document Document { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual OwnCompany OwnCompany { get; set; }
        public virtual PrincipleCompany PrincipleCompany1 { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Vehicle Vehicle1 { get; set; }
        public virtual VehicleType VehicleType1 { get; set; }
    }
}