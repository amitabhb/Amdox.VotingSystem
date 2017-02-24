using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;
using Amdox.IDataModel;

namespace Amdox.DataModel.ViewModel
{
    public class PartnervoteVM:IPartnervoteVM
    {
        public bool isValidUser { get; set; }
        public string message { get; set; }
        public List<IPartnerVoteObject> PartnerVoteList { get; set; }

        public int userid { get; set; }
        public bool isAllowed { get; set; }
    }
}
