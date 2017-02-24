using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRS.IBusinessManager;
using CRS.IDataModel.IViewModel;

namespace CRS.IBusinessManager
{
    public interface IEmailManager
    {
        bool sendmail(EmailContent mailContent);
        EmailContent EmailContentVotingOpen(int pollID);
    }
    public class EmailContent
    {
        public string SubjectLine { get; set; }
        public int EmailType { get; set; }
        public string Body { get; set; }
        public string Signature { get; set; }
        public List<IUserDetail> recipients { get; set; }
        public string from { get; set; }
        public string cc { get; set; }
    }
}
