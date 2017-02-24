using Amdox.IDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class EmailNotificationsVM:IEmailNotificationsVM
    {
        public EmailNotificationsVM()
        {
            UserList = new List<IPersonnelObject>();
        }
        public List<IPersonnelObject> UserList { get; set; }
    }
}
