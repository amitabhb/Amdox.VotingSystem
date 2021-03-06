//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Poll_Maintenance = new HashSet<Poll_Maintenance>();
            this.Poll_Maintenance1 = new HashSet<Poll_Maintenance>();
            this.Poll_Maintenance2 = new HashSet<Poll_Maintenance>();
            this.Poll_ProxyUser = new HashSet<Poll_ProxyUser>();
            this.Poll_ProxyUser1 = new HashSet<Poll_ProxyUser>();
            this.Poll_ProxyUser2 = new HashSet<Poll_ProxyUser>();
            this.Poll_Transactions = new HashSet<Poll_Transactions>();
            this.Poll_Transactions1 = new HashSet<Poll_Transactions>();
            this.Poll_Users = new HashSet<Poll_Users>();
            this.UserDetails = new HashSet<UserDetail>();
            this.UserDetails1 = new HashSet<UserDetail>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Initial { get; set; }
        public string Department { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        public virtual ICollection<Poll_Maintenance> Poll_Maintenance { get; set; }
        public virtual ICollection<Poll_Maintenance> Poll_Maintenance1 { get; set; }
        public virtual ICollection<Poll_Maintenance> Poll_Maintenance2 { get; set; }
        public virtual ICollection<Poll_ProxyUser> Poll_ProxyUser { get; set; }
        public virtual ICollection<Poll_ProxyUser> Poll_ProxyUser1 { get; set; }
        public virtual ICollection<Poll_ProxyUser> Poll_ProxyUser2 { get; set; }
        public virtual ICollection<Poll_Transactions> Poll_Transactions { get; set; }
        public virtual ICollection<Poll_Transactions> Poll_Transactions1 { get; set; }
        public virtual ICollection<Poll_Users> Poll_Users { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserDetail> UserDetails1 { get; set; }
    }
}
