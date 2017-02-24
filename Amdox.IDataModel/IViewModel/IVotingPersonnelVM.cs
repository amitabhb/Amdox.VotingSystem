using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVotingPersonnelVM
    {
        List<IPersonnelObject> PersonnelLIst { get; set; } 
       
    }
}
