using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVoteProxyListVM
    {
        List<IPersonnelForProxyVM> proxyPersonnelList { get; set; }
    }
}
