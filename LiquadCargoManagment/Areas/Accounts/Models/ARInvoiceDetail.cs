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
    
    public partial class ARInvoiceDetail
    {
        public int ID { get; set; }
        public int ARInvoiceID { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int DiscountInPercentage { get; set; }
        public int TaxRate { get; set; }
        public double TaxAmount { get; set; }
        public double Total { get; set; }
    
        public virtual ARInvoice ARInvoice { get; set; }
    }
}
