//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Amdox.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Poll_Transactions : IPoll_Transactions
    {
        [Key]
        public int PTID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public Nullable<int> OptionID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ProxyUserID { get; set; }
        public Nullable<System.DateTime> VotingDate { get; set; }
        public bool? isDeleted { get; set; }
        public Nullable<int> PollID { get; set; }
        
    }
}
