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
    
    public partial class UniversalBiltyVehicleLoadingReporting
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> LoadingDate { get; set; }
        public Nullable<System.TimeSpan> LoadingTime { get; set; }
        public Nullable<System.DateTime> LoadingGateInDate { get; set; }
        public Nullable<System.TimeSpan> LoadingGateInTime { get; set; }
        public Nullable<System.DateTime> LoadingGateOutDate { get; set; }
        public Nullable<System.TimeSpan> LoadingGateOutTime { get; set; }
        public string LoadingTotalDays { get; set; }
        public string LoadingTotalTime { get; set; }
        public Nullable<long> UniversalBiltyID { get; set; }
    
        public virtual UniversalBilty UniversalBilty { get; set; }
    }
}