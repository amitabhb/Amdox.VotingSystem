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
    
    public partial class Partner_Types
    {
        public Partner_Types()
        {
            this.Poll_Users = new HashSet<Poll_Users>();
            this.UserDetails = new HashSet<UserDetail>();
        }
    
        public int PTID { get; set; }
        public string PartnerTypeDescription { get; set; }
        public Nullable<int> WeightedVote { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        public virtual ICollection<Poll_Users> Poll_Users { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}