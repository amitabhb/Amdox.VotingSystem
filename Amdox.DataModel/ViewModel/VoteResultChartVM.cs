using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class VoteResultChartVM : IVoteResultChartVM
    {
        public string VoteTitle { get; set; }

        public string ChartName { get; set; }

        public int VoteNumber { get; set; }

        public string Tooltip { get; set; }

        public string xValues { get; set; }

        public string yValues { get; set; }

        public List<IResolutionStatisticsVM> ResolutionStatistics { get; set; }
        public List<IQuestionVM> Resolutions { get; set; }
        public int? TotalVotesExpected { get; set; }
        public int? ActualVotesSubmitted { get; set; }
        public int? TotalDidNotVote { get; set; }

        public int? PollType { get; set; }

        public int? TotalWeightedVotesExpected { get; set; }
        public int? ActualWeightedVotesSubmitted { get; set; }
        public int? TotalWeightedDidNotVote { get; set; }
        public decimal TotalDidNotVotePercentage { get; set; }
        public decimal? TotalWeightedDidNotVotePercentage { get; set; }

        public int Favour { get; set; }

        public int Against { get; set; }

        public int Abstain { get; set; }

        public int NotVoted { get; set; }

        public string ResolutionDescription { get; set; }
    }
}
