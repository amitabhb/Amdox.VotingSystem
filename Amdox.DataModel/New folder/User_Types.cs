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
    
    public partial class User_Types
    {
        public User_Types()
        {
            this.UserDetails = new HashSet<UserDetail>();
        }
    
        public int UserTypeID { get; set; }
        public string UserTypeDescription { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
