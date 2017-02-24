using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel;

namespace Amdox.DataModel
{
    public class PartnerVoteObject:IPartnerVoteObject
    {
        public int voteNumber { get; set; }
        public string partnerVoteTitle { get; set; }
        public string partnerVoteDescription { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public List<Questions> resolutionList { get; set; }
        public string PartnerVoteStatus { get; set; }
        public bool isOpen { get; set; }
        public bool isCompleted { get; set; }
        public string VotingType { get; set; }
        public string Document { get; set; }
        public DateTime? LastChanged { get; set; }

        public string PartnerVoteType { get; set; }
    }
}
