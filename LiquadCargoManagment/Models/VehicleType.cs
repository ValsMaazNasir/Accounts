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
    
    public partial class VehicleType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleType()
        {
            this.UniversalBiltyVehicleInformations = new HashSet<UniversalBiltyVehicleInformation>();
            this.UniversalVehicleDetails = new HashSet<UniversalVehicleDetail>();
            this.Vehicles = new HashSet<Vehicle>();
            this.VehicleDocuments = new HashSet<VehicleDocument>();
            this.VehicleMaintenances = new HashSet<VehicleMaintenance>();
            this.ParchoonBilties = new HashSet<ParchoonBilty>();
        }
    
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Description { get; set; }
        public Nullable<long> OwnCompanyId { get; set; }
    
        public virtual OwnCompany OwnCompany { get; set; }
        public virtual OwnCompany OwnCompany1 { get; set; }
        public virtual OwnCompany OwnCompany2 { get; set; }
        public virtual OwnCompany OwnCompany3 { get; set; }
        public virtual OwnCompany OwnCompany4 { get; set; }
        public virtual OwnCompany OwnCompany5 { get; set; }
        public virtual OwnCompany OwnCompany6 { get; set; }
        public virtual OwnCompany OwnCompany7 { get; set; }
        public virtual OwnCompany OwnCompany8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyVehicleInformation> UniversalBiltyVehicleInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalVehicleDetail> UniversalVehicleDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleDocument> VehicleDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleMaintenance> VehicleMaintenances { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParchoonBilty> ParchoonBilties { get; set; }
    }
}