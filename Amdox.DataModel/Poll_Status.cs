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

    public partial class Poll_Status : IPoll_Status
    {
        [Key]   
        public int StatusID { get; set; }
        public string StatusDescription { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
    }
}
