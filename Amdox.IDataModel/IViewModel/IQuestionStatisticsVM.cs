using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IQuestionStatisticsVM
    {
        decimal VoteForSimplePercentage { get; set; }
        decimal VoteAgainstSimplePercentage { get; set; }
        decimal AbstainSimplePercentage { get; set; }

        int VoteForSimpleCount { get; set; }
        int VoteAgainstSimpleCount { get; set; }
        int AbstainSimpleCount { get; set; }

        decimal VoteForWeightedPercentage { get; set; }
        decimal VoteAgainstWeightedPercentage { get; set; }
        decimal AbstainWeightedPercentage { get; set; }

        int VoteForWeightedCount { get; set; }
        int VoteAgainstWeightedCount { get; set; }
        int AbstainWeightedCount { get; set; }
        int NoVoteSimpleCount { get; set; }
        decimal NoVoteSimplePercentage { get; set; }
        int NoVoteWeightedCount { get; set; }
        decimal NoVoteWeightedPercentage { get; set; }
        string Description { get; set; }
        int Sequence { get; set; }
        string Outcome { get; set; }
    }
}
