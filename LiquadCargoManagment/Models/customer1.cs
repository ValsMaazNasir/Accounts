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
    
    public partial class customer1
    {
        public int customers_id { get; set; }
        public string customers_code { get; set; }
        public string customers_firstname { get; set; }
        public string customers_lastname { get; set; }
        public string customers_address { get; set; }
        public string customers_tel { get; set; }
        public string customers_area { get; set; }
        public string customers_city { get; set; }
        public string customers_country { get; set; }
        public bool isActive { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<int> customerTypeId { get; set; }
    }
}
