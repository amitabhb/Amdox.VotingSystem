using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel;

namespace Amdox.DataModel
{
    public class PublishPollObject :IPublishPollObject
    {

        public int voteNumber
        {
            get;
            set; 
        }

        public string partnerVoteTitle
        {
            get;
            set; 
        }

        public DateTime? startDate
        {
            get;
            set; 
        }

        public DateTime? endDate
        {
            get;
            set; 
        }

        public List<Questions> resolutionList
        {
            get;
            set; 
        }
        public int resolutionCount { get; set; }
        public bool? isPublished
        {
            get;
            set; 
        }

        public bool isOpen
        {
            get;
            set; 
        }

        public bool isClosed
        {
            get;
            set; 
        }
        public bool? IsVisible
        { 
            get; set; 
        }
        public string PollLongDescription
        { 
            get; 
            set; 
        }
        public int? PollType 
        {
            get;
            set;
        }
        public int? StatusID
        {
            get;
            set;
        }
    }
}
