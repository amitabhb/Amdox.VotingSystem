using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel
{
    public interface IPublishPollObject
    {
        int voteNumber { get; set; }
        string partnerVoteTitle { get; set; }
        System.DateTime? startDate { get; set; }
        System.DateTime? endDate { get; set; }
        List<Questions> resolutionList { get; set; }
        bool? isPublished{ get; set;}
        bool isOpen { get; set; }
        bool isClosed { get; set; }
        bool? IsVisible { get; set; }
        string PollLongDescription{ get; set; }
        int? PollType{ get; set; }
        int? StatusID { get; set; }
        int resolutionCount { get; set; }
    }

    public class Questions
    {

        

        public int? Sequence;
        public string resolutionDescription;
        public string answer;

    }
}
