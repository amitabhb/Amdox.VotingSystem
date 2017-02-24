using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class VoteProxyListVM : IVoteProxyListVM
    {
        public VoteProxyListVM()
        {
            proxyPersonnelList = new List<IPersonnelForProxyVM>();
        }
        public List<IPersonnelForProxyVM> proxyPersonnelList { get; set; }
        
    }
}
