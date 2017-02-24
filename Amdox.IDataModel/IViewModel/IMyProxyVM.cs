using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel;

namespace Amdox.IDataModel.IViewModel
{
    public interface IMyProxyVM
    {
        bool isProxy { get; set; }
        
        int dropDownNum { get; set; }
       
         List<proxyDropDownListClass> proxyDropDownlist { get; set; }

         IPersonnelObject myProxy { get; set; }
         IPersonnelObject MyDetails { get; set; }

    }
}
