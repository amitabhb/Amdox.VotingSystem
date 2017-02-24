using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;
using Amdox.IDataModel;

namespace Amdox.DataModel.ViewModel
{
    public class NewUserVM:INewUserVM
    {
        public List<IPartnerVoteObject> VoteList { get; set; }
        public IPersonnelObject User { get; set; }
    }
}
