using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IPersonnelForProxyVM
    {
        int UserID { get; set; }
        string Name { get; set; }
        string Initials { get; set; }
        string Title { get; set; }
        string Grade { get; set; }
        string AdminRights { get; set; }
        string International { get; set; }
        string Office { get; set; }
        string ProxyName { get; set; }
        string ProxyInitials { get; set; }
        bool isProxy { get; set; }

        
    }
}
