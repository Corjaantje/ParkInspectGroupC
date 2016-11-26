
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
    
public partial class Question
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Question()
    {

        this.QuestionAnswers = new HashSet<QuestionAnswer>();

        this.QuestionKeywords = new HashSet<QuestionKeyword>();

    }


    public int Id { get; set; }

    public int SortId { get; set; }

    public string Description { get; set; }

    public Nullable<int> ModuleId { get; set; }

    public System.DateTime DateCreated { get; set; }

    public System.DateTime DateUpdated { get; set; }



    public virtual Module Module { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<QuestionKeyword> QuestionKeywords { get; set; }

    public virtual QuestionSort QuestionSort { get; set; }

}

}
