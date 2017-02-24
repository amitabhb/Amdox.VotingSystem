using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IResolutionStatisticsVM
    {
        #region PollStatics table
        int PollStatisticsID { get; set; }
        int PollId { get; set; }
        string QuestionDescription { get; set; }
        int? OptionID { get; set; }
        int TotalWeighedVotes { get; set; }
        int? QuestionID { get; set; }
        int? TotalVotes { get; set; }
        decimal? TotalWeighedVotesPercentage { get; set; }
        decimal? TotalVotesPercentage { get; set; }
        #endregion
    }
}
