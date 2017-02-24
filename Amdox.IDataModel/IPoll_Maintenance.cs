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
    
    public interface IPoll_Maintenance
    {



        int PollId { get; set; }
        string PollTitle { get; set; }
        string PollLongDescription { get; set; }
        Nullable<System.DateTime> StartDate { get; set; }
        Nullable<System.DateTime> EndDate { get; set; }
        Nullable<int> PollOwner { get; set; }        
        Nullable<int> StatusID { get; set; }
        Nullable<bool> IsVisible { get; set; }
        Nullable<bool> IsPublished { get; set; }
        Nullable<int> CreatedBy { get; set; }
        Nullable<System.DateTime> CreatedDate { get; set; }
        Nullable<System.DateTime> ModifiedDate { get; set; }
        Nullable<int> ModifiedBy { get; set; }
        Nullable<int> TotalVotesExpected { get; set; }
        Nullable<int> ActualVotesSubmitted { get; set; }
        Nullable<int> TotalDidNotVote { get; set; }
        string PollShortDescription { get; set; }
        Nullable<int> PollType { get; set; }
        Nullable<bool> IsAutoClose { get; set; }

        Nullable<int> VoteTypePercentage { get; set; }
        
        Nullable <int> TotalWeightedVotesExpected {get;set;}
            
        Nullable <int>    ActualWeightedVotesSubmitted {get;set;}

        Nullable<int> TotalWeightedDidNotVote { get; set; }

        Nullable<decimal> TotalDidNotVotePercentage { get; set; }

        Nullable<decimal> TotalWeightedDidNotVotePercentage { get; set; }
    }
}
