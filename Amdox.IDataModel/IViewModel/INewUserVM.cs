using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface INewUserVM
    {
        List<IPartnerVoteObject> VoteList {get;set;}
        IPersonnelObject User { get; set; }
    }
}
