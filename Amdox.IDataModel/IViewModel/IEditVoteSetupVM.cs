using Amdox.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IEditVoteSetupVM
    {
        int PollId { get; set; }

        int? PollType { get; set; }

        string PollTitle { get; set; }

         [DataType(DataType.MultilineText)]
        string PollLongDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd MM yyyy}",ApplyFormatInEditMode=true)]
        DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}", ApplyFormatInEditMode = true)]
        DateTime? EndDate { get; set; }

        bool? IsPublished { get; set; }

        List<IDocumentVM> DocumentDetails { get; set; }

        List<IQuestionVM> Questions { get; set; }
        string Resolutions { get; set; }
    }
}
