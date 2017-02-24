//-----------------------------------------------------------------------
// <copyright file="IPollManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Signature for Poll manager.
/// </summary>
namespace Amdox.IBusinessManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Amdox.DataModel;
    using Amdox.IBusinessManager;
    using Amdox.IDataModel;
    using Amdox.IDataModel.IViewModel;

    /// <summary>
    /// Poll manager business.
    /// </summary>
    public interface IPollManager
    {
        /// <summary>
        /// Accept a poll result.
        /// </summary>
        /// <param name="pollid">poll id</param>
        void AcceptResult(int pollid);

        /// <summary>
        /// Create a poll.
        /// </summary>
        /// <param name="pollSetupVM">poll setup view model</param>
        /// <param name="createdUserName">logged user name</param>
        /// <returns>poll id</returns>
        int CreatePoll(IPollSetupVM pollSetupVM, string createdUserName);

        /// <summary>
        /// Update vote setup.
        /// </summary>
        /// <param name="editVoteSetUpVM">edited vote details</param>
        /// <returns>vote id</returns>
        int UpdateVote(IEditVoteSetupVM editVoteSetUpVM);

        /// <summary>
        /// Get published poll.
        /// </summary>
        /// <returns>Publish Poll Object</returns>
        List<IPublishPollObject> GetPublishPollVM();

        /// <summary>
        /// Get partner vote.
        /// </summary>
        /// <param name="id">vote id</param>
        /// <returns>Publish Poll Object</returns>
        IPartnerVoteObject GetPartnerVote(int id);

        /// <summary>
        /// Get vote document.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>document view model</returns>
        List<IDocumentVM> GetVoteDocuments(int pollID);

        /// <summary>
        /// Get vote questions,
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>question view model</returns>
        List<IQuestionVM> GetVoteQuestions(int pollID);

        /// <summary>
        /// Get Vote details.
        /// used for  view poll.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>vote setup view model</returns>
        IViewVoteSetupVM GetVoteDetails(int pollID);

        /// <summary>
        /// Get poll details.
        /// currently using for sending email notification
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>publish poll object</returns>
        IPublishPollObject GetPollDetails(int pollID);

        /// <summary>
        /// Add poll users.
        /// </summary>
        /// <param name="pollId">poll id</param>
        void AddPollUsers(int pollId);

        /// <summary>
        /// Update vote questions.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <param name="objPollSetup">poll setup</param>
        void UpdateQuestions(int pollID, IEditVoteSetupVM objPollSetup);

        /// <summary>
        /// Add questions to poll.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <param name="objPollSetup">poll setup view model</param>
        void AddQuestions(int pollID, IPollSetupVM objPollSetup);

        /// <summary>
        /// Update vote details.
        /// used for  edit poll.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>edit vote view model</returns>
        IEditVoteSetupVM GetEditVoteDetails(int pollID);

        /// <summary>
        /// Add questions options.
        /// </summary>
        /// <param name="questionID">question id</param>
        void AddOptions(int questionID);

        /// <summary>
        /// Update poll when publish checked.
        /// </summary>
        /// <param name="id">vote id</param>
        /// <param name="val">selected value</param>
        void SaveCheckboxPublished(int id, bool val);

        /// <summary>
        /// Send open poll notifications.
        /// </summary>
        /// <param name="pollId">poll id</param>
        /// <returns>flag true/false</returns>
        bool SendOpenPollNotification(int pollId);

        /// <summary>
        /// Publish poll details.
        /// </summary>
        /// <param name="pollId">poll id</param>
        /// <returns>flag true/false</returns>
        bool SendPublishPollNotification(int pollId);

        /// <summary>
        /// Publish results.
        /// </summary>
        /// <param name="pollId">poll id</param>
        /// <returns>flag true/false</returns>
        bool SendPublishResultNotification(int pollId);

        /// <summary>
        /// Send partner notification.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <param name="voterUserID">vote user id</param>
        /// <returns>flag true/false</returns>
        bool SendPartnerReminderNotification(int pollID, int voterUserID);

        /// <summary>
        /// Update vote when checked open.
        /// </summary>
        /// <param name="id">vote id</param>
        /// <param name="val">selected value</param>
        void SaveCheckboxOpen(int id, bool val);

        /// <summary>
        /// Update vote when checked closed.
        /// </summary>
        /// <param name="id">vote id</param>
        /// <param name="val">selected value</param>
        void SaveCheckboxClosed(int id, bool val);

        /// <summary>
        /// Get vote options.
        /// </summary>
        /// <returns>get all options</returns>
        Dictionary<string, int> GetOptions();

        /// <summary>
        /// Get active users.
        /// </summary>
        /// <returns>list of all active user id</returns>
        List<int> GetActiveUserIds();

        /// <summary>
        /// Get active partners for vote.
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>list of IPartnerVoteObject</returns>
        List<IPartnerVoteObject> GetActivePartnerVotes(int userid);

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
        int AddDocumentDetails(int pollId, string filenameGuid, string filename, string path, string contentType, string userName);

        /// <summary>
        /// Save vote.
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="pollid">poll id</param>
        /// <param name="options">all options</param>
        /// <param name="proxyid">proxy id</param>
        /// <param name="votingtypeid">vote type id</param>
        void SaveVote(int userid, int pollid, List<int> options, int? proxyid, int votingtypeid);

        /// <summary>
        /// GEt old doc details for vote
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="pollid">poll id</param>
        /// <returns>old vote id</returns>
        List<int> GetOldVoteDetails(int userid, int pollid);

        /// <summary>
        /// Update poll document.
        /// </summary>
        /// <param name="documentID">document id</param>
        /// <returns>flag true/false</returns>
        bool UpdatePollDocuments(int documentID);

        /// <summary>
        /// Get vote result.
        /// </summary>
        /// <returns>result view model</returns>
        IVoteResultsVM GetIVoteResultsVM();

        /// <summary>
        /// Set question approval.
        /// </summary>
        /// <param name="questionid">question id</param>
        /// <param name="value">selected value</param>
        void SetQuestionApproval(int questionid, bool value);

        /// <summary>
        /// Get vote results for partners.
        /// </summary>
        /// <returns>vote result view model</returns>
        IVoteResultsVM GetIVoteResultsVMForPartner();

        /// <summary>
        /// Get track voting.
        /// </summary>
        /// <returns>track vote view model</returns>
        ITrackVotingVM GetTrackVotingVM();

        /// <summary>
        /// Get poll users.
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <returns>voter view model</returns>
        List<IVoterVM> GetPollUsers(int pollid);

        /// <summary>
        /// Get resolution count.
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>count of resolution</returns>
        int GetVoteResolutionCount(int pollID);

        /// <summary>
        /// Get partner vote
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="userName">user name</param>
        /// <returns>Partner vote view model</returns>
        IPartnervoteVM GetPartnerVoteVM(int? userid, string userName);

        /// <summary>
        /// Get Vote 
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <param name="userid">user id</param>
        /// <param name="isView">is view result</param>
        /// <param name="userName">user name</param>
        /// <returns>Voting view model</returns>
        IVotingVM GetVotingVM(int? pollid, int? userid, bool? isView, string userName);

        /// <summary>
        /// Get confidential track vote
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>TrackVoteDetails view model</returns>
        ITrackVoteDetailsVM GetConfidentialTrackVote(string userName);

        /// <summary>
        /// Get confidential vote details
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>IVoteConfidentialInfo view model</returns>
        List<IVoteConfidentialInfoVM> GetConfidentialVoteDetails(int pollID);

        /// <summary>
        /// Get vote status
        /// </summary>
        /// <param name="statusID">status id</param>
        /// <returns>return status</returns>
        string GetVoteStatus(int statusID);

        /// <summary>
        /// Get all partner vote
        /// </summary>
        /// <returns>list of Partner Vote Object</returns>
        List<IPartnerVoteObject> GetAllPartnerVotes();

        /// <summary>
        /// Get vote resolution
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <returns>return question stat view model</returns>
        List<IQuestionStatisticsVM> GetVoteResolutions(int pollid);
    }
}
