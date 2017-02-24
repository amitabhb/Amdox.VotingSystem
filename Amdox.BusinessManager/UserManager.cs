//-----------------------------------------------------------------------
// <copyright file="UserManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Amdox.DataModel;
using Amdox.DataModel.ViewModel;
using Amdox.IBusinessManager;
using Amdox.IDataModel;
using Amdox.IDataModel.IViewModel;
using Ninject;

namespace Amdox.BusinessManager
{
    public class UserManager : IUserManager
    {
        /// <summary>
        /// DI kernel.
        /// </summary>
        private IKernel _kernel;

        /// <summary>
        /// Data context
        /// </summary>
        [Inject]
        public VoteContext dataContext;

        
        /// <summary>
        /// Proxy view model.
        /// </summary>
        [Inject]
        private IAssignProxyVM assignProxyVM;

        /// <summary>
        /// Application path.
        /// </summary>
        private readonly string applicationPath = "http://CRSTIRANOG/VotingSystem";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager" /> class.
        /// </summary>
        /// <param name="assignProxyVM"></param>
        /// <param name="dataContext">data context</param>
        /// <param name="_kernel"></param>
        public UserManager(IAssignProxyVM assignProxyVM, VoteContext dataContext, IKernel kernel)
        {
            
            this.dataContext = dataContext;
            this.assignProxyVM = assignProxyVM;
            applicationPath = ConfigurationManager.AppSettings["AppPath"];
            this._kernel = kernel;
        }

