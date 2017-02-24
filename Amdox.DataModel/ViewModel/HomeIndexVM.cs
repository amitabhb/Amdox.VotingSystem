using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Amdox.DataModel.ViewModel
{
    public class HomeIndexVM : IHomeIndexVM
    {

        public int UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public int UserType
        {
            get;
            set;
        }

        public int PartnerType
        {
            get;
            set;
        }
    }
}
