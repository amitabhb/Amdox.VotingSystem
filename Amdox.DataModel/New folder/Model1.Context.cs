﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRS.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Voting_System : DbContext
    {
        public Voting_System()
            : base("name=Voting_System")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Partner_Types> Partner_Types { get; set; }
        public virtual DbSet<Poll_Maintenance> Poll_Maintenance { get; set; }
        public virtual DbSet<Poll_Options> Poll_Options { get; set; }
        public virtual DbSet<Poll_ProxyUser> Poll_ProxyUser { get; set; }
        public virtual DbSet<Poll_Question_Options> Poll_Question_Options { get; set; }
        public virtual DbSet<Poll_Questions> Poll_Questions { get; set; }
        public virtual DbSet<Poll_Results> Poll_Results { get; set; }
        public virtual DbSet<Poll_Statistics> Poll_Statistics { get; set; }
        public virtual DbSet<Poll_Status> Poll_Status { get; set; }
        public virtual DbSet<Poll_Transactions> Poll_Transactions { get; set; }
        public virtual DbSet<Poll_Users> Poll_Users { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User_Types> User_Types { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voting_types> Voting_types { get; set; }
    }
}
