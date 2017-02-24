using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Amdox.IDataModel.IViewModel;
    public interface IPollSetupVM
    {
        bool? Editable { get; set; }
        bool? IsPublished { get; set; }
        int PollId { get; set; }

        [DisplayName("Partner Vote Type")]
        int? PollType { get; set; }

        [DisplayName("Partner Vote Title")]
         string PollTitle { get; set; }

        [DisplayName("Partner Vote Description")]
         string PollLongDescription { get; set; }

        [DisplayName("Partner Vote Start Date and Time")]
         DateTime? StartDate { get; set; }

        [DisplayName("Partner Vote End Date and Time")]
         DateTime? EndDate { get; set; }

        bool AutoClose { get; set; }
        [DisplayName("Partner Vote Document Number")]
        List<SupportingDocument> DocumentDetails { get; set; }

        List<QuestionDetails> Questions { get; set; }
        string Resolutions { get; set; }
         int VoteTypePercentage { get; set; }
          //List<IDocumentVM> DocumentDetails { get; set; }

          //List<IQuestionVM> Questions { get; set; } 
    }
public class SupportingDocument
{
    public string DocumentNumber { get; set; }

    public string DocumentURL { get; set; }
}

public class QuestionDetails
{
    public string Question { get; set; }
    public int Sequence { get; set; }
}