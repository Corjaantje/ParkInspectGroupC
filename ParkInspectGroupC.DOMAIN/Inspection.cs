//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParkInspectGroupC.DOMAIN
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inspection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inspection()
        {
            this.Coordinates = new HashSet<Coordinate>();
            this.InspectionImages = new HashSet<InspectionImage>();
            this.Questionnaires = new HashSet<Questionnaire>();
        }
    
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int RegionId { get; set; }
        public string Location { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public Nullable<int> InspectorId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
    
        public virtual Assignment Assignment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Coordinate> Coordinates { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual InspectionStatu InspectionStatu { get; set; }
        public virtual Region Region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InspectionImage> InspectionImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
    }
}
