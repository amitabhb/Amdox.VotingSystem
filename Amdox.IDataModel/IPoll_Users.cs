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
    
    public interface IPoll_Users
    {
        int PollUserID { get; set; }
        Nullable<int> UserID { get; set; }
        Nullable<int> VotingTypeID { get; set; }
        Nullable<int> PollId { get; set; }
        Nullable<int> PartnerTypeID { get; set; }
        Nullable<bool> IsCompleted { get; set; }
        Nullable<bool> IsProxySet { get; set; }

    }
}
