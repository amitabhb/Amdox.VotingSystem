using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class PersonnelForProxyVM:IPersonnelForProxyVM
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }
        public string AdminRights { get; set; }
        public string International { get; set; }
        public string Office { get; set; }
        public string ProxyName { get; set; }
        public string ProxyInitials { get; set; }
        public bool isProxy { get; set; }
    }
}
