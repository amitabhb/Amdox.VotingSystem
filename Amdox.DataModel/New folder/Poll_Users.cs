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
    
    public partial class Poll_Users
    {
        public int PollUserID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> VotingTypeID { get; set; }
        public Nullable<int> PollId { get; set; }
        public Nullable<int> PartnerTypeID { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<bool> IsProxySet { get; set; }
    
        public virtual Partner_Types Partner_Types { get; set; }
        public virtual Poll_Maintenance Poll_Maintenance { get; set; }
        public virtual User User { get; set; }
        public virtual Voting_types Voting_types { get; set; }
    }
}
