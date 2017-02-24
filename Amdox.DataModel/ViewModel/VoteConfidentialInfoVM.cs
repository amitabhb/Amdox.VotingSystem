using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class VoteConfidentialInfoVM : IVoteConfidentialInfoVM
    {
        public string strPartnerName { get; set; }

        public string strPartnerType { get; set; }

        public string strProxyName { get; set; }

        public string strIsVoted { get; set; }

        public string strResolutionDescription { get; set; }

        public string strOptionDescription { get; set; }
    }
}
