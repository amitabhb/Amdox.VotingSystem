//-----------------------------------------------------------------------
// <copyright file="ExportDataManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Export Data Manager
/// </summary>
namespace Amdox.BusinessManager
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Amdox.DataModel;
    using Amdox.IBusinessManager;
    using Amdox.IDataModel;
    using Amdox.IDataModel.IViewModel;
    using Ninject;

    /// <summary>
    /// Export data manager
    /// </summary>
    public class ExportDataManager : IExportDataManager
    {
        /// <summary>
        /// DI kernel
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// data context.
        /// </summary>
        [Inject]
        private VoteContext context;

        /// <summary>
        /// user manager
        /// </summary>
        [Inject]
        private IUserManager userManager;

        /// <summary>
        /// Poll manager object.
        /// </summary>
        [Inject]
        private IPollManager pollManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportDataManager" /> class.
        /// </summary>
        /// <param name="dataContext">data context</param>
        /// <param name="userManager">user manager business</param>
        /// <param name="kernel">DI kernel</param>
        /// <param name="pollManager">Poll manager business</param>
        public ExportDataManager(VoteContext dataContext, IUserManager userManager, IKernel kernel, IPollManager pollManager)
        {
            this.context = dataContext;
            this.userManager = userManager;
            this.kernel = kernel;
            this.pollManager = pollManager;
        }

        /// <summary>
        /// Get confidential vote details
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>Data table of vote details</returns>
        public DataTable GetConfidentialVoteDetailsList(int pollID)
        {
            DataTable datatTest = new DataTable();
            var voters = this.pollManager.GetConfidentialVoteDetails(pollID);
            datatTest.Columns.Add("Partner Name");
            datatTest.Columns.Add("Partner Type");
            datatTest.Columns.Add("Proxy Name");
            datatTest.Columns.Add("Resolution");
            datatTest.Columns.Add("Voted");
            datatTest.Columns.Add("Option");

            foreach (var a in voters)
            {
                datatTest.Rows.Add(
                                a.strPartnerName,
                                a.strPartnerType,
                                a.strProxyName,
                                a.strResolutionDescription,
                                a.strIsVoted,
                                a.strOptionDescription);
            }

            return datatTest;
        }

        /// <summary>
        /// Export vote outcome
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>export data in table</returns>
        public DataTable ExportVoteOutcome(int pollID)
        {
            DataTable datatTest = new DataTable();
            var objPollDetails = this.context.Poll_Maintenance.Find(pollID);
            List<IExportVoteOutcomeVM> lstVoteOutcome = new List<IExportVoteOutcomeVM>();
            if (objPollDetails != null)
            {
                var resolutions = this.GetVoteResolutions(pollID);
                foreach (var a in resolutions)
                {
                    var vote = this.kernel.Get<IExportVoteOutcomeVM>();
                    vote.voteNumber = objPollDetails.PollId;
                    vote.voteTitle = objPollDetails.PollTitle;

                    vote.voteType = objPollDetails.PollType == 1 ? "Ordinary" : "Special";
                    vote.voteDescription = objPollDetails.PollLongDescription;
                    vote.resolutionDescription = a.Description;
                    vote.VoteForSimpleCount = a.VoteForSimpleCount;
                    ////vote.VoteForSimplePercentage = a.VoteForSimplePercentage;
                    ////vote.VoteForWeightedCount = a.VoteForWeightedCount;
                    ////vote.VoteForWeightedPercentage = a.VoteForWeightedPercentage;
                    vote.VoteAgainstSimpleCount = a.VoteAgainstSimpleCount;
                    ////vote.VoteAgainstSimplePercentage = a.VoteAgainstSimplePercentage;
                    ////vote.VoteAgainstWeightedCount = a.VoteAgainstWeightedCount;
                    ////vote.VoteAgainstWeightedPercentage = a.VoteAgainstWeightedPercentage;
                    vote.AbstainSimpleCount = a.AbstainSimpleCount;
                    ////vote.AbstainSimplePercentage = a.AbstainSimplePercentage;
                    ////vote.AbstainWeightedCount = a.AbstainWeightedCount;
                    ////vote.AbstainWeightedPercentage = a.AbstainWeightedPercentage;

                    vote.NoVoteSimpleCount = objPollDetails.TotalDidNotVote.GetValueOrDefault();
                    ////vote.NoVoteSimplePercentage = objPollDetails.TotalDidNotVotePercentage.GetValueOrDefault();
                    ////vote.NoVoteWeightedCount = objPollDetails.TotalWeightedDidNotVote.GetValueOrDefault();
                    ////vote.NoVoteWeightedPercentage = objPollDetails.TotalWeightedDidNotVotePercentage.GetValueOrDefault();
                    ////vote.NoVoteSimpleCount = objPollDetails.TotalDidNotVote.GetValueOrDefault();
                    ////vote.NoVoteSimplePercentage = objPollDetails.TotalDidNotVotePercentage.GetValueOrDefault();
                    ////vote.NoVoteWeightedCount = objPollDetails.TotalWeightedDidNotVote.GetValueOrDefault();
                    ////vote.NoVoteWeightedPercentage = objPollDetails.TotalWeightedDidNotVotePercentage.GetValueOrDefault();
                    vote.Outcome = a.Outcome;
                    if (objPollDetails.StatusID == this.context.Poll_Status.FirstOrDefault(x => x.StatusDescription == "Accepted").StatusID)
                    {
                        vote.isAccepted = "Yes";
                    }
                    else
                    {
                        vote.isAccepted = "No";
                    }
                    ////vote.isAccepted = objPollDetails.StatusID == 4 ? "Yes" : "No";
                    lstVoteOutcome.Add(vote);
                }
            }

            datatTest.Columns.Add("Vote Number");
            datatTest.Columns.Add("Partner Vote Title");
            datatTest.Columns.Add("Vote Type");
            datatTest.Columns.Add("Resolutions");
            datatTest.Columns.Add("Vote For");
            datatTest.Columns.Add("Vote Against");
            datatTest.Columns.Add("Abstain");
            datatTest.Columns.Add("No Vote");
            datatTest.Columns.Add("Outcome");

            foreach (var a in lstVoteOutcome)
            {
                if (a.voteNumber > 0)
                {
                    datatTest.Rows.Add(
                                    a.voteNumber, 
                                    a.voteTitle, 
                                    a.voteType, 
                                    a.resolutionDescription, 
                                    a.VoteForSimpleCount, 
                                    a.VoteAgainstSimpleCount, 
                                    a.AbstainSimpleCount, 
                                    a.NoVoteSimpleCount, 
                                    a.Outcome);
                }
            }

            return datatTest;
        }

        /// <summary>
        /// Gets personnel list.
        /// </summary>
        /// <returns>return data table of partners</returns>
        public DataTable GetManagePersonnelList()
        {
            DataTable datatTest = new DataTable();

            var objUserDetails = (from UD in this.context.UserDetails
                                  join U in this.context.Users on UD.UserID equals U.UserID
                                  where UD.IsDeleted == false
                                  && U.IsEnabled == true
                                  select new
                                  {
                                      U.UserID,
                                      UD.FirstName,
                                      UD.LastName,
                                      U.Initial,
                                      UD.Title,
                                      UD.PartnerTypeID,
                                      UD.UserTypeID,
                                      UD.IsInternationalPartner,
                                      UD.OfficeLocation,
                                      UD.isResigned,
                                      UD.ResignationDate,
                                      UD.DateLeft,
                                      UD.EmailAddress,
                                      U.Department,
                                      UD.CreatedDate,
                                      UD.CreatedBy,
                                      UD.IsEmailNotificationRequired
                                  }).OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

            List<IExportPersonnelDetailsVM> lstpersonnelDetails = this.kernel.Get<List<IExportPersonnelDetailsVM>>();
            foreach (var a in objUserDetails)
            {
                if (a.FirstName != string.Empty || a.FirstName != null)
                {
                    IExportPersonnelDetailsVM personnelDetail = this.kernel.Get<IExportPersonnelDetailsVM>();
                    personnelDetail.Name = a.FirstName + " " + a.LastName;
                    personnelDetail.UserID = a.UserID;
                    personnelDetail.Initials = a.Initial;
                    personnelDetail.DateLeft = a.DateLeft;
                    personnelDetail.DateResigned = a.ResignationDate;
                    personnelDetail.EmailAddress = a.EmailAddress;
                    personnelDetail.Office = a.OfficeLocation;
                    personnelDetail.Title = a.Title;
                    personnelDetail.isEmailNotification = this.GetYesNoValue(a.IsEmailNotificationRequired);
                    personnelDetail.Department = a.Department;
                    personnelDetail.International = this.GetYesNoValue(a.IsInternationalPartner);
                    personnelDetail.Grade = this.GetGrade(a.PartnerTypeID);
                    personnelDetail.Department = a.Department;
                    personnelDetail.Resigned = this.GetYesNoValue(a.isResigned);
                    personnelDetail.AdminRights = this.GetAdminRights(a.UserTypeID);
                    lstpersonnelDetails.Add(personnelDetail);
                }
            }

            datatTest.Columns.Add("Name");
            datatTest.Columns.Add("Grade");
            datatTest.Columns.Add("Admin Rights");
            datatTest.Columns.Add("Int.");
            datatTest.Columns.Add("Office");
            datatTest.Columns.Add("Division");
            datatTest.Columns.Add("Resigned /Retired");
            datatTest.Columns.Add("Leaving Date");
            datatTest.Columns.Add("Email Address");
            datatTest.Columns.Add("Email Notif.");
            ////dtTest.Columns.Add("Last Modified Date");
            ////dtTest.Columns.Add("Modified By");
            foreach (var a in lstpersonnelDetails)
            {
                if (a.Name != null)
                {
                    datatTest.Rows.Add(
                                        a.Name, 
                                        a.Grade, 
                                        a.AdminRights, 
                                        a.International, 
                                        a.Office,
                                        a.Department, 
                                        a.Resigned, 
                                        a.DateLeft, 
                                        a.EmailAddress, 
                                        a.isEmailNotification);
                }
            }

            return datatTest;
        }

        /// <summary>
        /// Check admin right
        /// </summary>
        /// <param name="utype">user id</param>
        /// <returns>get user type</returns>
        public string GetAdminRights(int? utype)
        {
            var objAdminRights = this.context.User_Types.Find(utype);
            return objAdminRights.UserTypeDescription;
            ////if (utype == 1)
            ////{
            ////    return "Admin";
            ////}
            ////else if (utype == 2)
            ////{
            ////    return "Super Admin";
            ////}
            ////else if (utype == 3)
            ////{
            ////    return "N.A.";
            ////}
            ////else
            ////{
            ////    return "";
            ////}
        }

        /// <summary>
        /// Get partner grade.
        /// </summary>
        /// <param name="partner">partner id</param>
        /// <returns>return partner grade</returns>
        public string GetGrade(int? partner)
        {
            var objGrade = this.context.Partner_Types.Find(partner);
            return objGrade.PartnerTypeDescription;
            ////if (partner != null)
            ////{
            ////    if (partner == 1)
            ////    {
            ////        return "A";
            ////    }
            ////    else
            ////    {
            ////        return "B";
            ////    }

            ////}
            ////else
            ////{
            ////    return "N.A.";
            ////}
        }

        /// <summary>
        /// Get option answer (as Yes/No)
        /// </summary>
        /// <param name="isInt">question id</param>
        /// <returns>return result</returns>
        public string GetYesNoValue(bool? isInt)
        {
            string value = string.Empty;
            if (isInt != null)
            {
                if (isInt.GetValueOrDefault())
                {
                    value = "Yes";
                }
                else
                {
                    value = "No";
                }
            }
            else
            {
                value = string.Empty;
            }

            return value;
        }

        /// <summary>
        /// Get vote resolution
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <returns>list containing question stats</returns>
        public List<IQuestionStatisticsVM> GetVoteResolutions(int pollid)
        {
            var questions = this.context.Poll_Questions.Where(x => x.PollID == pollid && x.IsEnabled == true).OrderBy(x => x.Sequence).ToList();

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

                var results = this.context.Poll_Statistics.Where(x => x.QuestionID == q.QuestionID).ToList();
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
        /// Export to excel
        /// </summary>
        /// <param name="exportData">data to export</param>
        public void ToExcel(DataTable exportData)
        {
            ////var grid = new GridView();
            ////grid.DataSource = exportData;
            ////grid.DataBind();

            ////Response.ClearContent();
            ////Response.AddHeader("content-disposition", "attachment;filename=Partner List.xls");
            ////Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            ////StringWriter sw = new StringWriter();
            ////HtmlTextWriter htw = new HtmlTextWriter(sw);
            ////grid.RenderControl(htw);
            ////Response.Output.Write(sw.ToString());
            ////Response.Flush();
            ////Response.End();
        }
    }
}
