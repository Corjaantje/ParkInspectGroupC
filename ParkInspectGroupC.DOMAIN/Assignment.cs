
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
    
public partial class Assignment
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Assignment()
    {

        this.Inspections = new HashSet<Inspection>();

    }


    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ManagerId { get; set; }

    public string Description { get; set; }

    public Nullable<System.DateTime> StartDate { get; set; }

    public Nullable<System.DateTime> EndDate { get; set; }

    public System.DateTime DateCreated { get; set; }

    public System.DateTime DateUpdated { get; set; }



    public virtual Customer Customer { get; set; }

    public virtual Employee Employee { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Inspection> Inspections { get; set; }

}

}
