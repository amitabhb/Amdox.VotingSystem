using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVoterVM
    {
        //int UserID { get; set; }
        string Name { get; set; }
        string Grade { get; set; }
        string Proxy { get; set; }
        string isVoted { get; set; }

        int voterID { get; set; }
        //int VoteNo { get; set; }
    }
}
