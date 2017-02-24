﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel.IViewModel;
using Amdox.IDataModel;

namespace Amdox.DataModel
{
    public class PersonnelObject : IPersonnelObject
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedUserid { get; set; }
        public string Department { get; set; }
        public string LastName {get;set;}
        public string FirstName {get;set;}
        public string Initials {get;set;}
        public string Title {get;set;}
        public string Grade {get;set;}
        public string AdminRights {get;set;}
        public string International {get;set;}
        public string Office {get;set;}
        public string Resigned {get;set;}
        public DateTime? DateResigned {get;set;}
        public DateTime? DateLeft {get;set;}
        public string EmailAddress { get; set; }
        public bool isEmailNotification { get; set; }

        public string proxy { get; set; }
    }
}
