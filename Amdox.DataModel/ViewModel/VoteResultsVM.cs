using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class VoteResultsVM:IVoteResultsVM
    {
        public VoteResultsVM()
        {
            VoteResultList = new List<IVoteResultsDataVM>();
        }
        public List<IVoteResultsDataVM> VoteResultList { get; set; }
    }
}
