using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IDocumentVM
    {
        int DocumentId { get; set; }
        int PollId { get; set; }
        string ContentType { get; set; }
        string FileName { get; set; }
        string FileURL { get; set; }
    }
}
