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

    public partial class Poll_Results : IPoll_Results
    {
        [Key]
        public int ResultID { get; set; }
        public string ResultDescription { get; set; }
        public Nullable<bool> IsEnabled { get; set; }    
     
    }
}