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
    
    public interface IPoll_ProxyUser
    {
        int PollProxyUserID { get; set; }
        Nullable<int> UserId { get; set; }
        Nullable<int> ProxyUserId { get; set; }
        Nullable<System.DateTime> StartDate { get; set; }
        Nullable<System.DateTime> EndDate { get; set; }
        Nullable<bool> IsEnabled { get; set; }
        Nullable<System.DateTime> CreatedDate { get; set; }
        Nullable<int> CreatedUser { get; set; }

    }
}
