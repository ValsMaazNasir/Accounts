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
    
    public partial class UniversalBiltyProductInformation
    {
        public long ID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> UniversalBiltyID { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> WeightQtyTotal { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual UniversalBilty UniversalBilty { get; set; }
    }
}
