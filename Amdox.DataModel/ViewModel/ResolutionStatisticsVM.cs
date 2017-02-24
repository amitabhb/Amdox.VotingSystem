using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class ResolutionStatisticsVM : IResolutionStatisticsVM
    {
        public int PollStatisticsID { get; set; }

        public int PollId { get; set; }

        public int? OptionID { get; set; }

        public int TotalWeighedVotes { get; set; }
        public int? QuestionID { get; set; }
        public string QuestionDescription { get; set; }

        public int? TotalVotes { get; set; }

        public decimal? TotalWeighedVotesPercentage { get; set; }
        public decimal? TotalVotesPercentage { get; set; }
    }
}
