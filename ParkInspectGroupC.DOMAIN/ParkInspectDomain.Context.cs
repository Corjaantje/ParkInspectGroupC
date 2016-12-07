﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ParkInspectEntities : DbContext
    {
        public ParkInspectEntities()
            : base("name=ParkInspectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Availability> Availabilities { get; set; }
        public virtual DbSet<Coordinate> Coordinates { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeStatu> EmployeeStatus { get; set; }
        public virtual DbSet<Inspection> Inspections { get; set; }
        public virtual DbSet<InspectionImage> InspectionImages { get; set; }
        public virtual DbSet<InspectionStatu> InspectionStatus { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<KeywordCategory> KeywordCategories { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionKeyword> QuestionKeywords { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireModule> QuestionnaireModules { get; set; }
        public virtual DbSet<QuestionSort> QuestionSorts { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<WorkingHour> WorkingHours { get; set; }
        public virtual DbSet<ReportSection> ReportSections { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Diagram> Diagrams { get; set; }
    }
}
