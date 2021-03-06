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
    
    public partial class BiltyDetail
    {
        public long ID { get; set; }
        public Nullable<long> BiltyID { get; set; }
        public Nullable<long> ProductBrokerID { get; set; }
        public Nullable<long> ProductTypeId { get; set; }
        public Nullable<long> ProductId { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> QTY { get; set; }
        public Nullable<double> TotalWeight { get; set; }
        public Nullable<double> Freight { get; set; }
        public string Remarks { get; set; }
        public Nullable<double> AdditionalFreight { get; set; }
        public Nullable<double> Weighbridge { get; set; }
        public Nullable<double> Labour { get; set; }
    
        public virtual Bilty Bilty { get; set; }
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductBroker ProductBroker { get; set; }
    }
}
