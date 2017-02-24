using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVoteResultsVM
    {
        List<IVoteResultsDataVM> VoteResultList { get; set; }
    }
}
