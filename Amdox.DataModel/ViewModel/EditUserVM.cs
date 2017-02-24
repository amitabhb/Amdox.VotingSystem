using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;

namespace Amdox.DataModel.ViewModel
{
    public class EditUserVM : IEditUserVM
    {
        public EditUserVM()
        {
            //Grade = new DictObject();
            
            GradeOptions = new List<DictObject>()
            {
                new DictObject(){dicttext="A",dictvalue=0},
                new DictObject(){dicttext="B",dictvalue=1},
                new DictObject(){dicttext="Non Partner",dictvalue=2}
            };
            TitleOptions = new List<DictObject>()
            {
                new DictObject(){dicttext="Mr",dictvalue=0},
                new DictObject(){dicttext="Ms",dictvalue=1},
                new DictObject(){dicttext="Mrs",dictvalue=2},
                new DictObject(){dicttext="Miss",dictvalue=3},
                new DictObject(){dicttext="",dictvalue=4}
            };
            AdminRightsOptions = new List<DictObject>()
            {
                new DictObject(){dicttext="Y",dictvalue=0},
                new DictObject(){dicttext="N",dictvalue=1},
                new DictObject(){dicttext="S",dictvalue=2}

            };
            YesNoOptions = new List<DictObject>()
            {
                new DictObject(){dicttext="N",dictvalue=0},
                new DictObject(){dicttext="Y",dictvalue=1}
            };
        }

        public string UserID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Initials { get; set; }
        public string Department { get; set; }

        public string UserName { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedUserid { get; set; }

        public int Title { get; set; }
        public int selectedTitle { get; set; }

        public int Grade { get; set; }

        public int AdminRights { get; set; }

        public int International { get; set; }

        public string Office { get; set; }

        public int Resigned { get; set; }

        public DateTime? DateResigned { get; set; }

        public DateTime? DateLeft { get; set; }

        public string EmailAddress { get; set; }

        
        public List<DictObject> TitleOptions { get; set; }
        public List<DictObject> YesNoOptions { get; set; }
        public List<DictObject> AdminRightsOptions { get; set; }

        public List<DictObject> GradeOptions { get; set; }

        public bool IsNew { get; set; }

        public bool IsEmail { get; set; }

        public string PageTitle { get; set; }

        public bool isExists { get; set; }

        public int GetValue(string stext,List<DictObject> dolist)
        {
            if(stext==null)
            {
                stext = "";
            }
            var item = dolist.First(x => x.dicttext == stext);
            return item.dictvalue;
        }

        public string GetText(int svalue,List<DictObject> dolist)
        {
            var item = dolist.First(x => x.dictvalue == svalue);
            return item.dicttext;
        }
    }
}
