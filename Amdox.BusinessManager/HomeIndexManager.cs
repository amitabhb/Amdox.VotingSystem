//-----------------------------------------------------------------------
// <copyright file="HomeIndexManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Home manager business.
/// </summary>
namespace Amdox.BusinessManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Amdox.DataModel;
    using Amdox.IBusinessManager;
    using Ninject;

    /// <summary>
    /// Home manager implementation.
    /// </summary>
    public class HomeIndexManager : IHomeIndexManager
    {
        /// <summary>
        /// Home Detail view model.
        /// </summary>
        [Inject]
        private IHomeIndexVM userHomeDetails;

        /// <summary>
        /// Data context.
        /// </summary>
        [Inject]
        private VoteContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeIndexManager" /> class.
        /// </summary>
        /// <param name="homeVM">Home view model</param>
        /// <param name="dataContext">data context</param>
        public HomeIndexManager(IHomeIndexVM homeVM, VoteContext dataContext)
        {
            this.userHomeDetails = homeVM;
            this.dataContext = dataContext;
        }

        /// <summary>
        /// GEt user info
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>returns home index data</returns>
        public IHomeIndexVM GetUserInfo(string userName)
        {
            ////TODO : Write the logic to get the NavigationVM.
            ////userDetails are picked up from userdetails and users table. 
            ////checks for isenabled,isdeleted and username-windows logged inuser ex ad\salim mandrekar
            userName = Regex.Replace(userName, ".*\\\\(.*)", "$1", RegexOptions.None);
            var objUserDetails = (from UD in this.dataContext.UserDetails
                                join U in this.dataContext.Users on UD.UserID equals U.UserID 
                                where UD.IsDeleted == false 
                                && U.IsEnabled == true
                                && U.UserName.Equals(userName)
                                select new
                                {
                                    UD.FirstName,
                                    UD.MiddleName,
                                    UD.LastName,
                                    UD.UserTypeID,
                                    U.UserName,
                                    UD.UserID,
                                    UD.UserDetailsID,
                                    UD.PartnerTypeID,                                   
                                }).ToList();
            if (objUserDetails.Count > 0)
            {
                foreach (var s in objUserDetails)
                {
                    this.userHomeDetails.UserId = s.UserID.GetValueOrDefault();
                    this.userHomeDetails.UserName = s.UserName;
                    this.userHomeDetails.PartnerType = s.PartnerTypeID.GetValueOrDefault();
                    this.userHomeDetails.UserType = s.UserTypeID.GetValueOrDefault();                    
                }
            }
            
             return this.userHomeDetails;        
        }
    }
}
