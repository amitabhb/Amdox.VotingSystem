using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVoteResultsDataVM
    {
        int voteNumber { get; set; }
        string voteTitle { get; set; }
        string voteDescription { get; set; }
        List<IQuestionStatisticsVM> resolutions { get; set; }

        string voteType { get; set; }

        decimal NoVotePercentage { get; set; }

        bool isAccepted { get; set; }

        
    }

    


}
