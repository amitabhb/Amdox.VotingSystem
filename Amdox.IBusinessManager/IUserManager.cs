//-----------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Amdox.IBusinessManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Amdox.DataModel;
    using Amdox.IDataModel;
    using Amdox.IDataModel.IViewModel;

    /// <summary>
    /// Signature for User Manager business.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Get user id by username.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>user id</returns>
        int? GetUserID(string userName);

        /// <summary>
        /// Get user type.
        /// </summary>
        /// <param name="userCredentials">user name</param>
        /// <returns>user id</returns>
        Tuple<int, string> GetUserType(string userCredentials);

        /// <summary>
        /// Get partner type.
        /// </summary>
        /// <param name="userCredentials">partner user name</param>
        /// <returns>partner type</returns>
        Tuple<int, string> GetPartnerType(string userCredentials);

        /// <summary>
        /// Get personnel list.
        /// </summary>
        /// <returns>Personnel Object</returns>
        List<IPersonnelObject> GetPersonnelList();

        /// <summary>
        /// Get user info.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Personnel Object</returns>
        IPersonnelObject GetUserInfo(int id);

        /// <summary>
        /// Save user info.
        /// </summary>
        /// <param name="po">Personnel Object</param>
        /// <param name="isnew">is new flag</param>
        /// <param name="modifiedUser">modified user</param>
        /// <returns>user id</returns>
        int SaveUserInfo(IPersonnelObject po, bool isnew, string modifiedUser);

        /// <summary>
        /// Get assigned proxy
        /// </summary>
        /// <returns>Assign Proxy VM</returns>
        IAssignProxyVM GetAssignProxyVM();

        /// <summary>
        /// GEt proxy user.
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>proxy user id</returns>
        int? GetProxy(int userid);

        /// <summary>
        /// Add proxy
        /// </summary>
        /// <param name="proxyid">proxy user id</param>
        /// <param name="userid">user id</param>
        /// <param name="createdUserName">created user name</param>
        void AddProxy(int proxyid, int userid, string createdUserName);

        /// <summary>
        /// Remove proxy user.
        /// </summary>
        /// <param name="userid">user id</param>
        void RemoveProxy(int userid);

        /// <summary>
        /// Send notification to partner when proxy is selected.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <returns>return true false</returns>
        bool SendProxyAssignedNotificationToPartner(int proxyID, int userID);

        /// <summary>
        /// Send notification to proxy user when added.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <returns>flag true/false</returns>
        bool SendProxyAssignedNotificationToProxy(int proxyID, int userID);

        /// <summary>
        /// GEt proxy user list.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>Vote Proxy List VM</returns>
        IVoteProxyListVM GetVoteProxyListVM(string userName);

        /// <summary>
        /// Send notification to proxy user when removed.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <returns>flag true/false</returns>
        bool SendProxyRemovedNotificationToProxy(int? proxyID, int userID);

        /// <summary>
        /// Send proxy user notification.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <param name="pollID">poll id</param>
        /// <returns>flag true/false</returns>
        bool SendProxyCastVoteAlertToPartner(int? proxyID, int userID, int pollID);

        /// <summary>
        /// Save email preference for user.
        /// </summary>
        /// <param name="set">set data</param>
        /// <param name="value">value data</param>
        void SaveEmailPreference(string[] set, bool value);

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user details</returns>
        IEditUserVM GetEditUserVM(int? id);

        /// <summary>
        /// Get proxy users.
        /// </summary>
        /// <param name="strUser">user name</param>
        /// <returns>Proxy VM></returns>
        IMyProxyVM GetMyProxyVM(string strUser);

        /// <summary>
        /// Get new user.
        /// </summary>
        /// <param name="user">user id</param>
        /// <returns>User VM</returns>
        INewUserVM GetNewUserVM(int user);

        /// <summary>
        /// Add new user to poll.
        /// </summary>
        /// <param name="votes">vote data</param>
        /// <param name="userid">user id</param>
        void AddNewUsertoPoll(string[] votes, int userid);

        /// <summary>
        /// Disable user.
        /// </summary>
        /// <param name="userid">user id</param>
        void DisableUser(string userid);
    }
}
