using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class VoteResultsDataVM : IVoteResultsDataVM
    {
        public int voteNumber { get; set; }
        public string voteTitle { get; set; }
        public string voteDescription { get; set; }
        public List<IQuestionStatisticsVM> resolutions { get; set; }

        public string voteType { get; set; }

        public decimal NoVotePercentage { get; set; }

        public bool isAccepted { get; set; }
    }
}
