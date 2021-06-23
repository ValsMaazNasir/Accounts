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
    
    public partial class UniversalBiltyCustomerInformation
    {
        public long ID { get; set; }
        public Nullable<long> ConsignerSenderCustomerCode { get; set; }
        public Nullable<long> ConsignerReceiverCustomerCode { get; set; }
        public string ClientBillTo { get; set; }
        public Nullable<long> ClientCode { get; set; }
        public string BillingType { get; set; }
        public Nullable<long> ShipmentType { get; set; }
        public Nullable<long> UniversalBiltyID { get; set; }
    
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual CustomerDepartment CustomerDepartment1 { get; set; }
        public virtual CustomerDepartment CustomerDepartment2 { get; set; }
        public virtual ShipmentType ShipmentType1 { get; set; }
        public virtual UniversalBilty UniversalBilty { get; set; }
    }
}
