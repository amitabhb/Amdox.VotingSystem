using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IPartnervoteVM
    {
        bool isValidUser { get; set; }
        bool isAllowed { get; set; }
        int userid { get; set; }
        string message { get; set; }
        List<IPartnerVoteObject> PartnerVoteList { get; set; }
    }
}
