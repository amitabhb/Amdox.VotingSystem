using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class VotingPersonnelVM:IVotingPersonnelVM
    {
        public List<IDataModel.IPersonnelObject> PersonnelLIst { get; set; }
        
    }
}
