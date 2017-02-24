using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IQuestionVM
    {
        int QuestionId { get; set; }
        int PollId { get; set; }
        string QuestionDescription { get; set; }
        int Sequence { get; set; }
        bool IsEnabled { get; set; }
    }
}
