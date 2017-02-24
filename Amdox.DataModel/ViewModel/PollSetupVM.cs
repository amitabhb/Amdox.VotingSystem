using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class PollSetupVM : IPollSetupVM
    {

        public bool? Editable { get; set; }
        public bool? IsPublished { get; set; }
        public int PollId { get; set; }

        public int? PollType
        {
            get; set;
        }

        public string PollTitle
        {
            get; set;
        }

        public string PollLongDescription
        {
            get; set;
        }
        [DataType(DataType.DateTime)]
        public DateTime? StartDate
        {
           get; set;
        }
         [DataType(DataType.DateTime)]
        public DateTime? EndDate
        {
            get; set;
        }
        public bool AutoClose { get; set; }
        public string DocumentNumber
        {
            get; set;
        }

        public string DocumentURL
        {
            get; set;
        }

        public List<string> Questions
        {
            get; set;
        }
        public string Resolutions { get; set; }
        //public List<IDocumentVM> DocumentDetails { get; set; }

        //public List<IQuestionVM> Questions { get; set; } 
        public List<SupportingDocument> DocumentDetails
        {
            get;
            set;
        }

        List<QuestionDetails> IPollSetupVM.Questions
        {
            get;
            set;
        }

        public int VoteTypePercentage { get; set; }

        
    }

}
