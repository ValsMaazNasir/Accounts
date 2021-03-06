//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiquadCargoManagment.Areas.Accounts.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ARInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ARInvoice()
        {
            this.ARInvoiceDetails = new HashSet<ARInvoiceDetail>();
        }
    
        public int ID { get; set; }
        public string Code { get; set; }
        public System.DateTime PostingDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public System.DateTime DocumentDate { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public double TaxAmount { get; set; }
        public double GrandTotal { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> DeletedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ARInvoiceDetail> ARInvoiceDetails { get; set; }
    }
}
