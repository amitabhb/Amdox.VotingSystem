using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVoteResultChartVM
    {
        string VoteTitle { get; set; }
        string ChartName { get; set; }
        int VoteNumber { get; set; }
        string Tooltip { get; set; }
        string xValues { get; set; }
        string yValues { get; set; }
        List<IResolutionStatisticsVM> ResolutionStatistics { get; set; }
        List<IQuestionVM> Resolutions { get; set; }
        #region PollMaintenance table
        int? TotalVotesExpected { get; set; }
        int? ActualVotesSubmitted { get; set; }
        int? TotalDidNotVote { get; set; }

        int? PollType { get; set; }

        int? TotalWeightedVotesExpected { get; set; }
        int? ActualWeightedVotesSubmitted { get; set; }
        int? TotalWeightedDidNotVote { get; set; }
        decimal TotalDidNotVotePercentage { get; set; }
        decimal? TotalWeightedDidNotVotePercentage { get; set; }

        int Favour { get; set; }

        int Against { get; set; }

        int Abstain { get; set; }

        int NotVoted { get; set; }

        string ResolutionDescription { get; set; }

        #endregion


    }
}
