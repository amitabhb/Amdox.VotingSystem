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
    
    public partial class Poll_Status
    {
        public Poll_Status()
        {
            this.Poll_Maintenance = new HashSet<Poll_Maintenance>();
        }
    
        public int StatusID { get; set; }
        public string StatusDescription { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        public virtual ICollection<Poll_Maintenance> Poll_Maintenance { get; set; }
    }
}
