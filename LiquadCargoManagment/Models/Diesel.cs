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
    
    public partial class Diesel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Diesel()
        {
            this.DieselExpenses = new HashSet<DieselExpense>();
        }
    
        public long ID { get; set; }
        public Nullable<System.DateTime> DieselDate { get; set; }
        public Nullable<double> DieselRate { get; set; }
        public Nullable<double> OilRate { get; set; }
        public string PetrolPump { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> OwnCompanyID { get; set; }
        public Nullable<long> SerialNo { get; set; }
        public Nullable<double> DieselTotalLiter { get; set; }
        public Nullable<double> DieselTotalRate { get; set; }
        public Nullable<double> DieselTotalAmount { get; set; }
        public Nullable<double> OilTotalLiter { get; set; }
        public Nullable<double> OilTotalRate { get; set; }
        public Nullable<double> OilTotalAmount { get; set; }
        public Nullable<double> TotalOtherAmount { get; set; }
        public Nullable<double> TotalNetAmount { get; set; }
        public Nullable<double> TotalCashReceived { get; set; }
        public Nullable<double> TotalCashPaid { get; set; }
    
        public virtual OwnCompany OwnCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DieselExpense> DieselExpenses { get; set; }
    }
}
