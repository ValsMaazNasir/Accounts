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
    
    public partial class SalesOrderChildDieselExpens
    {
        public long ID { get; set; }
        public Nullable<long> OwnCompanyId { get; set; }
        public Nullable<long> SaleOrderChildID { get; set; }
        public Nullable<long> VendorID { get; set; }
        public Nullable<int> Liter { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<double> Amount { get; set; }
    
        public virtual SaleOrderChild SaleOrderChild { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}