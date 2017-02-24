using Amdox.IDataModel.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public partial class DocumentVM : IDocumentVM
    {
        public int DocumentId { get; set; }

        public int PollId { get; set; }

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }
    }
}
