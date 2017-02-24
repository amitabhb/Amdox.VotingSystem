//-----------------------------------------------------------------------
// <copyright file="PollManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Poll manager class
/// </summary>
namespace Amdox.BusinessManager
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;

    using Amdox.DataModel;
    using Amdox.DataModel.ViewModel;
    using Amdox.IBusinessManager;
    using Amdox.IDataModel;
    using Amdox.IDataModel.IViewModel;
    using Ninject;

    /// <summary>
    /// IPollManager implementation
    /// </summary>
    public class PollManager : IPollManager
    {
        /// <summary>
        /// application path
        /// </summary>
        private readonly string applicationPath = "http://CRSTIRANOG/VotingSystem";

        /// <summary>
        /// data context.
        /// </summary>
        [Inject]
        private VoteContext dataContext;

        /// <summary>
        /// Tracking view model
        /// </summary>
        [Inject]
        private ITrackVotingVM trackvotingdata;

        /// <summary>
        /// Tracking stats view model.
        /// </summary>
        [Inject]
        private ITrackVoteStatisticsVM trackVoteStatistics;

        /// <summary>
        /// Track detail view model.
        /// </summary>
        [Inject]
        private ITrackVoteDetailsVM trackVoteDetails;

        /// <summary>
        /// Vote result view model.
        /// </summary>
        [Inject]
        private IVoteResultsVM voteresultsVM;

        /// <summary>
        /// user manager business
        /// </summary>
        [Inject]
        private UserManager objUsermanager;

        /// <summary>
        /// View vote setup view model.
        /// </summary>
        [Inject]
        private IViewVoteSetupVM objIViewVoteSetupVM;

        /// <summary>
        /// Edit vote view model.
        /// </summary>
        [Inject]
        private IEditVoteSetupVM objIEditVoteSetupVM;

        /// <summary>
        /// DI kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// Publish poll object.
        /// </summary>
        [Inject]
        private IPublishPollObject objPublishPollObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="PollManager" /> class.
        /// </summary>
        /// <param name="datacontext">data context</param>
        /// <param name="objUsermanager">user manager business</param>
        /// <param name="objPublishPollObject">publish poll object</param>
        /// <param name="objIViewVoteSetupVM">View vote view model</param>
        /// <param name="objIEditVoteSetupVM">Edit vote view model</param>
        /// <param name="kernel">DI kernel</param>
        /// <param name="voteresultsVM">vote results view model</param>
        /// <param name="trackvotingdata">track voting data view model</param>
        /// <param name="trackVoteStatistics">track Vote Statistics view model</param>
        /// <param name="trackVoteDetails">track Vote Details view model</param>
        public PollManager(
            VoteContext datacontext,
            UserManager objUsermanager,
            IPublishPollObject objPublishPollObject,
            IViewVoteSetupVM objIViewVoteSetupVM,
            IEditVoteSetupVM objIEditVoteSetupVM,
            IKernel kernel,
            IVoteResultsVM voteresultsVM,
            ITrackVotingVM trackvotingdata,
            ITrackVoteStatisticsVM trackVoteStatistics,
            ITrackVoteDetailsVM trackVoteDetails)
        {
            this.dataContext = datacontext;
            this.objUsermanager = objUsermanager;
            this.objIViewVoteSetupVM = objIViewVoteSetupVM;
            this.objPublishPollObject = objPublishPollObject;
            this.objIEditVoteSetupVM = objIEditVoteSetupVM;

            this.applicationPath = ConfigurationManager.AppSettings["AppPath"];
            this.kernel = kernel;
            this.voteresultsVM = voteresultsVM;
            this.trackvotingdata = trackvotingdata;
            this.trackVoteStatistics = trackVoteStatistics;
            this.trackVoteDetails = trackVoteDetails;
        }

        /// <summary>
        /// Add doc details
        /// </summary>
        /// <param name="pollId">poll id</param>
        /// <param name="filenameGuid">file guid id</param>
        /// <param name="filename">file name</param>
        /// <param name="path">file path</param>
        /// <param name="contentType">content type</param>
        /// <param name="userName">user name</param>
        /// <returns>added document id</returns>
        public int AddDocumentDetails(int pollId, string filenameGuid, string filename, string path, string contentType, string userName)
        {
            Amdox.DataModel.Poll_Documents data = new Amdox.DataModel.Poll_Documents
            {
                DocumentURL = path,
                DocumentNumber = filename,
                UploadedBy = this.objUsermanager.GetUserID(userName),
                UploadedDate = DateTime.Now,
                PollID = pollId,
                IsEnabled = true,
                ContentType = contentType,
                FileName = filenameGuid
            };

            this.dataContext.Poll_Documents.Add(data);
            this.dataContext.SaveChanges();

            return data.DocumentID;
        }

        /// <summary>
        /// Update vote setup.
        /// </summary>
        /// <param name="objPollSetupInfo">edited vote details</param>
        /// <returns>vote id</returns>
        public int UpdateVote(IEditVoteSetupVM objPollSetupInfo)
        {
            try
            {
                var objPollInfo = this.dataContext.Poll_Maintenance.Find(objPollSetupInfo.PollId);
                if (objPollInfo != null)
                {
                    objPollInfo.PollTitle = objPollSetupInfo.PollTitle;
                    objPollInfo.PollLongDescription = objPollSetupInfo.PollLongDescription;
                    objPollInfo.StartDate = objPollSetupInfo.StartDate;
                    objPollInfo.EndDate = objPollSetupInfo.EndDate;
                    objPollInfo.PollType = objPollSetupInfo.PollType;
                    this.dataContext.SaveChanges();
                }

                ////update poll questions
                this.UpdateQuestions(objPollSetupInfo.PollId, objPollSetupInfo);
            }
            catch
            {
            }

            return objPollSetupInfo.PollId;
        }

        /// <summary>
        /// Create a poll.
        /// </summary>
        /// <param name="objPollSetupInfo">poll setup view model</param>
        /// <param name="createdUserName">logged user name</param>
        /// <returns>poll id</returns>
        public int CreatePoll(IPollSetupVM objPollSetupInfo, string createdUserName)
        {
            Amdox.DataModel.Poll_Maintenance data = new Amdox.DataModel.Poll_Maintenance
            {
                PollTitle = objPollSetupInfo.PollTitle,
                PollLongDescription = objPollSetupInfo.PollLongDescription,
                PollType = objPollSetupInfo.PollType,
                StartDate = objPollSetupInfo.StartDate,
                EndDate = objPollSetupInfo.EndDate,
                CreatedDate = DateTime.Now,
                StatusID = this.dataContext.Poll_Status.FirstOrDefault(x => x.StatusDescription == "Not Open").StatusID,
                IsPublished = false,
                IsAutoClose = objPollSetupInfo.AutoClose,
                CreatedBy = this.objUsermanager.GetUserID(createdUserName),
                IsVisible = true,
                VoteTypePercentage = objPollSetupInfo.VoteTypePercentage
            };
            this.dataContext.Poll_Maintenance.Add(data);
            this.dataContext.SaveChanges();
            ////Add Questions to the poll in poll_Questions
            this.AddQuestions(data.PollId, objPollSetupInfo);
            ////add Poll Users
            this.AddPollUsers(data.PollId);

            return data.PollId;
        }

        /// <summary>
        /// Update vote questions.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <param name="objPollSetup">poll setup</param>
        public void UpdateQuestions(int pollID, IEditVoteSetupVM objPollSetup)
        {
            try
            {
                ////update prev questions, set isenabled to false
                var objPollQuestions = this.dataContext.Poll_Questions.Where(x => x.PollID == pollID);

                if (objPollQuestions != null)
                {
                    foreach (var a in objPollQuestions)
                    {
                        a.IsEnabled = false;
                    }

                    this.dataContext.SaveChanges();
                }

                if (objPollSetup != null)
                {
                    string[] arrQuestion;
                    List<IQuestionVM> lstQuestions = new List<IQuestionVM>();
                    if (objPollSetup.Resolutions != string.Empty || objPollSetup.Resolutions != string.Empty)
                    {
                        arrQuestion = objPollSetup.Resolutions.TrimEnd('¬').Split('¬');
                        int count = 0;
                        foreach (var a in arrQuestion)
                        {
                            count++;
                            var data = this.kernel.Get<IQuestionVM>();
                            data.PollId = pollID;
                            data.QuestionDescription = a.ToString();
                            data.Sequence = count;
                            lstQuestions.Add(data);
                        }
                        ////_editVoteSetupVM.Questions = lstQuestions;
                    }

                    ////add new questions
                    foreach (var q in lstQuestions)
                    {
                        Amdox.DataModel.Poll_Questions qdata = new Amdox.DataModel.Poll_Questions
                        {
                            QuestionDescription = q.QuestionDescription,
                            Sequence = q.Sequence,
                            PollID = pollID,
                            ModifiedDate = DateTime.Now,
                            IsEnabled = true
                        };
                        this.dataContext.Poll_Questions.Add(qdata);
                        this.dataContext.SaveChanges();
                        this.AddOptions(qdata.QuestionID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Add questions to poll.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <param name="objPollSetup">poll setup view model</param>
        public void AddQuestions(int pollID, IPollSetupVM objPollSetup)
        {
            foreach (var q in objPollSetup.Questions)
            {
                Amdox.DataModel.Poll_Questions qdata = new Amdox.DataModel.Poll_Questions
                {
                    QuestionDescription = q.Question,
                    Sequence = q.Sequence,
                    PollID = pollID,
                    ModifiedDate = DateTime.Now,
                    IsEnabled = true
                };
                this.dataContext.Poll_Questions.Add(qdata);
                this.dataContext.SaveChanges();
                this.AddOptions(qdata.QuestionID);
            }
        }

        /// <summary>
        /// Get vote options.
        /// </summary>
        /// <returns>get all options</returns>
        public Dictionary<string, int> GetOptions()
        {
            Dictionary<string, int> lstoptions = new Dictionary<string, int>();
            try
            {
                var objOptions = (from a in this.dataContext.Poll_Options.AsEnumerable()
                                  where a.IsEnabled == true
                                  select new
                                  {
                                      a.OptionID,
                                      a.OptionName
                                  }).ToList();

                foreach (var a in objOptions)
                {
                    lstoptions.Add(a.OptionName, a.OptionID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return lstoptions;
        }

        /// <summary>
        /// Add questions options.
        /// </summary>
        /// <param name="questionID">question id</param>
        public void AddOptions(int questionID)
        {
            try
            {
                Dictionary<string, int> lstoptions = new Dictionary<string, int>();
                lstoptions = this.GetOptions();

                foreach (var a in lstoptions)
                {
                    Amdox.DataModel.Poll_Question_Options qdata = new Amdox.DataModel.Poll_Question_Options
                    {
                        OptionID = a.Value,
                        QuestionID = questionID
                    };

                    this.dataContext.Poll_Question_Options.Add(qdata);
                    this.dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Add poll users.
        /// </summary>
        /// <param name="pollId">poll id</param>
        public void AddPollUsers(int pollId)
        {
            try
            {
                List<int> lstGetUserIds = new List<int>();
                int totalVotesExpected, totalWeightedVotesExpected = 0;
                lstGetUserIds = this.GetActiveUserIds();
                totalVotesExpected = lstGetUserIds.Count;
                foreach (var u in lstGetUserIds)
                {
                    Amdox.DataModel.Poll_Users pollUsers = new Amdox.DataModel.Poll_Users
                    {
                        PollId = pollId,
                        UserID = Convert.ToInt32(u),
                        ////setting it to false as poll not started yet.
                        IsCompleted = false
                    };

                    var person = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == u && x.IsDeleted == false);
                    pollUsers.PartnerTypeID = person.PartnerTypeID;
                    this.dataContext.Poll_Users.Add(pollUsers);
                    this.dataContext.SaveChanges();

                    if (person.PartnerTypeID == 1)
                    {
                        totalWeightedVotesExpected += 2;
                    }
                    else if (person.PartnerTypeID == 2)
                    {
                        totalWeightedVotesExpected += 1;
                    }
                }

                var pollmant = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollId);
                pollmant.TotalVotesExpected = pollmant.TotalDidNotVote = totalVotesExpected;
                pollmant.ActualVotesSubmitted = pollmant.ActualWeightedVotesSubmitted = 0;
                pollmant.TotalWeightedDidNotVote = pollmant.TotalWeightedVotesExpected = totalWeightedVotesExpected;
                this.dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Get active users.
        /// </summary>
        /// <returns>list of all active user id</returns>
        public List<int> GetActiveUserIds()
        {
            List<int> lstGetUserIds = new List<int>();
            try
            {
                var objGetUserIds = (from u in this.dataContext.Users
                                     join ud in this.dataContext.UserDetails on u.UserID equals ud.UserID
                                     where u.IsEnabled == true &&
                                     u.IsAllowedToVote == true &&
                                     ud.IsDeleted == false &&
                                     ud.PartnerTypeID != null
                                     select new
                                     {
                                         u.UserID
                                     }).ToList();

                foreach (var u in objGetUserIds)
                {
                    lstGetUserIds.Add(u.UserID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return lstGetUserIds;
        }

        /// <summary>
        /// Send open poll notifications.
        /// </summary>
        /// <param name="pollId">poll id</param>
        /// <returns>flag true/false</returns>
        public bool SendOpenPollNotification(int pollId)
        {
            bool success = false;
            string strEmailSubject = Amdox.Common.Common.EmailText.OpenVoteSubject;
            string strEmailBody = Amdox.Common.Common.EmailText.OpenVoteEmailBody;

            var pollDetail = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollId);
            strEmailSubject = string.Format(strEmailSubject, pollDetail.PollTitle);
            strEmailBody = string.Format(strEmailBody, this.applicationPath, Convert.ToDateTime(pollDetail.EndDate).ToShortDateString());

            var userList = from a in this.dataContext.Poll_Users
                           join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                           where b.IsDeleted == false &&
                           b.IsEmailNotificationRequired == true &&
                           a.PollId == pollId
                           select b;

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            foreach (var pollUser in userList)
            {
                List<string> toEmailList = new List<string>();
                toEmailList.Add(pollUser.EmailAddress);
                success = Amdox.Common.EmailService.SendEmail(null, strFromEmailID, null, strEmailSubject, strEmailBody, toEmailList);
            }

            return success;
        }

        /// <summary>
        /// Publish poll details.
        /// </summary>
        /// <param name="pollId">poll id</param>
        /// <returns>flag true/false</returns>
        public bool SendPublishPollNotification(int pollId)
        {
            bool success = false;
            StringBuilder mailBody = new StringBuilder();
            string strEmailSubject = Amdox.Common.Common.EmailText.PublishVoteSubject;
            string strEmailBody = Amdox.Common.Common.EmailText.PublishVoteEmailBody;

            var pollDetail = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollId);
            strEmailSubject = string.Format(strEmailSubject, pollDetail.PollTitle);
            strEmailBody = string.Format(strEmailBody, this.applicationPath, Convert.ToDateTime(pollDetail.EndDate).ToShortDateString());

            var userList = from a in this.dataContext.Poll_Users
                           join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                           where b.IsDeleted == false &&
                           b.IsEmailNotificationRequired == true &&
                           a.PollId == pollId
                           select b;

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            foreach (var pollUser in userList)
            {
                List<string> toEmailList = new List<string>();
                toEmailList.Add(pollUser.EmailAddress);
                success = Amdox.Common.EmailService.SendEmail(toEmailList, strFromEmailID, null, strEmailSubject, strEmailBody, null);
            }

            return success;
        }

        /// <summary>
        /// Get poll details.
        /// currently using for sending email notification
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>publish poll object</returns>
        public IPublishPollObject GetPollDetails(int pollID)
        {
            try
            {
                var objPollDetails = from PM in this.dataContext.Poll_Maintenance
                                     where PM.PollId == pollID
                                     select new
                                     {
                                         PM.PollTitle,
                                         PM.StartDate,
                                         PM.EndDate
                                     };

                foreach (var obj in objPollDetails)
                {
                    this.objPublishPollObject.endDate = obj.EndDate;
                    this.objPublishPollObject.partnerVoteTitle = obj.PollTitle;
                    this.objPublishPollObject.startDate = obj.StartDate;
                }

                return this.objPublishPollObject;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get vote questions,
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>question view model</returns>
        public List<IQuestionVM> GetVoteQuestions(int pollID)
        {
            var objPollQuestions = from pq in this.dataContext.Poll_Questions
                                   where pq.PollID == pollID && pq.IsEnabled == true
                                   select pq;

            List<IQuestionVM> lstQuestions = new List<IQuestionVM>();
            if (objPollQuestions != null && objPollQuestions.Count() > 0)
            {
                foreach (var a in objPollQuestions)
                {
                    var data = this.kernel.Get<IQuestionVM>();
                    data.PollId = pollID;
                    data.QuestionId = a.QuestionID;
                    data.QuestionDescription = a.QuestionDescription;
                    data.Sequence = Convert.ToInt32(a.Sequence);
                    lstQuestions.Add(data);
                }
            }

            return lstQuestions;
        }

        /// <summary>
        /// Get vote document.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>document view model</returns>
        public List<IDocumentVM> GetVoteDocuments(int pollID)
        {
            List<IDocumentVM> lstDocuments = new List<IDocumentVM>();
            var objPollDocuments = from pd in this.dataContext.Poll_Documents
                                   where pd.PollID == pollID && pd.IsEnabled == true
                                   select pd;

            if (objPollDocuments != null && objPollDocuments.Count() > 0)
            {
                foreach (var a in objPollDocuments)
                {
                    var data = this.kernel.Get<IDocumentVM>();
                    data.DocumentId = a.DocumentID;
                    data.PollId = pollID;
                    data.FileName = a.DocumentNumber;
                    data.ContentType = a.ContentType;
                    data.FileURL = a.DocumentURL;
                    lstDocuments.Add(data);
                }
            }

            return lstDocuments;
        }

        /// <summary>
        /// Get Vote details.
        /// used for  view poll.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>vote setup view model</returns>
        public IViewVoteSetupVM GetVoteDetails(int pollID)
        {
            var objPollDetails = from PM in this.dataContext.Poll_Maintenance
                                 where PM.PollId == pollID
                                 select new
                                 {
                                     PM.PollTitle,
                                     PM.StartDate,
                                     PM.EndDate,
                                     PM.IsPublished,
                                     PM.PollLongDescription,
                                     PM.PollType
                                 };

            if (objPollDetails != null && objPollDetails.Count() > 0)
            {
                foreach (var a in objPollDetails)
                {
                    this.objIViewVoteSetupVM.PollLongDescription = a.PollLongDescription;
                    this.objIViewVoteSetupVM.PollTitle = a.PollTitle;
                    this.objIViewVoteSetupVM.PollType = a.PollType;
                    this.objIViewVoteSetupVM.StartDate = a.StartDate;
                    this.objIViewVoteSetupVM.EndDate = a.EndDate;
                    this.objIViewVoteSetupVM.IsPublished = a.IsPublished;
                    this.objIViewVoteSetupVM.PollId = pollID;
                }
            }

            this.objIViewVoteSetupVM.Questions = this.GetVoteQuestions(pollID);
            this.objIViewVoteSetupVM.DocumentDetails = this.GetVoteDocuments(pollID);
            return this.objIViewVoteSetupVM;
        }

        /// <summary>
        /// Update vote details.
        /// used for  edit poll.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>edit vote view model</returns>
        public IEditVoteSetupVM GetEditVoteDetails(int pollID)
        {
            var objPollDetails = from PM in this.dataContext.Poll_Maintenance
                                 where PM.PollId == pollID
                                 select new
                                 {
                                     PM.PollTitle,
                                     PM.StartDate,
                                     PM.EndDate,
                                     PM.IsPublished,
                                     PM.PollLongDescription,
                                     PM.PollType,
                                 };

            if (objPollDetails != null && objPollDetails.Count() > 0)
            {
                foreach (var a in objPollDetails)
                {
                    this.objIEditVoteSetupVM.PollLongDescription = a.PollLongDescription;
                    this.objIEditVoteSetupVM.PollTitle = a.PollTitle;
                    this.objIEditVoteSetupVM.PollType = a.PollType;
                    this.objIEditVoteSetupVM.StartDate = a.StartDate;
                    this.objIEditVoteSetupVM.EndDate = a.EndDate;
                    this.objIEditVoteSetupVM.IsPublished = a.IsPublished;
                    this.objIEditVoteSetupVM.PollId = pollID;
                }
            }

            this.objIEditVoteSetupVM.Questions = this.GetVoteQuestions(pollID);
            this.objIEditVoteSetupVM.DocumentDetails = this.GetVoteDocuments(pollID);

            return this.objIEditVoteSetupVM;
        }

        /// <summary>
        /// Get published poll.
        /// </summary>
        /// <returns>Publish Poll Object</returns>
        List<IPublishPollObject> IPollManager.GetPublishPollVM()
        {
            var objUserDetails = (from PM in this.dataContext.Poll_Maintenance
                                  join ps in this.dataContext.Poll_Status on PM.StatusID equals ps.StatusID
                                  where ps.StatusDescription != "Accepted"
                                  select new { PM, ps }).OrderByDescending(x => x.PM.CreatedDate).ToList();
            List<IDataModel.IPublishPollObject> publishPollObjectList = new List<IDataModel.IPublishPollObject>();
            if (objUserDetails.Count > 0)
            {
                int pollid = 0;

                foreach (var o in objUserDetails)
                {
                    if (pollid != o.PM.PollId)
                    {
                        pollid = o.PM.PollId;
                        PublishPollObject ppo = new PublishPollObject();
                        ppo.resolutionList = new List<IDataModel.Questions>();
                        publishPollObjectList.Add(ppo);
                        ppo.startDate = o.PM.StartDate;
                        ppo.endDate = o.PM.EndDate;
                        ppo.voteNumber = o.PM.PollId;
                        ppo.isPublished = o.PM.IsPublished;
                        ppo.partnerVoteTitle = o.PM.PollTitle;
                        ppo.resolutionCount = this.GetVoteResolutionCount(pollid);
                        if ((o.ps.StatusDescription == "Open") || (o.ps.StatusDescription == "Close"))
                        {
                            ppo.isOpen = true;
                        }
                        else
                        {
                            ppo.isOpen = false;
                        }

                        if (o.ps.StatusDescription == "Close")
                        {
                            ppo.isClosed = true;
                        }
                        else
                        {
                            ppo.isClosed = false;
                        }

                        ////ppo.resolutionList.Add(
                        ////    new IDataModel.Questions
                        ////    {
                        ////        Sequence = o.Sequence,
                        ////        resolutionDescription = o.QuestionDescription
                        ////    });
                    }
                    else
                    {
                        var ppo = publishPollObjectList.Last();
                        ////ppo.resolutionList.Add(
                        ////   new IDataModel.Questions
                        ////   {
                        ////       Sequence = o.Sequence,
                        ////       resolutionDescription = o.QuestionDescription

                        ////   });
                    }
                }
            }

            return publishPollObjectList;
        }

        /// <summary>
        /// Update poll when publish checked.
        /// </summary>
        /// <param name="id">vote id</param>
        /// <param name="val">selected value</param>
        public void SaveCheckboxPublished(int id, bool val)
        {
            var data = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == id);
            data.IsPublished = val;
            this.dataContext.SaveChanges();
            ////notify all active users vote is published
            this.SendPublishPollNotification(id);
        }

        /// <summary>
        /// Update vote when checked open.
        /// </summary>
        /// <param name="id">vote id</param>
        /// <param name="val">selected value</param>
        public void SaveCheckboxOpen(int id, bool val)
        {
            var data = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == id);
            if (val == true)
            {
                data.StatusID = this.dataContext.Poll_Status.FirstOrDefault(x => x.StatusDescription == "Open").StatusID;
                this.dataContext.SaveChanges();
                ////notify active users vote is open
                this.SendOpenPollNotification(id);
            }
        }

        /// <summary>
        /// Publish results.
        /// </summary>
        /// <param name="pollId">poll id</param>
        /// <returns>flag true/false</returns>
        public bool SendPublishResultNotification(int pollId)
        {
            bool success = false;
            StringBuilder mailBody = new StringBuilder();
            string strEmailSubject = Amdox.Common.Common.EmailText.PublishVoteOutcomeSubject;
            string strEmailBody = Amdox.Common.Common.EmailText.PublishVoteOutcomeEmailBody;

            var pollDetail = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollId);
            strEmailSubject = string.Format(strEmailSubject, pollDetail.PollTitle);
            strEmailBody = string.Format(strEmailBody, this.applicationPath);

            var userList = from a in this.dataContext.Poll_Users
                           join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                           where b.IsDeleted == false &&
                           b.IsEmailNotificationRequired == true &&
                           a.PollId == pollId
                           select b;

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            foreach (var pollUser in userList)
            {
                List<string> toEmailList = new List<string>();
                toEmailList.Add(pollUser.EmailAddress);
                success = Amdox.Common.EmailService.SendEmail(null, strFromEmailID, null, strEmailSubject, strEmailBody, toEmailList);
            }

            return success;
        }

        /// <summary>
        /// Send partner notification.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <param name="voterUserID">vote user id</param>
        /// <returns>flag true/false</returns>
        public bool SendPartnerReminderNotification(int pollID, int voterUserID)
        {
            bool success = false;
            StringBuilder mailBody = new StringBuilder();
            string strEmailSubject = Amdox.Common.Common.EmailText.PartnerReminderSubject;
            string strEmailBody = Amdox.Common.Common.EmailText.PartnerReminderBody;

            var pollDetail = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollID);
            strEmailSubject = string.Format(strEmailSubject, pollDetail.PollTitle);
            strEmailBody = string.Format(strEmailBody, this.applicationPath, Convert.ToDateTime(pollDetail.EndDate).ToShortDateString());

            ////get the users list whose voting is incomplete.
            var objUsers = (from a in this.dataContext.UserDetails
                            where a.IsDeleted == false &&
                            a.IsEmailNotificationRequired == true &&
                            a.UserID == voterUserID
                            select a).ToList();

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            List<string> toEmailList = new List<string>();
            foreach (var pollUser in objUsers)
            {
                toEmailList.Add(pollUser.EmailAddress);
            }

            int proxyID = Convert.ToInt32(this.objUsermanager.GetProxy(voterUserID));

            var objProxyUsers = (from a in this.dataContext.UserDetails
                                 where a.IsDeleted == false &&
                                 a.IsEmailNotificationRequired == true &&
                                 a.UserID == proxyID
                                 select a).ToList();
            foreach (var pollUser in objProxyUsers)
            {
                toEmailList.Add(pollUser.EmailAddress);
            }

            success = Amdox.Common.EmailService.SendEmail(null, strFromEmailID, null, strEmailSubject, strEmailBody, toEmailList);
            return success;
        }

        /// <summary>
        /// Update vote when checked closed.
        /// </summary>
        /// <param name="pollid">vote id</param>
        /// <param name="val">selected value</param>
        public void SaveCheckboxClosed(int pollid, bool val)
        {
            var data = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollid);
            var userList = (from a in this.dataContext.Poll_Users
                            join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                            join c in this.dataContext.Users on a.UserID equals c.UserID
                            where b.IsDeleted == false && a.PollId == pollid && c.IsEnabled == true && c.IsAllowedToVote == true && b.PartnerTypeID != null
                            select new
                            {
                                b.PartnerTypeID,
                                a.IsCompleted
                            }).ToList();

            int totalVotesExpected = userList.Count;
            int totalVotesSubmitted = 0;
            int totalDidNotVote = 0;
            int totalWeightedVotesExpected = 0;
            int totalWeightedVotedSubmitted = 0;
            int totalWeightedDidNotVote = 0;

            foreach (var u in userList)
            {
                if (u.IsCompleted == true)
                {
                    totalVotesSubmitted += 1;
                }
                else
                {
                    totalDidNotVote += 1;
                }

                if (u.PartnerTypeID == 1)
                {
                    totalWeightedVotesExpected += 2;
                    if (u.IsCompleted == true)
                    {
                        totalWeightedVotedSubmitted += 2;
                    }
                    else
                    {
                        totalWeightedDidNotVote += 2;
                    }
                }
                else if (u.PartnerTypeID == 2)
                {
                    totalWeightedVotesExpected += 1;
                    if (u.IsCompleted == true)
                    {
                        totalWeightedVotedSubmitted += 1;
                    }
                    else
                    {
                        totalWeightedDidNotVote += 1;
                    }
                }
            }

            data.ActualVotesSubmitted = totalVotesSubmitted;
            data.ActualWeightedVotesSubmitted = totalWeightedVotedSubmitted;
            data.TotalDidNotVote = totalDidNotVote;
            data.TotalVotesExpected = totalVotesExpected;
            data.TotalWeightedDidNotVote = totalWeightedDidNotVote;
            data.TotalWeightedVotesExpected = totalWeightedVotesExpected;
            data.TotalDidNotVotePercentage = Math.Round((Convert.ToDecimal(totalDidNotVote) / (Convert.ToDecimal(totalVotesExpected)) * 100), 2);
            data.TotalWeightedDidNotVotePercentage = Math.Round((Convert.ToDecimal(totalWeightedDidNotVote) / (Convert.ToDecimal(totalWeightedVotesExpected)) * 100), 2);
            this.dataContext.SaveChanges();

            if (val == true)
            {
                data.StatusID = this.dataContext.Poll_Status.FirstOrDefault(x => x.StatusDescription == "Close").StatusID;
                this.dataContext.SaveChanges();
            }

            var questionsForVote = this.GetVoteQuestions(pollid);

            foreach (var q in questionsForVote)
            {
                var optns = this.GetOptions();

                foreach (var o in optns)
                {
                    var optDetails = this.GetOptionStats(q.QuestionId, o.Value, totalVotesExpected, totalWeightedVotesExpected, pollid);

                    var pollstatobjecttemp = this.dataContext.Poll_Statistics.FirstOrDefault(x => x.PollID == pollid && x.OptionID == o.Value && x.QuestionID == q.QuestionId);
                    var pollstatobject = new Amdox.DataModel.Poll_Statistics();
                    if (pollstatobjecttemp != null)
                    {
                        pollstatobject = pollstatobjecttemp;
                    }

                    pollstatobject.OptionID = o.Value;
                    pollstatobject.PollID = pollid;
                    pollstatobject.QuestionID = q.QuestionId;
                    pollstatobject.TotalVotes = optDetails.simpleCount;
                    pollstatobject.TotalVotesPercentage = optDetails.simplePercentage;
                    pollstatobject.TotalWeighedVotes = optDetails.weightedCount;
                    pollstatobject.TotalWeighedVotesPercentage = optDetails.weightedPercentage;

                    if (pollstatobjecttemp == null)
                    {
                        this.dataContext.Poll_Statistics.Add(pollstatobject);
                    }
                    else
                    {
                        this.dataContext.SaveChanges();
                    }

                    if (o.Value == 1)
                    {
                        var question = this.dataContext.Poll_Questions.FirstOrDefault(x => x.QuestionID == q.QuestionId);
                        if (pollstatobject.TotalWeighedVotesPercentage > data.VoteTypePercentage.GetValueOrDefault())
                        {
                            question.ResultID = 1;
                        }
                        else
                        {
                            question.ResultID = 2;
                        }
                    }
                }
            }

            this.dataContext.SaveChanges();
        }

        /// <summary>
        /// Will get the votes stats.
        /// </summary>
        /// <param name="questionid">question id</param>
        /// <param name="optionid">option id</param>
        /// <param name="totalVotes">total votes</param>
        /// <param name="totalWeightedVotes">total weighted votes</param>
        /// <param name="pollid">poll id</param>
        /// <returns>returns Option Stats</returns>
        private IOptionStats GetOptionStats(int questionid, int optionid, int totalVotes, int totalWeightedVotes, int pollid)
        {
            var optionstats = (from PT in this.dataContext.Poll_Transactions
                               join PU in this.dataContext.Poll_Users on PT.UserID equals PU.UserID
                               join U in this.dataContext.Users on PT.UserID equals U.UserID
                               join UD in this.dataContext.UserDetails on U.UserID equals UD.UserID
                               where PT.isDeleted == false && U.IsAllowedToVote == true &&
                               PT.OptionID == optionid && PT.QuestionID == questionid && PU.PollId == pollid
                               && UD.IsDeleted == false
                               select new
                               {
                                   UD.PartnerTypeID
                               }).ToList();

            var optionStatsObject = this.kernel.Get<IOptionStats>();
            optionStatsObject.simpleCount = optionStatsObject.weightedCount = 0;

            foreach (var o in optionstats)
            {
                optionStatsObject.simpleCount += 1;
                if (o.PartnerTypeID == 1)
                {
                    optionStatsObject.weightedCount += 2;
                }
                else
                {
                    optionStatsObject.weightedCount += 1;
                }
            }

            optionStatsObject.simplePercentage = Math.Round(((Convert.ToDecimal(optionStatsObject.simpleCount)) / (Convert.ToDecimal(totalVotes)) * 100), 2);
            optionStatsObject.weightedPercentage = Math.Round(((Convert.ToDecimal(optionStatsObject.weightedCount)) / (Convert.ToDecimal(totalWeightedVotes)) * 100), 2);

            return optionStatsObject;
        }

        /// <summary>
        /// Get partner vote
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Partner vote view model</returns>
        public IPartnerVoteObject GetPartnerVote(int id)
        {
            try
            {
                var objUserDetails = (from PM in this.dataContext.Poll_Maintenance
                                      join PQ in this.dataContext.Poll_Questions on PM.PollId equals PQ.PollID
                                      join PS in this.dataContext.Poll_Status on PM.StatusID equals PS.StatusID
                                      where PM.IsPublished == true && PM.PollId == id
                                      && (PS.StatusDescription == "Open" || PS.StatusDescription == "Close") && PQ.IsEnabled == true
                                      select new
                                      {
                                          PM.PollId,
                                          PM.PollTitle,
                                          PM.PollLongDescription,
                                          PM.StartDate,
                                          PM.EndDate,
                                          PM.StatusID,
                                          PM.IsPublished,
                                          PQ.QuestionDescription,
                                          PQ.Sequence,
                                          PM.CreatedDate,
                                          PS.StatusDescription
                                      }).OrderByDescending(x => x.CreatedDate).ToList();

                if (objUserDetails.Count() > 0)
                {
                    int pollid = 0;
                    List<IPartnerVoteObject> partnerVoteObjectList = new List<IPartnerVoteObject>();
                    foreach (var o in objUserDetails)
                    {
                        if (pollid != o.PollId)
                        {
                            pollid = o.PollId;
                            IPartnerVoteObject ppo = new PartnerVoteObject();
                            ppo.resolutionList = new List<IDataModel.Questions>();
                            partnerVoteObjectList.Add(ppo);
                            ppo.startDate = o.StartDate;
                            ppo.endDate = o.EndDate;
                            ppo.voteNumber = o.PollId;
                            ppo.partnerVoteDescription = o.PollLongDescription;

                            if (o.StatusDescription == "Open")
                            {
                                ppo.PartnerVoteStatus = "Open";
                                ppo.isOpen = true;
                            }
                            else if (o.StatusDescription == "Close")
                            {
                                ppo.PartnerVoteStatus = "Closed";
                            }
                            else
                            {
                                ppo.PartnerVoteStatus = "Published";
                            }

                            ppo.partnerVoteTitle = o.PollTitle;

                            var docdetails = this.dataContext.Poll_Documents.FirstOrDefault(x => x.PollID == pollid);
                            if (docdetails != null)
                            {
                                ppo.Document = docdetails.DocumentURL;
                            }

                            ppo.resolutionList.Add(
                                new IDataModel.Questions
                                {
                                    Sequence = o.Sequence,
                                    resolutionDescription = o.QuestionDescription
                                });
                        }
                        else
                        {
                            var ppo = partnerVoteObjectList.Last();
                            ppo.resolutionList.Add(
                               new IDataModel.Questions
                               {
                                   Sequence = o.Sequence,
                                   resolutionDescription = o.QuestionDescription

                               });
                        }
                    }
                    return partnerVoteObjectList.First();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get active partners for vote.
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>list of IPartnerVoteObject</returns>
        public List<IPartnerVoteObject> GetActivePartnerVotes(int userid)
        {
            var objUserDetails = (from PM in this.dataContext.Poll_Maintenance
                                  join PQ in this.dataContext.Poll_Questions on PM.PollId equals PQ.PollID
                                  join PU in this.dataContext.Poll_Users on PM.PollId equals PU.PollId
                                  join PS in this.dataContext.Poll_Status on PM.StatusID equals PS.StatusID
                                  where PM.IsPublished == true && PU.UserID == userid && PQ.IsEnabled == true
                                  && (PS.StatusDescription == "Open" || PS.StatusDescription == "Not Open")
                                  select new
                                  {
                                      PM.PollId,
                                      PM.PollTitle,
                                      PM.StartDate,
                                      PM.EndDate,
                                      PM.StatusID,
                                      PM.IsPublished,
                                      PQ.QuestionDescription,
                                      PQ.Sequence,
                                      PM.CreatedDate,
                                      PU.IsCompleted,
                                      PU.VotingTypeID,
                                      PQ.QuestionID,
                                      PM.PollType,
                                      PS.StatusDescription
                                  }).OrderByDescending(x => x.CreatedDate).ToList();
            List<IPartnerVoteObject> partnerVoteObjectList = new List<IPartnerVoteObject>();
            if (objUserDetails.Count > 0)
            {
                int pollid = 0;

                foreach (var o in objUserDetails)
                {
                    if (pollid != o.PollId)
                    {
                        pollid = o.PollId;
                        IPartnerVoteObject ppo = new PartnerVoteObject();
                        ppo.resolutionList = new List<IDataModel.Questions>();
                        partnerVoteObjectList.Add(ppo);
                        ppo.startDate = o.StartDate;
                        ppo.endDate = o.EndDate;
                        ppo.voteNumber = o.PollId;

                        if (o.PollType == 1)
                        {
                            ppo.PartnerVoteType = "Ordinary";
                        }
                        else
                        {
                            ppo.PartnerVoteType = "Special";
                        }

                        if (o.IsCompleted == true)
                        {
                            var lastVotedetails = this.dataContext.Poll_Transactions.FirstOrDefault(x => x.QuestionID == o.QuestionID && x.UserID == userid && x.isDeleted == false);
                            ppo.LastChanged = lastVotedetails.VotingDate.GetValueOrDefault();
                        }
                        else
                        {
                            ppo.LastChanged = null;
                        }

                        if (o.StatusDescription == "Open")
                        {
                            ppo.PartnerVoteStatus = "Open";
                            ppo.isOpen = true;
                        }
                        else if (o.StatusDescription == "Close")
                        {
                            ppo.PartnerVoteStatus = "Closed";
                        }
                        else
                        {
                            ppo.PartnerVoteStatus = "Published";
                        }

                        ppo.partnerVoteTitle = o.PollTitle;
                        ppo.isCompleted = o.IsCompleted.GetValueOrDefault();

                        if (o.VotingTypeID == 1)
                        {
                            ppo.VotingType = "Self";
                        }
                        else if (o.VotingTypeID == 2)
                        {
                            var prxy = this.dataContext.Poll_Transactions.FirstOrDefault(x => x.UserID == userid && x.isDeleted == false && x.PollID == o.PollId);
                            if (prxy != null)
                            {
                                if (prxy.ProxyUserID != null)
                                {
                                    var proxydetails = this.objUsermanager.GetUserInfo(prxy.ProxyUserID.GetValueOrDefault());
                                    if (proxydetails != null)
                                    {
                                        ppo.VotingType = "Proxy (" + proxydetails.Initials + ")";
                                    }
                                }
                            }
                        }
                        else
                        {
                            ppo.VotingType = "None";
                        }

                        ppo.resolutionList.Add(
                            new IDataModel.Questions
                            {
                                Sequence = o.Sequence,
                                resolutionDescription = o.QuestionDescription
                            });
                    }
                    else
                    {
                        var ppo = partnerVoteObjectList.Last();
                        ppo.resolutionList.Add(
                           new IDataModel.Questions
                           {
                               Sequence = o.Sequence,
                               resolutionDescription = o.QuestionDescription
                           });
                    }
                }
            }

            return partnerVoteObjectList;
        }

        /// <summary>
        /// Save vote.
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="pollid">poll id</param>
        /// <param name="options">all options</param>
        /// <param name="proxyid">proxy id</param>
        /// <param name="votingtypeid">vote type id</param>
        public void SaveVote(int userid, int pollid, List<int> options, int? proxyid, int votingtypeid)
        {

            var objVoteDetails = (from PM in this.dataContext.Poll_Maintenance
                                  join PQ in this.dataContext.Poll_Questions on PM.PollId equals PQ.PollID
                                  join PT in this.dataContext.Poll_Transactions on PQ.QuestionID equals PT.QuestionID
                                  where PM.PollId == pollid && PT.UserID == userid && PT.isDeleted == false && PQ.IsEnabled == true

                                  select new
                                  {
                                      PT.PTID,
                                      PQ.QuestionID
                                  }).ToList();

            if (objVoteDetails.Count > 0)
            {
                foreach (var a in objVoteDetails)
                {
                    var voteData = this.dataContext.Poll_Transactions.FirstOrDefault(x => x.PTID == a.PTID);
                    voteData.isDeleted = true;
                }
            }

            var objnewVoteDetails = (from PM in this.dataContext.Poll_Maintenance
                                     join PQ in this.dataContext.Poll_Questions on PM.PollId equals PQ.PollID
                                     where PM.PollId == pollid && PQ.IsEnabled == true
                                     select new
                                     {
                                         PQ.QuestionID,
                                         PQ.Sequence,
                                         PM.PollId
                                     }).OrderBy(x => x.Sequence).ToList();

            if (objnewVoteDetails.Count > 0)
            {
                foreach (var a in objnewVoteDetails)
                {
                    var trans = new Amdox.DataModel.Poll_Transactions();
                    trans.isDeleted = false;
                    trans.QuestionID = a.QuestionID;
                    trans.OptionID = options[(a.Sequence.GetValueOrDefault() - 1)];
                    trans.UserID = userid;
                    trans.ProxyUserID = proxyid;
                    trans.PollID = a.PollId;
                    trans.VotingDate = DateTime.Now;
                    this.dataContext.Poll_Transactions.Add(trans);
                }
            }

            var objPollUserDetails = (from PM in this.dataContext.Poll_Maintenance
                                      join PU in this.dataContext.Poll_Users on PM.PollId equals PU.PollId
                                      where PM.PollId == pollid && PU.UserID == userid
                                      select new
                                      {
                                          PU.PollUserID
                                      }).ToList();

            if (objPollUserDetails.Count > 0)
            {
                int polluseridtemp = objPollUserDetails[0].PollUserID;
                var polluser = this.dataContext.Poll_Users.FirstOrDefault(x => x.PollUserID == polluseridtemp);
                polluser.VotingTypeID = votingtypeid;
                polluser.IsCompleted = true;
            }

            var pollobject = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollid);
            var userobject = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == userid && x.IsDeleted == false);

            pollobject.TotalDidNotVote -= 1;
            pollobject.ActualVotesSubmitted += 1;

            if (userobject.PartnerTypeID == 1)
            {
                pollobject.TotalWeightedDidNotVote -= 2;
                pollobject.ActualWeightedVotesSubmitted += 2;
            }
            else
            {
                pollobject.TotalWeightedDidNotVote -= 1;
                pollobject.ActualWeightedVotesSubmitted += 1;
            }

            pollobject.TotalDidNotVotePercentage = (Convert.ToDecimal(pollobject.TotalDidNotVote.GetValueOrDefault())) / (Convert.ToDecimal(pollobject.TotalVotesExpected.GetValueOrDefault())) * 100;
            pollobject.TotalWeightedDidNotVotePercentage = (Convert.ToDecimal(pollobject.TotalWeightedDidNotVote.GetValueOrDefault())) / (Convert.ToDecimal(pollobject.TotalWeightedVotesExpected.GetValueOrDefault())) * 100;
            this.dataContext.SaveChanges();

            if (proxyid != null)
            {
                ////email to proxySendProxyCastVoteAlertToPartner(int proxyID, int userID, int pollID)
                this.objUsermanager.SendProxyCastVoteAlertToPartner(proxyid, userid, pollid);
            }
        }

        /// <summary>
        /// Update poll document.
        /// </summary>
        /// <param name="documentID">document id</param>
        /// <returns>flag true/false</returns>
        public bool UpdatePollDocuments(int documentID)
        {
            bool status = false;

            var objPollDocs = this.dataContext.Poll_Documents.Find(documentID);
            if (objPollDocs != null)
            {
                objPollDocs.IsEnabled = false;
                this.dataContext.SaveChanges();
                status = true;
            }

            return status;
        }

        /// <summary>
        /// GEt old doc details for vote
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="pollid">poll id</param>
        /// <returns>old vote id</returns>
        public List<int> GetOldVoteDetails(int userid, int pollid)
        {
            var objPollUserDetails = (from PM in this.dataContext.Poll_Maintenance
                                      join PU in this.dataContext.Poll_Users on PM.PollId equals PU.PollId
                                      join PQ in this.dataContext.Poll_Questions on PM.PollId equals PQ.PollID
                                      join PT in this.dataContext.Poll_Transactions on PQ.QuestionID equals PT.QuestionID
                                      where PM.PollId == pollid && PU.UserID == userid && PT.isDeleted == false && PT.UserID == userid && PQ.IsEnabled == true
                                      select new
                                      {
                                          PQ.Sequence,
                                          PT.OptionID
                                      }).OrderBy(x => x.Sequence).ToList();

            List<int> Answers = new List<int>();

            if (objPollUserDetails.Count > 0)
            {
                foreach (var o in objPollUserDetails)
                {
                    Answers.Add(o.OptionID.GetValueOrDefault());
                }
                return Answers;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get vote result.
        /// </summary>
        /// <returns>result view model</returns>
        public IVoteResultsVM GetIVoteResultsVM()
        {
            ////var closedPollList = this.dataContext.Poll_Maintenance.Where(x => (x.StatusID == 2 || x.StatusID == 4) && x.IsVisible == true).ToList();
            var closedPollList = from pm in this.dataContext.Poll_Maintenance
                                 join ps in this.dataContext.Poll_Status on pm.StatusID equals ps.StatusID
                                 where (ps.StatusDescription == "Close" || ps.StatusDescription == "Accepted")
                                 select new { pm, ps };

            foreach (var p in closedPollList)
            {
                var vote = this.kernel.Get<IVoteResultsDataVM>();
                vote.voteNumber = p.pm.PollId;
                vote.voteTitle = p.pm.PollTitle;
                if (p.pm.PollType == 1)
                {
                    vote.voteType = "Ordinary";
                }
                else
                {
                    vote.voteType = "Special";
                }
                vote.voteDescription = p.pm.PollLongDescription;
                vote.resolutions = this.GetVoteResolutions(vote.voteNumber);

                foreach (var r in vote.resolutions)
                {
                    r.NoVoteSimpleCount = p.pm.TotalDidNotVote.GetValueOrDefault();
                    r.NoVoteSimplePercentage = p.pm.TotalDidNotVotePercentage.GetValueOrDefault();
                    r.NoVoteWeightedCount = p.pm.TotalWeightedDidNotVote.GetValueOrDefault();
                    r.NoVoteWeightedPercentage = p.pm.TotalWeightedDidNotVotePercentage.GetValueOrDefault();
                }

                if (p.ps.StatusDescription == "Accepted")
                {
                    vote.isAccepted = true;
                }
                else
                {
                    vote.isAccepted = false;
                }

                this.voteresultsVM.VoteResultList.Add(vote);
            }

            return this.voteresultsVM;
        }

        /// <summary>
        /// Get resolution count.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>count of resolution</returns>
        public int GetVoteResolutionCount(int pollID)
        {
            var objResolutions = this.dataContext.Poll_Questions.Where(x => x.PollID == pollID && x.IsEnabled == true).ToList();
            if (objResolutions != null && objResolutions.Count() > 0)
            {
                return objResolutions.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get vote resolution
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <returns>return question stat view model</returns>
        public List<IQuestionStatisticsVM> GetVoteResolutions(int pollid)
        {
            var questions = this.dataContext.Poll_Questions.Where(x => x.PollID == pollid && x.IsEnabled == true).OrderBy(x => x.Sequence).ToList();

            List<IQuestionStatisticsVM> qlist = new List<IQuestionStatisticsVM>();

            foreach (var q in questions)
            {
                var qobject = this.kernel.Get<IQuestionStatisticsVM>();
                qobject.Sequence = q.Sequence.GetValueOrDefault();
                if (q.ResultID == 1)
                {
                    qobject.Outcome = "Approved";
                }
                else
                {
                    qobject.Outcome = "Not Approved";
                }

                qobject.Description = q.QuestionDescription;
                var results = this.dataContext.Poll_Statistics.Where(x => x.QuestionID == q.QuestionID).ToList();
                foreach (var r in results)
                {
                    if (r.OptionID == 1)
                    {
                        qobject.VoteForSimpleCount = r.TotalVotes.GetValueOrDefault();
                        qobject.VoteForSimplePercentage = r.TotalVotesPercentage.GetValueOrDefault();
                        qobject.VoteForWeightedCount = r.TotalWeighedVotes.GetValueOrDefault();
                        qobject.VoteForWeightedPercentage = r.TotalWeighedVotesPercentage.GetValueOrDefault();
                    }
                    else if (r.OptionID == 2)
                    {
                        qobject.VoteAgainstSimpleCount = r.TotalVotes.GetValueOrDefault();
                        qobject.VoteAgainstSimplePercentage = r.TotalVotesPercentage.GetValueOrDefault();
                        qobject.VoteAgainstWeightedCount = r.TotalWeighedVotes.GetValueOrDefault();
                        qobject.VoteAgainstWeightedPercentage = r.TotalWeighedVotesPercentage.GetValueOrDefault();
                    }
                    else if (r.OptionID == 3)
                    {
                        qobject.AbstainSimpleCount = r.TotalVotes.GetValueOrDefault();
                        qobject.AbstainSimplePercentage = r.TotalVotesPercentage.GetValueOrDefault();
                        qobject.AbstainWeightedCount = r.TotalWeighedVotes.GetValueOrDefault();
                        qobject.AbstainWeightedPercentage = r.TotalWeighedVotesPercentage.GetValueOrDefault();
                    }
                }

                qlist.Add(qobject);
            }

            return qlist;
        }

        /// <summary>
        /// Set question approval.
        /// </summary>
        /// <param name="questionid">question id</param>
        /// <param name="value">selected value</param>
        public void SetQuestionApproval(int questionid, bool value)
        {
            var question = this.dataContext.Poll_Questions.FirstOrDefault(x => x.QuestionID == questionid);

            if (value == true)
            {
                question.ResultID = 1;
            }
            else
            {
                question.ResultID = 2;
            }

            this.dataContext.SaveChanges();
        }

        /// <summary>
        /// Accept a poll result.
        /// </summary>
        /// <param name="pollid">poll id</param>
        public void AcceptResult(int pollid)
        {
            var poll = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollid);
            poll.StatusID = this.dataContext.Poll_Status.FirstOrDefault(x => x.StatusDescription == "Accepted").StatusID;
            this.dataContext.SaveChanges();
            ////email code to inform all partners about vote outcome
            this.SendPublishResultNotification(pollid);
        }

        /// <summary>
        /// Get vote results for partners.
        /// </summary>
        /// <returns>vote result view model</returns>
        public IVoteResultsVM GetIVoteResultsVMForPartner()
        {
            var closedPollList = this.dataContext.Poll_Maintenance.
                Join(this.dataContext.Poll_Status,
                pm => pm.StatusID,
                ps => ps.StatusID,
                (pm, ps) => new { pm, ps }).
                Where(x => (x.ps.StatusDescription == "Accepted") && x.pm.IsVisible == true).ToList();

            foreach (var p in closedPollList)
            {
                var vote = this.kernel.Get<IVoteResultsDataVM>();
                vote.voteNumber = p.pm.PollId;
                vote.voteTitle = p.pm.PollTitle;
                if (p.pm.PollType == 1)
                {
                    vote.voteType = "Ordinary";
                }
                else
                {
                    vote.voteType = "Special";
                }
                vote.voteDescription = p.pm.PollLongDescription;
                vote.resolutions = GetVoteResolutions(vote.voteNumber);

                foreach (var r in vote.resolutions)
                {
                    r.NoVoteSimpleCount = p.pm.TotalDidNotVote.GetValueOrDefault();
                    r.NoVoteSimplePercentage = p.pm.TotalDidNotVotePercentage.GetValueOrDefault();
                    r.NoVoteWeightedCount = p.pm.TotalWeightedDidNotVote.GetValueOrDefault();
                    r.NoVoteWeightedPercentage = p.pm.TotalWeightedDidNotVotePercentage.GetValueOrDefault();
                }

                if (p.ps.StatusDescription == "Accepted")
                {
                    vote.isAccepted = true;
                }
                else
                {
                    vote.isAccepted = false;
                }

                this.voteresultsVM.VoteResultList.Add(vote);
            }

            return this.voteresultsVM;
        }

        /// <summary>
        /// Get poll users.
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <returns>voter view model</returns>
        public List<IVoterVM> GetPollUsers(int pollid)
        {
            List<IVoterVM> userlist = new List<IVoterVM>();
            var objGetUserIds = (from u in this.dataContext.Users
                                 join ud in this.dataContext.UserDetails on u.UserID equals ud.UserID
                                 join pu in this.dataContext.Poll_Users on u.UserID equals pu.UserID
                                 where u.IsEnabled == true &&
                                 u.IsAllowedToVote == true &&
                                 ud.IsDeleted == false &&
                                 ud.PartnerTypeID != null &&
                                 pu.PollId == pollid
                                 select new
                                 {
                                     u.UserID,
                                     pu.IsCompleted
                                 }).ToList();

            foreach (var a in objGetUserIds)
            {
                var person = this.objUsermanager.GetUserInfo(a.UserID);
                var voter = this.kernel.Get<IVoterVM>();
                ////voter.UserID = a.UserID;
                ////voter.VoteNo = pollid;
                voter.Name = person.Title + " " + person.FirstName + " " + person.LastName + " (" + person.Initials + ")";
                voter.Grade = person.Grade;
                voter.Proxy = person.proxy;
                voter.voterID = a.UserID;

                if (a.IsCompleted == true)
                {
                    voter.isVoted = "Yes";
                }
                else
                {
                    voter.isVoted = "No";
                }

                userlist.Add(voter);
            }

            return userlist;
        }

        /// <summary>
        /// Get vote status
        /// </summary>
        /// <param name="statusID">status id</param>
        /// <returns>return status</returns>
        public string GetVoteStatus(int statusID)
        {
            var objVoteStatus = this.dataContext.Poll_Status.Find(statusID);
            ////this.dataContext.Poll_Status.Where(x=>x.StatusID==statusID && x.IsEnabled==true).Select(x=>x.StatusDescription).ToList();
            return objVoteStatus.StatusDescription;
        }
        public ITrackVoteDetailsVM GetConfidentialTrackVote(string strUserName)
        {
            string sUserName = Regex.Replace(strUserName, ".*\\\\(.*)", "$1", RegexOptions.None);

            this.trackVoteDetails = this.kernel.Get<ITrackVoteDetailsVM>();

            var objUserType = (from u in this.dataContext.Users
                               join ud in this.dataContext.UserDetails on u.UserID equals ud.UserID
                               where u.IsEnabled == true &&
                               u.UserName.Equals(sUserName) &&
                               ud.IsDeleted == false
                               select ud.UserTypeID).FirstOrDefault();
            List<ITrackVoteStatisticsVM> lstVoteDetails = new List<ITrackVoteStatisticsVM>();
            int iUserTypeID = Convert.ToInt32(objUserType);
            ////visible only to super admin
            if (iUserTypeID == 2)
            {                                                            //-close
                var objVoteDetails = this.dataContext.Poll_Maintenance.
                    Join(this.dataContext.Poll_Status,
                    pm => pm.StatusID,
                    ps => ps.StatusID,
                    (pm, ps) => new { pm, ps }).OrderByDescending(x => x.pm.PollId);

                ////Where(x => x.StatusID == 1 || x.StatusID == 2).OrderByDescending(x => x.PollId);
                foreach (var p in objVoteDetails)
                {
                    this.trackVoteStatistics = this.kernel.Get<ITrackVoteStatisticsVM>();
                    var userList = (from a in this.dataContext.Poll_Users
                                    join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                                    where b.IsDeleted == false
                                    where a.PollId == p.pm.PollId
                                    select new
                                    {
                                        b.PartnerTypeID,
                                        a.IsCompleted
                                    }).ToList();

                    int totalVotesExpected = userList.Count;
                    int totalVotesSubmitted = 0;
                    int TotalDidNotVote = 0;
                    int totalWeightedVotesExpected = 0;
                    int totalWeightedVotedSubmitted = 0;
                    int totalWeightedDidNotVote = 0;

                    foreach (var u in userList)
                    {
                        if (u.IsCompleted == true)
                        {
                            totalVotesSubmitted += 1;
                        }
                        else
                        {
                            TotalDidNotVote += 1;
                        }
                        #region Partner type A
                        if (u.PartnerTypeID == 1)
                        {
                            totalWeightedVotesExpected += 2;
                            if (u.IsCompleted == true)
                            {
                                totalWeightedVotedSubmitted += 2;
                            }
                            else
                            {
                                totalWeightedDidNotVote += 2;
                            }

                        }
                        #endregion

                        #region Partner Type B
                        else if (u.PartnerTypeID == 2)
                        {
                            totalWeightedVotesExpected += 1;
                            if (u.IsCompleted == true)
                            {
                                totalWeightedVotedSubmitted += 1;
                            }
                            else
                            {
                                totalWeightedDidNotVote += 1;
                            }
                        }
                        #endregion

                    }

                    this.trackVoteStatistics.iVoteNumber = p.pm.PollId;
                    this.trackVoteStatistics.strVoteTitle = p.pm.PollTitle;
                    this.trackVoteStatistics.dtStartDate = p.pm.StartDate.GetValueOrDefault();
                    this.trackVoteStatistics.dteEndDate = p.pm.EndDate.GetValueOrDefault();
                    this.trackVoteStatistics.iVotesCastCount = totalVotesSubmitted;
                    this.trackVoteStatistics.dVotesCastPerc = Math.Round((Convert.ToDecimal(totalVotesSubmitted) / Convert.ToDecimal(totalVotesExpected) * 100), 2);
                    this.trackVoteStatistics.iOutstandingCount = TotalDidNotVote;
                    this.trackVoteStatistics.dOutstandingPerc = Math.Round((Convert.ToDecimal(TotalDidNotVote) / Convert.ToDecimal(totalVotesExpected) * 100), 2);
                    this.trackVoteStatistics.strvoteStatus = GetVoteStatus(Convert.ToInt32(p.pm.StatusID));
                    this.trackVoteStatistics.lstResolutions = GetVoteQuestions(p.pm.PollId);

                    lstVoteDetails.Add(this.trackVoteStatistics);
                }

            }

            this.trackVoteDetails.VoteDetails = lstVoteDetails;
            return this.trackVoteDetails;
        }

        /// <summary>
        /// Get confidential vote details
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>IVoteConfidentialInfo view model</returns>
        public List<IVoteConfidentialInfoVM> GetConfidentialVoteDetails(int pollID)
        {
            List<IVoteConfidentialInfoVM> lstConfidentialVoteDetails = new List<IVoteConfidentialInfoVM>();
            var objUserVotes = (from pu in this.dataContext.Poll_Users
                                join u in this.dataContext.Users on pu.UserID equals u.UserID
                                where u.IsAllowedToVote == true &&
                                u.IsEnabled == true &&
                                 pu.PollId == pollID
                                select pu).ToList();

            var objVoteDetails = (from pq in this.dataContext.Poll_Questions
                                  where pq.PollID == pollID &&
                                  pq.IsEnabled == true
                                  select pq).ToList();

            foreach (var uv in objUserVotes)//users pollusers6
            {
                foreach (var vd in objVoteDetails)//questions pollquestions1-2
                {

                    var objPartnerDetails = this.objUsermanager.GetUserInfo(Convert.ToInt32(uv.UserID));
                    var voter = this.kernel.Get<IVoteConfidentialInfoVM>();
                    voter.strPartnerName = objPartnerDetails.Title + " " + objPartnerDetails.FirstName + " " + objPartnerDetails.LastName + " (" + objPartnerDetails.Initials + ")";
                    voter.strPartnerType = objPartnerDetails.Grade;
                    voter.strProxyName = objPartnerDetails.proxy != null ? objPartnerDetails.proxy : "--";

                    var objResolution = this.dataContext.Poll_Questions.Find(vd.QuestionID);
                    var objPollTransaction = (from pt in this.dataContext.Poll_Transactions
                                              where pt.isDeleted == false &&
                                              pt.QuestionID == vd.QuestionID &&
                                              pt.UserID == uv.UserID
                                              select pt).ToList();

                    var objPollTransactions = this.dataContext.Poll_Transactions.Where(x => x.QuestionID == vd.QuestionID && x.isDeleted == false && x.UserID == uv.UserID).Select(x => x.OptionID);

                    var objOption = this.dataContext.Poll_Options.Find(objPollTransactions.FirstOrDefault());

                    if (uv.IsCompleted == true)
                    {
                        voter.strIsVoted = "Yes";
                        voter.strResolutionDescription = objResolution.QuestionDescription;
                        voter.strOptionDescription = objOption.OptionName;
                    }
                    else
                    {
                        voter.strIsVoted = "No";
                        voter.strResolutionDescription = objResolution.QuestionDescription;
                        voter.strOptionDescription = objOption == null ? "--" : objOption.OptionName;
                    }

                    lstConfidentialVoteDetails.Add(voter);
                }
            }
            return lstConfidentialVoteDetails;
        }

        /// <summary>
        /// Get track voting.
        /// </summary>
        /// <returns>track vote view model</returns>
        public ITrackVotingVM GetTrackVotingVM()
        {
            var partnervotes = this.dataContext.Poll_Maintenance.
                    Join(this.dataContext.Poll_Status,
                    pm => pm.StatusID,
                    ps => ps.StatusID,
                    (pm, ps) => new { pm, ps }).OrderByDescending(x => x.pm.PollId).OrderByDescending(x => x.pm.PollId);
            //Where(x => x.StatusID == 1).OrderByDescending(x => x.PollId);

            foreach (var p in partnervotes)
            {
                var voteobj = new Amdox.IDataModel.IViewModel.TrackVotingObject();

                var userList = (from a in this.dataContext.Poll_Users
                                join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                                where b.IsDeleted == false
                                where a.PollId == p.pm.PollId
                                select new
                                {
                                    b.PartnerTypeID,
                                    a.IsCompleted
                                }).ToList();

                int totalVotesExpected = userList.Count;
                int totalVotesSubmitted = 0;
                int TotalDidNotVote = 0;
                int totalWeightedVotesExpected = 0;
                int totalWeightedVotedSubmitted = 0;
                int totalWeightedDidNotVote = 0;

                foreach (var u in userList)
                {
                    if (u.IsCompleted == true)
                    {
                        totalVotesSubmitted += 1;
                    }
                    else
                    {
                        TotalDidNotVote += 1;
                    }
                    if (u.PartnerTypeID == 1)
                    {
                        totalWeightedVotesExpected += 2;
                        if (u.IsCompleted == true)
                        {
                            totalWeightedVotedSubmitted += 2;
                        }
                        else
                        {
                            totalWeightedDidNotVote += 2;
                        }

                    }
                    else if (u.PartnerTypeID == 2)
                    {
                        totalWeightedVotesExpected += 1;
                        if (u.IsCompleted == true)
                        {
                            totalWeightedVotedSubmitted += 1;
                        }
                        else
                        {
                            totalWeightedDidNotVote += 1;
                        }
                    }
                }

                voteobj.voteNumber = p.pm.PollId;
                voteobj.voteTitle = p.pm.PollTitle;
                voteobj.startDate = p.pm.StartDate.GetValueOrDefault();
                voteobj.endDate = p.pm.EndDate.GetValueOrDefault();
                voteobj.VotesCastCount = totalVotesSubmitted;
                voteobj.VotesCastPerc = Math.Round((Convert.ToDecimal(totalVotesSubmitted) / Convert.ToDecimal(totalVotesExpected) * 100), 2);
                voteobj.OutstandingCount = TotalDidNotVote;
                voteobj.OutstandingPerc = Math.Round((Convert.ToDecimal(TotalDidNotVote) / Convert.ToDecimal(totalVotesExpected) * 100), 2);

                voteobj.resolutions = GetVoteQuestions(p.pm.PollId);
                this.trackvotingdata.voteData.Add(voteobj);
            }

            return this.trackvotingdata;
        }

        /// <summary>
        /// Get partner vote
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="userName">user name</param>
        /// <returns>Partner vote view model</returns>
        public IPartnervoteVM GetPartnerVoteVM(int? userid, string strUserName)
        {
            IPersonnelObject po = this.objUsermanager.GetUserInfo(userid.GetValueOrDefault()); ;
            var partnervotevmobject = this.kernel.Get<IPartnervoteVM>();
            if (userid == null)
            {
                partnervotevmobject.isValidUser = true;
                userid = this.objUsermanager.GetUserID(strUserName);
            }
            else
            {
                if (po == null)
                {
                    partnervotevmobject.isValidUser = false;
                }
                else
                {
                    partnervotevmobject.isValidUser = true;
                }
            }

            if (partnervotevmobject.isValidUser)
            {
                int myusrid = this.objUsermanager.GetUserID(strUserName).GetValueOrDefault();

                if (userid != myusrid)
                {
                    if (myusrid != this.objUsermanager.GetProxy(userid.GetValueOrDefault()))
                    {
                        partnervotevmobject.isAllowed = false;
                        partnervotevmobject.message = "You are voting for " + po.FirstName + " " + po.LastName;
                    }
                    else
                    {

                        partnervotevmobject.message = "You are voting for " + po.FirstName + " " + po.LastName;
                        partnervotevmobject.isAllowed = true;
                    }
                }
                else
                {
                    partnervotevmobject.isAllowed = true;
                    partnervotevmobject.message = "You are voting for Yourself";
                }
                partnervotevmobject.PartnerVoteList = GetActivePartnerVotes(userid.GetValueOrDefault());
                partnervotevmobject.userid = userid.GetValueOrDefault();
            }
            else
            {
                partnervotevmobject.message = "Invalid User";
            }

            return partnervotevmobject;
        }

        /// <summary>
        /// Get Vote 
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <param name="userid">user id</param>
        /// <param name="isView">is view result</param>
        /// <param name="userName">user name</param>
        /// <returns>Voting view model</returns>
        public IVotingVM GetVotingVM(int? pollid, int? userid, bool? isView, string strUserName)
        {
            var _votingData = this.kernel.Get<IVotingVM>();
            _votingData.isProxy = false;

            var userinfo = this.kernel.Get<IPersonnelObject>();
            if (userid == null)
            {
                userid = this.objUsermanager.GetUserID(strUserName);

                userinfo = this.objUsermanager.GetUserInfo(userid.GetValueOrDefault());
                _votingData.username = userinfo.Title + " " + userinfo.FirstName + " " + userinfo.LastName;
                _votingData.isAllowed = true;
            }
            else
            {
                int myuserid = this.objUsermanager.GetUserID(strUserName).GetValueOrDefault();
                userinfo = this.objUsermanager.GetUserInfo(userid.GetValueOrDefault());

                if (userinfo != null)
                {
                    _votingData.username = userinfo.Title + " " + userinfo.FirstName + " " + userinfo.LastName;
                    _votingData.isValidUser = true;
                    if (myuserid != userid)
                    {
                        int? userproxy = this.objUsermanager.GetProxy(userid.GetValueOrDefault());
                        _votingData.isProxy = true;
                        if (myuserid != userproxy)
                        {
                            _votingData.isAllowed = false;
                        }
                        else
                        {
                            _votingData.isAllowed = true;
                        }
                    }
                    else
                    {
                        _votingData.isAllowed = true;
                    }
                }
                else
                {
                    _votingData.isValidUser = false;
                }
            }

            if (pollid != null)
            {
                IPartnerVoteObject pvo = GetPartnerVote(pollid.GetValueOrDefault());
                if (pvo != null)
                {
                    if (pvo.PartnerVoteStatus == "Open")
                    {
                        _votingData.isOpen = true;
                        _votingData.isView = isView.GetValueOrDefault();
                    }
                    else
                    {
                        _votingData.isOpen = false;
                        _votingData.isView = true;
                    }
                    _votingData.isValidPoll = true;
                    _votingData.voteNumber = pvo.voteNumber;

                    _votingData.DocumentDetails = GetVoteDocuments(pollid.GetValueOrDefault());
                    _votingData.voteTitle = pvo.partnerVoteTitle;
                    _votingData.questionList = pvo.resolutionList;
                    _votingData.voteDescription = pvo.partnerVoteDescription;
                    _votingData.userid = userid.GetValueOrDefault();

                    List<int> oldAns = GetOldVoteDetails(userid.GetValueOrDefault(), pollid.GetValueOrDefault());
                    if (oldAns != null)
                    {

                        _votingData.Answers = oldAns;
                    }
                    else
                    {
                        foreach (var a in pvo.resolutionList)
                        {
                            _votingData.Answers.Add(0);
                        }
                    }
                }
                else
                {
                    _votingData.isValidPoll = false;
                }
            }
            else
            {
                _votingData.isValidPoll = false;
            }

            return _votingData;
        }

        /// <summary>
        /// Get all partner vote
        /// </summary>
        /// <returns>list of Partner Vote Object</returns>
        public List<IPartnerVoteObject> GetAllPartnerVotes()
        {
            var objUserDetails = (from PM in this.dataContext.Poll_Maintenance
                                  join PS in this.dataContext.Poll_Status on PM.StatusID equals PS.StatusID
                                  where PM.IsPublished == true &&
                                  (PS.StatusDescription == "Open" || PS.StatusDescription == "Close")
                                  select new
                                  {
                                      PM.PollId,
                                      PM.PollTitle,
                                      PM.StartDate,
                                      PM.EndDate,
                                      PM.StatusID,
                                      PM.IsPublished,
                                      PM.CreatedDate,
                                      PM.PollType,
                                      PS.StatusDescription
                                  }).OrderByDescending(x => x.CreatedDate).ToList();

            List<IPartnerVoteObject> partnerVoteObjectList = new List<IPartnerVoteObject>();

            if (objUserDetails.Count > 0)
            {
                foreach (var o in objUserDetails)
                {
                    IPartnerVoteObject ppo = new PartnerVoteObject();

                    partnerVoteObjectList.Add(ppo);
                    ppo.startDate = o.StartDate;
                    ppo.endDate = o.EndDate;
                    ppo.voteNumber = o.PollId;

                    if (o.PollType == 1)
                    {
                        ppo.PartnerVoteType = "Ordinary";
                    }
                    else
                    {
                        ppo.PartnerVoteType = "Special";
                    }

                    if (o.StatusDescription == "Open")
                    {
                        ppo.PartnerVoteStatus = "Open";
                        ppo.isOpen = true;
                    }
                    else if (o.StatusDescription == "Close")
                    {
                        ppo.PartnerVoteStatus = "Closed";
                    }
                    else
                    {
                        ppo.PartnerVoteStatus = "Published";
                    }

                    ppo.partnerVoteTitle = o.PollTitle;
                }
            }

            return partnerVoteObjectList;
        }
    }
}
