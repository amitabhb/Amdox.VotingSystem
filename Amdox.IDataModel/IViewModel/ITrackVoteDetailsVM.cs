using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface ITrackVoteDetailsVM
    {
        List<ITrackVoteStatisticsVM> VoteDetails { get; set; }

        int selectedVoteId { get; set; }
    }
}
