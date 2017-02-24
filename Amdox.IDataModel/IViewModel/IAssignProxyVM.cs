using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IAssignProxyVM
    {
        List<proxyDropDownListClass> proxyDropDownlist { get; set; }
        List<IPersonnelForProxyVM> proxyPersonnelList { get; set; }
        int dropDownNum { get; set; }
    }

    public class proxyDropDownListClass
    {
        public int usrid {get;set;}
        public string usrname {get;set;}
    }
}
