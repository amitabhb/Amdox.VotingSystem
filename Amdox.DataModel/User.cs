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
    
    public partial class User : IUser
    {
    
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Initial { get; set; }
        public string Department { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<bool> IsAllowedToVote { get; set; }
        public string Password { get; set; }
        public int? OrganisationId { get; set; }
    }
}