        /// <summary>
        /// Get user id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int? GetUserID(string userName)
        {
            string strUserName = userName.ToLower();
            strUserName = Regex.Replace(strUserName, ".*\\\\(.*)", "$1", RegexOptions.None);
            var objUserDetails = (from U in this.dataContext.Users
                                  where U.UserName.Equals(strUserName)
                                  select new
                                  {
                                      U.UserID,
                                  }).ToList();

            if (objUserDetails.Count > 0)
            {
                return objUserDetails[0].UserID;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get user type.
        /// </summary>
        /// <param name="userCredentials">user name</param>
        /// <returns>user id</returns>
        public Tuple<int, string> GetUserType(string userName)
        {
            string strUserName = userName.ToLower();
            strUserName = Regex.Replace(strUserName, ".*\\\\(.*)", "$1", RegexOptions.None);
            var objUserDetails = (from UD in this.dataContext.UserDetails
                                  join U in this.dataContext.Users on UD.UserID equals U.UserID
                                  join UT in this.dataContext.User_Types on UD.UserTypeID equals UT.UserTypeID
                                  where UD.IsDeleted == false
                                  && U.IsEnabled == true
                                  && U.UserName.Equals(strUserName)
                                  select new
                                  {
                                      UD.UserTypeID,
                                      UT.UserTypeDescription

                                  }).ToList();
            if (objUserDetails.Count > 0)
            {
                Tuple<int, string> val = new Tuple<int, string>(objUserDetails[0].UserTypeID.GetValueOrDefault(), objUserDetails[0].UserTypeDescription);
                return val;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get partner type.
        /// </summary>
        /// <param name="userCredentials">partner user name</param>
        /// <returns>partner type</returns>
        public Tuple<int, string> GetPartnerType(string userName)
        {
            var objUserDetails = (from UD in this.dataContext.UserDetails
                                  join U in this.dataContext.Users on UD.UserID equals U.UserID
                                  join UP in this.dataContext.Partner_Types on UD.PartnerTypeID equals UP.PTID
                                  where UD.IsDeleted == false
                                  && U.IsEnabled == true
                                  && U.UserName.Equals(userName)
                                  select new
                                  {
                                      UD.PartnerTypeID,
                                      UP.PartnerTypeDescription
                                  }).ToList();
            if (objUserDetails.Count > 0)
            {
                Tuple<int, string> val = new Tuple<int, string>(objUserDetails[0].PartnerTypeID.GetValueOrDefault(), objUserDetails[0].PartnerTypeDescription);
                return val;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get personnel list.
        /// </summary>
        /// <returns>Personnel Object</returns>
        public List<IPersonnelObject> GetPersonnelList()
        {
            var objUserDetails = (from UD in this.dataContext.UserDetails
                                  join U in this.dataContext.Users on UD.UserID equals U.UserID
                                  where UD.IsDeleted == false
                                  && U.IsEnabled == true
                                  select new
                                  {
                                      U.UserID,
                                      UD.LastName,
                                      UD.FirstName,
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

                                  }).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();

            if (objUserDetails.Count > 0)
            {
                List<IPersonnelObject> PersonnelLIst = new List<IPersonnelObject>();
                foreach (var a in objUserDetails)
                {
                    PersonnelObject po = new PersonnelObject();
                    po.UserID = a.UserID;
                    po.LastName = a.LastName;
                    po.FirstName = a.FirstName;
                    po.Initials = a.Initial;
                    po.DateLeft = a.DateLeft;
                    po.DateResigned = a.ResignationDate;
                    po.EmailAddress = a.EmailAddress;
                    po.Office = a.OfficeLocation;
                    po.Title = a.Title;
                    po.isEmailNotification = a.IsEmailNotificationRequired.GetValueOrDefault();
                    po.Department = a.Department;

                    if (a.CreatedBy != null)
                    {
                        var userinfo = GetUserInfo(a.CreatedBy.GetValueOrDefault());
                        if(userinfo!=null)
                        {
                            po.LastModifiedUserid = userinfo.Initials;
                            po.LastModifiedDate = a.CreatedDate.GetValueOrDefault();
                        }
                    }

                    po.International = GetYesNoValue(a.IsInternationalPartner);
                    po.Grade = GetGrade(a.PartnerTypeID);
                    po.Department = a.Department;
                    po.Resigned = GetYesNoValue(a.isResigned);
                    po.AdminRights = GetAdminRights(a.UserTypeID);
                    PersonnelLIst.Add(po);
                }

                return PersonnelLIst;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get Admin right text.
        /// </summary>
        /// <param name="utype"></param>
        /// <returns></returns>
        private string GetAdminRights(int? utype)
        {
            if (utype == 1)
            {
                return "Admin";
            }
            else if (utype == 2)
            {
                return "Super Admin";
            }
            else if (utype == 3)
            {
                return "N.A.";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Will get partner grade.
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        private string GetGrade(int? partner)
        {
            if (partner != null)
            {
                if (partner == 1)
                {
                    return "A";
                }
                else
                {
                    return "B";
                }

            }
            else
            {
                return "N.A.";
            }
        }

        /// <summary>
        /// Will het the flag string.
        /// </summary>
        /// <param name="isInt"></param>
        /// <returns></returns>
        private string GetYesNoValue(bool? isInt)
        {
            if (isInt != null)
            {
                if (isInt.GetValueOrDefault())
                {
                    return ("Yes");
                }
                else
                {
                    return ("No");
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Get user info.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Personnel Object</returns>
        public IPersonnelObject GetUserInfo(int id)
        {
            var objUserDetails = (from UD in this.dataContext.UserDetails
                                  join U in this.dataContext.Users on UD.UserID equals U.UserID

                                  where UD.IsDeleted == false
                                  
                                  && U.UserID == id
                                  select new
                                  {
                                      U.UserID,
                                      U.UserName,
                                      UD.LastName,
                                      UD.CreatedBy,
                                      UD.CreatedDate,
                                      UD.FirstName,
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
                                      UD.IsEmailNotificationRequired
                                  }).ToList();

            PersonnelObject po = new PersonnelObject();

            if (objUserDetails.Count > 0)
            {
                po = new PersonnelObject();
                var a = objUserDetails[0];
                
                po.UserID = a.UserID;
                po.LastName = a.LastName;
                po.FirstName = a.FirstName;
                po.Initials = a.Initial;
                po.DateLeft = a.DateLeft;
                po.DateResigned = a.ResignationDate;
                po.EmailAddress = a.EmailAddress;
                po.Office = a.OfficeLocation;
                po.Title = a.Title;
                po.UserName = a.UserName;
                po.Department = a.Department;
                po.LastModifiedDate = a.CreatedDate.GetValueOrDefault();
                po.isEmailNotification = a.IsEmailNotificationRequired.GetValueOrDefault();

                if (a.IsInternationalPartner == true)
                {
                    po.International = "Y";
                }
                else
                {
                    po.International = "N";
                }

                if (a.PartnerTypeID == 1)
                {
                    po.Grade = "A";
                }
                else
                {
                    po.Grade = "B";
                }

                if (a.isResigned == true)
                {
                    po.Resigned = "Y";
                }
                else
                {
                    po.Resigned = "N";
                }

                if (a.UserTypeID == 1)
                {
                    po.AdminRights = "Y";
                }
                else if (a.UserTypeID == 2)
                {
                    po.AdminRights = "S";
                }
                else
                {
                    po.AdminRights = "N";
                }

                var proxy = GetProxy(a.UserID);

                if (proxy != null)
                {
                    var proxyobj = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == proxy && x.IsDeleted == false);
                    var proxyuserobj = this.dataContext.Users.FirstOrDefault(x => x.UserID == proxy);
                    po.proxy = proxyobj.Title + " " + proxyobj.FirstName + " " + proxyobj.LastName + " (" + proxyuserobj.Initial + ")";
                }
                else
                {
                    po.proxy = "";
                }
                
                return po;
            }
            else
            { 
                return null;
            }            
        }

        /// <summary>
        /// Save user info.
        /// </summary>
        /// <param name="po">Personnel Object</param>
        /// <param name="isnew">is new flag</param>
        /// <param name="modifiedUser">modified user</param>
        /// <returns>user id</returns>
        public int SaveUserInfo(IPersonnelObject po, bool isnew, string modifiedUser)
        {
            if (isnew)
            {
                var newuserrecord = new Amdox.DataModel.User();
                newuserrecord.IsEnabled = true;
                newuserrecord.IsAllowedToVote = true;

                this.dataContext.Users.Add(newuserrecord);
                this.dataContext.SaveChanges();
                po.UserID = newuserrecord.UserID;
            }

            var userdata = this.dataContext.Users.FirstOrDefault(x => x.UserID == po.UserID);
            var userdetailsolddata = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == po.UserID && x.IsDeleted == false);
            var userdetailsdata = new Amdox.DataModel.UserDetail();

            if (userdetailsolddata != null)
            {
                userdetailsolddata.IsDeleted = true;
                userdetailsdata.IsEmailNotificationRequired = userdetailsolddata.IsEmailNotificationRequired;
            }

            userdata.Initial = po.Initials;
            userdata.Department = po.Department;
            userdata.UserName = po.UserName;

            userdetailsdata.FirstName = po.FirstName;
            userdetailsdata.LastName = po.LastName;
            userdetailsdata.ResignationDate = po.DateResigned;
            userdetailsdata.DateLeft = po.DateLeft;
            userdetailsdata.EmailAddress = po.EmailAddress;
            userdetailsdata.OfficeLocation = po.Office;
            userdetailsdata.Title = po.Title;
            userdetailsdata.CreatedDate = DateTime.Now;
            userdetailsdata.CreatedBy = this.GetUserID(modifiedUser);
            userdetailsdata.UserID = po.UserID;
            userdetailsdata.IsDeleted = false;
            userdetailsdata.IsEmailNotificationRequired = po.isEmailNotification;

            if (po.Grade == "A")
            {
                userdetailsdata.PartnerTypeID = 1;

            }
            else if (po.Grade == "B")
            {
                userdetailsdata.PartnerTypeID = 2;
            }
            else
            {
                userdetailsdata.PartnerTypeID = null;
            }

            if (po.AdminRights == "Y")
            {
                userdetailsdata.UserTypeID = 1;
            }
            else if (po.AdminRights == "S")
            {
                userdetailsdata.UserTypeID = 2;
            }
            else
            {
                userdetailsdata.UserTypeID = 3;
            }

            if (po.International == "Y")
            {
                userdetailsdata.IsInternationalPartner = true;
            }
            else
            {
                userdetailsdata.IsInternationalPartner = false;
            }

            if (po.Resigned == "Y")
            {
                userdetailsdata.isResigned = true;
                userdata.IsAllowedToVote = false;
            }
            else
            {
                userdetailsdata.isResigned = false;
            }

            this.dataContext.UserDetails.Add(userdetailsdata);
            this.dataContext.SaveChanges();

            return po.UserID;
        }

        /// <summary>
        /// Get assigned proxy
        /// </summary>
        /// <returns>Assign Proxy VM</returns>
        public IAssignProxyVM GetAssignProxyVM()
        {
            var objUserDetails = (from UD in this.dataContext.UserDetails
                                  join U in this.dataContext.Users on UD.UserID equals U.UserID
                                  where UD.IsDeleted == false
                                  && U.IsEnabled == true && U.IsAllowedToVote == true && UD.isResigned == false
                                  select new
                                  {
                                      U.UserID,
                                      UD.LastName,
                                      UD.FirstName,
                                      U.Initial,
                                      UD.Title,
                                      UD.PartnerTypeID,
                                      UD.UserTypeID,
                                      UD.IsInternationalPartner,
                                      UD.OfficeLocation,
                                      UD.isResigned,
                                      UD.ResignationDate,
                                      UD.DateLeft,
                                      UD.EmailAddress

                                  }).OrderBy(x => x.FirstName).ToList();

            if (objUserDetails.Count > 0)
            {


                foreach (var a in objUserDetails)
                {
                    IPersonnelForProxyVM po = new PersonnelForProxyVM();
                    po.UserID = a.UserID;
                    po.Name = a.Title + ' ' + a.FirstName + ' ' + a.LastName;

                    po.Initials = a.Initial;

                    po.Office = a.OfficeLocation;
                    po.Title = a.Title;

                    int? proxyid = GetProxy(a.UserID);
                    if (proxyid != null)
                    {
                        var proxydetails = (from UD in this.dataContext.UserDetails
                                            join U in this.dataContext.Users on UD.UserID equals U.UserID
                                            where UD.IsDeleted == false && U.UserID == proxyid && U.IsAllowedToVote == true

                                            select new
                                            {
                                                U.UserID,
                                                UD.LastName,
                                                UD.FirstName,
                                                U.Initial,
                                                UD.Title,
                                                UD.PartnerTypeID,
                                                UD.UserTypeID,
                                                UD.IsInternationalPartner,
                                                UD.OfficeLocation,
                                                UD.isResigned,
                                                UD.ResignationDate,
                                                UD.DateLeft,
                                                UD.EmailAddress

                                            }).OrderBy(x => x.FirstName).ToList();

                        if (proxydetails.Count > 0)
                        {
                            po.ProxyInitials = proxydetails[0].Initial;
                            po.ProxyName = proxydetails[0].FirstName + " " + proxydetails[0].LastName + " (" + proxydetails[0].Initial + ")";

                        }
                    }

                    if (a.IsInternationalPartner == true)
                    {
                        po.International = "Y";
                    }
                    else
                    {
                        po.International = "N";
                    }

                    if (a.PartnerTypeID == 1)
                    {
                        po.Grade = "A";
                    }
                    else if (a.PartnerTypeID == 2)
                    {
                        po.Grade = "B";
                    }
                    else
                    {
                        po.Grade = "Admin";
                    }



                    if (a.UserTypeID == 1)
                    {
                        po.AdminRights = "Y";
                    }
                    else if (a.UserTypeID == 2)
                    {
                        po.AdminRights = "S";
                    }
                    else
                    {
                        po.AdminRights = "N";
                    }

                    proxyDropDownListClass pdd = new proxyDropDownListClass()
                    {
                        usrid = a.UserID,
                        usrname = a.FirstName + " " + a.LastName + " (" + a.Initial + ")"
                    };

                    this.assignProxyVM.proxyDropDownlist.Add(pdd);
                    this.assignProxyVM.proxyPersonnelList.Add(po);
                }
            }

            return this.assignProxyVM;
        }

        /// <summary>
        /// GEt proxy user.
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>proxy user id</returns>
        public int? GetProxy(int userid)
        {
            var objUserDetails = (from U in this.dataContext.Users
                                  join P in this.dataContext.Poll_ProxyUser on U.UserID equals P.UserId
                                  where P.IsEnabled == true && U.UserID == userid
                                  select new
                                  {
                                      P.ProxyUserId

                                  }).ToList();

            if (objUserDetails.Count > 0)
            {
                return objUserDetails[0].ProxyUserId;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Add proxy
        /// </summary>
        /// <param name="proxyid">proxy user id</param>
        /// <param name="userid">user id</param>
        /// <param name="createdUserName">created user name</param>
        public void AddProxy(int proxyid, int userid, string createdUser)
        {
            var proxyRecord = new Amdox.DataModel.Poll_ProxyUser();

            int? oldProxy = GetProxy(userid);

            if (oldProxy != null)
            {
                var oldProxyRec = this.dataContext.Poll_ProxyUser.FirstOrDefault(x => x.UserId == userid && x.IsEnabled == true);
                oldProxyRec.IsEnabled = false;

                SendProxyRemovedNotificationToProxy(oldProxy, userid);//mail to proxy saying he is removed as a proxy
            }

            proxyRecord.IsEnabled = true;
            proxyRecord.UserId = userid;
            proxyRecord.ProxyUserId = proxyid;
            proxyRecord.CreatedDate = DateTime.Now;
            proxyRecord.CreatedUser = this.GetUserID(createdUser);

            this.dataContext.Poll_ProxyUser.Add(proxyRecord);
            this.dataContext.SaveChanges();
            ////sends notification topartner and admin as cc
            SendProxyAssignedNotificationToPartner(proxyid, userid);
            ////sends notification to proxy and admin as cc
            SendProxyAssignedNotificationToProxy(proxyid, userid);
        }

        /// <summary>
        /// Send proxy user notification.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <param name="pollID">poll id</param>
        /// <returns>flag true/false</returns>
        public bool SendProxyCastVoteAlertToPartner(int? proxyID, int userID, int pollID)
        {
            bool success = false;
            StringBuilder MailBody = new StringBuilder();
            string strEmailSubject = Amdox.Common.Common.EmailText.ProxyVotedSubject;
            string strEmailBody = Amdox.Common.Common.EmailText.ProxyVotedEmailBody;

            //get proxy's firstname and last name
            var proxDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == proxyID);

            var pollDetail = this.dataContext.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollID);

            strEmailSubject = string.Format(strEmailSubject, pollDetail.PollTitle);
            strEmailBody = string.Format(strEmailBody, (proxDetail.FirstName + " " + proxDetail.LastName));
            //get partner's email id
            var partnerDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == userID);

            var AdminEmailDetails = from a in this.dataContext.Users
                                    join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                                    where b.IsDeleted == false &&
                                    b.IsEmailNotificationRequired == true &&
                                    b.UserTypeID == 1 || b.UserTypeID == 2
                                    select b;

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            foreach (var adminUser in AdminEmailDetails)
            {
                //send cc to admins
                List<string> CCEmailList = new List<string>();
                CCEmailList.Add(adminUser.EmailAddress);
            }
            List<string> toEmailList = new List<string>();
            toEmailList.Add(partnerDetail.EmailAddress);

            success = Amdox.Common.EmailService.SendEmail(null, strFromEmailID, null, strEmailSubject, strEmailBody, toEmailList);

            return success;

        }

        /// <summary>
        /// Send notification to partner when proxy is selected.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <returns>return true false</returns>
        public bool SendProxyAssignedNotificationToPartner(int proxyID, int userID)
        {
            bool success = false;
            StringBuilder MailBody = new StringBuilder();
            string strEmailSubject = Amdox.Common.Common.EmailText.NotifyPartnerSubject;
            string strEmailBody = Amdox.Common.Common.EmailText.NotifyPartnerEmailBody;

            //get proxy's firstname and last name
            var proxDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == proxyID);


            strEmailSubject = string.Format(strEmailSubject);
            strEmailBody = string.Format(strEmailBody, (proxDetail.FirstName + " " + proxDetail.LastName));
            //get partner's email id
            var partnerDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == userID);

            var AdminEmailDetails = from a in this.dataContext.Users
                                    join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                                    where b.IsDeleted == false &&
                                    b.IsEmailNotificationRequired == true &&
                                    b.UserTypeID == 1 || b.UserTypeID == 2
                                    select b;

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            foreach (var adminUser in AdminEmailDetails)
            {
                //send cc to admins
                List<string> CCEmailList = new List<string>();
                CCEmailList.Add(adminUser.EmailAddress);
            }
            List<string> toEmailList = new List<string>();
            toEmailList.Add(partnerDetail.EmailAddress);

            success = Amdox.Common.EmailService.SendEmail(null, strFromEmailID, null, strEmailSubject, strEmailBody, toEmailList);

            return success;

        }

        /// <summary>
        /// Send notification to proxy user when added.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <returns>flag true/false</returns>
        public bool SendProxyAssignedNotificationToProxy(int proxyID, int userID)
        {
            bool success = false;
            StringBuilder MailBody = new StringBuilder();
            string strEmailSubject = Amdox.Common.Common.EmailText.NotifyProxySubject;
            string strEmailBody = Amdox.Common.Common.EmailText.NotifyProxyEmailBody;

            ////get proxy's firstname and last name
            var proxDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == proxyID);

            ////get partner's details
            var partnerDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == userID);

            strEmailSubject = string.Format(strEmailSubject, (proxDetail.FirstName + " " + proxDetail.LastName));
            strEmailBody = string.Format(strEmailBody, (partnerDetail.FirstName + " " + partnerDetail.LastName), this.applicationPath);


            var AdminEmailDetails = from a in this.dataContext.Users
                                    join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                                    where b.IsDeleted == false &&
                                    b.IsEmailNotificationRequired == true &&
                                    b.UserTypeID == 1 || b.UserTypeID == 2
                                    select b;

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            foreach (var adminUser in AdminEmailDetails)
            {
                ////send cc to admins
                List<string> CCEmailList = new List<string>();
                CCEmailList.Add(adminUser.EmailAddress);
            }
            List<string> toEmailList = new List<string>();
            toEmailList.Add(proxDetail.EmailAddress);

            success = Amdox.Common.EmailService.SendEmail(null, strFromEmailID, null, strEmailSubject, strEmailBody, toEmailList);

            return success;

        }

        /// <summary>
        /// Will remove the proxy.
        /// </summary>
        /// <param name="userid">user id</param>
        public void RemoveProxy(int userid)
        {
            int? oldProxy = GetProxy(userid);

            if (oldProxy != null)
            {
                var oldProxyRec = this.dataContext.Poll_ProxyUser.FirstOrDefault(x => x.UserId == userid && x.IsEnabled == true);
                oldProxyRec.IsEnabled = false;
                SendProxyRemovedNotificationToProxy(oldProxy, userid);//mail to proxy saying he is removed as a proxy
            }

            this.dataContext.SaveChanges();
        }

        /// <summary>
        /// Get proxy user list.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns></returns>
        public IVoteProxyListVM GetVoteProxyListVM(string userName)
        {
            int? userid = GetUserID(userName);
            var _voteproxylist = _kernel.Get<IVoteProxyListVM>();
            if(userid!=null)
            {
                var ProxyList = (from PPU in this.dataContext.Poll_ProxyUser
                                 join UD in this.dataContext.UserDetails on PPU.UserId equals UD.UserID
                                 join U in this.dataContext.Users on UD.UserID equals U.UserID
                                 join PG in this.dataContext.Partner_Types on UD.PartnerTypeID equals PG.PTID
                                 where PPU.IsEnabled == true && UD.IsDeleted == false && U.IsEnabled == true && PPU.ProxyUserId == userid
                                 select new
                                 {
                                     U.UserID,
                                     U.Initial,
                                     UD.FirstName,
                                     UD.LastName,
                                     UD.Title,
                                     UD.PartnerTypeID,
                                     UD.OfficeLocation,
                                     UD.IsInternationalPartner,
                                     PG.PartnerTypeDescription
                                 }).ToList();

                if (ProxyList.Count > 0)
                {
                    foreach (var pl in ProxyList)
                    {
                        IPersonnelForProxyVM pp = new PersonnelForProxyVM();
                        pp.Grade = pl.PartnerTypeDescription;
                        pp.Name = pl.Title + " " + pl.FirstName + " " + pl.LastName;
                        if (pl.IsInternationalPartner == true)
                        {
                            pp.International = "Y";
                        }
                        else if (pl.IsInternationalPartner == false)
                        {
                            pp.International = "N";
                        }
                        else
                        {
                            pp.International = "";
                        }

                        pp.Initials = pl.Initial;
                        pp.Office = pl.OfficeLocation;
                        pp.UserID = pl.UserID;
                        _voteproxylist.proxyPersonnelList.Add(pp);

                    }
                    return _voteproxylist;
                }
                else
                {
                    return _voteproxylist;
                }
            }

            return _voteproxylist;
        }

        /// <summary>
        /// Send notification to proxy user when removed.
        /// </summary>
        /// <param name="proxyID">proxy id</param>
        /// <param name="userID">user id</param>
        /// <returns>flag true/false</returns>
        public bool SendProxyRemovedNotificationToProxy(int? proxyID, int userID)
        {
            bool success = false;
            StringBuilder MailBody = new StringBuilder();
            string strEmailSubject = Amdox.Common.Common.EmailText.ProxyRemovedNotifySubject;
            string strEmailBody = Amdox.Common.Common.EmailText.ProxyRemovedNotifyEmailBody;

            //get proxy's firstname and last name
            var proxDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == proxyID);

            //get partner's email id
            var partnerDetail = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == userID);

            strEmailSubject = string.Format(strEmailSubject, partnerDetail.FirstName + " " + partnerDetail.LastName);
            strEmailBody = string.Format(strEmailBody);


            var AdminEmailDetails = from a in this.dataContext.Users
                                    join b in this.dataContext.UserDetails on a.UserID equals b.UserID
                                    where b.IsDeleted == false &&
                                    b.IsEmailNotificationRequired == true &&
                                    b.UserTypeID == 1 || b.UserTypeID == 2
                                    select b;

            string strFromEmailID = ConfigurationManager.AppSettings["ITAdmin"];
            foreach (var adminUser in AdminEmailDetails)
            {
                //send cc to admins
                List<string> CCEmailList = new List<string>();
                CCEmailList.Add(adminUser.EmailAddress);
            }
            List<string> toEmailList = new List<string>();
            toEmailList.Add(proxDetail.EmailAddress);

            success = Amdox.Common.EmailService.SendEmail(null, strFromEmailID, null, strEmailSubject, strEmailBody, toEmailList);

            return success;
        }

        /// <summary>
        /// Save email preference for user.
        /// </summary>
        /// <param name="set">set data</param>
        /// <param name="value">value data</param>
        public void SaveEmailPreference(string[] set, bool value)
        {
            foreach (var s in set)
            {
                int i = int.Parse(s);
                var user = this.dataContext.UserDetails.FirstOrDefault(x => x.UserID == i && x.IsDeleted == false);
                user.IsEmailNotificationRequired = value;
            }

            this.dataContext.SaveChanges();
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user details</returns>
        public IEditUserVM GetEditUserVM(int? id)
        {
            var _editUserData = _kernel.Get<IEditUserVM>();
            if (id != null && id != 0)
            {
                IPersonnelObject po = GetUserInfo(id.GetValueOrDefault());
                if (po != null)
                {
                    _editUserData.isExists = true;
                    _editUserData.selectedTitle = _editUserData.GetValue(po.Title, _editUserData.TitleOptions);
                    _editUserData.International = _editUserData.GetValue(po.International, _editUserData.YesNoOptions);
                    _editUserData.Resigned = _editUserData.GetValue(po.Resigned, _editUserData.YesNoOptions);
                    _editUserData.UserID = po.UserID.ToString();
                    _editUserData.Grade = _editUserData.GetValue(po.Grade, _editUserData.GradeOptions);
                    _editUserData.AdminRights = _editUserData.GetValue(po.AdminRights, _editUserData.AdminRightsOptions);
                    _editUserData.DateLeft = po.DateLeft;
                    _editUserData.DateResigned = po.DateResigned;
                    _editUserData.EmailAddress = po.EmailAddress;
                    _editUserData.FirstName = po.FirstName;
                    _editUserData.LastName = po.LastName;
                    _editUserData.Initials = po.Initials;
                    _editUserData.Office = po.Office;
                    _editUserData.IsNew = false;
                    _editUserData.UserName = po.UserName;
                    _editUserData.Department = po.Department;
                    _editUserData.IsEmail = po.isEmailNotification;
                    _editUserData.PageTitle = "Edit User";

                }
                else
                {
                    _editUserData.PageTitle = "Edit User";
                    _editUserData.isExists = false;
                    _editUserData.IsNew = false;
                }


            }
            else if (id == null)
            {
                _editUserData.isExists = true;
                _editUserData.UserID = "(New User : Id not yet generated)";
                _editUserData.IsNew = true;
                _editUserData.PageTitle = "Add User";
            }

            return _editUserData;
        }

        /// <summary>
        /// Get proxy users.
        /// </summary>
        /// <param name="strUser">user name</param>
        /// <returns>Proxy VM></returns>
        public IMyProxyVM GetMyProxyVM(string strUser)
        {
            var proxyvmobject = _kernel.Get<IMyProxyVM>();

            var myuserid = GetUserID(strUser).GetValueOrDefault();
            proxyvmobject.MyDetails = GetUserInfo(myuserid);

            var proxy = this.dataContext.Poll_ProxyUser.FirstOrDefault(x => x.UserId == myuserid && x.IsEnabled == true);

            if ((proxy != null))
            {
                proxyvmobject.isProxy = true;

                proxyvmobject.myProxy = GetUserInfo(proxy.ProxyUserId.GetValueOrDefault());

            }
            else
            {
                proxyvmobject.isProxy = false;
            }

            var objUserDetails = (from UD in this.dataContext.UserDetails
                                  join U in this.dataContext.Users on UD.UserID equals U.UserID
                                  where UD.IsDeleted == false
                                  && U.IsEnabled == true && U.IsAllowedToVote == true && UD.isResigned == false
                                  select new
                                  {
                                      U.UserID,
                                      UD.LastName,
                                      UD.FirstName,
                                      U.Initial,
                                      UD.Title,
                                      UD.PartnerTypeID,
                                      UD.UserTypeID,
                                      UD.IsInternationalPartner,
                                      UD.OfficeLocation,
                                      UD.isResigned,
                                      UD.ResignationDate,
                                      UD.DateLeft,
                                      UD.EmailAddress

                                  }).OrderBy(x => x.FirstName).ToList();

            if (objUserDetails != null)
            {
                foreach (var a in objUserDetails)
                {
                    proxyDropDownListClass pdd = new proxyDropDownListClass()
                    {
                        usrid = a.UserID,
                        usrname = a.FirstName + " " + a.LastName + " (" + a.Initial + ")"
                    };

                    proxyvmobject.proxyDropDownlist.Add(pdd);
                }
            }

            return proxyvmobject;
        }

        /// <summary>
        /// Get new user.
        /// </summary>
        /// <param name="user">user id</param>
        /// <returns>User VM</returns>
        public INewUserVM GetNewUserVM(int user)
        {
            var newUserObject = _kernel.Get<INewUserVM>();
            var pollmanager = _kernel.Get<IPollManager>();
            newUserObject.VoteList = pollmanager.GetAllPartnerVotes();
            newUserObject.User = GetUserInfo(user);
            return newUserObject;
        }

        /// <summary>
        /// Add new user to poll.
        /// </summary>
        /// <param name="votes">vote data</param>
        /// <param name="userid">user id</param>
        public void AddNewUsertoPoll(string[] votes, int userid)
        {
            if(votes!=null)
            {
                foreach (var v in votes)
                {
                    int voteno = Convert.ToInt32(v);
                    var newpolluser = new Amdox.DataModel.Poll_Users();
                    newpolluser.PollId = voteno;
                    newpolluser.UserID = userid;
                    newpolluser.IsCompleted = false;
                    this.dataContext.Poll_Users.Add(newpolluser);
                }
            }
            
            this.dataContext.SaveChanges();
        }

        /// <summary>
        /// Disable user.
        /// </summary>
        /// <param name="userid">user id</param>
        public void DisableUser(string userid)
        {
            int user = Convert.ToInt32(userid);

            var userrecord = this.dataContext.Users.FirstOrDefault(x => x.UserID == user);
            userrecord.IsEnabled = false;
            this.dataContext.SaveChanges();
        }
    }
}
