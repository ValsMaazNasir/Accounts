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
    
    public partial class UniversalBilty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UniversalBilty()
        {
            this.LocalDropUniversalBilties = new HashSet<LocalDropUniversalBilty>();
            this.LocalPickUniversalBilties = new HashSet<LocalPickUniversalBilty>();
            this.UniversalBiltyContainerInformations = new HashSet<UniversalBiltyContainerInformation>();
            this.UniversalBiltyCustomerInformations = new HashSet<UniversalBiltyCustomerInformation>();
            this.UniversalBiltyDamageDetails = new HashSet<UniversalBiltyDamageDetail>();
            this.UniversalBiltyDispatchDocuments = new HashSet<UniversalBiltyDispatchDocument>();
            this.UniversalBiltyExpenseDetails = new HashSet<UniversalBiltyExpenseDetail>();
            this.UniversalBiltyFreightDetails = new HashSet<UniversalBiltyFreightDetail>();
            this.UniversalBiltyLocationInformations = new HashSet<UniversalBiltyLocationInformation>();
            this.UniversalBiltyProductInformations = new HashSet<UniversalBiltyProductInformation>();
            this.UniversalBiltyReceivingDocumentDetails = new HashSet<UniversalBiltyReceivingDocumentDetail>();
            this.UniversalBiltyReceivingInformations = new HashSet<UniversalBiltyReceivingInformation>();
            this.UniversalBiltyVehicleInformations = new HashSet<UniversalBiltyVehicleInformation>();
            this.UniversalBiltyVehicleLoadingReportings = new HashSet<UniversalBiltyVehicleLoadingReporting>();
            this.UniversalBiltyVehicleUnLoadingReportings = new HashSet<UniversalBiltyVehicleUnLoadingReporting>();
            this.UniversalCheckBills = new HashSet<UniversalCheckBill>();
            this.UniversalCheckBilties = new HashSet<UniversalCheckBilty>();
            this.UniversalContainerDetails = new HashSet<UniversalContainerDetail>();
            this.UniversalVehicleDetails = new HashSet<UniversalVehicleDetail>();
        }
    
        public long ID { get; set; }
        public Nullable<long> AutoBiltyNo { get; set; }
        public string ManualBiltyNo { get; set; }
        public Nullable<System.DateTime> BiltyDate { get; set; }
        public Nullable<long> CompanyID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<double> ContainerQty { get; set; }
        public Nullable<double> ContainerWeight { get; set; }
        public Nullable<double> ContainerTotalWeight { get; set; }
        public Nullable<double> ProductQty { get; set; }
        public Nullable<double> ProductWeight { get; set; }
        public Nullable<double> ProductTotalWeight { get; set; }
        public Nullable<long> OwnCompanyID { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<double> TotalAdditionalFreight { get; set; }
        public Nullable<double> TotalLabour { get; set; }
        public Nullable<double> TotalWeighbridge { get; set; }
        public Nullable<double> TotalFreight { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalDropUniversalBilty> LocalDropUniversalBilties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalPickUniversalBilty> LocalPickUniversalBilties { get; set; }
        public virtual OwnCompany OwnCompany { get; set; }
        public virtual OwnCompany OwnCompany1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyContainerInformation> UniversalBiltyContainerInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyCustomerInformation> UniversalBiltyCustomerInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyDamageDetail> UniversalBiltyDamageDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyDispatchDocument> UniversalBiltyDispatchDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyExpenseDetail> UniversalBiltyExpenseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyFreightDetail> UniversalBiltyFreightDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyLocationInformation> UniversalBiltyLocationInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyProductInformation> UniversalBiltyProductInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyReceivingDocumentDetail> UniversalBiltyReceivingDocumentDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyReceivingInformation> UniversalBiltyReceivingInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyVehicleInformation> UniversalBiltyVehicleInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyVehicleLoadingReporting> UniversalBiltyVehicleLoadingReportings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalBiltyVehicleUnLoadingReporting> UniversalBiltyVehicleUnLoadingReportings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalCheckBill> UniversalCheckBills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalCheckBilty> UniversalCheckBilties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalContainerDetail> UniversalContainerDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversalVehicleDetail> UniversalVehicleDetails { get; set; }
    }
}
