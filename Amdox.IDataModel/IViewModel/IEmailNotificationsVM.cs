using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IEmailNotificationsVM
    {
        List<IPersonnelObject> UserList { get; set; }
    }
}
