using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

using CRS.IBusinessManager;
using CRS.DataModel;
using CRS.IDataModel.IViewModel;
using Ninject;
using CRS.IDataModel;


namespace CRS.BusinessManager
{
    public class EmailManager : IEmailManager
    {
        [Inject]
        public VoteContext dataContext;

        [Inject]
        public UserManager objUsermanager;

        [Inject]
        public PollManager objPollManager;

        [Inject]
        public IPublishPollObject objPublishPollObject;

        [Inject]
        public EmailContent objEmailContent;
        
        public string ApplicationPath = "http://CRSTIRANOG/PartnerVotingSystem";
        //[Inject]GetPollDetails
        //public IPublishPollVM publishPollObjectVM;, CRS.DataModel.ViewModel.PublishPollVM _publishPollObjectVM

        public EmailManager(VoteContext _datacontext, UserManager _objUsermanager, 
            PollManager _objPollManager, IPublishPollObject _objPublishPollObject,EmailContent _objEmailContent)
        {
            this.dataContext = _datacontext;
            this.objUsermanager = _objUsermanager;
            this.objPollManager = _objPollManager;
            this.objPublishPollObject = _objPublishPollObject;
            this.objEmailContent=_objEmailContent;
        }

        public bool sendmail( EmailContent mailContent)
        {
           bool success = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(mailContent.from);
            message.IsBodyHtml = true;

            foreach(var recp in mailContent.recipients)
            {
                
                message.To.Add(recp.);
            }

            if(!string.IsNullOrEmpty(mailContent.cc))
            {
                message.To.Add(new MailAddress(mailContent.cc));
            }

            message.Subject = mailContent.SubjectLine;
            message.Body = mailContent.Body;

            SmtpClient client = new SmtpClient();
            client.Send(message);
            
            return success;
        }

        /*
         * Email type
         * 1: partner poll is open
         * 2. partner assigned proxy
         * 3. admin assgined proxy
         */

        public 
        public Tuple<string, string, string> EmailContentVotingOpen(int pollID)
        {
            //get the poll details
            objPublishPollObject=objPollManager.GetPollDetails(pollID);//ye kyu aa raha hai.. data type missmatch hain
            //ok ?wait..2 mins more

            //build the mail body
            StringBuilder MailBody=new StringBuilder();
            MailBody.Append(string.Format(@"<p><a href='" +ApplicationPath+">Please Click here</a>'"));
            MailBody.Append(@" to view resolution(s) and documents in relation to the above.</p>");
            MailBody.AppendLine();
            MailBody.Append(string.Format(@"<p>Your vote must be submitted before close on {0}</p>", objPublishPollObject.endDate));
            MailBody.Append(@"<p>If you have any questions, please contact Stefanie King.</p>");
            MailBody.AppendLine();
            MailBody.Append(@"<p>Many Thanks </p>");
          
            string SubjectLine = objPublishPollObject.partnerVoteTitle;//get poll title here
           // int EmailType = 1;//poll is open forall active partner 
                                        
            string Signature = string.Empty;//may be suppllied later

            objEmailContent.SubjectLine=SubjectLine;
            objEmailContent.Body=MailBody.ToString(); 
            objEmailContent.recipients = objUsermanager.GetPersonnelEmailIDs();//get all the active users email ids
            string from= ConfigurationManager.AppSettings["ADMINEMAIL"];// "salim.mandrekar@crsblaw.com";//currenlty hardcoding my email id

            objEmailContent.cc = string.Empty;

            Tuple<string, string, string, string> returnData = new Tuple<string, string, string, string>(SubjectLine,MailBody.ToString(),from,objEmailContent.recipients )

            return returnData;
        }
    }

}
