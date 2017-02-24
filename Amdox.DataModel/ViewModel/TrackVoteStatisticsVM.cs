using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class TrackVoteStatisticsVM : ITrackVoteStatisticsVM
    {
        public int iVoteNumber{get;set;}

        public string strVoteTitle { get; set; }

        public string strvoteStatus { get; set; }
        public DateTime dtStartDate { get; set; }

        public DateTime dteEndDate { get; set; }

        public int iVotesCastCount { get; set; }

        public decimal dVotesCastPerc { get; set; }

        public int iOutstandingCount { get; set; }
        public decimal dOutstandingPerc { get; set; }
        public List<IQuestionVM> lstResolutions { get; set; }
        public int selectedPollID { get; set; }
    }
}
