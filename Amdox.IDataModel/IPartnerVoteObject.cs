using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel
{
    public interface IPartnerVoteObject
    {
        int voteNumber { get; set; }
        string partnerVoteTitle { get; set; }
        string partnerVoteDescription { get; set; }
        System.DateTime? startDate { get; set; }
        System.DateTime? endDate { get; set; }
        List<Questions> resolutionList { get; set; }
        string PartnerVoteStatus { get; set; }
        bool isOpen { get; set; }
        bool isCompleted { get; set; }
        string VotingType { get; set; }
        string Document { get; set; }
        DateTime? LastChanged { get; set; }
        string PartnerVoteType { get; set; }
    }
}
