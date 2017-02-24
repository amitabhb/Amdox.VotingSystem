using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class EditVoteSetupVM: IEditVoteSetupVM
    {

        public int PollId { get; set; } 

        public int? PollType { get; set; }

        public string PollTitle { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string PollLongDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public bool? IsPublished { get; set; }

        public List<IDocumentVM> DocumentDetails { get; set; }

        public List<IQuestionVM> Questions { get; set; }
        public string Resolutions { get; set; }
    }
}
