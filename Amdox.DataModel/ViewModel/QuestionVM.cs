using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class QuestionVM : IQuestionVM
    {
        public int QuestionId { get; set; }
        public int PollId { get; set; }

        public string QuestionDescription { get; set; }

        public int Sequence { get; set; }

        public bool IsEnabled { get; set; }
    }
}
