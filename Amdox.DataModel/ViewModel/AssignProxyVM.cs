using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class AssignProxyVM:IAssignProxyVM
    {
        public AssignProxyVM()
        {
            proxyDropDownlist = new List<proxyDropDownListClass>();
            proxyPersonnelList = new List<IPersonnelForProxyVM>();
        }
        public int dropDownNum {get;set;}
        public List<IPersonnelForProxyVM> proxyPersonnelList { get; set; }
        public List<proxyDropDownListClass> proxyDropDownlist { get; set; }



    }
}
