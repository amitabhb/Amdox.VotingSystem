using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class TrackVotingVM:ITrackVotingVM
    {

        public List<Amdox.IDataModel.IViewModel.TrackVotingObject> voteData { get; set; }
        public int UserTypeID { get; set; }
        public TrackVotingVM()
        {
            voteData = new List<TrackVotingObject>();
        }
    }
}
