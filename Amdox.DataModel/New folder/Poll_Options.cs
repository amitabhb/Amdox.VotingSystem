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
    
    public partial class Poll_Options
    {
        public Poll_Options()
        {
            this.Poll_Question_Options = new HashSet<Poll_Question_Options>();
            this.Poll_Statistics = new HashSet<Poll_Statistics>();
            this.Poll_Transactions = new HashSet<Poll_Transactions>();
        }
    
        public int OptionID { get; set; }
        public string OptionName { get; set; }
        public bool IsEnabled { get; set; }
    
        public virtual ICollection<Poll_Question_Options> Poll_Question_Options { get; set; }
        public virtual ICollection<Poll_Statistics> Poll_Statistics { get; set; }
        public virtual ICollection<Poll_Transactions> Poll_Transactions { get; set; }
    }
}
