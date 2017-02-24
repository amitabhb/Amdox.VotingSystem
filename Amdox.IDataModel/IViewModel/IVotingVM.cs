using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IVotingVM
    {
        int userid { get; set; }
        string username { get; set; }
        int voteNumber { get; set; }
        List<Amdox.IDataModel.Questions> questionList { get; set; }
        List<int> Answers { get; set; }
        List<Options> OptionList { get; set; }
        string voteTitle { get; set; }
        string docURL { get; set; }
        string voteDescription { get; set; }
        bool isAllowed { get; set; }
        bool isValidUser { get; set; }
        bool isValidPoll { get; set; }
        bool isOpen { get; set; }

        List<IDocumentVM> DocumentDetails { get; set; }
        bool isView { get; set; }

        bool isProxy { get; set; }
    }

    public class Options
    {
        public int id;
        public string Name;
    }
}
