using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVoteConfidentialInfoVM
    {
        string strPartnerName { get; set; }
        string strPartnerType { get; set; }
        string strProxyName { get; set; }
        string strIsVoted { get; set; }
        string strResolutionDescription { get; set; }
        string strOptionDescription { get; set; }

    }
}
