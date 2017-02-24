using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel
{
    public interface IPersonnelObject
    {
        int UserID { get; set; }
        string UserName { get; set; }
        string Department { get; set; }
        DateTime LastModifiedDate { get; set; }
        string LastModifiedUserid { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string Initials { get; set; }
        string Title { get; set; }
        string Grade { get; set; }
        string AdminRights { get; set; }
        string International { get; set; }
        string Office { get; set; }
        string Resigned { get; set; }
        DateTime? DateResigned { get; set; }
        DateTime? DateLeft { get; set; }
        string EmailAddress { get; set; }
        bool isEmailNotification { get; set; }
        string proxy { get; set; }

    }
}
