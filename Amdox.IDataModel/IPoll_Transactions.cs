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
    
    public interface IPoll_Transactions
    {
        int PTID { get; set; }
        Nullable<int> QuestionID { get; set; }
        Nullable<int> OptionID { get; set; }
        Nullable<int> UserID { get; set; }
        Nullable<int> ProxyUserID { get; set; }
        Nullable<System.DateTime> VotingDate { get; set; }
        Nullable<bool> isDeleted { get; set; }
        Nullable<int> PollID { get; set; }

    }
}
