﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ChinookEntities : DbContext
    {
        public ChinookEntities()
            : base("name=ChinookEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLine { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<Track> Track { get; set; }
    
        public virtual ObjectResult<BestArtistsAlbums_Result> BestArtistsAlbums(Nullable<int> numberOfArtists, Nullable<System.DateTime> date1, Nullable<System.DateTime> date2)
        {
            var numberOfArtistsParameter = numberOfArtists.HasValue ?
                new ObjectParameter("numberOfArtists", numberOfArtists) :
                new ObjectParameter("numberOfArtists", typeof(int));
    
            var date1Parameter = date1.HasValue ?
                new ObjectParameter("date1", date1) :
                new ObjectParameter("date1", typeof(System.DateTime));
    
            var date2Parameter = date2.HasValue ?
                new ObjectParameter("date2", date2) :
                new ObjectParameter("date2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BestArtistsAlbums_Result>("BestArtistsAlbums", numberOfArtistsParameter, date1Parameter, date2Parameter);
        }
    
        public virtual ObjectResult<Customer_Employee_Result> Customer_Employee(string customerLastName, string customerFirstName, string employeeLastName, string employeeFirstName, Nullable<System.DateTime> date1, Nullable<System.DateTime> date2)
        {
            var customerLastNameParameter = customerLastName != null ?
                new ObjectParameter("CustomerLastName", customerLastName) :
                new ObjectParameter("CustomerLastName", typeof(string));
    
            var customerFirstNameParameter = customerFirstName != null ?
                new ObjectParameter("CustomerFirstName", customerFirstName) :
                new ObjectParameter("CustomerFirstName", typeof(string));
    
            var employeeLastNameParameter = employeeLastName != null ?
                new ObjectParameter("EmployeeLastName", employeeLastName) :
                new ObjectParameter("EmployeeLastName", typeof(string));
    
            var employeeFirstNameParameter = employeeFirstName != null ?
                new ObjectParameter("EmployeeFirstName", employeeFirstName) :
                new ObjectParameter("EmployeeFirstName", typeof(string));
    
            var date1Parameter = date1.HasValue ?
                new ObjectParameter("date1", date1) :
                new ObjectParameter("date1", typeof(System.DateTime));
    
            var date2Parameter = date2.HasValue ?
                new ObjectParameter("date2", date2) :
                new ObjectParameter("date2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Customer_Employee_Result>("Customer_Employee", customerLastNameParameter, customerFirstNameParameter, employeeLastNameParameter, employeeFirstNameParameter, date1Parameter, date2Parameter);
        }
    
        public virtual ObjectResult<CustomerTurnoverTry_Result> CustomerTurnoverTry(Nullable<System.DateTime> date1, Nullable<System.DateTime> date2)
        {
            var date1Parameter = date1.HasValue ?
                new ObjectParameter("date1", date1) :
                new ObjectParameter("date1", typeof(System.DateTime));
    
            var date2Parameter = date2.HasValue ?
                new ObjectParameter("date2", date2) :
                new ObjectParameter("date2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CustomerTurnoverTry_Result>("CustomerTurnoverTry", date1Parameter, date2Parameter);
        }
    
        public virtual ObjectResult<genresa3_Result> genresa3(Nullable<int> timelessGenre)
        {
            var timelessGenreParameter = timelessGenre.HasValue ?
                new ObjectParameter("timelessGenre", timelessGenre) :
                new ObjectParameter("timelessGenre", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<genresa3_Result>("genresa3", timelessGenreParameter);
        }
    
        public virtual ObjectResult<quarterSales_Result> quarterSales(Nullable<System.DateTime> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<quarterSales_Result>("quarterSales", dateParameter);
        }
    
        public virtual ObjectResult<Top10Tracks_Result> Top10Tracks(Nullable<System.DateTime> date1, Nullable<System.DateTime> date2)
        {
            var date1Parameter = date1.HasValue ?
                new ObjectParameter("date1", date1) :
                new ObjectParameter("date1", typeof(System.DateTime));
    
            var date2Parameter = date2.HasValue ?
                new ObjectParameter("date2", date2) :
                new ObjectParameter("date2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Top10Tracks_Result>("Top10Tracks", date1Parameter, date2Parameter);
        }
    
        public virtual int try1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("try1");
        }
    }
}
