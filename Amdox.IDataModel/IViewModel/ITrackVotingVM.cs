using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface ITrackVotingVM
    {
        List<TrackVotingObject> voteData { get; set; }
        int UserTypeID { get; set; }
        
    }

    public class TrackVotingObject
    {
        public int voteNumber;
        public string voteTitle;
        public DateTime startDate;
        public DateTime endDate;
        public int VotesCastCount;
        public decimal VotesCastPerc;
        public int OutstandingCount;
        public decimal OutstandingPerc;
        public List<IQuestionVM> resolutions;
       



    }
}
