using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class VotingVM : IVotingVM
    {
        public VotingVM()
        {
            Answers = new List<int>();
            OptionList = new List<Options>()
            {
                new Options(){id=1,Name = "In Favour"},
                new Options(){id=2,Name = "Against"},
                new Options(){id=3,Name = "Abstain"}

            };
        }
        public string username { get; set; }
        public int userid { get; set; }
        public bool isView { get; set; }
        public List<int> Answers { get; set; }
        public List<Options> OptionList { get; set; }
        public string voteTitle { get; set; }
        public string docURL { get; set; }
        public string voteDescription { get; set; }
        public bool isAllowed { get; set; }
        public bool isValidUser { get; set; }
        public bool isValidPoll { get; set; }
        public bool isOpen { get; set; }

        public List<IDocumentVM> DocumentDetails { get; set; }

        public int voteNumber { get; set; }

        public List<Amdox.IDataModel.Questions> questionList { get; set; }
        public bool isProxy { get; set; }
    }
}
