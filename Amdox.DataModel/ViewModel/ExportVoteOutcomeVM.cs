using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class ExportVoteOutcomeVM : IExportVoteOutcomeVM
    {
        public int voteNumber { get; set; }
        public string voteTitle { get; set; }
        public string voteDescription { get; set; }
        public List<IQuestionStatisticsVM> resolutions { get; set; }

        public string resolutionDescription { get; set; }
        public string voteType { get; set; }

        public decimal NoVotePercentage { get; set; }

        public string isAccepted { get; set; }
        public decimal VoteForSimplePercentage { get; set; }
        public decimal VoteAgainstSimplePercentage { get; set; }
        public decimal AbstainSimplePercentage { get; set; }

        public int VoteForSimpleCount { get; set; }
        public int VoteAgainstSimpleCount { get; set; }
        public int AbstainSimpleCount { get; set; }

        public decimal VoteForWeightedPercentage { get; set; }
        public decimal VoteAgainstWeightedPercentage { get; set; }
        public decimal AbstainWeightedPercentage { get; set; }

        public int VoteForWeightedCount { get; set; }
        public int VoteAgainstWeightedCount { get; set; }
        public int AbstainWeightedCount { get; set; }
        public int NoVoteSimpleCount { get; set; }
        public decimal NoVoteSimplePercentage { get; set; }
        public int NoVoteWeightedCount { get; set; }
        public decimal NoVoteWeightedPercentage { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public string Outcome { get; set; }
    }
}
