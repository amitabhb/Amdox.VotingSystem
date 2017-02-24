using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IPublishPollVM
    {
        List<IPublishPollObject> publishPollObjectList { get; set; }
    }
}
