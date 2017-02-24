using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IEditUserVM
    {
        int selectedTitle { get; set; }
        string UserID { get; set; }
        string LastName { get; set; }
        string Department { get; set; }
        string FirstName { get; set; }
        string Initials { get; set; }
        string UserName { get; set; }
        DateTime LastModifiedDate { get; set; }
        string LastModifiedUserid { get; set; }
        int Title { get; set; }
        int Grade { get; set; }
        int AdminRights { get; set; }
        int International { get; set; }
        string Office { get; set; }
        int Resigned { get; set; }
        DateTime? DateResigned { get; set; }
        DateTime? DateLeft { get; set; }
        string EmailAddress { get; set; }
        List<DictObject> GradeOptions { get; set; }
        List<DictObject> TitleOptions { get; set; }
        List<DictObject> YesNoOptions { get; set; }
        List<DictObject> AdminRightsOptions { get; set; }
        int GetValue(string stext,List<DictObject> dolist);
        string GetText(int svalue,List<DictObject> dolist);
        bool IsNew { get; set; }
        bool IsEmail { get; set; }
        string PageTitle { get; set; }
        bool isExists { get; set; }

        
    }

    public class DictObject
    {
        public int dictvalue {get;set;}
        public string dicttext{get;set;}

        
    }
}
