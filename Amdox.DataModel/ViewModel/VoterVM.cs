using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class VoterVM:IVoterVM
    {
        //public int UserID { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public string Proxy { get; set; }
        public string isVoted { get; set; }
        public int voterID { get; set; }
        //public int VoteNo { get; set; }
    }
}
