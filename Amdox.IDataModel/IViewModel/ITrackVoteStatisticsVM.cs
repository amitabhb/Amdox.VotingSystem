using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface ITrackVoteStatisticsVM
    {
         int iVoteNumber{get;set;}
         string strVoteTitle{get;set;}
         string strvoteStatus { get; set; }
         DateTime dtStartDate{get;set;}
         DateTime dteEndDate{get;set;}
         int iVotesCastCount{get;set;}
         decimal dVotesCastPerc { get; set; }
         int iOutstandingCount{get;set;}
         decimal dOutstandingPerc{get;set;}
         List<IQuestionVM> lstResolutions { get; set; }

         int selectedPollID { get; set; }
    }
}
