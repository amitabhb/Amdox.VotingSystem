using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class TrackVoteDetailsVM:ITrackVoteDetailsVM
    {
        public List<ITrackVoteStatisticsVM> VoteDetails { get; set; }

        public int selectedVoteId { get; set; }
    }
}
