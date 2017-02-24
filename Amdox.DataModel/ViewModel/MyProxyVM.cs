using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;
using Amdox.IDataModel;

namespace Amdox.DataModel.ViewModel
{
    public class MyProxyVM : IMyProxyVM
    {
        public MyProxyVM()
        {
            proxyDropDownlist = new List<proxyDropDownListClass>();
            
        }
        public bool isProxy { get; set; }

        public int dropDownNum { get; set; }

        public List<proxyDropDownListClass> proxyDropDownlist { get; set; }

        public IPersonnelObject myProxy { get; set; }
        public IPersonnelObject MyDetails { get; set; }


        
    }
}
